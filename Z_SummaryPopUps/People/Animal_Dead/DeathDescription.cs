// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal_Dead.DeathDescription
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal_Dead
{
  internal class DeathDescription
  {
    private CustomerFrame customerframe;
    private MiniHeading_V2 miniheading;
    private SimpleTextHandler text;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    public Vector2 location;

    public DeathDescription(
      float width,
      float basescale_,
      PrisonerInfo prisonerInfo,
      Player player)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.framescale.X = width;
      this.text = new SimpleTextHandler(DeathDescription.GetReasonOfDeathToDescriptionString(prisonerInfo.causeofdeath, prisonerInfo, player), false, (width - 2f * this.uiscale.DefaultBuffer.X) / Sengine.ReferenceScreenRes.X, this.basescale, false, false);
      this.text.AutoCompleteParagraph();
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.miniheading = new MiniHeading_V2("Description", this.basescale);
      float heightOfParagraph = this.text.GetHeightOfParagraph();
      this.framescale.Y = this.miniheading.GetSize_True().Y + this.uiscale.DefaultBuffer.Y + heightOfParagraph;
      this.customerframe = new CustomerFrame(this.framescale, CustomerFrameColors.Brown, this.basescale);
      this.text.Location.X = -0.5f * this.framescale.X + this.uiscale.DefaultBuffer.X;
      this.text.Location.Y = -0.5f * this.framescale.Y + this.miniheading.GetSize_True().Y;
      this.miniheading.SetPostionFromVSCale(this.framescale);
    }

    public void UpdateDeathDescription()
    {
    }

    public static string GetReasonOfDeathToDescriptionString(
      CauseOfDeath causeOfDeath,
      PrisonerInfo prisonerInfo,
      Player player)
    {
      switch (causeOfDeath)
      {
        case CauseOfDeath.Hunger:
          return "This animal died from hunger." + string.Format(" It went {0} days without food.", (object) prisonerInfo.DaysWithoutFood);
        case CauseOfDeath.KilledForFood:
          return "This animal was killed by another animal for food.";
        case CauseOfDeath.OldAge:
          return "This animal died due to old age.";
        case CauseOfDeath.euthanized:
          return "This animal was euthanized.";
        case CauseOfDeath.KilledInAnimalFight:
          return "This animal died in a fight with another animal.";
        case CauseOfDeath.KilledByThePolice:
          return "This animal was being shot by the police.";
        case CauseOfDeath.Sickness:
          return prisonerInfo.SicknessHasBeeDiagnosed ? string.Format("This animal died from {0} disease", (object) player.Stats.GetThisDisease(prisonerInfo.SicknessUID)) : "This animal died from an unknown disease.";
        case CauseOfDeath.Thirst:
          return "This animal died from thirst." + string.Format(" It went {0} days without water.", (object) prisonerInfo.DaysWithoutWater);
        default:
          return "Unknown";
      }
    }

    public void DrawDeathDescription(Vector2 offset)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, AssetContainer.pointspritebatchTop05);
      this.miniheading.DrawMiniHeading_V2(offset, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(offset, 1f, AssetContainer.pointspritebatchTop05);
    }

    public Vector2 GetSize() => this.framescale;
  }
}

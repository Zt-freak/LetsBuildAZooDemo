// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Sickness.AnimalSickStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Elements;
using TinyZoo.Z_Diseases;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Sickness
{
  internal class AnimalSickStatus
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler currentStatus;
    private SimpleTextHandler lastVisited;
    private LittleSummaryButton infoButton;
    private DiseaseIcon diseaseIcon;

    public AnimalSickStatus(
      PrisonerInfo prisonerInfo,
      Player player,
      float BaseScale,
      float width,
      float height = -1f)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Disease");
      Disease disease = (Disease) null;
      string str = "?";
      if (prisonerInfo.GetIsSick() && prisonerInfo.SicknessHasBeeDiagnosed)
      {
        disease = player.Stats.GetThisDisease(prisonerInfo.SicknessUID);
        str = disease.Name;
      }
      string TextToWrite1 = string.Format("Last Diagnosis: ~{0}", (object) str);
      string TextToWrite2 = "Last Checked: ~?";
      this.currentStatus = new SimpleTextHandler(TextToWrite1, width, _Scale: BaseScale, AutoComplete: true);
      this.currentStatus.SetAllColours(ColourData.Z_Cream);
      this.lastVisited = new SimpleTextHandler(TextToWrite2, width - defaultBuffer.X, _Scale: BaseScale, AutoComplete: true);
      this.lastVisited.SetAllColours(ColourData.Z_Cream);
      if (disease != null)
        this.diseaseIcon = new DiseaseIcon(BaseScale);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.X += defaultBuffer.X;
      zero.Y += defaultBuffer.Y * 0.5f;
      this.currentStatus.Location = zero;
      zero.Y += this.currentStatus.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y;
      this.lastVisited.Location = zero;
      zero.Y += this.lastVisited.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y * 0.5f;
      if (this.diseaseIcon != null)
      {
        this.diseaseIcon.vLocation = this.currentStatus.Location;
        this.diseaseIcon.vLocation.X += this.currentStatus.GetSize(true).X + defaultBuffer.X * 0.5f;
        this.diseaseIcon.vLocation.Y += this.currentStatus.GetHeightOfParagraph() * 0.5f;
      }
      zero.X = width;
      if ((double) height != -1.0)
        zero.Y = height;
      this.customerFrame.Resize(zero);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.currentStatus.Location += vector2;
      this.lastVisited.Location += vector2;
      if (this.diseaseIcon == null)
        return;
      DiseaseIcon diseaseIcon = this.diseaseIcon;
      diseaseIcon.vLocation = diseaseIcon.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateAnimalSickStatus()
    {
    }

    public void DrawAnimalSickStatus(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.currentStatus.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.lastVisited.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.diseaseIcon == null)
        return;
      this.diseaseIcon.DrawDiseaseIcon(offset, spriteBatch);
    }
  }
}

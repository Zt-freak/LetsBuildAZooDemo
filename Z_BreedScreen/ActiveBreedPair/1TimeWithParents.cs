// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPair.TimeWithParents
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPair
{
  internal class TimeWithParents
  {
    public Vector2 Location;
    public PriceAdjuster priceadjuster;
    private ZGenericText nursingHeading;
    private bool IsProduction;
    private bool IsCreate;
    private Parents_AndChild REF_parents_and_child;
    public float totalHeight;

    public TimeWithParents(
      Parents_AndChild _REF_parents_and_child,
      float BaseScale,
      bool _IsProduction = false,
      int ForceOption = 0)
    {
      this.totalHeight = 0.0f;
      this.REF_parents_and_child = _REF_parents_and_child;
      this.IsCreate = true;
      this.nursingHeading = new ZGenericText("NURSING PERIOD", BaseScale);
      this.IsProduction = _IsProduction;
      float y = this.nursingHeading.GetSize().Y;
      this.nursingHeading.vLocation.Y += y * 0.5f;
      this.totalHeight += y;
      int Max = 2;
      if (this.IsProduction)
      {
        Max = 11;
        this.nursingHeading.textToWrite = "PRODUCTION TARGET";
      }
      this.priceadjuster = new PriceAdjuster(0, Max, ForceOption, _BaseScale: BaseScale);
      this.priceadjuster.Location.Y = this.totalHeight + this.priceadjuster.GetSize().Y * 0.5f;
      this.totalHeight += this.priceadjuster.GetSize().Y;
      this.SetString();
    }

    internal static int GetProductionCapacityToValue(ProductionCapacity productiontarget) => productiontarget == ProductionCapacity.Endless ? 100000 : (int) productiontarget;

    internal static int GetNursingDayOptionToDays(TIMEWITHPARENTS nursingDayOption)
    {
      switch (nursingDayOption)
      {
        case TIMEWITHPARENTS.None:
          return 0;
        case TIMEWITHPARENTS.OneWeek:
          return 7;
        case TIMEWITHPARENTS.TwoWeeks:
          return 14;
        default:
          return 0;
      }
    }

    private void SetString()
    {
      float Length = 85f;
      if (this.IsProduction)
      {
        this.REF_parents_and_child.ProductionTargetOption = this.priceadjuster.CurrentValue;
        if (this.priceadjuster.CurrentValue == 0)
          this.priceadjuster.SetToString("Paused", Length, this.IsCreate);
        else if (this.priceadjuster.CurrentValue == 11)
          this.priceadjuster.SetToString("Infinite", Length, this.IsCreate);
        else
          this.priceadjuster.SetToString("X" + (object) this.priceadjuster.CurrentValue, Length, this.IsCreate);
      }
      else
      {
        this.REF_parents_and_child.NursingDaysOption = this.priceadjuster.CurrentValue;
        PlayerStats.LastSetNursingOption = this.priceadjuster.CurrentValue;
        switch (this.priceadjuster.CurrentValue)
        {
          case 0:
            this.priceadjuster.SetToString("none", Length, this.IsCreate);
            break;
          case 1:
            this.priceadjuster.SetToString("1 week", Length, this.IsCreate);
            break;
          case 2:
            this.priceadjuster.SetToString("2 weeks", Length, this.IsCreate);
            break;
        }
      }
      this.IsCreate = false;
    }

    public bool UpdateTimeWithParents(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      Offset.Y -= this.totalHeight * 0.5f;
      if (!this.priceadjuster.UpdatePriceAdjuster(player, Offset, DeltaTime))
        return false;
      this.SetString();
      if (this.IsProduction)
        PlayerStats.LastSetProductionTargetOption = this.priceadjuster.CurrentValue;
      else
        PlayerStats.LastSetNursingOption = this.priceadjuster.CurrentValue;
      return true;
    }

    public void DrawTimeWithParents(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      Offset.Y -= this.totalHeight * 0.5f;
      this.priceadjuster.DrawPriceAdjuster(Offset, spritebatch);
      this.nursingHeading.DrawZGenericText(Offset, spritebatch);
    }
  }
}

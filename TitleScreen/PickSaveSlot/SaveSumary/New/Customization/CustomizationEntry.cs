// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization.CustomizationEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization
{
  internal class CustomizationEntry
  {
    private Cust_SelectionToggle custselectintoggle;
    public Vector2 Location;
    private GameObject GoHeading;
    private Vector2 VScale;
    private TScreenFrame tscreenframe;
    private bool IsHeading;
    private string HEader;

    public CustomizationEntry(
      CustomizationOption customizationoption,
      float BaseScale,
      float RowHeight,
      bool RowOne,
      string Heading = "")
    {
      this.VScale = new Vector2(800f * BaseScale, RowHeight);
      if (Heading != "")
      {
        this.IsHeading = true;
        this.HEader = Heading;
        this.VScale.Y *= 0.5f;
      }
      else
      {
        this.custselectintoggle = new Cust_SelectionToggle(BaseScale, customizationoption);
        this.HEader = CustomizationData.GetCustomizationOptionToSTring(customizationoption);
      }
      this.tscreenframe = new TScreenFrame(BaseScale, IsDark: true);
      this.tscreenframe.VScale = this.VScale;
      if (RowOne)
        this.tscreenframe.tframe.SetAlpha(0.4f);
      this.GoHeading = new GameObject();
      this.GoHeading.SetAllColours(0.0f, 0.0f, 0.0f);
      this.GoHeading.vLocation.X = (float) (-(double) this.VScale.X * 0.5);
      this.GoHeading.vLocation.X += BaseScale * 10f;
      if (!this.IsHeading)
      {
        this.GoHeading.vLocation.Y = BaseScale * -7f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      else
      {
        this.GoHeading.vLocation.Y = BaseScale * -14f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.GoHeading.scale *= 2f;
      }
      this.GoHeading.SetAllColours(ColourData.Z_Cream);
    }

    public void UpdateCustomizationEntry(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      if (this.custselectintoggle == null)
        return;
      this.custselectintoggle.UpdateCust_SelectionToggle(player, DeltaTime, Offset);
    }

    public void DrawCustomizationEntry(Vector2 Offset)
    {
      Offset += this.Location;
      this.tscreenframe.DrawTScreenFrame(Offset);
      if (this.custselectintoggle != null)
        this.custselectintoggle.DrawCust_SelectionToggle(Offset);
      if (this.IsHeading)
        TextFunctions.DrawTextWithDropShadow(this.HEader, this.GoHeading.scale, this.GoHeading.vLocation + Offset, this.GoHeading.GetColour(), this.GoHeading.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03, false);
      else
        TextFunctions.DrawTextWithDropShadow(this.HEader, this.GoHeading.scale, this.GoHeading.vLocation + Offset, this.GoHeading.GetColour(), this.GoHeading.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03, false);
    }
  }
}

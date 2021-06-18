// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.BigNumberAndHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class BigNumberAndHeading
  {
    private NumberLerp numberlerper;
    private string Text;
    private BigTextType bigtexttype;
    private GameObject LowerTextObject;
    private Vector2 Position;
    private SpringFont LowerFont;
    private SpringFont TopFont;
    private GameObject TopTextObj;
    private float BaseScale;
    private string NumberPrefix;

    public BigNumberAndHeading(
      string HEader,
      int Number,
      float _BaseScale,
      BigTextType _bigtexttype,
      string _NumberPrefix = "$")
    {
      this.NumberPrefix = _NumberPrefix;
      this.BaseScale = _BaseScale;
      this.bigtexttype = _bigtexttype;
      this.Text = HEader;
      this.numberlerper = new NumberLerp();
      this.numberlerper.SetBasicNumberAndTarget(Number, 0.5f);
      this.TopTextObj = new GameObject();
      this.LowerTextObject = new GameObject();
      float baseScale = this.BaseScale;
      switch (this.bigtexttype)
      {
        case BigTextType.SmallText_BigNumber:
          this.TopFont = Z_GameFlags.GetSmallFont(ref baseScale);
          this.TopTextObj.scale = baseScale;
          this.LowerFont = AssetContainer.roundaboutFont;
          this.LowerTextObject.scale = this.BaseScale * 2f;
          this.LowerTextObject.vLocation.Y = this.BaseScale * 12f * Sengine.ScreenRatioUpwardsMultiplier.Y;
          break;
        case BigTextType.BigText_SmallNumder:
          this.LowerFont = Z_GameFlags.GetSmallFont(ref baseScale);
          this.LowerTextObject.scale = baseScale;
          this.TopFont = AssetContainer.SpringFontX1AndHalf;
          this.TopTextObj.scale = this.BaseScale * 3f;
          this.LowerTextObject.vLocation.Y = this.LowerTextObject.scale * 60f;
          break;
        case BigTextType.MediumText_MediumNumber:
          this.LowerFont = Z_GameFlags.GetSmallFont(ref baseScale);
          this.LowerTextObject.scale = baseScale;
          this.TopFont = Z_GameFlags.GetSmallFont(ref this.TopTextObj.scale);
          this.TopTextObj.scale *= 2f;
          this.LowerTextObject.scale *= 2f;
          this.LowerTextObject.vLocation.Y = this.LowerTextObject.scale * 40f;
          break;
      }
    }

    public void SetLocation(bool Top, bool Bottom)
    {
      this.Position = new Vector2((float) ((double) EndPOfWeekSummaryManager.SIZE * (double) EndPOfWeekSummaryManager.BaseScale * -0.5), (float) ((double) EndPOfWeekSummaryManager.SIZE * (double) EndPOfWeekSummaryManager.BaseScale * -0.5));
      this.Position.X += EndPOfWeekSummaryManager.BaseScale * 10f;
      if (Top)
      {
        this.Position.Y += EndPOfWeekSummaryManager.BaseScale * 20f;
        if (this.bigtexttype != BigTextType.SmallText_BigNumber)
          return;
        this.Position.Y = this.BaseScale * -90f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      else if (Bottom)
      {
        this.Position.Y *= -1f;
        this.Position.Y -= EndPOfWeekSummaryManager.BaseScale * 20f;
        if (this.bigtexttype != BigTextType.SmallText_BigNumber)
          return;
        this.Position.Y = this.BaseScale * 33f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      else
      {
        this.Position.Y = 0.0f;
        if (this.bigtexttype == BigTextType.SmallText_BigNumber)
        {
          this.Position.Y = this.BaseScale * -28f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        }
        else
        {
          int bigtexttype = (int) this.bigtexttype;
        }
      }
    }

    public void CheckValue(int NewValue)
    {
      if (NewValue == this.numberlerper.CurrentTargetValue_DoNotDraw)
        return;
      this.numberlerper.AddOrRemoveValue(NewValue - this.numberlerper.CurrentTargetValue_DoNotDraw);
    }

    public void UpdateBuigNumberAndHeading(float DeltaTime) => this.numberlerper.UpdateNumberLerp(DeltaTime);

    public void DrawBuigNumberAndHeading(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Position;
      TextFunctions.DrawTextWithDropShadow(this.Text, this.TopTextObj.scale, Offset + this.TopTextObj.vLocation, this.TopTextObj.GetColour(), this.TopTextObj.fAlpha, this.TopFont, spritebatch, false);
      TextFunctions.DrawTextWithDropShadow(this.NumberPrefix + (object) (int) this.numberlerper.CurrentTransitioningValue, this.LowerTextObject.scale, Offset + this.LowerTextObject.vLocation, this.LowerTextObject.GetColour(), this.LowerTextObject.fAlpha, this.LowerFont, spritebatch, false);
    }
  }
}

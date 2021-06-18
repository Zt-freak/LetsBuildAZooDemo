// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.LittleSummaryButton_AndHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_GenericUI
{
  internal class LittleSummaryButton_AndHeading
  {
    public Vector2 Location;
    private LittleSummaryButton summarybutton;
    private float BaseScale;
    private SimpleTextHandler text;
    private float textheight;
    private UIScaleHelper uiscale;
    private Vector2 framesize;

    public LittleSummaryButton_AndHeading(LittleSummaryButtonType summarytype, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.uiscale = new UIScaleHelper(this.BaseScale);
      this.summarybutton = new LittleSummaryButton(summarytype, _BaseScale: this.BaseScale);
      string NewHeading = "NA~NA";
      if (summarytype == LittleSummaryButtonType.CallSomeone)
        NewHeading = "Call~Someone";
      this.textheight = this.uiscale.ScaleY(AssetContainer.springFont.MeasureString("asdgfasgkljsvn").Y);
      this.framesize = this.summarybutton.GetSize();
      this.SetCustomHeading(NewHeading);
    }

    public Vector2 GetSize() => this.framesize;

    public Vector2 GetTextSize() => this.text.GetSize(true);

    public void SetCustomHeading(string NewHeading, float maxWidth = -1f)
    {
      float PercentagePfScreenWidth = 200f;
      if ((double) maxWidth != -1.0)
        PercentagePfScreenWidth = maxWidth;
      this.text = new SimpleTextHandler(NewHeading, true, PercentagePfScreenWidth, this.BaseScale);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      this.text.Location.Y = (float) ((double) this.summarybutton.DrawRect.Height * (double) this.summarybutton.scale * -0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.text.Location.Y -= this.text.GetHeightOfParagraph() * 0.5f;
      this.text.Location.Y -= this.text.GetHeightOfOneLine() * 0.5f;
    }

    public bool UpdateLittleSummaryButton_AndHeading(
      float DeltaTime,
      Player player,
      Vector2 Offset)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      return this.summarybutton.UpdateLittleSummaryButton(DeltaTime, player, Offset);
    }

    public void DrawLittleSummaryButton_AndHeading(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      this.text.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.summarybutton.vLocation = Vector2.Zero;
      this.summarybutton.DrawLittleSummaryButton(Offset);
    }
  }
}

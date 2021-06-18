// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Credits.GridOfRidiculousDeath.Credits.CreditsDisplayEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Rendering;

namespace TinyZoo.Settings.Credits.GridOfRidiculousDeath.Credits
{
  internal class CreditsDisplayEntry : GameObject
  {
    private bool IsHeading;
    private string DrawThis;
    private bool UseHeadingSpacer;
    private bool CheckTaps;

    public CreditsDisplayEntry(
      string Text,
      bool _IsHeading = false,
      bool _UseHeadingSpacer = true,
      bool _CheckTaps = false)
    {
      this.CheckTaps = _CheckTaps;
      this.UseHeadingSpacer = _UseHeadingSpacer;
      this.IsHeading = _IsHeading;
      this.DrawThis = Text;
      this.SetAllColours(1f, 1f, 1f);
      this.SetAllColours(ColourData.FernLemon);
    }

    public bool UpdateCreditsDisplayEntry(Vector2 Offset, Player player) => this.CheckTaps && (double) player.player.touchinput.MultiTouchTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.vLocation + Offset, 1f, 400f, 50f, player.player.touchinput.MultiTouchTapArray[0]);

    public void DrawCreditsDisplayEntry(Vector2 Offset) => TextFunctions.DrawJustifiedText(this.DrawThis, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);

    public void SetPositionsOnRotate(ref float Y)
    {
      float num = 10f;
      if (!Orientation.IsLandscape)
        num = 5f * Sengine.DifferenceInXWidthWhenInPortrait_ReductionMultiplierEquivilant;
      this.scale = OrientationMath.GetScaleToMatchRotation(3f);
      if (this.IsHeading)
      {
        this.scale = OrientationMath.GetScaleToMatchRotation(4f);
        if (this.UseHeadingSpacer)
          Y += this.scale * num;
      }
      this.vLocation = new Vector2(512f, Y);
      Y += this.scale * num;
    }
  }
}

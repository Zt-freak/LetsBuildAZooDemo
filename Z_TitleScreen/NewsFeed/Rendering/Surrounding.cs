// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.Rendering.Surrounding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_TitleScreen.NewsFeed.Rendering
{
  internal class Surrounding : GameObject
  {
    private Vector2 VScale;
    private GameObject tetx;
    private Vector2 DrawOffSet;
    private BackButton PreviousButton;
    private BackButton Close;
    private LerpHandler_Float lerper;

    public Surrounding(float BaseScale, float Height)
    {
      this.DrawOffSet.Y = 40f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      Height += this.DrawOffSet.Y;
      this.vLocation = new Vector2(100f, 150f);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetAlpha(0.5f);
      this.SetAllColours(0.0f, 0.0f, 0.0f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, Height, Height, 3f);
      this.VScale = new Vector2(300f, this.DrawOffSet.Y);
      this.scale = Z_GameFlags.GetBaseScaleForUI();
      this.tetx = new GameObject();
      this.tetx.SetAllColours(ColourData.GildedYellow);
      this.tetx.vLocation = new Vector2(25f * BaseScale, 15f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.PreviousButton = new BackButton(true, _IsPrevious: true);
      this.PreviousButton.vLocation = new Vector2(250f, 20f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.Close = new BackButton(true);
      this.Close.vLocation = new Vector2(280f, 20f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    public bool UpdateSurrounding(Player player, float DeltaTime, Vector2 Offset, out bool CLOSED)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      CLOSED = this.Close.UpdateBackButton(player, DeltaTime, this.vLocation);
      return (double) this.lerper.Value == (double) this.lerper.TargetValue && this.PreviousButton.UpdateBackButton(player, DeltaTime, this.vLocation);
    }

    public Vector2 GetDrawLocation() => this.vLocation + this.DrawOffSet;

    public void SetNewHeight(float Height) => this.lerper.SetLerp(false, Height, Height, 3f, true);

    public void DrawSurrounding(Vector2 Offset, int Value, int All)
    {
      this.VScale = new Vector2(300f, this.DrawOffSet.Y + this.lerper.Value);
      this.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VScale);
      TextFunctions.DrawTextWithDropShadow("News Feed " + (object) (Value + 1) + "/" + (object) All, this.scale * 1f, Offset + this.vLocation + this.tetx.vLocation, this.tetx.GetColour(), this.tetx.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03, false);
      this.PreviousButton.DrawBackButton(Offset + this.vLocation, AssetContainer.pointspritebatch03);
      this.Close.DrawBackButton(Offset + this.vLocation, AssetContainer.pointspritebatch03);
    }
  }
}

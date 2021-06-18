// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BetaEnd.BetaBanner
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_BetaEnd
{
  internal class BetaBanner
  {
    private GameObject whitebox;
    private Vector2 VSCALE;

    public BetaBanner()
    {
      this.whitebox = new GameObject();
      this.whitebox.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.whitebox.SetDrawOriginToCentre();
      this.whitebox.SetAlpha(0.4f);
    }

    public void UpdateBetaBar()
    {
    }

    public void DrawBetaBanner()
    {
      this.whitebox.vLocation = new Vector2(880f, 80f);
      if (TinyZoo.Game1.gamestate == GAMESTATE.WorldMap)
        this.whitebox.vLocation = new Vector2(150f, 740f);
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      SpringFont smallFont = Z_GameFlags.GetSmallFont(ref baseScaleForUi);
      this.VSCALE = new Vector2(baseScaleForUi * 300f, baseScaleForUi * 30f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.whitebox.SetAlpha(0.6f);
      this.whitebox.Draw(AssetContainer.pointspritebatch07Final, AssetContainer.SpriteSheet, Vector2.Zero, this.VSCALE);
      TextFunctions.DrawJustifiedText("EARLY BETA VERSION", baseScaleForUi * 2f, this.whitebox.vLocation + new Vector2(0.0f, baseScaleForUi * 4f), Color.Black, 0.8f, smallFont, AssetContainer.pointspritebatch07Final);
    }
  }
}

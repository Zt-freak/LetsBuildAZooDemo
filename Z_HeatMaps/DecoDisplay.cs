// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.DecoDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HeatMaps
{
  internal class DecoDisplay
  {
    private GameObject DecoSector;
    private string DecoVal;
    private int Efficiency;
    private Vector2 TextLocation;
    private DecoRegularPanel regular;
    private DecoCondensedPanel condensed;
    private BigBrownPanel panel;
    private UIScaleHelper scalehelper;
    private float basescale;
    private Vector2 framescale;
    private float maxscrollscale;
    private LerpHandler_Float lerper;
    private DecoDisplay.DecoDisplayMode smallmode;

    public DecoDisplay(int XLoc, int YLoc, float basescale_)
    {
      this.smallmode = DecoDisplay.DecoDisplayMode.None;
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.lerper = new LerpHandler_Float();
      this.DecoSector = new GameObject();
      this.DecoSector.SetAllColours(0.2f, 0.6f, 0.2f);
      this.DecoSector.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.DecoSector.SetAlpha(0.6f);
      this.DecoVal = string.Concat((object) Math.Round((double) DecoCalculator.GetThisValue(XLoc, YLoc), 1));
      this.DecoSector.scale = (float) TileMath.SectorSize * TileMath.TileSize;
      this.Efficiency = (int) Math.Round((double) DecoCalculator.GetThisEfficiency(XLoc, YLoc) * 100.0, 1);
      if (XLoc % 2 == 0 && YLoc % 2 == 0)
        this.DecoSector.SetAllColours(0.1f, 0.5f, 0.1f);
      else if (XLoc % 2 == 1 && YLoc % 2 == 1)
        this.DecoSector.SetAllColours(0.1f, 0.5f, 0.1f);
      this.regular = new DecoRegularPanel(XLoc, YLoc, this.basescale);
      this.condensed = new DecoCondensedPanel(XLoc, YLoc, this.basescale);
      this.framescale = this.regular.GetSize();
      this.panel = new BigBrownPanel(this.framescale, _BaseScale: this.basescale);
      this.framescale = this.regular.GetSize();
      this.panel.Finalize(this.framescale);
      this.DecoSector.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(XLoc * TileMath.SectorSize, YLoc * TileMath.SectorSize));
      this.DecoSector.scale = (float) TileMath.SectorSize * TileMath.TileSize;
      this.DecoSector.vLocation.X -= TileMath.HalfTileSize;
      this.DecoSector.vLocation.Y -= TileMath.HalfTileSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.TextLocation = this.DecoSector.vLocation;
      this.TextLocation += new Vector2((float) ((double) TileMath.SectorSize * (double) TileMath.TileSize * 0.5), (float) ((double) TileMath.SectorSize * (double) TileMath.TileSize * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    public void UpdateDecoDisplay(float DeltaTime)
    {
      int smallmode1 = (int) this.smallmode;
      float num1 = TileMath.TileSize * (float) TileMath.SectorSize * Sengine.WorldOriginandScale.Z;
      this.smallmode = (double) this.regular.GetSize().X >= 0.899999976158142 * (double) num1 ? DecoDisplay.DecoDisplayMode.Condensed : DecoDisplay.DecoDisplayMode.Full;
      int smallmode2 = (int) this.smallmode;
      if (smallmode1 != smallmode2)
      {
        if (this.smallmode != DecoDisplay.DecoDisplayMode.Full)
          this.lerper.SetLerp(true, 0.0f, 1f, 3f);
        else
          this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      }
      if (!this.lerper.IsComplete())
      {
        Vector2 size1 = this.regular.GetSize();
        Vector2 size2 = this.condensed.GetSize();
        float num2 = this.lerper.Value;
        this.framescale = (1f - num2) * size1 + num2 * size2;
        this.panel.Finalize(this.framescale);
      }
      this.lerper.UpdateLerpHandler(DeltaTime);
    }

    public void DrawDecoDisplay()
    {
      this.DecoSector.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
      this.panel.DrawBigBrownPanel(RenderMath.TranslateWorldSpaceToScreenSpace(this.TextLocation), AssetContainer.pointspritebatch03);
      float num = 3f * this.lerper.Value;
      this.condensed.DrawDecoDisplay(AssetContainer.pointspritebatch03, RenderMath.TranslateWorldSpaceToScreenSpace(this.TextLocation), MathHelper.Clamp(num, 0.0f, 1f));
      this.regular.DrawDecoDisplay(AssetContainer.pointspritebatch03, RenderMath.TranslateWorldSpaceToScreenSpace(this.TextLocation), MathHelper.Clamp(1f - num, 0.0f, 1f));
    }

    private enum DecoDisplayMode
    {
      None = -1, // 0xFFFFFFFF
      Condensed = 0,
      Full = 1,
    }
  }
}

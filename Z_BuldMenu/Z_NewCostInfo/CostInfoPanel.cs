// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.CostInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.Z_NewCostInfo.PenBuildInfo;
using TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo
{
  internal class CostInfoPanel
  {
    private float BaseScale;
    private BigBrownPanel BigFrame;
    private LerpHandler_Float lerper;
    private Vector2 Location;
    private PenBuildInfoManager peninfomanager;
    private StatsCostManager statsinfo;
    public TILETYPE tiletype;
    public bool CanAfford = true;

    public CostInfoPanel(
      CATEGORYTYPE category,
      TILETYPE _tiletype,
      Player player,
      bool CanPlayerBuildThis)
    {
      this.tiletype = _tiletype;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.BigFrame = new BigBrownPanel(Vector2.Zero, true, "Build", this.BaseScale);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.statsinfo = new StatsCostManager(this.tiletype, this.BaseScale, player, CanPlayerBuildThis);
      this.BigFrame.Finalize(this.statsinfo.GetSize());
      this.Location.X = (float) (1024.0 - (double) this.BigFrame.vScale.X * 0.5);
      this.Location.X -= uiScaleHelper.ScaleX(30f);
      this.Location.Y = (float) (768.0 - (double) this.BigFrame.vScale.Y * 0.5) - this.BigFrame.InternalOffset.Y;
      this.Location.Y -= uiScaleHelper.ScaleY(30f);
    }

    public bool CheckMouseOver(Player player)
    {
      Vector2 offset = new Vector2(this.lerper.Value * 768f, 0.0f) + this.Location;
      return this.BigFrame.CheckMouseOver(player, offset);
    }

    public bool UpdateCostInfoPanel(Player player, float DeltaTime, Z_PenBuilder z_penbuilder)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      int TileCount = -1;
      this.CanAfford = true;
      if (this.statsinfo != null)
        this.CanAfford = this.statsinfo.GetCanAfford();
      if (this.peninfomanager != null && z_penbuilder != null)
      {
        this.peninfomanager.UpdatePenBuildInfoManager(player, z_penbuilder.GetVolume(), z_penbuilder, DeltaTime);
        TileCount = z_penbuilder.GetVolume();
      }
      if (z_penbuilder != null)
        TileCount = z_penbuilder.GetVolume();
      if (this.statsinfo != null)
        this.statsinfo.UpdateStatsCostManager(player, DeltaTime, this.Location, TileCount, z_penbuilder);
      return (double) this.lerper.Value == 0.0 && this.BigFrame.UpdatePanelCloseButton(player, DeltaTime, this.Location);
    }

    public void FlashRedCantAfford()
    {
      if (this.statsinfo == null)
        return;
      this.statsinfo.FlashRedCantAfford();
    }

    public void DrawCostInfoPanel()
    {
      Vector2 vector2 = new Vector2(this.lerper.Value * 768f, 0.0f) + this.Location;
      this.BigFrame.DrawBigBrownPanel(vector2, AssetContainer.pointspritebatchTop05);
      if (this.peninfomanager != null)
        this.peninfomanager.DrawPenBuildInfoManager(vector2, AssetContainer.pointspritebatchTop05);
      if (this.statsinfo == null)
        return;
      this.statsinfo.DrawStatsCostManager(vector2, AssetContainer.pointspritebatchTop05);
    }
  }
}

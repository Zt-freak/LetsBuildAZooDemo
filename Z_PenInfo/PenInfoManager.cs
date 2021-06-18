// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.PenInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_ManagePen.PenInfoPanel;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_PenInfo
{
  internal class PenInfoManager
  {
    private MainBarManager mainbarmanager;
    internal static bool RemakeThisBarNow;
    private BAR_TYPE bartype;
    private bool HasTransfer;
    private TILETYPE tiletype;
    private PenInfoFrameManager pendetailsframe;

    public PenInfoManager(
      BAR_TYPE _bartype,
      bool _HasTransfer,
      TILETYPE _tiletype,
      Player player,
      PrisonZone prisonzone)
    {
      if (OverWorldManager.zoopopupHolder.ScrubOnOpenNewBar(_bartype, _tiletype, player))
        OverWorldManager.zoopopupHolder.SetNull();
      this.bartype = _bartype;
      this.HasTransfer = _HasTransfer;
      this.tiletype = _tiletype;
      if (prisonzone != null && !prisonzone.IsFarm)
      {
        this.pendetailsframe = new PenInfoFrameManager(prisonzone, player);
        this.pendetailsframe.Location = new Vector2(850f, 480f);
      }
      this.Create(player);
      if (this.tiletype != TILETYPE.WaterPumpStation)
        return;
      Z_GameFlags.SetHeatMapType(HeatMapType.Water);
    }

    public void AddTransferButton() => this.mainbarmanager.AddTransferButton();

    public bool IsMouseOverButton(Player player) => this.pendetailsframe != null && this.pendetailsframe.CheckMouseOver(player, Vector2.Zero) || this.mainbarmanager.CheckMouseOver(player);

    public void UpdateTempExitLerp(float DeltaTime) => this.mainbarmanager.UpdateTempExitLerp(DeltaTime);

    private void Create(Player player)
    {
      if (!PenInfoManager.RemakeThisBarNow && this.mainbarmanager != null && this.mainbarmanager.bartypeonConstrust == this.bartype)
        return;
      PenInfoManager.RemakeThisBarNow = false;
      this.mainbarmanager = new MainBarManager(this.bartype, this.tiletype, this.HasTransfer, player);
    }

    public BuildingManageButton UpdatePenInfoManager(
      float DeltaTime,
      Player player,
      out bool GoBack)
    {
      if (PenInfoManager.RemakeThisBarNow)
        this.Create(player);
      bool SkipMainBar = false;
      if (this.pendetailsframe != null)
      {
        if (this.pendetailsframe.UpdatePenInfoManager(player, DeltaTime, Vector2.Zero, out SkipMainBar))
        {
          this.pendetailsframe = (PenInfoFrameManager) null;
          this.Create(player);
        }
        else if (SkipMainBar)
        {
          Z_GameFlags.MouseIsOverAPanel = true;
          GoBack = false;
          return BuildingManageButton.Count;
        }
      }
      return this.mainbarmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
    }

    public void TempLerpOff() => this.mainbarmanager.TempLerpOff();

    public void LerpBackOn() => this.mainbarmanager.LerpBackOn();

    public void DrawPenInfoManager(Player player)
    {
      if (PenInfoManager.RemakeThisBarNow)
        return;
      this.mainbarmanager.DrawMainBarManager(player);
      if (this.pendetailsframe == null)
        return;
      this.pendetailsframe.DrawPenInfoManager(Vector2.Zero, AssetContainer.pointspritebatchTop05);
    }
  }
}

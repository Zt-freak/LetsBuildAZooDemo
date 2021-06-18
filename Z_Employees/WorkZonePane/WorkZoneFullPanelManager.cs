// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.WorkZonePane.WorkZoneFullPanelManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.Tile_Data;
using TinyZoo.Z_EditZone;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Employees.WorkZonePane
{
  internal class WorkZoneFullPanelManager
  {
    private BigBrownPanel bigpanel;
    private WorkZonePanelManager workzoneselectionpanel;
    public Vector2 Location;
    private WorkZonePanelManager workzonemanager;
    private TILETYPE buildingtype;
    private SelectedAndSellManager selectedtileandsell;
    private LerpHandler_Float lerper;

    public WorkZoneFullPanelManager(
      float BaseScale,
      WorkZoneInfo workzoneinfo,
      TILETYPE _buildingtype = TILETYPE.Count,
      SelectedAndSellManager _selectedtileandsell = null)
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      FeatureFlags.BlockAllUI = true;
      this.buildingtype = _buildingtype;
      float EdgeBuffer = 10f;
      this.workzonemanager = new WorkZonePanelManager(BaseScale, workzoneinfo, EdgeBuffer, _buildingtype);
      this.bigpanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Activity Zone", BaseScale);
      this.bigpanel.Finalize(this.workzonemanager.GetSize());
      this.selectedtileandsell = _selectedtileandsell;
      if (this.selectedtileandsell != null)
        this.selectedtileandsell.TempLerpOff();
      this.Location = new Vector2(this.bigpanel.vScale.X * 0.5f, 450f);
      this.Location.X += 10f * BaseScale;
    }

    public bool CheckMouseOver(Player player) => this.bigpanel.CheckMouseOver(player, this.Location);

    public bool UpdateWorkZoneFullPanelManager(Player player, float DeltaTime, Vector2 Offset)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.selectedtileandsell != null)
        this.selectedtileandsell.UpdateTempExitLerp(DeltaTime);
      Offset += this.Location;
      if (this.bigpanel.UpdatePanelCloseButton(player, DeltaTime, Offset))
      {
        if (this.selectedtileandsell != null)
          this.selectedtileandsell.LerpBackOn();
        FeatureFlags.BlockAllUI = false;
        return true;
      }
      bool EditZone = false;
      if ((double) this.lerper.Value == 0.0)
        this.workzonemanager.UpdateWorkZonePanelManager(player, DeltaTime, Offset, out EditZone);
      if (!EditZone)
        return false;
      OverWorldManager.overworldstate = OverWOrldState.EditZone;
      OverWorldManager.zoneEditManager = new ZoneEditManager(this.workzonemanager.workzoneinfo, player, this.buildingtype, this.selectedtileandsell);
      return true;
    }

    public void DrawWorkZoneFullPanelManager(Vector2 Offset)
    {
      Offset += this.Location;
      Offset.X += this.lerper.Value * -512f;
      this.bigpanel.DrawBigBrownPanel(Offset);
      this.workzonemanager.DrawWorkZonePanelManager(Offset);
    }
  }
}

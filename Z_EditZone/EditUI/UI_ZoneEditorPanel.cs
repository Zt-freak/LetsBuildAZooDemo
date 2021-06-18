// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.EditUI.UI_ZoneEditorPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.Tile_Data;
using TinyZoo.Z_EditZone.EditUI.InflienceInfo;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_EditZone.EditUI
{
  internal class UI_ZoneEditorPanel
  {
    private BigBrownPanel mainPanel;
    private InfluenceInfoManager influencemanager;
    public Vector2 Location;
    private SelectedAndSellManager selectedtileandsell;
    private LerpHandler_Float lerper;

    public UI_ZoneEditorPanel(
      WorkZoneInfo _workzoneinfo,
      float BaseScale,
      TILETYPE buildingtype = TILETYPE.Count,
      SelectedAndSellManager _selectedtileandsell = null)
    {
      FeatureFlags.BlockAllUI = true;
      this.selectedtileandsell = _selectedtileandsell;
      this.mainPanel = new BigBrownPanel(new Vector2(100f, 100f), true, "Set Influence Zone", BaseScale);
      this.influencemanager = new InfluenceInfoManager(BaseScale, _workzoneinfo, 10f, buildingtype);
      this.mainPanel.Finalize(this.influencemanager.customerframe.VSCale);
      this.Location = new Vector2(this.mainPanel.vScale.X * 0.5f, 450f);
      this.Location.X += BaseScale * 10f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public bool CheckMouseOver(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      return this.mainPanel.CheckMouseOver(player, Offset);
    }

    public void SetRed() => this.influencemanager.SetRed();

    public void UpdateUI_ZoneEditorPanel(Player player, float DeltaTime, Vector2 Offset)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset += this.Location;
      Z_GameFlags.ForceRightAndLeftMouseDrag = true;
      if (this.selectedtileandsell != null)
        this.selectedtileandsell.UpdateTempExitLerp(DeltaTime);
      if (this.mainPanel.UpdatePanelCloseButton(player, DeltaTime, Offset))
      {
        Z_GameFlags.ForceRightAndLeftMouseDrag = false;
        OverWorldManager.overworldstate = OverWOrldState.MainMenu;
        FeatureFlags.BlockAllUI = false;
        if (this.selectedtileandsell != null)
          this.selectedtileandsell.LerpBackOn();
      }
      this.influencemanager.UpdateInfluenceInfoManager(Offset, player, DeltaTime);
    }

    public void DrawUI_ZoneEditorPanel(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      Offset.X += this.lerper.Value * -512f;
      this.mainPanel.DrawBigBrownPanel(Offset);
      this.influencemanager.DrawInfluenceInfoManager(Offset, spritebatch);
    }
  }
}

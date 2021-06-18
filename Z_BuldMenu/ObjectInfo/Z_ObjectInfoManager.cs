// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.ObjectInfo.Z_ObjectInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.ObjectInfo
{
  internal class Z_ObjectInfoManager
  {
    private Vector2 VSCALE;
    private GameObjectNineSlice Frame;
    private LerpHandler_Float lerper;
    private Vector2 Offset;
    private TextButton COST;
    private bool IsGreen;
    private string HEADING;
    private GameObject TextClObj;
    private bool CanAfford;
    private int cost;

    public Z_ObjectInfoManager()
    {
      Vector3 SecondaryColour;
      this.Frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.VSCALE = new Vector2(175f, 768f - Z_BuildingIconPanel.MinHeight);
      this.Frame.vLocation = new Vector2(512f, (float) ((768.0 - (double) Z_BuildingIconPanel.MinHeight) * 0.5));
      this.Frame.vLocation.X = (float) (1024.0 - (double) this.VSCALE.X * 0.5);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.TextClObj = new GameObject();
      this.TextClObj.SetAllColours(SecondaryColour);
    }

    public void Reactivate() => this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);

    public void ChangeItem(Player player, TILETYPE tiletobuild, bool Locked, int Duplicates = 1)
    {
      this.cost = player.livestats.GetCost(tiletobuild, player, true) * Duplicates;
      TileStats tileStats = TileData.GetTileStats(tiletobuild);
      this.CanAfford = player.Stats.GetCashHeld() >= this.cost;
      if (Locked)
        this.HEADING = nameof (Locked);
      if (tileStats != null)
      {
        this.HEADING = tileStats.Name;
        this.COST = new TextButton("$" + (object) this.cost, 50f);
        this.COST.SetAsBuyButton(this.cost, player);
      }
      else
        this.COST = (TextButton) null;
    }

    public void Exit() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public void UpdateZ_ObjectInfoManager(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.Offset.Y = Z_BuildingIconPanel.MinHeight + this.lerper.Value * 200f;
      if (this.COST == null)
        return;
      if (ObjectInfoPanel.z_dragbuilder != null)
      {
        if (ObjectInfoPanel.z_dragbuilder.GetIsBlocked() == this.IsGreen)
          return;
        this.IsGreen = ObjectInfoPanel.z_dragbuilder.GetIsBlocked();
        if (this.IsGreen && this.CanAfford)
          this.COST.SetButtonColour(BTNColour.Green);
        else
          this.COST.SetButtonColour(BTNColour.Red);
      }
      else if (ObjectInfoPanel.z_penbuilder != null)
      {
        this.ChangeItem(player, ObjectInfoPanel.z_penbuilder.GetTileTypeBeingBult(), false, ObjectInfoPanel.z_penbuilder.GetVolume());
        this.IsGreen = !ObjectInfoPanel.z_penbuilder.GetIsBlocked();
        if (this.IsGreen && this.CanAfford)
          this.COST.SetButtonColour(BTNColour.Green);
        else
          this.COST.SetButtonColour(BTNColour.Red);
        this.COST.UpdateTextButton(player, this.Offset + this.Frame.vLocation, DeltaTime);
        this.COST.MouseOver = false;
      }
      else
      {
        if (ObjectInfoPanel.z_penbuilder_oldsquare == null)
          return;
        this.ChangeItem(player, ObjectInfoPanel.z_penbuilder_oldsquare.GetTileTypeBeingBult(), false, ObjectInfoPanel.z_penbuilder_oldsquare.GetVolume());
        this.IsGreen = !ObjectInfoPanel.z_penbuilder_oldsquare.GetIsBlocked();
        if (this.IsGreen && this.CanAfford)
          this.COST.SetButtonColour(BTNColour.Green);
        else
          this.COST.SetButtonColour(BTNColour.Red);
        this.COST.UpdateTextButton(player, this.Offset + this.Frame.vLocation, DeltaTime);
        this.COST.MouseOver = false;
      }
    }

    public void DrawZ_ObjectInfoManager(int TotalPenSpace)
    {
    }
  }
}

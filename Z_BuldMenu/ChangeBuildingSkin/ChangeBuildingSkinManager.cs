// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.ChangeBuildingSkin.ChangeBuildingSkinManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.ChangeBuildingSkin
{
  internal class ChangeBuildingSkinManager
  {
    private ChangeBuildingSkinPopup popup;
    private TILETYPE buildingtype;

    public ChangeBuildingSkinManager(TILETYPE _buildingtype, Vector2Int _Location)
    {
      this.buildingtype = _buildingtype;
      this.popup = new ChangeBuildingSkinPopup(this.buildingtype, Z_GameFlags.GetBaseScaleForUI(), _Location);
      this.popup.location = Sengine.HalfReferenceScreenRes;
    }

    public bool IsChangingThisSkin(TILETYPE tiletype)
    {
      if (TileData.IsAManagementOffice(tiletype))
      {
        if (TileData.IsAManagementOffice(this.buildingtype))
          return true;
      }
      else if (TileData.IsAStoreRoom(tiletype))
      {
        if (TileData.IsAStoreRoom(this.buildingtype))
          return true;
      }
      else if (TileData.IsATicketOffice(tiletype) && TileData.IsATicketOffice(this.buildingtype))
        return true;
      return false;
    }

    public bool CheckMouseOver(Player player) => this.popup.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateChangeBuildingSkinManager(
      Player player,
      float DeltaTime,
      ref bool WillClearInput)
    {
      return (0 | (this.popup.UpdateChangeBuildingSkinPopup(player, Vector2.Zero, DeltaTime, ref WillClearInput) ? 1 : 0)) != 0;
    }

    public void DrawChangeBuildingSkinManager() => this.popup.DrawChangeBuildingSkinPopup(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}

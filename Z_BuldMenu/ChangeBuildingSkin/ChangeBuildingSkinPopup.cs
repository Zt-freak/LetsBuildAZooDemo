// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.ChangeBuildingSkin.ChangeBuildingSkinPopup
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuldMenu.ChangeBuildingSkin
{
  internal class ChangeBuildingSkinPopup
  {
    public Vector2 location;
    private UIScaleHelper uiscale;
    private ButtonGrid buttongrid;
    private BigBrownPanel panel;
    private CustomerFrame frame;
    private SimpleTextHandler text;
    private Vector2 framescale;
    private float basescale;
    private TILETYPE tiletype;
    private Vector2Int Location;

    public ChangeBuildingSkinPopup(TILETYPE tiletype_, float basescale_, Vector2Int _Location)
    {
      this.Location = new Vector2Int(_Location);
      this.tiletype = tiletype_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.buttongrid = new ButtonGrid(4, this.basescale);
      Rectangle rectangle1 = new Rectangle(590, 136, 38, 38);
      Rectangle rectangle2 = new Rectangle(551, 136, 38, 38);
      HashSet<TILETYPE> tiletypeSet = new HashSet<TILETYPE>();
      if (TileData.IsAManagementOffice(this.tiletype))
        tiletypeSet = TileData.GetManagementOfficeTileTypes();
      else if (TileData.IsAStoreRoom(this.tiletype))
        tiletypeSet = TileData.GetStoreRoomTileTypes();
      else if (TileData.IsATicketOffice(this.tiletype))
        tiletypeSet = TileData.GetTicketOfficeTileTypes();
      foreach (TILETYPE tiletype_1 in tiletypeSet)
        this.buttongrid.Add((ZGenericButton) new BuildingOnAButton(tiletype_1, this.basescale, tiletype_1 == this.tiletype));
      this.buttongrid.PositionButtons();
      this.framescale = this.buttongrid.GetSize();
      this.text = new SimpleTextHandler("Choose a new appearance for the building", false, (this.framescale.X - 2f * this.uiscale.GetDefaultXBuffer()) / Sengine.ReferenceScreenRes.X, this.basescale, false, false);
      this.text.AutoCompleteParagraph();
      float heightOfParagraph = this.text.GetHeightOfParagraph();
      this.framescale.Y += heightOfParagraph + this.uiscale.GetDefaultYBuffer();
      Vector2 vector2 = -0.5f * this.framescale + new Vector2(this.uiscale.GetDefaultXBuffer(), this.uiscale.GetDefaultYBuffer());
      this.text.Location = vector2;
      vector2.Y += heightOfParagraph;
      this.buttongrid.location.Y = vector2.Y + 0.5f * this.buttongrid.GetSize().Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.panel = new BigBrownPanel(Vector2.Zero, true, "Renovate", this.basescale);
      this.panel.Finalize(this.framescale);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateChangeBuildingSkinPopup(
      Player player,
      Vector2 offset,
      float DeltaTime,
      ref bool WillClearInput)
    {
      offset += this.location;
      bool flag1 = false;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      bool flag2 = flag1 | this.panel.UpdatePanelCloseButton(player, DeltaTime, offset);
      if (this.panel.CheckCollision(player.inputmap.PointerLocation, offset))
        WillClearInput = false;
      int key1 = this.buttongrid.UpdateButtonGrid(player, offset, DeltaTime);
      if (key1 > -1)
      {
        BuildingOnAButton buildingOnAbutton1 = (BuildingOnAButton) this.buttongrid[key1];
        TILETYPE tiletype = buildingOnAbutton1.tiletype;
        if (tiletype != this.tiletype)
        {
          for (int key2 = 0; key2 < this.buttongrid.Count; ++key2)
          {
            BuildingOnAButton buildingOnAbutton2 = (BuildingOnAButton) this.buttongrid[key2];
            if (buildingOnAbutton2.tiletype == this.tiletype)
              buildingOnAbutton2.SetSelected(false);
          }
          buildingOnAbutton1.SetSelected(true);
          this.tiletype = tiletype;
          player.prisonlayout.layout.BaseTileTypes[this.Location.X, this.Location.Y].tiletype = buildingOnAbutton1.tiletype;
          OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: this.Location, HyperOptimized: true);
        }
      }
      return flag2;
    }

    public void DrawChangeBuildingSkinPopup(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.buttongrid.DrawButtonGrid(spritebatch, offset);
    }
  }
}

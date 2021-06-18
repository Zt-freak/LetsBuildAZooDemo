// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.MainBar.BarButtonIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Text;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople;

namespace TinyZoo.Z_PenInfo.MainBar
{
  internal class BarButtonIcon : GameObject
  {
    private string TEXT;
    private AnimalRenderer animalrenderer;
    private SimpleBuildingRenderer simplebuildingrenderer;
    private PenItem Ref_penitem;
    private CorpseRenderer corpse;
    private float BaseScale;
    private Vector2 AnimalSize;
    private bool AnimalIsHybrid;
    public StringScroller scroller;
    private TILETYPE tile_type;

    public BarButtonIcon(
      BuildingManageButton _ManageButtonType,
      PrisonerInfo prisonerinfo = null,
      TILETYPE tiletype = TILETYPE.Count,
      PenItem penitem = null,
      float _BaseScale = -1f)
    {
      this.BaseScale = _BaseScale;
      if ((double) this.BaseScale == -1.0)
        this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.scale = this.BaseScale * 2f;
      this.TEXT = BarButtonIcon.GetBuildingManageButtonToString(_ManageButtonType);
      switch (_ManageButtonType)
      {
        case BuildingManageButton.StoreRoom:
          this.DrawRect = new Rectangle(449, 132, 15, 21);
          break;
        case BuildingManageButton.StoreRoomShop:
          this.DrawRect = new Rectangle(409, 123, 18, 18);
          break;
        case BuildingManageButton.Move:
          this.DrawRect = new Rectangle(495, 111, 23, 23);
          break;
        case BuildingManageButton.Destroy:
          this.DrawRect = new Rectangle(476, 111, 18, 20);
          break;
        case BuildingManageButton.Transfer:
          this.DrawRect = new Rectangle(452, 111, 23, 20);
          break;
        case BuildingManageButton.AnimalInfo:
          this.DrawRect = new Rectangle(486, 135, 25, 25);
          break;
        case BuildingManageButton.AnimalStatus:
          this.DrawRect = new Rectangle(465, 132, 20, 19);
          break;
        case BuildingManageButton.ManageShop:
          this.DrawRect = new Rectangle(430, 107, 21, 21);
          break;
        case BuildingManageButton.Architect_Design:
          this.DrawRect = new Rectangle(431, 129, 17, 18);
          break;
        case BuildingManageButton.BuildPen_Water:
          this.DrawRect = new Rectangle(409, 142, 14, 19);
          break;
        case BuildingManageButton.BuildPen_Enrichment:
          this.DrawRect = new Rectangle(446, 154, 15, 20);
          break;
        case BuildingManageButton.BuildPen_Shelter:
          this.DrawRect = new Rectangle(424, 148, 20, 20);
          break;
        case BuildingManageButton.BuildPen_Decoration:
          this.DrawRect = new Rectangle(466, 179, 22, 26);
          break;
        case BuildingManageButton.Pen_EditPen:
          this.DrawRect = new Rectangle(495, 111, 23, 23);
          break;
        case BuildingManageButton.Pen_AddItemsToPen:
          this.DrawRect = new Rectangle(546, 85, 26, 26);
          break;
        case BuildingManageButton.Pen_ItemsViewEdit:
          this.DrawRect = new Rectangle(518, 85, 27, 26);
          break;
        case BuildingManageButton.Pen_Animals:
          this.DrawRect = new Rectangle(603, 263, 25, 28);
          break;
        case BuildingManageButton.MoveGate:
          this.DrawRect = new Rectangle(462, 154, 24, 24);
          break;
        case BuildingManageButton.EditPenFloor:
          this.DrawRect = new Rectangle(495, 111, 23, 23);
          break;
        case BuildingManageButton.TicketPrice:
          this.DrawRect = new Rectangle(441, 175, 24, 24);
          break;
        case BuildingManageButton.GetStaff:
          this.DrawRect = new Rectangle(580, 263, 22, 23);
          break;
        case BuildingManageButton.GetParkStaff:
          this.DrawRect = new Rectangle(580, 263, 22, 23);
          break;
        case BuildingManageButton.Pen_GetMoreAnimals:
          this.DrawRect = new Rectangle(417, 169, 23, 27);
          break;
        case BuildingManageButton.ExpandLand:
          this.DrawRect = new Rectangle(349, 121, 28, 31);
          break;
        case BuildingManageButton.Nursery_Breeding:
          this.DrawRect = new Rectangle(302, 151, 18, 23);
          break;
        case BuildingManageButton.ManageProcessingPlant:
          this.DrawRect = new Rectangle(594, 447, 29, 20);
          break;
        case BuildingManageButton.AreaOfCollection:
          this.DrawRect = new Rectangle(561, 537, 28, 29);
          break;
        case BuildingManageButton.CRISPR_menu:
          this.DrawRect = new Rectangle(545, 342, 16, 24);
          break;
        case BuildingManageButton.ParkSummary:
          this.DrawRect = new Rectangle(368, 168, 19, 20);
          break;
        case BuildingManageButton.Transport:
          this.DrawRect = new Rectangle(386, 194, 26, 20);
          break;
        case BuildingManageButton.ChangeBuildingSkin:
          this.DrawRect = new Rectangle(396, 445, 20, 22);
          break;
        case BuildingManageButton.Collection:
          this.DrawRect = new Rectangle(440, 445, 25, 21);
          break;
        case BuildingManageButton.Tasks:
          this.DrawRect = new Rectangle(417, 445, 22, 22);
          break;
        case BuildingManageButton.RideTicketing:
          this.DrawRect = new Rectangle(441, 175, 24, 24);
          break;
        case BuildingManageButton.Pen_DeliveryOrders:
          this.DrawRect = new Rectangle(321, 151, 25, 23);
          break;
        case BuildingManageButton.Surveillance_People:
          this.DrawRect = new Rectangle(376, 414, 25, 23);
          break;
        case BuildingManageButton.DiseaseResearch:
          this.DrawRect = new Rectangle(735, 920, 27, 28);
          break;
        case BuildingManageButton.MedicalJournal:
          this.DrawRect = new Rectangle(657, 920, 25, 27);
          break;
        case BuildingManageButton.VetVistSummary:
          this.DrawRect = new Rectangle(630, 920, 26, 28);
          break;
        case BuildingManageButton.Quarantine:
          this.DrawRect = new Rectangle(683, 920, 28, 28);
          break;
        case BuildingManageButton.QuarantineSettings:
          this.DrawRect = new Rectangle(430, 107, 21, 21);
          break;
        case BuildingManageButton.ManageIncinerator:
          this.DrawRect = new Rectangle(938, 387, 22, 28);
          break;
        case BuildingManageButton.Farm_EditCrop:
          this.DrawRect = new Rectangle(914, 392, 23, 27);
          break;
        case BuildingManageButton.Slaughterhouse_Culling:
          this.DrawRect = new Rectangle(442, 896, 25, 23);
          break;
        case BuildingManageButton.Warehouse:
          this.DrawRect = new Rectangle(391, 911, 26, 23);
          break;
      }
      this.SetDrawOriginToCentre();
      if (prisonerinfo != null)
      {
        this.TEXT = prisonerinfo.intakeperson.Name;
        this.animalrenderer = new AnimalRenderer(prisonerinfo.intakeperson.animaltype, prisonerinfo.intakeperson.CLIndex, prisonerinfo.intakeperson.HeadType, prisonerinfo.intakeperson.HeadVariant);
        this.animalrenderer.UpdateAnimal(0.0f);
        this.animalrenderer.enemy.scale = this.BaseScale * 3f;
        this.animalrenderer.enemy.vLocation = new Vector2(0.0f, 20f * Sengine.ScreenRatioUpwardsMultiplier.Y);
        if (prisonerinfo.GetIsABaby())
        {
          this.animalrenderer.enemy.vLocation.Y *= 0.5f;
          this.animalrenderer.enemy.scale *= 0.5f;
        }
        if (prisonerinfo.intakeperson.HeadType == AnimalType.None)
        {
          this.AnimalIsHybrid = false;
          this.animalrenderer.enemy.SetDrawOriginToCentre();
        }
        else
          this.AnimalIsHybrid = true;
        if (prisonerinfo.IsDead)
        {
          this.corpse = new CorpseRenderer(prisonerinfo.causeofdeath, prisonerinfo.intakeperson.animaltype, prisonerinfo.intakeperson.CLIndex);
          this.corpse.scale = 3f;
          this.corpse.SetDrawOriginToCentre();
          this.corpse.vLocation = new Vector2(0.0f, 8f * Sengine.ScreenRatioUpwardsMultiplier.Y);
          if (prisonerinfo.GetIsABaby())
            this.corpse.scale *= 0.5f;
        }
        this.animalrenderer.GetSize(out float _, out float _);
      }
      if (penitem != null)
      {
        this.Ref_penitem = penitem;
        this.tile_type = penitem.tiletype;
        this.simplebuildingrenderer = new SimpleBuildingRenderer(this.tile_type);
        this.simplebuildingrenderer.SetSize(80f * _BaseScale, 2f * _BaseScale);
        this.TEXT = TileData.GetTileStats(penitem.tiletype).Name;
      }
      if (tiletype != TILETYPE.Count)
      {
        this.tile_type = tiletype;
        switch (tiletype - 37)
        {
          case TILETYPE.None:
          case TILETYPE.GreenWallCorner:
          case TILETYPE.GreenWallSide:
          case TILETYPE.Floor_GreenGrass:
          case TILETYPE.Floor_Dirt:
          case TILETYPE.Floor_RedCircles:
          case TILETYPE.Floor_GreyBricks:
            this.simplebuildingrenderer = new SimpleBuildingRenderer(BarButtonIcon.GetTileTypeToPenTileTypeIcon(tiletype));
            break;
          default:
            this.simplebuildingrenderer = new SimpleBuildingRenderer(tiletype);
            break;
        }
        this.simplebuildingrenderer.SetSize(80f * _BaseScale, 2f * _BaseScale);
        this.TEXT = TileData.GetTileStats(this.tile_type).Name;
        if (tiletype == TILETYPE.Lamppost)
        {
          this.simplebuildingrenderer.DrawOrigin.Y -= 5f;
          this.simplebuildingrenderer.TopLayer.DrawOrigin.Y -= 5f;
        }
      }
      this.scroller = new StringScroller(this.scale * 50f * Sengine.ScreenRatioUpwardsMultiplier.Y, this.TEXT, AssetContainer.SpringFontX1AndHalf);
      this.vLocation.Y = -16f * this.BaseScale;
    }

    public TILETYPE GetTileType() => this.tile_type;

    public void ReplaceScrollerText(string NewText) => this.scroller = new StringScroller(this.scale * 50f * Sengine.ScreenRatioUpwardsMultiplier.Y, NewText, AssetContainer.SpringFontX1AndHalf);

    internal static TILETYPE GetTileTypeToPenTileTypeIcon(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.GrassEnclosure:
          return TILETYPE.GrassEnclosureIcon;
        case TILETYPE.DesertEnclosure:
          return TILETYPE.DesertEnclosureIcon;
        case TILETYPE.MountainEnclosure:
          return TILETYPE.MountainEnclosureIcon;
        case TILETYPE.ArcticEnclosure:
          return TILETYPE.ArcticEnclosureIcon;
        case TILETYPE.TropicalEnclosure:
          return TILETYPE.TropicalEnclosureIcon;
        case TILETYPE.ForestEnclosure:
          return TILETYPE.ForestEnclosureIcon;
        case TILETYPE.SavannahEnclosure:
          return TILETYPE.SavannahEnclosureIcon;
        default:
          return TILETYPE.Count;
      }
    }

    internal static string GetBuildingManageButtonToString(BuildingManageButton btn)
    {
      switch (btn)
      {
        case BuildingManageButton.StoreRoom:
          return SEngine.Localization.Localization.GetText(267);
        case BuildingManageButton.StoreRoomShop:
          return SEngine.Localization.Localization.GetText(844);
        case BuildingManageButton.Move:
          return SEngine.Localization.Localization.GetText(840);
        case BuildingManageButton.Destroy:
          return SEngine.Localization.Localization.GetText(841);
        case BuildingManageButton.Transfer:
          return SEngine.Localization.Localization.GetText(11);
        case BuildingManageButton.AnimalInfo:
          return SEngine.Localization.Localization.GetText(842);
        case BuildingManageButton.AnimalStatus:
          return SEngine.Localization.Localization.GetText(843);
        case BuildingManageButton.ManageShop:
          return SEngine.Localization.Localization.GetText(845);
        case BuildingManageButton.Architect_Design:
          return SEngine.Localization.Localization.GetText(56);
        case BuildingManageButton.BuildPen_Water:
          return SEngine.Localization.Localization.GetText(442);
        case BuildingManageButton.BuildPen_Enrichment:
          return SEngine.Localization.Localization.GetText(854);
        case BuildingManageButton.BuildPen_Shelter:
          return SEngine.Localization.Localization.GetText(853);
        case BuildingManageButton.BuildPen_Decoration:
          return SEngine.Localization.Localization.GetText(852);
        case BuildingManageButton.Pen_EditPen:
          return SEngine.Localization.Localization.GetText(832);
        case BuildingManageButton.Pen_AddItemsToPen:
          return SEngine.Localization.Localization.GetText(856);
        case BuildingManageButton.Pen_ItemsViewEdit:
          return SEngine.Localization.Localization.GetText(857);
        case BuildingManageButton.Pen_Animals:
          return SEngine.Localization.Localization.GetText(855);
        case BuildingManageButton.MoveGate:
          return SEngine.Localization.Localization.GetText(849);
        case BuildingManageButton.EditPenFloor:
          return SEngine.Localization.Localization.GetText(850);
        case BuildingManageButton.SpecificAnimal:
          return SEngine.Localization.Localization.GetText(862);
        case BuildingManageButton.TicketPrice:
          return SEngine.Localization.Localization.GetText(846);
        case BuildingManageButton.GetStaff:
          return SEngine.Localization.Localization.GetText(859);
        case BuildingManageButton.GetParkStaff:
          return SEngine.Localization.Localization.GetText(858);
        case BuildingManageButton.Pen_GetMoreAnimals:
          return SEngine.Localization.Localization.GetText(860);
        case BuildingManageButton.ExpandLand:
          return SEngine.Localization.Localization.GetText(851);
        case BuildingManageButton.Nursery_Breeding:
          return SEngine.Localization.Localization.GetText(861);
        case BuildingManageButton.ManageProcessingPlant:
          return SEngine.Localization.Localization.GetText(847);
        case BuildingManageButton.AreaOfCollection:
          return SEngine.Localization.Localization.GetText(848);
        case BuildingManageButton.CRISPR_menu:
          return SEngine.Localization.Localization.GetText(864);
        case BuildingManageButton.BuildStructure_PEN:
          return SEngine.Localization.Localization.GetText(863);
        case BuildingManageButton.ParkSummary:
          return SEngine.Localization.Localization.GetText(865);
        case BuildingManageButton.Transport:
          return SEngine.Localization.Localization.GetText(1);
        case BuildingManageButton.ChangeBuildingSkin:
          return SEngine.Localization.Localization.GetText(866);
        case BuildingManageButton.Collection:
          return SEngine.Localization.Localization.GetText(31);
        case BuildingManageButton.Tasks:
          return SEngine.Localization.Localization.GetText(867);
        case BuildingManageButton.RideTicketing:
          return SEngine.Localization.Localization.GetText(837);
        case BuildingManageButton.Pen_DeliveryOrders:
          return SEngine.Localization.Localization.GetText(868);
        case BuildingManageButton.Surveillance_People:
          return SEngine.Localization.Localization.GetText(595);
        case BuildingManageButton.DiseaseResearch:
          return SEngine.Localization.Localization.GetText(870);
        case BuildingManageButton.MedicalJournal:
          return SEngine.Localization.Localization.GetText(869);
        case BuildingManageButton.VetVistSummary:
          return SEngine.Localization.Localization.GetText(871);
        case BuildingManageButton.Quarantine:
          return SEngine.Localization.Localization.GetText(855);
        case BuildingManageButton.QuarantineSettings:
          return SEngine.Localization.Localization.GetText(33);
        case BuildingManageButton.ManageIncinerator:
          return SEngine.Localization.Localization.GetText(872);
        case BuildingManageButton.Farm_EditCrop:
          return SEngine.Localization.Localization.GetText(873);
        case BuildingManageButton.Slaughterhouse_Culling:
          return SEngine.Localization.Localization.GetText(874);
        case BuildingManageButton.Warehouse:
          return SEngine.Localization.Localization.GetText(603);
        default:
          return "MISSING";
      }
    }

    public void SetDisabled()
    {
      this.SetAlpha(0.3f);
      if (this.simplebuildingrenderer == null)
        return;
      this.simplebuildingrenderer.SetAllColours(0.0f, 0.0f, 0.0f);
      this.simplebuildingrenderer.SetAlpha(0.3f);
      if (this.simplebuildingrenderer.TopLayer == null)
        return;
      this.simplebuildingrenderer.TopLayer.SetAllColours(0.0f, 0.0f, 0.0f);
      this.simplebuildingrenderer.TopLayer.SetAlpha(0.3f);
    }

    public void UpdateButtonIcon(float DeltaTime) => this.scroller.UpdateStringScroller(DeltaTime);

    public void DrawBarButtonIcon(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float ScaleMultiplier,
      float ParentScale)
    {
      if (this.animalrenderer != null)
      {
        if (this.corpse != null)
        {
          this.corpse.ScreenSpaceDrawCorpseRenderer(Offset + this.vLocation, AssetContainer.pointspritebatchTop05, ScaleMultiplier);
        }
        else
        {
          if (this.AnimalIsHybrid)
            this.animalrenderer.enemy.vLocation = Vector2.Zero;
          else
            this.animalrenderer.enemy.vLocation = new Vector2(0.0f, ParentScale * -4f * Sengine.ScreenRatioUpwardsMultiplier.Y);
          this.animalrenderer.ScreenSpaceDraw(Offset + new Vector2(0.0f, (float) ((double) this.AnimalSize.Y * (double) ScaleMultiplier * 0.5)), AssetContainer.pointspritebatchTop05, false, ScaleMultiplier);
        }
      }
      else if (this.simplebuildingrenderer != null)
        this.simplebuildingrenderer.DrawSimpleBuildingRenderer(Offset + this.vLocation, AssetContainer.pointspritebatchTop05, ScaleMultiplier, this.fAlpha);
      else
        this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.scale * ScaleMultiplier, this.fAlpha);
      Offset.Y += 19f * ParentScale * Sengine.ScreenRatioUpwardsMultiplier.Y * ScaleMultiplier;
      TextFunctions.DrawJustifiedText(this.scroller.GetString(), this.BaseScale * ScaleMultiplier, Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }
  }
}

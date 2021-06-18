// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.MainBar.MainBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarInfo.MainBar;
using TinyZoo.Z_BarMenu.Build;
using TinyZoo.Z_ControllerLayouts;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_ManagePen.Buttons;
using TinyZoo.Z_Morality;
using TinyZoo.Z_MoralitySummary.SubElements.ScoreDisplay;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_PenInfo.MainBar
{
  internal class MainBarManager
  {
    private GameObject BG;
    public List<PenInfoButton> BuildingOnfoButtons;
    private static float Height = 200f;
    private int ExtraIndexes;
    private TRC_ButtonDisplay DpadHint;
    private PrisonZone Ref_prisonzone;
    private float BaseScale;
    private BarScroller barscroller;
    private Z_BreakoutButton Back;
    private Z_BreakoutButton CloseMenu;
    private Z_BreakoutButton Heading;
    private LerpHandler_Float ShrinkLerper;
    private Controller_BuildingStatusBar controllerbuildbarmatrix;
    private bool IsEnabled_Controller;
    private MoralityScoreProgressionDisplay_WithFrame moralityDisplay;
    public BAR_TYPE bartypeonConstrust;
    internal static TILETYPE SelectedBuilding;
    internal static bool BarIsOnScreen;
    public bool IsShrunk;
    private int ControllerSelected;
    public int PressedIndex;
    private static float ShrinkVerticalOffset = 60f;

    public MainBarManager(BAR_TYPE bartype, TILETYPE tiletype, bool _HasTransfer, Player player)
    {
      MainBarManager.SelectedBuilding = TILETYPE.Count;
      this.bartypeonConstrust = bartype;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      Z_GameFlags.SetHeatMapType(HeatMapType.None);
      this.ShrinkLerper = new LerpHandler_Float();
      this.ShrinkLerper.SetLerp(true, 0.0f, 0.0f, 3f);
      this.ExtraIndexes = 0;
      this.BG = new GameObject();
      this.BG.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BuildingOnfoButtons = new List<PenInfoButton>();
      this.controllerbuildbarmatrix = new Controller_BuildingStatusBar();
      this.DpadHint = new TRC_ButtonDisplay(TinyZoo.GameFlags.GetTRCButtonScale());
      this.DpadHint.SetUpAnimation(new List<ControllerAnim>()
      {
        new ControllerAnim(ControllerButton.DPadLeft, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 1f),
        new ControllerAnim(ControllerButton.DpadNeutral, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.1f),
        new ControllerAnim(ControllerButton.DpadRight, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 1f),
        new ControllerAnim(ControllerButton.DpadNeutral, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.1f)
      });
      this.DpadHint.Position = new Vector2(25f, 600f);
      bool flag = false;
      switch (bartype)
      {
        case BAR_TYPE.Pen:
          PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID);
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_Animals));
          if (thisCellBlock.prisonercontainer.prisoners.Count <= 0)
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_ItemsViewEdit));
          if (thisCellBlock.penItems.items.Count <= 0)
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_AddItemsToPen));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_EditPen));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_GetMoreAnimals));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_DeliveryOrders));
          if (!player.Stats.TutorialsComplete[29])
          {
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 2].Disable();
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 3].Disable();
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 4].Disable();
          }
          this.AddHeading(SEngine.Localization.Localization.GetText(452));
          goto case BAR_TYPE.Pen_ViewItems;
        case BAR_TYPE.Pen_MainEditPen:
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.BuildPen_Water));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.BuildPen_Enrichment));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.BuildPen_Shelter));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.BuildPen_Decoration));
          this.AddHeading(SEngine.Localization.Localization.GetText(831));
          goto case BAR_TYPE.Pen_ViewItems;
        case BAR_TYPE.Pen_EditPen:
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Move));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.MoveGate));
          if (!Z_DebugFlags.IsBetaVersion)
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.EditPenFloor));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Destroy));
          if (player.prisonlayout.cellblockcontainer.prisonzones.Count <= 1)
            this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
          this.AddHeading(SEngine.Localization.Localization.GetText(832));
          goto case BAR_TYPE.Pen_ViewItems;
        case BAR_TYPE.Pen_ViewItems:
        case BAR_TYPE.Pen_Animals:
          if (MoralityUnlocksData.IsAMoralityBuilding(tiletype))
            this.AddMoralityInfo(tiletype, player);
          this.CheckAddBarScroller();
          this.AddBackButton();
          this.EnableControllerControls(true);
          break;
        case BAR_TYPE.Pen_ViewSpecificItem:
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Move));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Destroy));
          this.AddHeading(SEngine.Localization.Localization.GetText(834));
          goto case BAR_TYPE.Pen_ViewItems;
        case BAR_TYPE.BuyMoreLand:
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ExpandLand));
          this.AddHeading(SEngine.Localization.Localization.GetText(833));
          goto case BAR_TYPE.Pen_ViewItems;
        case BAR_TYPE.FarmField:
          player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
          this.AddHeading(SEngine.Localization.Localization.GetText(830));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Farm_EditCrop));
          this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Pen_EditPen));
          goto case BAR_TYPE.Pen_ViewItems;
        default:
          if (TileData.IsATicketOffice(tiletype))
          {
            bartype = BAR_TYPE.TicketBooth;
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.TicketPrice));
            if (!Z_GameFlags.HasStartedFirstDay)
              this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetParkStaff));
            if (!Z_GameFlags.HasStartedFirstDay)
              this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Transport));
            if (!Z_GameFlags.HasStartedFirstDay)
              this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ChangeBuildingSkin));
            this.AddHeading(SEngine.Localization.Localization.GetText(835));
            goto case BAR_TYPE.Pen_ViewItems;
          }
          else if (TileData.IsThisAFacility(tiletype))
          {
            switch (tiletype)
            {
              case TILETYPE.QuarantineOffice:
                this.AddHeading(SEngine.Localization.Localization.GetText(188));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Quarantine));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.QuarantineSettings));
                break;
              case TILETYPE.VetOffice:
                this.AddHeading(SEngine.Localization.Localization.GetText(189));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.DiseaseResearch));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.MedicalJournal));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.VetVistSummary));
                break;
              case TILETYPE.Slaughterhouse:
                this.AddHeading(SEngine.Localization.Localization.GetText(373));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Slaughterhouse_Culling));
                break;
              case TILETYPE.Incinerator:
                this.AddHeading(SEngine.Localization.Localization.GetText(451));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ManageIncinerator));
                break;
              case TILETYPE.Farmhouse:
                this.AddHeading(SEngine.Localization.Localization.GetText(597));
                break;
              case TILETYPE.Warehouse:
                this.AddHeading(SEngine.Localization.Localization.GetText(603));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Warehouse));
                break;
            }
            if (TileData.IsAMeatProcessingPlant(tiletype))
            {
              this.AddHeading(SEngine.Localization.Localization.GetText(449));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ManageProcessingPlant));
            }
            else if (TileData.IsAVegetableProcessingPlant(tiletype))
            {
              this.AddHeading(SEngine.Localization.Localization.GetText(598));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ManageProcessingPlant));
            }
            if (EmployeeData.ThisStoreHasAnEmployee(tiletype))
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Move));
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Destroy));
            goto case BAR_TYPE.Pen_ViewItems;
          }
          else
          {
            switch (tiletype)
            {
              case TILETYPE.Nursery:
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Nursery_Breeding));
                this.AddHeading(SEngine.Localization.Localization.GetText(187));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
                break;
              case TILETYPE.ArchitectOffice:
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Architect_Design));
                this.AddHeading(SEngine.Localization.Localization.GetText(269));
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
                break;
            }
            if (TileData.IsAManagementOffice(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Tasks));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Collection));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ChangeBuildingSkin));
              if (!player.Stats.TutorialsComplete[28])
              {
                this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 2].Disable();
                this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
                flag = true;
              }
              this.AddHeading(SEngine.Localization.Localization.GetText(836));
            }
            if (TileData.IsAStoreRoom(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.StoreRoom));
              if (Z_GameFlags.HasStartedFirstDay)
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ChangeBuildingSkin));
              this.AddHeading(SEngine.Localization.Localization.GetText(267));
            }
            if (TileData.IsThisAShopWithShopStats(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ManageShop));
              if (EmployeeData.ThisStoreHasAnEmployee(tiletype) && Z_GameFlags.HasStartedFirstDay)
                this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
              this.AddHeading("Shop");
            }
            if (TileData.IsASlaughterhouse(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ManageProcessingPlant));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.AreaOfCollection));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
            }
            if (TileData.IsACRISPRBuilding(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.CRISPR_menu));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
              this.AddHeading(SEngine.Localization.Localization.GetText(448));
            }
            if (TileData.IsATicketedRide(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.RideTicketing));
              this.AddHeading(SEngine.Localization.Localization.GetText(837));
            }
            if (TileData.IsASurveillanceBuilding(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Surveillance_People));
              this.AddHeading(SEngine.Localization.Localization.GetText(595));
            }
            if (TileData.IsAFactory(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.ManageProcessingPlant));
              if (MoralityUnlocksData.IsAMoralityBuilding(tiletype))
                this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].AddMoralityIcons(MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(tiletype) > 0, MoralityUnlocksData.PlayerHasEnoughPointsToUseThis(tiletype, player));
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.GetStaff));
              this.AddHeading(SEngine.Localization.Localization.GetText(838));
            }
            this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Move));
            if (flag)
              this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable();
            if (!TileData.IsAStoreRoom(tiletype) && !TileData.IsAManagementOffice(tiletype) && !TileData.IsACRISPRBuilding(tiletype))
            {
              this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Destroy));
              goto case BAR_TYPE.Pen_ViewItems;
            }
            else
              goto case BAR_TYPE.Pen_ViewItems;
          }
      }
    }

    public void UnShrink()
    {
      MainBarManager.SelectedBuilding = TILETYPE.Count;
      this.IsShrunk = false;
      this.ShrinkLerper.SetLerp(false, 1f, 0.0f, 3f, true);
    }

    public void Shrink(TILETYPE tiletype)
    {
      MainBarManager.SelectedBuilding = tiletype;
      this.IsShrunk = true;
      this.ShrinkLerper.SetLerp(false, 0.0f, 1f, 3f, true);
    }

    public void AddBackButton()
    {
      this.Back = new Z_BreakoutButton(BreakOutButtonType.Back, 120f, Z_GameFlags.GetBaseScaleForUI());
      this.Back.Location = new Vector2(100f, 740f);
    }

    public void AddHeading(string HEadingName)
    {
      this.Heading = new Z_BreakoutButton(BreakOutButtonType.Heading, 250f, this.BaseScale);
      this.Heading.SetText(HEadingName);
      this.Heading.Location = new Vector2(150f * this.BaseScale, (float) (768.0 - 218.0 * (double) this.BaseScale));
      this.Heading.Location.Y = Build_BarManager.GetVerticalCenterForHeading();
    }

    public void AddMoralityInfo(TILETYPE tileType, Player player)
    {
      this.moralityDisplay = new MoralityScoreProgressionDisplay_WithFrame(this.BaseScale);
      if (this.Heading != null)
      {
        this.moralityDisplay.location = this.Heading.Location;
        this.moralityDisplay.location.X += this.BaseScale * 200f;
      }
      else
        this.moralityDisplay.location = new Vector2(100f, 550f);
      this.moralityDisplay.location.X += this.moralityDisplay.GetSize().X * 0.5f;
      this.moralityDisplay.SmartSetScoreForBuilding(tileType, player);
    }

    public void SetUpForEditEnclosure(Player player, PrisonZone prisonzone)
    {
      for (int index = 0; index < prisonzone.penItems.items.Count; ++index)
        this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.Move, penitem: prisonzone.penItems.items[index]));
      this.AddHeading(SEngine.Localization.Localization.GetText(839));
      this.CheckAddBarScroller();
    }

    public void SetUpBuildings(
      List<TILETYPE> Buildings,
      Player player,
      CATEGORYTYPE categorytype,
      float BaseScale)
    {
      this.ExtraIndexes = this.BuildingOnfoButtons.Count;
      List<TILETYPE> tiletypeList1 = new List<TILETYPE>();
      List<TILETYPE> tiletypeList2 = new List<TILETYPE>();
      if (Buildings != null)
      {
        for (int index = 0; index < Buildings.Count; ++index)
        {
          if (MainBarManager.IsThisBuildingDisabled(Buildings[index], player))
            tiletypeList2.Add(Buildings[index]);
          else
            tiletypeList1.Add(Buildings[index]);
        }
      }
      tiletypeList1.AddRange((IEnumerable<TILETYPE>) tiletypeList2);
      for (int index = 0; index < tiletypeList1.Count; ++index)
      {
        this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.BuildStructure_PEN, _tiletype: tiletypeList1[index], _BaseScale: BaseScale));
        if (MainBarManager.IsThisBuildingDisabled(tiletypeList1[index], player))
          this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].Disable(false | categorytype == CATEGORYTYPE.Pen_Shelter | categorytype == CATEGORYTYPE.Pen_Deco);
        if (MoralityUnlocksData.IsAMoralityBuilding(tiletypeList1[index]))
          this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].AddMoralityIcons(MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(tiletypeList1[index]) > 0, true);
      }
      this.CheckAddBarScroller();
    }

    public static bool IsThisBuildingDisabled(TILETYPE tileType, Player player)
    {
      if (MainBarManager.CannotBuildAnymoreOfThisBuilding(tileType, player))
        return true;
      return TileData.IsThisACellBlock(tileType) ? !player.Stats.research.CellBlocksReseacrhed.Contains(tileType) : !player.Stats.research.BuildingsResearched.Contains(tileType);
    }

    public static bool CannotBuildAnymoreOfThisBuilding(TILETYPE tileType, Player player)
    {
      if (TileData.IsAStoreRoom(tileType))
        return player.storerooms.HasBuiltStoreRoom;
      if (TileData.IsThisanArchitectOffice(tileType))
      {
        for (int index = 0; index < player.shopstatus.ArchitectOffice.Count; ++index)
        {
          if (player.shopstatus.ArchitectOffice[index].tiletype == tileType)
            return true;
        }
        return false;
      }
      return TileData.IsACRISPRBuilding(tileType) && player.shopstatus.GetTotalOfThisFacility(tileType) > 0;
    }

    public int GetBuildingIndex() => this.PressedIndex - this.ExtraIndexes;

    public void SetUpAnimals(List<PrisonerInfo> prisoners)
    {
      for (int index = 0; index < prisoners.Count; ++index)
      {
        this.BuildingOnfoButtons.Add(new PenInfoButton(this.BuildingOnfoButtons.Count, MainBarManager.Height, BuildingManageButton.SpecificAnimal, prisoners[index]));
        if (prisoners[index].GetIsSick() && prisoners[index].SicknessHasBeeDiagnosed)
          this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].AddDiseaseIcon();
      }
      this.CheckAddBarScroller();
    }

    private void CheckAddBarScroller()
    {
      if (this.BuildingOnfoButtons.Count <= 0 || this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].PAGE <= 0)
        return;
      this.barscroller = new BarScroller(this.BuildingOnfoButtons[this.BuildingOnfoButtons.Count - 1].PAGE, this.BaseScale);
    }

    public void AddTransferButton()
    {
    }

    public void SetNew(DirectionPressed direction)
    {
      if (!this.IsEnabled_Controller)
        return;
      if (direction == DirectionPressed.Right)
      {
        if (this.ControllerSelected < this.BuildingOnfoButtons.Count - 1)
          ++this.ControllerSelected;
      }
      else if (this.ControllerSelected > 0)
        --this.ControllerSelected;
      if (this.barscroller == null)
        return;
      this.barscroller.ForceToThisPage(this.BuildingOnfoButtons[this.ControllerSelected].PAGE);
    }

    public void SkipToNextPage(bool Forwards)
    {
      if (!this.IsEnabled_Controller || this.barscroller == null || !this.LerpOnComplete(this.BuildingOnfoButtons[this.ControllerSelected].PAGE))
        return;
      this.barscroller.TryChangePage(Forwards);
    }

    public void EnableControllerControls(bool isEnable)
    {
      this.IsEnabled_Controller = isEnable;
      if (this.barscroller == null)
        return;
      this.barscroller.EnableControllerHint(isEnable);
    }

    public bool LerpOnComplete(int CheckThisPageOnly = -1)
    {
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
      {
        if (CheckThisPageOnly != -1 && this.BuildingOnfoButtons[index].PAGE != CheckThisPageOnly)
        {
          if (this.BuildingOnfoButtons[index].PAGE > CheckThisPageOnly)
            break;
        }
        else if (!this.BuildingOnfoButtons[index].LerpOnComplete())
          return false;
      }
      return true;
    }

    public bool CheckMouseOver(Player player)
    {
      float ScaleMultiplier = (float) (1.0 - (double) this.ShrinkLerper.Value * 0.5);
      Vector2 zero = Vector2.Zero;
      if (this.barscroller != null && this.barscroller.MouseOverlapping(player, Vector2.Zero, ref zero))
        return true;
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
      {
        if (this.BuildingOnfoButtons[index].MouseOverlapping(player, zero, ScaleMultiplier))
          return true;
      }
      return this.Back != null && this.Back.MouseOverlapping(player, zero) || this.Heading != null && this.Heading.MouseOverlapping(player, zero);
    }

    public void TempLerpOff()
    {
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
        this.BuildingOnfoButtons[index].LerpOff();
      this.Back.LerpOff();
    }

    public void LerpBackOn()
    {
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
        this.BuildingOnfoButtons[index].LerpOn();
      this.Back.LerpOn();
    }

    public void UpdateTempExitLerp(float DeltaTime)
    {
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
        this.BuildingOnfoButtons[index].UpdateTempExitLerp(DeltaTime);
      this.Back.UpdateTempExitLerp(DeltaTime);
    }

    public void FlashRed(BuildingManageButton buttontype)
    {
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
      {
        if (this.BuildingOnfoButtons[index].ManageButtonType == buttontype)
          this.BuildingOnfoButtons[index].FlashRed();
      }
    }

    public BuildingManageButton UpdateMainBarManager(
      float DeltaTime,
      Player player,
      out bool GoBack)
    {
      this.controllerbuildbarmatrix.UpdateController_BuildingStatisBar(player, DeltaTime, this);
      this.ShrinkLerper.UpdateLerpHandler(DeltaTime);
      float num = (float) (1.0 - (double) this.ShrinkLerper.Value * 0.25);
      GoBack = false;
      if (this.Back != null && !FeatureFlags.BlockBackFromMainBar)
      {
        MainBarManager.BarIsOnScreen = (double) this.Back.lerper.Value == 0.0 || (double) this.Back.lerper.TargetValue == 0.0;
        GoBack = this.Back.UpdateZ_BreakoutButton(Vector2.Zero, player, DeltaTime);
        if (!GoBack && (double) this.Back.lerper.Value == 0.0 && (player.inputmap.ReleasedThisFrame[7] && OverWorldManager.zoopopupHolder.IsNull()) && OverWorldManager.zoopopupHolder.TopLayerIsNull())
          GoBack = true;
      }
      if (this.Heading != null)
        this.Heading.UpdateZ_BreakoutButton(Vector2.Zero, player, DeltaTime);
      Vector2 zero = Vector2.Zero;
      if (this.barscroller != null)
      {
        this.barscroller.UpdateBarScroller(player, DeltaTime, ref zero, num);
        if (this.barscroller.GetCurrentPage() != this.BuildingOnfoButtons[this.ControllerSelected].PAGE)
          this.ControllerSelected = this.barscroller.GetCurrentPage() * this.BuildingOnfoButtons[0].TotalPerPage;
      }
      this.PressedIndex = -1;
      this.DpadHint.UpdateTRC_ButtonDisplay(DeltaTime);
      BuildingManageButton buildingManageButton = BuildingManageButton.Count;
      for (int index1 = 0; index1 < this.BuildingOnfoButtons.Count; ++index1)
      {
        if (this.BuildingOnfoButtons[index1].UpdatePenInfoButton(DeltaTime, player, this.ControllerSelected == index1, zero, num))
        {
          this.PressedIndex = index1;
          buildingManageButton = this.BuildingOnfoButtons[index1].ManageButtonType;
          player.player.touchinput.ReleaseTapArray[0].X = -10000f;
          this.BuildingOnfoButtons[index1].HardSelected = true;
          for (int index2 = 0; index2 < this.BuildingOnfoButtons.Count; ++index2)
          {
            if (index2 != index1)
              this.BuildingOnfoButtons[index2].HardSelected = false;
          }
        }
      }
      return buildingManageButton;
    }

    public TILETYPE GetTileType() => this.BuildingOnfoButtons[this.PressedIndex].GetTileType();

    public Vector2 GetShrinkOffset(out float _ShrinkValue)
    {
      if ((double) this.ShrinkLerper.Value != 0.0)
      {
        _ShrinkValue = (float) (1.0 - (double) this.ShrinkLerper.Value * 0.25);
        Vector2 vector2 = new Vector2(0.0f, (float) (75.0 * (double) this.BaseScale * (1.0 - (double) _ShrinkValue)));
        vector2.X -= (float) ((double) this.BaseScale * (double) this.ShrinkLerper.Value * 60.0);
        return vector2;
      }
      _ShrinkValue = 0.0f;
      return Vector2.Zero;
    }

    public void DrawMainBarManager(Player player)
    {
      float num = (float) (1.0 - (double) this.ShrinkLerper.Value * 0.25);
      SpriteBatch pointspritebatchTop05 = AssetContainer.pointspritebatchTop05;
      Vector2 zero1 = Vector2.Zero;
      if (TinyZoo.GameFlags.IsUsingController && this.BuildingOnfoButtons.Count > 1 && this.IsEnabled_Controller)
      {
        Vector2 Offset = Vector2.Zero;
        if (this.Heading != null)
          Offset = new Vector2(this.Heading.lerper.Value * this.Heading.LerperOffset.X * this.Heading.LerpMult, 0.0f);
        this.DpadHint.DrawTRC_ButtonDisplay(pointspritebatchTop05, AssetContainer.TRC_Sprites, Offset);
      }
      if (this.barscroller != null)
        this.barscroller.DrawBarScroller(pointspritebatchTop05, ref zero1, num);
      if (this.Heading != null)
        this.Heading.DrawZ_BreakoutButton(Vector2.Zero, pointspritebatchTop05, 1f - num);
      for (int index = 0; index < this.BuildingOnfoButtons.Count; ++index)
        this.BuildingOnfoButtons[index].DrawPenInfoButton(zero1, pointspritebatchTop05, num, player);
      if (this.Back != null && !FeatureFlags.BlockBackFromMainBar)
        this.Back.DrawZ_BreakoutButton(Vector2.Zero, pointspritebatchTop05);
      if (this.moralityDisplay == null)
        return;
      Vector2 zero2 = Vector2.Zero;
      if (this.Heading != null)
        zero2 += new Vector2(this.Heading.lerper.Value * this.Heading.LerperOffset.X * this.Heading.LerpMult, 0.0f);
      this.moralityDisplay.DrawMoralityScoreProgressionDisplay_WithFrame(zero2, pointspritebatchTop05);
    }
  }
}

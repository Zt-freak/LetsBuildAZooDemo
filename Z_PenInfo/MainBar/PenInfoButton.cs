// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.MainBar.PenInfoButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarInfo.MainBar;
using TinyZoo.Z_BarMenu.Build;
using TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Elements;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers;

namespace TinyZoo.Z_PenInfo.MainBar
{
  internal class PenInfoButton : GameObject
  {
    private LerpHandler_Float lerper;
    private GameObject BG;
    private Vector2 VScale;
    private BullDozerButton button;
    private BarButtonIcon barbuttonicon;
    public bool MouseOver_ControllerSelected;
    public BuildingManageButton ManageButtonType;
    public bool HardSelected;
    private GameObject SelectionHighlight;
    private GameObject RedFlash;
    private bool Disabled_BlockClicks;
    private float PageX;
    public int PAGE;
    private CustomerFrameMouseOverBox mouseovertext;
    private GoodEvilIcon goodEvilIcon;
    private SlashCross slashCross;
    private Vector2 goodEvilIconOffset;
    private DiseaseIcon diseaseIcon;
    private Vector2 diseaseIconOffset;
    private ShortcutHighlightIcon shortcuthighlight;
    private ShortcutHighlightIcon shortcuthighlightB;
    private GameObject SelectedFrame;
    public TILETYPE tiletype;
    private static RedLight redlight;
    private TileCategory tilecategory;
    private float BaseScale;
    public int TotalPerPage;
    private float RedValue;

    public bool Disabled { get; private set; }

    public PenInfoButton(
      int Index,
      float Height,
      BuildingManageButton _ManageButtonType,
      PrisonerInfo prisonerinfo = null,
      TILETYPE _tiletype = TILETYPE.Count,
      PenItem penitem = null,
      float _BaseScale = -1f)
    {
      this.ManageButtonType = _ManageButtonType;
      this.tiletype = _tiletype;
      this.SelectedFrame = new GameObject();
      this.SelectedFrame.DrawRect = new Rectangle(204, 155, 54, 54);
      this.SelectedFrame.SetDrawOriginToCentre();
      this.BaseScale = _BaseScale;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      switch (_ManageButtonType)
      {
        case BuildingManageButton.GetStaff:
          this.tilecategory = TileCategory.ManageStaff;
          break;
        case BuildingManageButton.GetParkStaff:
          this.tilecategory = TileCategory.GetParkStaff;
          break;
        case BuildingManageButton.CRISPR_menu:
          this.tilecategory = TileCategory.DNAChamber;
          break;
      }
      if (this.tiletype != TILETYPE.Count)
      {
        if (TileData.IsForFood(this.tiletype))
          this.tilecategory = TileCategory.Food;
        else if (TileData.IsForSouvenir(this.tiletype))
          this.tilecategory = TileCategory.Gifts;
        else if (TileData.IsForThirst(this.tiletype))
          this.tilecategory = TileCategory.Drink;
        else if (TileData.IsThisaToilet(this.tiletype))
          this.tilecategory = TileCategory.Toilet;
        else if (TileData.IsThisABin(this.tiletype))
          this.tilecategory = TileCategory.Bin;
        else if (TileData.IsThisABench(this.tiletype))
          this.tilecategory = TileCategory.Bench;
        else if (this.tiletype == TILETYPE.StoreRoom || this.tiletype == TILETYPE.DNABuilding || this.tiletype == TILETYPE.ArchitectOffice)
          this.tilecategory = TileCategory.StoreRoom;
      }
      this.PAGE = 0;
      this.Disabled = false;
      if (this.ManageButtonType == BuildingManageButton.Destroy)
        this.button = new BullDozerButton(ControllerButton.XboxA);
      else if (this.ManageButtonType == BuildingManageButton.Transfer)
        this.button = new BullDozerButton(ControllerButton.XboxY, true);
      this.barbuttonicon = new BarButtonIcon(_ManageButtonType, prisonerinfo, this.tiletype, penitem, this.BaseScale);
      if (this.tiletype != TILETYPE.Count)
        this.mouseovertext = new CustomerFrameMouseOverBox(Z_GameFlags.GetBaseScaleForUI(), TileData.GetTileStats(this.tiletype).Name, 125f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.lerper.SetDelay((float) Index * 0.1f);
      this.BG = new GameObject();
      this.BG.SetDrawOriginToCentre();
      this.BG.DrawRect = new Rectangle(232, 365, 50, 50);
      this.BG.scale = this.BaseScale * 2f;
      this.BG.SetDrawOriginToCentre();
      this.VScale = new Vector2(200f, Height);
      this.TotalPerPage = 15;
      this.TotalPerPage = (int) (1024.0 / ((double) this.BaseScale * 115.0));
      --this.TotalPerPage;
      int num1 = Index;
      if (Index >= this.TotalPerPage)
      {
        this.PAGE = Index / this.TotalPerPage;
        num1 %= this.TotalPerPage;
      }
      float num2 = 115f * this.BaseScale;
      float num3 = (float) ((1024.0 - (double) this.TotalPerPage * (115.0 * (double) this.BaseScale)) * 0.5);
      float x = (float) (140.0 * (double) this.BaseScale * 0.5);
      this.PageX = (float) ((double) num2 * 0.5 + (double) num1 * (double) num2);
      this.vLocation = new Vector2(x, Build_BarManager.GetVerticalCenterForAllIcons());
      this.vLocation.X += (float) (1024 * this.PAGE);
      this.SelectionHighlight = new GameObject(this.BG);
      this.SelectionHighlight.SetAllColours(1f, 1f, 1f);
      this.SelectionHighlight.DrawRect = new Rectangle(283, 370, 50, 50);
      this.SelectionHighlight.SetAllColours(ColourData.Z_TextOrange);
      this.BG.SetAllColours(ColourData.Z_PaleBrown);
      switch (_ManageButtonType)
      {
        case BuildingManageButton.BuildPen_Water:
          this.shortcuthighlight = new ShortcutHighlightIcon(OffscreenPointerType.PointAtNoWater, this.BaseScale);
          break;
        case BuildingManageButton.BuildPen_Enrichment:
          this.tilecategory = TileCategory.DNAChamber;
          break;
        case BuildingManageButton.Pen_AddItemsToPen:
          this.shortcuthighlight = new ShortcutHighlightIcon(OffscreenPointerType.PointAtNoWater, this.BaseScale);
          this.shortcuthighlightB = new ShortcutHighlightIcon(OffscreenPointerType.NoEnrichment, this.BaseScale);
          break;
        case BuildingManageButton.Tasks:
          this.shortcuthighlight = new ShortcutHighlightIcon(OffscreenPointerType.GoToQuestBuilding, this.BaseScale);
          break;
      }
      if (PenInfoButton.redlight == null)
        PenInfoButton.redlight = new RedLight(true, BaseScale: this.scale);
      PenInfoButton.redlight.scale = this.scale;
      PenInfoButton.redlight.vLocation = new Vector2(20f * this.scale, (float) (20.0 * -(double) this.scale) * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);

    public void LerpOn() => this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);

    public void UpdateTempExitLerp(float DeltaTime) => this.lerper.UpdateLerpHandler(DeltaTime);

    public void FlashRed() => this.RedValue = 1f;

    public void Disable(bool BlockMouseClicks = true)
    {
      this.Disabled = true;
      this.Disabled_BlockClicks = BlockMouseClicks;
      this.SelectionHighlight.SetAllColours(0.3f, 0.3f, 0.3f);
      this.BG.SetAllColours(0.5f, 0.5f, 0.5f);
      this.barbuttonicon.SetDisabled();
      if (this.mouseovertext == null)
        return;
      string textToWrite = "Locked. Research to unlock.";
      this.mouseovertext = new CustomerFrameMouseOverBox(Z_GameFlags.GetBaseScaleForUI(), textToWrite, 80f);
      this.barbuttonicon.ReplaceScrollerText("???");
    }

    public void AddMoralityIcons(bool IsGoodOrEvil, bool PlayerHasPointsToUse)
    {
      float scale = this.barbuttonicon.scale;
      this.goodEvilIcon = new GoodEvilIcon(IsGoodOrEvil, basescale_: scale);
      this.goodEvilIcon.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      this.goodEvilIconOffset = new Vector2((float) -(this.BG.DrawRect.Width - 6), (float) -(this.BG.DrawRect.Height - 6)) * scale * Sengine.ScreenRatioUpwardsMultiplier * 0.5f;
      if (PlayerHasPointsToUse)
        return;
      this.slashCross = new SlashCross(scale);
      SlashCross slashCross = this.slashCross;
      slashCross.vLocation = slashCross.vLocation + new Vector2(0.0f, -5f) * scale * Sengine.ScreenRatioUpwardsMultiplier;
      this.Disable();
    }

    public void AddDiseaseIcon()
    {
      float scale = this.barbuttonicon.scale;
      this.diseaseIcon = new DiseaseIcon(scale);
      this.diseaseIcon.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      this.diseaseIconOffset = new Vector2((float) -(this.BG.DrawRect.Width - 6), (float) -(this.BG.DrawRect.Height - 6)) * scale * Sengine.ScreenRatioUpwardsMultiplier * 0.5f;
    }

    public bool LerpOnComplete() => (double) this.lerper.Value == 0.0;

    public bool MouseOverlapping(Player player, Vector2 Offset, float ScaleMultiplier)
    {
      Offset.Y += this.lerper.Value * this.VScale.Y;
      Offset.X += this.PageX;
      return MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.BG.scale, (float) this.BG.DrawRect.Width, (float) this.BG.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
    }

    public bool UpdatePenInfoButton(
      float DeltaTime,
      Player player,
      bool ControllerSelected,
      Vector2 Offset,
      float ScaleMultiplier)
    {
      this.barbuttonicon.UpdateButtonIcon(DeltaTime);
      Offset.X += this.PageX * ScaleMultiplier;
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset.Y += this.lerper.Value * this.VScale.Y;
      this.BG.UpdateColours(DeltaTime);
      if (GameFlags.IsUsingController)
      {
        this.MouseOver_ControllerSelected = ControllerSelected;
        if (this.MouseOver_ControllerSelected && player.inputmap.PressedThisFrame[0])
        {
          if (!this.Disabled_BlockClicks)
          {
            player.inputmap.PressedThisFrame[0] = false;
            return true;
          }
          player.inputmap.PressedThisFrame[0] = false;
          this.BG.SetPrimaryColours(0.3f, new Vector3(0.8f, 0.2f, 0.2f));
        }
      }
      else
      {
        this.MouseOver_ControllerSelected = MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.BG.scale * ScaleMultiplier, (float) this.BG.DrawRect.Width, (float) this.BG.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
        if (this.MouseOver_ControllerSelected && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !this.Disabled_BlockClicks)
          return true;
      }
      return false;
    }

    public TILETYPE GetTileType() => this.barbuttonicon.GetTileType();

    public void DrawPenInfoButton(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float ScaleMultiplier,
      Player player)
    {
      Offset.Y += this.lerper.Value * this.VScale.Y;
      Offset.X += this.PageX * ScaleMultiplier;
      if (this.HardSelected && OverWorldManager.zoopopupHolder.IsNull())
        this.HardSelected = false;
      if (this.ManageButtonType == BuildingManageButton.BuildStructure_PEN)
      {
        this.HardSelected = false;
        if (MainBarManager.SelectedBuilding == this.tiletype)
          this.HardSelected = true;
      }
      this.BG.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation, this.BG.scale * ScaleMultiplier, this.BG.fAlpha);
      if (this.button != null)
      {
        this.button.dozer.scale = 4f;
        this.button.DrawBullDozerButton(Offset + this.vLocation, ScaleMultiplier);
      }
      if (this.HardSelected)
        this.SelectedFrame.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation, this.BG.scale * ScaleMultiplier, this.SelectedFrame.fAlpha);
      if (this.MouseOver_ControllerSelected && !FeatureFlags.BlockMouseOverOnBuildBar)
      {
        this.SelectionHighlight.fAlpha = 1f;
        this.SelectionHighlight.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation, this.SelectionHighlight.scale * ScaleMultiplier, this.SelectionHighlight.fAlpha);
      }
      else
      {
        this.SelectionHighlight.fAlpha = 0.2f;
        this.SelectionHighlight.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation, this.SelectionHighlight.scale * ScaleMultiplier, this.SelectionHighlight.fAlpha);
      }
      if ((double) this.RedValue > 0.0)
      {
        this.RedValue -= GameFlags.RefDeltaTime * 3f;
        if (this.RedFlash == null)
        {
          this.RedFlash = new GameObject(this.SelectionHighlight);
          this.RedFlash.SetAllColours(1f, 0.0f, 0.0f);
        }
        if ((double) this.RedValue > 0.0)
          this.RedFlash.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation, this.SelectionHighlight.scale * ScaleMultiplier, this.RedValue);
      }
      this.barbuttonicon.DrawBarButtonIcon(Offset + this.vLocation, AssetContainer.pointspritebatchTop05, ScaleMultiplier, this.BG.scale);
      if (this.tilecategory != TileCategory.None && !this.Disabled)
      {
        bool flag = false;
        if (this.tilecategory == TileCategory.Drink)
        {
          if (FeatureFlags.FlashBuildDrinksShop)
            flag = true;
        }
        else if (this.tilecategory == TileCategory.Food)
        {
          if (FeatureFlags.FlashBuildFoodShop)
            flag = true;
        }
        else if (this.tilecategory == TileCategory.Gifts)
        {
          if (FeatureFlags.FlashBuildGiftShop)
            flag = true;
        }
        else if (this.tilecategory == TileCategory.Bin)
        {
          if (FeatureFlags.FlashBuildBin)
            flag = true;
        }
        else if (this.tilecategory == TileCategory.Bench)
        {
          if (FeatureFlags.FlashBuildBench)
            flag = true;
        }
        else if (this.tilecategory == TileCategory.ManageStaff)
        {
          if (FeatureFlags.FlashHireStaffFromShop && SellBar.SelectedShopEntry != null && SellBar.SelectedShopEntry.GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(SellBar.SelectedShopEntry.tiletype, player))
            flag = true;
        }
        else if (this.tilecategory == TileCategory.Toilet)
        {
          if (FeatureFlags.FlashBuildToilet)
            flag = true;
        }
        else if (this.tiletype == TILETYPE.ArchitectOffice && FeatureFlags.FlashResearchFromTask)
          flag = true;
        else if (this.ManageButtonType == BuildingManageButton.GetParkStaff && (FeatureFlags.FlashHasApplicantsAtGate || FeatureFlags.FlashHireFromGateForQuest))
          flag = true;
        else if (this.tiletype == TILETYPE.StoreRoom && FeatureFlags.FlashStoreRoomFromTask)
          flag = true;
        else if (this.tiletype == TILETYPE.DNABuilding && FeatureFlags.FlashCRISPRFromTask)
          flag = true;
        else if (this.ManageButtonType == BuildingManageButton.CRISPR_menu && FeatureFlags.FlashCRISPRFromBirth)
          flag = true;
        if (flag)
        {
          PenInfoButton.redlight.scale = this.scale * 0.5f;
          PenInfoButton.redlight.vLocation = new Vector2(20f * this.scale, (float) (20.0 * -(double) this.scale) * Sengine.ScreenRatioUpwardsMultiplier.Y);
          PenInfoButton.redlight.DrawRedLight(spritebatch, spritebatch, Offset + this.vLocation, true);
        }
      }
      if (this.shortcuthighlight != null)
      {
        this.shortcuthighlight.DrawShortcutHighlightIcon(AssetContainer.pointspritebatchTop05, Offset + this.vLocation, this.scale);
        if (this.shortcuthighlightB != null)
          this.shortcuthighlightB.DrawShortcutHighlightIcon(AssetContainer.pointspritebatchTop05, Offset + this.vLocation, this.scale);
      }
      if (this.goodEvilIcon != null)
      {
        this.goodEvilIcon.DrawGoodEvilIcon(Offset + this.vLocation + this.goodEvilIconOffset * ScaleMultiplier, spritebatch, ScaleMultiplier);
        if (this.slashCross != null)
          this.slashCross.DrawSlashCross(Offset + this.vLocation, spritebatch, ScaleMultiplier);
      }
      if (this.diseaseIcon != null)
        this.diseaseIcon.DrawDiseaseIcon(Offset + this.vLocation + this.diseaseIconOffset * ScaleMultiplier, spritebatch, ScaleMultiplier);
      if (!this.MouseOver_ControllerSelected || this.mouseovertext == null)
        return;
      this.mouseovertext.Active = true;
      this.mouseovertext.DrawCustomerFrameMouseOverBoc(Offset + this.vLocation + new Vector2(0.0f, (float) (30.0 * -(double) this.scale) * Sengine.ScreenRatioUpwardsMultiplier.Y), spritebatch);
    }
  }
}

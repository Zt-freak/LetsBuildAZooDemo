// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SellBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_PenInfo;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SellUI
{
  internal class SellBar
  {
    private GameObject BGFrame;
    private Vector2 VSCale;
    private Vector2Int CenterBottomTile;
    private BullDozerButton DestroyButton;
    private BullDozerButton transferbutton;
    private Vector2 Offset;
    private bool HasTransfer;
    private bool IsResearch;
    private string MainString;
    private string BtinTwoString;
    public bool IsReanimate;
    public bool IsReanimate_SelectGraveYardMessage;
    private float ExtraX;
    private bool IsCellBlock;
    private string ValueSTring;
    private StringInBox eranings;
    public PenInfoManager SelectedStructureInfoBar;
    private BAR_TYPE bartype;
    internal static ShopEntry SelectedShopEntry;

    public SellBar(
      TILETYPE tiletype,
      Vector2Int _CenterBottomTile,
      float _ExtraX,
      bool _HasTransfer,
      bool _IsResearch,
      bool _IsReanimate,
      bool IsFullGraveyard,
      bool _IsCellBlock,
      int Earnings,
      int PotentialEarnings,
      Player player,
      PrisonZone zone = null,
      Vector2Int LocClickedConfusing = null)
    {
      this.IsCellBlock = _IsCellBlock;
      this.SelectedStructureInfoBar = (PenInfoManager) null;
      this.bartype = BAR_TYPE.SelectedBuildingRoot;
      if (this.IsCellBlock)
      {
        this.bartype = BAR_TYPE.Pen;
        if (zone.IsFarm)
          this.bartype = BAR_TYPE.FarmField;
      }
      else
      {
        SellBar.SelectedShopEntry = (ShopEntry) null;
        LayoutEntry baseTileType = player.prisonlayout.layout.BaseTileTypes[LocClickedConfusing.X, LocClickedConfusing.Y];
        if (baseTileType != null)
          SellBar.SelectedShopEntry = !baseTileType.GetIsChild() ? player.shopstatus.TryAndFindThisShop(LocClickedConfusing, tiletype) : player.shopstatus.TryAndFindThisShop(baseTileType.GetParentLocation(), tiletype);
      }
      this.SelectedStructureInfoBar = new PenInfoManager(this.bartype, _HasTransfer, tiletype, player, zone);
      if (this.IsCellBlock)
      {
        this.ValueSTring = SEngine.Localization.Localization.GetText(41) + " $" + (object) Earnings + "/$" + (object) PotentialEarnings;
        if (Earnings >= PotentialEarnings)
          this.ValueSTring = SEngine.Localization.Localization.GetText(41) + " $" + (object) Earnings + ".";
        this.eranings = new StringInBox(this.ValueSTring, 2f, 150f, true);
        if (Earnings >= PotentialEarnings)
          this.eranings.SetButtonGreen();
        else
          this.eranings.SetButtonRed();
      }
      this.ExtraX = _ExtraX;
      this.IsReanimate_SelectGraveYardMessage = IsFullGraveyard;
      this.IsReanimate = _IsReanimate;
      this.MainString = SEngine.Localization.Localization.GetText(10).ToUpper();
      this.IsResearch = _IsResearch;
      this.HasTransfer = _HasTransfer;
      this.CenterBottomTile = _CenterBottomTile;
      this.BGFrame = new GameObject();
      this.BGFrame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BGFrame.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.BGFrame.SetAllColours(0.0f, 0.0f, 0.0f);
      this.VSCale = new Vector2(150f, 48f);
      this.BGFrame.SetAlpha(0.5f);
      this.DestroyButton = new BullDozerButton(ControllerButton.XboxA);
      this.DestroyButton.vLocation.Y = this.VSCale.Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.DestroyButton.vLocation.Y -= 30f;
      this.DestroyButton.vLocation.Y += 15f * this.DestroyButton.dozer.scale;
      this.Offset = TileMath.GetTileToWorldSpace(this.CenterBottomTile);
      this.Offset.X += this.ExtraX;
      this.Offset = RenderMath.TranslateWorldSpaceToScreenSpace(this.Offset);
      if (this.SelectedStructureInfoBar != null && (this.HasTransfer || this.IsResearch || this.IsReanimate_SelectGraveYardMessage))
        this.SelectedStructureInfoBar.AddTransferButton();
      if (this.HasTransfer || this.IsResearch || this.IsReanimate_SelectGraveYardMessage)
      {
        this.BtinTwoString = SEngine.Localization.Localization.GetText(11).ToUpper();
        this.VSCale.X *= 2f;
        this.transferbutton = new BullDozerButton(ControllerButton.XboxY, true);
        if (this.IsCellBlock)
        {
          this.transferbutton.SetLeft();
          this.DestroyButton.SetRight();
        }
        else
        {
          this.transferbutton.SetLeft();
          this.DestroyButton.SetRight();
        }
        this.transferbutton.vLocation.Y = this.DestroyButton.vLocation.Y;
      }
      if (this.IsResearch)
      {
        this.BtinTwoString = SEngine.Localization.Localization.GetText(56).ToUpper();
        this.transferbutton.SetAsResearchButton();
      }
      if (this.IsReanimate)
      {
        this.DestroyButton.SetAsReanimate();
        this.MainString = "REVIVE";
      }
      else
      {
        if (!this.IsReanimate_SelectGraveYardMessage)
          return;
        this.BtinTwoString = "REVIVE";
        this.transferbutton.SetAsReanimate();
      }
    }

    public void TempLerpOff() => this.SelectedStructureInfoBar.TempLerpOff();

    public void LerpBackOn() => this.SelectedStructureInfoBar.LerpBackOn();

    public void UpdateTempExitLerp(float DeltaTime) => this.SelectedStructureInfoBar.UpdateTempExitLerp(DeltaTime);

    public BAR_TYPE GetCurrentBarType() => this.bartype;

    public bool IsMouseOverButton(Player player) => this.SelectedStructureInfoBar != null && this.SelectedStructureInfoBar.IsMouseOverButton(player);

    public bool UpdateSellBar(
      Player player,
      float DeltaTime,
      SelectionOutline selectionoutline,
      ref BuildingManageButton PressedThis,
      out bool ExitNow)
    {
      ExitNow = false;
      if (this.SelectedStructureInfoBar != null)
      {
        BuildingManageButton buildingManageButton = this.SelectedStructureInfoBar.UpdatePenInfoManager(DeltaTime, player, out ExitNow);
        if (buildingManageButton == BuildingManageButton.Count)
          return false;
        PressedThis = buildingManageButton;
        return true;
      }
      this.Offset = TileMath.GetTileToWorldSpace(this.CenterBottomTile);
      this.Offset.X += this.ExtraX;
      this.Offset = RenderMath.TranslateWorldSpaceToScreenSpace(this.Offset);
      if (selectionoutline != null)
      {
        float topInScreenSpace = selectionoutline.GetTopInScreenSpace();
        if ((double) this.Offset.Y > 768.0 - 60.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
        {
          this.Offset.Y = (float) (768.0 - 60.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
          if ((double) this.Offset.Y < (double) topInScreenSpace)
            this.Offset.Y = topInScreenSpace;
        }
        float num = 150f;
        float rightInScreenSpace = selectionoutline.GetRightInScreenSpace();
        if ((double) this.Offset.X - (double) num < 0.0)
        {
          this.Offset.X = num;
          if ((double) this.Offset.X > (double) rightInScreenSpace)
            this.Offset.X = rightInScreenSpace;
        }
        float leftInScreenSpace = selectionoutline.GetLeftInScreenSpace();
        if ((double) this.Offset.X + (double) num > 1024.0)
        {
          this.Offset.X = 1024f - num;
          if ((double) this.Offset.X < (double) leftInScreenSpace)
            this.Offset.X = leftInScreenSpace;
        }
      }
      if (this.DestroyButton.UpdateBullDozerButton(this.Offset, player) || player.inputmap.PressedThisFrame[23] || player.inputmap.PressedThisFrame[22])
      {
        if (!player.inputmap.PressedThisFrame[22] && !player.inputmap.PressedThisFrame[23])
          player.inputmap.ClearAllInput(player);
        return true;
      }
      if (this.transferbutton != null && this.transferbutton.UpdateBullDozerButton(this.Offset, player))
      {
        player.inputmap.ClearAllInput(player);
        return true;
      }
      if (GameFlags.IsUsingController)
      {
        int num1 = player.inputmap.PressedThisFrame[15] ? 1 : 0;
      }
      return false;
    }

    public void DrawSellBar(Player player)
    {
      if (this.SelectedStructureInfoBar != null)
      {
        this.SelectedStructureInfoBar.DrawPenInfoManager(player);
      }
      else
      {
        if (PlayerStats.language == Language.German)
        {
          double toSpecificLength1 = (double) TextFunctions.GetStringPercentageReScaledToSpecificLength(this.MainString, "XXXXXXXX", AssetContainer.springFont, true);
        }
        int num1 = this.IsCellBlock ? 1 : 0;
        this.DestroyButton.DrawBullDozerButton(this.Offset);
        if (!this.HasTransfer && !this.IsResearch && !this.IsReanimate_SelectGraveYardMessage)
          return;
        if (PlayerStats.language == Language.Russian)
        {
          double toSpecificLength2 = (double) TextFunctions.GetStringPercentageReScaledToSpecificLength(this.BtinTwoString, "XXXXXXXX", AssetContainer.springFont, true);
        }
        if (PlayerStats.language == Language.French || PlayerStats.language == Language.Spanish || PlayerStats.language == Language.Portuguese)
        {
          double toSpecificLength3 = (double) TextFunctions.GetStringPercentageReScaledToSpecificLength(this.BtinTwoString, "XXXXXXXX", AssetContainer.springFont, true);
        }
        if (PlayerStats.language == Language.German)
        {
          double toSpecificLength4 = (double) TextFunctions.GetStringPercentageReScaledToSpecificLength(this.BtinTwoString, "XXXXXXXXX", AssetContainer.springFont, true);
        }
        this.transferbutton.DrawBullDozerButton(this.Offset);
        bool flag = false;
        if (this.IsResearch && (!Researcher.IsCurrentlyResearching || Researcher.IsCurrentlyResearching && Researcher.ResearchComplete) && (int) ((double) OverWorldEnvironmentManager.FlashTimer * 5.0) % 2 == 0)
          flag = true;
        int num2 = flag ? 1 : 0;
      }
    }
  }
}

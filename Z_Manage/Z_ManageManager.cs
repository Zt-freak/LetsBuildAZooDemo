// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Z_ManageManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_Manage.Accounts;
using TinyZoo.Z_Manage.BuyLand;
using TinyZoo.Z_Manage.Hiring;
using TinyZoo.Z_Manage.Hiring.PossibleHires;
using TinyZoo.Z_Manage.Lab;
using TinyZoo.Z_Manage.MainButtons;
using TinyZoo.Z_Manage.Research;
using TinyZoo.Z_Manage.Transport;

namespace TinyZoo.Z_Manage
{
  internal class Z_ManageManager
  {
    private BackButton backbutton;
    private StoreBGManager storeBGManager;
    private LerpHandler_Float screenlerper;
    private Vector2 Offset;
    private bool Exiting;
    internal static ManageButtonType currentstate;
    private ManageMainSCreen managescreen;
    private HiringManager hiringmanager;
    private AccountsManager accountsmanager;
    private LabManager labmanager;
    private BuildingResearch buildingresearch;
    private BuyLandManager buylandmanager;
    private TransportManager transportmanager;

    public Z_ManageManager(Player player)
    {
      this.backbutton = new BackButton(true);
      this.screenlerper = new LerpHandler_Float();
      this.screenlerper.SetLerp(true, 1f, 0.0f, 3f);
      this.storeBGManager = new StoreBGManager(true);
      Z_ManageManager.currentstate = ManageButtonType.ModeSelect;
      this.managescreen = new ManageMainSCreen();
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.ResearchView)
        return;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
      Z_ManageManager.currentstate = ManageButtonType.Research;
      this.MakeScreen(ManageButtonType.Research, player);
    }

    private void MakeScreen(ManageButtonType GoHere, Player player)
    {
      switch (GoHere)
      {
        case ManageButtonType.Hiring:
          Z_ManageManager.currentstate = ManageButtonType.Hiring;
          this.backbutton.LerpOn();
          this.hiringmanager = new HiringManager(player);
          this.storeBGManager.BlendToNewColour(true, false);
          break;
        case ManageButtonType.Research:
          Z_ManageManager.currentstate = ManageButtonType.Research;
          this.backbutton.LerpOn();
          this.buildingresearch = new BuildingResearch(player);
          break;
        case ManageButtonType.Genomesequencing:
          Z_ManageManager.currentstate = ManageButtonType.Genomesequencing;
          this.backbutton.LerpOn();
          this.labmanager = new LabManager();
          break;
        case ManageButtonType.Accounts:
          Z_ManageManager.currentstate = ManageButtonType.Accounts;
          this.backbutton.LerpOn();
          this.accountsmanager = new AccountsManager(player);
          break;
        case ManageButtonType.BusUpgrades:
          Z_ManageManager.currentstate = ManageButtonType.BusUpgrades;
          this.backbutton.LerpOn();
          this.transportmanager = new TransportManager();
          break;
        case ManageButtonType.BuyLand:
          this.storeBGManager.BlendToNewColour(false, false, true);
          Z_ManageManager.currentstate = ManageButtonType.BuyLand;
          this.backbutton.LerpOn();
          this.buylandmanager = new BuyLandManager(player);
          break;
      }
    }

    public bool UpdateZ_ManageManager(float DeltaTime, Player player)
    {
      if (this.backbutton.UpdateBackButton(player, DeltaTime))
        player.inputmap.ReleasedThisFrame[7] = true;
      if (PossibleHireManager.ForceExitAllTheWay)
        player.inputmap.ReleasedThisFrame[7] = true;
      if ((double) this.screenlerper.Value == 0.0 && !this.Exiting && (!FeatureFlags.BlockExitFromManage && player.inputmap.PressedBackOnController()))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuClose);
        if (Z_ManageManager.currentstate == ManageButtonType.ModeSelect || PossibleHireManager.ForceExitAllTheWay)
        {
          this.Exiting = true;
          PossibleHireManager.ForceExitAllTheWay = false;
          this.screenlerper.SetLerp(false, 0.0f, 1f, 3f, true);
        }
        else
        {
          switch (Z_ManageManager.currentstate)
          {
            case ManageButtonType.Hiring:
              if (this.hiringmanager.TryToSwitch(player))
              {
                this.backbutton.LerpOn();
                break;
              }
              Z_ManageManager.currentstate = ManageButtonType.ModeSelect;
              this.managescreen = new ManageMainSCreen();
              this.storeBGManager.BlendToNewColour(false, true);
              this.backbutton.LerpOn();
              break;
            case ManageButtonType.Accounts:
              if (this.accountsmanager.BlockExit())
              {
                this.backbutton.LerpOn();
                break;
              }
              Z_ManageManager.currentstate = ManageButtonType.ModeSelect;
              this.managescreen = new ManageMainSCreen();
              this.storeBGManager.BlendToNewColour(false, true);
              this.backbutton.LerpOn();
              break;
            default:
              Z_ManageManager.currentstate = ManageButtonType.ModeSelect;
              this.managescreen = new ManageMainSCreen();
              this.storeBGManager.BlendToNewColour(false, true);
              this.backbutton.LerpOn();
              break;
          }
        }
        player.inputmap.ReleasedThisFrame[7] = false;
      }
      this.screenlerper.UpdateLerpHandler(DeltaTime);
      this.Offset = new Vector2(this.screenlerper.Value * 1024f, 0.0f);
      this.storeBGManager.UpdateStoreBGManager(DeltaTime);
      if (Z_ManageManager.currentstate == ManageButtonType.ModeSelect)
      {
        ManageButtonType GoHere = this.managescreen.UpdateManageMainSCreen(player, DeltaTime, this.Offset);
        switch (GoHere)
        {
          case ManageButtonType.Hiring:
          case ManageButtonType.BuyLand:
          case ManageButtonType.Count:
            break;
          default:
            this.MakeScreen(GoHere, player);
            break;
        }
      }
      if (Z_ManageManager.currentstate == ManageButtonType.Hiring)
        this.hiringmanager.UpdateHiringManager(player, DeltaTime, this.Offset);
      else if (Z_ManageManager.currentstate == ManageButtonType.Research)
        this.buildingresearch.UpdateBuildingResearch(player, DeltaTime);
      else if (Z_ManageManager.currentstate == ManageButtonType.Genomesequencing)
        this.labmanager.UpdateLabManager(player, DeltaTime);
      else if (Z_ManageManager.currentstate == ManageButtonType.Accounts)
      {
        bool FlippedToNewState;
        this.accountsmanager.UpdateAccountsManager(player, DeltaTime, this.Offset, out FlippedToNewState);
        if (FlippedToNewState)
          this.backbutton.LerpOn();
      }
      else if (Z_ManageManager.currentstate == ManageButtonType.BusUpgrades)
        this.transportmanager.UpdateTransportManager(player, DeltaTime);
      else if (Z_ManageManager.currentstate == ManageButtonType.BuyLand)
        this.buylandmanager.UpdateBuyLandManager(DeltaTime, player, this.Offset);
      return this.Exiting && (double) this.screenlerper.Value == (double) this.screenlerper.TargetValue;
    }

    public void DrawZ_ManageManager()
    {
      this.storeBGManager.DrawStoreBGManager(this.Offset);
      if (!FeatureFlags.BlockExitFromManage)
        this.backbutton.DrawBackButton(this.Offset);
      switch (Z_ManageManager.currentstate)
      {
        case ManageButtonType.Hiring:
          this.hiringmanager.DrawHiringManager(this.Offset);
          break;
        case ManageButtonType.Research:
          this.buildingresearch.DrawBuildingResearch();
          break;
        case ManageButtonType.Genomesequencing:
          this.labmanager.DrawLabManager();
          break;
        case ManageButtonType.Accounts:
          this.accountsmanager.DrawAccountsManager(this.Offset);
          break;
        case ManageButtonType.BusUpgrades:
          this.transportmanager.DrawTransportManager();
          break;
        case ManageButtonType.BuyLand:
          this.buylandmanager.DrawBuyLandManager(this.Offset);
          break;
        case ManageButtonType.ModeSelect:
          this.managescreen.DrawManageMainSCreen(this.Offset);
          break;
      }
    }
  }
}

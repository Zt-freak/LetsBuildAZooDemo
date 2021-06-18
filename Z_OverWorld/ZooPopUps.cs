// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.ZooPopUps
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_Notification;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock;

namespace TinyZoo.Z_OverWorld
{
  internal class ZooPopUps
  {
    private ZooPopUp zoopopup;
    private ZooPopUp TopPopUp;
    private BlackOut blackout;

    public void CreateZooPopUps(WalkingPerson person, Player player) => this.zoopopup = new ZooPopUp(person, player);

    public void CreateZooPopUps(POPUPSTATE popupstate, WalkingPerson walkingperson)
    {
      this.CreateBlackOut();
      this.TopPopUp = new ZooPopUp(popupstate, walkingperson);
    }

    public bool IsInAStateThatShouldBlockSave() => false;

    public bool TimboTutorial() => this.zoopopup != null && this.zoopopup.thisstate == POPUPSTATE.HeroQuests && this.zoopopup.heroquestPanel.GetBlockExitFroTimboTutorial();

    private void CreateBlackOut()
    {
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(true, 0.2f, 0.0f, 0.7f);
    }

    public bool TopLayerIsNull() => this.TopPopUp == null;

    public bool IsNull() => this.zoopopup == null;

    public void SetNull() => this.zoopopup = (ZooPopUp) null;

    public bool ScrubOnBuyNewLand(Player player)
    {
      if (this.zoopopup == null || this.zoopopup.CheckHolderTimer())
        return false;
      if (this.zoopopup.thisstate == POPUPSTATE.HeroQuests && !this.zoopopup.heroquestPanel.TryAndClose(player))
      {
        this.SetNull();
        return true;
      }
      this.zoopopup = (ZooPopUp) null;
      return true;
    }

    public bool ScrubOnEnteringBuildMode(Player player)
    {
      if (this.zoopopup != null && !this.zoopopup.CheckHolderTimer())
      {
        if (this.zoopopup.thisstate == POPUPSTATE.HeroQuests && !this.zoopopup.heroquestPanel.TryAndClose(player))
        {
          this.SetNull();
          return true;
        }
        if (this.zoopopup != null)
        {
          this.zoopopup = (ZooPopUp) null;
          return true;
        }
      }
      return false;
    }

    public bool ScrubOnCancelBar(Player player)
    {
      if (this.zoopopup != null && !this.zoopopup.CheckHolderTimer())
      {
        if (this.zoopopup.thisstate == POPUPSTATE.HeroQuests)
        {
          if (!this.zoopopup.heroquestPanel.TryAndClose(player))
          {
            this.SetNull();
            return true;
          }
        }
        else if (this.zoopopup.thisstate != POPUPSTATE.People && (this.zoopopup.thisstate != POPUPSTATE.HeroQuests || !this.zoopopup.heroquestPanel.GetBlockExitFroTimboTutorial()))
        {
          this.zoopopup = (ZooPopUp) null;
          return true;
        }
      }
      return false;
    }

    public bool ScrubOnOpenNewBar(BAR_TYPE bartype, TILETYPE _tiletype, Player player)
    {
      if (this.zoopopup != null && !this.zoopopup.CheckHolderTimer())
      {
        switch (this.zoopopup.thisstate)
        {
          case POPUPSTATE.Notification:
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.Animal:
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.Ticket:
          case POPUPSTATE.GeneralEmployees:
          case POPUPSTATE.ParkSummary:
          case POPUPSTATE.Transport:
            switch (bartype)
            {
              case BAR_TYPE.SelectedBuildingRoot:
                if (TileData.IsATicketOffice(_tiletype))
                  goto label_21;
                else
                  break;
              case BAR_TYPE.TicketBooth:
                goto label_21;
            }
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.ManageEmployee:
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.HeroQuests:
            if (this.zoopopup.thisstate == POPUPSTATE.HeroQuests && this.zoopopup.heroquestPanel.GetBlockExitFroTimboTutorial())
              return false;
            if (this.zoopopup.thisstate == POPUPSTATE.HeroQuests && !this.zoopopup.heroquestPanel.TryAndClose(player))
            {
              this.SetNull();
              return true;
            }
            if (bartype == BAR_TYPE.SelectedBuildingRoot && TileData.IsAManagementOffice(_tiletype))
              return false;
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.ChangeBuildingSkin:
            if (this.zoopopup.changebuildingskinmanager != null && bartype == BAR_TYPE.SelectedBuildingRoot && this.zoopopup.changebuildingskinmanager.IsChangingThisSkin(_tiletype))
              return false;
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.Collection:
            if (bartype == BAR_TYPE.SelectedBuildingRoot && TileData.IsAManagementOffice(_tiletype))
              return false;
            this.zoopopup = (ZooPopUp) null;
            return true;
        }
      }
label_21:
      return false;
    }

    public bool ScrubOnExitMainBar()
    {
      if (this.zoopopup != null && !this.zoopopup.CheckHolderTimer())
      {
        switch (this.zoopopup.thisstate)
        {
          case POPUPSTATE.HeroQuests:
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.Collection:
            this.zoopopup = (ZooPopUp) null;
            return true;
        }
      }
      return false;
    }

    public bool ScrubOnOpenBuildMenu(Player player)
    {
      if (this.zoopopup != null && !this.zoopopup.CheckHolderTimer())
      {
        switch (this.zoopopup.thisstate)
        {
          case POPUPSTATE.HeroQuests:
            if (this.zoopopup.heroquestPanel.TryAndClose(player))
            {
              this.SetNull();
              return true;
            }
            this.zoopopup = (ZooPopUp) null;
            return true;
          case POPUPSTATE.Collection:
            this.zoopopup = (ZooPopUp) null;
            return true;
        }
      }
      return false;
    }

    public void CreateZooPopUps(
      Player player,
      bool IsBreakOut,
      PrisonZone prisonzone,
      POPUPSTATE popupstate = POPUPSTATE.None)
    {
      this.zoopopup = new ZooPopUp(player, IsBreakOut, prisonzone, popupstate);
    }

    public void CreateZooPopUps(BuildingManageButton actiontype, Player player) => this.zoopopup = new ZooPopUp(actiontype, player);

    public void CreateZooPopUps(
      BuildingManageButton actiontype,
      PrisonZone prisonzone,
      Player player)
    {
      this.zoopopup = new ZooPopUp(actiontype, prisonzone, player);
    }

    public void CreateZooPopUps(bool ISTEST_CONSTRUCTOR, Player player) => this.zoopopup = new ZooPopUp(ISTEST_CONSTRUCTOR, player);

    public bool HasThisPopUp(POPUPSTATE popuptype) => this.zoopopup != null && this.zoopopup.thisstate == popuptype;

    public void CreateZooPopUps(
      POPUPSTATE popstate,
      Player player,
      TILETYPE buildingtype,
      Vector2Int SelectedLcation)
    {
      this.zoopopup = new ZooPopUp(popstate, player, buildingtype, SelectedLcation);
    }

    public void CreateZooPopUps(
      WorkZoneInfo workZoneInfo,
      TILETYPE buildingtype = TILETYPE.Count,
      SelectedAndSellManager selectedtileandsell = null)
    {
      this.zoopopup = new ZooPopUp(workZoneInfo, buildingtype, selectedtileandsell);
    }

    public void CreateZooPopUps(QuarantineBuilding quaratineBuilding, Player player) => this.zoopopup = new ZooPopUp(quaratineBuilding, player);

    public void CreateZooPopUps(Player player, Vector2Int Buildinglocation, POPUPSTATE popupstate) => this.zoopopup = new ZooPopUp(player, Buildinglocation, popupstate);

    public void CreateZooPopUps(Employee employeetoquit, ZOOMOMENT moment) => this.zoopopup = new ZooPopUp(employeetoquit, moment);

    public void CreateZooPopUps(
      POPUPSTATE popstate,
      Vector2Int builingLocation,
      TILETYPE tileType,
      Player player)
    {
      this.zoopopup = new ZooPopUp(popstate, builingLocation, tileType, player);
    }

    public void CreateZooPopUps(PrisonerInfo anaimal, Player player) => this.zoopopup = new ZooPopUp(anaimal, player);

    public void CreateZooPopUps(AnimalType genomeUnlocked, POPUPSTATE popupstate) => this.zoopopup = new ZooPopUp(genomeUnlocked, popupstate);

    public void CreateZooPopUps(
      List<NotificationPackage> notifications,
      out bool RemoveThisNotification,
      Player player)
    {
      this.zoopopup = new ZooPopUp(notifications, out RemoveThisNotification, player);
    }

    public void CreateZooPopUps(ZooMoment zoomoment, Player player, out bool Launched) => this.zoopopup = new ZooPopUp(zoomoment, player, out Launched);

    public void CreateFeatureReveal(FeatureUnlockDisplayType featureunlockdisplaytype)
    {
      this.CreateBlackOut();
      this.TopPopUp = new ZooPopUp(featureunlockdisplaytype);
    }

    public void CreateZooPopUps(Player player, POPUPSTATE popupstate)
    {
      if (popupstate == POPUPSTATE.SaveAlert)
      {
        this.CreateBlackOut();
        this.TopPopUp = new ZooPopUp(player, popupstate);
      }
      else
        this.zoopopup = new ZooPopUp(player, popupstate);
    }

    public void CreaLockingCharacterStory(FeatureUnlockSpeechPack pack)
    {
      this.CreateBlackOut();
      this.TopPopUp = new ZooPopUp(FeatureUnlockDisplayType.Speech, pack);
    }

    public void CreateZooPopUps(Player player, int FarmFieldUID, POPUPSTATE popupstate)
    {
      if (popupstate != POPUPSTATE.Crops)
        throw new Exception("NOT THIS");
      this.zoopopup = new ZooPopUp(player, FarmFieldUID, popupstate);
    }

    public void CreateZooPopUps(
      HeroQuestDescription questDesc,
      Player player,
      POPUPSTATE popupstate,
      bool isForQuestCompleted)
    {
      this.zoopopup = new ZooPopUp(questDesc, player, popupstate, isForQuestCompleted);
    }

    public void SetbreakOutData(int HumanDeaths, int AnimalDeaths, int AnimalsLoose) => this.zoopopup.SetbreakOutData(HumanDeaths, AnimalDeaths, AnimalsLoose);

    public bool ShouldCancelDeltaTime()
    {
      if (this.TopPopUp != null)
        return true;
      return this.zoopopup != null && this.zoopopup.ShouldCancelDeltaTime();
    }

    public bool CheckMouseOver(Player player)
    {
      if (this.TopPopUp != null)
        return true;
      return this.zoopopup != null && OverWorldManager.overworldstate != OverWOrldState.CellSelect && this.zoopopup.CheckMouseOver(player);
    }

    public bool GetHasState(POPUPSTATE state) => this.zoopopup.thisstate == state;

    public bool UpdateZooPopUps(Player player, float DeltaTime)
    {
      if (this.TopPopUp != null)
      {
        this.blackout.UpdateColours(DeltaTime);
        if (this.TopPopUp.UpdateZooPopUps(player, DeltaTime))
        {
          if (this.TopPopUp.ForceThisFeatureUnlockedPopUp != FeatureUnlockDisplayType.Count)
          {
            this.TopPopUp = new ZooPopUp(this.TopPopUp.ForceThisFeatureUnlockedPopUp);
          }
          else
          {
            bool flag = true;
            if (this.TopPopUp.thisstate == POPUPSTATE.SaveAlert && Player.financialrecords.GetDaysPassed() == 13L && Z_DebugFlags.IsBetaVersion)
            {
              flag = false;
              this.TopPopUp = new ZooPopUp(FeatureUnlockDisplayType.Speech, new FeatureUnlockSpeechPack("Hi, I have had a great holiday, and am finally on my way home. I can't wait to see what you did with the place.~I can't thank you enough for your help, but sadly:~~THIS IS YOUR LAST DAY RUNNING THE ZOO~~I hope you had fun, and next time, Let's Build a Zoo together!", AnimalType.SpecialEvent_PipTheZooKeeper, "Pip's Holiday"));
            }
            if (flag)
            {
              this.blackout = (BlackOut) null;
              this.TopPopUp = (ZooPopUp) null;
              if (this.zoopopup == null)
                return true;
            }
          }
        }
        player.inputmap.ClearAllInput(player);
        return false;
      }
      return this.zoopopup != null && OverWorldManager.overworldstate != OverWOrldState.CellSelect && this.zoopopup.UpdateZooPopUps(player, DeltaTime);
    }

    public void DrawZooPopUps()
    {
      if (OverWorldManager.IsGameIntro)
        return;
      if (this.zoopopup != null)
      {
        if (OverWorldManager.overworldstate == OverWOrldState.CellSelect)
        {
          if (this.TopPopUp == null)
            return;
        }
        else
          this.zoopopup.DrawZooPopUp();
      }
      if (this.TopPopUp == null)
        return;
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.TopPopUp.DrawZooPopUp();
    }
  }
}

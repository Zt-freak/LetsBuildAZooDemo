// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.PointAtThings.PointOffScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_Fights;
using TinyZoo.Z_HUD.Z_Notification;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_Notification;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_HUD.PointAtThings
{
  internal class PointOffScreenManager
  {
    private static List<EventPointer> eventpointers;
    private static EventPointer QuestPointer;
    private static List<NotificationLists> notificationstotrack;

    private void Processotification(Player player)
    {
      bool flag1 = false;
      switch (PointOffScreenManager.notificationstotrack[0].notificationtotrack[0].notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          this.RemovByEventType(OffscreenPointerType.NewBirths);
          for (int index = 0; index < PointOffScreenManager.notificationstotrack[0].notificationtotrack.Count; ++index)
          {
            PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.NewBirths, player, -1, OverWorldManager.overworldenvironment.animalsinpens.GetAnimalRendererByUID(PointOffScreenManager.notificationstotrack[0].notificationtotrack[index].AnimalOrPenUID, -1, out int _));
            flag1 = true;
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.NewBirths, player);
          break;
        case Z_NotificationType.A_AnimalHunger:
          this.RemovByEventType(OffscreenPointerType.HungryAnimals);
          for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
          {
            for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
            {
              if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysWithoutFood > 2)
              {
                flag1 = true;
                PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.HungryAnimals, player, -1, OverWorldManager.overworldenvironment.animalsinpens.GetAnimalRendererByUID(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.UID, player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID, out int _));
              }
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.HungryAnimals, player);
          break;
        case Z_NotificationType.A_AnimalDeath:
          this.RemovByEventType(OffscreenPointerType.DeadAnimals);
          for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
          {
            for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
            {
              if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].IsDead)
              {
                flag1 = true;
                PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.DeadAnimals, player, -1, OverWorldManager.overworldenvironment.animalsinpens.GetAnimalRendererByUID(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.UID, player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID, out int _));
              }
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.DeadAnimals, player);
          break;
        case Z_NotificationType.A_CRISPR_HybridBirth:
          for (int index1 = 0; index1 < PointOffScreenManager.notificationstotrack.Count; ++index1)
          {
            for (int index2 = 0; index2 < PointOffScreenManager.notificationstotrack[index1].notificationtotrack.Count; ++index2)
            {
              NotificationPackage notificationPackage = PointOffScreenManager.notificationstotrack[index1].notificationtotrack[index2];
              PointOffScreenManager.AddPointer(player.crisprBreeds.GetBuildingForThisBreed(notificationPackage.AnimalOrPenUID).Location + new Vector2Int(0, -2), OffscreenPointerType.CripserBirth);
            }
          }
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.CripserBirth, player);
          break;
        case Z_NotificationType.A_AddAnimalsToYourPen:
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.AddAnimalsToYourPen, player);
          for (int index = 0; index < PointOffScreenManager.notificationstotrack[0].notificationtotrack[0].ListOfImpactedAnimalsByUID.Count; ++index)
            PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.AddAnimalsToYourPen, player, PointOffScreenManager.notificationstotrack[0].notificationtotrack[0].ListOfImpactedAnimalsByUID[index]);
          break;
        case Z_NotificationType.A_NoWater:
          this.RemovByEventType(OffscreenPointerType.PointAtNoWater);
          for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          {
            if ((double) player.prisonlayout.cellblockcontainer.prisonzones[index].TempTotalWater <= 0.0)
            {
              flag1 = true;
              PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.PointAtNoWater, player, player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID);
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.PointAtNoWater, player);
          break;
        case Z_NotificationType.A_NoEnrichment:
          this.RemovByEventType(OffscreenPointerType.NoEnrichment);
          for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          {
            if ((double) player.prisonlayout.cellblockcontainer.prisonzones[index].TempAnimalEnrichmentUnfulfillment > 0.0)
            {
              flag1 = true;
              PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.NoEnrichment, player, player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID);
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.NoEnrichment, player);
          break;
        case Z_NotificationType.F_BuildAnyShop:
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.BuildAnyShop, player);
          break;
        case Z_NotificationType.F_BuildAFoodShop:
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.BuildAFoodShop, player);
          break;
        case Z_NotificationType.F_BuildADrinksShop:
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.BuildADrinksShop, player);
          break;
        case Z_NotificationType.F_BuildAGiftShop:
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.BuildAGiftShop, player);
          break;
        case Z_NotificationType.F_AShopNeedsAnEmployee:
          this.RemovByEventType(OffscreenPointerType.ShopNeedsEmployee);
          for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
          {
            if (player.shopstatus.shopentries[index].GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(player.shopstatus.shopentries[index].tiletype, player))
            {
              flag1 = true;
              PointOffScreenManager.AddPointer(player.shopstatus.shopentries[index].LocationOfThisShop, OffscreenPointerType.ShopNeedsEmployee);
            }
          }
          for (int index = 0; index < player.shopstatus.ArchitectOffice.Count; ++index)
          {
            if (player.shopstatus.ArchitectOffice[index].GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(player.shopstatus.ArchitectOffice[index].tiletype, player))
            {
              flag1 = true;
              PointOffScreenManager.AddPointer(player.shopstatus.ArchitectOffice[index].LocationOfThisShop, OffscreenPointerType.ShopNeedsEmployee);
            }
          }
          for (int index = 0; index < player.shopstatus.FacilitiesWithEmployees.Count; ++index)
          {
            if (player.shopstatus.FacilitiesWithEmployees[index].GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(player.shopstatus.FacilitiesWithEmployees[index].tiletype, player))
            {
              flag1 = true;
              PointOffScreenManager.AddPointer(player.shopstatus.shopentries[index].LocationOfThisShop, OffscreenPointerType.ShopNeedsEmployee);
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.ShopNeedsEmployee, player);
          break;
        case Z_NotificationType.F_AJobHasApplicants:
          this.RemovByEventType(OffscreenPointerType.JobApplicants);
          List<OpenPositions> applicantsOpenPositions = player.employees.openPositionsContainer.GetAllJobsWithApplicantsOpenPositions();
          bool flag2 = false;
          for (int index1 = 0; index1 < applicantsOpenPositions.Count; ++index1)
          {
            if (applicantsOpenPositions[index1].RoamingEmployeeType != EmployeeType.None)
            {
              if (applicantsOpenPositions[index1].RoamingEmployeeType != EmployeeType.Keeper && !flag2)
              {
                flag2 = true;
                flag1 = true;
                Vector2Int locationvector2Int = TileMath.GetGateLocationvector2Int();
                locationvector2Int.X += 2;
                locationvector2Int.Y -= 2;
                PointOffScreenManager.AddPointer(locationvector2Int, OffscreenPointerType.JobApplicants);
              }
            }
            else if (TileData.IsAnArchitectOffice(applicantsOpenPositions[index1].tileType))
            {
              for (int index2 = 0; index2 < player.shopstatus.ArchitectOffice.Count; ++index2)
              {
                ShopEntry shopEntry = player.shopstatus.ArchitectOffice[index2];
                flag1 = true;
                PointOffScreenManager.AddPointer(shopEntry.LocationOfThisShop, OffscreenPointerType.JobApplicants);
              }
            }
            else if (TileData.IsThisAFacility(applicantsOpenPositions[index1].tileType))
            {
              for (int index2 = 0; index2 < player.shopstatus.FacilitiesWithEmployees.Count; ++index2)
              {
                if (player.shopstatus.FacilitiesWithEmployees[index2].tiletype == applicantsOpenPositions[index1].tileType)
                {
                  ShopEntry facilitiesWithEmployee = player.shopstatus.FacilitiesWithEmployees[index2];
                  flag1 = true;
                  PointOffScreenManager.AddPointer(facilitiesWithEmployee.LocationOfThisShop, OffscreenPointerType.JobApplicants);
                }
              }
            }
            else
            {
              for (int index2 = 0; index2 < player.shopstatus.shopentries.Count; ++index2)
              {
                if (player.shopstatus.shopentries[index2].tiletype == applicantsOpenPositions[index1].tileType)
                {
                  ShopEntry shopentry = player.shopstatus.shopentries[index2];
                  flag1 = true;
                  PointOffScreenManager.AddPointer(shopentry.LocationOfThisShop, OffscreenPointerType.JobApplicants);
                }
              }
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.JobApplicants, player);
          break;
        case Z_NotificationType.A_NoWaterConnection:
          this.RemovByEventType(OffscreenPointerType.NoWaterConnection);
          for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          {
            if (player.prisonlayout.cellblockcontainer.prisonzones[index].HasWaterTrough() && (double) player.prisonlayout.cellblockcontainer.prisonzones[index].TempTotalWater <= 0.0)
            {
              flag1 = true;
              PointOffScreenManager.AddPointerOnPenOrAnimal(OffscreenPointerType.NoWaterConnection, player, player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID);
            }
          }
          if (!flag1)
            break;
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.NoWaterConnection, player);
          break;
        case Z_NotificationType.F_TicketPrice:
          this.RemovByEventType(OffscreenPointerType.TicketPrice);
          PointOffScreenManager.AddPointer(TileMath.GetGateLocationvector2Int() + new Vector2Int(2, -2), OffscreenPointerType.TicketPrice);
          ZHudManager.zquestpins.PinRibbonTrackView(OffscreenPointerType.TicketPrice, player);
          break;
      }
    }

    private void RemovByEventType(OffscreenPointerType offscreenPointerType)
    {
      for (int index = PointOffScreenManager.eventpointers.Count - 1; index > -1; --index)
      {
        if (PointOffScreenManager.eventpointers[index].offscreenPointerType == offscreenPointerType)
          PointOffScreenManager.eventpointers.RemoveAt(index);
      }
    }

    public PointOffScreenManager()
    {
      PointOffScreenManager.eventpointers = new List<EventPointer>();
      if (PointOffScreenManager.QuestPointer != null)
        return;
      PointOffScreenManager.QuestPointer = new EventPointer(SpecialEventType.GoToQuestBuilding, OffscreenPointerType.GoToQuestBuilding);
      PointOffScreenManager.QuestPointer.SetTargetLocation(TileMath.GetTileToWorldSpace(new Vector2Int(152, 202)));
    }

    internal static bool DoubleCheckNotification(Z_NotificationType notificationType)
    {
      OffscreenPointerType offscreenPointerType = NotificationPackage.GetZ_NotificationTypeToOffscreenPointerType(notificationType);
      int index = 0;
      return index < PointOffScreenManager.eventpointers.Count && PointOffScreenManager.eventpointers[index].offscreenPointerType == offscreenPointerType;
    }

    internal static void AddPointerFromNotification(List<NotificationPackage> _notificationtotrack)
    {
      if (PointOffScreenManager.notificationstotrack == null)
        PointOffScreenManager.notificationstotrack = new List<NotificationLists>();
      PointOffScreenManager.notificationstotrack.Add(new NotificationLists(_notificationtotrack));
    }

    internal static void AddPointer(
      AnimalRenderMan enemyA,
      AnimalRenderMan EnemyB,
      SpecialEventType specialevent,
      FightManager fightman)
    {
      EventPointer eventPointer = new EventPointer(SpecialEventType.AnimalFight, OffscreenPointerType.AnimalFight);
      eventPointer.fightmanager = fightman;
      PointOffScreenManager.eventpointers.Add(eventPointer);
      eventPointer.PointAtThisAnimal = enemyA.enemyrenderere;
      eventPointer.PointAtThisOtherAnimal = EnemyB.enemyrenderere;
    }

    internal static void SetTaskPointerLocation(int Xloc, int YLoc)
    {
      if (PointOffScreenManager.QuestPointer == null)
        PointOffScreenManager.QuestPointer = new EventPointer(SpecialEventType.GoToQuestBuilding, OffscreenPointerType.GoToQuestBuilding);
      PointOffScreenManager.QuestPointer.SetTargetLocation(TileMath.GetTileToWorldSpace(new Vector2Int(Xloc, YLoc)));
    }

    internal static bool QuestPointerIsActive(BuildingManageButton thistype = BuildingManageButton.Count)
    {
      switch (thistype)
      {
        case BuildingManageButton.Pen_AddItemsToPen:
          for (int index = PointOffScreenManager.eventpointers.Count - 1; index > -1; --index)
          {
            if (PointOffScreenManager.eventpointers[index].offscreenPointerType == OffscreenPointerType.PointAtNoWater || PointOffScreenManager.eventpointers[index].offscreenPointerType == OffscreenPointerType.NoWaterConnection || PointOffScreenManager.eventpointers[index].offscreenPointerType == OffscreenPointerType.NoEnrichment)
              return true;
          }
          break;
        case BuildingManageButton.Count:
          if (PointOffScreenManager.QuestPointer != null)
            return PointOffScreenManager.QuestPointer.bActive;
          break;
      }
      return false;
    }

    internal static bool QuestPointerIsActive(OffscreenPointerType offscreenpoiintertpe)
    {
      for (int index = PointOffScreenManager.eventpointers.Count - 1; index > -1; --index)
      {
        if (PointOffScreenManager.eventpointers[index].offscreenPointerType == offscreenpoiintertpe)
          return true;
      }
      return false;
    }

    internal static void RemovePointer(OffscreenPointerType offsecreenpointerrype)
    {
      for (int index = PointOffScreenManager.eventpointers.Count - 1; index > -1; --index)
      {
        if (PointOffScreenManager.eventpointers[index].offscreenPointerType == offsecreenpointerrype)
          PointOffScreenManager.eventpointers.RemoveAt(index);
      }
    }

    internal static void RemovePointer(
      SpecialEventType pointertype,
      TutorialQuestSpecial _tutorialquestspecial)
    {
      for (int index = 0; index < PointOffScreenManager.eventpointers.Count; ++index)
      {
        if (PointOffScreenManager.eventpointers[index].tutorialquestspecial == _tutorialquestspecial && PointOffScreenManager.eventpointers[index].eventtype == pointertype)
        {
          PointOffScreenManager.eventpointers.RemoveAt(index);
          break;
        }
      }
    }

    internal static void AddPointerOnPenOrAnimal(
      OffscreenPointerType pointertype,
      Player player,
      int CellUID,
      AnimalRenderMan animalrender = null)
    {
      if (animalrender != null)
      {
        int uid = animalrender.REF_prisonerinfo.intakeperson.UID;
        PointOffScreenManager.eventpointers.Add(new EventPointer(SpecialEventType.COunt, pointertype));
        PointOffScreenManager.eventpointers[PointOffScreenManager.eventpointers.Count - 1].PointAtThisAnimal = animalrender.enemyrenderere;
      }
      else
      {
        Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(player.prisonlayout.GetThisCellBlock(CellUID).GetSpaceBehindGate());
        PointOffScreenManager.eventpointers.Add(new EventPointer(SpecialEventType.COunt, pointertype));
        PointOffScreenManager.eventpointers[PointOffScreenManager.eventpointers.Count - 1].SetTargetLocation(tileToWorldSpace);
      }
    }

    internal static void AddPointer(Vector2Int PointHere, OffscreenPointerType pointertype)
    {
      EventPointer eventPointer = new EventPointer(SpecialEventType.COunt, pointertype);
      eventPointer.SetTargetLocation(TileMath.GetTileToWorldSpace(PointHere));
      PointOffScreenManager.eventpointers.Add(eventPointer);
    }

    internal static void AddPointer(
      SpecialEventType pointertype,
      Player player,
      TutorialQuestSpecial _tutorialquestspecial)
    {
      for (int index = 0; index < PointOffScreenManager.eventpointers.Count; ++index)
      {
        if (PointOffScreenManager.eventpointers[index].tutorialquestspecial == _tutorialquestspecial)
          return;
      }
      EventPointer eventPointer = new EventPointer(pointertype, OffscreenPointerType.None, _tutorialquestspecial);
      eventPointer.SetTargetLocation(TileMath.GetTileToWorldSpace(new Vector2Int(152, 202)));
      PointOffScreenManager.eventpointers.Add(eventPointer);
    }

    public void UpdatePointOffScreenManager(Player player, float DeltaTime)
    {
      if (PointOffScreenManager.notificationstotrack != null)
      {
        this.Processotification(player);
        PointOffScreenManager.notificationstotrack.RemoveAt(0);
        if (PointOffScreenManager.notificationstotrack.Count == 0)
          PointOffScreenManager.notificationstotrack = (List<NotificationLists>) null;
      }
      bool flag = false;
      if (!OverWorldManager.zoopopupHolder.IsNull())
        flag = !OverWorldManager.zoopopupHolder.IsNull();
      if (OverWorldManager.overworldstate == OverWOrldState.CellSelect)
        flag = true;
      if (Z_GameFlags.HasCompleteQuestsToView && !flag)
        PointOffScreenManager.QuestPointer.bActive = true;
      else
        PointOffScreenManager.QuestPointer.bActive = false;
      PointOffScreenManager.QuestPointer.UpdateEventPointer(DeltaTime);
      for (int index = PointOffScreenManager.eventpointers.Count - 1; index > -1; --index)
      {
        if (PointOffScreenManager.eventpointers[index].UpdateEventPointer(DeltaTime))
          PointOffScreenManager.eventpointers.RemoveAt(index);
      }
    }

    public void DrawPointOffScreenManager()
    {
      if (PointOffScreenManager.eventpointers.Count > 0)
      {
        for (int index = 0; index < PointOffScreenManager.eventpointers.Count; ++index)
          PointOffScreenManager.eventpointers[index].DrawEventPointer();
      }
      if (SellUIManager.GetCurrentBarType() != BAR_TYPE.Count)
        return;
      PointOffScreenManager.QuestPointer.DrawEventPointer();
    }
  }
}

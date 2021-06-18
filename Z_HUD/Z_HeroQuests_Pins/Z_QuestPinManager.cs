// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_HeroQuests_Pins.Z_QuestPinManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Tile_Data;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_HeroQuests_Pins
{
  internal class Z_QuestPinManager
  {
    internal static bool DoubleCheckTaskNotifications;
    private List<QuestPinHolder> buubles;
    private Vector2 TopOfPins;
    private static HashSet<OffscreenPointerType> BuildShortcuts;
    private static HashSet<OffscreenPointerType> ShopTypeShortcuts;

    public Z_QuestPinManager()
    {
      this.buubles = new List<QuestPinHolder>();
      if (Z_QuestPinManager.BuildShortcuts == null)
      {
        Z_QuestPinManager.BuildShortcuts = new HashSet<OffscreenPointerType>();
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildABench);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildAFoodShop);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildAGiftShop);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildAnyShop);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildADrinksShop);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildABin);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildAnyDeco);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildABench);
        Z_QuestPinManager.BuildShortcuts.Add(OffscreenPointerType.BuildAPen);
        Z_QuestPinManager.ShopTypeShortcuts = new HashSet<OffscreenPointerType>();
        Z_QuestPinManager.ShopTypeShortcuts.Add(OffscreenPointerType.BuildAFoodShop);
        Z_QuestPinManager.ShopTypeShortcuts.Add(OffscreenPointerType.BuildAGiftShop);
        Z_QuestPinManager.ShopTypeShortcuts.Add(OffscreenPointerType.BuildAnyShop);
        Z_QuestPinManager.ShopTypeShortcuts.Add(OffscreenPointerType.BuildADrinksShop);
      }
      this.TopOfPins = new Vector2(0.0f, 150f);
    }

    public void PinRibbonTrackView(
      OffscreenPointerType offscreenpointer,
      Player player,
      bool IsFromRbbon = true)
    {
      bool flag = false;
      int num = -1;
      for (int index = this.buubles.Count - 1; index > -1; --index)
      {
        if (this.buubles[index].offscreenpointertype == offscreenpointer)
        {
          num = index;
          flag = true;
          break;
        }
      }
      if (Z_QuestPinManager.BuildShortcuts.Contains(offscreenpointer))
        FeatureFlags.FlashBuildFromNotificationTrack = true;
      if (Z_QuestPinManager.ShopTypeShortcuts.Contains(offscreenpointer))
        FeatureFlags.FlashBuildShop = true;
      if (offscreenpointer == OffscreenPointerType.ShopNeedsEmployee)
        FeatureFlags.FlashHireStaffFromShop = true;
      if (offscreenpointer == OffscreenPointerType.BuildAGiftShop)
        FeatureFlags.FlashBuildGiftShop = true;
      else if (offscreenpointer == OffscreenPointerType.BuildADrinksShop)
        FeatureFlags.FlashBuildDrinksShop = true;
      else if (offscreenpointer == OffscreenPointerType.BuildAPen)
        FeatureFlags.FlashBuildPen = true;
      else if (offscreenpointer == OffscreenPointerType.CripserBirth)
        FeatureFlags.FlashCRISPRFromBirth = true;
      else if (offscreenpointer == OffscreenPointerType.BuildAFoodShop)
        FeatureFlags.FlashBuildFoodShop = true;
      else if (offscreenpointer == OffscreenPointerType.BuildABench)
        FeatureFlags.FlashBuildBench = true;
      else if (offscreenpointer == OffscreenPointerType.BuildABin)
        FeatureFlags.FlashBuildBin = true;
      else if (offscreenpointer == OffscreenPointerType.JobApplicants)
        FeatureFlags.FlashHasApplicantsAtGate = true;
      for (int index = this.buubles.Count - 1; index > -1; --index)
      {
        if (this.buubles[index].IsFromRbbon && index != num)
        {
          PointOffScreenManager.RemovePointer(this.buubles[index].offscreenpointertype);
          this.buubles.RemoveAt(index);
        }
      }
      if (flag)
        return;
      this.buubles.Add(new QuestPinHolder("TRACK", EventPointer.GetOffscreenPointerTypeToPinString(offscreenpointer), this.buubles.Count, (HeroQuestDescription) null, player, true, offscreenpointer, IsFromRbbon));
    }

    public void PinQuest(HeroQuestDescription thisquest, Player player, bool AllowLerpAlways = false)
    {
      if (thisquest.herocharacter == HeroCharacter.Critical_Scientist)
      {
        FeatureFlags.FlashBuildFromTask = true;
        FeatureFlags.FlashResearchFromTask = true;
      }
      else if (thisquest.herocharacter == HeroCharacter.Complainer)
      {
        if (thisquest.UID == 0)
        {
          FeatureFlags.FlashBuildFromTask = true;
          FeatureFlags.FlashStoreRoomFromTask = true;
        }
        else if (thisquest.UID == 1)
        {
          FeatureFlags.FlashHireFromGateForQuest = true;
          if (!PointOffScreenManager.QuestPointerIsActive(OffscreenPointerType.HireFromGate))
            PointOffScreenManager.AddPointer(TileMath.GetGateLocationvector2Int() + new Vector2Int(2, -2), OffscreenPointerType.HireFromGate);
        }
      }
      if (thisquest.herocharacter == HeroCharacter.Critical_Geneticist && thisquest.UID == 0)
      {
        FeatureFlags.FlashBuildFromTask = true;
        FeatureFlags.FlashCRISPRFromTask = true;
      }
      this.buubles.Add(new QuestPinHolder("TASK", SEngine.Localization.Localization.GetText((int) thisquest.TaskShortSummary), this.buubles.Count, thisquest, player, AllowLerpAlways));
    }

    public void UnPinQuest(HeroQuestDescription thisquest, Player player)
    {
      for (int index = this.buubles.Count - 1; index > -1; --index)
      {
        if (this.buubles[index].thisquest != null && this.buubles[index].thisquest == thisquest)
        {
          this.buubles.RemoveAt(index);
          if (thisquest.herocharacter == HeroCharacter.Complainer || thisquest.UID == 1)
            Z_NotificationManager.RemoveThese(Z_NotificationType.F_AJobHasApplicants);
        }
      }
      this.CheckTasks(player);
    }

    public void UnPinQuest(OffscreenPointerType RemoveThis, Player player)
    {
      bool flag = false;
      for (int index = this.buubles.Count - 1; index > -1; --index)
      {
        if (this.buubles[index].offscreenpointertype == RemoveThis)
        {
          flag = true;
          this.buubles.RemoveAt(index);
          switch (RemoveThis)
          {
            case OffscreenPointerType.NewBirths:
              Z_NotificationManager.RemoveThese(Z_NotificationType.A_AnimalBirth);
              continue;
            case OffscreenPointerType.CripserBirth:
              Z_NotificationManager.RemoveThese(Z_NotificationType.A_CRISPR_HybridBirth);
              continue;
            case OffscreenPointerType.JobApplicants:
              Z_NotificationManager.RemoveThese(Z_NotificationType.F_AJobHasApplicants);
              continue;
            default:
              continue;
          }
        }
      }
      if (flag)
      {
        switch (RemoveThis)
        {
          case OffscreenPointerType.HungryAnimals:
            PointOffScreenManager.RemovePointer(OffscreenPointerType.HungryAnimals);
            break;
          case OffscreenPointerType.NoEnrichment:
            PointOffScreenManager.RemovePointer(OffscreenPointerType.NoEnrichment);
            break;
          case OffscreenPointerType.JobApplicants:
            FeatureFlags.FlashHasApplicantsAtGate = false;
            PointOffScreenManager.RemovePointer(OffscreenPointerType.JobApplicants);
            break;
        }
      }
      if (!Z_QuestPinManager.BuildShortcuts.Contains(RemoveThis))
        return;
      FeatureFlags.FlashHasApplicantsAtGate = false;
      FeatureFlags.FlashBuildFromNotificationTrack = false;
      FeatureFlags.FlashBuildShop = false;
      FeatureFlags.FlashBuildDrinksShop = false;
      FeatureFlags.FlashBuildGiftShop = false;
      FeatureFlags.FlashBuildFoodShop = false;
      FeatureFlags.FlashBuildBench = false;
      FeatureFlags.FlashBuildBin = false;
      FeatureFlags.FlashBuildPen = false;
      FeatureFlags.FlashHasApplicantsAtGate = false;
      FeatureFlags.FlashCRISPRFromBirth = false;
      for (int index = this.buubles.Count - 1; index > -1; --index)
      {
        if (Z_QuestPinManager.BuildShortcuts.Contains(this.buubles[index].offscreenpointertype))
          FeatureFlags.FlashBuildFromNotificationTrack = true;
        if (Z_QuestPinManager.ShopTypeShortcuts.Contains(this.buubles[index].offscreenpointertype))
          FeatureFlags.FlashBuildShop = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.CripserBirth)
          FeatureFlags.FlashCRISPRFromBirth = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.BuildAFoodShop)
          FeatureFlags.FlashBuildFoodShop = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.BuildADrinksShop)
          FeatureFlags.FlashBuildDrinksShop = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.BuildAGiftShop)
          FeatureFlags.FlashBuildGiftShop = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.BuildABench)
          FeatureFlags.FlashBuildBench = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.BuildABin)
          FeatureFlags.FlashBuildBin = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.BuildAPen)
          FeatureFlags.FlashBuildPen = true;
        if (this.buubles[index].offscreenpointertype == OffscreenPointerType.JobApplicants)
        {
          if (player.employees.openPositionsContainer.GetHasApplicantsAtGate())
            FeatureFlags.FlashHasApplicantsAtGate = true;
        }
        else if (this.buubles[index].offscreenpointertype == OffscreenPointerType.ShopNeedsEmployee)
          FeatureFlags.FlashHireStaffFromShop = true;
      }
    }

    public bool GetIsPinned(HeroQuestDescription thisquest)
    {
      for (int index = 0; index < this.buubles.Count; ++index)
      {
        if (this.buubles[index].thisquest != null && this.buubles[index].thisquest == thisquest)
          return true;
      }
      return false;
    }

    public bool CheckMouseOver(Player player)
    {
      float DrawHeight = 0.0f;
      for (int index = 0; index < this.buubles.Count; ++index)
      {
        if (this.buubles[index].UpdateQuestPinHolder(this.TopOfPins, player, 0.0f, index, ref DrawHeight))
          return true;
      }
      return false;
    }

    public void UpdateZ_QuestPinManager(Player player, float DeltaTime)
    {
      float DrawHeight = 0.0f;
      for (int index = 0; index < this.buubles.Count; ++index)
        this.buubles[index].UpdateQuestPinHolder(this.TopOfPins, player, DeltaTime, index, ref DrawHeight);
      if (!Z_QuestPinManager.DoubleCheckTaskNotifications)
        return;
      this.CheckTasks(player);
    }

    private void CheckTasks(Player player)
    {
      Z_QuestPinManager.DoubleCheckTaskNotifications = false;
      FeatureFlags.FlashBuildFromTask = false;
      FeatureFlags.FlashStoreRoomFromTask = false;
      FeatureFlags.FlashResearchFromTask = false;
      FeatureFlags.FlashCRISPRFromTask = false;
      FeatureFlags.FlashHasApplicantsAtGate = false;
      FeatureFlags.FlashHireFromGateForQuest = false;
      for (int index = this.buubles.Count - 1; index > -1; --index)
      {
        if (this.buubles[index].thisquest != null && !this.buubles[index].TaskIsComplete)
        {
          HeroQuestDescription thisquest = this.buubles[index].thisquest;
          if (thisquest.herocharacter == HeroCharacter.Critical_Scientist)
          {
            FeatureFlags.FlashBuildFromTask = true;
            FeatureFlags.FlashResearchFromTask = true;
          }
          if (thisquest.herocharacter == HeroCharacter.Complainer)
          {
            if (thisquest.UID == 0)
            {
              FeatureFlags.FlashBuildFromTask = true;
              FeatureFlags.FlashStoreRoomFromTask = true;
            }
            else if (thisquest.UID == 1)
              FeatureFlags.FlashHireFromGateForQuest = true;
          }
          if (thisquest.herocharacter == HeroCharacter.Critical_Geneticist && thisquest.UID == 0 && !player.shopstatus.HasThisFacility(TILETYPE.DNABuilding))
          {
            FeatureFlags.FlashBuildFromTask = true;
            FeatureFlags.FlashCRISPRFromTask = true;
          }
        }
        else if (this.buubles[index].IsFromRbbon && this.buubles[index].offscreenpointertype == OffscreenPointerType.JobApplicants)
        {
          if (player.employees.openPositionsContainer.GetHasApplicantsAtGate())
            FeatureFlags.FlashHasApplicantsAtGate = true;
          else
            PointOffScreenManager.RemovePointer(OffscreenPointerType.JobApplicants);
        }
      }
      if (FeatureFlags.FlashHireFromGateForQuest)
        return;
      PointOffScreenManager.RemovePointer(OffscreenPointerType.HireFromGate);
    }

    public void DrawZ_QuestPinManager()
    {
      float DrawHeight = 0.0f;
      for (int index = 0; index < this.buubles.Count; ++index)
        this.buubles[index].DrawQuestPinHolder(this.TopOfPins, ref DrawHeight);
    }
  }
}

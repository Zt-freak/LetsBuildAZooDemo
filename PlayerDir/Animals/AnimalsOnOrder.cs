// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Animals.AnimalsOnOrder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir.Animals
{
  internal class AnimalsOnOrder
  {
    public List<AnimalOrder> animalsonorder;
    public int HasThisManyOrdersToday;
    private int CurrentAnimalsInTransit;

    public AnimalsOnOrder() => this.animalsonorder = new List<AnimalOrder>();

    public bool HasOrderToThisPen(int PrisonUID)
    {
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (this.animalsonorder[index].PrisonUID == PrisonUID)
          return true;
      }
      return false;
    }

    public void AddNewOrder(WaveInfo AnimalsJustTraded, int PrisonUID, Player player)
    {
      FeatureFlags.FlashTrade = false;
      this.animalsonorder.Add(new AnimalOrder(AnimalsJustTraded, PrisonUID));
      OverWorldManager.overworldenvironment.wallsandfloors.SetUpAnimalsOnOrder(player, PrisonUID);
      Z_NotificationManager.ScrubEmptyPensForNewAnimals = true;
    }

    public int GetPrisonID(int OrderUID)
    {
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (this.animalsonorder[index].OrderUID == OrderUID)
          return this.animalsonorder[index].PrisonUID;
      }
      throw new Exception("Order not found");
    }

    public void ReomveOrderAfterChopprDropOff(int OrderUID, Player player)
    {
      int PrisonUID = -1;
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (this.animalsonorder[index].InTransit && this.animalsonorder[index].OrderUID == OrderUID)
        {
          int prisonUid = this.animalsonorder[index].PrisonUID;
          PrisonUID = this.animalsonorder[index].PrisonUID;
          this.animalsonorder.RemoveAt(index);
          OverWorldManager.overworldenvironment.wallsandfloors.SetUpAnimalsOnOrder(player, prisonUid);
          break;
        }
      }
      bool flag = false;
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (this.animalsonorder[index].OrderUID == OrderUID)
          flag = true;
      }
      if (flag || PrisonUID <= -1)
        return;
      Z_GameFlags.TryAndRemoveOnOrderSign(PrisonUID);
    }

    public void StartNewDay()
    {
      this.HasThisManyOrdersToday = 0;
      for (int index = 0; index < this.animalsonorder.Count; ++index)
      {
        if (this.animalsonorder[index].StartNewDay())
          ++this.HasThisManyOrdersToday;
      }
    }

    public void UpdateAnimalsOnOrder(Player player, OverWorldManager overworldmanager)
    {
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (!this.animalsonorder[index].InTransit && this.animalsonorder[index].DaysToArrival <= 0 && ((double) Z_GameFlags.DayTimer > (double) this.animalsonorder[index].SecondInDayOrArrival || !Z_GameFlags.HasStartedFirstDay) && (player.livestats.AnimalsJustTraded == null && !overworldmanager.IsCurrentlyMovingThisPen(this.animalsonorder[index].PrisonUID)))
        {
          ++this.CurrentAnimalsInTransit;
          this.animalsonorder[index].InTransit = true;
          OverWorldEnvironmentManager.airspacemanager.AddChinookDelivery(this.animalsonorder[index].GetAnimalsAsWaveInfo(), TileMath.GetTileToWorldSpace(player.prisonlayout.cellblockcontainer.GetThisCellBlock(this.animalsonorder[index].PrisonUID).GetSpaceBehindGate()), this.animalsonorder[index].OrderUID, this.animalsonorder[index].IsBlackMarket, PenUID: this.animalsonorder[index].PrisonUID);
        }
      }
      if (this.CurrentAnimalsInTransit <= 0)
        return;
      this.CurrentAnimalsInTransit = 0;
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (this.animalsonorder[index].InTransit)
        {
          ++this.CurrentAnimalsInTransit;
          if (!OverWorldEnvironmentManager.airspacemanager.ThisIsOnOrder(this.animalsonorder[index].OrderUID))
            OverWorldEnvironmentManager.airspacemanager.AddChinookDelivery(this.animalsonorder[index].GetAnimalsAsWaveInfo(), TileMath.GetTileToWorldSpace(player.prisonlayout.cellblockcontainer.GetThisCellBlock(this.animalsonorder[index].PrisonUID).GetSpaceBehindGate()), this.animalsonorder[index].OrderUID, this.animalsonorder[index].IsBlackMarket, PenUID: this.animalsonorder[index].PrisonUID);
        }
      }
    }

    public void CompletedDelivery(int OrderUID)
    {
      for (int index = this.animalsonorder.Count - 1; index > -1; --index)
      {
        if (this.animalsonorder[index].InTransit && this.animalsonorder[index].OrderUID == OrderUID)
          this.animalsonorder.RemoveAt(index);
      }
    }

    public AnimalsOnOrder(Reader reader, int VersionNumberForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("a", ref _out);
      this.animalsonorder = new List<AnimalOrder>();
      for (int index = 0; index < _out; ++index)
        this.animalsonorder.Add(new AnimalOrder(reader, VersionNumberForLoad));
    }

    public void SaveAnimalOrder(Writer writer)
    {
      writer.WriteInt("a", this.animalsonorder.Count);
      for (int index = 0; index < this.animalsonorder.Count; ++index)
        this.animalsonorder[index].SaveAnimalOrder(writer);
    }
  }
}

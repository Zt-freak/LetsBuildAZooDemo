// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CurrentEvent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir
{
  internal class CurrentEvent
  {
    public AnimalType Event_Enemytype;
    public bool EventActive;
    public bool NewEvent;
    private int CurrentEventIndex;
    public bool LastEventSuccess;
    public List<EVProgress> evprogress;

    public CurrentEvent()
    {
      this.CurrentEventIndex = -1;
      this.Event_Enemytype = AnimalType.None;
      this.evprogress = new List<EVProgress>();
    }

    public void CapturedThis(AnimalType enemytype)
    {
      if (this.evprogress.Count <= 0)
        return;
      this.evprogress[0].CapturedThis(enemytype);
    }

    public bool IsComplete_AndClaimed(Player player) => this.evprogress.Count > 0 && this.evprogress[0].GetClaimed();

    public bool CheckEvent(Player player)
    {
      if (this.evprogress.Count > 0 && this.evprogress[0].CurrentProgress >= this.evprogress[0].TargetValue && !this.evprogress[0].GetClaimed())
      {
        TinyZoo.Game1.gamestate = GAMESTATE.RewardSetUp;
        player.livestats.eventdone = true;
        player.livestats.eventenemy = this.Event_Enemytype;
        this.evprogress[0].SetClained();
        if (player.inventory.SecretAliensAvailable[(int) Inventory.GetThisEnemyToSecretAlien(this.Event_Enemytype)])
          player.livestats.EVWasMoney = true;
        player.inventory.SecretAliensAvailable[(int) Inventory.GetThisEnemyToSecretAlien(this.Event_Enemytype)] = true;
        return true;
      }
      bool WasServerTime;
      DateTime dateTimeNow = player.Stats.datetimemanager.GetDateTimeNow(out WasServerTime);
      if (WasServerTime)
      {
        this.EventActive = true;
        int num = (int) (new TimeSpan(dateTimeNow.Ticks).TotalDays / 7.0);
        if (this.CurrentEventIndex != num)
        {
          this.CurrentEventIndex = num;
          this.NewEvent = true;
          this.LastEventSuccess = this.Event_Enemytype == AnimalType.None || this.IsComplete_AndClaimed(player);
          this.evprogress = new List<EVProgress>();
          EVProgress evProgress = new EVProgress(AnimalType.None, 50, new DateTime(new TimeSpan((num + 1) * 7, 0, 0, 0).Ticks));
          switch ((num - 1) % 9)
          {
            case 0:
              this.Event_Enemytype = AnimalType.Bunsen;
              evProgress.enemytype = AnimalType.Walrus;
              evProgress.TargetValue = 20;
              break;
            case 1:
              this.Event_Enemytype = AnimalType.Tribble;
              break;
            case 2:
              this.Event_Enemytype = AnimalType.EmperorPorcupine;
              evProgress.enemytype = AnimalType.Duck;
              evProgress.TargetValue = 20;
              break;
            case 3:
              this.Event_Enemytype = AnimalType.Hal9000;
              evProgress.enemytype = AnimalType.Badger;
              evProgress.TargetValue = 20;
              break;
            case 4:
              this.Event_Enemytype = AnimalType.Leeloo;
              evProgress.enemytype = AnimalType.Rabbit;
              evProgress.TargetValue = 20;
              break;
            case 5:
              this.Event_Enemytype = AnimalType.Tardigrade;
              evProgress.enemytype = AnimalType.Ostrich;
              evProgress.TargetValue = 20;
              break;
            case 6:
              this.Event_Enemytype = AnimalType.Riddick;
              break;
            case 7:
              this.Event_Enemytype = AnimalType.Galaxian;
              break;
            case 8:
              this.Event_Enemytype = AnimalType.Bill;
              break;
          }
          if (dateTimeNow.Month == 12)
          {
            if (dateTimeNow.Day >= 28)
            {
              this.Event_Enemytype = AnimalType.Gremlin;
              evProgress.enemytype = AnimalType.None;
              evProgress.TargetValue = 50;
            }
            else if (dateTimeNow.Day >= 14 && dateTimeNow.Day < 22)
            {
              this.Event_Enemytype = AnimalType.Krampus;
              evProgress.enemytype = AnimalType.Rabbit;
              evProgress.TargetValue = 20;
            }
            else if (dateTimeNow.Day >= 21 && dateTimeNow.Day < 28)
            {
              this.Event_Enemytype = AnimalType.Grinch;
              evProgress.enemytype = AnimalType.Walrus;
              evProgress.TargetValue = 20;
            }
          }
          this.evprogress.Add(evProgress);
          player.OldSaveThisPlayer();
        }
      }
      return false;
    }

    public CurrentEvent(Reader reader)
    {
      this.EventActive = false;
      int num1 = (int) reader.ReadInt("e", ref this.CurrentEventIndex);
      int _out = 0;
      int num2 = (int) reader.ReadInt("e", ref _out);
      this.evprogress = new List<EVProgress>();
      for (int index = 0; index < _out; ++index)
        this.evprogress.Add(new EVProgress(reader));
      int num3 = (int) reader.ReadInt("e", ref _out);
      this.Event_Enemytype = (AnimalType) _out;
    }

    public void SaveCurrentEvent(Writer writer)
    {
      writer.WriteInt("e", this.CurrentEventIndex);
      writer.WriteInt("e", this.evprogress.Count);
      for (int index = 0; index < this.evprogress.Count; ++index)
        this.evprogress[index].SaveEVProgress(writer);
      writer.WriteInt("e", (int) this.Event_Enemytype);
    }
  }
}

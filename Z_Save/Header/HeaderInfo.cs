// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Save.Header.HeaderInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_Save.Header
{
  internal class HeaderInfo
  {
    public int SaveSlot;
    public string MoralityRating;
    public int DaysPlayed;
    public int CashHeld;
    public int LandUnlocked;
    public int ResearchDiscovered;
    public DateTime LastSave;
    public AnimalType ZooKeeper;
    public float PercentComplete;

    public HeaderInfo(int _SaveSlot)
    {
      this.SaveSlot = _SaveSlot;
      this.MoralityRating = "0";
    }

    public HeaderInfo(Reader reader)
    {
      int num1 = (int) reader.ReadInt("h", ref this.SaveSlot);
      int num2 = (int) reader.ReadString("h", ref this.MoralityRating);
      int num3 = (int) reader.ReadInt("h", ref this.DaysPlayed);
      int num4 = (int) reader.ReadInt("h", ref this.CashHeld);
      int num5 = (int) reader.ReadInt("h", ref this.LandUnlocked);
      int num6 = (int) reader.ReadInt("h", ref this.ResearchDiscovered);
      this.LastSave = reader.ReadDateTime("h");
      int num7 = (int) reader.ReadFloat("h", ref this.PercentComplete);
      int _out = 0;
      int num8 = (int) reader.ReadInt("h", ref _out);
      this.ZooKeeper = (AnimalType) _out;
    }

    public void UpdateHeaderInfoToMatchActive(Player player)
    {
      this.LastSave = DateTime.Now;
      this.DaysPlayed = (int) Player.financialrecords.GetDaysPassed();
      this.MoralityRating = MoralityData.GetDisplayStringForMoralityValue(player.livestats.MoralityScore);
    }

    public void SaveHeaderInfo(Writer writer)
    {
      writer.WriteInt("h", this.SaveSlot);
      writer.WriteString("h", this.MoralityRating);
      writer.WriteInt("h", this.DaysPlayed);
      writer.WriteInt("h", this.CashHeld);
      writer.WriteInt("h", this.LandUnlocked);
      writer.WriteInt("h", this.ResearchDiscovered);
      writer.WriteDateTime("h", this.LastSave);
      writer.WriteFloat("h", this.PercentComplete);
      writer.WriteInt("h", (int) this.ZooKeeper);
    }
  }
}

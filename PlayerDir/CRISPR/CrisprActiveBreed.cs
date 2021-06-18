// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CRISPR.CrisprActiveBreed
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BalanceSystems.Animals.CRISPR;
using TinyZoo.Z_Collection.Shared.Grid;

namespace TinyZoo.PlayerDir.CRISPR
{
  internal class CrisprActiveBreed
  {
    public AnimalType genomeOne;
    public AnimalType genomeTwo;
    public float DaysLeft;
    public bool IsBorn_CanCollect;
    public float DayStarted;
    public bool isBoy;
    public AnimalType resultBody;
    public AnimalType resultHead;
    public int resultBodyVariant;
    public int resultHeadVariant;
    public int percentChanceOfSuccess;
    public int UID;
    public bool TEMP_IsFirstDay;
    public float TEMP_TimeStarted;
    private ZooMoment TempLastZooMoment;
    private float totalDays = -1f;
    public float TimeLeft_Seconds;

    public CrisprActiveBreed(AnimalType _animalOne, AnimalType _animalTwo, int _UID)
    {
      this.genomeOne = _animalOne;
      this.genomeTwo = _animalTwo;
      this.TEMP_IsFirstDay = true;
      this.TEMP_TimeStarted = Z_GameFlags.DayTimer;
      this.DayStarted = (float) Player.financialrecords.GetDaysPassed();
      this.DaysLeft = CRISPRCalculator.GetDaysForThisCRISPRBreed(this.genomeOne, this.genomeTwo);
      this.isBoy = TinyZoo.Game1.Rnd.Next(0, 2) == 0;
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
      {
        this.resultBody = this.genomeOne;
        this.resultHead = this.genomeTwo;
      }
      else
      {
        this.resultBody = this.genomeTwo;
        this.resultHead = this.genomeOne;
      }
      this.resultBodyVariant = TinyZoo.Game1.Rnd.Next(0, 10);
      this.resultHeadVariant = TinyZoo.Game1.Rnd.Next(0, 10);
      this.UID = _UID;
    }

    public void StartNewDay(Player player)
    {
      if (this.IsBorn_CanCollect)
        return;
      --this.DaysLeft;
      this.DaysLeft = MathHelper.Clamp(this.DaysLeft, 0.0f, this.DaysLeft);
      if ((double) this.DaysLeft <= 0.0)
      {
        this.TempLastZooMoment = new ZooMoment(ZOOMOMENT.CRISPR_Birth, _UID: this.UID);
        LiveStats.AddEventToTheDay(this.TempLastZooMoment);
      }
      this.TEMP_IsFirstDay = false;
    }

    public AnimalRenderDescriptor DoBirth(
      Player player,
      ref bool WasNewVariant)
    {
      this.IsBorn_CanCollect = true;
      return new AnimalRenderDescriptor(this.resultBody, this.resultBodyVariant, this.resultHead, this.resultHeadVariant);
    }

    public float GetFloatPercentProgress()
    {
      if (this.IsBorn_CanCollect)
      {
        this.TimeLeft_Seconds = 0.0f;
        return 1f;
      }
      if ((double) this.totalDays == -1.0)
        this.totalDays = CRISPRCalculator.GetDaysForThisCRISPRBreed(this.genomeOne, this.genomeTwo);
      float num1 = 1f / this.totalDays;
      float num2 = num1 * 0.5f;
      float num3 = Z_GameFlags.DayTimer / Z_GameFlags.SecondsInDay;
      float num4;
      if ((double) this.DaysLeft == 0.0)
      {
        float num5 = (float) this.TempLastZooMoment.TimeOfDay / Z_GameFlags.SecondsInDay;
        if ((double) num3 >= (double) num5)
        {
          this.TimeLeft_Seconds = 0.0f;
          return 1f;
        }
        num4 = (float) (1.0 - (double) num2 * (1.0 - (double) num3 / (double) num5));
      }
      else if (this.TEMP_IsFirstDay)
      {
        float num5 = (float) (1.0 - (double) this.TEMP_TimeStarted / (double) Z_GameFlags.SecondsInDay);
        num4 = (float) (1.0 - (1.0 - (double) Z_GameFlags.DayTimer / (double) Z_GameFlags.SecondsInDay) / (double) num5) * num2;
      }
      else
        num4 = num3 * num1 + num1 * (this.totalDays - 1f - this.DaysLeft + num2);
      this.TimeLeft_Seconds = (1f - num4) * Z_GameFlags.SecondsInDay * this.totalDays;
      return num4;
    }

    public CrisprActiveBreed(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("c", ref _out);
      this.genomeOne = (AnimalType) _out;
      int num2 = (int) reader.ReadInt("c", ref _out);
      this.genomeTwo = (AnimalType) _out;
      int num3 = (int) reader.ReadFloat("c", ref this.DaysLeft);
      int num4 = (int) reader.ReadFloat("c", ref this.DayStarted);
      int num5 = (int) reader.ReadBool("c", ref this.isBoy);
      int num6 = (int) reader.ReadInt("c", ref _out);
      this.resultBody = (AnimalType) _out;
      int num7 = (int) reader.ReadInt("c", ref _out);
      this.resultHead = (AnimalType) _out;
      int num8 = (int) reader.ReadInt("c", ref this.resultBodyVariant);
      int num9 = (int) reader.ReadInt("c", ref this.resultHeadVariant);
      int num10 = (int) reader.ReadInt("c", ref this.UID);
    }

    public void SaveCrisprActiveBreed(Writer writer)
    {
      writer.WriteInt("c", (int) this.genomeOne);
      writer.WriteInt("c", (int) this.genomeTwo);
      writer.WriteFloat("c", this.DaysLeft);
      writer.WriteFloat("c", this.DayStarted);
      writer.WriteBool("c", this.isBoy);
      writer.WriteInt("c", (int) this.resultBody);
      writer.WriteInt("c", (int) this.resultHead);
      writer.WriteInt("c", this.resultBodyVariant);
      writer.WriteInt("c", this.resultHeadVariant);
      writer.WriteInt("c", this.UID);
    }
  }
}

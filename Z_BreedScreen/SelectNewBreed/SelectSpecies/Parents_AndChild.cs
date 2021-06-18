// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies.Parents_AndChild
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BreedScreen.ActiveBreedPair;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies
{
  internal class Parents_AndChild
  {
    public int FemaleParentVariant;
    public int MaleParentVariant;
    public int MaleUID;
    public int FemaleUID;
    public int PercentChanceOfThisChild;
    public int ChildVariant;
    public AnimalType animaltype;
    public int MaleCellBlockUD;
    public int FemaleCellBlockUID;
    public int Births;
    public int Attempts;
    public PrisonerInfo HeldBaby;
    public bool IsArticificalInsemination;
    public int NursingDays;
    public int NursingDaysOption;
    public int ProductionTargetOption;
    public bool MotherIsDead;
    public bool FatherDead;

    public Parents_AndChild(
      int _FemaleParentVariant,
      int _MaleParentVariant,
      int _FemaleUID,
      int _MaleUID,
      int _PercentChanceOfThisChild,
      AnimalType _animaltype,
      int _ChildVariant)
    {
      this.IsArticificalInsemination = PlayerStats.LastSetIsArticificalInsemination;
      this.ProductionTargetOption = PlayerStats.LastSetProductionTargetOption;
      this.NursingDaysOption = PlayerStats.LastSetNursingOption;
      this.ChildVariant = _ChildVariant;
      this.animaltype = _animaltype;
      this.FemaleParentVariant = _FemaleParentVariant;
      this.MaleParentVariant = _MaleParentVariant;
      this.MaleUID = _MaleUID;
      this.FemaleUID = _FemaleUID;
      this.PercentChanceOfThisChild = _PercentChanceOfThisChild;
    }

    public void RemovedBaby_SetMarality(Player player)
    {
      if (this.NursingDays < 3)
      {
        ++player.Stats.BabiesMovedEarly;
        MoralityCalculator.CalculateMorality(player);
      }
      else
      {
        if (this.NursingDays <= 12)
          return;
        ++player.Stats.BabiesMovedLate;
        MoralityCalculator.CalculateMorality(player);
      }
    }

    public bool GetIsActive(Player player)
    {
      int productionCapacityToValue = TimeWithParents.GetProductionCapacityToValue((ProductionCapacity) this.ProductionTargetOption);
      return this.HeldBaby != null || productionCapacityToValue != 0 && !this.MotherIsDead && (!this.FatherDead || player.breeds.IsThisMotherPregnant(this.FemaleUID) && player.prisonlayout.GetThisNotInPenAnimal(this.FemaleUID).IsFertile && player.prisonlayout.GetThisNotInPenAnimal(this.MaleUID).IsFertile);
    }

    public int GiveBirth(ActiveBreed breed, Player player)
    {
      this.HeldBaby = new PrisonerInfo(new IntakePerson(breed.animalType, _IsAGirl: breed.boy, Variant: breed.ChildType), false, Vector2.Zero, CellBlockType.CorruptedGrass);
      this.HeldBaby.FatherUID = this.MaleUID;
      this.HeldBaby.MotherUID = this.FemaleUID;
      ++this.Births;
      player.prisonlayout.AddAnimalNotInPen(this.HeldBaby);
      if (this.ProductionTargetOption != 11)
      {
        --this.ProductionTargetOption;
        if (this.ProductionTargetOption < 0)
          this.ProductionTargetOption = 0;
      }
      this.NursingDays = 0;
      return this.HeldBaby.intakeperson.UID;
    }

    public int GetChildFromThisCouple()
    {
      int OffspringPercent;
      int MaleDupliacePercent;
      this.GetPercentages(out OffspringPercent, out MaleDupliacePercent, out int _);
      int num = TinyZoo.Game1.Rnd.Next(0, 100);
      if (num < OffspringPercent && OffspringPercent > -1)
        return this.ChildVariant;
      return MaleDupliacePercent > -1 && (OffspringPercent > -1 && num < MaleDupliacePercent + OffspringPercent || OffspringPercent == -1 && num < MaleDupliacePercent) ? this.MaleParentVariant : this.FemaleParentVariant;
    }

    public bool KilledAnAnimal(int UID)
    {
      if (this.MaleUID == UID)
      {
        this.FatherDead = true;
        return true;
      }
      if (this.FemaleUID != UID)
        return false;
      this.MotherIsDead = true;
      return true;
    }

    public bool MoveBabyToPen(Player player, int BabyUID)
    {
      if (this.HeldBaby == null || this.HeldBaby.intakeperson.UID != BabyUID)
        return false;
      int PrisonUID = player.prisonlayout.GetThisCellBlock(this.FemaleCellBlockUID) == null ? (player.prisonlayout.GetThisCellBlock(this.MaleCellBlockUD) == null ? player.prisonlayout.FindCellBlockWIthThisAnimal(this.animaltype) : this.MaleCellBlockUD) : this.FemaleCellBlockUID;
      if (PrisonUID > -1)
      {
        player.prisonlayout.GetThisCellBlock(PrisonUID).AddAnimalFromBreedingRoom(this.HeldBaby);
        player.prisonlayout.RemoveThisNotInPenAnimal(this.HeldBaby.intakeperson.UID);
        this.HeldBaby = (PrisonerInfo) null;
      }
      return true;
    }

    public bool AbortBaby(ref List<ActiveBreed> breeds)
    {
      for (int index = breeds.Count - 1; index > -1; --index)
      {
        if (breeds[index].MotherUID == this.FemaleUID)
        {
          breeds.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public void GetPercentages(
      out int OffspringPercent,
      out int MaleDupliacePercent,
      out int FemaleDuplicatePercent)
    {
      OffspringPercent = 0;
      MaleDupliacePercent = 0;
      FemaleDuplicatePercent = 0;
      bool flag1 = this.FemaleParentVariant != this.MaleParentVariant;
      bool flag2 = this.ChildVariant != this.FemaleParentVariant && this.ChildVariant != this.MaleParentVariant;
      if (flag1 & flag2)
      {
        int num = (100 - this.PercentChanceOfThisChild) / 2;
        OffspringPercent = this.PercentChanceOfThisChild;
        if (num * 2 + OffspringPercent != 100)
          ++OffspringPercent;
        MaleDupliacePercent = num;
        FemaleDuplicatePercent = num;
      }
      else if (flag1 && !flag2)
      {
        OffspringPercent = -1;
        MaleDupliacePercent = 50;
        FemaleDuplicatePercent = 50;
      }
      else if (!flag1 & flag2)
      {
        OffspringPercent = this.PercentChanceOfThisChild;
        FemaleDuplicatePercent = 100 - this.PercentChanceOfThisChild;
        MaleDupliacePercent = -1;
      }
      else
      {
        if (flag1 || flag2)
          throw new Exception("THIS SEEMS IMPOSSIBLE - you explored every option didnt you?");
        FemaleDuplicatePercent = 100;
        MaleDupliacePercent = -1;
        OffspringPercent = -1;
      }
    }

    public Parents_AndChild(Reader reader, int VersionNumberForLoad)
    {
      int num1 = (int) reader.ReadInt("p", ref this.FemaleParentVariant);
      int num2 = (int) reader.ReadInt("p", ref this.MaleParentVariant);
      int num3 = (int) reader.ReadInt("p", ref this.MaleUID);
      int num4 = (int) reader.ReadInt("p", ref this.FemaleUID);
      int num5 = (int) reader.ReadInt("p", ref this.PercentChanceOfThisChild);
      int num6 = (int) reader.ReadInt("p", ref this.ChildVariant);
      int _out1 = 0;
      int num7 = (int) reader.ReadInt("p", ref _out1);
      this.animaltype = (AnimalType) _out1;
      int num8 = (int) reader.ReadInt("p", ref this.MaleCellBlockUD);
      int num9 = (int) reader.ReadInt("p", ref this.FemaleCellBlockUID);
      int num10 = (int) reader.ReadInt("p", ref this.Births);
      int num11 = (int) reader.ReadInt("p", ref this.Attempts);
      int num12 = (int) reader.ReadInt("p", ref this.NursingDays);
      int num13 = (int) reader.ReadInt("p", ref this.NursingDaysOption);
      int num14 = (int) reader.ReadInt("p", ref this.ProductionTargetOption);
      bool _out2 = false;
      int num15 = (int) reader.ReadBool("p", ref _out2);
      if (_out2)
        this.HeldBaby = new PrisonerInfo(reader, VersionNumberForLoad);
      int num16 = (int) reader.ReadBool("p", ref this.IsArticificalInsemination);
      int num17 = (int) reader.ReadBool("p", ref this.MotherIsDead);
      int num18 = (int) reader.ReadBool("p", ref this.FatherDead);
    }

    public void SaveParents_AndChild(Writer writer)
    {
      writer.WriteInt("p", this.FemaleParentVariant);
      writer.WriteInt("p", this.MaleParentVariant);
      writer.WriteInt("p", this.MaleUID);
      writer.WriteInt("p", this.FemaleUID);
      writer.WriteInt("p", this.PercentChanceOfThisChild);
      writer.WriteInt("p", this.ChildVariant);
      writer.WriteInt("p", (int) this.animaltype);
      writer.WriteInt("p", this.MaleCellBlockUD);
      writer.WriteInt("p", this.FemaleCellBlockUID);
      writer.WriteInt("p", this.Births);
      writer.WriteInt("p", this.Attempts);
      writer.WriteInt("p", this.NursingDays);
      writer.WriteInt("p", this.NursingDaysOption);
      writer.WriteInt("p", this.ProductionTargetOption);
      writer.WriteBool("p", this.HeldBaby != null);
      if (this.HeldBaby != null)
        this.HeldBaby.SavePrisonerInfo(writer);
      writer.WriteBool("p", this.IsArticificalInsemination);
      writer.WriteBool("p", this.MotherIsDead);
      writer.WriteBool("p", this.FatherDead);
    }
  }
}

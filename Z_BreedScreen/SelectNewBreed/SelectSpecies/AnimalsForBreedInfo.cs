// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies.AnimalsForBreedInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Breeding;
using TinyZoo.Z_Player;

namespace TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies
{
  internal class AnimalsForBreedInfo
  {
    private List<PrisonerInfo> prisoners;
    private List<int> CellBlockArrayIndexes;
    public int[] Males_UID;
    public int[] Females_UID;
    public AnimalType animaltype;
    public Parents_AndChild[] PossibleChildVariants;
    public bool[] IsNew;

    public AnimalsForBreedInfo()
    {
      this.prisoners = new List<PrisonerInfo>();
      this.CellBlockArrayIndexes = new List<int>();
      this.Males_UID = new int[10];
      this.Females_UID = new int[10];
      this.PossibleChildVariants = new Parents_AndChild[10];
      this.IsNew = new bool[10];
      for (int index = 0; index < this.Females_UID.Length; ++index)
      {
        this.Females_UID[index] = -1;
        this.Males_UID[index] = -1;
      }
    }

    public void AddThis(PrisonerInfo thisprisoner, int CellBlockArrayIndex)
    {
      this.animaltype = thisprisoner.intakeperson.animaltype;
      if (thisprisoner.intakeperson.IsAGirl)
        this.Females_UID[thisprisoner.intakeperson.CLIndex] = thisprisoner.intakeperson.UID;
      else
        this.Males_UID[thisprisoner.intakeperson.CLIndex] = thisprisoner.intakeperson.UID;
      this.prisoners.Add(thisprisoner);
      this.CellBlockArrayIndexes.Add(CellBlockArrayIndex);
    }

    public bool HasBreedingPair()
    {
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < this.prisoners.Count; ++index)
      {
        if (this.prisoners[index].intakeperson.IsAGirl)
          flag2 = true;
        else
          flag1 = true;
      }
      return flag2 & flag1;
    }

    public int HasNewOpportunity(Player player)
    {
      int num = 0;
      List<BreedEntry> breedentries = BreedData.breedinfo[(int) this.animaltype].breedentries;
      for (int index = 0; index < breedentries.Count - 1; ++index)
      {
        if (breedentries[index].Parent1_girl != -1 && breedentries[index].Parent2 != -1)
        {
          bool flag = false;
          if (Z_DebugFlags.AllowMaleAndFemalBreedingPairs && this.Females_UID[breedentries[index].Parent2] > -1 && (this.Males_UID[breedentries[index].Parent1_girl] > -1 && this.PossibleChildVariants[index] == null))
          {
            flag = true;
            this.PossibleChildVariants[index] = new Parents_AndChild(breedentries[index].Parent2, breedentries[index].Parent1_girl, this.Females_UID[breedentries[index].Parent2], this.Males_UID[breedentries[index].Parent1_girl], breedentries[index].PercentChanceOfThisChild, this.animaltype, index);
          }
          if (this.Females_UID[breedentries[index].Parent1_girl] > -1 && this.Males_UID[breedentries[index].Parent2] > -1 && this.PossibleChildVariants[index] == null)
          {
            flag = true;
            this.PossibleChildVariants[index] = new Parents_AndChild(breedentries[index].Parent1_girl, breedentries[index].Parent2, this.Females_UID[breedentries[index].Parent1_girl], this.Males_UID[breedentries[index].Parent2], breedentries[index].PercentChanceOfThisChild, this.animaltype, index);
          }
          if (flag && !player.animalcollection.HasThisVariant(this.animaltype, index, VariantSex.Any))
          {
            this.IsNew[index] = true;
            ++num;
          }
        }
      }
      return num;
    }
  }
}

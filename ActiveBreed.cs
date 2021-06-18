// Decompiled with JetBrains decompiler
// Type: TinyZoo.ActiveBreed
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Time;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;

namespace TinyZoo
{
  internal class ActiveBreed
  {
    public int MaleParentVARIANT;
    public int FemaleParentVARIANT;
    public TimeToResult researchtimeleft;
    public int DaysLeft;
    public int startingdays;
    public AnimalType animalType;
    public bool boy;
    public int ChildType;
    public int MotherUID;
    public int FatherUID;
    public bool ISActive;

    public ActiveBreed()
    {
      this.researchtimeleft = new TimeToResult(new DateTime(), DateTime.UtcNow);
      this.ISActive = false;
    }

    public ActiveBreed(Parents_AndChild Ref_ParentsAndChild)
    {
      this.researchtimeleft = new TimeToResult(new DateTime(), DateTime.UtcNow);
      this.ISActive = true;
      this.FatherUID = Ref_ParentsAndChild.MaleUID;
      this.MotherUID = Ref_ParentsAndChild.FemaleUID;
      this.boy = Game1.Rnd.Next(0, 2) == 0;
      this.MaleParentVARIANT = Ref_ParentsAndChild.MaleParentVariant;
      this.FemaleParentVARIANT = Ref_ParentsAndChild.FemaleParentVariant;
      this.ChildType = Ref_ParentsAndChild.GetChildFromThisCouple();
      this.animalType = Ref_ParentsAndChild.animaltype;
      this.DaysLeft = ActiveBreed.GetDaysForpregnancy(Ref_ParentsAndChild.animaltype);
      TimeSpan timeSpan = new TimeSpan(0, 0, 30);
    }

    public void StartNewDay(bool IsInBreedingRoom)
    {
      if (this.DaysLeft > 0)
        --this.DaysLeft;
      if (!this.ISActive || this.DaysLeft > 0)
        return;
      LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.Birth));
    }

    public bool Claim(Player player)
    {
      if (this.ISActive)
      {
        if (DebugFlags.IsPCVersion)
        {
          if (this.DaysLeft <= 0)
          {
            this.ISActive = false;
            return true;
          }
        }
        else if (this.researchtimeleft.IsComplete(player.Stats.datetimemanager, out bool _, false))
        {
          this.researchtimeleft.ClaimReward();
          this.ISActive = false;
          return true;
        }
      }
      return false;
    }

    public ActiveBreed(
      AnimalType _Fauna,
      int _ChildType,
      Player player,
      int _MaleParent,
      int _FemaleParent,
      bool _boy,
      int _MotherUID,
      int _FatherUID)
    {
      this.FatherUID = _FatherUID;
      this.MotherUID = _MotherUID;
      this.boy = _boy;
      this.MaleParentVARIANT = _MaleParent;
      this.FemaleParentVARIANT = _FemaleParent;
      this.ISActive = true;
      this.ChildType = _ChildType;
      this.animalType = _Fauna;
      TimeSpan timeSpan = new TimeSpan();
      this.DaysLeft = ActiveBreed.GetDaysForpregnancy(_Fauna);
      switch (_Fauna)
      {
        case AnimalType.Rabbit:
          timeSpan = new TimeSpan(0, 0, 30);
          break;
        case AnimalType.Goose:
          timeSpan = new TimeSpan(0, 1, 0);
          break;
        case AnimalType.Capybara:
          timeSpan = new TimeSpan(0, 2, 0);
          break;
        case AnimalType.Pig:
          timeSpan = new TimeSpan(0, 3, 0);
          break;
        default:
          timeSpan = new TimeSpan(0, 5, 0);
          break;
      }
      DateTime _LengthOfTimeToObjective = new DateTime(timeSpan.Ticks);
      this.researchtimeleft = new TimeToResult(player.Stats.datetimemanager.GetDateTimeNow(out bool _), _LengthOfTimeToObjective);
    }

    internal static int GetDaysForpregnancy(AnimalType _Fauna)
    {
      int num = 1;
      switch (_Fauna)
      {
        case AnimalType.Capybara:
        case AnimalType.Pig:
        case AnimalType.Duck:
        case AnimalType.Snake:
        case AnimalType.Badger:
        case AnimalType.Hyena:
        case AnimalType.Porcupine:
          num = 2;
          break;
        case AnimalType.Bear:
        case AnimalType.Meerkat:
        case AnimalType.Horse:
        case AnimalType.Armadillo:
        case AnimalType.Donkey:
        case AnimalType.Cow:
        case AnimalType.Tapir:
        case AnimalType.Ostrich:
        case AnimalType.Tortoise:
        case AnimalType.Chicken:
          num = 3;
          break;
        case AnimalType.Camel:
        case AnimalType.Penguin:
        case AnimalType.Antelope:
        case AnimalType.Panther:
        case AnimalType.Seal:
        case AnimalType.Wolf:
        case AnimalType.Lemur:
        case AnimalType.Alpaca:
          num = 4;
          break;
        case AnimalType.KomodoDragon:
        case AnimalType.Walrus:
        case AnimalType.Orangutan:
        case AnimalType.PolarBear:
        case AnimalType.Skunk:
        case AnimalType.Crocodile:
        case AnimalType.WildBoar:
        case AnimalType.Peacock:
        case AnimalType.Platypus:
        case AnimalType.Deer:
        case AnimalType.Monkey:
          num = 5;
          break;
        case AnimalType.Flamingo:
        case AnimalType.Gorilla:
        case AnimalType.Tiger:
        case AnimalType.Kangaroo:
        case AnimalType.Beavers:
        case AnimalType.RedPanda:
        case AnimalType.Zebra:
        case AnimalType.Fox:
        case AnimalType.Raccoon:
          num = 6;
          break;
        case AnimalType.Elephant:
        case AnimalType.Cheetah:
        case AnimalType.Otter:
        case AnimalType.Owl:
        case AnimalType.Rhino:
          num = 7;
          break;
        case AnimalType.Panda:
        case AnimalType.Giraffe:
        case AnimalType.Hippopotamus:
        case AnimalType.Lion:
        case AnimalType.None:
          num = 8;
          break;
      }
      return num;
    }

    public ActiveBreed(Reader reader)
    {
      int num1 = (int) reader.ReadInt("b", ref this.MaleParentVARIANT);
      int num2 = (int) reader.ReadInt("b", ref this.FemaleParentVARIANT);
      int num3 = (int) reader.ReadInt("b", ref this.DaysLeft);
      int num4 = (int) reader.ReadInt("b", ref this.ChildType);
      int num5 = (int) reader.ReadBool("b", ref this.boy);
      int num6 = (int) reader.ReadBool("b", ref this.ISActive);
      int _out = 0;
      int num7 = (int) reader.ReadInt("b", ref _out);
      this.animalType = (AnimalType) _out;
      int num8 = (int) reader.ReadInt("b", ref this.MotherUID);
      int num9 = (int) reader.ReadInt("b", ref this.FatherUID);
    }

    public void SaveActiveBreed(Writer writer)
    {
      writer.WriteInt("b", this.MaleParentVARIANT);
      writer.WriteInt("b", this.FemaleParentVARIANT);
      writer.WriteInt("b", this.DaysLeft);
      writer.WriteInt("b", this.ChildType);
      writer.WriteBool("b", this.boy);
      writer.WriteBool("b", this.ISActive);
      writer.WriteInt("b", (int) this.animalType);
      writer.WriteInt("b", this.MotherUID);
      writer.WriteInt("b", this.FatherUID);
    }
  }
}

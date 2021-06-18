// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Incinerator.DeadAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.PlayerDir.Incinerator
{
  internal class DeadAnimal
  {
    public AnimalType animalType = AnimalType.None;
    public AnimalType headType = AnimalType.None;
    public int variant;
    public int headVariant;
    public AnimalAgeType ageType;
    public float weight;
    public int NameIndex;
    public bool IsAGirl;
    public float daysDead;
    private string Name;
    public CROPTYPE cropType = CROPTYPE.Count;
    private bool IsACrop;

    public DeadAnimal(PrisonerInfo prisonerInfo)
    {
      this.animalType = prisonerInfo.intakeperson.animaltype;
      this.headType = prisonerInfo.intakeperson.HeadType;
      this.variant = prisonerInfo.intakeperson.CLIndex;
      this.headVariant = prisonerInfo.intakeperson.HeadVariant;
      this.ageType = !prisonerInfo.GetIsABaby() ? (!prisonerInfo.IsFertile ? AnimalAgeType.Geriatic_MeansOld : AnimalAgeType.Adult) : AnimalAgeType.Baby;
      this.NameIndex = prisonerInfo.intakeperson.NameIndex;
      this.IsAGirl = prisonerInfo.intakeperson.IsAGirl;
      this.daysDead = (float) prisonerInfo.DaysSinceDeath;
      this.weight = prisonerInfo.GetCurrentWeightInKG();
      this.cropType = CROPTYPE.Count;
    }

    public DeadAnimal()
    {
    }

    public DeadAnimal(CROPTYPE _cropType)
    {
      this.cropType = _cropType;
      this.IsACrop = true;
    }

    public string GetName()
    {
      if (string.IsNullOrEmpty(this.Name))
        this.Name = PeopleNames.GetName(!this.IsAGirl, out int _, this.animalType, this.NameIndex);
      return this.Name;
    }

    public void SaveDeadAnimal(Writer writer)
    {
      this.IsACrop = this.cropType != CROPTYPE.Count;
      writer.WriteBool("d", this.IsACrop);
      if (this.IsACrop)
      {
        writer.WriteInt("d", (int) this.cropType);
      }
      else
      {
        writer.WriteInt("d", (int) this.animalType);
        writer.WriteInt("d", (int) this.headType);
        writer.WriteInt("d", this.variant);
        writer.WriteInt("d", this.headVariant);
        writer.WriteInt("d", (int) this.ageType);
      }
      writer.WriteFloat("d", this.weight);
      if (this.IsACrop)
        return;
      writer.WriteInt("d", this.NameIndex);
      writer.WriteBool("d", this.IsAGirl);
      writer.WriteFloat("d", this.daysDead);
    }

    public DeadAnimal(Reader reader, int VersionForLoad)
    {
      int _out = 0;
      if (VersionForLoad > 14)
      {
        int num1 = (int) reader.ReadBool("d", ref this.IsACrop);
      }
      if (this.IsACrop)
      {
        int num2 = (int) reader.ReadInt("d", ref _out);
        this.cropType = (CROPTYPE) _out;
      }
      else
      {
        int num2 = (int) reader.ReadInt("d", ref _out);
        this.animalType = (AnimalType) _out;
        int num3 = (int) reader.ReadInt("d", ref _out);
        this.headType = (AnimalType) _out;
        int num4 = (int) reader.ReadInt("d", ref this.variant);
        int num5 = (int) reader.ReadInt("d", ref this.headVariant);
        int num6 = (int) reader.ReadInt("d", ref _out);
        this.ageType = (AnimalAgeType) _out;
      }
      int num7 = (int) reader.ReadFloat("d", ref this.weight);
      if (this.IsACrop)
        return;
      int num8 = (int) reader.ReadInt("d", ref this.NameIndex);
      int num9 = (int) reader.ReadBool("d", ref this.IsAGirl);
      int num10 = (int) reader.ReadFloat("d", ref this.daysDead);
    }
  }
}

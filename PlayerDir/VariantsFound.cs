// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.VariantsFound
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BalanceSystems.CustomerStats;

namespace TinyZoo.PlayerDir
{
  internal class VariantsFound
  {
    private VariantSet[] variantsets;

    public VariantsFound()
    {
      this.variantsets = new VariantSet[70];
      for (int index = 0; index < this.variantsets.Length; ++index)
        this.variantsets[index] = new VariantSet();
    }

    private void Create()
    {
      this.variantsets = new VariantSet[70];
      for (int index = 0; index < this.variantsets.Length; ++index)
        this.variantsets[index] = new VariantSet();
    }

    public bool AnimalBredOrFound(AnimalType animal, int Skin)
    {
      CalculateStat.RebuildAnimalMap = true;
      return this.variantsets[(int) animal].FoundVariant(Skin);
    }

    public int GetTotalOfThisVariantFound(AnimalType animal, int VarientIndex) => this.variantsets[(int) animal].GetTotalOfThisVariantFound(VarientIndex);

    public int GetTotalVaiantsFound(AnimalType animal = AnimalType.None)
    {
      if (animal != AnimalType.None)
        return this.variantsets[(int) animal].GetTotalVaiantsFound();
      int num = 0;
      for (int index = 0; index < this.variantsets.Length; ++index)
        num += this.variantsets[index].GetTotalVaiantsFound();
      return num;
    }

    public int GetDayDiscovered(AnimalType animal, int Variant = 0) => this.variantsets[(int) animal].GetDayDiscovered(Variant);

    public bool IsThisGenomeMapped(AnimalType animal) => this.variantsets[(int) animal].IsThisGenomeMapped();

    public void ForceUnlockGenome(AnimalType animal) => this.variantsets[(int) animal].ForceUnlockGenome();

    public VariantsFound(Reader reader, int VersionNumberForLoad)
    {
      this.Create();
      int _out = 0;
      int num = (int) reader.ReadInt("v", ref _out);
      for (int index = 0; index < _out; ++index)
        this.variantsets[index].LoadVariantSet(reader, VersionNumberForLoad);
    }

    public void SaveVariantsfound(Writer writer)
    {
      writer.WriteInt("v", this.variantsets.Length);
      for (int index = 0; index < this.variantsets.Length; ++index)
        this.variantsets[index].SaveVariantSet(writer);
    }
  }
}

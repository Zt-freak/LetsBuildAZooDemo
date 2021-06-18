// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.TradeInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Objects;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_Quests
{
  internal class TradeInfo
  {
    public AnimalType animal;
    public int VariantIndex;
    public NumberObfiscatorV1 Total;

    public TradeInfo(AnimalType animaltype, int GetThisMany, int Index)
    {
      this.animal = animaltype;
      this.Total = new NumberObfiscatorV1();
      this.Total.ForceSetNewValue(GetThisMany);
      this.VariantIndex = Index;
    }

    public TradeInfo() => this.Total = new NumberObfiscatorV1();
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Customers.NewCustomers.VIP_Entry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;

namespace TinyZoo.Z_BalanceSystems.Customers.NewCustomers
{
  internal class VIP_Entry
  {
    public AnimalType animaltype;
    public CustomerType customertype;
    public string ForcedName;

    public VIP_Entry(AnimalType _animaltype, CustomerType _customertype, string _ForcedName = "")
    {
      this.animaltype = _animaltype;
      this.customertype = _customertype;
      this.ForcedName = _ForcedName;
    }
  }
}

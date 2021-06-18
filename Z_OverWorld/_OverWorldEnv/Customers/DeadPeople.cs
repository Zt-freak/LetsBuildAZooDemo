// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.DeadPeople
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers
{
  internal class DeadPeople
  {
    private List<DeadPersonRenderer> deadpersonrenderers;

    public DeadPeople() => this.deadpersonrenderers = new List<DeadPersonRenderer>();

    public void AddDeadPerson(WalkingPerson walking) => this.deadpersonrenderers.Add(new DeadPersonRenderer(walking.vLocation));

    public void DrawDeadPeople()
    {
      for (int index = 0; index < this.deadpersonrenderers.Count; ++index)
        this.deadpersonrenderers[index].DrawDeadPersonRenderer(AssetContainer.pointspritebatch01);
    }
  }
}

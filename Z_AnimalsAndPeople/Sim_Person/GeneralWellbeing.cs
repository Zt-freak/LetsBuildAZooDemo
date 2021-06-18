// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.GeneralWellbeing
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class GeneralWellbeing
  {
    public int Thriftiness;

    public GeneralWellbeing()
    {
      int num = Game1.Rnd.Next(0, 10);
      if (num < 5)
      {
        this.Thriftiness = Game1.Rnd.Next(45, 55);
      }
      else
      {
        switch (num)
        {
          case 5:
            this.Thriftiness = Game1.Rnd.Next(35, 60);
            break;
          case 6:
            this.Thriftiness = Game1.Rnd.Next(25, 70);
            break;
          case 7:
            this.Thriftiness = Game1.Rnd.Next(15, 75);
            break;
          case 8:
            this.Thriftiness = Game1.Rnd.Next(5, 85);
            break;
          default:
            this.Thriftiness = Game1.Rnd.Next(0, 100);
            break;
        }
      }
    }
  }
}

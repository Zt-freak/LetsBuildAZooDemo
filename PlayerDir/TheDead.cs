// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.TheDead
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_AnimalsAndPeople;

namespace TinyZoo.PlayerDir
{
  internal class TheDead
  {
    public int UID;
    public CauseOfDeath causeofdeath;

    public TheDead(int _UID, CauseOfDeath _causeofdeath)
    {
      this.causeofdeath = _causeofdeath;
      this.UID = _UID;
    }
  }
}

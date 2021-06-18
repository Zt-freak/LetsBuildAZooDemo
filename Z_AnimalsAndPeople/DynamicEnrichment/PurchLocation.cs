// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.PurchLocation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class PurchLocation
  {
    public Vector2 Location;
    public bool DrawBehind;

    public PurchLocation(Vector2 _Location, bool _DrawBehind = false)
    {
      this.Location = _Location;
      this.DrawBehind = _DrawBehind;
    }
  }
}

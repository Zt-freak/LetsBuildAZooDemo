// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.beams.IntersectionPoint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.GamePlay.beams
{
  internal class IntersectionPoint
  {
    public Vector2 ChildLocation;
    public Vector2 IntersectionPointLocation;

    public IntersectionPoint(Vector2 _ChildLocation, Vector2 _IntersectionPoint)
    {
      this.ChildLocation = _ChildLocation;
      this.IntersectionPointLocation = _IntersectionPoint;
    }
  }
}

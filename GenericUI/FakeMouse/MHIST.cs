// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.FakeMouse.MHIST
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GenericUI.FakeMouse
{
  internal class MHIST
  {
    public Vector2 LocationInWorldSpace;
    public float Alpha;

    public MHIST(Vector2 Loc)
    {
      this.LocationInWorldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(Loc);
      this.Alpha = 1f;
    }
  }
}

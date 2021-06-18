// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.DecoManagers.BaseDecoObject
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.PenDecoEnrich.DecoManagers
{
  internal class BaseDecoObject
  {
    public virtual bool UpdateBaseDecoObject(float DeltaTime) => false;

    public virtual void DrawBaseDecoObject(Vector2 Location)
    {
    }
  }
}

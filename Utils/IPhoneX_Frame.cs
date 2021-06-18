// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.IPhoneX_Frame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Utils
{
  internal class IPhoneX_Frame : GameObject
  {
    private Vector2 VSCale;

    public IPhoneX_Frame()
    {
      this.DrawRect = new Rectangle(0, 0, 2437, 1127);
      this.VSCale.X = 768f / (float) this.DrawRect.Width;
      this.VSCale.Y = 1024f / (float) this.DrawRect.Height;
    }

    public void DrawIPhoneX_Frame()
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.DragBuilder.RightMouseDrag
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Input;

namespace TinyZoo.Z_BuldMenu.DragBuilder
{
  internal class RightMouseDrag
  {
    private Vector2 LocationLastFrame;
    public Vector2 RMouseDrag;
    public bool RMouseDragActive;

    public void UpdateRightMouseDrag(TinyZoo.PlayerDir.InputMap inputmap)
    {
      this.RMouseDrag = Vector2.Zero;
      this.RMouseDragActive = MouseStatus.RMouseHeld;
      if (MouseStatus.RMouseHeld)
        this.RMouseDrag = inputmap.PointerLocation - this.LocationLastFrame;
      this.LocationLastFrame = inputmap.PointerLocation;
    }

    public void DrawRightMouseDrag()
    {
    }
  }
}

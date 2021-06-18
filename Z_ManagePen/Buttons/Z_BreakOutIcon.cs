// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Buttons.Z_BreakOutIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_ManagePen.Buttons
{
  internal class Z_BreakOutIcon : GameObject
  {
    public Z_BreakOutIcon(BreakOutButtonType buttontype)
    {
      switch (buttontype)
      {
        case BreakOutButtonType.Back:
          this.DrawRect = new Rectangle(298, 195, 26, 26);
          this.SetAllColours(ColourData.Z_BreakOutIconBACLORANGE);
          break;
        case BreakOutButtonType.CloseMenu:
          this.DrawRect = new Rectangle(325, 195, 26, 26);
          this.SetAllColours(ColourData.Z_BreakOutIconCLOSERED);
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public void DrawZ_BreakOutIcon(Vector2 Offset) => this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
  }
}

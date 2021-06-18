// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BetaEnd.PictureOfPeople
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_BetaEnd
{
  internal class PictureOfPeople : GameObject
  {
    public PictureOfPeople()
    {
      this.DrawRect = new Rectangle(0, 256, 1024, 769);
      this.SetDrawOriginToCentre();
      this.scale = 0.3f;
    }

    public void UpdatePictureOfPeople()
    {
    }

    public void DrawPictureOfPeople() => this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.LogoSheet);
  }
}

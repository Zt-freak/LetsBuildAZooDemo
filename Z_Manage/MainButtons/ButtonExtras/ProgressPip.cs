// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.ButtonExtras.ProgressPip
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_Manage.MainButtons.ButtonExtras
{
  internal class ProgressPip : GameObject
  {
    private GameObject RED_ONE;
    private float Percent;

    public ProgressPip()
    {
      this.DrawRect = new Rectangle(925, 546, 17, 13);
      this.SetDrawOriginToCentre();
      this.scale = 1.5f;
      this.RED_ONE = new GameObject();
      this.RED_ONE.DrawRect = new Rectangle(907, 546, 17, 13);
      this.RED_ONE.SetDrawOriginToCentre();
      this.RED_ONE.scale = this.scale;
    }

    public void SetFullness(float FullNess)
    {
      this.RED_ONE.DrawRect.Width = (int) Math.Floor((double) this.DrawRect.Width * (double) FullNess);
      if (this.RED_ONE.DrawRect.Width <= this.DrawRect.Width)
        return;
      this.RED_ONE.DrawRect.Width = this.DrawRect.Width;
    }

    public void DrawProgressPip(Vector2 Offset)
    {
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      if (this.RED_ONE.DrawRect.Width <= 0)
        return;
      this.RED_ONE.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.vLocation);
    }
  }
}

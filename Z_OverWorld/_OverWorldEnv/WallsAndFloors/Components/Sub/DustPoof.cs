// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub.DustPoof
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub
{
  internal class DustPoof : AnimatedGameObject
  {
    public DustPoof()
    {
      this.DrawRect = new Rectangle(1585, 1255, 16, 19);
      this.DrawOrigin = new Vector2(8f, 10f);
      this.SetUpSimpleAnimation(6, 0.15f);
      this.PlayOnlyOnce = true;
    }

    public void UpdateDustPoof(float DeltaTime)
    {
      if (this.UpdateAnimation(DeltaTime) && (double) this.fTargetAlpha > 0.0)
        this.SetAlpha(false, 0.0f, 1f, 0.0f);
      this.UpdateAnimation(DeltaTime);
      this.UpdateColours(DeltaTime);
    }

    public void DrawDustPoof(SpriteBatch spritebatch) => this.WorldOffsetDraw(spritebatch, AssetContainer.EnvironmentSheet);
  }
}

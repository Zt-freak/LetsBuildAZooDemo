// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.beams.Explosion
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GamePlay.beams
{
  internal class Explosion : AnimatedGameObject
  {
    public Explosion(Vector2 Location) => this.Reset(Location);

    public void Reset(Vector2 Location)
    {
      this.vLocation = Location;
      this.DrawRect = new Rectangle(0, 192, 38, 32);
      this.SetDrawOriginToCentre();
      this.SetUpSimpleAnimation(7, 0.1f);
      this.bActive = true;
      this.PlayOnlyOnce = true;
    }

    public void UpdateExplosion(float DeltaTime)
    {
      if (!this.UpdateAnimation(DeltaTime))
        return;
      this.bActive = false;
    }

    public void DrawExplosion()
    {
      if (!this.bActive)
        return;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
    }
  }
}

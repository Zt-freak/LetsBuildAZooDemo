// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Particles.Z_Particle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Particles;

namespace TinyZoo.Z_Particles
{
  internal class Z_Particle : AnimatedGameObject
  {
    private bool DrewThisFrame;
    private BounceController bouncer;
    private float SpawnDelay;
    private Texture2D texturepointer;

    public void SpawnParticle(Vector2 Location, ParticleType particle, float FloorHeight)
    {
      this.texturepointer = AssetContainer.SpriteSheet;
      this.bActive = true;
      this.DrewThisFrame = true;
      this.SetAlpha(1f);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.scale = 2f;
      this.SetDrawOriginToCentre();
      float ForceStartingVelocity = 300f;
      float _Gravity = 400f;
      bool flag = true;
      this.IsAnimating = false;
      float _HorizontalDirection = MathStuff.getRandomFloat(-50f, 50f);
      this.SpawnDelay = MathStuff.getRandomFloat(0.0f, 1f);
      switch (particle)
      {
        case ParticleType.Blood:
          this.SetAllColours(1f, 0.0f, 0.0f);
          _Gravity = 200f;
          ForceStartingVelocity = -50f;
          this.scale = (float) TinyZoo.Game1.Rnd.Next(1, 3);
          this.SpawnDelay = MathStuff.getRandomFloat(0.0f, 0.3f);
          break;
        case ParticleType.AttackFlash:
          this.SpawnDelay = 0.0f;
          flag = false;
          _Gravity = 0.0f;
          ForceStartingVelocity = 0.0f;
          this.DrawRect = new Rectangle(1385, 129, 22, 23);
          this.SetDrawOriginToCentre();
          this.SetAllColours(1f, 1f, 1f);
          this.PlayOnlyOnce = true;
          this.SetUpSimpleAnimation(3, 0.1f);
          this.IsAnimating = true;
          this.scale = 1f;
          this.SetDrawOriginToCentre();
          this.texturepointer = AssetContainer.AnimalSheet;
          _HorizontalDirection = 0.0f;
          break;
        case ParticleType.FightDeath:
          this.SpawnDelay = 0.0f;
          flag = false;
          _Gravity = 0.0f;
          ForceStartingVelocity = 0.0f;
          this.DrawRect = new Rectangle(1621, 0, 27, 28);
          this.SetDrawOriginToCentre();
          this.SetAllColours(1f, 1f, 1f);
          this.scale = 1f;
          this.SetDrawOriginToCentre();
          this.texturepointer = AssetContainer.AnimalSheet;
          _HorizontalDirection = 0.0f;
          this.SetAlpha(false, 1f, 1f, 0.0f);
          break;
      }
      if (flag)
        Location.X += MathStuff.getRandomFloat(-10f, 10f);
      this.bouncer = new BounceController(Location, _HorizontalDirection, FloorHeight, ForceStartingVelocity: ForceStartingVelocity, _Gravity: _Gravity);
    }

    public void UpdateZ_Particle(float DeltaTime)
    {
      if (!this.bActive)
        return;
      if ((double) this.SpawnDelay > 0.0)
      {
        this.SpawnDelay -= DeltaTime;
        if ((double) this.SpawnDelay > 0.0)
          return;
      }
      if (this.IsAnimating && this.UpdateAnimation(DeltaTime))
        this.bActive = false;
      this.UpdateColours(DeltaTime);
      if ((double) this.fAlpha == 0.0)
        this.bActive = false;
      this.bouncer.UpdateBounceController_GetChangeFromLastFrame(DeltaTime);
      if (!this.bouncer.IsActive && (double) this.fTargetAlpha != 0.0)
        this.SetAlpha(false, 2f, 1f, 0.0f);
      if (this.DrewThisFrame)
        return;
      this.bActive = false;
    }

    public void DrawParticle(SpriteBatch spritebatch)
    {
      if (!this.bActive || (double) this.SpawnDelay > 0.0)
        return;
      this.vLocation = this.bouncer.CurrentLoc;
      this.DrewThisFrame = true;
      this.WorldOffsetDraw(spritebatch, this.texturepointer);
    }
  }
}

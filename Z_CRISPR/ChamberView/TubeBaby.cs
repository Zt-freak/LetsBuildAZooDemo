// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ChamberView.TubeBaby
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_CRISPR.ChamberView
{
  internal class TubeBaby
  {
    public Vector2 location;
    private SinSocillator Xoscillator;
    private SinSocillator Yoscillator;
    private GameObject someBlackThing;
    private EnemyRenderer animal;

    public TubeBaby(
      float babyProgress,
      AnimalType body,
      AnimalType head,
      int bodyVariant,
      int headVariant,
      float BaseScale)
    {
      if ((double) babyProgress >= 0.899999976158142)
      {
        this.animal = new EnemyRenderer(body, bodyVariant, head, headVariant);
        this.animal.scale = BaseScale;
        this.animal.SetAllColours(Color.Black.ToVector3());
        this.animal.vLocation.Y += (float) ((double) this.animal.DrawRect.Height * (double) this.animal.scale * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5);
      }
      else
      {
        this.someBlackThing = new GameObject();
        this.someBlackThing.DrawRect = TinyZoo.Game1.WhitePixelRect;
        this.someBlackThing.scale = BaseScale * 2f;
        this.someBlackThing.SetDrawOriginToCentre();
        this.someBlackThing.SetAllColours(Color.Black.ToVector3());
      }
      this.Xoscillator = new SinSocillator(0.2f, BaseScale * 2f);
      this.Yoscillator = new SinSocillator(0.2f, BaseScale * 2f);
    }

    public void UpdateTubeBaby(float DeltaTime)
    {
      this.Xoscillator.UpdateSinOscillator(DeltaTime);
      this.Yoscillator.UpdateSinOscillator(DeltaTime);
    }

    public void DrawTubeBaby(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.someBlackThing != null)
        this.someBlackThing.Draw(spriteBatch, AssetContainer.SpriteSheet, offset + new Vector2(this.Xoscillator.CurrentOffset, this.Yoscillator.CurrentOffset));
      if (this.animal == null)
        return;
      this.animal.ScreenSpaceDrawEnemyRenderer(offset + new Vector2(this.Xoscillator.CurrentOffset, this.Yoscillator.CurrentOffset), spriteBatch);
    }
  }
}

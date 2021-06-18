// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ChamberView.AnimalInTube
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_CRISPR.ChamberView
{
  internal class AnimalInTube
  {
    public Vector2 location;
    private GameObject tube;
    private GameObject tubeOverlay;
    private TubeBubbles tubeBubbles;
    private TubeBaby tubeBaby;

    public AnimalInTube(
      AnimalType body,
      AnimalType head,
      int bodyVariant,
      int headVariant,
      float BaseScale,
      float BabyProgress = 1f)
    {
      this.tube = new GameObject();
      this.tube.DrawRect = new Rectangle(432, 960, 56, 64);
      this.tube.SetDrawOriginToCentre();
      this.tube.scale = BaseScale;
      this.tubeOverlay = new GameObject();
      this.tubeOverlay.DrawRect = new Rectangle(489, 960, 56, 64);
      this.tubeOverlay.SetDrawOriginToCentre();
      this.tubeOverlay.scale = BaseScale;
      float num1 = 6f * this.tube.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num2 = 2f * this.tube.scale;
      this.tubeBaby = new TubeBaby(BabyProgress, body, head, bodyVariant, headVariant, BaseScale);
      this.tubeBaby.location.Y += num1;
      this.tubeBubbles = new TubeBubbles(10, 20, this.GetSize().X * -0.5f + num2, this.GetSize().X * 0.5f - num2, this.GetSize().Y - num1, BaseScale);
      this.tubeBubbles.location.Y += this.GetSize().Y * 0.5f;
    }

    public Vector2 GetSize() => new Vector2((float) this.tube.DrawRect.Width, (float) this.tube.DrawRect.Height) * this.tube.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void UpdateAnimalInTube(float DeltaTime)
    {
      this.tubeBubbles.UpdateTubeBubbles(DeltaTime);
      if (this.tubeBaby == null)
        return;
      this.tubeBaby.UpdateTubeBaby(DeltaTime);
    }

    public void DrawAnimalInTube(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tube.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      if (this.tubeBaby != null)
        this.tubeBaby.DrawTubeBaby(offset, spriteBatch);
      this.tubeBubbles.DrawTubeBubbles(offset, spriteBatch);
      this.tubeOverlay.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}

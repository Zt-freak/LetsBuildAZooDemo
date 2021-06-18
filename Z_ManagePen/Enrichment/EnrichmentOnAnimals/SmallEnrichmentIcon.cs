// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals.SmallEnrichmentIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals
{
  internal class SmallEnrichmentIcon : GameObject
  {
    public SmallEnrichmentIcon(float BaseScale)
    {
      this.DrawRect = new Rectangle(322, 359, 11, 10);
      this.SetDrawOriginToCentre();
      this.scale = BaseScale;
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width * this.scale, (float) this.DrawRect.Height * this.scale) * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawSmallEnrichmentIcon(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}

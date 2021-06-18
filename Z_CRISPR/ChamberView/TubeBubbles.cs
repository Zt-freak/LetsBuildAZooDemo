// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.ChamberView.TubeBubbles
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TinyZoo.Z_CRISPR.ChamberView
{
  internal class TubeBubbles
  {
    public Vector2 location;
    private List<TubeBubble> bubbles;

    public TubeBubbles(
      int minCount,
      int maxCount,
      float minX,
      float maxX,
      float maxHeight,
      float BaseScale)
    {
      this.bubbles = new List<TubeBubble>();
      int num = Game1.Rnd.Next(minCount, maxCount);
      for (int index = 0; index < num; ++index)
        this.bubbles.Add(new TubeBubble(BaseScale, maxHeight, minX, maxX));
    }

    public void UpdateTubeBubbles(float DeltaTime)
    {
      for (int index = 0; index < this.bubbles.Count; ++index)
        this.bubbles[index].UpdateTubeBubble(DeltaTime);
    }

    public void DrawTubeBubbles(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.bubbles.Count; ++index)
        this.bubbles[index].DrawTubeBubble(offset, spriteBatch);
    }
  }
}

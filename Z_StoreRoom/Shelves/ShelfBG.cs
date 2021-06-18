// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.ShelfBG
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_StoreRoom.Shelves
{
  internal class ShelfBG
  {
    private GameObject Thing;

    public ShelfBG()
    {
      this.Thing = new GameObject();
      this.Thing.DrawRect = new Rectangle(358, 921, 655, 102);
      this.Thing.scale = 2f;
    }

    public void DrawShelfBG(Vector2 Offset, SpriteBatch spritebatch, bool DrawOne)
    {
      if (DrawOne)
      {
        this.Thing.DrawRect.Height = (int) (75.0 * (double) Sengine.ScreenRationReductionMultiplier.Y);
        this.Thing.Draw(spritebatch, AssetContainer.UISheet, Offset);
        this.Thing.DrawRect.Height = 102;
      }
      else
      {
        for (float y = 0.0f; (double) y < 768.0; y += (float) this.Thing.DrawRect.Height * this.Thing.scale * Sengine.ScreenRatioUpwardsMultiplier.Y)
          this.Thing.Draw(spritebatch, AssetContainer.UISheet, new Vector2(0.0f, y) + Offset);
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.SexSymbol
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_AnimalNotification
{
  internal class SexSymbol
  {
    private static Rectangle mars = new Rectangle(1008, 890, 16, 17);
    private static Rectangle venus = new Rectangle(984, 851, 12, 19);
    private GameObject symbol;
    private bool isGirl;
    private float basescale;
    public Vector2 location;

    public SexSymbol(bool isAGirl, float basescale_)
    {
      this.isGirl = isAGirl;
      this.basescale = basescale_;
      this.symbol = new GameObject();
      this.symbol.DrawRect = !isAGirl ? SexSymbol.mars : SexSymbol.venus;
      this.symbol.scale = basescale_;
    }

    public void DrawSexSymbol(Vector2 offset, SpriteBatch spritebatch) => this.symbol.Draw(spritebatch, AssetContainer.AnimalSheet, offset + this.location);

    public Vector2 GetSize() => new Vector2((float) this.symbol.DrawRect.Width, (float) this.symbol.DrawRect.Height) * this.basescale * Sengine.ScreenRatioUpwardsMultiplier;
  }
}

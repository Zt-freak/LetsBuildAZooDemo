// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame.Chromosone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame
{
  internal class Chromosone : GameObject
  {
    public Chromosone(bool IsAGirl, float BaseScale = -1f)
    {
      this.DrawRect = new Rectangle(1008, 890, 16, 17);
      if (IsAGirl)
        this.DrawRect = new Rectangle(984, 851, 12, 19);
      this.SetDrawOriginToCentre();
      if ((double) BaseScale == -1.0)
        this.scale = 2f;
      else
        this.scale = BaseScale;
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawChromosone(Vector2 Offset, SpriteBatch DrawWithThis) => this.Draw(DrawWithThis, AssetContainer.AnimalSheet, Offset);
  }
}

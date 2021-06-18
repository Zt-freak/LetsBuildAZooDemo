// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralityTypeIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralityTypeIcon : GameObject
  {
    private bool locked = true;
    private MoralityType type;
    private new float scale;
    private float scalemult = 1f;

    public MoralityTypeIcon(MoralityType type_, float basescale)
    {
      this.type = type_;
      switch (this.type)
      {
        case MoralityType.FoodQuality:
          this.DrawRect = new Rectangle(951, 191, 32, 32);
          break;
        case MoralityType.Unions:
          this.DrawRect = new Rectangle(848, 69, 32, 32);
          break;
        case MoralityType.GarbageGenerated:
          this.DrawRect = new Rectangle(99, 504, 32, 32);
          break;
        case MoralityType.ElectricityUse:
          this.DrawRect = new Rectangle(66, 504, 32, 32);
          break;
        case MoralityType.CarbonMeat:
          this.DrawRect = new Rectangle(33, 504, 32, 32);
          break;
        case MoralityType.CarbonFuel:
          this.DrawRect = new Rectangle(0, 504, 32, 32);
          break;
        case MoralityType.Breeding:
          this.DrawRect = new Rectangle(617, 82, 32, 32);
          break;
        case MoralityType.CriticalChoice:
          this.DrawRect = new Rectangle(132, 504, 32, 32);
          break;
        case MoralityType.Beta:
          this.DrawRect = new Rectangle(742, 657, 32, 32);
          break;
        default:
          this.DrawRect = new Rectangle(742, 657, 32, 32);
          throw new Exception("MISSED THIS");
      }
      this.scale = basescale;
      this.SetDrawOriginToCentre();
    }

    public void DrawMoralityTypeIcon(Vector2 offset) => this.DrawMoralityTypeIcon(offset, AssetContainer.pointspritebatch03);

    public void DrawMoralityTypeIcon(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, this.scalemult * this.scale, 1f);

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      Vector2 vector2 = new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scalemult * this.scale;
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }
  }
}

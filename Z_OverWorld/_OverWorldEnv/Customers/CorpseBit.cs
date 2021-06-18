// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.Customers.CorpseBit
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.Customers
{
  internal class CorpseBit : GameObject
  {
    public CorpseBit(Vector2 WorldSpaceLocation, CorpseEntryType corpseEntryType)
    {
      this.vLocation = WorldSpaceLocation;
      switch (corpseEntryType)
      {
        case CorpseEntryType.Arms:
          this.DrawRect = new Rectangle(1505, 522, 12, 9);
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          break;
        case CorpseEntryType.BodyOb_A_L1:
          this.DrawRect = new Rectangle(1505, 514, 12, 7);
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          break;
        case CorpseEntryType.BodyOb_A_L2:
          this.DrawRect = new Rectangle(1551, 514, 7, 6);
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          ++this.DrawOrigin.Y;
          this.DrawOrigin.X -= 3f;
          this.SetAllColours(GetRandomColour.GetRandomBrightColour(TinyZoo.Game1.Rnd));
          break;
        case CorpseEntryType.BodyOb_A_L3:
          this.DrawRect = new Rectangle(1544, 514, 6, 6);
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          this.DrawOrigin.Y += 2f;
          this.DrawOrigin.X += 3.5f;
          this.SetAllColours(GetRandomColour.GetRandomBrightColour(TinyZoo.Game1.Rnd));
          break;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.DeadPersonRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers;

namespace TinyZoo.Z_OverWorld._OverWorldEnv
{
  internal class DeadPersonRenderer : GameObject
  {
    private List<CorpseBit> CorpseBits;
    private Vector2 VSCALE;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;

    public DeadPersonRenderer(Vector2 WorldSpaceLocation)
    {
      this.vLocation = WorldSpaceLocation;
      this.DrawRect = new Rectangle(1544, 521, 16, 11);
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.VSCALE = Vector2.One;
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
        this.FlipRender = true;
      this.CorpseBits = new List<CorpseBit>();
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
      {
        this.CorpseBits.Add(new CorpseBit(WorldSpaceLocation, CorpseEntryType.Arms));
      }
      else
      {
        this.CorpseBits.Add(new CorpseBit(WorldSpaceLocation, CorpseEntryType.BodyOb_A_L1));
        this.CorpseBits.Add(new CorpseBit(WorldSpaceLocation, CorpseEntryType.BodyOb_A_L2));
        this.CorpseBits.Add(new CorpseBit(WorldSpaceLocation, CorpseEntryType.BodyOb_A_L3));
      }
    }

    public void DrawDeadPersonRenderer(SpriteBatch spritebatch)
    {
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.QuickWorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, ref this.vLocation, ref this.VSCALE, this.Rotation, this.DrawRect, 1f, this.GetColour(), this.scale, false, ref DeadPersonRenderer.ThreadLoc, ref DeadPersonRenderer.ThreadScale);
      for (int index = 0; index < this.CorpseBits.Count; ++index)
      {
        this.CorpseBits[index].SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
        this.CorpseBits[index].QuickWorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, ref this.CorpseBits[index].vLocation, ref this.VSCALE, this.Rotation, this.CorpseBits[index].DrawRect, 1f, this.CorpseBits[index].GetColour(), this.scale, false, ref DeadPersonRenderer.ThreadLoc, ref DeadPersonRenderer.ThreadScale);
      }
    }
  }
}

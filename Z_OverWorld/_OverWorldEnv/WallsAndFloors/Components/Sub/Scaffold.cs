// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub.Scaffold
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub
{
  internal class Scaffold : GameObject
  {
    public bool IsBottomRow;
    private float DelayToDeath;
    private float Speed;
    private float Timer;

    public Scaffold(bool HasTopRenderer)
    {
      switch (TinyZoo.Game1.Rnd.Next(0, 6))
      {
        case 0:
          this.DrawRect = new Rectangle(921, 1174, 16, 24);
          break;
        case 1:
          this.DrawRect = new Rectangle(938, 1166, 16, 32);
          break;
        case 2:
          this.DrawRect = new Rectangle(955, 1168, 16, 30);
          break;
        case 3:
          this.DrawRect = new Rectangle(972, 1167, 16, 31);
          break;
        case 4:
          this.DrawRect = new Rectangle(989, 1166, 16, 32);
          break;
        case 5:
          this.DrawRect = new Rectangle(1006, 1176, 16, 22);
          break;
      }
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.DrawOrigin.Y -= 8f;
      this.DelayToDeath = (float) TinyZoo.Game1.Rnd.Next(10, 100);
      this.DelayToDeath *= 0.01f;
      if (!HasTopRenderer)
        this.DelayToDeath += 0.5f;
      this.Speed = (float) TinyZoo.Game1.Rnd.Next(10, 20) * 0.1f / (float) this.DrawRect.Height;
    }

    public bool UpdateScaffold(float DeltaTime, bool TopStillActive)
    {
      if (!TopStillActive)
      {
        if ((double) this.DelayToDeath > 0.0)
          this.DelayToDeath -= DeltaTime;
        if ((double) this.DelayToDeath <= 0.0)
        {
          this.Timer += DeltaTime;
          while ((double) this.Timer > (double) this.Speed && this.DrawRect.Height > 0)
          {
            this.Timer -= this.Speed;
            --this.DrawRect.Height;
            --this.DrawOrigin.Y;
          }
        }
      }
      return this.DrawRect.Height > 0;
    }

    public void DrawScaffold(SpriteBatch spritebatch)
    {
      if (this.DrawRect.Height <= 0)
        return;
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.WorldOffsetDraw(spritebatch, AssetContainer.EnvironmentSheet);
    }
  }
}

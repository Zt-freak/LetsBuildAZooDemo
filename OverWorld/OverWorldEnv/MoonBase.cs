// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.MoonBase
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_DayNight;

namespace TinyZoo.OverWorld.OverWorldEnv
{
  internal class MoonBase : GameObject
  {
    private static Vector2 VSCALE = Vector2.One;
    private static Vector2 LocVec = Vector2.One;
    private static Vector2 ScaleVec = Vector2.One;

    public MoonBase(int IndexX, int INDEXY, bool IsNormalGround = false)
    {
      this.DrawRect = new Rectangle(0, 0, 256, 256);
      this.SetDrawOriginToPoint(DrawOriginPosition.BottomRight);
      this.DrawOrigin.X += 8f;
      this.DrawOrigin.Y += 8f;
      this.vLocation = new Vector2((float) (IndexX * 256), (float) (INDEXY * 256) * Sengine.ScreenRatioUpwardsMultiplier.Y);
      if (IsNormalGround)
        return;
      float num = 0.2f;
      this.SetAllColours(num, num, num);
    }

    public void DrawMoonBase()
    {
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.QuickWorldOffsetDraw(AssetContainer.FloorBatch, AssetContainer.EnvironmentSheet, ref this.vLocation, ref MoonBase.VSCALE, this.Rotation, this.DrawRect, this.fAlpha, this.GetColour(), this.scale, false, ref MoonBase.LocVec, ref MoonBase.ScaleVec);
    }
  }
}

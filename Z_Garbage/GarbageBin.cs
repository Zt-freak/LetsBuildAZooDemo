// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Garbage.GarbageBin
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_Garbage
{
  internal class GarbageBin : GameObject
  {
    public GarbageBin(int BinIndex)
    {
      this.SetBinState(0);
      this.vLocation = TileMath.GetTileToWorldSpace(TileMath.GetGarbageBinLocation_Right(BinIndex));
    }

    public void SetBinState(int BagsInThisBin)
    {
      switch (BagsInThisBin)
      {
        case 0:
          this.DrawRect = new Rectangle(1636, 706, 16, 28);
          break;
        case 1:
          this.DrawRect = new Rectangle(1653, 706, 16, 28);
          break;
        default:
          this.DrawRect = new Rectangle(1619, 706, 16, 28);
          break;
      }
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.DrawOrigin.Y -= 8f;
    }

    public void DrawGarbageBin()
    {
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.UnifiedWaterAnimator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class UnifiedWaterAnimator : RenderComponent
  {
    private static float FrameTimer;
    private static int Frame;
    private int LastFrame;
    private Rectangle BaseRect;

    public UnifiedWaterAnimator(TileRenderer parent)
      : base(parent, RenderComponentType.WaterAnimator)
    {
      this.LastFrame = 0;
      this.BaseRect = parent.DrawRect;
    }

    internal static void UpdateUnifiedWaterAnimator(float DeltaTime)
    {
      UnifiedWaterAnimator.FrameTimer += DeltaTime;
      while ((double) UnifiedWaterAnimator.FrameTimer > 0.400000005960464)
      {
        UnifiedWaterAnimator.FrameTimer -= 0.4f;
        ++UnifiedWaterAnimator.Frame;
        if (UnifiedWaterAnimator.Frame > 2)
          UnifiedWaterAnimator.Frame = 0;
      }
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (this.LastFrame != UnifiedWaterAnimator.Frame)
      {
        parent.DrawRect.X = this.OriginalRectangle.X + (this.OriginalRectangle.Width + 1) * UnifiedWaterAnimator.Frame;
        parent.DrawRect.Y = this.OriginalRectangle.Y;
        this.LastFrame = UnifiedWaterAnimator.Frame;
      }
      return false;
    }
  }
}

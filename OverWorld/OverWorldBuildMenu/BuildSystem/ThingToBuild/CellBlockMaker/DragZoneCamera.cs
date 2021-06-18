// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker.DragZoneCamera
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Camera;
using TinyZoo.OverWorld.OverWorldEnv;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker
{
  internal class DragZoneCamera
  {
    private float Timer;

    public void UpdateDragZoneCamera(Player player, float DeltaTime)
    {
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0)
      {
        this.Timer += DeltaTime;
        if ((double) this.Timer <= 0.300000011920929)
          return;
        float num1 = 180f;
        float num2 = 250f;
        float num3 = 0.0f;
        float num4 = 0.0f;
        if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 1024.0 - (double) num1)
        {
          num3 = (float) ((1024.0 - (double) num1 - (double) player.player.touchinput.MultiTouchTouchLocations[0].X) * -1.0) / (num1 * 0.5f);
          if ((double) num3 > 1.0)
            num3 = 1f;
        }
        else if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X < (double) num1)
        {
          float num5 = (num1 - player.player.touchinput.MultiTouchTouchLocations[0].X) / (num1 * 0.5f);
          if ((double) num5 > 1.0)
            num5 = 1f;
          num3 = num5 * -1f;
        }
        if ((double) player.player.touchinput.MultiTouchTouchLocations[0].Y > 768.0 - (double) num1)
        {
          num4 = (float) ((768.0 - (double) num1 - (double) player.player.touchinput.MultiTouchTouchLocations[0].Y) * -1.0) / (num1 * 0.5f);
          if ((double) num4 > 1.0)
            num4 = 1f;
        }
        else if ((double) player.player.touchinput.MultiTouchTouchLocations[0].Y < (double) num2)
        {
          float num5 = (num2 - player.player.touchinput.MultiTouchTouchLocations[0].Y) / (num2 * 0.5f);
          if ((double) num5 > 1.0)
            num5 = 1f;
          num4 = num5 * -1f;
        }
        float num6 = 160f;
        if (!GameFlags.IsConsoleVersion)
          num6 = 800f;
        Sengine.WorldOriginandScale.X += num3 * DeltaTime * num6;
        Sengine.WorldOriginandScale.Y += num4 * DeltaTime * num6;
        CameraManager.HardSetCameraPosition(Sengine.WorldOriginandScale);
        OverWorldCamera.CurrentPos.X = Sengine.WorldOriginandScale.X;
        OverWorldCamera.CurrentPos.Y = Sengine.WorldOriginandScale.Y;
      }
      else
        this.Timer = 0.0f;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Threading.Thread_FloorRender
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using TinyZoo.OverWorld.OverWorldEnv.Ground;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.Z_Threading
{
  internal class Thread_FloorRender
  {
    private static List<Texture2D> FloorBatchTextures;
    private static List<Texture2D> FloorBatch2Textures;
    private static Vector2 ThreadVecLoc1;
    private static Vector2 ThreadVecLoc2;
    private static Vector2 ThreadVecLoc3;
    private static Vector2 ThresdVecScale1;
    private static Vector2 ThresdVecScale2;
    private static Vector2 ThresdVecScale3;

    internal static void DrawFloor(
      GroundManager groundmanager,
      WallsAndFloorsManager wallsandfloors)
    {
      int XInc = 0;
      int StartX;
      int StartY;
      int ENDY;
      int ENDX;
      TileMath.GetDrawArrayLimits(out StartX, out StartY, out ENDX, out ENDY);
      if (ENDX > wallsandfloors.FloorTilesArray.GetLength(0))
        ENDX = wallsandfloors.FloorTilesArray.GetLength(0);
      if (ENDY > wallsandfloors.FloorTilesArray.GetLength(1))
        ENDY = wallsandfloors.FloorTilesArray.GetLength(1);
      XInc = (ENDX - StartX) / 3;
      if (Z_DebugFlags.UseRenderThreading)
      {
        if (Thread_FloorRender.FloorBatch2Textures == null)
        {
          if (AssetContainer.EnvironmentSheet == null || AssetContainer.EnvironmentSheet2 == null)
            throw new Exception("Trying to initialize Unity texture sheets - when not ready");
          Thread_FloorRender.FloorBatchTextures = new List<Texture2D>();
          Thread_FloorRender.FloorBatchTextures.Add(AssetContainer.EnvironmentSheet);
          Thread_FloorRender.FloorBatch2Textures = new List<Texture2D>();
          Thread_FloorRender.FloorBatch2Textures.Add(AssetContainer.EnvironmentSheet);
          Thread_FloorRender.FloorBatch2Textures.Add(AssetContainer.EnvironmentSheet2);
          Thread_FloorRender.FloorBatch2Textures.Add(AssetContainer.AnimalSheet);
        }
        ThreadFlags.THREAD_FloorDraw = false;
        ThreadFlags.THREAD_FloorDraw2 = false;
        ThreadFlags.THREAD_FloorDraw3 = false;
        ThreadFlags.THREAD_UnderGroundDraw = false;
        groundmanager.DrawGroundManager();
        ThreadPool.QueueUserWorkItem((WaitCallback) (state => wallsandfloors.DrawFloorsManager(StartX, StartX + XInc, StartY, ENDY, ref ThreadFlags.THREAD_FloorDraw, AssetContainer.WF_FloorBatch, ref Thread_FloorRender.ThreadVecLoc1, ref Thread_FloorRender.ThresdVecScale1)));
        int STARTX2 = StartX + XInc;
        ThreadPool.QueueUserWorkItem((WaitCallback) (state => wallsandfloors.DrawFloorsManager(STARTX2, STARTX2 + XInc, StartY, ENDY, ref ThreadFlags.THREAD_FloorDraw2, AssetContainer.WF_FloorBatch2, ref Thread_FloorRender.ThreadVecLoc2, ref Thread_FloorRender.ThresdVecScale2)));
        int STARTX3 = STARTX2 + XInc;
        ThreadPool.QueueUserWorkItem((WaitCallback) (state => wallsandfloors.DrawFloorsManager(STARTX3, ENDX, StartY, ENDY, ref ThreadFlags.THREAD_FloorDraw3, AssetContainer.WF_FloorBatch3, ref Thread_FloorRender.ThreadVecLoc3, ref Thread_FloorRender.ThresdVecScale3)));
      }
      else
      {
        groundmanager.DrawGroundManager();
        wallsandfloors.DrawFloorsManager(StartX, ENDX, StartY, ENDY, ref ThreadFlags.THREAD_FloorDraw3, AssetContainer.WF_FloorBatch, ref Thread_FloorRender.ThreadVecLoc1, ref Thread_FloorRender.ThresdVecScale1);
      }
    }
  }
}

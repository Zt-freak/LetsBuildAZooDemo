// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.Fog.FogManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.OverWorldEnv.Fog
{
  internal class FogManager
  {
    private static List<FogLayer> fogs;
    private Vector2 WorldPosLastFrame;

    public FogManager()
    {
      FogManager.fogs = new List<FogLayer>();
      FogManager.fogs.Add(new FogLayer(0));
      FogManager.fogs.Add(new FogLayer(1));
      FogManager.fogs.Add(new FogLayer(2));
      FogManager.fogs.Add(new FogLayer(3));
      FogManager.fogs.Add(new FogLayer(4));
    }

    public void StartGame()
    {
      for (int index = 0; index < FogManager.fogs.Count; ++index)
        FogManager.fogs[index].SetAlpha(false, 4f, 0.0f, 1f);
    }

    internal static void StartFogFade(float BlendTime, float TargetAlpha)
    {
      if (LiveStats.IsChristmas || FogManager.fogs == null)
        return;
      for (int index = 0; index < FogManager.fogs.Count; ++index)
        FogManager.fogs[index].SetAlpha(true, BlendTime, 1f, TargetAlpha);
    }

    public void UpateFogManager(float DeltaTime)
    {
      if (LiveStats.IsChristmas)
      {
        for (int index = 0; index < FogManager.fogs.Count; ++index)
        {
          FogManager.fogs[index].SetAlpha(0.3f);
          FogManager.fogs[index].scale = 2f;
          FogManager.fogs[index].SetAllColours(1f, 1f, 1f);
        }
        DeltaTime *= 2f;
      }
      for (int index = 0; index < FogManager.fogs.Count; ++index)
        FogManager.fogs[index].UpdateFogLayer(DeltaTime * 0.4f);
    }

    public void UpateFogManagerInResearch(float DeltaTime)
    {
      if (LiveStats.IsChristmas)
      {
        for (int index = 0; index < FogManager.fogs.Count; ++index)
        {
          FogManager.fogs[index].SetAlpha(0.3f);
          FogManager.fogs[index].scale = 2f;
          FogManager.fogs[index].SetAllColours(1f, 1f, 1f);
        }
        DeltaTime *= 2f;
      }
      for (int index = 0; index < FogManager.fogs.Count; ++index)
      {
        FogManager.fogs[index].UpdateFogLayer(DeltaTime * 0.4f);
        float num = 0.4f + (float) index * 0.1f;
        FogLayer fog = FogManager.fogs[index];
        fog.vLocation = fog.vLocation + new Vector2(this.WorldPosLastFrame.X - Sengine.WorldOriginandScale.X, this.WorldPosLastFrame.Y - Sengine.WorldOriginandScale.Y) * -num;
      }
      this.WorldPosLastFrame = new Vector2(Sengine.WorldOriginandScale.X, Sengine.WorldOriginandScale.Y);
    }

    public void DrawFogManager()
    {
      for (int index = 0; index < FogManager.fogs.Count; ++index)
        FogManager.fogs[index].DrawFogLayer(AssetContainer.pointspritebatch03, AssetContainer.Fog);
    }

    public void DrawFogManagerInResearch()
    {
      for (int index = 0; index < FogManager.fogs.Count; ++index)
      {
        FogManager.fogs[index].SetAlpha(0.4f);
        FogManager.fogs[index].DrawFogLayer(AssetContainer.PointBlendBatch02, AssetContainer.Fog, 5f);
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Smear.SmearManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using System;
using System.Collections.Generic;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Smear
{
  internal class SmearManager
  {
    private List<SmearItem> spearitems;
    private static int Seed = 26;

    public SmearManager() => this.Create();

    private void Create()
    {
      int num = 30;
      Random rndd = new Random(SmearManager.Seed);
      this.spearitems = new List<SmearItem>();
      float _Width = (float) (TileMath.GetOverWorldMapSize_XDefault() * 16);
      float _Height = (float) (TileMath.GetOverWorldMapSize_YSize() * 16);
      for (int index = 0; index < num; ++index)
      {
        this.spearitems.Add(new SmearItem(rndd, _Width, _Height));
        this.spearitems[index].vLocation = new Vector2((float) rndd.Next(0, (int) _Width), (float) rndd.Next(0, (int) _Height));
        this.spearitems[index].vLocation.Y *= Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      ++SmearManager.Seed;
    }

    public void UpdateSmearManager(float DeltaTime)
    {
      for (int index = 0; index < this.spearitems.Count; ++index)
        this.spearitems[index].UpdateSmearItem(DeltaTime);
    }

    public void DrawSmearManager()
    {
      int num = PC_KeyState.J_PressedThisFrame ? 1 : 0;
      for (int index = 0; index < this.spearitems.Count; ++index)
        this.spearitems[index].DrawSmear();
    }
  }
}

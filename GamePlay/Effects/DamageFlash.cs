// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Effects.DamageFlash
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo.GamePlay.Effects
{
  internal class DamageFlash
  {
    private List<FlashEffect> flashes;
    internal static List<Vector2> FlashLocations;
    private GameObject WhiteOut;

    public DamageFlash()
    {
      this.WhiteOut = new GameObject();
      this.WhiteOut.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.WhiteOut.SetAllColours(1f, 0.0f, 0.0f);
      this.WhiteOut.scale = 1024f;
      this.flashes = new List<FlashEffect>();
      DamageFlash.FlashLocations = new List<Vector2>();
      this.WhiteOut.SetAlpha(0.0f);
    }

    public void DoFlash(Vector2 Location)
    {
      bool flag = false;
      for (int index = 0; index < this.flashes.Count; ++index)
      {
        if (!this.flashes[index].BActive && !flag)
        {
          flag = true;
          this.flashes[index].DoFlash(Location);
        }
      }
      if (flag)
        return;
      this.flashes.Add(new FlashEffect());
      this.flashes[this.flashes.Count - 1].DoFlash(Location);
    }

    public void UpdateDamageFlash(float DeltaTime)
    {
      if (DamageFlash.FlashLocations.Count > 0)
      {
        for (int index = 0; index < DamageFlash.FlashLocations.Count; ++index)
          this.DoFlash(DamageFlash.FlashLocations[index]);
        DamageFlash.FlashLocations = new List<Vector2>();
        this.WhiteOut.SetAlpha(false, 0.8f, 1f, 0.0f);
      }
      this.WhiteOut.UpdateColours(DeltaTime);
      for (int index = 0; index < this.flashes.Count; ++index)
        this.flashes[index].UpdateFlashEffect(DeltaTime);
    }

    public void DrawDamageFlash()
    {
      this.WhiteOut.Draw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet);
      int num = 0;
      while (num < this.flashes.Count)
        ++num;
    }
  }
}

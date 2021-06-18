// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo.ConsumptionFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo
{
  internal class ConsumptionFrame
  {
    private GameObject BG;
    private Vector2 BGScale;
    private List<ConsumptionEntry> consumptionentries;

    public ConsumptionFrame(bool IsConusmption, int[] Values)
    {
      this.BGScale = new Vector2(100f, 30f);
      this.BG = new GameObject();
      this.BG.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BG.SetDrawOriginToCentre();
      this.BG.SetAllColours(0.0f, 0.0f, 0.0f);
      this.BG.SetAlpha(0.3f);
      int num = 1;
      if (IsConusmption)
        num = -1;
      this.consumptionentries = new List<ConsumptionEntry>();
      for (int index = 0; index < Values.Length; ++index)
      {
        if (Values[index] != 0)
        {
          this.consumptionentries.Add(new ConsumptionEntry((ProductionType) index, Values[index] * num));
          this.consumptionentries[this.consumptionentries.Count - 1].Location.Y = (float) ((this.consumptionentries.Count - 1) * 30 - 20);
        }
      }
    }

    public void DrawConsumptionFrame(Vector2 Offset)
    {
      this.BG.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(0.0f, -15f), this.BGScale);
      for (int index = 0; index < this.consumptionentries.Count; ++index)
        this.consumptionentries[index].DrawConsumptionEntry(Offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.CollectionScreen.CharacterNeeds
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldStatus.StatsRend;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;

namespace TinyZoo.CollectionScreen
{
  internal class CharacterNeeds
  {
    private List<StatBar> statbar;

    public CharacterNeeds(AnimalType enemytype)
    {
      this.statbar = new List<StatBar>();
      for (int index = 0; index < LiveStats.reqforpeople.wantsbyperson[(int) enemytype].Uses.Length; ++index)
      {
        if (LiveStats.reqforpeople.wantsbyperson[(int) enemytype].Uses[index] > 0)
        {
          this.statbar.Add(new StatBar((ProductionType) index));
          this.statbar[this.statbar.Count - 1].SetForSummary(LiveStats.reqforpeople.wantsbyperson[(int) enemytype].Uses[index]);
        }
      }
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.statbar.Count; ++index)
      {
        this.statbar[index].Location = new Vector2((float) (num1 * 80 - 130), (float) (200.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * (double) Sengine.UltraWideSreenDownardsMultiplier + (double) (num2 * 80) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * (double) Sengine.UltraWideSreenDownardsMultiplier));
        ++num1;
        if (num1 > 3)
        {
          num1 = 0;
          ++num2;
        }
      }
    }

    public void UpdateCharacterNeeds()
    {
    }

    public void DrawCharacterNeeds(Vector2 Offset)
    {
      for (int index = 0; index < this.statbar.Count; ++index)
        this.statbar[index].DrawStatBar(Offset, index);
    }
  }
}

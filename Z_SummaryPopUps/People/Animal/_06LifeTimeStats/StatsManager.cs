// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats.StatsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats
{
  internal class StatsManager
  {
    public Vector2 location;
    private List<StatEntry> statsentries;
    private Vector2 size;

    public StatsManager(PrisonerInfo animal, float width, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      this.statsentries = new List<StatEntry>();
      for (int index = 0; index < 5; ++index)
      {
        StatEntry statEntry = new StatEntry(width, (AnimalStatType) index, animal, BaseScale);
        statEntry.location.Y = this.size.Y;
        statEntry.location.Y += statEntry.GetSize().Y * 0.5f;
        this.size.Y += statEntry.GetSize().Y;
        if (index != 4)
          this.size.Y += defaultBuffer.Y;
        this.statsentries.Add(statEntry);
      }
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateStatsManager()
    {
    }

    public void DrawStatsManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < 5; ++index)
        this.statsentries[index].DrawCustomerFrame(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats.LifetimeStatsTabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats
{
  internal class LifetimeStatsTabManager
  {
    public Vector2 location;
    private StatsManager satsmanager;

    public LifetimeStatsTabManager(
      PrisonerInfo animal,
      Player player,
      float width,
      float BaseScale)
    {
      this.satsmanager = new StatsManager(animal, width, BaseScale);
    }

    public Vector2 GetSize() => this.satsmanager.GetSize();

    public void UpdateLifetimeStatsTabManager() => this.satsmanager.UpdateStatsManager();

    public void DrawLifetimeStatsTabManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.satsmanager.DrawStatsManager(offset, spriteBatch);
    }
  }
}

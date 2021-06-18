// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.ZooWeek.WeekSummaryList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TinyZoo.Z_WeekOver.ZooWeek
{
  internal class WeekSummaryList
  {
    private List<DayBox> daybox;
    public Vector2 Location;

    public WeekSummaryList(SummaryListType thislist, Player player)
    {
      this.daybox = new List<DayBox>();
      for (int index = 0; index < 8; ++index)
      {
        this.daybox.Add(new DayBox(thislist, index - 1, player));
        this.daybox[index].vLocation.X = (float) (index * 100);
        this.daybox[index].vLocation.X -= 350f;
      }
    }

    public void UpdateWeekSummaryList(float DeltaTime)
    {
      for (int index = 0; index < this.daybox.Count; ++index)
        this.daybox[index].UpdateDayBox(DeltaTime);
    }

    public void DrawWeekSummaryList()
    {
      for (int index = 0; index < this.daybox.Count; ++index)
        this.daybox[index].DrawDayBox(this.Location);
    }
  }
}

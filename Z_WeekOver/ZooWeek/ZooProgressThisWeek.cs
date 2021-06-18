// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.ZooWeek.ZooProgressThisWeek
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_WeekOver.ZooWeek
{
  internal class ZooProgressThisWeek
  {
    private TextButton Next;
    private GameObjectNineSlice FrameForWHoleThing;
    private Vector2 VSCALE;
    private ScreenHeading subheading;
    private StringInBox SalarySummary;
    private List<WeekSummaryList> weeksummaries;

    public ZooProgressThisWeek(Player player)
    {
      this.Next = new TextButton(nameof (Next), 40f);
      this.Next.vLocation = new Vector2(900f, 700f);
      this.subheading = new ScreenHeading("HIGHLIGHTS!", 90f);
      this.subheading.header.vLocation = Vector2.Zero;
      this.FrameForWHoleThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out Vector3 _), 7);
      this.FrameForWHoleThing.vLocation = new Vector2(512f, 384f);
      this.VSCALE = new Vector2(900f, 600f);
      this.weeksummaries = new List<WeekSummaryList>();
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.DayOfWeek, player));
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.NewAnimalsBorn, player));
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.AnimalsRemoved, player));
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.AnimalsDied, player));
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.NewEmployees, player));
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.FiredEmployees, player));
      this.weeksummaries.Add(new WeekSummaryList(SummaryListType.EmployeeDeaths, player));
      float y = 150f;
      for (int index = 0; index < this.weeksummaries.Count; ++index)
      {
        if (index == 0)
        {
          this.weeksummaries[index].Location = new Vector2(512f, y);
          y += 65f;
        }
        else
          this.weeksummaries[index].Location = new Vector2(512f, y + (float) ((index - 1) * 100));
      }
    }

    public bool UpdateZooProgressThisWeek(Player player, float DeltaTime, Vector2 Offset)
    {
      for (int index = 0; index < this.weeksummaries.Count; ++index)
        this.weeksummaries[index].UpdateWeekSummaryList(DeltaTime);
      return this.Next.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawZooProgressThisWeek(Vector2 Offset)
    {
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.subheading.DrawScreenHeading(this.FrameForWHoleThing.vLocation + Offset + new Vector2(0.0f, this.VSCALE.Y * -0.5f), AssetContainer.pointspritebatch03);
      this.Next.DrawTextButton(Offset);
      for (int index = 0; index < this.weeksummaries.Count; ++index)
        this.weeksummaries[index].DrawWeekSummaryList();
    }
  }
}

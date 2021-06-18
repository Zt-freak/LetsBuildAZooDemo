// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GraphBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph
{
  internal class GraphBar
  {
    private List<GraphBarRenderer> graphbarrenderer;
    private List<string> CatNames;
    private float HORIZScale;
    public Vector2 Location;
    private float GapPad;
    private float HorizontalMaxScale;

    public GraphBar(
      ComaprerSet compareset,
      int Index,
      float HorizontalScale,
      bool IsDarker,
      float _GapPad = 2f)
    {
      this.GapPad = _GapPad;
      this.HorizontalMaxScale = HorizontalScale;
      this.CatNames = new List<string>();
      this.graphbarrenderer = new List<GraphBarRenderer>();
      for (int index = 0; index < compareset.ComparableGraphs.Length; ++index)
      {
        this.graphbarrenderer.Add(new GraphBarRenderer(compareset.ComparableGraphs[index].graphentries[Index].ThisEntryValue, compareset.ComparableGraphs[index].Colours[compareset.ComparableGraphs[index].graphentries[Index].ColourIndex], compareset.ComparableGraphs[index].HighestValueInGraph, IsDarker, compareset.ComparableGraphs[index].graphentries[Index].BackBarEntryValue, compareset.ComparableGraphs[index].Colours[compareset.ComparableGraphs[index].graphentries[Index].BackColourIndex]));
        this.CatNames.Add(compareset.ComparableGraphs[index].Name);
      }
      this.Location.X = (float) Index * HorizontalScale;
    }

    public void DrawGraphBar(Vector2 Offset, float VerticalHeight, List<int> DrawThese)
    {
      Offset += this.Location;
      Offset.X += this.HorizontalMaxScale * 0.5f;
      Vector2 SCALEMULT = new Vector2((this.HorizontalMaxScale - this.GapPad) / (float) DrawThese.Count, VerticalHeight);
      for (int index = 0; index < DrawThese.Count; ++index)
        this.graphbarrenderer[DrawThese[index]].DrawGraphBarRenderer(Offset + new Vector2(SCALEMULT.X * (float) index, 0.0f), SCALEMULT);
    }
  }
}

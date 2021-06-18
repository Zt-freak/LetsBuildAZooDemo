// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.GraphManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper;

namespace TinyZoo.Z_Manage.Accounts.GraphView
{
  internal class GraphManager
  {
    private bool Exiting;
    private List<ComaprerSet> comparesets;
    private GraphDisplay graphdisplay;
    private GraphDisplay graphdisplayRevenue;
    private GraphDisplay Balance;
    private bool IsOneWeek;

    public GraphManager(Player player, bool _IsOneWeek)
    {
      this.IsOneWeek = _IsOneWeek;
      this.comparesets = CreateGraphData.GetAcountGraph(player);
      this.graphdisplay = new GraphDisplay(this.comparesets[0]);
      this.graphdisplay.CENTERLOCATION = new Vector2(512f, 180f);
      this.graphdisplayRevenue = new GraphDisplay(this.comparesets[1]);
      this.graphdisplayRevenue.CENTERLOCATION = new Vector2(512f, 400f);
      this.Balance = new GraphDisplay(this.comparesets[2]);
      this.Balance.CENTERLOCATION = new Vector2(512f, 620f);
    }

    public void ForceExit() => this.Exiting = true;

    public bool UpdateGraphManager(Player player, float DeltaTime, Vector2 Offset)
    {
      this.graphdisplay.UpdateGraphDisplay(Offset, DeltaTime, player);
      this.graphdisplayRevenue.UpdateGraphDisplay(Offset, DeltaTime, player);
      this.Balance.UpdateGraphDisplay(Offset, DeltaTime, player);
      return this.Exiting;
    }

    public void DrawGraphManager(Vector2 Offset)
    {
      this.graphdisplay.DrawGraphDisplay(Offset);
      this.graphdisplayRevenue.DrawGraphDisplay(Offset);
      this.Balance.DrawGraphDisplay(Offset);
    }
  }
}

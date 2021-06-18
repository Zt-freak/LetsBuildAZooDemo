// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GraphAxisMain
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG;
using TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph
{
  internal class GraphAxisMain
  {
    private GameObjectNineSlice gameobjectnineslice;
    private AxisBars axisbars;
    private Vector2 VSCALE;
    private GraphBG graphBG;
    public Vector2 SIZEOFGRAPH;
    private HeadingAndFilters headingandfilers;

    public GraphAxisMain(
      float GraphHEight,
      float BarWidth,
      int TotalEntries,
      ComaprerSet comparerset)
    {
      Vector3 SecondaryColour;
      this.gameobjectnineslice = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.SIZEOFGRAPH = new Vector2(BarWidth * (float) TotalEntries, GraphHEight);
      this.axisbars = new AxisBars(SecondaryColour, this.SIZEOFGRAPH.Y, this.SIZEOFGRAPH.X);
      this.VSCALE = new Vector2(900f, GraphHEight + 60f);
      this.graphBG = new GraphBG(this.SIZEOFGRAPH);
      this.gameobjectnineslice.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      this.headingandfilers = new HeadingAndFilters(comparerset, SecondaryColour);
    }

    public void UpdateGrapAxis(
      Vector2 Offset,
      Vector2 OffsetFromOffsetToBottomLeft,
      Player player,
      float DeltaTime,
      ref List<int> CurrentFilter)
    {
      this.headingandfilers.UpdateHeadingAndFilters(Offset + this.gameobjectnineslice.vLocation, this.VSCALE, player, DeltaTime, ref CurrentFilter);
    }

    public void DrawGrapAxis(Vector2 Offset, Vector2 OffsetFromOffsetToBottomLeft)
    {
      this.gameobjectnineslice.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.headingandfilers.DrawHeadingAndFilters(Offset + this.gameobjectnineslice.vLocation, this.VSCALE);
      this.graphBG.DrawGraphBG(OffsetFromOffsetToBottomLeft + Offset);
      this.axisbars.DrawAxisBars(OffsetFromOffsetToBottomLeft + Offset);
    }
  }
}

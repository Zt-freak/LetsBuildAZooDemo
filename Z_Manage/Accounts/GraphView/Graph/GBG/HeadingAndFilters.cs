// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG.HeadingAndFilters
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG.filters;
using TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG
{
  internal class HeadingAndFilters : GameObject
  {
    private string MainHeading;
    private List<CatFilterBt> catfilters;

    public HeadingAndFilters(ComaprerSet comparerset, Vector3 SecondaryColour)
    {
      this.MainHeading = comparerset.TableName;
      this.SetAllColours(SecondaryColour);
      this.catfilters = new List<CatFilterBt>();
      for (int index = 0; index < comparerset.ComparableGraphs.Length; ++index)
        this.catfilters.Add(new CatFilterBt(comparerset.ComparableGraphs[index].Name, comparerset.ComparableGraphs[index].Colours[0], SecondaryColour));
    }

    public void UpdateHeadingAndFilters(
      Vector2 Center,
      Vector2 ScaleOfFrame,
      Player player,
      float DeltaTime,
      ref List<int> Filters)
    {
      float num = 10f;
      this.vLocation = new Vector2(Center.X - ScaleOfFrame.X * 0.5f + num, Center.Y - ScaleOfFrame.Y * 0.5f + num);
      for (int index1 = 0; index1 < this.catfilters.Count; ++index1)
      {
        if (this.catfilters[index1].UpdateCatFilterBt(this.vLocation + new Vector2(0.0f, (float) (40 * (1 + index1))), player))
        {
          Filters = new List<int>();
          for (int index2 = 0; index2 < this.catfilters.Count; ++index2)
          {
            if (this.catfilters[index2].IsOn)
              Filters.Add(index2);
          }
        }
      }
    }

    public void DrawHeadingAndFilters(Vector2 Center, Vector2 ScaleOfFrame)
    {
      TextFunctions.DrawTextWithDropShadow(this.MainHeading, 1f, this.vLocation, this.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      for (int index = 0; index < this.catfilters.Count; ++index)
        this.catfilters[index].DrawCatFilterBt(this.vLocation + new Vector2(0.0f, (float) (40 * (1 + index))));
    }
  }
}

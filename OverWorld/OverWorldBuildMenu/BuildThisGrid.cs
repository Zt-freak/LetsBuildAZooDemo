// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildThisGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu
{
  internal class BuildThisGrid
  {
    public List<TILETYPE> buildables;
    private List<BIconAndCost> bicons_Depricated;

    public BuildThisGrid(CATEGORYTYPE category, Player player)
    {
      List<TILETYPE> entriesInThisCategory = CategoryData.GetEntriesInThisCategory(category);
      this.buildables = new List<TILETYPE>();
      for (int index = 0; index < entriesInThisCategory.Count; ++index)
        this.buildables.Add(entriesInThisCategory[index]);
      if (category == CATEGORYTYPE.Amenities)
      {
        if (player.Stats.ADisabled(true, player))
        {
          this.buildables.Add(TILETYPE.PinkTreeIAP);
          this.buildables.Add(TILETYPE.BlueTreeIAP);
          this.buildables.Add(TILETYPE.GoatIAP);
        }
        if (player.Stats.Vortex())
        {
          this.buildables.Add(TILETYPE.ResearchIAP);
          this.buildables.Add(TILETYPE.TrashCompactorIAP);
        }
        if (player.Stats.GetFlower())
          this.buildables.Add(TILETYPE.FlowerIAP);
      }
      this.bicons_Depricated = new List<BIconAndCost>();
      for (int index = 0; index < this.buildables.Count; ++index)
        this.bicons_Depricated.Add(new BIconAndCost(this.buildables[index], index, player));
      BIconAndCost.Total = this.buildables.Count;
      if (FeatureFlags.OnlyALlowTisThingsToBeBuilt != null && FeatureFlags.OnlyALlowTisThingsToBeBuilt.Count > 0)
      {
        for (int index = 0; index < this.buildables.Count; ++index)
        {
          if (!FeatureFlags.OnlyALlowTisThingsToBeBuilt.Contains(this.buildables[index]) && player.Stats.research.BuildingsResearched.Contains(this.buildables[index]))
            this.bicons_Depricated[index].DarkenThis();
          else if (!FeatureFlags.OnlyALlowTisThingsToBeBuilt.Contains(this.buildables[index]) && (this.buildables[index] == TILETYPE.GoatIAP || this.buildables[index] == TILETYPE.PinkTreeIAP || (this.buildables[index] == TILETYPE.BlueTreeIAP || this.buildables[index] == TILETYPE.ResearchIAP) || (this.buildables[index] == TILETYPE.TrashCompactorIAP || this.buildables[index] == TILETYPE.FlowerIAP)))
            this.bicons_Depricated[index].DarkenThis();
        }
      }
      if (category == CATEGORYTYPE.Amenities)
      {
        for (int index = 0; index < this.buildables.Count; ++index)
        {
          if (!player.Stats.research.BuildingsResearched.Contains(this.buildables[index]))
            this.bicons_Depricated[index].LockThis();
        }
      }
      if (category != CATEGORYTYPE.Enclosure)
        return;
      for (int index = 0; index < this.buildables.Count; ++index)
      {
        if (!player.Stats.research.CellBlocksReseacrhed.Contains(this.buildables[index]))
          this.bicons_Depricated[index].LockThis();
      }
    }

    public bool IsThisLocked(int Index) => this.bicons_Depricated[Index].Locked;

    public int UpateBuildThisGrid(Vector2 Offset, Player player, float DeltaTime)
    {
      int num = -1;
      if (0 < this.buildables.Count)
        throw new Exception("YOU SHOULD BE USING THE CONTROLLER MATRIX HERE");
      return num;
    }

    public void DrawBuildThisGrid(Vector2 Offset, int SelectedIndex)
    {
      for (int index = 0; index < this.buildables.Count; ++index)
        this.bicons_Depricated[index].DrawBIconAndCost(Offset, SelectedIndex == index, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

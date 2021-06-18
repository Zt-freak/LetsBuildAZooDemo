// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.IconGrid.Z_IconPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.IconGrid
{
  internal class Z_IconPanel
  {
    private GameObjectNineSlice Frame;
    public List<TILETYPE> buildables;
    public List<BIconAndCost> bicons;
    public CATEGORYTYPE ThisCategory;
    private Vector2 DragOffset;
    private float MinY;
    private Vector2 VSCALE;
    public bool UsingExternalIconPanel;
    private int ControllerForceSelect = -1;
    internal static Vector2 TargetBuildTileLocation;

    public Z_IconPanel(
      CATEGORYTYPE category,
      Player player,
      float MinHeight,
      bool _UsingExternalIconPanel = false)
    {
      this.UsingExternalIconPanel = _UsingExternalIconPanel;
      this.ControllerForceSelect = -1;
      this.ThisCategory = category;
      List<TILETYPE> entriesInThisCategory = CategoryData.GetEntriesInThisCategory(category);
      this.buildables = new List<TILETYPE>();
      for (int index = 0; index < entriesInThisCategory.Count; ++index)
        this.buildables.Add(entriesInThisCategory[index]);
      this.bicons = new List<BIconAndCost>();
      for (int index = 0; index < this.buildables.Count; ++index)
        this.bicons.Add(new BIconAndCost(this.buildables[index], index, player));
      BIconAndCost.Total = this.buildables.Count;
      if (FeatureFlags.OnlyALlowTisThingsToBeBuilt != null && FeatureFlags.OnlyALlowTisThingsToBeBuilt.Count > 0)
      {
        for (int index = 0; index < this.buildables.Count; ++index)
        {
          if (!FeatureFlags.OnlyALlowTisThingsToBeBuilt.Contains(this.buildables[index]) && player.Stats.research.BuildingsResearched.Contains(this.buildables[index]))
            this.bicons[index].DarkenThis();
          else if (!FeatureFlags.OnlyALlowTisThingsToBeBuilt.Contains(this.buildables[index]) && (this.buildables[index] == TILETYPE.GoatIAP || this.buildables[index] == TILETYPE.PinkTreeIAP || (this.buildables[index] == TILETYPE.BlueTreeIAP || this.buildables[index] == TILETYPE.ResearchIAP) || (this.buildables[index] == TILETYPE.TrashCompactorIAP || this.buildables[index] == TILETYPE.FlowerIAP)))
            this.bicons[index].DarkenThis();
        }
      }
      if (category != CATEGORYTYPE.Enclosure)
      {
        for (int index = 0; index < this.buildables.Count; ++index)
        {
          if (!player.Stats.research.BuildingsResearched.Contains(this.buildables[index]))
            this.bicons[index].LockThis();
        }
      }
      if (category == CATEGORYTYPE.Enclosure)
      {
        for (int index = 0; index < this.buildables.Count; ++index)
        {
          if (!player.Stats.research.CellBlocksReseacrhed.Contains(this.buildables[index]))
            this.bicons[index].LockThis();
        }
      }
      this.MinY = this.bicons[this.bicons.Count - 1].Location.Y - this.bicons[0].Location.Y;
      this.MinY *= -1f;
      this.Frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Brown, out Vector3 _), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.VSCALE = new Vector2(374f, 768f - MinHeight);
      this.Frame.vLocation = new Vector2(512f, (float) ((768.0 - (double) MinHeight) * 0.5));
    }

    public bool IsThisLocked(int Index) => this.bicons[Index].Locked;

    public void TryToSelectFromController(int _ControllerForceSelect) => this.ControllerForceSelect = _ControllerForceSelect;

    public int UpateBuildThisGrid(Vector2 Offset, Player player, float DeltaTime, float MinHeight)
    {
      if (this.UsingExternalIconPanel)
        return -1;
      if (Z_BuildingIconPanel.DisableDrag(player) && (double) this.MinY < 0.0)
        this.DragOffset.Y += SpringDrag.UpdateSpringyDrag(player.player.touchinput.DragActive, player.player.touchinput.DragVectorThisFrame.Y, 100f, this.MinY, 0.0f, this.DragOffset.Y);
      int num = -1;
      for (int index = 0; index < this.buildables.Count; ++index)
      {
        if (this.bicons[index].UpdateBIconAndCost(DeltaTime, player, Offset + this.DragOffset, MinHeight, this.ControllerForceSelect == index))
          num = index;
      }
      this.ControllerForceSelect = -1;
      return num;
    }

    public void DrawBuildThisGrid(
      Vector2 Offset,
      int SelectedIndex,
      float MinHeight,
      bool DrawFrameOnly = false)
    {
      if (this.UsingExternalIconPanel)
        return;
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      if (DrawFrameOnly)
        return;
      for (int index = 0; index < this.buildables.Count; ++index)
        this.bicons[index].DrawBIconAndCost(Offset + this.DragOffset, SelectedIndex == index, MinHeight, AssetContainer.pointspritebatch03);
      if (!FeatureFlags.LockToBuildPen)
        return;
      Z_IconPanel.TargetBuildTileLocation = this.bicons[0].Location + Offset + this.DragOffset + new Vector2(0.0f, (float) (-(double) this.bicons[0].SelectedThing.scale * 0.5));
    }
  }
}

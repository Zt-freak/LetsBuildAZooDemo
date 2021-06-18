// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.ResearchTree.TreeDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Architcture.RData;

namespace TinyZoo.Z_Architcture.ResearchTree
{
  internal class TreeDisplay
  {
    private List<TreeEntry> treeentries;
    private GameObject Frame;
    private Vector2 VSCALE;
    private CatButton catbutt;

    public TreeDisplay(CATEGORYTYPE categorytype, Player player, ref Vector2 Sizee)
    {
      this.treeentries = new List<TreeEntry>();
      ResearchSet researchSet = ArchitectResearchData.GetResearchSet(categorytype);
      this.Frame = new GameObject();
      this.Frame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Frame.vLocation.Y = (float) ((int) categorytype * 200) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Frame.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.Frame.SetAlpha(0.5f);
      this.Frame.SetAllColours(0.0f, 0.0f, 0.5f);
      if ((int) categorytype % 2 == 0)
        this.Frame.SetAllColours(0.4f, 0.1f, 0.1f);
      int LastUnlockedTier = -1;
      for (int index = 0; index < researchSet.entries.Count; ++index)
        this.treeentries.Add(new TreeEntry(researchSet.entries[index], player, ref Sizee, index > 0, ref LastUnlockedTier, categorytype));
      this.VSCALE = new Vector2(1024f, 195f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.catbutt = new CatButton();
      this.catbutt.SetCategory(categorytype);
      this.catbutt.vLocation = new Vector2(30f, 0.0f);
      if ((double) Sizee.Y >= (double) this.Frame.vLocation.Y + (double) this.VSCALE.Y * 0.5)
        return;
      Sizee.Y = this.Frame.vLocation.Y + this.VSCALE.Y * 0.5f;
    }

    public void Lock()
    {
      for (int index = 0; index < this.treeentries.Count; ++index)
        this.treeentries[index].SetLocked();
    }

    public bool UpdateTreeDisplay(Player player, float DeltaTime, Vector2 Offset)
    {
      bool flag = false;
      for (int index = 0; index < this.treeentries.Count; ++index)
      {
        if (this.treeentries[index].UpdateTreeEntry(player, DeltaTime, Offset + this.Frame.vLocation))
          flag = true;
      }
      return flag;
    }

    public void DrawTreeDisplay(Vector2 Offset, float Zoom)
    {
      this.Frame.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, new Vector2(0.0f, Offset.Y), this.VSCALE);
      Offset += this.Frame.vLocation;
      this.catbutt.DrawCatButton(Offset, false);
      for (int index = 0; index < this.treeentries.Count; ++index)
        this.treeentries[index].PreDrawTreeEntry(AssetContainer.pointspritebatch03, Offset);
      for (int index = 0; index < this.treeentries.Count; ++index)
        this.treeentries[index].DrawTreeEntry(AssetContainer.pointspritebatch03, Offset);
    }
  }
}

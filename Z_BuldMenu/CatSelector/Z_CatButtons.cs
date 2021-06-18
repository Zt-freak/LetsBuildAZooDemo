// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.CatSelector.Z_CatButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.CatSelector
{
  internal class Z_CatButtons
  {
    private DirectionPressed CycleDirection = DirectionPressed.None;
    private List<CatButton> caybuttons;
    private GameObject gameeobjectfortext;

    public Z_CatButtons(Vector3 TextColour, Player player, int ColumnCount = 6)
    {
      this.caybuttons = new List<CatButton>();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < 14; ++index)
      {
        this.caybuttons.Add(new CatButton(0.7f));
        this.caybuttons[index].SetCategory((CATEGORYTYPE) index);
        this.caybuttons[index].vLocation = new Vector2((float) num1 * (40f * this.caybuttons[index].scale), (float) num2 * (40f * this.caybuttons[index].scale * Sengine.ScreenRatioUpwardsMultiplier.Y));
        this.caybuttons[index].vLocation.Y -= 38f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        ++num1;
        if (num1 == ColumnCount)
        {
          num1 = 0;
          ++num2;
        }
        this.caybuttons[index].vLocation.X += 20f * this.caybuttons[index].scale;
        this.caybuttons[index].DoPop((float) index);
        this.caybuttons[index].vLocation.X -= RenderMath.GetPixelZoomOneToOne() * 80f;
        if (!player.Stats.TutorialsComplete[29] && index > 0)
          this.caybuttons[index].Disable();
      }
      if (FeatureFlags.LockToBuildPen)
      {
        for (int index = 1; index < 14; ++index)
          this.caybuttons[index].Disable();
      }
      this.gameeobjectfortext = new GameObject();
      this.gameeobjectfortext.SetAllColours(TextColour);
    }

    public void TryToCycleSelection(DirectionPressed direction) => this.CycleDirection = direction;

    public CATEGORYTYPE UpdateZ_CatButtons(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      CATEGORYTYPE SelectedCategory)
    {
      CATEGORYTYPE categorytype = CATEGORYTYPE.Count;
      if (this.CycleDirection != DirectionPressed.None)
      {
        int num = (int) SelectedCategory;
        if (this.CycleDirection == DirectionPressed.Right)
        {
          if (num < this.caybuttons.Count - 1)
            this.caybuttons[num + 1].ForceSelectFromController = true;
          else
            this.caybuttons[0].ForceSelectFromController = true;
        }
        else if (num > 0)
          this.caybuttons[num - 1].ForceSelectFromController = true;
        else
          this.caybuttons[this.caybuttons.Count - 1].ForceSelectFromController = true;
        this.CycleDirection = DirectionPressed.None;
      }
      for (int index = 0; index < this.caybuttons.Count; ++index)
      {
        if (this.caybuttons[index].UpdateCatButton(player, Offset, DeltaTime))
          categorytype = this.caybuttons[index].category;
      }
      return categorytype;
    }

    public bool CheckMouseOver(Player player, Vector2 Offset, float ScaleMult)
    {
      for (int index = 0; index < this.caybuttons.Count; ++index)
      {
        if (this.caybuttons[index].CheckMouseOver(player, Offset))
          return true;
      }
      return false;
    }

    public void DrawZ_CatButtons(Vector2 Offset, CATEGORYTYPE SelectedCategory)
    {
      for (int index = 0; index < this.caybuttons.Count; ++index)
        this.caybuttons[index].DrawCatButton(Offset, this.caybuttons[index].category == SelectedCategory);
    }
  }
}

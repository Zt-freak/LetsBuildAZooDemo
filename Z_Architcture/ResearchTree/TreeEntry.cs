// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.ResearchTree.TreeEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Architcture.RData;

namespace TinyZoo.Z_Architcture.ResearchTree
{
  internal class TreeEntry
  {
    public Vector2 Location;
    private GameObjectNineSlice gameobjectninslice;
    private Vector2 VScale;
    private bool MouseOver;
    private UIBar uibar;
    private BIconAndCost bicon;
    private string Prgress;
    private TreeLines treelines;
    public bool IsLocked;
    private bool CanBeStarted;
    private TextButton StartButton;
    private ResearchEntry researchentry;
    private CATEGORYTYPE categorytype;

    public TreeEntry(
      ResearchEntry _researchentry,
      Player player,
      ref Vector2 SIZE,
      bool HasTreeLines,
      ref int LastUnlockedTier,
      CATEGORYTYPE _categorytype)
    {
      this.categorytype = _categorytype;
      this.researchentry = _researchentry;
      this.IsLocked = false;
      bool flag1 = true;
      if (this.categorytype == CATEGORYTYPE.Enclosure)
      {
        if (!player.Stats.research.CellBlocksReseacrhed.Contains(this.researchentry.ThingToDiscover))
          flag1 = false;
      }
      else if (!player.Stats.research.BuildingsResearched.Contains(this.researchentry.ThingToDiscover))
        flag1 = false;
      if (!flag1)
      {
        this.IsLocked = true;
        if (LastUnlockedTier >= this.researchentry.COLUMN - 1 && !player.z_research.IsResearching())
        {
          this.CanBeStarted = true;
          this.StartButton = new TextButton("Research", _UseRoundaboutFont: true);
        }
      }
      else
        LastUnlockedTier = this.researchentry.COLUMN;
      float Width = 100f;
      this.Prgress = "0/" + (object) this.researchentry.TotalDaysToUnlock;
      bool flag2 = false;
      bool flag3 = false;
      if (player.z_research.IsResearching())
      {
        if (player.z_research.ResearchingThis == _researchentry.ThingToDiscover)
        {
          flag3 = true;
          this.Prgress = (this.researchentry.TotalDaysToUnlock - player.z_research.DaysLeft).ToString() + "/" + (object) this.researchentry.TotalDaysToUnlock;
        }
        else if (this.IsLocked)
          flag2 = true;
      }
      this.Location = new Vector2((float) this.researchentry.COLUMN * Width, (float) (this.researchentry.Row * 50) * Sengine.ScreenRatioUpwardsMultiplier.Y);
      Vector3 SecondaryColour;
      Rectangle frameColourRect = StringInBox.GetFrameColourRect(BTNColour.Blue, out SecondaryColour);
      if (this.IsLocked)
      {
        frameColourRect = StringInBox.GetFrameColourRect(BTNColour.Red, out SecondaryColour);
        if (flag2)
          frameColourRect = StringInBox.GetFrameColourRect(BTNColour.Grey, out SecondaryColour);
        else if (flag3)
        {
          frameColourRect = StringInBox.GetFrameColourRect(BTNColour.Green, out SecondaryColour);
          this.uibar = new UIBar(new Vector2(40f, 5f * Sengine.ScreenRationReductionMultiplier.Y), 1f, TinyZoo.Game1.WhitePixelRect);
          this.uibar.SetFullness((float) (1.0 - (double) player.z_research.DaysLeft / (double) this.researchentry.TotalDaysToUnlock));
          this.uibar.SetUpGreenBar();
        }
      }
      this.gameobjectninslice = new GameObjectNineSlice(frameColourRect, 7);
      this.gameobjectninslice.scale = 2f;
      this.gameobjectninslice.SetAllColours(0.7490196f, 0.7098039f, 0.6117647f);
      this.VScale = new Vector2(80f, 80f);
      this.Location.X += 100f;
      this.bicon = new BIconAndCost(this.researchentry.ThingToDiscover, 0, player, false);
      if (this.IsLocked)
      {
        if (this.StartButton != null)
        {
          this.bicon.LockThis();
          this.bicon.Location = new Vector2(0.0f, -15f * Sengine.ScreenRatioUpwardsMultiplier.Y);
          this.bicon.SetPopUpText(this.Prgress);
        }
        else
        {
          this.bicon.LockThis();
          this.bicon.Location = new Vector2(0.0f, -15f * Sengine.ScreenRatioUpwardsMultiplier.Y);
        }
      }
      else
        this.bicon.Location = new Vector2(0.0f, 0.0f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      if ((double) SIZE.X < (double) this.Location.X + (double) this.VScale.X)
        SIZE.X = this.Location.X + this.VScale.X;
      if (!HasTreeLines)
        return;
      this.treelines = new TreeLines((float) (this.researchentry.Row * 50) * Sengine.ScreenRatioUpwardsMultiplier.Y, Width, this.researchentry.COLUMN % 3 == 2);
    }

    public void SetLocked() => this.StartButton = (TextButton) null;

    public bool UpdateTreeEntry(Player player, float DeltaTime, Vector2 Offset)
    {
      bool flag = false;
      Offset += this.Location;
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset + this.gameobjectninslice.vLocation, 1f, this.VScale.X, this.VScale.Y, player.inputmap.PointerLocation);
      this.bicon.UpdateBIconAndCost(DeltaTime, player, Offset, -100f, false);
      if (this.StartButton != null && this.StartButton.UpdateTextButton(player, Offset + new Vector2(0.0f, 20f * Sengine.ScreenRatioUpwardsMultiplier.Y), DeltaTime))
      {
        player.z_research.StartResearch(this.bicon.tiletype, this.researchentry.TotalDaysToUnlock);
        flag = true;
      }
      this.bicon.MouseOver = this.MouseOver;
      return flag;
    }

    public void PreDrawTreeEntry(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      if (this.treelines == null)
        return;
      this.treelines.DrawTreeLines(Offset + this.gameobjectninslice.vLocation);
    }

    public void DrawTreeEntry(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.gameobjectninslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.IsLocked)
      {
        if (this.StartButton != null)
        {
          TextFunctions.DrawJustifiedText(this.Prgress, 2f, Offset + new Vector2(15f, -12f * Sengine.ScreenRatioUpwardsMultiplier.Y), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
          this.StartButton.DrawTextButton(Offset + new Vector2(0.0f, 20f * Sengine.ScreenRatioUpwardsMultiplier.Y));
          Offset.X -= 18f;
          this.bicon.DrawBIconAndCost(Offset, false, -100f, AssetContainer.pointspritebatch03, 18f);
        }
        else
        {
          if (this.uibar != null)
            this.uibar.DrawUI_Bar(spritebatch, Offset + new Vector2(0.0f, 25f * Sengine.ScreenRatioUpwardsMultiplier.Y), AssetContainer.SpriteSheet);
          TextFunctions.DrawJustifiedText(this.Prgress, 2f, Offset + new Vector2(0.0f, 12f * Sengine.ScreenRatioUpwardsMultiplier.Y), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
          this.bicon.DrawBIconAndCost(Offset, false, -100f, AssetContainer.pointspritebatch03);
        }
      }
      else
      {
        TextFunctions.DrawJustifiedText("  ", 2f, Offset + new Vector2(0.0f, 12f * Sengine.ScreenRatioUpwardsMultiplier.Y), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
        this.bicon.DrawBIconAndCost(Offset, false, -100f, AssetContainer.pointspritebatch03);
      }
    }
  }
}

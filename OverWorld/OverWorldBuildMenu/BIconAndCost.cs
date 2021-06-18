// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BIconAndCost
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.OverWorldBuildMenu
{
  internal class BIconAndCost
  {
    private BuldCost Cost;
    public Vector2 Location;
    public BuildingIcon buldingcon;
    private static BuildingIcon Lockedbuldingcon;
    internal static int PerRow;
    internal static int Total;
    public bool MouseOver;
    private StringInBox objname;
    public GameObject SelectedThing;
    public bool Locked;
    private bool Darkened;
    public TILETYPE tiletype;
    private bool DrawCost;

    public BIconAndCost(TILETYPE _tiletype, int Index, Player player, bool _DrawCost = true)
    {
      this.DrawCost = _DrawCost;
      this.tiletype = _tiletype;
      float pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(3f);
      if (DebugFlags.IsPCVersion)
        pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(2f);
      this.buldingcon = new BuildingIcon(this.tiletype, pixelSizeBestMatch);
      int cost = player.livestats.GetCost(_tiletype, player, true);
      this.objname = new StringInBox(TileData.GetTileStats(_tiletype).Name, RenderMath.GetPixelSizeBestMatch(1f), 130f, true);
      this.objname.SetAsButtonFrame(BTNColour.Blue);
      this.objname.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      BIconAndCost.Lockedbuldingcon = new BuildingIcon(TILETYPE.Research_PrisonPlanet, pixelSizeBestMatch);
      BIconAndCost.Lockedbuldingcon.DrawRect = new Rectangle(632, 2, 16, 16);
      BIconAndCost.PerRow = 4;
      if (GameFlags.HasNotch)
        BIconAndCost.PerRow = 3;
      float num1 = 60f;
      float num2 = pixelSizeBestMatch * 18f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Location = new Vector2((float) (Index % BIconAndCost.PerRow) * num1, (float) (Index / BIconAndCost.PerRow) * num2);
      this.Location.X -= (float) ((double) (BIconAndCost.PerRow - 1) * (double) num1 * 0.5);
      this.Location.Y += 40f;
      this.Cost = new BuldCost(this.tiletype, cost);
      this.SelectedThing = new GameObject();
      this.SelectedThing.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SelectedThing.SetDrawOriginToCentre();
      this.SelectedThing.scale = pixelSizeBestMatch * (float) this.buldingcon.DrawRect.Width;
      this.SelectedThing.fAlpha = 0.4f;
      int num3 = GameFlags.HasNotch ? 1 : 0;
      if (!DebugFlags.IsPCVersion)
        return;
      float num4 = num2 + 20f;
      BIconAndCost.PerRow = 10;
      this.Location = new Vector2((float) (Index % BIconAndCost.PerRow) * num1, (float) (Index / BIconAndCost.PerRow) * num4);
      this.Location.X -= (float) ((double) (BIconAndCost.PerRow - 1) * (double) num1 * 0.5);
      this.Location.Y += 40f;
      this.Location.X += 512f;
    }

    public void DarkenThis()
    {
      this.Darkened = true;
      this.buldingcon.SetAllColours(0.3f, 0.3f, 0.3f);
      this.Locked = true;
    }

    public void SetPopUpText(string NewString)
    {
      this.objname = new StringInBox(NewString, RenderMath.GetPixelSizeBestMatch(1f), 130f, true);
      this.objname.SetAsButtonFrame(BTNColour.Blue);
      this.objname.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public void LockThis()
    {
      this.objname = new StringInBox("Locked", RenderMath.GetPixelSizeBestMatch(1f), 130f, true);
      this.objname.SetAsButtonFrame(BTNColour.Red);
      this.objname.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.Locked = true;
    }

    public void UnLockThis() => this.Locked = false;

    public bool UpdateBIconAndCost(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      float MinHeight,
      bool TryAndForceSelect)
    {
      Offset += this.Location;
      this.buldingcon.UpdateBuildingIcon(Offset, player);
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset + this.buldingcon.vLocation, this.buldingcon.scale, (float) this.buldingcon.DrawRect.Width, (float) this.buldingcon.DrawRect.Height, player.inputmap.PointerLocation);
      if (!TryAndForceSelect && ((double) player.player.touchinput.ReleaseTapArray[0].X < 0.0 || (double) player.player.touchinput.ReleaseTapArray[0].Y <= (double) MinHeight))
        return false;
      if (!TryAndForceSelect)
        return MathStuff.CheckPointCollision(true, Offset + this.buldingcon.vLocation, this.buldingcon.scale, (float) this.buldingcon.DrawRect.Width, (float) this.buldingcon.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
      TryAndForceSelect = false;
      return true;
    }

    public void DrawBIconAndCost(
      Vector2 Offset,
      bool Selected,
      float MinHeight,
      SpriteBatch spritebatch,
      float ExtraMouseOverffset = 0.0f)
    {
      float num1 = 1f;
      Offset += this.Location;
      float num2 = (float) ((double) this.buldingcon.scale * (double) this.buldingcon.DrawRect.Height * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5);
      int num3 = 0;
      if ((double) this.buldingcon.vLocation.Y + (double) Offset.Y < (double) MinHeight + (double) num2)
      {
        num3 = this.buldingcon.DrawRect.Height - (int) Math.Round((double) ((this.buldingcon.vLocation.Y + Offset.Y - MinHeight + num2) / (this.buldingcon.scale * Sengine.ScreenRatioUpwardsMultiplier.Y)));
        if (num3 > this.buldingcon.DrawRect.Height)
          return;
        this.buldingcon.DrawRect.Height -= num3;
        this.buldingcon.DrawRect.Y += num3;
        this.buldingcon.DrawOrigin.Y -= (float) num3;
        if ((double) num1 <= 0.0)
          return;
      }
      if (this.Locked && !this.Darkened)
      {
        BIconAndCost.Lockedbuldingcon.vLocation = this.buldingcon.vLocation;
        BIconAndCost.Lockedbuldingcon.DrawBuildingIcon(Offset, num1, spritebatch);
      }
      else
      {
        this.buldingcon.DrawBuildingIcon(Offset, num1, spritebatch);
        if (TutorialManager.currenttutorial == TUTORIALTYPE.RevealCashAndBuild && this.tiletype == TILETYPE.LifeSupport && !Selected)
        {
          this.SelectedThing.SetAllColours(ColourData.YellowHighlight);
          this.SelectedThing.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, FlashingAlpha.Fast.fAlpha * 0.3f * num1);
          this.SelectedThing.SetAllColours(Vector3.One);
        }
      }
      if (this.DrawCost)
        this.Cost.DrawBuldCost(Offset + new Vector2(0.0f, 22f * Sengine.ScreenRatioUpwardsMultiplier.Y), spritebatch, num1);
      if (Selected)
        this.buldingcon.DrawBuildingIcon(Offset, 0.6f, AssetContainer.PointBlendBatch04);
      if (this.MouseOver)
        this.objname.DrawStringInBox(this.buldingcon.vLocation + Offset + new Vector2(ExtraMouseOverffset, -30f), spritebatch, num1);
      if (num3 == 0)
        return;
      this.buldingcon.DrawRect.Height += num3;
      this.buldingcon.DrawRect.Y -= num3;
      this.buldingcon.SetDrawOriginToCentre();
    }
  }
}

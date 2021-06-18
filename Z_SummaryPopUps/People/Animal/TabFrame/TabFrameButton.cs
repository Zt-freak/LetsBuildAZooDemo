// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame.TabFrameButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BarInfo.MainBar;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame
{
  internal class TabFrameButton
  {
    private GameObjectNineSlice Frame;
    private Vector2 VSCALE;
    public bool Selected;
    private TabFrameIcon tabframeicon;
    public Vector2 Location;
    public TabType refTabType;
    private float refBaseScale;
    private ShortcutHighlightIcon notificationIcon;
    public bool ForceReturnTruNextUpdate_FORCONTROLLER;
    private RedLight redlight;

    public TabFrameButton(TabType tabType, float XVSCALE, float BaseScale)
    {
      this.refTabType = tabType;
      this.refBaseScale = BaseScale;
      this.Frame = new GameObjectNineSlice(new Rectangle(948, 550, 21, 21), 7);
      if ((double) BaseScale == -1.0)
        this.Frame.scale = RenderMath.GetPixelSizeBestMatch(2f);
      else
        this.Frame.scale = BaseScale;
      this.tabframeicon = new TabFrameIcon(tabType, BaseScale);
      this.VSCALE = new Vector2(XVSCALE, XVSCALE);
      this.Location.Y = (float) (-(double) this.VSCALE.Y * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Location.Y += (float) ((double) this.Frame.scale * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 2.0);
    }

    public Vector2 GetSize() => this.VSCALE * Sengine.ScreenRatioUpwardsMultiplier.Y;

    public void AddRedLight(OffscreenPointerType pointerType, bool RemoveInstead = false)
    {
      if (RemoveInstead)
      {
        this.redlight = (RedLight) null;
      }
      else
      {
        this.redlight = new RedLight(true, BaseScale: (this.refBaseScale * 0.5f));
        this.redlight.vLocation.X = this.VSCALE.X * -0.5f;
        this.redlight.vLocation.Y -= this.VSCALE.Y * 0.5f;
        this.redlight.vLocation.X += this.refBaseScale * 8f;
        this.redlight.vLocation.Y += this.refBaseScale * 8f;
      }
    }

    public void AddNotificationIcon(OffscreenPointerType pointerType, bool RemoveInstead = false)
    {
      if (RemoveInstead)
      {
        this.notificationIcon = (ShortcutHighlightIcon) null;
      }
      else
      {
        this.notificationIcon = new ShortcutHighlightIcon(pointerType, this.refBaseScale);
        this.notificationIcon.vLocation.X += this.VSCALE.X * 0.5f;
        this.notificationIcon.vLocation.Y -= this.VSCALE.Y * 0.5f;
        this.notificationIcon.vLocation.X -= this.notificationIcon.GetSize().X * 0.5f;
      }
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return MathStuff.CheckPointCollision(true, offset + this.Frame.vLocation, 1f, this.VSCALE.X, this.VSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
    }

    public bool UpdateTabFrameButton(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.ForceReturnTruNextUpdate_FORCONTROLLER)
        return MathStuff.CheckPointCollision(true, Offset + this.Frame.vLocation, 1f, this.VSCALE.X, this.VSCALE.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
      this.ForceReturnTruNextUpdate_FORCONTROLLER = false;
      return true;
    }

    public void PrerawTabFrameButton(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      if (this.Selected)
        return;
      this.DrawThisThing(spritebatch, Offset);
    }

    public void PostDrawTabFrameButton(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.Selected)
        return;
      this.DrawThisThing(spritebatch, Offset);
      Offset.Y -= 6f;
      this.DrawThisThing(spritebatch, Offset);
    }

    private void DrawThisThing(SpriteBatch spritebatch, Vector2 Offset)
    {
      this.Frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCALE * Sengine.ScreenRatioUpwardsMultiplier);
      this.tabframeicon.DrawTabFrameIcon(this.Frame.vLocation + Offset, spritebatch);
      if (this.notificationIcon != null)
        this.notificationIcon.DrawShortcutHighlightIcon(spritebatch, Offset, 0.0f);
      if (this.redlight == null || !FeatureFlags.FlashHireStaffFromShop)
        return;
      this.redlight.DrawRedLight(spritebatch, spritebatch, Offset, true);
    }
  }
}

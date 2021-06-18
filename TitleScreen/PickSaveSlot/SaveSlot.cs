// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSlot
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.Z_Save.Header;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.TitleScreen.PickSaveSlot
{
  internal class SaveSlot
  {
    public TScreenFrame tscreenframe;
    public HeaderInfo headerinf;
    private string TopString;
    private string CenterString;
    private string DateString;
    private GameObject TopText;
    private GameObject MidText;
    public Vector2 Location;
    private GameObject Plus;
    private AnimalRenderer zookeeper;
    private StarBarStar star;
    private Vector2 ProgressOffset;
    private bool MouseOver;
    private TScreenFrame tscreenframeMouseOver;

    public SaveSlot(float BaseScale, int SaveSlot)
    {
      this.TopString = "Slot " + (object) (SaveSlot + 1);
      this.tscreenframe = new TScreenFrame(BaseScale);
      this.tscreenframe.VScale = new Vector2(120f * BaseScale, 120f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.tscreenframeMouseOver = new TScreenFrame(BaseScale, true);
      this.tscreenframeMouseOver.VScale = this.tscreenframe.VScale;
      this.headerinf = Z_SaveUtils.GetHeaderInfo(SaveSlot);
      if (this.headerinf == null)
      {
        this.Plus = new GameObject();
        this.Plus.DrawRect = new Rectangle(895, 372, 22, 22);
        this.Plus.SetDrawOriginToCentre();
        this.Plus.scale = BaseScale * 2f;
        this.Plus.SetAllColours(0.0f, 0.0f, 0.0f);
        this.Plus.SetAlpha(0.7f);
        this.DateString = "Start new game";
      }
      else
      {
        this.CenterString = Math.Round((double) this.headerinf.PercentComplete, 1).ToString() + "%";
        bool Crashed;
        this.DateString = DateTimeToString.GstDateToString(this.headerinf.LastSave, DateTimeDisplay.June_15_hour_min_PM, out Crashed);
        if (Crashed)
          this.DateString = " ";
        this.zookeeper = new AnimalRenderer(AnimalType.KeeperAsian);
        this.zookeeper.enemy.scale = BaseScale * 2f;
        this.zookeeper.enemy.vLocation = Vector2.Zero;
        this.zookeeper.enemy.vLocation.Y = 5f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.DateString = "26 June, 2:30 PM";
        this.star = new StarBarStar(1f, BaseScale);
        this.star.vLocation = new Vector2(40f * BaseScale, -40f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      }
      this.TopText = new GameObject();
      this.TopText.scale = BaseScale;
      this.TopText.SetAllColours(0.0f, 0.0f, 0.0f);
      this.TopText.vLocation.Y = -47f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.MidText = new GameObject();
      this.MidText.scale = BaseScale * 1f;
      this.MidText.vLocation.Y = BaseScale * 30f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.MidText.SetAllColours(0.0f, 0.0f, 0.0f);
      this.ProgressOffset.Y = -15f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public bool UpdateSaveSlot(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      this.MouseOver = this.tscreenframe.CheckForCollision(Offset, player.inputmap.PointerLocation);
      return this.MouseOver && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
    }

    public void DrawSaveSlot(Vector2 Offset)
    {
      Offset += this.Location;
      this.tscreenframe.DrawTScreenFrame(Offset);
      this.CenterString = "18.7%";
      TextFunctions.DrawJustifiedText(this.TopString, this.TopText.scale, this.TopText.vLocation + Offset, this.TopText.GetColour(), this.TopText.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatch03);
      if (this.Plus != null)
      {
        this.Plus.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset);
      }
      else
      {
        if (this.zookeeper != null)
          this.zookeeper.ScreenSpaceDraw(Offset, AssetContainer.pointspritebatch03, false);
        TextFunctions.DrawJustifiedText("Progress", this.TopText.scale, this.MidText.vLocation + Offset + this.ProgressOffset, this.TopText.GetColour(), this.TopText.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatch03);
        TextFunctions.DrawJustifiedText(this.CenterString, this.MidText.scale, this.MidText.vLocation + Offset, this.MidText.GetColour(), this.MidText.fAlpha, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03);
      }
      TextFunctions.DrawJustifiedText(this.DateString, this.TopText.scale, this.TopText.vLocation * -1f + Offset, this.TopText.GetColour(), this.TopText.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatch03);
      if (this.star != null)
        this.star.DrawStar(AssetContainer.pointspritebatch03, Offset);
      if (!this.MouseOver)
        return;
      this.tscreenframeMouseOver.DrawTScreenFrame(Offset);
    }
  }
}

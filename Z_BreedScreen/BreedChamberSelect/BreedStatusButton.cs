// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChamberSelect.BreedStatusButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.Z_BreedResult;
using TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay;

namespace TinyZoo.Z_BreedScreen.BreedChamberSelect
{
  internal class BreedStatusButton : GameObject
  {
    private ThisHutchStatus thishutchstatus;
    private GameObjectNineSlice FrameForWHoleThing;
    private Vector2 VSCale;
    private Vector2 Location;
    private bool MouseOver;
    private StringInBox header;
    private AnimalFrame_AndTimer animalandtimer;
    private ActiveBreedSummary activebreed;
    private WatchVideoButton SpeedUp;
    private TextButton Finish;
    private TextButton GetExtraSlot;
    private TextButton GoBreed;
    private TextButton Reveal;
    private bool IsServerError;

    public BreedStatusButton(int Index, Player player)
    {
      Vector3 SecondaryColour;
      this.FrameForWHoleThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.FrameForWHoleThing.scale = 2f;
      this.VSCale = new Vector2(220f, 450f);
      this.Location = new Vector2((float) (256 + Index * 266), 400f);
      string Text = "EMPTY";
      if (player.breeds.IsBreeding(Index))
      {
        this.activebreed = new ActiveBreedSummary(player, Index);
        this.animalandtimer = new AnimalFrame_AndTimer(player, Index);
        if (!DebugFlags.IsPCVersion)
        {
          this.SpeedUp = new WatchVideoButton("Speed Up");
          this.SpeedUp.vLocation.Y = 150f;
          this.Finish = new TextButton("FINISH", 60f);
          this.Finish.vLocation.Y = 200f;
        }
        Text = "HUTCH " + (object) (Index + 1);
        this.thishutchstatus = ThisHutchStatus.Breeding;
      }
      else if (Index >= player.breeds.GetTotalSlots())
      {
        Text = "LOCKED";
        this.FrameForWHoleThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Grey, out SecondaryColour), 7);
        this.FrameForWHoleThing.scale = 2f;
        this.thishutchstatus = ThisHutchStatus.Locked;
        this.GetExtraSlot = new TextButton("UNLOCK", 60f);
        this.GetExtraSlot.SetAsBuyButton(1000, player);
      }
      else
      {
        this.thishutchstatus = ThisHutchStatus.Empty;
        this.GoBreed = new TextButton("BREED", 60f);
        this.GoBreed.SetButtonBlue();
      }
      this.header = new StringInBox(Text, 4f, 40f, true, true);
      this.header.SetAsButtonFrame(BTNColour.PaleYellow);
    }

    public bool UpdateBreedStatusButton(Player player, float DeltaTime, Vector2 Offset)
    {
      if ((double) Offset.X == 0.0 && (double) player.player.touchinput.MultiTouchTapArray[0].X > 0.0 && (this.thishutchstatus != ThisHutchStatus.Breeding && MathStuff.CheckPointCollision(true, this.Location, 1f, this.VSCale.X, this.VSCale.Y, player.player.touchinput.MultiTouchTapArray[0])) && this.thishutchstatus != ThisHutchStatus.Locked)
        return true;
      if (this.activebreed != null)
      {
        this.activebreed.UpdateActiveBreedSummary();
        bool IsComplete;
        bool ServerError;
        this.animalandtimer.UpdateAnimalFrame_AndTimer(player, out IsComplete, out ServerError);
        if (IsComplete | ServerError && this.Reveal == null)
        {
          this.SpeedUp = (WatchVideoButton) null;
          this.Finish = (TextButton) null;
          this.Reveal = new TextButton("GET!", 60f);
          this.Reveal.vLocation.Y = 150f;
        }
        if (this.SpeedUp != null)
        {
          this.SpeedUp.UpdateWatchVideoButton(player, Offset + this.Location);
          this.Finish.UpdateTextButton(player, Offset + this.Location, DeltaTime);
        }
        AnimalType animal;
        int ChildSkin;
        bool ABOY;
        bool WasNew;
        if (this.Reveal != null && this.Reveal.UpdateTextButton(player, Offset + this.Location, DeltaTime) && this.animalandtimer.Consume(out animal, out ChildSkin, player, out ABOY, out WasNew))
        {
          BreedResultManager.newthingget = new newThingRenderer(animal, ABOY, Typeskn: ChildSkin, _WasNew: WasNew);
          return false;
        }
      }
      if (this.thishutchstatus == ThisHutchStatus.Locked)
        this.GetExtraSlot.UpdateTextButton(player, Offset + this.Location, DeltaTime);
      else if (this.thishutchstatus == ThisHutchStatus.Empty)
        this.GoBreed.UpdateTextButton(player, Offset + this.Location, DeltaTime);
      this.MouseOver = MathStuff.CheckPointCollision(true, this.Location, 1f, this.VSCale.X, this.VSCale.Y, player.inputmap.PointerLocation);
      return false;
    }

    public void DrawBreedStatusButton(Vector2 Offset)
    {
      if (this.MouseOver || this.thishutchstatus == ThisHutchStatus.Breeding)
        this.FrameForWHoleThing.fAlpha = 1f;
      else
        this.FrameForWHoleThing.fAlpha = 0.5f;
      this.MouseOver = false;
      this.FrameForWHoleThing.fAlpha = 1f;
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location + Offset, this.VSCale);
      this.header.DrawStringInBox(this.Location + Offset + new Vector2(0.0f, this.VSCale.Y * -0.5f), AssetContainer.pointspritebatch03);
      if (this.activebreed != null)
      {
        this.activebreed.DrawActiveBreedSummary(this.Location + Offset);
        this.animalandtimer.DrawAnimalFrame_AndTimer(this.Location + Offset);
        if (this.SpeedUp != null)
          this.SpeedUp.DrawWatchVideoButton(this.Location + Offset);
        if (this.Finish != null)
        {
          this.Finish.SetButtonBlue();
          this.Finish.DrawTextButton(this.Location + Offset, 1f, AssetContainer.pointspritebatch03);
        }
      }
      if (this.thishutchstatus == ThisHutchStatus.Locked)
        this.GetExtraSlot.DrawTextButton(this.Location + Offset, 1f, AssetContainer.pointspritebatch03);
      else if (this.thishutchstatus == ThisHutchStatus.Empty)
        this.GoBreed.DrawTextButton(this.Location + Offset, 1f, AssetContainer.pointspritebatch03);
      if (this.Reveal == null)
        return;
      this.Reveal.DrawTextButton(this.Location + Offset, 1f, AssetContainer.pointspritebatch03);
    }
  }
}

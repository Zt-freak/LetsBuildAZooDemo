// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.AnimalTradeView
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Tutorials;
using TinyZoo.Z_BreedScreen.BreedPairing;
using TinyZoo.Z_Quests;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame;

namespace TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade
{
  internal class AnimalTradeView
  {
    private bool Exiting;
    private BackButton close;
    private GameObjectNineSlice gameobjectninslice;
    private GameObjectNineSlice box;
    private LerpHandler_Float lerper;
    private SexAnimalRow animalRow;
    private SexAnimalRow animalRowBoy;
    private BlackOut blackout;
    private SmartCharacterBox charactertextbox;
    private TextButton info;
    private bool ViewingAll;
    private AnimalTradeGrid aialtradegrid;
    private TextButton ConfirmTrade;
    private SimpleTextBox tbox;

    public AnimalTradeView(Player player, QuestPack Ref_Zquest)
    {
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.5f, 0.0f, 0.7f);
      this.gameobjectninslice = new GameObjectNineSlice(new Rectangle(961, 372, 21, 21), 7);
      this.gameobjectninslice.scale = 2f;
      this.gameobjectninslice.vLocation = new Vector2(512f, 384f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.close = new BackButton(true);
      this.box = new GameObjectNineSlice(new Rectangle(992, 434, 21, 21), 7);
      this.box.scale = 2f;
      this.animalRow = new SexAnimalRow(Ref_Zquest.trades_ListOnlyOne[0].animal, true, player, true);
      this.animalRowBoy = new SexAnimalRow(Ref_Zquest.trades_ListOnlyOne[0].animal, false, player, true);
      this.animalRow.Location = new Vector2(0.0f, 300f);
      this.animalRowBoy.Location = new Vector2(0.0f, 500f);
      this.info = new TextButton("View All", 100f);
      bool StillHasBreedingPair;
      int BoysUsed;
      bool HasTheseAnimals = player.prisonlayout.HasTheseAnimals(Ref_Zquest, out StillHasBreedingPair, out BoysUsed);
      int getThisAnimal = (int) Ref_Zquest.GetThisAnimal;
      string FirstText = "You dont have enough animals to trade, try breeding more!";
      if (!StillHasBreedingPair)
      {
        if (HasTheseAnimals)
          FirstText = "You have these animals to trade, but you can't trade your last breeding pair, go and breed some more!";
      }
      else if (HasTheseAnimals)
        FirstText = "Great! you have exactly what they are looking for.~~Trade these animals and get something new now!";
      this.charactertextbox = new SmartCharacterBox(FirstText, AnimalType.Administrator, ShortenForCloseButton: true);
      this.aialtradegrid = new AnimalTradeGrid(player, HasTheseAnimals, Ref_Zquest, StillHasBreedingPair, BoysUsed);
      if (HasTheseAnimals & StillHasBreedingPair)
      {
        this.ConfirmTrade = new TextButton("Confirm Trade", 120f);
        this.ConfirmTrade.vLocation = new Vector2(512f, 700f);
      }
      else
      {
        this.tbox = !HasTheseAnimals || StillHasBreedingPair ? new SimpleTextBox("You dont have enough animals to fulfill the trade", 700f, false, GameFlags.GetSmallTextScale()) : new SimpleTextBox("You cannot trade away your last breeding pair", 700f, false, GameFlags.GetSmallTextScale());
        this.tbox.FrameForWHoleThing.SetAllColours(1f, 0.5f, 0.5f);
      }
    }

    public bool UpdateAnimalTradeView(Player player, float DeltaTime, out bool WillDoClaim)
    {
      WillDoClaim = false;
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      this.blackout.UpdateColours(DeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (!this.ViewingAll)
      {
        this.aialtradegrid.UpdateAnimalTradeGrid(DeltaTime, player);
        if (this.info.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          this.ViewingAll = true;
      }
      if (this.ViewingAll)
      {
        this.animalRow.UpdateSexAnimalRow(player, Vector2.Zero, DeltaTime);
        this.animalRowBoy.UpdateSexAnimalRow(player, Vector2.Zero, DeltaTime);
      }
      if ((double) this.lerper.Value == 0.0)
      {
        if (this.close.UpdateBackButton(player, DeltaTime) && !this.Exiting)
        {
          this.Exiting = true;
          this.blackout.SetAlpha(true, 0.3f, 1f, 0.0f);
          this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
        }
        if (this.ConfirmTrade != null && !this.Exiting && this.ConfirmTrade.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        {
          WillDoClaim = true;
          this.Exiting = true;
          this.blackout.SetAlpha(true, 0.3f, 1f, 0.0f);
          this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
        }
      }
      return this.Exiting && (double) this.lerper.Value == 1.0;
    }

    public void DrawAnimalTradeView()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.box.vLocation = new Vector2(512f, 384f);
      this.gameobjectninslice.vLocation = Vector2.Zero;
      this.box.scale = 3f;
      this.charactertextbox.DrawSmartCharacterBox(Offset);
      if (this.ViewingAll)
      {
        this.animalRow.DrawSexAnimalRow(Offset, AssetContainer.pointspritebatchTop05);
        this.animalRowBoy.DrawSexAnimalRow(Offset, AssetContainer.pointspritebatchTop05);
      }
      else
        this.aialtradegrid.DrawAnimalTradeGrid(Offset);
      if (this.ConfirmTrade != null)
        this.ConfirmTrade.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      if (this.tbox != null)
      {
        float y = 700f;
        if (DebugFlags.IsPCVersion)
          y = 650f;
        this.tbox.Location = new Vector2(512f, y);
        this.tbox.DrawSimpleTextBox(Offset);
      }
      this.close.DrawBackButton(Offset);
    }
  }
}

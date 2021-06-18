// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Transfer.TransferScreen.reanimate.ReanimatonManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.AdvertPlayer;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Intake.InmateSummary;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.Transfer.TransferScreen.reanimate
{
  internal class ReanimatonManager
  {
    private StoreBGManager storeBG;
    private LerpHandler_Float Lerper;
    private SmartCharacterBox charactertextbox;
    private TextButton WatchAdvert;
    private BackButton backbutton;
    private HCAdvertPlayer advertplayer;
    private bool TransferComplete;
    private Vector2Int Location;
    private prisonerIcon prisonericon;
    private DeadPerson deadperson;
    private newThingRenderer newthingrenderer;
    public bool DidTransfer;

    public ReanimatonManager(Player player, Vector2Int _Location)
    {
      this.DidTransfer = false;
      this.Location = new Vector2Int(_Location);
      this.TransferComplete = false;
      FeatureFlags.DemolishEnabled = false;
      FeatureFlags.BlockStats = true;
      FeatureFlags.BlockCash = true;
      FeatureFlags.BlockTimer = true;
      this.storeBG = new StoreBGManager();
      this.storeBG.SetSpecialRed();
      this.Lerper = new LerpHandler_Float();
      this.Lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.backbutton = new BackButton();
      this.WatchAdvert = new TextButton("Watch Ad", 100f);
      this.WatchAdvert.vLocation = new Vector2(850f, 700f);
      string FirstText = "Our sponsors can invest in reviving this prisoner!~However in order to do so, they need to cover their expenses with some advertising.";
      if (player.Stats.ADisabled(false, player))
      {
        this.WatchAdvert.SetText("Revive");
        FirstText = "We can use our technology to revive this prisoner so that they may serve the rest of their sentence.";
      }
      this.charactertextbox = new SmartCharacterBox(FirstText, AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.deadperson = player.prisonlayout.GetThisDeadPerson(_Location);
      this.prisonericon = new prisonerIcon(this.deadperson.intakeperson);
      this.newthingrenderer = new newThingRenderer(this.deadperson.intakeperson.animaltype);
    }

    public bool UpdateReanimatonManager(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      if (this.advertplayer != null)
      {
        this.advertplayer.UpdateAdvertPlayer(DeltaTime);
        if (!this.advertplayer.IsWaiting)
        {
          if (this.advertplayer.WasSuccess)
          {
            this.advertplayer = (HCAdvertPlayer) null;
            this.DoTransfer(player, wallsandfloors, true);
          }
          else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0])
          {
            this.advertplayer = (HCAdvertPlayer) null;
            this.charactertextbox = new SmartCharacterBox("Our sponsors seem to be letting us down, maybe try again later", AnimalType.Administrator);
          }
        }
        else if (this.advertplayer.WasTimeout && ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0]))
          this.advertplayer = (HCAdvertPlayer) null;
        return false;
      }
      this.newthingrenderer.UpdatenewThingRenderer(DeltaTime, player);
      Vector2 Offset = new Vector2(this.Lerper.Value * 1024f, 0.0f);
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      this.Lerper.UpdateLerpHandler(DeltaTime);
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (!this.TransferComplete && this.WatchAdvert.UpdateTextButton(player, Offset, DeltaTime))
      {
        if (player.Stats.ADisabled(false, player))
          this.DoTransfer(player, wallsandfloors, false);
        else
          this.advertplayer = new HCAdvertPlayer(false, player);
      }
      if (this.backbutton.UpdateBackButton(player, DeltaTime))
        return true;
      player.inputmap.ClearAllInput(player);
      return false;
    }

    private void DoTransfer(
      Player player,
      WallsAndFloorsManager wallsandfloors,
      bool WatchedAdvert)
    {
      this.DidTransfer = true;
      this.TransferComplete = true;
      this.charactertextbox = new SmartCharacterBox("The prisoner has been transfered to a holding cell.", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      player.prisonlayout.cellblockcontainer.RemoveThisDeadPerson(this.deadperson);
      player.prisonlayout.cellblockcontainer.TryToAddPrisonersToHoldingCells(new PrisonerInfo(this.deadperson.intakeperson, false, Vector2.Zero, CellBlockType.HoldingCell), player);
      wallsandfloors.VallidateGraveYardAndApplyToLayout(player);
      wallsandfloors.RemakeTileList();
      if (WatchedAdvert)
        player.tracking.WatchedAdvert(AdvertLocation.RevivePrisoner, player);
      player.tracking.RevivedAnAlien();
      player.OldSaveThisPlayer();
    }

    public void DrawReanimatonManager()
    {
      Vector2 Offset = new Vector2(this.Lerper.Value * 1024f, 0.0f);
      this.storeBG.DrawStoreBGManager(Offset);
      this.charactertextbox.DrawSmartCharacterBox(Offset);
      if (!this.TransferComplete)
        this.WatchAdvert.DrawTextButton(Offset);
      this.newthingrenderer.DrawnewThingRenderer(!this.TransferComplete);
      this.backbutton.DrawBackButton(Offset);
      if (this.advertplayer == null)
        return;
      this.advertplayer.DrawAdvertPlayer();
    }
  }
}

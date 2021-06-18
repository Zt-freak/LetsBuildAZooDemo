// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.NewDiscoveryScreen.NewDiscovery
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Research;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.NewDiscoveryScreen
{
  internal class NewDiscovery
  {
    private StoreBGManager storeBG;
    private CharacterTextBox charactertextbox;
    private newThingRenderer newthingrenderer;
    private StringInBox Habitatstring;
    private float Timer;
    private TextButton reset;
    private bool GoBackToEventSCreen;

    public NewDiscovery(Player player)
    {
      this.storeBG = new StoreBGManager(true);
      this.storeBG.SetSpecial();
      this.GoBackToEventSCreen = false;
      if (player.livestats.eventdone)
      {
        player.livestats.eventdone = false;
        if (player.livestats.EVWasMoney)
          player.livestats.EVWasMoney = false;
        else
          this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, string.Format("You located {0}.", (object) EnemyData.GetEnemyTypeName(player.livestats.eventenemy)));
        this.GoBackToEventSCreen = true;
        this.newthingrenderer = new newThingRenderer(player.livestats.eventenemy);
        player.OldSaveThisPlayer();
      }
      else
      {
        switch (player.Stats.research.reasearchingThis)
        {
          case ResearchType.Building:
            bool ServerTimeError1;
            bool AlreadyClaimed1;
            TILETYPE unlockBuilding = player.Stats.research.TryToUnlockBuilding(player, out ServerTimeError1, out AlreadyClaimed1);
            if (AlreadyClaimed1)
            {
              this.SetAlreadyClaimed();
              break;
            }
            if (ServerTimeError1)
            {
              this.SetServerError();
              break;
            }
            if (unlockBuilding == TILETYPE.Count)
            {
              this.SetQuantumEntanglementError();
              break;
            }
            string TextToSay = string.Format("You can now build the {0}.", (object) TileData.GetTileStats(unlockBuilding).Name);
            if (unlockBuilding == TILETYPE.Water)
              TextToSay = "The water storage will be essential for the welfare of some aliens, start building this when you catch the aliens that need it";
            this.newthingrenderer = new newThingRenderer(unlockBuilding);
            this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, TextToSay);
            player.OldSaveThisPlayer();
            break;
          case ResearchType.Alien:
            bool ServerTimeError2;
            bool AlreadyClaimed2;
            AnimalType unlockAlien = player.Stats.research.TryToUnlockAlien(player, out ServerTimeError2, out AlreadyClaimed2);
            if (AlreadyClaimed2)
            {
              this.SetAlreadyClaimed();
              break;
            }
            if (ServerTimeError2)
            {
              this.SetServerError();
              break;
            }
            if (unlockAlien == AnimalType.None)
            {
              this.SetQuantumEntanglementError();
              break;
            }
            this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, string.Format("You have discovered a new alien race: {0}.~Look out for them in the new prisoners area.", (object) EnemyData.GetEnemyTypeName(unlockAlien)));
            this.newthingrenderer = new newThingRenderer(unlockAlien);
            this.Habitatstring = new StringInBox("Ideal Habitat: " + EnemyData.getHabitatString(unlockAlien), 3f, 200f, true);
            this.Habitatstring.vLocation = new Vector2(512f, 730f);
            this.Habitatstring.SetLemonANdBlue();
            player.OldSaveThisPlayer();
            break;
          case ResearchType.CellType:
            bool ServerTimeError3;
            bool AlreadyClaimed3;
            TILETYPE unlockCellBlock = player.Stats.research.TryToUnlockCellBlock(player, out ServerTimeError3, out AlreadyClaimed3);
            if (AlreadyClaimed3)
            {
              this.SetAlreadyClaimed();
              break;
            }
            if (ServerTimeError3)
            {
              this.SetServerError();
              break;
            }
            if (unlockCellBlock == TILETYPE.Count)
            {
              this.SetQuantumEntanglementError();
              break;
            }
            this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, string.Format("You can now build {0}.", (object) TileData.GetTileStats(unlockCellBlock).Name));
            this.newthingrenderer = new newThingRenderer(unlockCellBlock);
            player.OldSaveThisPlayer();
            break;
        }
      }
    }

    private void SetAlreadyClaimed()
    {
      this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "Unexpectedly, this research is a duplicate! Dear user of this game, please avoid trying to hack our games.");
      this.storeBG.SetSpecialRed();
    }

    private void SetServerError()
    {
      this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "To download the research results, you need to be connected to the internet. Your research scientists demand the internet so they can goof off during work hours!");
      this.storeBG.SetSpecialRed();
    }

    private void SetQuantumEntanglementError()
    {
      this.reset = new TextButton(SEngine.Localization.Localization.GetText(86), 50f);
      this.reset.SetButtonRed();
      this.reset.vLocation = new Vector2(750f, 700f);
      this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "There was a quantum entanglement issue, please try again later.");
    }

    public void UpdateNewDiscovery(float DeltaTime, Player player)
    {
      if (this.reset != null && this.reset.UpdateTextButton(player, Vector2.Zero, DeltaTime))
      {
        if ((double) this.Timer > 0.5)
        {
          player.Stats.research.CancelResearch();
          Game1.SetNextGameState(GAMESTATE.OverWorld);
          Game1.screenfade.BeginFade(true);
        }
        player.inputmap.ClearAllInput(player);
      }
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (this.newthingrenderer != null)
        this.newthingrenderer.UpdatenewThingRenderer(DeltaTime, player);
      if (this.charactertextbox != null)
        this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
      this.Timer += DeltaTime;
      if ((double) this.Timer <= 2.0 || (double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 && !player.inputmap.PressedBackOnController() && !player.inputmap.PressedThisFrame[0])
        return;
      if (this.GoBackToEventSCreen)
      {
        if ((double) Game1.screenfade.fAlpha != 0.0)
          return;
        Game1.SetNextGameState(GAMESTATE.EventViewSetUp);
        Game1.screenfade.BeginFade(true);
      }
      else
      {
        if (Game1.GetNextGameState() == GAMESTATE.OverWorld)
          return;
        Game1.SetNextGameState(GAMESTATE.OverWorld);
        Game1.screenfade.BeginFade(true);
      }
    }

    public void DrawNewDiscovery()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      if (this.newthingrenderer != null)
        this.newthingrenderer.DrawnewThingRenderer(false);
      if (this.Habitatstring != null)
        this.Habitatstring.DrawStringInBox(Vector2.Zero, 0.8f);
      if (this.charactertextbox != null)
        this.charactertextbox.DrawCharacterTextBox(new Vector2(512f, 100f), AssetContainer.pointspritebatchTop05);
      if (this.reset == null)
        return;
      this.reset.DrawTextButton(Vector2.Zero);
    }
  }
}

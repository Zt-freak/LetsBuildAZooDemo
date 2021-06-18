// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedSystemManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_BreedScreen.BreedChamberSelect;
using TinyZoo.Z_BreedScreen.BreedPairing;
using TinyZoo.Z_BreedScreen.PetSelect;

namespace TinyZoo.Z_BreedScreen
{
  internal class BreedSystemManager
  {
    private ChamberSelectManager chamberselect;
    public MATESTATE gamestate;
    private PetSelectManager petselectmanager;
    private BreedPairManager breedpaid;
    private int SelectedChamber;

    public BreedSystemManager(Player player) => this.chamberselect = new ChamberSelectManager(player);

    public bool UpdateBreedSystemManager(Vector2 Offset, Player player, float DeltaTime)
    {
      bool flag = false;
      if (this.gamestate == MATESTATE.Chamber)
      {
        this.SelectedChamber = this.chamberselect.UpdateChamberSelectManager(Offset, player, DeltaTime);
        if (this.SelectedChamber > -1)
        {
          flag = true;
          this.petselectmanager = new PetSelectManager(player);
          this.gamestate = MATESTATE.PetSelect;
          GameStateManager.tutorialmanager.ActivatedPetSelectManager();
        }
      }
      if (this.gamestate == MATESTATE.PetSelect && this.petselectmanager.UpdateSelectManager(DeltaTime, player))
      {
        flag = true;
        this.gamestate = MATESTATE.Pairing;
        this.breedpaid = new BreedPairManager(player, this.petselectmanager.SelectedAnimal, this.SelectedChamber);
        GameStateManager.tutorialmanager.GoToBreedPairingView();
      }
      if (this.gamestate == MATESTATE.Pairing && this.breedpaid.UpdateBreedPairManager(player, Offset, DeltaTime))
      {
        this.chamberselect = new ChamberSelectManager(player);
        flag = true;
        this.gamestate = MATESTATE.Chamber;
        GameStateManager.tutorialmanager.BreedButtonPressed();
      }
      return flag;
    }

    public void DrawBreedSystemManager(Vector2 Offset)
    {
      if (this.gamestate == MATESTATE.Chamber)
        this.chamberselect.DrawChamberSelectManager(Offset);
      if (this.gamestate == MATESTATE.PetSelect)
        this.petselectmanager.DrawPetSelectManager(Offset);
      if (this.gamestate != MATESTATE.Pairing)
        return;
      this.breedpaid.DrawBreedPairManager(Offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.PlayerDir.Breeding;
using TinyZoo.Z_BreedScreen.AbortConfirm;
using TinyZoo.Z_BreedScreen.ActiveBreedPairManage;
using TinyZoo.Z_BreedScreen.ConfirmBreed;
using TinyZoo.Z_BreedScreen.SelectBreedingPair;
using TinyZoo.Z_BreedScreen.SelectNewBreed;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;

namespace TinyZoo.Z_BreedScreen
{
  internal class BreedPopUp
  {
    private AllChambers allchambers;
    private NewBreedSelectManager newbreed;
    private SelectBreedingPairManager selectedbreedingpauir;
    private ConfirmBreedmanager confirmbreed;
    private BreedingBuilding REF_building;
    private ActivePairManage manageactivepair;
    private AbortConfirmationManager abortconfirm;
    public float XsideMargin = 50f;
    public static float LerpDistance;
    private float BaseScale;

    public BreedPopUp(BreedingBuilding building, Player player)
    {
      this.REF_building = building;
      BreedPopUp.LerpDistance = 768f;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.CreateMainPanels(player);
    }

    private void CreateMainPanels(Player player)
    {
      this.allchambers = new AllChambers(this.REF_building, player, BaseScale: this.BaseScale);
      this.allchambers.location = new Vector2((float) (1024.0 - (double) this.XsideMargin - (double) this.allchambers.mainPanel.vScale.X * 0.5), 384f);
    }

    public bool CheckMouseOver(Player player)
    {
      bool flag = false;
      if (this.confirmbreed != null)
        flag |= this.confirmbreed.CheckMouseOver(player, Vector2.Zero);
      if (this.selectedbreedingpauir != null)
        flag |= this.selectedbreedingpauir.CheckMouseOver(player, Vector2.Zero);
      if (this.newbreed != null)
        flag |= this.newbreed.CheckMouseOver(player, Vector2.Zero);
      if (this.manageactivepair != null)
        flag |= this.manageactivepair.CheckMouseOver(player, Vector2.Zero);
      if (this.abortconfirm != null)
        flag |= this.abortconfirm.CheckMouseOver(player, Vector2.Zero);
      if (this.allchambers != null)
        flag |= this.allchambers.CheckMouseOver(player, Vector2.Zero);
      return flag;
    }

    public bool UpdateBreedPopUp(Player player, float DeltaTime)
    {
      bool Cancel = false;
      if (this.confirmbreed != null)
      {
        bool GoBack;
        if (this.confirmbreed.UpdateConfirmBreedmanager(player, DeltaTime, out GoBack))
        {
          player.breeds.AddBreedToNurseryBuilding(this.REF_building.UID, this.confirmbreed.Ref_ParentsAndChild, player);
          this.selectedbreedingpauir.LerpOff();
          this.newbreed.LerpOff();
          this.confirmbreed.LerpOff();
          this.CreateMainPanels(player);
        }
        else if (GoBack)
        {
          Cancel = false;
          this.selectedbreedingpauir.LerpIn();
          this.confirmbreed.LerpOff();
        }
      }
      if (this.selectedbreedingpauir != null)
      {
        Parents_AndChild PandC = this.selectedbreedingpauir.UpdateSelectBreedingPairManager(player, DeltaTime, out Cancel);
        if (PandC != null)
        {
          this.confirmbreed = new ConfirmBreedmanager(PandC, this.BaseScale);
          this.confirmbreed.Location = new Vector2((float) (1024.0 - (double) this.XsideMargin - (double) this.confirmbreed.bigBrownPanel.vScale.X * 0.5), 384f);
          this.selectedbreedingpauir.LerpOff();
        }
        else if (Cancel)
        {
          Cancel = false;
          this.selectedbreedingpauir.LerpOff();
          this.newbreed.LerpIn();
          this.newbreed.ResetSelection();
        }
      }
      if (this.newbreed != null)
      {
        AnimalsForBreedInfo selectedanimal = this.newbreed.UpdateNewBreedSelectManager(player, DeltaTime, out Cancel);
        if (selectedanimal != null)
        {
          this.selectedbreedingpauir = new SelectBreedingPairManager(selectedanimal, this.BaseScale);
          this.selectedbreedingpauir.location = new Vector2((float) (1024.0 - (double) this.XsideMargin - (double) this.selectedbreedingpauir.bigBrownPanel.vScale.X * 0.5), 384f);
          this.newbreed.LerpOff();
        }
        else if (Cancel)
        {
          Cancel = false;
          this.newbreed.LerpOff();
          this.allchambers.LerpIn();
        }
      }
      bool ReturnAnimalsToPen;
      bool SkipNursing;
      bool variablesChanged;
      if (this.manageactivepair != null && this.manageactivepair.UpdateActivePairManage(player, DeltaTime, Vector2.Zero, ref this.abortconfirm, out ReturnAnimalsToPen, out SkipNursing, out variablesChanged))
      {
        this.manageactivepair.LerpOff();
        if (ReturnAnimalsToPen)
        {
          player.breeds.RemoveBreedingPair(player, this.REF_building, this.manageactivepair.REF_parents_and_child);
          this.CreateMainPanels(player);
        }
        else if (SkipNursing)
        {
          this.manageactivepair.REF_parents_and_child.MoveBabyToPen(player, this.manageactivepair.REF_parents_and_child.HeldBaby.intakeperson.UID);
          this.CreateMainPanels(player);
        }
        else if (variablesChanged)
          this.CreateMainPanels(player);
        else
          this.allchambers.LerpIn();
      }
      if (this.abortconfirm != null && this.abortconfirm.UpdateAbortConfirmationManager(DeltaTime, player, Vector2.Zero))
      {
        this.manageactivepair = new ActivePairManage(this.manageactivepair.REF_parents_and_child, (ActiveBreed) null, player, this.BaseScale);
        this.manageactivepair.Location = new Vector2((float) (1024.0 - (double) this.XsideMargin - (double) this.manageactivepair.bigBrownPanel.vScale.X * 0.5), 384f);
      }
      bool GoToManageBreedPair = false;
      int index = this.allchambers.UpdateAllChambers(player, DeltaTime, Vector2.Zero, out Cancel, out GoToManageBreedPair);
      if (index > -1)
      {
        if (GoToManageBreedPair)
        {
          this.manageactivepair = new ActivePairManage(this.allchambers.panels[index].REF_parents_and_child, this.allchambers.panels[index].REF_breed, player, this.BaseScale);
          this.manageactivepair.Location = new Vector2((float) (1024.0 - (double) this.XsideMargin - (double) this.manageactivepair.bigBrownPanel.vScale.X * 0.5), 400f);
        }
        else
        {
          this.newbreed = new NewBreedSelectManager(player, Z_GameFlags.GetBaseScaleForUI());
          this.newbreed.location = new Vector2((float) (1024.0 - (double) this.XsideMargin - (double) this.newbreed.GetSize().X * 0.5), 384f);
        }
        this.allchambers.LerpOff();
      }
      return Cancel;
    }

    public void DrawBreedPopUp()
    {
      this.allchambers.DrawAllChamber(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.newbreed != null)
        this.newbreed.DrawNewBreedSelectManager(AssetContainer.pointspritebatchTop05);
      if (this.selectedbreedingpauir != null)
        this.selectedbreedingpauir.DrawSelectBreedingPairManager(AssetContainer.pointspritebatchTop05);
      if (this.confirmbreed != null)
        this.confirmbreed.DrawConfirmBreedmanager(AssetContainer.pointspritebatchTop05, Vector2.Zero);
      if (this.manageactivepair != null)
        this.manageactivepair.DrawActivePairManage(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.abortconfirm == null)
        return;
      this.abortconfirm.DrawAbortConfirmationManager(AssetContainer.pointspritebatchTop05, Vector2.Zero);
    }
  }
}

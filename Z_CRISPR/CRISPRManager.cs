// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.CRISPRManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedScreen;
using TinyZoo.Z_CRISPR.ManageActive;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_CRISPR
{
  internal class CRISPRManager
  {
    private CRISPRBuilding ref_Building;
    private CRISPRMainPanel mainPanel;
    private CRISPR_SelectSpecies selectSpecies;
    private CRISPR_ManageActivePanel manageActive;
    private float BaseScale;

    public CRISPRManager(CRISPRBuilding crisprBuilding, Player player)
    {
      this.ref_Building = crisprBuilding;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      BreedPopUp.LerpDistance = 1024f;
      this.CreateMainPanels(player);
    }

    public void CreateMainPanels(Player player)
    {
      this.mainPanel = new CRISPRMainPanel(this.ref_Building, player, 4, this.BaseScale);
      this.mainPanel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player)
    {
      bool flag = false;
      if (this.manageActive != null)
        flag |= this.manageActive.CheckMouseOver(player);
      if (this.selectSpecies != null)
        flag |= this.selectSpecies.CheckMouseOver(player);
      if (this.mainPanel != null)
        flag |= this.mainPanel.CheckMouseOver(player);
      return flag;
    }

    public bool UpdateCRISPRManager(Player player, float DeltaTime)
    {
      bool flag = false;
      bool Cancel;
      if (this.manageActive != null)
      {
        bool throwBabyOut;
        bool isSell;
        bool isPutInPen;
        if (this.manageActive.UpdateCRISPR_ManageActivePanel(player, DeltaTime, out Cancel, out throwBabyOut, out isSell, out isPutInPen))
        {
          CrisprActiveBreed breedForThisPanel = this.manageActive.GetBreedForThisPanel();
          if (throwBabyOut)
          {
            player.crisprBreeds.RemoveBreedFromCRISPRBuilding(this.ref_Building.BuildingUID, breedForThisPanel);
            this.manageActive.LerpOff();
            this.CreateMainPanels(player);
            Z_NotificationManager.ScrubCrisprBirths = true;
          }
          else if (isSell)
          {
            player.crisprBreeds.RemoveBreedFromCRISPRBuilding(this.ref_Building.BuildingUID, breedForThisPanel);
            this.manageActive.LerpOff();
            this.CreateMainPanels(player);
            Z_NotificationManager.ScrubCrisprBirths = true;
          }
          else if (isPutInPen)
          {
            player.crisprBreeds.ReleaseBreedToPen(breedForThisPanel, player, this.ref_Building.BuildingUID);
            flag = true;
            Z_NotificationManager.ScrubCrisprBirths = true;
          }
        }
        if (Cancel)
        {
          this.manageActive.LerpOff();
          this.mainPanel.LerpIn();
        }
      }
      if (this.selectSpecies != null)
      {
        AnimalType[] animalTypeArray = this.selectSpecies.UpdateCRISPR_SelectSpecies(player, DeltaTime, Vector2.Zero, out Cancel);
        if (Cancel)
        {
          this.selectSpecies.LerpOff();
          this.mainPanel.LerpIn();
        }
        else if (animalTypeArray != null)
        {
          player.crisprBreeds.AddBreedToCRISPRBuilding(this.ref_Building.BuildingUID, animalTypeArray[0], animalTypeArray[1], this.selectSpecies.refselectedBreedSlot);
          this.selectSpecies.LerpOff();
          this.CreateMainPanels(player);
        }
      }
      bool GoToManage;
      int selectedBreedSlot = this.mainPanel.UpdateCRISPRMainPanel(player, DeltaTime, Vector2.Zero, out Cancel, out GoToManage);
      if (selectedBreedSlot > -1)
      {
        if (GoToManage)
        {
          this.manageActive = new CRISPR_ManageActivePanel(this.ref_Building.crisprBreeds[selectedBreedSlot], this.BaseScale, player);
          this.manageActive.location = new Vector2(512f, 384f);
          this.mainPanel.LerpOff();
        }
        else
        {
          this.selectSpecies = new CRISPR_SelectSpecies(player, this.BaseScale, selectedBreedSlot);
          this.selectSpecies.location = new Vector2(512f, 384f);
          this.mainPanel.LerpOff();
        }
      }
      if (!(Cancel | flag))
        return false;
      this.mainPanel.LerpOff();
      return true;
    }

    public void DrawCRISPRManager()
    {
      this.mainPanel.DrawCRISPRMainPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.selectSpecies != null)
        this.selectSpecies.DrawCRISPR_SelectSpecies(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.manageActive == null)
        return;
      this.manageActive.DrawCRISPR_ManageActivePanel(AssetContainer.pointspritebatchTop05);
    }
  }
}

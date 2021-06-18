// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.AnimalPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal;
using TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat;
using TinyZoo.Z_SummaryPopUps.People.Animal._03FamilyTree;
using TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability;
using TinyZoo.Z_SummaryPopUps.People.Animal._05Info;
using TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats;
using TinyZoo.Z_SummaryPopUps.People.Animal.ExtraPopups.Quarantine;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;
using TinyZoo.Z_SummaryPopUps.People.Animal_Dead;

namespace TinyZoo.Z_SummaryPopUps.People.Animal
{
  internal class AnimalPopUpManager
  {
    private AnimalPopUpControllerMatrix controllermatrix;
    private TabFrameManager tabframemanager;
    internal static float Space = 50f;
    internal static float VerticalBuffer = 10f;
    private AnimalViewTabType selectedtab;
    private PrisonerInfo REF_animal;
    private AnimalTabManager animaltabmanager;
    private HabitatTabManager habitatamanager;
    private LifetimeStatsTabManager lifetimestatstabmanager;
    private FamilyTreeTabManager familytreetabmanager;
    private ProfitabilityTabManager profitabilitymanager;
    private InfoTabManager infotabmanager;
    internal static float TopAreaBuffer = 30f;
    private DeadAnimalPopUp deadanimalpopup;
    private QuarantineInfoPopup quarantineInfoPopUp;
    private LerpHandler_Float lerper;
    private float basescale;
    private Vector2 topCenterLocationForPanel;

    public AnimalPopUpManager(PrisonerInfo anaimal, Player player)
    {
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.REF_animal = anaimal;
      this.topCenterLocationForPanel = new Vector2(850f, 175f);
      this.tabframemanager = new TabFrameManager(this.basescale);
      this.tabframemanager.location = new Vector2(850f, 425f);
      this.selectedtab = AnimalViewTabType.Count;
      if (anaimal.IsDead)
        this.deadanimalpopup = new DeadAnimalPopUp(anaimal, player, this.basescale);
      else
        this.TrySwitchToThisTab(AnimalViewTabType.Animal, player);
      this.controllermatrix = new AnimalPopUpControllerMatrix();
    }

    public bool CheckMouseOver(Player player)
    {
      bool flag = false;
      if (this.deadanimalpopup != null)
        flag |= this.deadanimalpopup.CheckMouseOver(player, Vector2.Zero);
      if (this.quarantineInfoPopUp != null)
        flag |= this.quarantineInfoPopUp.CheckMouseOver(player, Vector2.Zero);
      if (this.tabframemanager != null)
        flag |= this.tabframemanager.CheckMouseOver(player, Vector2.Zero);
      return flag;
    }

    public bool UpdateAnimalPopUpManager(Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      bool PrssedClose = false;
      if (Z_GameFlags.JustExitedWorldMap_CheckAnimalExistsInAnimalPanel && this.deadanimalpopup == null)
      {
        int CellBoockUID;
        player.prisonlayout.GetThisAnimal(this.REF_animal.intakeperson.UID, out CellBoockUID);
        if (player.prisonlayout.GetThisCellBlock(CellBoockUID) == null)
          return true;
      }
      Z_GameFlags.JustExitedWorldMap_CheckAnimalExistsInAnimalPanel = false;
      if (this.deadanimalpopup != null)
      {
        if (this.deadanimalpopup.UpdateDeadAnimalPopUp(player, Vector2.Zero, DeltaTime))
          PrssedClose = true;
      }
      else if (this.quarantineInfoPopUp != null)
      {
        if (this.quarantineInfoPopUp.UpdateQuarantinePopup(player, DeltaTime, Vector2.Zero))
          this.quarantineInfoPopUp = (QuarantineInfoPopup) null;
      }
      else
      {
        if ((double) this.lerper.Value != 0.0)
          return false;
        this.controllermatrix.UpdateAnimalPopUpControllerMatrix(player, DeltaTime, this.tabframemanager);
        AnimalViewTabType animalviewtab;
        PrssedClose = this.tabframemanager.UpdateTabFrameManager(Vector2.Zero, player, DeltaTime, out animalviewtab);
        if (animalviewtab != AnimalViewTabType.Count)
          this.TrySwitchToThisTab(animalviewtab, player);
        switch (this.selectedtab)
        {
          case AnimalViewTabType.Animal:
            bool PopupQuarantineInfo;
            if (this.animaltabmanager.UpdateAnimalTabManager(this.tabframemanager.location, player, DeltaTime, out PopupQuarantineInfo))
            {
              if (PopupQuarantineInfo)
              {
                this.DoQuarantinePopup(player);
                break;
              }
              PrssedClose = true;
              break;
            }
            break;
          case AnimalViewTabType.Habitat:
            this.habitatamanager.UpdateHabitatTabManager(this.tabframemanager.location, player, DeltaTime, ref PrssedClose);
            break;
          case AnimalViewTabType.FamilyTree:
            this.familytreetabmanager.UpdateFamilyTreeTabManager();
            break;
          case AnimalViewTabType.Profitability:
            this.profitabilitymanager.UpdateProfitabilityTabManager();
            break;
          case AnimalViewTabType.Info:
            this.infotabmanager.UpdateInfoTabManager(player, DeltaTime, this.tabframemanager.location);
            break;
          case AnimalViewTabType.LifeTimeStats:
            this.lifetimestatstabmanager.UpdateLifetimeStatsTabManager();
            break;
        }
      }
      if (PrssedClose && this.animaltabmanager != null)
        this.animaltabmanager.OnExit();
      return PrssedClose;
    }

    private void TrySwitchToThisTab(AnimalViewTabType animalviwtype, Player player)
    {
      if (animalviwtype == this.selectedtab || animalviwtype == AnimalViewTabType.Count)
        return;
      this.selectedtab = animalviwtype;
      switch (animalviwtype)
      {
        case AnimalViewTabType.Animal:
          if (this.animaltabmanager == null)
            this.animaltabmanager = new AnimalTabManager(this.REF_animal, player, this.tabframemanager.Vscale.X, this.basescale);
          this.tabframemanager.SetSize(this.animaltabmanager.GetSize(), this.topCenterLocationForPanel);
          this.animaltabmanager.location.Y = this.tabframemanager.GetFrameOffsetForContents().Y;
          break;
        case AnimalViewTabType.Habitat:
          if (this.habitatamanager == null)
            this.habitatamanager = new HabitatTabManager(this.REF_animal, player, this.tabframemanager.Vscale.X, this.basescale);
          this.tabframemanager.SetSize(this.habitatamanager.GetSize(), this.topCenterLocationForPanel);
          this.habitatamanager.location.Y = this.tabframemanager.GetFrameOffsetForContents().Y;
          break;
        case AnimalViewTabType.FamilyTree:
          this.familytreetabmanager = new FamilyTreeTabManager(this.REF_animal, player, this.tabframemanager.Vscale.X, this.basescale);
          this.tabframemanager.SetSize(this.familytreetabmanager.GetSize(), this.topCenterLocationForPanel);
          this.familytreetabmanager.location.Y += this.tabframemanager.GetFrameOffsetForContents().Y;
          break;
        case AnimalViewTabType.Profitability:
          this.profitabilitymanager = new ProfitabilityTabManager(this.REF_animal, player, this.tabframemanager.Vscale.X, this.basescale);
          this.tabframemanager.SetSize(this.profitabilitymanager.GetSize(), this.topCenterLocationForPanel);
          this.profitabilitymanager.location.Y += this.tabframemanager.GetFrameOffsetForContents().Y;
          break;
        case AnimalViewTabType.Info:
          this.infotabmanager = new InfoTabManager(this.REF_animal, player, this.tabframemanager.Vscale.X, this.basescale);
          this.tabframemanager.SetSize(this.infotabmanager.GetSize(), this.topCenterLocationForPanel);
          this.infotabmanager.location.Y += this.tabframemanager.GetFrameOffsetForContents().Y;
          break;
        case AnimalViewTabType.LifeTimeStats:
          this.lifetimestatstabmanager = new LifetimeStatsTabManager(this.REF_animal, player, this.tabframemanager.Vscale.X, this.basescale);
          this.tabframemanager.SetSize(this.lifetimestatstabmanager.GetSize(), this.topCenterLocationForPanel);
          this.lifetimestatstabmanager.location.Y += this.tabframemanager.GetFrameOffsetForContents().Y;
          break;
      }
      this.tabframemanager.SetNewHeading(TabHeading.GetHeaderForThisTab(animalviwtype, this.REF_animal.intakeperson.animaltype));
      if (animalviwtype == AnimalViewTabType.Animal)
        this.animaltabmanager.AddLookAtThisButton(this.REF_animal, this.tabframemanager.GetMiniHeadingSize());
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      if (animalviwtype == AnimalViewTabType.FamilyTree || animalviwtype == AnimalViewTabType.Profitability || animalviwtype == AnimalViewTabType.LifeTimeStats)
        this.tabframemanager.LockForBeta();
      else
        this.tabframemanager.LockForBeta(true);
    }

    private void DoQuarantinePopup(Player player)
    {
      this.quarantineInfoPopUp = new QuarantineInfoPopup(player, Z_GameFlags.GetBaseScaleForUI());
      this.quarantineInfoPopUp.location = new Vector2(512f, 384f);
    }

    public void DrawAnimalPopUpManager()
    {
      SpriteBatch pointspritebatchTop05 = AssetContainer.pointspritebatchTop05;
      Vector2 vector2 = new Vector2(this.lerper.Value * 512f, 0.0f);
      if (this.deadanimalpopup != null)
      {
        this.deadanimalpopup.DrawDeadAnimalPopUp(vector2, pointspritebatchTop05);
      }
      else
      {
        if (Z_GameFlags.JustExitedWorldMap_CheckAnimalExistsInAnimalPanel)
          return;
        this.tabframemanager.DrawTabFrameManager(vector2, pointspritebatchTop05);
        switch (this.selectedtab)
        {
          case AnimalViewTabType.Animal:
            this.animaltabmanager.DrawAnimalTabManager(this.tabframemanager.location + vector2, pointspritebatchTop05);
            break;
          case AnimalViewTabType.Habitat:
            this.habitatamanager.DrawHabitatTabManager(this.tabframemanager.location + vector2, pointspritebatchTop05);
            break;
          case AnimalViewTabType.FamilyTree:
            this.familytreetabmanager.DrawFamilyTreeTabManager(this.tabframemanager.location + vector2, pointspritebatchTop05);
            break;
          case AnimalViewTabType.Profitability:
            this.profitabilitymanager.DrawProfitabilityTabManager(this.tabframemanager.location + vector2, pointspritebatchTop05);
            break;
          case AnimalViewTabType.Info:
            this.infotabmanager.DrawInfoTabManager(this.tabframemanager.location + vector2, pointspritebatchTop05);
            break;
          case AnimalViewTabType.LifeTimeStats:
            this.lifetimestatstabmanager.DrawLifetimeStatsTabManager(this.tabframemanager.location + vector2, pointspritebatchTop05);
            break;
        }
        if (this.quarantineInfoPopUp != null)
          this.quarantineInfoPopUp.DrawQuarantineInfoPopup(vector2, pointspritebatchTop05);
        this.tabframemanager.PostDrawTabFrameManager(vector2, pointspritebatchTop05);
      }
    }
  }
}

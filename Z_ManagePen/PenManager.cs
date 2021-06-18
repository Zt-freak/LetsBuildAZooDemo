// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.PenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Manage.MainButtons;
using TinyZoo.Z_ManagePen.Cleanliness;
using TinyZoo.Z_ManagePen.Diet;
using TinyZoo.Z_ManagePen.FoodChain;
using TinyZoo.Z_ManagePen.NoAnimal;
using TinyZoo.Z_ManagePen.Summary;

namespace TinyZoo.Z_ManagePen
{
  internal class PenManager
  {
    internal static Vector2Int SelectedPen;
    private StoreBGManager storeBGManager;
    private BackButton backbutton;
    private ScreenHeading screenheading;
    private PenManageButtons penbts;
    private DietManager dietmanager;
    private FoodChainManager foodchainmanager;
    private CleanlinessManager cleanlinessmanager;
    private PenSummaryManager pensummarymamaner;
    private NoAnimalManager noanimals;
    private ManageButtonType selectedpage;
    private PrisonZone SelectedEnclosure;
    public static float HeadingBuffer = 60f;
    private bool HasNoAnimals;

    public PenManager(Player player)
    {
      float MasterMult = 1f;
      PenManager.HeadingBuffer = 50f;
      this.SelectedEnclosure = player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID);
      this.selectedpage = ManageButtonType.Feed;
      this.screenheading = new ScreenHeading("ENCLOSURE MANAGEMENT", 90f);
      this.backbutton = new BackButton();
      this.storeBGManager = new StoreBGManager(IsBlue: true);
      this.penbts = new PenManageButtons(player);
      if (this.SelectedEnclosure.prisonercontainer.prisoners.Count == 0)
      {
        this.HasNoAnimals = true;
        this.noanimals = new NoAnimalManager();
      }
      if (this.noanimals == null)
        this.dietmanager = new DietManager(player, this.SelectedEnclosure, MasterMult);
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.DietManagement)
        return;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
    }

    public void UpdatePenManager(float DeltaTime, Player player)
    {
      float SimulationTime = DeltaTime;
      GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref SimulationTime, player);
      if (this.backbutton.UpdateBackButton(player, DeltaTime))
      {
        TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
      this.storeBGManager.UpdateStoreBGManager(DeltaTime);
      if (this.noanimals != null)
        return;
      ManageButtonType manageButtonType = this.penbts.UpdatePenManageButtons(player, DeltaTime, Vector2.Zero);
      if (manageButtonType != ManageButtonType.Count && manageButtonType != this.selectedpage)
      {
        this.selectedpage = manageButtonType;
        switch (this.selectedpage)
        {
          case ManageButtonType.CleanPen:
            this.cleanlinessmanager = new CleanlinessManager(this.SelectedEnclosure);
            break;
          case ManageButtonType.Feed:
            float MasterMult = 1f;
            this.dietmanager = new DietManager(player, this.SelectedEnclosure, MasterMult);
            break;
          case ManageButtonType.FoodChain:
            this.foodchainmanager = new FoodChainManager();
            break;
          case ManageButtonType.PenSummary:
            this.pensummarymamaner = new PenSummaryManager();
            break;
        }
      }
      switch (this.selectedpage)
      {
        case ManageButtonType.CleanPen:
          this.cleanlinessmanager.UpdateCleanlinessManager(player, DeltaTime);
          break;
        case ManageButtonType.Feed:
          this.dietmanager.UpdateDietManager(DeltaTime, player);
          break;
        case ManageButtonType.FoodChain:
          this.foodchainmanager.UpdateFoodChainManager(player, DeltaTime);
          break;
        case ManageButtonType.PenSummary:
          this.pensummarymamaner.UpdateSummaryManager(player, DeltaTime);
          break;
      }
    }

    public void DrawPenManager()
    {
      this.storeBGManager.DrawStoreBGManager(Vector2.Zero);
      this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.backbutton.DrawBackButton(Vector2.Zero);
      if (this.noanimals != null)
      {
        this.noanimals.DrawNoAnimals();
      }
      else
      {
        this.penbts.DrawPenManageButtons(Vector2.Zero);
        switch (this.selectedpage)
        {
          case ManageButtonType.CleanPen:
            this.cleanlinessmanager.DrawCleanlinessManager();
            break;
          case ManageButtonType.Feed:
            this.dietmanager.DrawDietManager();
            break;
          case ManageButtonType.FoodChain:
            this.foodchainmanager.DrawFoodChainManager();
            break;
          case ManageButtonType.PenSummary:
            this.pensummarymamaner.DrawSummaryManager();
            break;
        }
        GameStateManager.tutorialmanager.DrawTutorialManager();
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.InmateSummary.PrisonersPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.Intake.InmateSummary
{
  internal class PrisonersPanel
  {
    public Vector2 Location;
    private GenericBox box;
    private SimpleTextHandler simpletext;
    private TextButton Button;
    private StringInBox InUse;
    private StringInBox Available;
    private IntakeInfo refinfo;
    private PrisonerIcons prisonericons;
    private int CST;
    private SimpleTextHandler CostDesc;
    private bool CantAfford;
    private DollarPanel dollarpanel;

    public PrisonersPanel(IntakeInfo thisentry, Player player)
    {
      this.refinfo = thisentry;
      this.box = new GenericBox(new Vector2(550f, 400f));
      this.CST = thisentry.GetRecCost(true);
      this.CostDesc = new SimpleTextHandler(SEngine.Localization.Localization.GetText(1));
      this.dollarpanel = new DollarPanel(player);
      this.dollarpanel.SetBuy();
      this.dollarpanel.ForceCost(this.CST);
      this.CostDesc.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      this.CostDesc.Location.X = -100f;
      this.CostDesc.Location.Y = 150f;
      this.CostDesc.AutoCompleteParagraph();
      this.Button = new TextButton(SEngine.Localization.Localization.GetText(2), 60f);
      this.Button.AddControllerButton(ControllerButton.XboxA);
      this.Button.SetButtonGreen();
      this.CostDesc.Location.Y = 140f;
      this.Button.vLocation.X = 150f;
      if (player.Stats.GetCashHeld() < this.CST)
      {
        this.Button.SetButtonRed();
        this.CantAfford = true;
      }
      float _Scale = 2f;
      if (GameFlags.MobileUIScale)
        _Scale = 3f;
      this.simpletext = new SimpleTextHandler(this.GetText(thisentry, player), false, 0.4f, _Scale, false, false);
      this.simpletext.paragraph.linemaker.SetAllColours(1f, 1f, 1f);
      this.simpletext.Location = new Vector2(-200f, -150f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.CreateButtons(player);
      this.prisonericons = new PrisonerIcons(thisentry);
    }

    private void CreateButtons(Player player)
    {
      this.Available = new StringInBox("Power: " + (object) player.inventory.RightBeamUpgrades, 2f, 80f);
      this.Available.SetGreen();
      this.Available.vLocation = new Vector2(-120f, -140f);
      if (this.InUse == null)
        return;
      this.InUse.SetRed();
      this.InUse.vLocation = new Vector2(this.Available.vLocation.X, this.Available.vLocation.Y + 40f);
    }

    private string GetText(IntakeInfo thisentry, Player player) => "";

    public void UpdatePrisonersPanel(
      float DeltaTime,
      Player player,
      bool CanPress,
      out bool GoToCellBlockSelect)
    {
      this.dollarpanel.UpdateDollarPanel(DeltaTime, player);
      if (this.CantAfford && player.Stats.GetCashHeld() >= this.CST)
      {
        this.CantAfford = false;
        this.Button.SetButtonGreen();
      }
      GoToCellBlockSelect = false;
      if (CanPress)
        this.simpletext.UpdateSimpleTextHandler(DeltaTime);
      if (this.Button.UpdateTextButton(player, this.Location, DeltaTime) || player.inputmap.PressedThisFrame[0])
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        if (player.Stats.SpendCash(this.refinfo.GetRecCost(false), SpendingCashOnThis.UnkownThing, player))
        {
          if (TinyZoo.Game1.GetNextGameState() != GAMESTATE.GamePlaySetUp)
          {
            if (player.prisonlayout.cellblockcontainer.prisonzones.Count == 1)
            {
              if (player.prisonlayout.cellblockcontainer.HasSpaceInHoldingCells() >= this.refinfo.People.Count)
                GoToCellBlockSelect = true;
              else
                player.livestats.SelectedPrisonID = player.prisonlayout.cellblockcontainer.prisonzones[0].Cell_UID;
            }
            else
              GoToCellBlockSelect = true;
            this.Button.DoWhiteFlash();
            this.CreateButtons(player);
            player.livestats.intakefornextlevel = this.refinfo;
            player.livestats.SetPrisonersForNextPlay(this.refinfo);
            if (!GoToCellBlockSelect)
            {
              TinyZoo.Game1.screenfade.BeginFade(true);
              TinyZoo.Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
              player.livestats.AddExtraEnemiesForNextPlay(player, player.livestats.SelectedPrisonID);
            }
            this.Available.SetPrimaryColours(1f, Vector3.One);
          }
        }
        else
          GameStateManager.tutorialmanager.CannotAffordFuel(this.CST);
      }
      this.Available.UpdateColours(DeltaTime);
    }

    public void DrawEntryDetailPanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.box.DrawGenericBox(Offset + this.Location, spritebatch);
      this.CostDesc.Location.X = -250f;
      this.Button.vLocation.Y = 150f * Sengine.UltraWideSreenUpwardsMultiplier;
      this.CostDesc.Location.Y = 140f * Sengine.UltraWideSreenUpwardsMultiplier;
      this.CostDesc.DrawSimpleTextHandler(Offset + this.Location, 1f, spritebatch);
      this.dollarpanel.Location = Vector2.Zero;
      this.dollarpanel.DrawDollarPanel(Offset + this.Location + this.CostDesc.Location + new Vector2(220f, 10f * Sengine.ScreenRatioUpwardsMultiplier.Y), false);
      if (TutorialManager.currenttutorial == TUTORIALTYPE.SelectIntake)
      {
        if ((double) FlashingAlpha.Fast.fAlpha > 0.5 && !this.Button.MouseOver)
          this.Button.stringinabox.SetAllColours(1f, 1f, 0.0f);
        else
          this.Button.stringinabox.SetAllColours(ColourData.FernGreen);
      }
      this.Button.DrawTextButton(Offset + this.Location, 1f, spritebatch);
      this.simpletext.Location.Y = -180f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      if ((double) Sengine.UltraWideSreenUpwardsMultiplier <= 1.0 || TutorialManager.currenttutorial != TUTORIALTYPE.SelectIntake)
        this.simpletext.DrawSimpleTextHandler(Offset + this.Location, 1f, spritebatch);
      this.prisonericons.DrawPrisonerIcons(Offset + this.Location, spritebatch);
      if (this.InUse == null)
        return;
      this.InUse.DrawStringInBox(Offset + this.Location, spritebatch);
    }
  }
}

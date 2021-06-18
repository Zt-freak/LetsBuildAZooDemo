// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Research.ResOpt.ResearchOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.IAPInfo;
using TinyZoo.PlayerDir;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.Research.ResOpt
{
  internal class ResearchOption
  {
    private GameObject BG;
    private Vector2 VSCALE;
    private SimpleTextHandler text;
    private TRC_ButtonDisplay ControllerButton;
    public Vector2 location;
    private bool Exiting;
    private LerpHandler_Float lerper;
    private ResearchIcon researchicon;
    private bool IsComplete;
    public ResearchType reearchtype;
    private bool IsHighEnoughLevel;
    private GameObject SHortCutToProgress;
    private GameObject SCMedal;

    public ResearchOption(
      ResearchType _reearchtype,
      bool _IsComplete,
      string Times,
      Player player,
      bool _IsHighEnoughLevel = true,
      int NextRank = -1)
    {
      this.IsHighEnoughLevel = _IsHighEnoughLevel;
      this.ControllerButton = new TRC_ButtonDisplay(2f);
      this.ControllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, SEngine.ControllerButton.XboxA);
      this.reearchtype = _reearchtype;
      this.BG = new GameObject();
      this.BG.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BG.SetDrawOriginToCentre();
      this.BG.SetAllColours(ColourData.FernDarkBlue);
      this.VSCALE = new Vector2(800f, 100f * Sengine.UltraWideSreenDownardsMultiplier);
      this.IsComplete = _IsComplete;
      string TEXT = "";
      if (this.IsComplete || !this.IsHighEnoughLevel)
      {
        string text = SEngine.Localization.Localization.GetText(43);
        if (!this.IsHighEnoughLevel)
        {
          this.SHortCutToProgress = new GameObject();
          this.SHortCutToProgress.DrawRect = new Rectangle(280, 101, 25, 26);
          this.SHortCutToProgress.SetDrawOriginToCentre();
          this.SHortCutToProgress.vLocation.X = 350f;
          this.SHortCutToProgress.scale = 2f;
          TEXT = string.Format(SEngine.Localization.Localization.GetText(362), (object) (NextRank + 1));
          this.IsComplete = true;
          this.SCMedal = IAPStatusManager.GetMedalIcon(player);
        }
        else
        {
          switch (this.reearchtype)
          {
            case ResearchType.Building:
              TEXT = "Facilities: " + text;
              break;
            case ResearchType.Alien:
              TEXT = "Alien Races: " + text;
              break;
            case ResearchType.CellType:
              TEXT = "Cell Blocks: " + text;
              break;
          }
        }
      }
      else
      {
        switch (this.reearchtype)
        {
          case ResearchType.Building:
            TEXT = "Facilities: " + Times + "~New buildings will enhance the effectiveness of this prison facility.";
            break;
          case ResearchType.Alien:
            TEXT = "Alien Races: " + Times + "~Discovering new solar systems allows us to take in more valuable criminals.";
            break;
          case ResearchType.CellType:
            TEXT = "Cell Blocks: " + Times + "~Different aliens will prosper in different environments.";
            break;
        }
      }
      this.SetUpText(TEXT);
      this.researchicon = new ResearchIcon();
      this.researchicon.vLocation.X = -350f;
      if (this.IsComplete)
        this.researchicon.SetAllColours(0.2f, 0.2f, 0.2f);
      this.lerper = new LerpHandler_Float();
    }

    public void SetPartialComplete() => this.SetUpText(SEngine.Localization.Localization.GetText(44));

    private void SetUpText(string TEXT)
    {
      float num1 = 1f;
      float num2 = 1f;
      if (PlayerStats.language == Language.French)
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(TEXT, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.Japanese)
      {
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(TEXT, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        num2 = 0.9f;
      }
      this.text = new SimpleTextHandler(TEXT, false, 0.7f * num2, 3f * Sengine.UltraWideSreenDownardsMultiplier * num1, false, false);
      this.text.Location.X = -300f;
      this.text.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      this.text.AutoCompleteParagraph();
    }

    public void Exit()
    {
      if (this.Exiting)
        return;
      this.Exiting = true;
      this.lerper.SetLerp(true, 0.0f, 1f, 3f);
    }

    public bool UpdateResearchOption(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      ref bool ExitComplete,
      bool IsSelectedWIthController)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset += this.location;
      this.text.UpdateSimpleTextHandler(DeltaTime);
      if (!this.Exiting)
      {
        ExitComplete = false;
        if (!this.IsComplete)
        {
          if (this.researchicon.UpdateResearchIcon(player, Offset, IsSelectedWIthController))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade);
            return true;
          }
        }
        else if (!this.IsHighEnoughLevel)
        {
          if (this.researchicon.UpdateResearchIcon(player, Offset, IsSelectedWIthController))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade);
            TinyZoo.Game1.SetNextGameState(GAMESTATE.ProfitLadderSetUp);
            TinyZoo.Game1.screenfade.BeginFade(true);
          }
          else if (this.SHortCutToProgress != null && MathStuff.CheckPointCollision(true, Offset + this.SHortCutToProgress.vLocation, this.SHortCutToProgress.scale, (float) this.SHortCutToProgress.DrawRect.Width, (float) this.SHortCutToProgress.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade);
            TinyZoo.Game1.SetNextGameState(GAMESTATE.ProfitLadderSetUp);
            TinyZoo.Game1.screenfade.BeginFade(true);
          }
        }
      }
      else if ((double) this.lerper.Value != 1.0)
        ExitComplete = false;
      return false;
    }

    public void DrawResearchOption(Vector2 Offset, bool IsSelectedWithController)
    {
      Offset += this.location;
      Offset.X += this.lerper.Value * 1024f;
      if (IsSelectedWithController)
      {
        this.BG.SetAllColours(ColourData.FernGreen);
        this.BG.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, (this.VSCALE + new Vector2(6f, 6f)) * Sengine.ScreenRatioUpwardsMultiplier);
        this.BG.SetAllColours(ColourData.FernDarkBlue);
      }
      this.BG.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE * Sengine.ScreenRatioUpwardsMultiplier);
      this.text.Location.Y = -35f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.UltraWideSreenDownardsMultiplier;
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.researchicon.DrawResearchIcon(Offset);
      if (this.SHortCutToProgress != null)
      {
        this.SHortCutToProgress.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
        this.SCMedal.scale = this.SHortCutToProgress.scale * 0.5f;
        this.SCMedal.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.SHortCutToProgress.vLocation);
      }
      if (!IsSelectedWithController || this.IsComplete)
        return;
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.researchicon.vLocation + Offset + new Vector2(30f, -35f));
    }
  }
}

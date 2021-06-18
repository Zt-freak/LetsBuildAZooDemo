// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectionPanel.SelectedPersonInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.Characters;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectionPanel
{
  internal class SelectedPersonInfo
  {
    private GenericBox genericbox;
    private LerpHandler_Float lerper;
    private SimpleTextHandler Body;
    private TalkingHead talkinghead;
    private TextButton Parole;
    private TextButton Transfer;
    private bool TransferSelected;
    private TextButton EasyMode;
    public AnimalType ThisPerson;
    private SimpleTextHandler Unhappy;
    public bool IsDead;
    private bool UsedNotchInCOnstruct;
    internal static bool SelectedPersonPanelUpdatedThisFrame;
    public Vector2 Offset;

    public SelectedPersonInfo(IntakePerson enemy, bool _IsDead, Player player)
    {
      this.IsDead = _IsDead;
      string decription = EnemyData.GetDecription(enemy);
      this.Create(enemy.animaltype, decription, enemy.CLIndex, true);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      if (enemy.WrongCell)
      {
        this.Unhappy = new SimpleTextHandler(string.Format(SEngine.Localization.Localization.GetText(45), (object) EnemyData.getHabitatString(enemy.animaltype).ToUpper()), false, 0.21f, 2.5f * Sengine.UltraWideSreenDownardsMultiplier, false, false);
        this.Unhappy.AutoCompleteParagraph();
        this.Unhappy.SetAllColours(ColourData.FernRed);
      }
      if (this.IsDead || enemy.WrongCell)
      {
        this.Body.paragraph.linemaker.SetAllColours(ColourData.FernRed);
        this.genericbox.SetEdgeRed();
        this.talkinghead.SetEdgeRed();
      }
      if (this.IsDead)
        return;
      string str = "";
      int num = GameFlags.IsConsoleVersion ? 1 : 0;
      this.Parole = new TextButton(str + "Sell", 50f);
      this.Parole.SetButtonYellow();
      this.Parole.vLocation = new Vector2(80f, 120f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.Parole.AddControllerButton(ControllerButton.XboxY);
      this.TransferSelected = false;
      if (player.prisonlayout.HasSpaceInHoldingCells() <= 0 || player.prisonlayout.IsThisPrisonerInAHoldingCell(enemy))
        return;
      SEngine.Localization.Localization.GetText(11);
      this.Transfer = new TextButton(SEngine.Localization.Localization.GetText(11), 50f);
      this.Transfer.AddControllerButton(ControllerButton.XboxY);
      this.Transfer.vLocation = this.Parole.vLocation;
      this.Transfer.vLocation.X = -80f;
      this.Transfer.SetButtonYellow();
    }

    private void Create(AnimalType enemytype, string TXT, int Variant, bool IsForNotch = false)
    {
      this.ThisPerson = enemytype;
      this.talkinghead = new TalkingHead(enemytype, Variant);
      this.talkinghead.SetLemon();
      float x = 350f;
      if (IsForNotch && GameFlags.HasNotch)
      {
        this.UsedNotchInCOnstruct = true;
        x += GameFlags.NotchSize * 2f;
      }
      this.genericbox = new GenericBox(new Vector2(x, 300f));
      this.genericbox.Location = new Vector2(x / 2f, 360f);
      this.genericbox.SetEdgeLemon();
      this.Body = new SimpleTextHandler(TXT, false, 0.3f, 2f, false, false);
      this.Body.Location.X = (float) -((double) x / 2.0);
      this.Body.Location.X += 15f;
      this.Body.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
    }

    public void LerpOff(bool GoRight)
    {
      if (GoRight && (double) this.lerper.TargetValue != 1.0)
      {
        this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
      }
      else
      {
        if (GoRight || (double) this.lerper.TargetValue <= -1.0)
          return;
        this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
      }
    }

    public SelectedPersonInfo(int ArcadeLevel, Player player)
    {
      string TXT = "Complete previous stages to unlock this";
      switch (player.Stats.ArcadeProgress[ArcadeLevel])
      {
        case -1:
          TXT = SEngine.Localization.Localization.GetText(362);
          break;
        case 0:
          TXT = SEngine.Localization.Localization.GetText(362);
          break;
        case 1:
          TXT = SEngine.Localization.Localization.GetText(362) + "~NORMAL";
          break;
        case 2:
          TXT = SEngine.Localization.Localization.GetText(362) + "~HARD";
          break;
      }
      this.Create(AnimalType.Administrator, TXT, 0);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.genericbox.Location.X = 1024f - this.genericbox.Location.X;
      this.genericbox.Location.Y = 210f;
      this.genericbox.SetPurpleWithYellowFrame();
      if (player.Stats.ArcadeProgress[ArcadeLevel] < 0 && !DebugFlags.UnlockAllArcadeLevels)
      {
        this.talkinghead.SetLocked();
      }
      else
      {
        int num = GameFlags.IsConsoleVersion ? 1 : 0;
        this.Parole = new TextButton("Hard", 45f);
        this.Parole.AddControllerButton(ControllerButton.XboxY);
        this.Parole.SetButtonYellow();
        if (player.Stats.ArcadeProgress[ArcadeLevel] > 0)
          this.Parole.SetButtonGreen();
        this.Parole.vLocation = new Vector2(80f, 120f * Sengine.ScreenRatioUpwardsMultiplier.Y);
        this.Parole.SetLemonANdBlue();
        this.Parole.stringinabox.SetAllColours(new Vector3(0.5f, 0.0f, 0.8f));
        this.Transfer = (TextButton) null;
        this.EasyMode = new TextButton("Easy", 45f);
        this.EasyMode.AddControllerButton(ControllerButton.XboxA);
        this.EasyMode.SetButtonYellow();
        this.EasyMode.vLocation = this.Parole.vLocation;
        this.EasyMode.vLocation.X -= 150f;
        this.EasyMode.SetAllColours(ColourData.FernLemon);
      }
    }

    public SelectedPersonInfo(AnimalType enemy, Player player)
    {
      this.Create(enemy, EnemyData.GetCellectinDecription(enemy, player), 0);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.genericbox.Location.X = 1024f - this.genericbox.Location.X;
      this.genericbox.Location.Y = 210f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.genericbox.SetPurpleWithYellowFrame();
      if (player.Stats.GetAliensCaptured(enemy) > 0)
        return;
      this.talkinghead.SetLocked();
    }

    public bool UpdateSelectedPersonInfo(
      float DeltaTime,
      Player player,
      out bool OnParole,
      out bool Transfered)
    {
      Transfered = false;
      SelectedPersonInfo.SelectedPersonPanelUpdatedThisFrame = true;
      OnParole = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.Body.UpdateSimpleTextHandler(DeltaTime);
      Vector2 zero = Vector2.Zero;
      if (this.UsedNotchInCOnstruct)
        zero.X += GameFlags.NotchSize;
      if ((double) this.lerper.Value != 0.0)
        return false;
      bool flag = this.genericbox.CheckForTaps(player, zero + new Vector2(this.lerper.Value * 450f, 0.0f));
      if (this.Parole != null)
      {
        if (this.Transfer != null)
        {
          if (player.inputmap.PressedThisFrame[5])
          {
            if (!this.TransferSelected)
            {
              this.TransferSelected = true;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 1f);
            }
          }
          else if (player.inputmap.PressedThisFrame[6] && this.TransferSelected)
          {
            this.TransferSelected = false;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 1f);
          }
        }
        OnParole = this.Parole.UpdateTextButton(player, new Vector2(this.lerper.Value * 450f, 0.0f) + this.genericbox.Location + zero, DeltaTime);
        if (OnParole)
          GameFlags.DifficultyIsEasy = false;
        if (this.Transfer != null)
        {
          if (OnParole && this.TransferSelected && GameFlags.IsUsingController)
            OnParole = false;
          Transfered = this.Transfer.UpdateTextButton(player, new Vector2(this.lerper.Value * 450f, 0.0f) + this.genericbox.Location + zero, DeltaTime);
          if (Transfered && !this.TransferSelected && GameFlags.IsUsingController)
            Transfered = false;
        }
      }
      if (this.EasyMode != null && this.EasyMode.UpdateTextButton(player, new Vector2(this.lerper.Value * 450f, 0.0f) + this.genericbox.Location + zero, DeltaTime))
      {
        OnParole = true;
        GameFlags.DifficultyIsEasy = true;
      }
      if (player.inputmap.PressedBackOnController())
      {
        player.inputmap.ReleasedThisFrame[7] = false;
        flag = true;
      }
      if (flag | OnParole)
        player.inputmap.ClearAllInput(player);
      return flag;
    }

    public void DrawSelectedPersonInfo(Vector2 ESOffset)
    {
      this.Offset = new Vector2(this.lerper.Value * 450f, 0.0f);
      this.Offset += ESOffset;
      Vector2 zero = Vector2.Zero;
      if (this.UsedNotchInCOnstruct)
      {
        this.Offset.X += GameFlags.NotchSize;
        zero.X = -GameFlags.NotchSize;
      }
      this.genericbox.DrawGenericBox(this.Offset + zero);
      this.Offset += this.genericbox.Location;
      this.talkinghead.DrawTalkingHead(this.Offset + new Vector2(-110f, -83f * Sengine.ScreenRatioUpwardsMultiplier.Y), AssetContainer.pointspritebatchTop05);
      this.Body.DrawSimpleTextHandler(this.Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.Body.Location.Y = -10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      if (this.Parole != null)
      {
        bool BlockControllerIcon = false;
        if (this.Transfer != null)
        {
          if (this.TransferSelected)
            BlockControllerIcon = true;
          if (GameFlags.IsUsingController)
          {
            if (this.TransferSelected)
              this.Parole.MouseOver = true;
            else
              this.Transfer.MouseOver = true;
          }
        }
        this.Parole.DrawTextButton(new Vector2(this.lerper.Value * 450f, 0.0f) + this.genericbox.Location, 1f, AssetContainer.pointspritebatchTop05, BlockControllerIcon);
        if (this.Transfer != null)
          this.Transfer.DrawTextButton(new Vector2(this.lerper.Value * 450f, 0.0f) + this.genericbox.Location, 1f, AssetContainer.pointspritebatchTop05, !this.TransferSelected);
      }
      if (this.EasyMode != null)
        this.EasyMode.DrawTextButton(new Vector2(this.lerper.Value * 450f, 0.0f) + this.genericbox.Location, 1f, AssetContainer.pointspritebatchTop05);
      if (this.Unhappy == null)
        return;
      this.Unhappy.DrawSimpleTextHandler(this.Offset + new Vector2(-50f, -135f * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

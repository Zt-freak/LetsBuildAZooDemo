// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.RateGame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Utils;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class RateGame
  {
    private GameObject Pigles;
    private LerpHandler_Float lerper;
    private SmartCharacterBox charactertextbox;
    private TextButton Love;
    private TextButton Hate;
    private BlackOut blackout;
    private bool WaitingForRatingPopUp;
    private bool WaitingForEmail;
    private LerpHandler_Float SecondLerper;
    private bool Exiting;

    public RateGame()
    {
      this.Pigles = new GameObject();
      this.Pigles.DrawRect = new Rectangle(589, 304, 89, 86);
      this.Pigles.SetDrawOriginToCentre();
      this.lerper = new LerpHandler_Float();
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(0.7f, 0.35f, 0.0f);
      this.blackout.SetAlpha(false, 0.3f, 0.0f, 0.8f);
      this.charactertextbox = new SmartCharacterBox("", AnimalType.Ostrich);
      this.Love = new TextButton("", 150f);
      this.Hate = new TextButton("", 150f);
      this.Love.SetButtonGreen();
      this.Hate.SetButtonYellow();
      this.Love.vLocation = new Vector2(768f, 600f);
      this.Hate.vLocation = new Vector2(256f, 600f);
    }

    public bool UpdateRateGame(float DeltaTime, Player player)
    {
      if (OverWorldManager.IsGameIntro)
        return false;
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.blackout.UpdateColours(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        if (!this.WaitingForRatingPopUp && !this.WaitingForEmail)
        {
          if (this.Love.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            this.WaitingForRatingPopUp = true;
            this.charactertextbox = new SmartCharacterBox("", AnimalType.Ostrich);
            this.Love = new TextButton(SEngine.Localization.Localization.GetText(79));
            this.Hate = new TextButton(SEngine.Localization.Localization.GetText(66));
            this.Love.SetButtonGreen();
            this.Hate.SetButtonRed();
            this.Love.vLocation = new Vector2(768f, 600f);
            this.Hate.vLocation = new Vector2(256f, 600f);
            this.SecondLerper = new LerpHandler_Float();
            this.SecondLerper.SetLerp(true, 1f, 0.0f, 3f);
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          }
          else if (this.Hate.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
            this.WaitingForEmail = true;
            this.SecondLerper = new LerpHandler_Float();
            this.SecondLerper.SetLerp(true, 1f, 0.0f, 3f);
            this.charactertextbox = new SmartCharacterBox("", AnimalType.Ostrich);
            this.Love = new TextButton(SEngine.Localization.Localization.GetText(36), 60f);
            this.Hate = new TextButton(SEngine.Localization.Localization.GetText(66), 60f);
            this.Love.vLocation = new Vector2(768f, 600f);
            this.Hate.vLocation = new Vector2(256f, 600f);
            this.Love.SetButtonYellow();
            this.Hate.SetButtonYellow();
          }
        }
        else if (this.WaitingForEmail && !this.Exiting)
        {
          if ((double) this.SecondLerper.Value == 0.0)
          {
            if (this.Love.UpdateTextButton(player, Vector2.Zero, DeltaTime))
            {
              this.Exit(player);
              PIP_Emailer.SendEmail(player, false);
            }
            else if (this.Hate.UpdateTextButton(player, Vector2.Zero, DeltaTime))
              this.Exit(player);
          }
        }
        else if (this.WaitingForRatingPopUp && !this.Exiting && (double) this.SecondLerper.Value == 0.0)
        {
          if (this.Love.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            this.Exit(player);
            AppStoreLinker.OpenAppStore(ExternalLinks.PrisonPlanet, true);
          }
          else if (this.Hate.UpdateTextButton(player, Vector2.Zero, DeltaTime))
            this.Exit(player);
        }
      }
      if (this.SecondLerper != null)
        this.SecondLerper.UpdateLerpHandler(DeltaTime);
      player.inputmap.ClearAllInput(player);
      player.inputmap.Movementstick = Vector2.Zero;
      return this.Exiting && (double) this.lerper.Value == 1.0;
    }

    private void Exit(Player player)
    {
      if (this.Exiting)
        return;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
      this.Exiting = true;
      this.lerper.SetLerp(false, 0.0f, 1f, 3f);
      player.Stats.HasRatedGame = true;
      this.blackout.SetAlpha(true, 0.2f, 1f, 0.0f);
      this.charactertextbox.UpdateSmartCharacterBox(0.0f, player, ForceContinue: true);
    }

    public void DrawRateGame()
    {
      if (OverWorldManager.IsGameIntro)
        return;
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.blackout.DrawBlackOut(Offset, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawSmartCharacterBox();
      this.Pigles.vLocation = new Vector2(512f, 350f);
      this.Pigles.scale = 2f;
      this.Pigles.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      Vector2 zero = Vector2.Zero;
      if (this.SecondLerper != null)
        zero.X += 1024f * this.SecondLerper.Value;
      this.Love.DrawTextButton(Offset + zero, 1f, AssetContainer.pointspritebatchTop05);
      if (this.SecondLerper != null)
        zero.X = -1024f * this.SecondLerper.Value;
      this.Hate.DrawTextButton(Offset + zero, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

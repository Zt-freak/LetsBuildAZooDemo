// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SmartCharacterBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Tutorials
{
  internal class SmartCharacterBox
  {
    private List<textBoxPair> characterboxpaisr;
    private CharacterTextBox textbox;
    public bool IsActive;
    public Vector2 Location;
    private LerpHandler_Float lerper;
    public bool Exiting;
    public int ThisLine;
    public float Delay;
    public TinyTextAndButton tinytext;
    private float TopLoc;
    private float ScaleMult;

    public SmartCharacterBox(
      string FirstText,
      AnimalType firstcharacter,
      bool IsBootom = false,
      float _ScaleMult = 1f,
      float Height = 115f,
      bool ShortenForCloseButton = false)
    {
      this.ScaleMult = _ScaleMult;
      this.ThisLine = 0;
      this.IsActive = true;
      this.textbox = new CharacterTextBox(firstcharacter, FirstText, _ScaleMult, Height, ShortenForCloseButton);
      this.characterboxpaisr = new List<textBoxPair>();
      this.TopLoc = (float) (40.0 + 40.0 * (double) Sengine.UltraWideSreenUpwardsMultiplier);
      this.Location = new Vector2(512f, this.TopLoc);
      if (IsBootom)
        this.Location.Y = 688f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Exiting = false;
    }

    public void SetRed() => this.textbox.SetRed();

    public void ForceEndLerp() => this.lerper.SetLerp(true, 0.0f, 0.0f, 3f);

    public void SetNewHeight(float NewHeight) => this.Location.Y = NewHeight;

    public void AddNewText(textBoxPair pair) => this.characterboxpaisr.Add(pair);

    public void AddControllerButtonToLasttextBoxPair(TinyTextAndButton tinytext) => this.characterboxpaisr[this.characterboxpaisr.Count - 1].AddControllerButtonToLasttextBoxPair(tinytext);

    public void AutoLerpOff() => this.Exit();

    private void Exit()
    {
      if (this.Exiting)
        return;
      this.Exiting = true;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
    }

    public bool FinishedLerping() => this.lerper.IsComplete();

    public bool UpdateSmartCharacterBox(
      float DeltaTime,
      Player player,
      bool BlockContinue = false,
      bool ForceContinue = false,
      bool DoNotClearInput = false)
    {
      if ((double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
        return false;
      if ((double) this.Delay > 0.0)
      {
        this.Delay -= DeltaTime;
        return false;
      }
      if (this.tinytext != null)
        this.tinytext.UpdateTinyTextAndButton(DeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.textbox.UpdateCharacterTextBox(DeltaTime);
      if (((double) player.player.touchinput.ReleaseTapArray[0].X >= 0.0 | ForceContinue || player.inputmap.PressedThisFrame[11] || player.inputmap.PressedBackOnController()) && (this.textbox.TryToCompleteParagraph() && !BlockContinue))
      {
        if (this.characterboxpaisr.Count == 0)
        {
          if (!this.Exiting)
          {
            this.IsActive = false;
            this.Exiting = true;
            this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
          }
        }
        else
          this.SetUpNext();
        if (!DoNotClearInput)
          player.inputmap.ClearAllInput(player);
      }
      return this.Exiting && (double) this.lerper.Value == -1.0;
    }

    private void SetUpNext()
    {
      ++this.ThisLine;
      this.Location = new Vector2(512f, this.TopLoc);
      this.textbox = new CharacterTextBox(this.characterboxpaisr[0].talkingguy, this.characterboxpaisr[0].Text, this.ScaleMult);
      this.tinytext = this.characterboxpaisr[0].tinytext;
      if ((double) this.characterboxpaisr[0].OverrideY > -1.0)
        this.Location.Y = this.characterboxpaisr[0].OverrideY;
      this.characterboxpaisr.RemoveAt(0);
    }

    public void DrawSmartCharacterBox(Vector2 Offset)
    {
      Offset += this.Location + new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.textbox.DrawCharacterTextBox(Offset, AssetContainer.pointspritebatchTop05);
      if (this.tinytext == null || TutorialManager.currenttutorial == TUTORIALTYPE.GamePlayIntro && PRISONPLANET_GamePlayManager.beammanager.AreAnyBeamsActive())
        return;
      this.tinytext.DrawTinyTextAndButton(Offset + new Vector2(340f, 0.0f));
    }

    public void DrawSmartCharacterBox() => this.DrawSmartCharacterBox(Vector2.Zero);
  }
}

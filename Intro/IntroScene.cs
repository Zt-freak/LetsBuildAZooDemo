// Decompiled with JetBrains decompiler
// Type: TinyZoo.Intro.IntroScene
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Utils;

namespace TinyZoo.Intro
{
  internal class IntroScene
  {
    private SimpleTextHandler simpletexthandler;
    private bool Started;
    private float Delay;
    public IntroSHot shot;
    private GameObject BG;
    private bool Exiting;
    private float AutoSkipDelay;

    public IntroScene(IntroSHot _shot, float _Delay = 1f, string UseThis = "", bool UseOverrideString = false)
    {
      this.AutoSkipDelay = 0.0f;
      this.shot = _shot;
      string TextToWrite = "";
      switch (this.shot)
      {
        case IntroSHot.Text2:
          this.BG = new GameObject();
          this.BG.DrawRect = new Rectangle(0, 375, 500, 374);
          this.BG.SetColours(false, 0.2f, Vector3.Zero, Vector3.One);
          break;
        case IntroSHot.Text3:
          this.BG = new GameObject();
          this.BG.SetColours(false, 0.2f, Vector3.Zero, Vector3.One);
          this.BG.DrawRect = new Rectangle(501, 0, 500, 374);
          break;
      }
      if (this.BG != null)
      {
        this.BG.SetDrawOriginToCentre();
        this.BG.vLocation = Sengine.ReferenceScreenRes * 0.5f;
        this.BG.scale = 1024f / (float) this.BG.DrawRect.Width;
      }
      if (UseOverrideString)
        TextToWrite = UseThis;
      this.Delay = _Delay;
      float TargetClamp = 2f;
      float PercentagePfScreenWidth = 0.9f;
      Vector2 vector2 = new Vector2(51.2f, 700f);
      if (GameFlags.MobileUIScale)
      {
        TargetClamp = 3f * Sengine.UltraWideSreenDownardsMultiplier;
        vector2 += new Vector2(-20.48f, -7f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier);
        PercentagePfScreenWidth = 0.95f;
      }
      this.simpletexthandler = new SimpleTextHandler(TextToWrite, false, PercentagePfScreenWidth, RenderMath.GetPixelSizeBestMatch(TargetClamp), false, false);
      this.simpletexthandler.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      this.simpletexthandler.Location = vector2;
    }

    public bool UpdateIntroScene(float DeltaTime, Player[] players)
    {
      if (this.BG != null)
        this.BG.UpdateColours(DeltaTime);
      if (!this.Started)
      {
        this.Delay -= DeltaTime;
        if ((double) this.Delay <= 0.0)
          this.Started = true;
      }
      else
      {
        this.simpletexthandler.UpdateSimpleTextHandler(DeltaTime);
        if (this.simpletexthandler.IsComplete() && !this.Exiting)
          this.AutoSkipDelay += DeltaTime;
        if ((ControllerHelper.DidAPlayerPressThis(players, ButtonPressed.Confirm) > -1 || (double) players[0].player.touchinput.ReleaseTapArray[0].X > 0.0 || (double) this.AutoSkipDelay > 4.0) && (!this.Exiting && this.simpletexthandler.TryToCompleteParagraph()))
        {
          if (this.shot != IntroSHot.Text3)
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ModeSelectButton, 0.6f, -1f);
            this.Exiting = true;
            if (this.BG != null)
              this.BG.SetColours(true, 0.2f, Vector3.Zero, Vector3.Zero);
          }
          else
          {
            this.Exiting = true;
            if ((double) this.AutoSkipDelay < 4.0)
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ModeSelectButton, 0.6f, -1f);
            if (TinyZoo.Game1.GetNextGameState() != GAMESTATE.OverWorldSetUp)
            {
              TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
              TinyZoo.Game1.screenfade.BeginFade(true);
            }
          }
        }
      }
      return this.Exiting && (this.BG == null || this.BG != null && (double) this.BG.fRed == 0.0);
    }

    public void DrawIntroScene(Vector2 ExtraOffset)
    {
      GameObject bg = this.BG;
      this.simpletexthandler.DrawSimpleTextHandler(ExtraOffset);
    }
  }
}

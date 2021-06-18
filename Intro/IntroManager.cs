// Decompiled with JetBrains decompiler
// Type: TinyZoo.Intro.IntroManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;

namespace TinyZoo.Intro
{
  internal class IntroManager
  {
    private Planet planet;
    private StarRenderer starset;
    private IntroScene introscene;
    private BlackBars blackbars;
    private TextButton skip;

    public IntroManager()
    {
      TinyZoo.Game1.ClsCLR.SetAllColours(0.0f, 0.0f, 0.0f);
      this.planet = new Planet();
      this.starset = new StarRenderer();
      this.blackbars = new BlackBars();
      this.introscene = new IntroScene(IntroSHot.GameStart);
      MusicManager.playsong(SongTitle.EvilIntro, false, false);
      this.skip = new TextButton(SEngine.Localization.Localization.GetText(81));
      this.skip.AddControllerButton(ControllerButton.XboxY);
      this.skip.SetButtonYellow();
      this.skip.vLocation = new Vector2(900f, 50f);
      this.skip.bActive = false;
    }

    public void UpdateIntroManager(float DeltaTime, Player[] players)
    {
      this.planet.UpdatePlanet(DeltaTime);
      this.starset.UpdateStarRenderer(DeltaTime);
      if (this.skip.bActive && this.skip.UpdateTextButton(players[0], Vector2.Zero, DeltaTime) && TinyZoo.Game1.GetNextGameState() != GAMESTATE.OverWorldSetUp)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ModeSelectButton, 0.6f);
        TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
        TinyZoo.Game1.screenfade.BeginFade(true);
        players[0].inputmap.ClearAllInput(players[0]);
      }
      if (TinyZoo.Game1.GetNextGameState() == GAMESTATE.OverWorldSetUp)
        players[0].inputmap.ClearAllInput(players[0]);
      if ((double) players[0].player.touchinput.ReleaseTapArray[0].X > 0.0 || players[0].inputmap.PressedThisFrame[11])
        this.skip.bActive = true;
      if (!this.introscene.UpdateIntroScene(DeltaTime, players))
        return;
      if (this.introscene.shot == IntroSHot.GameStart)
        this.introscene = new IntroScene(IntroSHot.Text2, 0.5f);
      else
        this.introscene = new IntroScene(IntroSHot.Text3, 0.5f);
    }

    public void DrawIntroManager()
    {
      this.starset.DrawStarRenderer(Vector2.Zero);
      this.planet.DrawPlanet(Vector2.Zero);
      this.blackbars.DrawBlackBars();
      this.introscene.DrawIntroScene(Vector2.Zero);
      if (!this.skip.bActive)
        return;
      this.skip.DrawTextButton(Vector2.Zero);
    }
  }
}

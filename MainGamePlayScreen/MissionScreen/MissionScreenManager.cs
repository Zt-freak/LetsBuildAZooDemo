// Decompiled with JetBrains decompiler
// Type: TinyZoo.MainGamePlayScreen.MissionScreen.MissionScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Intro;

namespace TinyZoo.MainGamePlayScreen.MissionScreen
{
  internal class MissionScreenManager
  {
    private Planet planet;
    private StarRenderer starset;
    private IntroScene introscene;
    private LerpHandler_Float lerper;

    public MissionScreenManager()
    {
      this.planet = new Planet();
      this.starset = new StarRenderer();
      this.introscene = new IntroScene(IntroSHot.GameStart);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public void UpdateMissionScreenManager(float DeltaTime, Player[] players)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.planet.UpdatePlanet(DeltaTime);
      this.starset.UpdateStarRenderer(DeltaTime);
      if (!this.introscene.UpdateIntroScene(DeltaTime, players))
        return;
      this.introscene = new IntroScene(IntroSHot.Text2);
    }

    public void DrawMissionScreenManager()
    {
      Vector2 ExtraOffset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.starset.DrawStarRenderer(ExtraOffset);
      this.planet.DrawPlanet(ExtraOffset);
      this.introscene.DrawIntroScene(ExtraOffset);
    }
  }
}

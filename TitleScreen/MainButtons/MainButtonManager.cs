// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.MainButtons.MainButtonManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.TitleScreen.MainButtons
{
  internal class MainButtonManager
  {
    private TScreenFrame tscreenframe;
    private TitleScreenMainButtons titlescreenbuttons;
    private Vector2 PanelCenter;
    private GameLogo logo;
    private LerpHandler_Float lerper;
    private GameObject go;

    public MainButtonManager(float BaseScale)
    {
      this.tscreenframe = new TScreenFrame(BaseScale);
      this.tscreenframe.VScale = new Vector2(250f, 300f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.tscreenframe.tframe.SetAlpha(0.4f);
      this.PanelCenter = new Vector2(270f, 250f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.titlescreenbuttons = new TitleScreenMainButtons(BaseScale);
      this.titlescreenbuttons.InternalOffset.Y = 80f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.logo = new GameLogo();
      this.logo.Logo.vLocation.Y = -60f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
    }

    public MainMenuButton UpdateMainButtonManager(Player player, float DeltaTime)
    {
      MainMenuButton mainMenuButton = MainMenuButton.Count;
      if ((double) TinyZoo.Game1.screenfade.fAlpha == 0.0)
      {
        this.lerper.UpdateLerpHandler(DeltaTime);
        if ((double) this.lerper.Value == 0.0)
          mainMenuButton = this.titlescreenbuttons.UpdateTitleScreenMainButtons(player, DeltaTime, this.PanelCenter);
      }
      return mainMenuButton;
    }

    public void DrawMainButtonManager()
    {
      this.PanelCenter = new Vector2(780f, 500f);
      Vector2 Offset = this.PanelCenter + new Vector2(this.lerper.Value * -450f, 0.0f);
      this.tscreenframe.DrawTScreenFrame(Offset);
      this.logo.DrawGameLogo(Offset);
      this.titlescreenbuttons.DrawTitleScreenMainButtons(Offset);
    }
  }
}

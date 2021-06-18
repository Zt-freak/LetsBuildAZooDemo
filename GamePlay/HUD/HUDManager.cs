// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.HUD.HUDManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.HUD.StatusArea;

namespace TinyZoo.GamePlay.HUD
{
  internal class HUDManager
  {
    private GameStatusManager topstatus;

    public HUDManager() => this.topstatus = new GameStatusManager();

    public bool UpdateHUDManager(
      float DeltaTime,
      Player player,
      bool BeamFiring,
      bool IsResultsOrIntro)
    {
      GameFlags.JustShuffled = false;
      return this.topstatus.UpdateGameStatusManager(DeltaTime, player, BeamFiring, IsResultsOrIntro);
    }

    public void DrawHUDManager(bool IsResultsOrIntro) => this.topstatus.DrawGameStatusManager(IsResultsOrIntro);
  }
}

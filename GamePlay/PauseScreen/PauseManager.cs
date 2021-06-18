// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.PauseScreen.PauseManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.GamePlay.PauseScreen
{
  internal class PauseManager
  {
    internal static bool ForcePause;
    private PauseButton pausebutton;
    private PauseScreenManager pausescreenmanager;

    public PauseManager()
    {
      GameFlags.GamePaused = false;
      this.pausebutton = new PauseButton();
    }

    public bool UpdatePauseManager(ref float DeltaTime, Player player, bool IsResultsOrIntro)
    {
      if (this.pausebutton.UpdatePauseButton(player, DeltaTime, !IsResultsOrIntro))
      {
        GameFlags.GamePaused = true;
        this.pausescreenmanager = new PauseScreenManager(player);
      }
      if (GameFlags.GamePaused)
      {
        if (this.pausescreenmanager.UpdatePauseScreenManager(DeltaTime, player))
        {
          GameFlags.GamePaused = false;
          if (this.pausescreenmanager.WillRetry)
          {
            this.pausescreenmanager.WillRetry = false;
            return true;
          }
        }
        DeltaTime = 0.0f;
      }
      return false;
    }

    public void DrawPauseManager()
    {
      this.pausebutton.DrawPauseButton();
      if (!GameFlags.GamePaused)
        return;
      this.pausescreenmanager.DrawPauseScreenManager();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Credits.CreditsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;

namespace TinyZoo.Settings.Credits
{
  internal class CreditsManager
  {
    private BackButton back;
    private LerpHandler_Float lerper;
    private CredistsTextManager credits;
    private bool AutoExitOnLoop;

    public CreditsManager(bool _AutoExitOnLoop = false)
    {
      this.AutoExitOnLoop = _AutoExitOnLoop;
      this.back = new BackButton();
      this.lerper = new LerpHandler_Float();
      this.credits = new CredistsTextManager(!this.AutoExitOnLoop);
    }

    public bool UpdateCreditsManager(Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.credits.UpdateCreditsManager(player, DeltaTime, Vector2.Zero) && this.AutoExitOnLoop && TinyZoo.Game1.GetNextGameState() != GAMESTATE.ArcadeModeSetUp)
      {
        TinyZoo.Game1.screenfade.BeginFade(true);
        TinyZoo.Game1.SetNextGameState(GAMESTATE.ArcadeModeSetUp);
      }
      if (this.AutoExitOnLoop)
        return false;
      if (this.back.UpdateBackButton(player, DeltaTime) && (double) this.lerper.TargetValue != -1.0)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        this.lerper.SetLerp(false, -1f, -1f, 3f, true);
      }
      return (double) this.lerper.Value == -1.0;
    }

    public void DrawCreditsManager()
    {
      Vector2 Offset = new Vector2(this.lerper.Value * -1024f, 0.0f);
      if (!this.AutoExitOnLoop)
      {
        this.back.DrawBackButton(Offset);
      }
      else
      {
        Vector2 zero = Vector2.Zero;
      }
      this.credits.DrawCreditsManager(Vector2.Zero);
    }
  }
}

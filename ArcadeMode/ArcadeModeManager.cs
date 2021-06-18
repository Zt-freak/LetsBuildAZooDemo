// Decompiled with JetBrains decompiler
// Type: TinyZoo.ArcadeMode.ArcadeModeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Audio;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.ArcadeMode
{
  internal class ArcadeModeManager
  {
    private MainScreen mainscreen;
    private StoreBGManager storeBG;

    public ArcadeModeManager(Player player)
    {
      this.mainscreen = new MainScreen(player);
      this.storeBG = new StoreBGManager(IsAutumnal: true);
    }

    public void UpdteArcadeModeManager(float DeltaTime, Player player)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      int num = this.mainscreen.UpdateMainScreen(Vector2.Zero, DeltaTime, player, out bool _);
      if (num < 0)
        return;
      GameFlags.ArcadeLevel = num;
      Game1.screenfade.BeginFade(true);
      Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
    }

    public void DrawArcadeModeManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      this.mainscreen.DrawMainScreenManager(Vector2.Zero);
    }
  }
}

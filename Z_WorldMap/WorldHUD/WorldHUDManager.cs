// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldHUD.WorldHUDManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld;

namespace TinyZoo.Z_WorldMap.WorldHUD
{
  internal class WorldHUDManager
  {
    private BackButton close;
    private ScreenHeading screenhead;

    public WorldHUDManager()
    {
      this.screenhead = new ScreenHeading(SEngine.Localization.Localization.GetText(900), 70f);
      this.close = new BackButton(true);
    }

    public void UpdateWorldHUDManager(bool BlockExit, Player player, float DeltaTime)
    {
      if (FeatureFlags.BlockExitFromWorldMap)
        return;
      if (BlockExit)
        this.close.TryLerpOff();
      else
        this.close.TryLerpOn();
      if (!this.close.UpdateBackButton(player, DeltaTime) || BlockExit)
        return;
      Game1.screenfade.BeginFade(true);
      Game1.SetNextGameState(GAMESTATE.OverWorld);
      OverwoldMainButtons.RestExitStatus();
    }

    public void DrawWorldHUDManager()
    {
      if (this.screenhead != null)
        this.screenhead.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      if (FeatureFlags.BlockExitFromWorldMap)
        return;
      this.close.DrawBackButton(Vector2.Zero);
    }
  }
}

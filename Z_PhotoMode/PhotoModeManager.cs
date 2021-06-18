// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PhotoMode.PhotoModeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Input;
using TinyZoo.GenericUI;
using TinyZoo.Z_HUD.VirtualMouse;
using TinyZoo.Z_Pause.Elements;

namespace TinyZoo.Z_PhotoMode
{
  internal class PhotoModeManager
  {
    private BackButton backButton;

    public PhotoModeManager() => this.backButton = new BackButton(true, BaseScale: Z_GameFlags.GetBaseScaleForUI());

    public void UpdatePhotoModeManager(
      Player player,
      float DeltaTime,
      OverWorldManager overworldmanager,
      Player[] players)
    {
      overworldmanager.QuickUpdateOverWorldManager(players, DeltaTime, 0.0f);
      Z_GameFlags.MouseIsOverAPanel = false;
      Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = false;
      if (!player.inputmap.PressedThisFrame[7] && !player.inputmap.PressedThisFrame[20] && (!PC_KeyState.Escape_PressedThisFrame && !this.backButton.UpdateBackButton(player, DeltaTime, Vector2.Zero)))
        return;
      PhotoModeHelper.ExitPhotoMode();
    }

    public void DrawPhotoModeManager(OverWorldManager overworldmanager, Player player)
    {
      overworldmanager.DrawOverWorldManager(player);
      this.backButton.DrawBackButton(Vector2.Zero, AssetContainer.pointspritebatch01, VirtualMouseManager.MouseAlpha);
    }
  }
}

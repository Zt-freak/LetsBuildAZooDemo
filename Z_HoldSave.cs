// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HoldSave
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;

namespace TinyZoo
{
  internal class Z_HoldSave
  {
    internal static void SetHold(bool IsHold, Player player)
    {
      if (IsHold)
        Z_GameFlags.HoldTheClock = true;
      else
        Z_GameFlags.HoldTheClock = false;
    }

    internal static bool ShouldHoldSave() => OverWorldManager.overworldstate == OverWOrldState.MoveBuilding && Z_GameFlags.IsMovingSomething || OverWorldManager.overworldstate == OverWOrldState.Build && ObjectInfoPanel.z_penbuilder != null && ObjectInfoPanel.z_penbuilder.penmover != null || (OverWorldManager.overworldstate == OverWOrldState.QuickPickEmployee || OverWorldManager.zoopopupHolder.IsInAStateThatShouldBlockSave());
  }
}

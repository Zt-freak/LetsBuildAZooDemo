// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.ManageMainSCreen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_Manage.MainButtons
{
  internal class ManageMainSCreen
  {
    private mainButtonsManager mainbtns;

    public ManageMainSCreen() => this.mainbtns = new mainButtonsManager();

    public ManageButtonType UpdateManageMainSCreen(
      Player player,
      float DeltaTime,
      Vector2 Offset)
    {
      Z_GameFlags.BlockPointer = true;
      return this.mainbtns.UpdatemainButtonsManager(player, DeltaTime, Offset);
    }

    public void DrawManageMainSCreen(Vector2 Offset) => this.mainbtns.DrawmainButtonsManager(Offset);
  }
}

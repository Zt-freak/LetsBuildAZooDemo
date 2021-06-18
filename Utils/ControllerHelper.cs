// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.ControllerHelper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir;

namespace TinyZoo.Utils
{
  internal class ControllerHelper
  {
    public static int DidAPlayerPressThis(Player[] players, ButtonPressed buttonpressed)
    {
      for (int index = 0; index < players.Length; ++index)
      {
        if (players[index].inputmap.PressedThisFrame[(int) buttonpressed])
          return index;
      }
      return -1;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Elements.PhotoModeHelper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_Pause.Elements
{
  internal class PhotoModeHelper
  {
    public static void EnterPhotoMode()
    {
      Game1.SetNextGameState(GAMESTATE.PhotoModeSetUp);
      Game1.screenfade.BeginFade(true);
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.LookAtAnimal)
        return;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
    }

    public static void ExitPhotoMode()
    {
      Game1.SetNextGameState(GAMESTATE.OverWorld);
      Game1.screenfade.BeginFade(true);
    }
  }
}

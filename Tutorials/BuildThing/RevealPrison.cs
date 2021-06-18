// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.BuildThing.RevealPrison
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Fog;

namespace TinyZoo.Tutorials.BuildThing
{
  internal class RevealPrison
  {
    private bool FirstUpdateComplete;

    public RevealPrison()
    {
      FogManager.StartFogFade(2f, 0.0f);
      if (OverWorldEnvironmentManager.overworldcam == null)
        return;
      OverWorldEnvironmentManager.overworldcam.DoPan(OverWorldCamera.CameraStartPos, 3f, new Vector3(OverWorldCamera.CameraStartPos.X, OverWorldCamera.CameraStartPos.Y, 1f));
    }

    public bool UpdateRevealPrison()
    {
      if (!this.FirstUpdateComplete)
        this.FirstUpdateComplete = true;
      return false;
    }

    public void DrawRevealPrison()
    {
    }
  }
}

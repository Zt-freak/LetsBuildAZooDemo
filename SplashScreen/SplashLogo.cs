// Decompiled with JetBrains decompiler
// Type: TinyZoo.SplashScreen.SplashLogo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.SplashScreen
{
  internal class SplashLogo : GameObject
  {
    private bool USEFULLSCREEN = true;
    private NMR_Logo nmrlogo;

    public SplashLogo()
    {
      this.DrawRect = new Rectangle(5, 0, 53, 14);
      this.scale = 10f;
      this.vLocation = Sengine.ReferenceScreenRes * 0.5f;
      this.vLocation.X += 6f;
      this.SetDrawOriginToCentre();
      this.vLocation.X = 256f;
      this.scale *= 0.3f;
      this.nmrlogo = new NMR_Logo();
    }

    public void UpdateSplahs(float DeltaTime) => this.nmrlogo.UpdateNMR_Logo(DeltaTime);

    public void DrawSplashLogo()
    {
      this.Draw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
      this.nmrlogo.DrawNMR_Logo();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.SplashScreen.NMR_Logo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.SplashScreen
{
  internal class NMR_Logo : GameObject
  {
    private GameObject WORDS;
    private AnimatedGameObject Leaves;

    public NMR_Logo()
    {
      this.WORDS = new GameObject();
      this.WORDS.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.WORDS.DrawRect = new Rectangle(0, 101, 71, 53);
      this.WORDS.SetDrawOriginToCentre();
      this.vLocation = new Vector2(768f, 384f);
      this.WORDS.SetAlpha(false, 0.5f, 0.0f, 1f);
      this.WORDS.SetColourDelay(0.5f);
      this.Leaves = new AnimatedGameObject();
      this.Leaves.DrawRect = new Rectangle(0, 15, 47, 85);
      this.Leaves.SetDrawOriginToCentre();
      this.Leaves.scale = this.WORDS.scale;
      this.Leaves.SetUpSimpleAnimation(17, 0.1f);
      this.Leaves.SetColourDelay(0.3f);
      this.Leaves.PlayOnlyOnce = true;
      this.Leaves.DrawOrigin.X = 40f;
      this.WORDS.DrawOrigin.X = 3f;
      this.WORDS.DrawOrigin.Y -= 16f;
      this.Leaves.SetAlpha(false, 0.4f, 0.0f, 1f);
    }

    public void UpdateNMR_Logo(float DeltaTime)
    {
      this.Leaves.UpdateColours(DeltaTime);
      if ((double) this.Leaves.fAlpha != 1.0)
        return;
      this.Leaves.UpdateAnimation(DeltaTime);
      if (this.Leaves.Frame <= 10)
        return;
      this.WORDS.UpdateColours(DeltaTime);
    }

    public void DrawNMR_Logo()
    {
      this.Leaves.Draw(AssetContainer.pointspritebatch01, AssetContainer.LogoSheet, this.vLocation);
      this.WORDS.Draw(AssetContainer.pointspritebatch01, AssetContainer.LogoSheet, this.vLocation);
    }
  }
}

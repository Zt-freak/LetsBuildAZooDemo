// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Corpse.Fly
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;

namespace TinyZoo.Z_AnimalsAndPeople.Corpse
{
  internal class Fly : GameObject
  {
    private DualSinOscillator oscialltor;
    private Vector2 OscilationScale;

    public Fly()
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetAllColours(0.0f, 0.0f, 0.0f);
      this.DrawOrigin.Y = (float) TinyZoo.Game1.Rnd.Next(8, 15);
      this.DrawOrigin.X = (float) TinyZoo.Game1.Rnd.Next(-5, 5);
      this.oscialltor = new DualSinOscillator(MathStuff.getRandomFloat(0.5f, 1f), MathStuff.getRandomFloat(0.5f, 1f), MathStuff.getRandomFloat(-3f, 3f), MathStuff.getRandomFloat(-3f, 3f));
      this.OscilationScale = new Vector2((float) TinyZoo.Game1.Rnd.Next(-3, 3), (float) TinyZoo.Game1.Rnd.Next(2, 8));
    }

    public void UpdateFly(float DeltaTime) => this.oscialltor.UpdateDualSinOscillator(DeltaTime);

    public void DrawFly(Vector2 Location)
    {
      this.scale = 1f;
      this.fAlpha = 0.5f;
      this.vLocation = Location + this.oscialltor.CurrentOffset * this.OscilationScale;
      this.vLocation.Y -= 6f;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
    }
  }
}

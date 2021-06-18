// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.FlyHereTutorial
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class FlyHereTutorial
  {
    private FlyHereCircle[] flyhere;
    private int OLDCNT;
    private bool _IsComplete;

    public FlyHereTutorial()
    {
      this.flyhere = new FlyHereCircle[3];
      this.flyhere[0] = new FlyHereCircle();
      this.flyhere[0].vLocation = new Vector2(1610f, 1560f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.flyhere[0].bActive = true;
      this.flyhere[1] = new FlyHereCircle();
      this.flyhere[1].vLocation = new Vector2(1440f, 1536f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.flyhere[2] = new FlyHereCircle();
      this.flyhere[2].vLocation = new Vector2(1528f, 1536f * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    public bool IsComplete() => this._IsComplete;

    public void UpdateFlyHereTutorial(float DeltaTime)
    {
      for (int index = 0; index < this.flyhere.Length; ++index)
      {
        bool ActivateNext;
        this.flyhere[index].UpdateFlyHereCircle(DeltaTime, out ActivateNext);
        if (ActivateNext)
        {
          if (index + 1 < this.flyhere.Length)
            this.flyhere[index + 1].bActive = true;
          else
            this._IsComplete = true;
        }
      }
      if (this.OLDCNT == FeatureFlags.ShipMoved && !GameFlags.IsUsingController)
        return;
      this.OLDCNT = FeatureFlags.ShipMoved;
      for (int index = 0; index < this.flyhere.Length; ++index)
        this.flyhere[index].CheckCollision(FeatureFlags.ShipMovedHere);
    }

    public void DrawFlyHereTutorial()
    {
      for (int index = 0; index < this.flyhere.Length; ++index)
        this.flyhere[index].DrawFlyHereCircle();
    }
  }
}

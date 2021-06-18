// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.BuildThing.BuiodHereFootPrint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Tutorials.BuildThing
{
  internal class BuiodHereFootPrint
  {
    private FingerPointer fingerpointer;
    private GameObject FakeTiles;
    private Vector2Int LOCC;

    public BuiodHereFootPrint()
    {
      this.LOCC = new Vector2Int();
      this.fingerpointer = new FingerPointer();
      this.FakeTiles = new GameObject();
      this.FakeTiles.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FakeTiles.SetDrawOriginToCentre();
      this.FakeTiles.scale = 16f;
      this.FakeTiles.SetAllColours(0.0f, 1f, 0.0f);
      this.FakeTiles.SetAlpha(0.4f);
    }

    public void UpdateBuiodHereFootPrint(float DeltaTime)
    {
    }

    public void DrawBuiodHereFootPrint()
    {
      for (int index1 = 0; index1 < 4; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
        {
          this.LOCC.X = 104 + index1;
          this.LOCC.Y = 92 + index2;
          this.FakeTiles.vLocation = TileMath.GetTileToWorldSpace(this.LOCC);
          this.FakeTiles.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
        }
      }
    }
  }
}

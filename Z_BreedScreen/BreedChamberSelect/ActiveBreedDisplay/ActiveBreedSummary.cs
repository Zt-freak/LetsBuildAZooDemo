// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay.ActiveBreedSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay
{
  internal class ActiveBreedSummary
  {
    private AnimalSexFrame Left;
    private AnimalSexFrame Right;
    private GameObject Plus;

    public ActiveBreedSummary(Player player, int Index) => throw new Exception("DepricatedBreeds 32");

    public void UpdateActiveBreedSummary()
    {
    }

    public void DrawActiveBreedSummary(Vector2 Offset)
    {
      Vector2 vector2 = new Vector2(0.0f, -140f);
      this.Left.DrawAnimalSexFrame(Offset + new Vector2(-55f, 0.0f) + vector2, AssetContainer.pointspritebatch03);
      this.Plus.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + vector2);
      this.Right.DrawAnimalSexFrame(Offset + new Vector2(55f, 0.0f) + vector2, AssetContainer.pointspritebatch03);
    }
  }
}

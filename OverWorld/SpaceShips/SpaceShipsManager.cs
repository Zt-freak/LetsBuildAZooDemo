// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SpaceShips.SpaceShipsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using System.Collections.Generic;

namespace TinyZoo.OverWorld.SpaceShips
{
  internal class SpaceShipsManager
  {
    private List<SpaceShip> spaceships;

    public SpaceShipsManager()
    {
      this.spaceships = new List<SpaceShip>();
      this.spaceships.Add(new SpaceShip());
      this.spaceships[0].StartFlight(DirectionPressed.Right);
      this.spaceships[0].vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(89, 82));
      this.spaceships[0].Speed = 220f;
      this.spaceships.Add(new SpaceShip());
      this.spaceships[1].StartFlight(DirectionPressed.Left);
      this.spaceships[1].vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(135, 74));
      this.spaceships[1].Speed = 200f;
      this.spaceships.Add(new SpaceShip());
      this.DoIt(2);
      this.spaceships.Add(new SpaceShip());
      this.DoIt(3);
    }

    public void UpdateSpaceShipsManager(float DeltaTime)
    {
      for (int index = 0; index < this.spaceships.Count; ++index)
        this.spaceships[index].UpdateSpaceShip(DeltaTime);
    }

    private void DoIt(int Index)
    {
      this.spaceships[Index].StartFlight((DirectionPressed) TinyZoo.Game1.Rnd.Next(0, 4));
      this.spaceships[Index].SetDelay((float) TinyZoo.Game1.Rnd.Next(0, 10));
    }

    public void DrawSpaceShipsManager()
    {
      for (int index = 0; index < this.spaceships.Count; ++index)
        this.spaceships[index].DrawSpaceShip();
    }
  }
}

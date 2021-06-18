// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.Graves.GraveYardBlockInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.Layout.Graves
{
  internal class GraveYardBlockInfo
  {
    public DeadPeople deadpeople;
    public bool PeopleChanged;
    public Vector2Int TopLeft;
    public Vector2Int Size;

    public GraveYardBlockInfo(int LeftFloorSpace, int TopFloorSpace, int Width, int Height)
    {
      this.TopLeft = new Vector2Int(LeftFloorSpace, TopFloorSpace);
      this.deadpeople = new DeadPeople(Width, Height);
      this.Size = new Vector2Int(Width, Height);
    }

    public Vector2Int GetTopLeft() => new Vector2Int(this.TopLeft.X - 1, this.TopLeft.Y - 1);

    public Vector2Int GetTopRight() => new Vector2Int(this.TopLeft.X + this.Size.X, this.TopLeft.Y - 1);

    public Vector2Int GetBottomLeft() => new Vector2Int(this.TopLeft.X - 1, this.TopLeft.Y + this.Size.Y);

    public void ResetForLanguage()
    {
      for (int index = 0; index < this.deadpeople.deadpeople.Count; ++index)
        this.deadpeople.deadpeople[index].intakeperson.ResetForLanguage();
    }

    public bool HasThisAlienSomewhere(AnimalType enemytype)
    {
      for (int index = 0; index < this.deadpeople.deadpeople.Count; ++index)
      {
        if (this.deadpeople.deadpeople[index].intakeperson.animaltype == enemytype)
          return true;
      }
      return false;
    }

    public DeadPerson GetThisGraveyardBlockDeadPerson(Vector2Int location)
    {
      for (int index = 0; index < this.deadpeople.deadpeople.Count; ++index)
      {
        int num1 = this.TopLeft.X + this.deadpeople.deadpeople[index].GraveLocation.X;
        int num2 = this.TopLeft.Y + this.deadpeople.deadpeople[index].GraveLocation.Y;
        int x = location.X;
        if (num1 == x && num2 == location.Y)
          return this.deadpeople.deadpeople[index];
      }
      return (DeadPerson) null;
    }

    public Vector2Int GetBottomRight() => new Vector2Int(this.TopLeft.X + this.Size.X, this.TopLeft.Y + this.Size.Y);

    public void CheckGraveStonesOnLoad(LayoutData layout)
    {
      for (int index1 = 0; index1 < this.deadpeople.deadpeople.Count; ++index1)
      {
        int index2 = this.TopLeft.X + this.deadpeople.deadpeople[index1].GraveLocation.X;
        int index3 = this.TopLeft.Y + this.deadpeople.deadpeople[index1].GraveLocation.Y;
        if (layout.BaseTileTypes[index2, index3].tiletype != TILETYPE.GraveYard_FloorGraveStone)
          layout.BaseTileTypes[index2, index3].tiletype = TILETYPE.GraveYard_FloorGraveStone;
      }
    }

    public void CheckAgainstLayoutAndMap(TileRenderer[,] tilesasarray, PrisonLayout prisonlayout)
    {
      for (int index1 = 0; index1 < this.deadpeople.deadpeople.Count; ++index1)
      {
        int index2 = this.TopLeft.X + this.deadpeople.deadpeople[index1].GraveLocation.X;
        int index3 = this.TopLeft.Y + this.deadpeople.deadpeople[index1].GraveLocation.Y;
        if (tilesasarray[index2, index3].Ref_layoutentry.tiletype != TILETYPE.GraveYard_FloorGraveStone)
        {
          prisonlayout.layout.AddTile(TILETYPE.GraveYard_FloorGraveStone, new Vector2Int(index2, index3), 0);
          tilesasarray[this.TopLeft.X + this.deadpeople.deadpeople[index1].GraveLocation.X, this.TopLeft.Y + this.deadpeople.deadpeople[index1].GraveLocation.Y] = new TileRenderer(prisonlayout.layout.BaseTileTypes[index2, index3], index2, index3, false);
        }
      }
      for (int x = this.TopLeft.X; x < this.TopLeft.X + this.Size.X; ++x)
      {
        for (int y = this.TopLeft.Y; y < this.TopLeft.Y + this.Size.Y; ++y)
        {
          if (tilesasarray[x, y] != null && tilesasarray[x, y].Ref_layoutentry.tiletype == TILETYPE.GraveYard_FloorGraveStone)
          {
            bool flag = false;
            for (int index = 0; index < this.deadpeople.deadpeople.Count; ++index)
            {
              if (this.deadpeople.deadpeople[index].GraveLocation.X + this.TopLeft.X == x && this.deadpeople.deadpeople[index].GraveLocation.Y + this.TopLeft.Y == y)
                flag = true;
            }
            if (!flag)
            {
              prisonlayout.layout.AddTile(TILETYPE.GraveYard_Floor, new Vector2Int(x, y), 0);
              tilesasarray[x, y] = new TileRenderer(prisonlayout.layout.BaseTileTypes[x, y], x, y, false);
            }
          }
        }
      }
    }

    public DeadPerson GetThisDeadPerson(Vector2Int Locaton) => this.deadpeople.GetThisDeadPerson(Locaton, this.TopLeft);

    public bool IsThisLocationInThisDungeon(Vector2Int locaaation) => locaaation.X >= this.TopLeft.X - 1 && locaaation.X <= this.TopLeft.X + this.Size.X && (locaaation.Y >= this.TopLeft.Y - 1 && locaaation.Y <= this.TopLeft.Y + this.Size.Y);

    public bool RemoveThisDeadPerson(DeadPerson deadperson)
    {
      if (!this.deadpeople.RemoveThisDeadPerson(deadperson))
        return false;
      this.PeopleChanged = true;
      return true;
    }

    public Vector2Int AddNewlyDead(IntakePerson intakeperson)
    {
      this.PeopleChanged = true;
      return this.deadpeople.AddNewlyDead(intakeperson, this.TopLeft);
    }

    public int HasThisMuchSpaceInGraveYard() => this.Size.X * this.Size.Y - this.deadpeople.deadpeople.Count;

    public void SaveGraveYardBlockInfo(Writer writer)
    {
      this.TopLeft.SaveVector2Int(writer);
      this.Size.SaveVector2Int(writer);
      this.deadpeople.SaveDeadPeople(writer);
    }

    public GraveYardBlockInfo(Reader reader)
    {
      this.TopLeft = new Vector2Int(reader);
      this.Size = new Vector2Int(reader);
      this.deadpeople = new DeadPeople(reader, this.Size.X, this.Size.Y);
    }
  }
}

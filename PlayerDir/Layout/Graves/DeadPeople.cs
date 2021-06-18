// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.Graves.DeadPeople
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.PlayerDir.Layout.Graves
{
  internal class DeadPeople
  {
    public List<DeadPerson> deadpeople;
    private List<Vector2Int> EmptyGraves;
    private int SizeX;
    private int SizeY;

    public DeadPeople(int WidthOfGraveYard, int HeightOfGraveYard)
    {
      this.SizeX = WidthOfGraveYard;
      this.SizeY = HeightOfGraveYard;
      this.CreateEmtptyGraves();
      this.deadpeople = new List<DeadPerson>();
    }

    private void CreateEmtptyGraves()
    {
      this.EmptyGraves = new List<Vector2Int>();
      for (int _X = 0; _X < this.SizeX; ++_X)
      {
        for (int _Y = 0; _Y < this.SizeY; ++_Y)
          this.EmptyGraves.Add(new Vector2Int(_X, _Y));
      }
      if (this.deadpeople == null)
        return;
      for (int index1 = 0; index1 < this.deadpeople.Count; ++index1)
      {
        for (int index2 = this.EmptyGraves.Count - 1; index2 > -1; --index2)
        {
          if (this.EmptyGraves[index2].CompareMatches(this.deadpeople[index1].GraveLocation))
            this.EmptyGraves.RemoveAt(index2);
        }
      }
    }

    public bool RemoveThisDeadPerson(DeadPerson deadperson)
    {
      for (int index = this.deadpeople.Count - 1; index > -1; --index)
      {
        if (deadperson == this.deadpeople[index])
        {
          this.deadpeople.RemoveAt(index);
          this.CreateEmtptyGraves();
          return true;
        }
      }
      return false;
    }

    public Vector2Int AddNewlyDead(IntakePerson intakeperson, Vector2Int TopLeft)
    {
      int index = TinyZoo.Game1.Rnd.Next(0, this.EmptyGraves.Count);
      this.deadpeople.Add(new DeadPerson(this.EmptyGraves[index], intakeperson));
      Vector2Int vector2Int1 = new Vector2Int(this.EmptyGraves[index]);
      this.EmptyGraves.RemoveAt(index);
      GameFlags.GraveYardUpdated = true;
      Vector2Int vector2Int2 = TopLeft;
      return vector2Int1 + vector2Int2;
    }

    public DeadPerson GetThisDeadPerson(Vector2Int Locaton, Vector2Int TopLeft)
    {
      for (int index = 0; index < this.deadpeople.Count; ++index)
      {
        if (this.deadpeople[index].GraveLocation.X + TopLeft.X == Locaton.X && this.deadpeople[index].GraveLocation.Y + TopLeft.Y == Locaton.Y)
          return this.deadpeople[index];
      }
      return (DeadPerson) null;
    }

    public void SaveDeadPeople(Writer writer)
    {
      writer.WriteInt("x", this.deadpeople.Count);
      for (int index = 0; index < this.deadpeople.Count; ++index)
        this.deadpeople[index].SaveDeadPerson(writer);
    }

    public DeadPeople(Reader reader, int WidthOfGraveYard, int HeightOfGraveYard)
    {
      this.SizeX = WidthOfGraveYard;
      this.SizeY = HeightOfGraveYard;
      int _out = 0;
      int num = (int) reader.ReadInt("x", ref _out);
      this.deadpeople = new List<DeadPerson>();
      for (int index = 0; index < _out; ++index)
        this.deadpeople.Add(new DeadPerson(reader));
      this.CreateEmtptyGraves();
    }
  }
}

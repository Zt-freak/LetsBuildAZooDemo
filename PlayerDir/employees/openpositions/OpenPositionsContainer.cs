// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.openpositions.OpenPositionsContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.employees.openpositions
{
  internal class OpenPositionsContainer
  {
    private List<OpenPositions> openPositions;

    public OpenPositionsContainer() => this.openPositions = new List<OpenPositions>();

    public List<OpenPositions> GetAllJobsWithApplicantsOpenPositions()
    {
      List<OpenPositions> openPositionsList = new List<OpenPositions>();
      for (int index = 0; index < this.openPositions.Count; ++index)
      {
        if (this.openPositions[index].GetNumberOfApplicants() > 0)
          openPositionsList.Add(this.openPositions[index]);
      }
      return openPositionsList;
    }

    public bool GetHasApplicantsAtGate()
    {
      for (int index = 0; index < this.openPositions.Count; ++index)
      {
        if (this.openPositions[index].GetNumberOfApplicants() > 0 && this.openPositions[index].RoamingEmployeeType != EmployeeType.None)
          return true;
      }
      return false;
    }

    public void AddNewOpenPosition(OpenPositions position)
    {
      position.DayStarted = (int) Player.financialrecords.GetDaysPassed();
      this.openPositions.Add(position);
    }

    public void RemoveThisOpenPosition(TILETYPE _tileType)
    {
      OpenPositions positionForThisShop = this.GetOpenPositionForThisShop(_tileType);
      if (positionForThisShop == null)
        return;
      this.RemoveThisOpenPosition(positionForThisShop);
    }

    public void RemoveThisOpenPosition(OpenPositions position) => this.openPositions.Remove(position);

    public OpenPositions GetOpenPositionForThisEmployee(EmployeeType employeetype)
    {
      for (int index = 0; index < this.openPositions.Count; ++index)
      {
        if (this.openPositions[index].RoamingEmployeeType == employeetype)
          return this.openPositions[index];
      }
      return (OpenPositions) null;
    }

    public OpenPositions GetOpenPositionForThisShop(TILETYPE _tileType)
    {
      for (int index = 0; index < this.openPositions.Count; ++index)
      {
        if (this.openPositions[index].tileType == _tileType)
          return this.openPositions[index];
      }
      return (OpenPositions) null;
    }

    public OpenPositions GetOpenPositionForThis(
      TILETYPE _tileType,
      EmployeeType roamingEmployeeType)
    {
      return roamingEmployeeType != EmployeeType.None ? this.GetOpenPositionForThisEmployee(roamingEmployeeType) : this.GetOpenPositionForThisShop(_tileType);
    }

    public List<OpenPositions> GetAllOpenPositions() => this.openPositions;

    public OpenPositionsContainer(Reader reader, int VersionForLoad)
    {
      this.openPositions = new List<OpenPositions>();
      int _out = 0;
      int num = (int) reader.ReadInt("o", ref _out);
      for (int index = 0; index < _out; ++index)
        this.openPositions.Add(new OpenPositions(reader, VersionForLoad));
    }

    public void SaveOpenPositionsContainer(Writer writer)
    {
      writer.WriteInt("o", this.openPositions.Count);
      for (int index = 0; index < this.openPositions.Count; ++index)
        this.openPositions[index].SaveOpenPositions(writer);
    }
  }
}

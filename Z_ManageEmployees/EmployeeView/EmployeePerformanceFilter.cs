// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.EmployeePerformanceFilter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.GeneralEmployees.NotHere;
using TinyZoo.Z_GenericUI.Toggler;

namespace TinyZoo.Z_ManageEmployees.EmployeeView
{
  internal class EmployeePerformanceFilter
  {
    public Vector2 location;
    public EmployeeFilterType filterType;
    private TogglerWithText toggler;
    private float BaseScale;
    private List<EmployeeFilterType> filterTypes;

    public EmployeePerformanceFilter(float _BaseScale, EmployeeType employeeType, float maxWidth)
    {
      this.BaseScale = _BaseScale;
      this.filterTypes = new List<EmployeeFilterType>();
      this.filterTypes.Add(EmployeeFilterType.ThisBranch);
      if (NotHereManager.ShouldDisplayNotHere(employeeType) && employeeType != EmployeeType.Keeper && employeeType != EmployeeType.Architect)
        this.filterTypes.Add(EmployeeFilterType.SameShopBranches);
      string[] strArray = new string[this.filterTypes.Count];
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = this.GetEmployeeFilterTypeToString(this.filterTypes[index]);
      this.toggler = new TogglerWithText(this.BaseScale, maxWidth, AssetContainer.SpringFontX1AndHalf, strArray);
      this.toggler.location.Y -= this.toggler.GetSize().Y * 0.5f;
    }

    public bool UpdateEmployeePerformanceFilter(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.toggler.UpdateTogglerWithText(player, DeltaTime, offset))
        return false;
      this.filterType = this.filterTypes[this.toggler.currentIndex];
      return true;
    }

    public string GetEmployeeFilterTypeToString(EmployeeFilterType filterType)
    {
      switch (filterType)
      {
        case EmployeeFilterType.ThisBranch:
          return "Branch Average";
        case EmployeeFilterType.SameShopBranches:
          return "All Branch Average";
        case EmployeeFilterType.RoamingEmployee:
          return "Average";
        case EmployeeFilterType.AllEmployees:
          return "All Employees";
        default:
          return "NA";
      }
    }

    public Vector2 GetSize() => this.toggler.GetSize();

    public void DrawEmployeePerformanceFilter(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.toggler.DrawTogglerWithText(offset, spriteBatch);
    }
  }
}

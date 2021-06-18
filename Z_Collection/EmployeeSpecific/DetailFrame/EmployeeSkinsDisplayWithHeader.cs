// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.EmployeeSpecific.DetailFrame.EmployeeSkinsDisplayWithHeader
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.EmployeeSpecific.DetailFrame
{
  internal class EmployeeSkinsDisplayWithHeader
  {
    public Vector2 location;
    private ZGenericText header;
    private AnimalInFrameGrid skinGrid;

    public EmployeeSkinsDisplayWithHeader(
      float BaseScale,
      EmployeeType employeeType,
      AnimalType animalType = AnimalType.None,
      TILETYPE tileType = TILETYPE.Count)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = 0.0f;
      this.header = new ZGenericText(string.Format(" Found: {0}/{1}", (object) "?", (object) "?"), BaseScale, false, _UseOnePointFiveFont: true);
      float num2 = num1 + this.header.GetSize().Y + defaultYbuffer * 0.5f;
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      bool _IsUnlocked = false;
      if (employeeType == EmployeeType.ShopKeeper)
      {
        for (int index = 0; index < 6; ++index)
        {
          AnimalType buildingtoEmployee = EmployeeData.GetBuildingtoEmployee(tileType, out bool _);
          animals.Add(new AnimalRenderDescriptor(buildingtoEmployee, _IsUnlocked: _IsUnlocked));
        }
      }
      else
      {
        List<AnimalType> employeeAsLict = Employees.GetEmployeeAsLict(employeeType);
        for (int index = 0; index < employeeAsLict.Count; ++index)
          animals.Add(new AnimalRenderDescriptor(employeeAsLict[index], _IsUnlocked: _IsUnlocked));
      }
      int numberPerRow = Math.Max((int) Math.Ceiling((double) animals.Count / 2.0), 3);
      this.skinGrid = new AnimalInFrameGrid(BaseScale, numberPerRow, defaultXbuffer, defaultYbuffer, animals);
      this.skinGrid.location.Y = num2;
    }

    public void DrawEmployeeSkinsDisplayWithHeader(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      this.skinGrid.DrawAnimalInFrameGrid(offset, spriteBatch);
    }
  }
}

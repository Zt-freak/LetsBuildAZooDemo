// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.EmployeeSpecific.EmployeeDetailFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Collection.EmployeeSpecific.DetailFrame;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.EmployeeSpecific
{
  internal class EmployeeDetailFrame
  {
    public Vector2 location;
    private EmployeeDescColumn DescColumn;
    private EmployeeSkinsDisplayWithHeader employeeSkins;

    public EmployeeDetailFrame(
      EmployeeType employeeType,
      AnimalType animalType,
      TILETYPE tileType,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      double defaultYbuffer = (double) uiScaleHelper.GetDefaultYBuffer();
      float x1 = defaultXbuffer;
      float y = (float) defaultYbuffer;
      float maxWidth = uiScaleHelper.ScaleX(250f);
      this.DescColumn = new EmployeeDescColumn(BaseScale, maxWidth, employeeType, animalType, tileType);
      this.DescColumn.location = new Vector2(x1, y);
      float x2 = x1 + maxWidth;
      this.employeeSkins = new EmployeeSkinsDisplayWithHeader(BaseScale, employeeType, animalType, tileType);
      this.employeeSkins.location = new Vector2(x2, y);
    }

    public void UpdateEmployeeDetailFrame(Player player, float DeltaTime, Vector2 offset) => offset += this.location;

    public void DrawEmployeeDetailFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.DescColumn.DrawEmployeeDescColumn(offset, spriteBatch);
      this.employeeSkins.DrawEmployeeSkinsDisplayWithHeader(offset, spriteBatch);
    }
  }
}

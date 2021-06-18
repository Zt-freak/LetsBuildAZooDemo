// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.EmployeeSpecific.DetailFrame.EmployeeDescColumn
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.EmployeeSpecific.DetailFrame
{
  internal class EmployeeDescColumn
  {
    public Vector2 location;
    private ZGenericText jobTitle;
    private SimpleTextHandler jobDesc;

    public EmployeeDescColumn(
      float BaseScale,
      float maxWidth,
      EmployeeType employeeType,
      AnimalType animalType = AnimalType.None,
      TILETYPE tileType = TILETYPE.Count)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      float num2 = 0.0f;
      this.jobTitle = new ZGenericText(employeeType != EmployeeType.ShopKeeper ? Employees.GetEmployeeThypeToString(employeeType) : TileData.GetTileStats(tileType).Name + " Clerk", BaseScale, false, _UseOnePointFiveFont: true);
      float num3 = num2 + this.jobTitle.GetSize().Y + num1 * 0.5f;
      this.jobDesc = new SimpleTextHandler(Employees.GetEmployeeTypeDescription(employeeType), false, (float) ((double) maxWidth / 1024.0 * 0.899999976158142), BaseScale, false, true);
      this.jobDesc.SetAllColours(ColourData.Z_Cream);
      this.jobDesc.Location.Y = num3;
      float num4 = num3 + uiScaleHelper.ScaleY(45f);
    }

    public void DrawEmployeeDescColumn(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.jobTitle.DrawZGenericText(offset, spriteBatch);
      this.jobDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}

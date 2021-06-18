// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.GeneralEmployees.EM_Bar.TotalEmployeesCol
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Employees.GeneralEmployees.EM_Bar
{
  internal class TotalEmployeesCol
  {
    private ColoumnBG columnBG;
    private SimpleTextHandler simpletext;
    public Vector2 Location;

    public TotalEmployeesCol(
      Vector2 FrameScale,
      float BaseScale,
      List<Employee> theseemployees,
      bool IsMoney)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      float width_ = 80f;
      this.columnBG = new ColoumnBG(FrameScale, BaseScale, width_ * BaseScale);
      string TextToWrite = "Total~Employees:~" + (object) theseemployees.Count;
      if (IsMoney)
      {
        int num = 0;
        for (int index = 0; index < theseemployees.Count; ++index)
          num += theseemployees[index].Salary;
        TextToWrite = "Weekly~Salary:~$" + (object) num;
      }
      this.simpletext = new SimpleTextHandler(TextToWrite, width_, _Scale: BaseScale);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location = new Vector2((float) (-(double) uiScaleHelper.ScaleX(width_) * 0.5), 0.0f);
      this.simpletext.Location.X += defaultBuffer.X;
      this.simpletext.Location.Y -= this.simpletext.GetHeightOfParagraph() * 0.5f;
    }

    public Vector2 GetSize() => this.columnBG.GetSize();

    public void UpdateTotalEmployeesCol()
    {
    }

    public void DrawTotalEmployeesCol(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.columnBG.DrawColoumnBG(Offset, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}

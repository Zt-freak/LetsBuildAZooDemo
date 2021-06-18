// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.SalaryPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_Employees.Emp_Summary.Hiring;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class SalaryPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private SalarySliderBarWithText slider;

    public SalaryPanel(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.frame = new CustomerFrame(Vector2.Zero, BaseScale: this.basescale);
      this.frame.AddMiniHeading("Salary");
      double miniHeadingHeight = (double) this.frame.GetMiniHeadingHeight();
      this.framescale = 2f * defaultBuffer;
      this.frame.Resize(this.framescale);
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateSalaryPanel(Player player, Vector2 offset, float DeltaTime) => false;

    public void DrawSalaryPanel(SpriteBatch spritebatch, Vector2 offset) => offset += this.location;
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.PersonnelAssign
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class PersonnelAssign
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private PortraitRow portraitrow;
    private SliderWithValue slider;
    private CheckBoxWithString checkbox;
    private ZGenericText idlebusy;
    private int numIdle;
    private int numBusy;
    private bool assignbusy;

    public PersonnelAssign(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.numIdle = 6;
      this.numBusy = 4;
      this.checkbox = new CheckBoxWithString("Re-assign nearest busy guards", false, this.basescale);
      this.checkbox.SetTextColour(ColourData.Z_Cream);
      this.slider = new SliderWithValue(1, this.numIdle, 200f, this.basescale, 0.0f, "Assign ");
      this.portraitrow = new PortraitRow(5, this.basescale, this.uiscale.ScaleX(40f));
      this.idlebusy = new ZGenericText(this.numIdle.ToString() + " IDLE, " + (object) this.numBusy + " BUSY", this.basescale);
      int num = this.slider.GetValue();
      for (int index = 0; index < num; ++index)
        this.portraitrow.Add((AnimalType) Game1.Rnd.Next(208, 212), AnimalType.None);
      this.framescale = new Vector2();
      this.framescale = 2f * defaultBuffer;
      this.framescale.X += Math.Max(this.portraitrow.GetSize().X, this.slider.GetSize().X);
      this.framescale.Y += this.portraitrow.GetSize().Y + 0.5f * defaultBuffer.Y + this.slider.GetSize().Y;
      this.framescale.Y += this.checkbox.GetBoxSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.idlebusy.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      this.portraitrow.location = vector2;
      this.portraitrow.location.X = 0.0f;
      this.portraitrow.location.Y += 0.5f * this.portraitrow.GetSize().Y;
      vector2.Y += this.portraitrow.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.slider.location = vector2;
      this.slider.location.X = 0.0f;
      this.slider.location.Y += 0.5f * this.slider.GetSize().Y;
      vector2.Y += this.slider.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.idlebusy.vLocation = vector2;
      this.idlebusy.vLocation.X = 0.0f;
      this.idlebusy.vLocation.Y += 0.5f * this.idlebusy.GetSize().Y;
      vector2.Y += this.idlebusy.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.checkbox.Location = vector2;
      this.checkbox.Location.X = (float) (0.5 * (double) this.framescale.X - 0.5 * (double) this.checkbox.GetBoxSize().X) - defaultBuffer.X;
      this.checkbox.Location.Y += 0.5f * this.checkbox.GetBoxSize().Y;
      vector2.Y += this.checkbox.GetBoxSize().Y + 0.5f * defaultBuffer.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdatePersonnelAssign(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.checkbox.UpdateCheckBoxWithString(player, offset);
      if (this.assignbusy != this.checkbox.IsTicked())
      {
        this.assignbusy = !this.assignbusy;
        int maxvalue = this.numIdle + (this.assignbusy ? this.numBusy : 0);
        float num = (float) (((double) this.slider.GetValue() - 1.0) / ((double) maxvalue - 1.0));
        float startval0to1 = (double) num > 1.0 ? 1f : num;
        Vector2 location = this.slider.location;
        this.slider = new SliderWithValue(1, maxvalue, 200f, this.basescale, startval0to1, "Assign ");
        this.slider.location = location;
      }
      this.slider.UpdateSliderWithValue(player, offset, DeltaTime);
      int num1 = this.slider.GetValue();
      if (num1 < this.portraitrow.Count)
      {
        while (num1 < this.portraitrow.Count)
          this.portraitrow.RemoveLast();
      }
      else if (num1 > this.portraitrow.Count)
      {
        for (int count = this.portraitrow.Count; count < num1; ++count)
          this.portraitrow.Add((AnimalType) Game1.Rnd.Next(208, 212), AnimalType.None);
      }
      this.idlebusy.textToWrite = this.numIdle.ToString() + " IDLE, " + (object) this.numBusy + " BUSY";
      return flag;
    }

    public void DrawPersonnelAssign(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.portraitrow.DrawPortraitRow(spritebatch, offset);
      this.checkbox.DrawCheckBoxWithString(offset, spritebatch);
      this.slider.DrawSliderWithValue(spritebatch, offset);
      this.idlebusy.DrawZGenericText(offset, spritebatch);
    }
  }
}

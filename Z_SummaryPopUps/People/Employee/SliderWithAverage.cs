// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.SliderWithAverage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class SliderWithAverage
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private DragAndBar slider;
    private ZGenericText averagetext;
    private float salary;
    private GameObject line;
    private float sliderwidth;
    private Vector2 linevscale;
    private bool hasaverage;
    private float average;
    private float slidervalue;

    public float Value => this.slidervalue;

    public float Average => this.average;

    public SliderWithAverage(
      float basescale_,
      float initial0to1,
      float sliderwidth_,
      bool hidehandle = false,
      float average0to1 = -1f,
      string averageText = "Average")
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.average = average0to1;
      this.hasaverage = (double) this.average >= 0.0;
      this.sliderwidth = sliderwidth_;
      this.slider = new DragAndBar(false, initial0to1, this.sliderwidth, this.basescale);
      this.slider.HideHandle = hidehandle;
      this.slidervalue = this.slider.CurrentDragPercent;
      if (this.hasaverage)
      {
        this.line = new GameObject();
        this.line.DrawRect = TinyZoo.Game1.WhitePixelRect;
        this.line.SetDrawOriginToCentre();
        this.linevscale = this.scalehelper.ScaleVector2(new Vector2(2f, 24f));
        this.averagetext = new ZGenericText(averageText, this.basescale);
      }
      this.framescale = new Vector2();
      this.framescale.X += this.slider.GetSize().X;
      if (this.hasaverage)
      {
        this.framescale.Y += this.linevscale.Y;
        this.framescale.Y += 2f * this.averagetext.GetSize().Y;
      }
      else
        this.framescale.Y += this.slider.GetSize().Y;
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      if (!this.hasaverage)
        return;
      this.line.vLocation.X = (-0.5f * this.framescale).X + this.average * this.sliderwidth;
      this.line.vLocation.Y = 0.0f;
      Vector2 vLocation = this.line.vLocation;
      vLocation.Y += 0.5f * this.linevscale.Y;
      this.averagetext.vLocation = vLocation + 0.5f * this.averagetext.GetSize();
      this.averagetext.vLocation.X = this.line.vLocation.X;
    }

    public Vector2 GetSize() => this.framescale;

    public void SetToMinimum() => this.slider.ForceFullness((Player) null, -1f);

    public void SetIsActive(bool isActive) => this.slider.SetIsActive(isActive);

    public bool UpdateSliderWithAverage(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.slider.UpdateDragAndBar(player, DeltaTime, offset);
      this.slidervalue = this.slider.CurrentDragPercent;
      return false;
    }

    public void DrawSliderWithAverage(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.slider.DrawDragAndBar(spritebatch, offset);
      if (!this.hasaverage)
        return;
      this.line.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.linevscale, 1f);
      this.averagetext.DrawZGenericText(offset, spritebatch);
    }
  }
}

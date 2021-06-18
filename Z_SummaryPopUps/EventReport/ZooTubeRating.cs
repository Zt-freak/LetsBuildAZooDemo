// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ZooTubeRating
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ZooTubeRating
  {
    private static Rectangle bar2 = new Rectangle(0, 834, 230, 24);
    private static Rectangle yummy = new Rectangle(246, 719, 53, 53);
    private static Rectangle yucky = new Rectangle(296, 669, 53, 53);
    private static Rectangle arrowrect = new Rectangle(430, 549, 34, 34);
    private static Rectangle happyrect = new Rectangle(242, 773, 53, 54);
    private static Rectangle sadrect = new Rectangle(296, 773, 53, 54);
    private static Rectangle barrect = new Rectangle(0, 885, 230, 24);
    private ZGenericUIDrawObject bar;
    private ZGenericUIDrawObject happy;
    private ZGenericUIDrawObject sad;
    private ZGenericUIDrawObject arrow;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private bool animating;
    private bool increasing;
    private int numlaps;
    private static int maxlaps = 2;
    private static float arrowspeed = 300f;
    private float maxX;
    private float destX;
    private float alpha = 1f;

    public float Alpha
    {
      get => this.alpha;
      set
      {
        this.alpha = value;
        this.bar.Alpha = this.alpha;
        this.happy.Alpha = this.alpha;
        this.sad.Alpha = this.alpha;
        this.arrow.Alpha = this.alpha;
      }
    }

    public ZooTubeRating(float rating0to1, float basescale_, bool foodcriticstyle = false)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      if (foodcriticstyle)
      {
        this.happy = new ZGenericUIDrawObject(ZooTubeRating.yummy, this.basescale, AssetContainer.UISheet);
        this.sad = new ZGenericUIDrawObject(ZooTubeRating.yucky, this.basescale, AssetContainer.UISheet);
        this.bar = new ZGenericUIDrawObject(ZooTubeRating.bar2, this.basescale, AssetContainer.UISheet);
      }
      else
      {
        this.happy = new ZGenericUIDrawObject(ZooTubeRating.happyrect, this.basescale, AssetContainer.UISheet);
        this.sad = new ZGenericUIDrawObject(ZooTubeRating.sadrect, this.basescale, AssetContainer.UISheet);
        this.bar = new ZGenericUIDrawObject(ZooTubeRating.barrect, this.basescale, AssetContainer.UISheet);
      }
      this.arrow = new ZGenericUIDrawObject(ZooTubeRating.arrowrect, this.basescale, AssetContainer.UISheet);
      this.increasing = true;
      this.maxX = (float) (0.5 * ((double) this.bar.GetSize().X - (double) defaultBuffer.X));
      this.animating = true;
      this.destX = (float) (2.0 * ((double) rating0to1 - 0.5)) * this.maxX;
      this.framescale = new Vector2();
      this.framescale += this.bar.GetSize();
      this.framescale.X += this.happy.GetSize().X + defaultBuffer.X;
      this.framescale.X += this.sad.GetSize().X + defaultBuffer.X;
      this.framescale.Y = Math.Max(this.happy.GetSize().Y, this.bar.GetSize().Y + 2f * this.arrow.GetSize().Y + defaultBuffer.Y);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.sad.location.X = (float) (-0.5 * (double) this.bar.GetSize().X - (double) defaultBuffer.X - 0.5 * (double) this.sad.GetSize().X);
      this.happy.location.X = (float) (0.5 * (double) this.bar.GetSize().X + (double) defaultBuffer.X + 0.5 * (double) this.happy.GetSize().X);
      this.arrow.location = new Vector2();
      this.arrow.location.Y += (float) (0.5 * ((double) this.arrow.GetSize().Y + (double) this.bar.GetSize().Y + (double) defaultBuffer.Y));
    }

    public Vector2 GetSize() => this.framescale;

    public void StartAnimation() => this.animating = true;

    public bool UpdateZooTubeRating(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      if (this.animating)
      {
        if (this.increasing)
        {
          this.arrow.location.X += DeltaTime * ZooTubeRating.arrowspeed;
          if (this.numlaps >= ZooTubeRating.maxlaps)
          {
            if ((double) this.arrow.location.X > (double) this.destX)
            {
              this.arrow.location.X = this.destX;
              this.increasing = false;
              this.animating = false;
              flag = true;
            }
          }
          else if ((double) this.arrow.location.X > (double) this.maxX)
          {
            this.arrow.location.X = this.maxX;
            this.increasing = false;
            ++this.numlaps;
          }
        }
        else
        {
          this.arrow.location.X -= DeltaTime * ZooTubeRating.arrowspeed;
          if ((double) this.arrow.location.X < -(double) this.maxX)
          {
            this.arrow.location.X = -this.maxX;
            this.increasing = true;
            ++this.numlaps;
          }
        }
      }
      return flag;
    }

    public void DrawZooTubeRating(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.bar.DrawZGenericUIDrawObject(spritebatch, offset);
      this.happy.DrawZGenericUIDrawObject(spritebatch, offset);
      this.sad.DrawZGenericUIDrawObject(spritebatch, offset);
      this.arrow.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}

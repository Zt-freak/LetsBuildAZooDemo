// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.CarTireGuy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class CarTireGuy
  {
    private static Rectangle yellowbgrect = new Rectangle(647, 169, 377, 244);
    private static Rectangle bigstarrect = new Rectangle(293, 607, 59, 61);
    private static Rectangle starrect = new Rectangle(387, 539, 42, 44);
    private static Rectangle tireguyrect = new Rectangle(0, 691, 238, 142);
    private static Rectangle yellownineslice = new Rectangle(253, 676, 42, 42);
    private static Vector2 bannersize;
    private ZGenericText recommended;
    private ZGenericUIDrawObject tireguy;
    private ZGenericUIDrawObject smallstar1;
    private ZGenericUIDrawObject smallstar2;
    private ZGenericUIDrawObject smallstar3;
    private ZGenericUIDrawObject smallstar4;
    private ZGenericUIDrawObject bigstar;
    private GameObjectNineSlice banner;
    private static float maxstaroffset = 2f;
    private static float offsetspd = 10f;
    private Vector2 staroffset;
    private bool up;
    private bool whichscreen;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private float alpha;

    public float Alpha
    {
      get => this.alpha;
      set => this.SetAlpha(value);
    }

    private void SetAlpha(float value)
    {
      this.alpha = value;
      this.tireguy.Alpha = value;
      this.banner.SetAlpha(value);
      this.recommended.SetAlpha(value);
      this.smallstar1.Alpha = value;
      this.smallstar2.Alpha = value;
      this.smallstar3.Alpha = value;
      this.smallstar4.Alpha = value;
      this.bigstar.Alpha = value;
    }

    public CarTireGuy(ReportResultRank rank, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.tireguy = new ZGenericUIDrawObject(CarTireGuy.tireguyrect, this.basescale, AssetContainer.UISheet);
      this.smallstar1 = new ZGenericUIDrawObject(CarTireGuy.starrect, this.basescale, AssetContainer.UISheet);
      this.smallstar2 = new ZGenericUIDrawObject(CarTireGuy.starrect, this.basescale, AssetContainer.UISheet);
      this.smallstar3 = new ZGenericUIDrawObject(CarTireGuy.starrect, this.basescale, AssetContainer.UISheet);
      this.smallstar4 = new ZGenericUIDrawObject(CarTireGuy.starrect, this.basescale, AssetContainer.UISheet);
      this.bigstar = new ZGenericUIDrawObject(CarTireGuy.bigstarrect, this.basescale, AssetContainer.UISheet);
      this.staroffset = Vector2.Zero;
      this.banner = new GameObjectNineSlice(CarTireGuy.yellownineslice, 14);
      CarTireGuy.bannersize = this.scalehelper.ScaleVector2(new Vector2(400f, 70f));
      string _textToWrite = "";
      Vector3 Clr = new Vector3();
      switch (rank)
      {
        case ReportResultRank.A:
          _textToWrite = "IT'S A BIG YUM!";
          Clr = ColourData.Z_TextGreen;
          break;
        case ReportResultRank.B:
          _textToWrite = "IT'S A YUM";
          Clr = ColourData.Z_TextGreen;
          break;
        case ReportResultRank.C:
          _textToWrite = "IT'S A MEH";
          Clr = ColourData.IconYellow;
          break;
        case ReportResultRank.F:
          _textToWrite = "IT'S A YUCK";
          Clr = ColourData.Z_ArrowAndTextRed;
          break;
      }
      this.recommended = new ZGenericText(_textToWrite, 2f * this.basescale, _UseOnePointFiveFont: true);
      this.recommended.SetAllColours(Clr);
      this.recommended.SetAlpha(0.0f);
      this.framescale = this.tireguy.GetSize();
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.tireguy.location = new Vector2();
      this.banner.vLocation = new Vector2();
      this.banner.vLocation.Y -= this.scalehelper.ScaleY(80f);
      this.recommended.vLocation = this.banner.vLocation;
      this.smallstar1.location = this.scalehelper.ScaleVector2(new Vector2(-90f, -65f));
      this.smallstar2.location = this.scalehelper.ScaleVector2(new Vector2(90f, -65f));
      this.smallstar3.location = this.scalehelper.ScaleVector2(new Vector2(-50f, -100f));
      this.smallstar4.location = this.scalehelper.ScaleVector2(new Vector2(50f, -100f));
      this.bigstar.location.Y = -130f;
    }

    public void ChangeScreen() => this.whichscreen = !this.whichscreen;

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCarTireGuy(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      int num = 0;
      if (this.up)
      {
        this.staroffset.Y += DeltaTime * CarTireGuy.offsetspd;
        if ((double) this.staroffset.Y <= (double) CarTireGuy.maxstaroffset)
          return num != 0;
        this.staroffset.Y = CarTireGuy.maxstaroffset;
        this.up = false;
        return num != 0;
      }
      this.staroffset.Y -= DeltaTime * CarTireGuy.offsetspd;
      if ((double) this.staroffset.Y >= -(double) CarTireGuy.maxstaroffset)
        return num != 0;
      this.staroffset.Y = -CarTireGuy.maxstaroffset;
      this.up = true;
      return num != 0;
    }

    public void DrawCarTireGuy(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      if (this.whichscreen)
      {
        this.banner.DrawGameObjectNineSlice(spritebatch, AssetContainer.UISheet, offset, this.scalehelper.ScaleVector2(CarTireGuy.bannersize));
        this.recommended.DrawZGenericText(offset, spritebatch);
      }
      else
      {
        this.smallstar1.DrawZGenericUIDrawObject(spritebatch, offset + this.staroffset);
        this.smallstar2.DrawZGenericUIDrawObject(spritebatch, offset + this.staroffset);
        this.smallstar3.DrawZGenericUIDrawObject(spritebatch, offset - this.staroffset);
        this.smallstar4.DrawZGenericUIDrawObject(spritebatch, offset - this.staroffset);
        this.bigstar.DrawZGenericUIDrawObject(spritebatch, offset + this.staroffset);
      }
      this.tireguy.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}

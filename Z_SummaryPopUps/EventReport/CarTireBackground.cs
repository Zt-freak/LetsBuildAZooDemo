// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.CarTireBackground
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class CarTireBackground
  {
    private static Rectangle paperbgrect = new Rectangle(559, 496, 465, 423);
    private static Rectangle frameTopRect = new Rectangle(609, 121, 416, 44);
    private static Rectangle frameSideRect = new Rectangle(0, 359, 1, 331);
    private static Rectangle frameBottomRect = new Rectangle(608, 166, 416, 1);
    private ZGenericUIDrawObject frametop;
    private ZGenericUIDrawObject framesideL;
    private ZGenericUIDrawObject framesideR;
    private ZGenericUIDrawObject framebot;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;

    public CarTireBackground(float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.frametop = new ZGenericUIDrawObject(CarTireBackground.frameTopRect, this.basescale, AssetContainer.UISheet);
      this.framesideL = new ZGenericUIDrawObject(CarTireBackground.frameSideRect, this.basescale, AssetContainer.UISheet);
      this.framesideR = new ZGenericUIDrawObject(CarTireBackground.frameSideRect, this.basescale, AssetContainer.UISheet);
      this.framebot = new ZGenericUIDrawObject(CarTireBackground.frameBottomRect, this.basescale, AssetContainer.UISheet);
      this.framescale = this.scalehelper.ScaleVector2(new Vector2((float) CarTireBackground.frameTopRect.Width, (float) CarTireBackground.frameSideRect.Height));
      this.frame = new CustomerFrame(this.framescale, new Vector3(1f), this.basescale);
      this.frametop.location.Y = -0.5f * this.framesideL.GetSize().Y - this.scalehelper.ScaleY(4f);
      this.framebot.location.Y = 0.5f * this.framesideR.GetSize().Y;
      this.framesideL.location.X = -0.5f * this.frametop.GetSize().X;
      this.framesideR.location.X = 0.5f * this.frametop.GetSize().X;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateCarTireBackground(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawCarTireBackground(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.frametop.DrawZGenericUIDrawObject(spritebatch, offset);
      this.framesideL.DrawZGenericUIDrawObject(spritebatch, offset);
      this.framesideR.DrawZGenericUIDrawObject(spritebatch, offset);
      this.framebot.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.BikerInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class BikerInfo : VIPInfo
  {
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private LabelledBar attitude;
    private LabelledBar boredom;
    private LabelledBar drunkeness;
    private ZGenericText interactions;

    public BikerInfo(float basescale_, float forceThisWidth = -1f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.attitude = new LabelledBar(0.5f, ColourData.TopBarBlue, "Attitude", this.basescale);
      this.boredom = new LabelledBar(0.5f, ColourData.TopBarBlue, "Boredom", this.basescale);
      this.drunkeness = new LabelledBar(0.5f, ColourData.TopBarBlue, "Drunkeness", this.basescale);
      this.interactions = new ZGenericText("People Upset: 69/420", this.basescale, false);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.frame.AddMiniHeading("Info");
      this.framescale = new Vector2();
      this.framescale += 2f * defaultBuffer;
      this.framescale.Y += this.attitude.GetSize().Y;
      this.framescale.Y += this.boredom.GetSize().Y;
      this.framescale.Y += this.drunkeness.GetSize().Y;
      this.framescale.Y += 3f * defaultBuffer.Y;
      this.framescale.Y += this.interactions.GetSize().Y;
      this.framescale.Y += this.frame.GetMiniHeadingHeight();
      this.framescale.X += 2f * this.drunkeness.GetBarSize().X;
      if ((double) forceThisWidth > 0.0)
        this.framescale.X = forceThisWidth;
      this.frame.Resize(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.frame.GetMiniHeadingHeight();
      float num = this.drunkeness.GetSize().X - this.drunkeness.GetBarSize().X;
      this.attitude.location = vector2;
      this.attitude.location.X += num;
      vector2.Y += this.attitude.GetBarSize().Y + defaultBuffer.Y;
      this.boredom.location = vector2;
      this.boredom.location.X += num;
      vector2.Y += this.boredom.GetBarSize().Y + defaultBuffer.Y;
      this.drunkeness.location = vector2;
      this.drunkeness.location.X += num;
      vector2.Y += this.drunkeness.GetBarSize().Y + defaultBuffer.Y;
      this.interactions.vLocation = vector2;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.attitude.DrawLabelledBar(offset, spritebatch);
      this.boredom.DrawLabelledBar(offset, spritebatch);
      this.drunkeness.DrawLabelledBar(offset, spritebatch);
      this.interactions.DrawZGenericText(offset, spritebatch);
    }
  }
}

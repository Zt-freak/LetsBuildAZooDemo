// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.InfluencerInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class InfluencerInfo : VIPInfo
  {
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private MiniHeading heading;
    private string groupstr;
    private Vector2 groupstrLoc;
    private int numfollowers;
    private ZGenericText followers;
    private StarBar starbar;
    private ZGenericText ratingtext;
    private int thisindex;

    public InfluencerInfo(WalkingPerson thisperson, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.heading = new MiniHeading(Vector2.Zero, "Info", 1f, this.basescale);
      this.followers = new ZGenericText(this.basescale);
      this.followers.textToWrite = "Followers: " + (object) this.numfollowers;
      this.ratingtext = new ZGenericText(this.basescale);
      this.ratingtext.textToWrite = "Current Impression";
      this.starbar = new StarBar((float) TinyZoo.Game1.Rnd.NextDouble() * 5f, this.basescale, true);
      this.framescale.X = this.uiscale.ScaleX(220f);
      this.framescale.Y = 2f * defaultBuffer.Y;
      this.framescale.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.followers.GetSize().Y;
      this.framescale.Y += this.starbar.GetSize().Y + 0.5f * defaultBuffer.Y;
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.followers.vLocation = vector2;
      ZGenericText followers = this.followers;
      followers.vLocation = followers.vLocation + 0.5f * this.followers.GetSize();
      vector2.Y += this.followers.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.starbar.Location = vector2 + 0.5f * this.starbar.GetSize();
      this.ratingtext.vLocation.Y = this.starbar.Location.Y;
      this.ratingtext.vLocation.X = (vector2.X += 0.5f * this.ratingtext.GetSize().X);
      this.starbar.Location.X += this.ratingtext.GetSize().X + 0.5f * defaultBuffer.X;
      vector2.Y += this.starbar.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.heading.SetTextPosition(this.framescale);
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime) => this.UpdateInfluencerInfo(player, offset, DeltaTime);

    public bool UpdateInfluencerInfo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset) => this.DrawInfluencerInfo(spritebatch, offset);

    public void DrawInfluencerInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.followers.DrawZGenericText(offset, spritebatch);
      this.ratingtext.DrawZGenericText(offset, spritebatch);
      this.starbar.DrawStarBar(offset, spritebatch);
      TextFunctions.DrawJustifiedText(this.groupstr, this.basescale, offset + this.groupstrLoc, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
    }
  }
}

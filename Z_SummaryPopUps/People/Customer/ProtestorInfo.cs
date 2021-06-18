// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.ProtestorInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class ProtestorInfo : VIPInfo
  {
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private MiniHeading heading;
    private string groupstr;
    private Vector2 groupstrLoc;
    private SimpleTextHandler reason;
    private PortraitRow portraitrow;
    private LabelledBar bar;
    private int thisindex;

    public ProtestorInfo(WalkingPerson person_, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.heading = new MiniHeading(Vector2.Zero, "Info", 1f, this.basescale);
      this.groupstr = "Group Size";
      Vector2 vector2_1 = this.uiscale.ScaleVector2(AssetContainer.springFont.MeasureString(this.groupstr));
      this.reason = new SimpleTextHandler("Protesting Against: Bad animal living conditions", this.uiscale.ScaleX(195f), _Scale: this.basescale);
      this.reason.SetAllColours(ColourData.Z_Cream);
      this.reason.AutoCompleteParagraph();
      this.portraitrow = new PortraitRow(5, this.basescale);
      List<WalkingPerson> walkingPersons = CustomerManager.ProtestGroupNavigator.GetWalkingPersons();
      this.thisindex = -1;
      for (int index = 0; index < walkingPersons.Count; ++index)
      {
        if (walkingPersons[index].simperson == person_.simperson)
          this.thisindex = index;
      }
      this.portraitrow.Add(walkingPersons[this.thisindex].thispersontype, AnimalType.None);
      for (int index = 0; index < walkingPersons.Count; ++index)
      {
        if (index != this.thisindex)
          this.portraitrow.Add(walkingPersons[index].thispersontype, AnimalType.None);
      }
      this.bar = new LabelledBar(0.6f, ColourData.ACDarkerBlue, "Determination", this.basescale, false);
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale += this.reason.GetSize();
      this.framescale.Y += this.bar.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.portraitrow.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale.Y += vector2_1.Y;
      Vector2 vector2_2 = -0.5f * this.framescale + defaultBuffer;
      vector2_2.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.reason.Location = vector2_2;
      vector2_2.Y += this.reason.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.groupstrLoc = vector2_2 + 0.5f * vector2_1;
      vector2_2.Y += vector2_1.Y;
      this.portraitrow.location = vector2_2 + 0.5f * this.portraitrow.GetSize();
      vector2_2.Y += this.portraitrow.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.bar.location = vector2_2;
      vector2_2.Y += this.bar.GetSize().Y;
      this.heading.SetTextPosition(this.framescale);
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
    }

    public override Vector2 GetSize() => this.framescale;

    public bool UpdateProtestorInfo(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset) => this.DrawProtestorInfo(spritebatch, offset);

    public void DrawProtestorInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.reason.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.bar.DrawLabelledBar(offset, spritebatch);
      this.portraitrow.DrawPortraitRow(spritebatch, offset);
      TextFunctions.DrawJustifiedText(this.groupstr, this.basescale, offset + this.groupstrLoc, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
    }
  }
}

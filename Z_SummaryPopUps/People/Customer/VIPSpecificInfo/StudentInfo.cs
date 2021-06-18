﻿// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.StudentInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo
{
  internal class StudentInfo : VIPInfo
  {
    private int numstudents;
    private ZGenericText studentsstr;
    private ZGenericText teacherstr;
    private LabelledBar bar;
    private MiniHeading heading;
    private PortraitRow portraits;
    private PortraitRow teacher;
    private float basescale;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;
    private CustomerFrame frame;

    public StudentInfo(WalkingPerson person, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.numstudents = 5;
      this.studentsstr = new ZGenericText("Group: " + (object) this.numstudents, this.basescale, false);
      this.teacherstr = new ZGenericText("Teacher", this.basescale, false);
      this.bar = new LabelledBar(0.4f, ColourData.TopBarBlue, "Amount learned", this.basescale, false);
      this.heading = new MiniHeading(Vector2.Zero, "Info", 1f, this.basescale);
      this.portraits = new PortraitRow(6, this.basescale, this.scalehelper.ScaleX(40f));
      this.teacher = new PortraitRow(1, this.basescale, this.scalehelper.ScaleX(40f));
      this.teacher.Add((AnimalType) Game1.Rnd.Next(413, 415), AnimalType.None);
      for (int index = 0; index < 5; ++index)
        this.portraits.Add((AnimalType) Game1.Rnd.Next(433, 438), AnimalType.None);
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += this.heading.GetSize().Y;
      this.framescale.Y += defaultBuffer.Y;
      this.framescale.Y += this.teacherstr.GetSize().Y;
      this.framescale.Y += this.teacher.GetSize().Y;
      this.framescale.Y += 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.studentsstr.GetSize().Y;
      this.framescale.Y += this.portraits.GetSize().Y;
      this.framescale.Y += 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.bar.GetSize().Y;
      this.framescale.X = this.portraits.GetSize().X + 2f * defaultBuffer.X;
      this.heading.SetTextPosition(this.framescale);
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.heading.GetSize().Y + defaultBuffer.Y;
      this.teacherstr.vLocation = vector2;
      vector2.Y += this.teacherstr.GetSize().Y;
      this.teacher.location = vector2 + 0.5f * this.portraits.GetSize();
      vector2.Y += this.teacher.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.studentsstr.vLocation = vector2;
      vector2.Y += this.studentsstr.GetSize().Y;
      this.portraits.location = vector2 + 0.5f * this.portraits.GetSize();
      vector2.Y += this.portraits.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.bar.location = vector2;
      vector2.Y += this.bar.GetSize().Y;
    }

    public override Vector2 GetSize() => this.framescale;

    public override bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime) => false;

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.studentsstr.DrawZGenericText(offset, spritebatch);
      this.bar.DrawLabelledBar(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.portraits.DrawPortraitRow(spritebatch, offset);
      this.teacherstr.DrawZGenericText(offset, spritebatch);
      this.teacher.DrawPortraitRow(spritebatch, offset);
    }
  }
}

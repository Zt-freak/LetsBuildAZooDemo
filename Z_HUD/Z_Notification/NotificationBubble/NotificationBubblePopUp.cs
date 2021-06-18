// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationBubble.NotificationBubblePopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationBubble
{
  internal class NotificationBubblePopUp
  {
    private static Rectangle crownrect = new Rectangle(515, 463, 16, 14);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame panel;
    private Vector2 framescale;
    private string headingstr;
    private Vector2 headingloc;
    private SimpleTextHandler text;
    private Vector2 textsize;
    private LerpHandler_Float lerper;
    private Vector2 lerpoffset;
    private Vector2 lerpstartdisplacement;
    private AnimalInFrame portrait;
    private ZGenericUIDrawObject icon;
    private NoticicationExtraIcon icontype;
    private NotificationBubbleFrameType frametype;
    private MouseoverHandler mouseoverhandler;
    private float timer;
    private float lifetime;
    public bool clicked;
    public bool exiting;
    public bool HoldForever;
    private bool mouseover;
    internal static bool StaticForceExitCurrentBubble;
    private Vector3 textcolour;

    public NotificationBubblePopUp(
      string heading_,
      string text_,
      float basescale_,
      float lifetime_ = 2.5f,
      NotificationBubbleFrameType frametype_ = NotificationBubbleFrameType.Regular)
    {
      this.Init(heading_, text_, basescale_, lifetime_, frametype_: frametype_);
    }

    public NotificationBubblePopUp(NotificationBubbleInfo info, float basescale_, float lifetime_ = 2.5f)
    {
      if (info.HoldForever)
        lifetime_ = -1f;
      this.Init(info.heading, info.bodytext, basescale_, lifetime_, info.animalRenderDescriptor, info.frametype, info.notificationicon);
    }

    public NotificationBubblePopUp(
      WalkingPerson person,
      string heading_,
      string text_,
      float basescale_,
      float lifetime_ = 2.5f,
      NotificationBubbleFrameType frametype_ = NotificationBubbleFrameType.Regular)
    {
      AnimalRenderDescriptor descriptor = (AnimalRenderDescriptor) null;
      if (person != null)
        descriptor = new AnimalRenderDescriptor(person.thispersontype);
      this.Init(heading_, text_, basescale_, lifetime_, descriptor, frametype_);
    }

    public NotificationBubblePopUp(
      AnimalRenderDescriptor animalRenderDescriptor,
      string heading_,
      string text_,
      float basescale_,
      float lifetime_ = 2.5f,
      NotificationBubbleFrameType frametype_ = NotificationBubbleFrameType.Regular)
    {
      this.Init(heading_, text_, basescale_, lifetime_, animalRenderDescriptor, frametype_);
    }

    public NotificationBubblePopUp(
      NoticicationExtraIcon icontype_,
      string heading_,
      string text_,
      float basescale_,
      float lifetime_ = 2.5f,
      NotificationBubbleFrameType frametype_ = NotificationBubbleFrameType.Regular)
    {
      this.Init(heading_, text_, basescale_, lifetime_, frametype_: frametype_, icontype_: icontype_);
    }

    private void Init(
      string heading_,
      string text_,
      float basescale_,
      float lifetime_ = 2.5f,
      AnimalRenderDescriptor descriptor = null,
      NotificationBubbleFrameType frametype_ = NotificationBubbleFrameType.Regular,
      NoticicationExtraIcon icontype_ = NoticicationExtraIcon.None)
    {
      this.frametype = frametype_;
      this.icontype = icontype_;
      this.HoldForever = (double) this.lifetime < 0.0;
      this.lifetime = lifetime_;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.exiting = false;
      if (this.icontype != NoticicationExtraIcon.None)
      {
        if (this.icontype != NoticicationExtraIcon.Crown)
          throw new Exception("icon not added");
        this.icon = new ZGenericUIDrawObject(NotificationBubblePopUp.crownrect, this.basescale, AssetContainer.UISheet);
      }
      else if (descriptor != null)
        this.portrait = new AnimalInFrame(descriptor.bodyAnimalType, descriptor.headAnimalType, descriptor.variant, 25f);
      this.textcolour = ColourData.Z_TextBrown;
      Vector3 colour = Color.White.ToVector3();
      CustomerFrameColors color;
      switch (this.frametype)
      {
        case NotificationBubbleFrameType.Regular:
          color = CustomerFrameColors.CreamWithBorder;
          break;
        case NotificationBubbleFrameType.Gilded:
          color = CustomerFrameColors.CrownGold;
          this.textcolour = ColourData.GildedYellow;
          break;
        case NotificationBubbleFrameType.Colored_ForVariants:
          color = CustomerFrameColors.CreamWithBorder;
          colour = new Vector3(0.7f, 0.9f, 0.9f);
          break;
        default:
          throw new Exception("frame not added");
      }
      Vector2 vector2_1 = this.uiscale.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString(heading_));
      this.headingstr = heading_;
      this.textsize.X = this.uiscale.ScaleX(160f);
      this.textsize.Y = 1f * this.uiscale.ScaleY(AssetContainer.springFont.MeasureString("arbitrary string").Y);
      this.text = new SimpleTextHandler(text_, false, this.textsize.X / Sengine.HalfReferenceScreenRes.X, this.basescale, false, false);
      this.text.SetAllColours(this.textcolour);
      this.text.AutoCompleteParagraph();
      this.framescale = new Vector2();
      this.framescale.X = Math.Max(this.text.GetSize(true).X + 2f * defaultBuffer.X, this.textsize.X + 2f * defaultBuffer.X);
      this.framescale.Y = (float) ((double) vector2_1.Y + (double) this.textsize.Y + 2.0 * (double) defaultBuffer.Y);
      this.lerpstartdisplacement = new Vector2();
      this.lerpstartdisplacement.Y = this.framescale.Y + defaultBuffer.Y;
      this.mouseoverhandler = new MouseoverHandler(this.framescale.X, this.framescale.Y, this.basescale);
      this.panel = new CustomerFrame(this.framescale, color, this.basescale);
      this.panel.SetColour(colour);
      Vector2 vector2_2 = -0.5f * this.framescale + defaultBuffer;
      vector2_2.X += defaultBuffer.X;
      if (this.portrait != null)
      {
        this.portrait.Location = vector2_2 + 0.5f * this.portrait.GetSize();
        this.portrait.Location.Y = 0.0f;
        vector2_2.X += this.portrait.GetSize().X + 0.5f * defaultBuffer.X;
      }
      if (this.icon != null)
      {
        this.icon.location = vector2_2 + 0.5f * this.icon.GetSize();
        this.icon.location.Y = 0.0f;
        vector2_2.X += this.icon.GetSize().X + 0.5f * defaultBuffer.X;
      }
      this.headingloc = vector2_2;
      vector2_2.Y += vector2_1.Y;
      this.text.Location = vector2_2;
    }

    public bool CheckMouseOver(Vector2 offset, Player player)
    {
      int num = this.mouseoverhandler.UpdateMouseoverHandler(player, offset, 0.0f) ? 1 : 0;
      this.clicked = this.mouseoverhandler.Clicked;
      return num != 0;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateNotificationBubblePopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.clicked = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.lerpoffset = this.lerper.Value * this.lerpstartdisplacement;
      if ((double) this.lerper.Value == 0.0)
      {
        this.mouseover = this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime);
        this.clicked = this.mouseoverhandler.Clicked;
      }
      if (!this.exiting)
      {
        if ((!this.HoldForever || NotificationBubblePopUp.StaticForceExitCurrentBubble) && ((double) this.timer >= (double) this.lifetime || this.clicked || NotificationBubblePopUp.StaticForceExitCurrentBubble))
        {
          NotificationBubblePopUp.StaticForceExitCurrentBubble = false;
          this.lerper.SetLerp(false, 0.0f, 1f, 3f);
          this.lerpstartdisplacement.X = -this.framescale.X - this.uiscale.DefaultBuffer.X;
          this.lerpstartdisplacement.Y = 0.0f;
          this.exiting = true;
        }
      }
      else if ((double) this.lerper.Value > 0.990000009536743)
        flag = true;
      this.timer += DeltaTime;
      return flag;
    }

    public void DrawNotificationBubblePopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location + this.lerpoffset;
      this.panel.DrawCustomerFrame(offset, spritebatch);
      TextFunctions.DrawTextWithDropShadow(this.headingstr, this.basescale, offset + this.headingloc, new Color(this.textcolour), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
      if (this.portrait != null)
        this.portrait.DrawAnimalInFrame(offset, spritebatch);
      if (this.icon == null)
        return;
      this.icon.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}

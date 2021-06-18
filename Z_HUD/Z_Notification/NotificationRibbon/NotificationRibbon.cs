// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Notification;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon
{
  internal class NotificationRibbon
  {
    private static Rectangle borderRect = new Rectangle(607, 471, 21, 21);
    private static Rectangle headerRect = new Rectangle(965, 471, 12, 12);
    private static Rectangle butterRect = new Rectangle(978, 471, 12, 12);
    private static Rectangle arrowRect = new Rectangle(0, 570, 12, 7);
    private static Vector3 frameblue = new Vector3(0.2784314f, 0.6156863f, 0.6627451f);
    private static Vector3 midblue = new Vector3(0.3058824f, 0.6705883f, 0.7137255f);
    private static Vector3 lightblue = new Vector3(0.3215686f, 0.7215686f, 0.7647059f);
    private static Rectangle backbuttonrect = new Rectangle(203, 636, 17, 17);
    private static Rectangle backbuttonmouseoverrect = new Rectangle(203, 654, 17, 17);
    private LerpHandler_Float lerper;
    private CustomerFrame frame;
    private GameObjectNineSlice headernineslice;
    private GameObjectNineSlice bordernineslice;
    private GameObjectNineSlice butternineslice;
    private GameObject uparrow;
    private GameObject downarrow;
    private ZGenericButton backbutton;
    private string title;
    private Vector2 titleLoc;
    private Vector2 headingLoc;
    private NotificationScroll notificationScroll;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiScale;
    private float lerpoffset;
    private bool hasdismissbutton;
    private ButtHolder Ref_ButtHolder;
    private NotificationInfo info;
    private static Rectangle backbuttonlightrect = new Rectangle(987, 502, 26, 26);
    private static Rectangle backbuttondarkrect = new Rectangle(987, 529, 26, 26);
    private Vector2 textsize;
    private Vector2 headingsize;
    private Vector2 headernineslicesize;
    private Vector2 butternineslicesize;
    private Vector2 framesize;
    public bool Exiting;

    public NotificationRibbon(
      ButtHolder buttonpressed,
      float basescale_,
      float lerpoffset_,
      NotificationInfo notificationInfo = null,
      bool dontlerp = false)
    {
      this.Ref_ButtHolder = buttonpressed;
      this.info = notificationInfo;
      this.hasdismissbutton = false;
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(this.basescale);
      float defaultYbuffer = this.uiScale.GetDefaultYBuffer();
      this.lerpoffset = lerpoffset_;
      this.lerper = new LerpHandler_Float();
      if (dontlerp)
        this.lerper.SetLerp(true, 0.0f, 0.0f, 3f);
      else
        this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.PullInfo();
      this.title = Z_NotificationManager.GetNotificationCategoryToSTring(buttonpressed.notificationcat);
      this.backbutton = new ZGenericButton(this.basescale, TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.backbuttonrect, TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.backbuttonmouseoverrect);
      this.headernineslice = new GameObjectNineSlice(TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.headerRect, 4);
      this.headernineslice.scale = 2f * this.basescale;
      this.headernineslice.SetAllColours(TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.midblue);
      this.bordernineslice = new GameObjectNineSlice(TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.borderRect, 7);
      this.bordernineslice.scale = 2f * this.basescale;
      this.bordernineslice.SetAllColours(TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.frameblue);
      this.butternineslice = new GameObjectNineSlice(TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.butterRect, 4);
      this.butternineslice.scale = 2f * this.basescale;
      this.butternineslice.SetAllColours(TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.midblue);
      this.uiScale.ScaleVector2(AssetContainer.springFont.MeasureString("Notification"));
      this.textsize = this.uiScale.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString(this.title));
      this.headernineslicesize = new Vector2();
      this.headernineslicesize.X = this.uiScale.ScaleX(140f);
      this.headernineslicesize.Y = this.textsize.Y + 1.5f * defaultYbuffer;
      this.butternineslicesize = new Vector2();
      this.butternineslicesize.X = this.uiScale.ScaleX(140f);
      this.butternineslicesize.Y = this.headernineslicesize.Y;
      this.uparrow = new GameObject();
      this.uparrow.DrawRect = TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.arrowRect;
      this.uparrow.SetDrawOriginToCentre();
      this.downarrow = new GameObject();
      this.downarrow.DrawRect = TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.NotificationRibbon.arrowRect;
      this.downarrow.SetDrawOriginToCentre();
      this.SizeFrame();
      Vector2 zero = Vector2.Zero;
      zero.Y = -0.5f * this.GetSize().Y;
      this.headernineslice.vLocation = zero;
      this.headernineslice.vLocation.Y += 0.5f * this.headernineslicesize.Y;
      this.backbutton.location.X = (float) (0.5 * (double) this.GetSize().X - 0.75 * (double) this.uiScale.DefaultBuffer.X);
      this.backbutton.location.Y = (float) (-0.5 * (double) this.GetSize().Y + 0.75 * (double) this.uiScale.DefaultBuffer.Y);
      this.backbutton.location.X -= this.backbutton.GetSize().X * 0.5f;
      this.backbutton.location.Y += this.backbutton.GetSize().Y * 0.5f;
      this.titleLoc = -0.5f * this.GetSize() + 0.5f * this.uiScale.DefaultBuffer + 0.5f * this.textsize;
      this.titleLoc.X += 0.5f * this.uiScale.DefaultBuffer.X;
      this.titleLoc.Y += 0.5f * this.uiScale.DefaultBuffer.Y;
      zero.Y += this.textsize.Y + 1.5f * this.uiScale.GetDefaultYBuffer();
      this.uparrow.vLocation = zero;
      this.uparrow.vLocation.Y -= 0.5f * defaultYbuffer;
      this.notificationScroll.location.Y = zero.Y + 0.5f * this.notificationScroll.GetSize().Y;
      zero.Y += this.notificationScroll.GetSize().Y;
      this.downarrow.vLocation = zero;
      this.downarrow.vLocation.Y += 0.5f * defaultYbuffer;
      this.butternineslice.vLocation = zero;
      this.butternineslice.vLocation.Y += 0.5f * this.butternineslicesize.Y;
      zero.Y += defaultYbuffer;
    }

    private void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public void RefreshEntries()
    {
      this.PullInfo();
      this.SizeFrame();
    }

    private void PullInfo()
    {
      List<RibbonEntry> ribbonEntries_ = new List<RibbonEntry>();
      if (this.info != null)
      {
        List<NotificationPackage> notificationsOfThisType = this.Ref_ButtHolder.GetAllNotificationsOfThisType(this.info.notificationType);
        for (int index = 0; index < notificationsOfThisType.Count; ++index)
          ribbonEntries_.Add(new RibbonEntry(this.basescale, (NotificationInfo) null, notificationsOfThisType[index]));
      }
      else
      {
        List<NotificationInfo> notificationTypesAndCounts = this.Ref_ButtHolder.GetNotificationTypesAndCounts();
        for (int index = 0; index < notificationTypesAndCounts.Count; ++index)
        {
          List<NotificationPackage> notificationsOfThisType = this.Ref_ButtHolder.GetAllNotificationsOfThisType(notificationTypesAndCounts[index].notificationType);
          ribbonEntries_.Add(new RibbonEntry(this.basescale, notificationTypesAndCounts[index], notificationsOfThisType[0]));
        }
      }
      ribbonEntries_.Sort(new Comparison<RibbonEntry>(RibbonEntry.SortRibbonEntries));
      this.notificationScroll = new NotificationScroll(ribbonEntries_, this.Ref_ButtHolder, this.basescale);
      bool flag = false;
      foreach (RibbonEntry ribbonEntry in ribbonEntries_)
      {
        ribbonEntry.tint = flag;
        flag = !flag;
      }
    }

    private void SizeFrame()
    {
      double defaultYbuffer = (double) this.uiScale.GetDefaultYBuffer();
      float y = 0.0f + this.headernineslicesize.Y + this.notificationScroll.GetSize().Y + this.butternineslicesize.Y;
      this.framesize = new Vector2(this.uiScale.ScaleX(140f), y);
      this.frame = new CustomerFrame(this.framesize, CustomerFrameColors.White, 2f * this.basescale);
      this.frame.SetColour(new Vector3(0.3215686f, 0.7215686f, 0.7647059f));
    }

    public float GetOffset() => 1f - this.lerper.Value;

    public Vector2 GetSize() => this.frame.VSCale;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      offset.X += this.lerper.Value * this.lerpoffset;
      return this.frame.CheckCollicion(player.inputmap.PointerLocation, offset);
    }

    public bool UpdateNotificationRibbon(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out NotificationInfo notifcationpressed,
      out bool RemakeRibbon)
    {
      bool updatebuttons = (double) this.lerper.Value == 0.0;
      RemakeRibbon = false;
      Vector2 offset1 = offset + this.location;
      offset.X += this.lerper.Value * this.lerpoffset;
      notifcationpressed = (NotificationInfo) null;
      this.lerper.UpdateLerpHandler(DeltaTime);
      bool dismissAll = false;
      int num1 = this.backbutton.UpdateZGenericButton(player, offset1, DeltaTime) ? 1 : 0;
      int num2 = this.hasdismissbutton ? 1 : 0;
      if (this.CheckMouseOver(player, offset))
      {
        this.notificationScroll.UpdateScroller(player);
        Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = true;
      }
      this.notificationScroll.UpdateNotificationScroll(player, updatebuttons, dismissAll, offset1, DeltaTime, out notifcationpressed, out RemakeRibbon);
      if (num1 != 0)
      {
        this.Exiting = true;
        this.LerpOff();
      }
      return this.Exiting && (double) this.lerper.Value == 1.0;
    }

    public void DrawNotificationRibbon(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      offset.X += this.lerper.Value * this.lerpoffset;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.notificationScroll.DrawNotificationScroll(spritebatch, offset);
      this.headernineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.headernineslicesize);
      this.butternineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.butternineslicesize);
      TextFunctions.DrawJustifiedText(this.title, 1f * this.basescale, offset + this.titleLoc, new Color(new Vector3(1f)), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
      if (!this.notificationScroll.maxedup)
        this.uparrow.Draw(spritebatch, AssetContainer.SpriteSheet, offset + this.uparrow.vLocation, this.basescale, 0.0f, true);
      if (!this.notificationScroll.maxeddown)
        this.downarrow.Draw(spritebatch, AssetContainer.SpriteSheet, offset + this.downarrow.vLocation, this.basescale, 3.141593f, true);
      int num = this.hasdismissbutton ? 1 : 0;
      this.bordernineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.framesize);
      this.backbutton.DrawZGenericButton(spritebatch, offset);
    }
  }
}

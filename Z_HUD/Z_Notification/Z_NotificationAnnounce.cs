// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Z_NotificationAnnounce
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification
{
  internal class Z_NotificationAnnounce
  {
    private static Rectangle rect = new Rectangle(877, 350, 21, 21);
    private float basescale;
    public Vector2 location;
    private UIScaleHelper uiscale;
    private LerpHandler_Float lerphandler;
    private GameObjectNineSlice nineslice;
    private Vector2 framesize;
    private Vector2 textloc;
    private Vector2 lerpoffset;
    private float timer;
    private static float lifetime = 3f;
    private bool isDone;
    public NotificationPackage package;
    private ZGenericText text;
    private bool mouseover;

    public Z_NotificationAnnounce(float basescale_, NotificationPackage package_)
    {
      this.basescale = basescale_;
      float Scale = basescale_;
      SpringFont smallFont = Z_GameFlags.GetSmallFont(ref Scale);
      this.uiscale = new UIScaleHelper(this.basescale);
      this.package = package_;
      this.text = new ZGenericText(NotificationPackage.GetZ_NotificationTypeToString(this.package.notificationtype), Scale, smallFont);
      this.text.SetAllColours(ColourData.Z_TextBrown);
      this.lerphandler = new LerpHandler_Float();
      this.lerphandler.SetLerp(true, 1f, 0.0f, 3f);
      this.nineslice = new GameObjectNineSlice(Z_NotificationAnnounce.rect, 7);
      this.nineslice.scale = this.basescale;
      Vector2 size = this.text.GetSize();
      this.framesize = new Vector2(size.X + 2f * this.uiscale.GetDefaultXBuffer(), this.uiscale.ScaleY(22f));
      this.textloc = new Vector2();
      this.textloc.X = -0.5f * this.framesize.X + this.uiscale.GetDefaultXBuffer();
      this.textloc.Y = -0.5f * size.Y;
      this.lerpoffset = new Vector2();
      this.lerpoffset.X = -500f;
      this.timer = 0.0f;
      this.isDone = false;
    }

    public Vector2 GetSize() => this.framesize;

    public bool UpdateZ_NotificationAnnounce(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool kill)
    {
      this.lerphandler.UpdateLerpHandler(DeltaTime);
      kill = false;
      bool flag = false;
      this.mouseover = false;
      this.mouseover = MathStuff.CheckPointCollision(true, offset + this.location, 1f, this.framesize.X, this.framesize.Y, player.inputmap.PointerLocation);
      if (this.mouseover && (double) this.lerphandler.Value == 0.0 && ((double) this.timer > 0.200000002980232 && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0))
        flag = true;
      if ((double) this.lerphandler.Value == 0.0)
      {
        this.timer += DeltaTime;
        if ((double) this.timer >= (double) Z_NotificationAnnounce.lifetime)
        {
          this.lerphandler.SetLerp(true, 0.0f, 1f, 3f);
          this.isDone = true;
        }
      }
      if (this.isDone && (double) this.lerphandler.Value == 1.0)
        kill = true;
      return flag;
    }

    public void DrawZ_NotificationAnnounce(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      Vector2 framesize = this.framesize;
      framesize.X *= 1f - this.lerphandler.Value;
      framesize.X = Math.Max(framesize.X, this.uiscale.ScaleX((float) (Z_NotificationAnnounce.rect.Width * 2 / 3)));
      Vector2 vector2 = new Vector2(-0.5f * this.framesize.X * this.lerphandler.Value, 0.0f);
      this.nineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset + vector2, framesize);
      this.text.DrawZGenericText(offset, spritebatch, 1f - this.lerphandler.Value);
    }
  }
}

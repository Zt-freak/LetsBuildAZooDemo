// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.GoodEvilIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_MoralitySummary
{
  internal class GoodEvilIcon : GameObject
  {
    private static Rectangle goodTinyRect = new Rectangle(68, 262, 11, 10);
    private static Rectangle evilTinyRect = new Rectangle(184, 245, 11, 10);
    private static Rectangle goodTinyRectLocked = new Rectangle(68, 273, 11, 10);
    private static Rectangle evilTinyRectLocked = new Rectangle(309, 115, 11, 10);
    private static Rectangle goodBigRect = new Rectangle(182, 52, 18, 18);
    private static Rectangle evilBigRect = new Rectangle(163, 50, 18, 19);
    private static Rectangle goodBigRectLocked = new Rectangle(702, 102, 18, 18);
    private static Rectangle evilBigRectLocked = new Rectangle(683, 102, 18, 19);
    private static Rectangle hugeEvilRect = new Rectangle(321, 867, 27, 27);
    private static Rectangle hugeGoodRect = new Rectangle(341, 930, 26, 25);
    private bool isGood;
    private bool useBig;
    private bool isLocked;
    private float basescale;
    private LittleSummaryButton infoIcon;

    public GoodEvilIcon(
      bool isGoodNotEvil,
      bool isLocked_ = false,
      bool useBigIcon = false,
      float basescale_ = 1f,
      bool addInfoIcon = false,
      bool useXtraBig = false)
    {
      this.isGood = isGoodNotEvil;
      this.useBig = useBigIcon;
      this.isLocked = isLocked_;
      this.basescale = basescale_;
      if (useBigIcon)
      {
        if (isGoodNotEvil)
          this.DrawRect = this.isLocked ? GoodEvilIcon.goodBigRectLocked : GoodEvilIcon.goodBigRect;
        else
          this.DrawRect = this.isLocked ? GoodEvilIcon.evilBigRectLocked : GoodEvilIcon.evilBigRect;
      }
      else if (useXtraBig)
      {
        if (this.isLocked)
          throw new Exception("Not drawn for this size");
        if (isGoodNotEvil)
          this.DrawRect = GoodEvilIcon.hugeGoodRect;
        else
          this.DrawRect = GoodEvilIcon.hugeEvilRect;
      }
      else if (isGoodNotEvil)
        this.DrawRect = this.isLocked ? GoodEvilIcon.goodTinyRectLocked : GoodEvilIcon.goodTinyRect;
      else
        this.DrawRect = this.isLocked ? GoodEvilIcon.evilTinyRectLocked : GoodEvilIcon.evilTinyRect;
      if (!addInfoIcon)
        return;
      this.infoIcon = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: this.basescale);
      LittleSummaryButton infoIcon = this.infoIcon;
      infoIcon.vLocation = infoIcon.vLocation + this.GetSize();
    }

    public void SetAlignment(bool isGoodNotEvil)
    {
      this.isGood = isGoodNotEvil;
      if (this.useBig)
      {
        if (isGoodNotEvil)
          this.DrawRect = GoodEvilIcon.goodBigRect;
        else
          this.DrawRect = GoodEvilIcon.evilBigRect;
      }
      else if (isGoodNotEvil)
        this.DrawRect = GoodEvilIcon.goodTinyRect;
      else
        this.DrawRect = GoodEvilIcon.evilTinyRect;
    }

    public bool UpdateForInfoIcon(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.vLocation;
      return this.infoIcon != null && this.infoIcon.UpdateLittleSummaryButton(DeltaTime, player, offset);
    }

    public void DrawGoodEvilIcon(Vector2 offset, SpriteBatch spritebatch, float ScaleMult = 1f)
    {
      this.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.scale * this.basescale * ScaleMult, 1f);
      offset += this.vLocation;
      if (this.infoIcon == null)
        return;
      this.infoIcon.DrawLittleSummaryButton(offset, spritebatch);
    }

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      Vector2 vector2 = new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * this.basescale;
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }
  }
}

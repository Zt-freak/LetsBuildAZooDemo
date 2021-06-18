// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarInfo.MainBar.ShortcutHighlightIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_BarInfo.MainBar
{
  internal class ShortcutHighlightIcon : GameObject
  {
    private OffscreenPointerType thistype;

    public ShortcutHighlightIcon(OffscreenPointerType thisclass, float BaseScale)
    {
      this.thistype = thisclass;
      switch (this.thistype)
      {
        case OffscreenPointerType.GoToQuestBuilding:
          this.DrawRect = new Rectangle(557, 264, 22, 22);
          this.SetDrawOriginToCentre();
          break;
        case OffscreenPointerType.PointAtNoWater:
          this.DrawRect = new Rectangle(212, 509, 22, 22);
          break;
        case OffscreenPointerType.NoEnrichment:
          this.DrawRect = new Rectangle(557, 264, 22, 22);
          break;
        case OffscreenPointerType.JobApplicants:
          this.DrawRect = new Rectangle(0, 437, 22, 22);
          break;
        case OffscreenPointerType.NoWaterConnection:
          this.DrawRect = new Rectangle(212, 509, 22, 22);
          break;
      }
      this.scale = BaseScale;
      this.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawShortcutHighlightIcon(
      SpriteBatch spritebatch,
      Vector2 Position,
      float FrameScale)
    {
      this.bActive = false;
      switch (this.thistype)
      {
        case OffscreenPointerType.GoToQuestBuilding:
          this.bActive = PointOffScreenManager.QuestPointerIsActive();
          break;
        case OffscreenPointerType.PointAtNoWater:
          this.bActive = PointOffScreenManager.QuestPointerIsActive(OffscreenPointerType.PointAtNoWater);
          break;
        case OffscreenPointerType.NoEnrichment:
          this.bActive = PointOffScreenManager.QuestPointerIsActive(OffscreenPointerType.NoEnrichment);
          break;
        case OffscreenPointerType.JobApplicants:
          this.bActive = Z_NotificationManager.HasThisNotification(Z_NotificationType.F_AJobHasApplicants);
          break;
        case OffscreenPointerType.NoWaterConnection:
          this.bActive = PointOffScreenManager.QuestPointerIsActive(OffscreenPointerType.NoWaterConnection);
          break;
      }
      if (!this.bActive)
        return;
      Position += new Vector2(28f * FrameScale, -34f * FrameScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      Position.Y += FlashingAlpha.MediumSin * 2f;
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Position);
    }
  }
}

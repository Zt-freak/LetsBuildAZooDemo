// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ReportStamp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;
using TinyZoo.Audio;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ReportStamp
  {
    private static Rectangle stamprect = new Rectangle(353, 584, 110, 120);
    private static Rectangle shadowrect = new Rectangle(350, 704, 114, 114);
    private static Vector2 stamporigin = new Vector2();
    private ZGenericUIDrawObject stamp;
    private ZGenericUIDrawObject shadow;
    private SinLerper lerper;
    private ReportStamp.ReportStampState state;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private static Vector2 stampoffset;
    private float timer;
    private float landduration;
    private float startdelay;
    private bool animate;

    public ReportStamp(float basescale_)
    {
      this.landduration = 0.25f;
      this.startdelay = 0.4f;
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.animate = false;
      this.stamp = new ZGenericUIDrawObject(ReportStamp.stamprect, this.basescale, AssetContainer.UISheet);
      this.shadow = new ZGenericUIDrawObject(ReportStamp.shadowrect, this.basescale, AssetContainer.UISheet);
      this.stamp.Alpha = 0.0f;
      this.shadow.Alpha = 0.0f;
      this.lerper = new SinLerper();
      this.timer = 0.0f;
      ReportStamp.stampoffset = this.scalehelper.ScaleVector2(new Vector2(60f, -100f));
      this.stamp.location = ReportStamp.stampoffset;
      this.shadow.location = this.stamp.location;
      this.shadow.location.Y = 0.0f;
      this.StartAnimation();
    }

    public void StartAnimation()
    {
      this.animate = true;
      this.timer = 0.0f;
    }

    public bool UpdateReportStamp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      switch (this.state)
      {
        case ReportStamp.ReportStampState.Invisible:
          if (this.animate)
          {
            this.timer += DeltaTime;
            if ((double) this.timer > (double) this.startdelay)
            {
              this.timer = 0.0f;
              this.animate = false;
              this.lerper.SetLerp(SinCurveType.EaseIn, 0.15f, 0.0f, 1f);
              this.state = ReportStamp.ReportStampState.Descending;
              break;
            }
            break;
          }
          break;
        case ReportStamp.ReportStampState.Descending:
          this.lerper.UpdateSinLerper(DeltaTime);
          this.stamp.Alpha = this.lerper.CurrentValue;
          this.shadow.Alpha = this.lerper.CurrentValue;
          this.stamp.location = (1f - this.lerper.CurrentValue) * ReportStamp.stampoffset;
          this.shadow.location = this.stamp.location;
          this.shadow.location.Y = 0.0f;
          if (this.lerper.IsComplete)
          {
            this.state = ReportStamp.ReportStampState.Landed;
            this.timer = 0.0f;
            flag = true;
            break;
          }
          break;
        case ReportStamp.ReportStampState.Landed:
          if ((double) this.timer < (double) this.landduration)
          {
            this.timer += DeltaTime;
            break;
          }
          this.lerper.SetLerp(SinCurveType.EaseIn, 0.15f, 1f, 0.0f);
          this.state = ReportStamp.ReportStampState.Ascending;
          this.timer = 0.0f;
          break;
        case ReportStamp.ReportStampState.Ascending:
          this.lerper.UpdateSinLerper(DeltaTime);
          this.stamp.Alpha = this.lerper.CurrentValue;
          this.shadow.Alpha = this.lerper.CurrentValue;
          this.stamp.location = (1f - this.lerper.CurrentValue) * ReportStamp.stampoffset;
          this.shadow.location = this.stamp.location;
          this.shadow.location.Y = 0.0f;
          if (this.lerper.IsComplete)
          {
            this.state = ReportStamp.ReportStampState.Invisible;
            break;
          }
          break;
      }
      if (flag)
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.Stamp);
      return flag;
    }

    public void DrawReportStamp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      if (this.state == ReportStamp.ReportStampState.Invisible)
        return;
      this.shadow.DrawZGenericUIDrawObject(spritebatch, offset);
      this.stamp.DrawZGenericUIDrawObject(spritebatch, offset);
    }

    private enum ReportStampState
    {
      Invisible,
      Descending,
      Landed,
      Ascending,
    }
  }
}

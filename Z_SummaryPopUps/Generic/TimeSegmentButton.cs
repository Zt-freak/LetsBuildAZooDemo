// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.Generic.TimeSegmentButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI.Toggler;

namespace TinyZoo.Z_SummaryPopUps.Generic
{
  internal class TimeSegmentButton
  {
    public TimeSegmentType SegmentType;
    public TogglerWithText priceadjsueter;

    public TimeSegmentButton(float _OverallMultiplier = 1f, float BaseScale = -1f)
    {
      float num = 1f;
      if (DebugFlags.IsPCVersion)
        num = 0.75f;
      Vector2 reductionMultiplier = Sengine.ScreenRationReductionMultiplier;
      string[] strArray = new string[4];
      for (int index = 0; index < 4; ++index)
        strArray[index] = TimeSegmentButton.GetTimeSegmentToString((TimeSegmentType) index);
      this.priceadjsueter = new TogglerWithText(BaseScale, 1024f, AssetContainer.SpringFontX1AndHalf, strArray);
    }

    public bool UpdateTimeSegmentButton(Player player, Vector2 Offset, float DeltaTime)
    {
      if (!this.priceadjsueter.UpdateTogglerWithText(player, DeltaTime, Offset))
        return false;
      this.SegmentType = (TimeSegmentType) this.priceadjsueter.currentIndex;
      return true;
    }

    public static string GetTimeSegmentToString(TimeSegmentType timeSegment)
    {
      switch (timeSegment)
      {
        case TimeSegmentType.Today:
          return "Today";
        case TimeSegmentType.Yesterday:
          return "Yesterday";
        case TimeSegmentType.Last7Days:
          return "Last 7 Days";
        case TimeSegmentType.AllTime:
          return "All Time";
        case TimeSegmentType.SinceLastChange:
          return "Since Last Change";
        default:
          return "NA";
      }
    }

    public void DrawTimeSegmentButton(Vector2 Offset, SpriteBatch spriteBatch) => this.priceadjsueter.DrawTogglerWithText(Offset, spriteBatch);
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.SnappedPixelScale
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;

namespace TinyZoo.Utils
{
  internal class SnappedPixelScale
  {
    internal static float TextBoxTextSize = 3f;
    internal static float TextScaleFour = 4f;
    internal static float TextScaleFive = 5f;
    internal static float TextScaleSix = 6f;
    internal static float TextScaleTwo = 2f;

    internal static void UpdateAtTheStartOfEveryFrame()
    {
      if (!Orientation.RotationFlippedThisFrame)
        return;
      SnappedPixelScale.SetPixelSizes();
    }

    internal static void SetPixelSizes()
    {
      float num = SEngine.Game1.ScreenResolution.X / Sengine.ReferenceScreenRes.X;
      SnappedPixelScale.TextBoxTextSize = (float) Math.Round(3.0 * (double) num) * (Sengine.ReferenceScreenRes.X / SEngine.Game1.ScreenResolution.X);
      SnappedPixelScale.TextScaleFour = (float) Math.Round(4.0 * (double) num) * (Sengine.ReferenceScreenRes.X / SEngine.Game1.ScreenResolution.X);
      SnappedPixelScale.TextScaleFive = (float) Math.Round(5.0 * (double) num) * (Sengine.ReferenceScreenRes.X / SEngine.Game1.ScreenResolution.X);
      SnappedPixelScale.TextScaleSix = (float) Math.Round(6.0 * (double) num) * (Sengine.ReferenceScreenRes.X / SEngine.Game1.ScreenResolution.X);
      SnappedPixelScale.TextScaleTwo = (float) Math.Round(2.0 * (double) num) * (Sengine.ReferenceScreenRes.X / SEngine.Game1.ScreenResolution.X);
    }
  }
}

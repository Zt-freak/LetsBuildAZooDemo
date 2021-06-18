// Decompiled with JetBrains decompiler
// Type: TinyZoo.UIScaleSettings
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo
{
  internal class UIScaleSettings
  {
    internal static int MinUIScaleMult = 2;

    internal static int GetDefaultUIScale(out int Max)
    {
      Max = 1;
      switch ((int) SEngine.Game1.ScreenResolution.X)
      {
        case 1024:
          if ((double) SEngine.Game1.ScreenResolution.Y == 768.0)
          {
            Max = 2;
            return 2;
          }
          break;
        case 1280:
          if ((double) SEngine.Game1.ScreenResolution.Y == 1024.0)
          {
            Max = 2;
            return 2;
          }
          if ((double) SEngine.Game1.ScreenResolution.Y == 720.0)
          {
            Max = 2;
            return 2;
          }
          if ((double) SEngine.Game1.ScreenResolution.Y == 800.0)
          {
            Max = 2;
            return 2;
          }
          break;
        case 1360:
          if ((double) SEngine.Game1.ScreenResolution.Y == 768.0)
          {
            Max = 2;
            return 2;
          }
          break;
        case 1366:
          if ((double) SEngine.Game1.ScreenResolution.Y == 768.0)
          {
            Max = 2;
            return 2;
          }
          break;
        case 1440:
          if ((double) SEngine.Game1.ScreenResolution.Y == 900.0)
          {
            Max = 2;
            return 2;
          }
          break;
        case 1600:
          if ((double) SEngine.Game1.ScreenResolution.Y == 900.0)
          {
            Max = 2;
            return 2;
          }
          break;
        case 1680:
          if ((double) SEngine.Game1.ScreenResolution.Y == 1050.0)
          {
            Max = 3;
            return 3;
          }
          break;
        case 1920:
          if ((double) SEngine.Game1.ScreenResolution.Y == 1200.0)
          {
            Max = 3;
            return 3;
          }
          if ((double) SEngine.Game1.ScreenResolution.Y == 1080.0)
          {
            Max = 3;
            return 3;
          }
          break;
        case 2560:
          if ((double) SEngine.Game1.ScreenResolution.Y == 1440.0)
          {
            Max = 4;
            return 4;
          }
          if ((double) SEngine.Game1.ScreenResolution.Y == 1080.0)
          {
            Max = 3;
            return 3;
          }
          break;
        case 3440:
          if ((double) SEngine.Game1.ScreenResolution.Y == 1440.0)
          {
            Max = 4;
            return 4;
          }
          break;
        case 3840:
          if ((double) SEngine.Game1.ScreenResolution.Y == 2160.0)
          {
            Max = 6;
            return 4;
          }
          break;
      }
      if ((double) SEngine.Game1.ScreenResolution.X > 3000.0)
      {
        Max = 4;
        return 4;
      }
      if ((double) SEngine.Game1.ScreenResolution.X > 1600.0)
      {
        Max = 3;
        return 3;
      }
      Max = 2;
      return 2;
    }

    public static float GetUIScaleMultToDisplayValue(float UIScaleMult)
    {
      switch (UIScaleMult)
      {
        case 1f:
          return 0.5f;
        case 2f:
          return 1f;
        case 3f:
          return 1.5f;
        case 4f:
          return 2f;
        case 5f:
          return 2.5f;
        case 6f:
          return 3f;
        default:
          return 1f;
      }
    }
  }
}

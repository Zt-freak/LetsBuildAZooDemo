// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.ScreenRes
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Linq;

namespace TinyZoo.Utils
{
  internal class ScreenRes
  {
    internal static void initializeScreenRes(
      GraphicsDeviceManager graphics,
      GraphicsDevice _GraphicsDevice)
    {
      Flags.SetPlatformOrientationForReferenceScreenRes(true);
      graphics.IsFullScreen = true;
      if (Config.StartInSafeMode)
      {
        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
      }
      else
      {
        DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Last<DisplayMode>();
        graphics.PreferredBackBufferWidth = displayMode.Width;
        graphics.PreferredBackBufferHeight = displayMode.Height;
      }
    }
  }
}

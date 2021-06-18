// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Carbon.CarbonDrops
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_OverWorld;

namespace TinyZoo.PlayerDir.Carbon
{
  internal class CarbonDrops
  {
    private int CarbonIn;
    private int CarbonAbsorbed;

    public void DropCarbon(Vector2 Location, int _CarbonDrop)
    {
      if (FeatureFlags.BlockCarbon || Z_DebugFlags.IsBetaVersion)
        return;
      MoneyRenderer.DropCarbon(Location, _CarbonDrop);
      this.CarbonIn += _CarbonDrop;
    }

    public void AbsorbCarbon(Vector2 Location, int _CarbonAbsorbed)
    {
      if (FeatureFlags.BlockCarbon || Z_DebugFlags.IsBetaVersion)
        return;
      this.CarbonAbsorbed += _CarbonAbsorbed;
      MoneyRenderer.DropCarbon(Location, _CarbonAbsorbed * -1);
    }
  }
}

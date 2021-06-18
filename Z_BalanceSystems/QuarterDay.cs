// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.QuarterDay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_BalanceSystems.Animals;

namespace TinyZoo.Z_BalanceSystems
{
  internal class QuarterDay
  {
    internal static void StartQuarterDay(int QuarterIndex, Player player)
    {
      NewQuarterDayByPen.DoQuarterDay(player, QuarterIndex);
      player.farms.StartNewQuarterDay(player);
    }
  }
}

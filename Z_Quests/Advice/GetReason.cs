// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.Advice.GetReason
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_Quests.Advice
{
  internal class GetReason
  {
    public static int GetHungerReason(Player player, int animalUID) => Game1.Rnd.Next(0, 4);
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.JapFemaleNames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo
{
  internal class JapFemaleNames
  {
    private static List<string> Names;

    internal static string GetName(out int Choice, int ForceThis = -1)
    {
      if (JapFemaleNames.Names == null)
      {
        JapFemaleNames.Names = new List<string>();
        JapFemaleNames.Names.Add("はな");
        JapFemaleNames.Names.Add("ひまり");
        JapFemaleNames.Names.Add("あかり");
        JapFemaleNames.Names.Add("いちか");
        JapFemaleNames.Names.Add("ゆい");
        JapFemaleNames.Names.Add("あおい");
        JapFemaleNames.Names.Add("にこ");
        JapFemaleNames.Names.Add("かんな");
        JapFemaleNames.Names.Add("さな");
        JapFemaleNames.Names.Add("ゆず");
        JapFemaleNames.Names.Add("ゆいる");
        JapFemaleNames.Names.Add("ゆずき");
        JapFemaleNames.Names.Add("ゆつき");
        JapFemaleNames.Names.Add("ゆな");
        JapFemaleNames.Names.Add("ゆの");
        JapFemaleNames.Names.Add("ゆめ");
        JapFemaleNames.Names.Add("もも");
        JapFemaleNames.Names.Add("あんな");
        JapFemaleNames.Names.Add("あんず");
        JapFemaleNames.Names.Add("さくら");
        JapFemaleNames.Names.Add("りん");
        JapFemaleNames.Names.Add("めい");
        JapFemaleNames.Names.Add("あお");
        JapFemaleNames.Names.Add("つむぎ");
        JapFemaleNames.Names.Add("りこ");
        JapFemaleNames.Names.Add("あい");
        JapFemaleNames.Names.Add("あいこ");
        JapFemaleNames.Names.Add("あいみ");
        JapFemaleNames.Names.Add("あいり");
        JapFemaleNames.Names.Add("あけみ");
        JapFemaleNames.Names.Add("あやか");
        JapFemaleNames.Names.Add("あやみ");
        JapFemaleNames.Names.Add("あやの");
        JapFemaleNames.Names.Add("べにこ");
        JapFemaleNames.Names.Add("ちなつ");
        JapFemaleNames.Names.Add("ちよ");
        JapFemaleNames.Names.Add("ちよこ");
        JapFemaleNames.Names.Add("えみ");
        JapFemaleNames.Names.Add("ふみこ");
        JapFemaleNames.Names.Add("はるか");
        JapFemaleNames.Names.Add("はるな");
        JapFemaleNames.Names.Add("ひな");
        JapFemaleNames.Names.Add("ひろこ");
        JapFemaleNames.Names.Add("ほたる");
        JapFemaleNames.Names.Add("かおり");
        JapFemaleNames.Names.Add("かすみ");
        JapFemaleNames.Names.Add("けいこ");
        JapFemaleNames.Names.Add("こはる");
        JapFemaleNames.Names.Add("くみこ");
        JapFemaleNames.Names.Add("まい");
        JapFemaleNames.Names.Add("まみ");
        JapFemaleNames.Names.Add("まな");
        JapFemaleNames.Names.Add("まゆみ");
        JapFemaleNames.Names.Add("めぐみ");
        JapFemaleNames.Names.Add("みどり");
        JapFemaleNames.Names.Add("みな");
        JapFemaleNames.Names.Add("みさき");
        JapFemaleNames.Names.Add("みゆ");
        JapFemaleNames.Names.Add("もえ");
        JapFemaleNames.Names.Add("ももこ");
        JapFemaleNames.Names.Add("なおこ");
        JapFemaleNames.Names.Add("なおみ");
        JapFemaleNames.Names.Add("のりこ");
        JapFemaleNames.Names.Add("れいか");
        JapFemaleNames.Names.Add("さちこ");
        JapFemaleNames.Names.Add("さだこ");
        JapFemaleNames.Names.Add("さき");
        JapFemaleNames.Names.Add("さつき");
        JapFemaleNames.Names.Add("さや");
        JapFemaleNames.Names.Add("さゆり");
        JapFemaleNames.Names.Add("せつこ");
        JapFemaleNames.Names.Add("しげこ");
        JapFemaleNames.Names.Add("しずか");
        JapFemaleNames.Names.Add("ていこ");
        JapFemaleNames.Names.Add("ともこ");
        JapFemaleNames.Names.Add("ともみ");
        JapFemaleNames.Names.Add("うめこ");
        JapFemaleNames.Names.Add("よこ");
        JapFemaleNames.Names.Add("よしこ");
        JapFemaleNames.Names.Add("ようこ");
        JapFemaleNames.Names.Add("ゆあ");
        JapFemaleNames.Names.Add("ゆきこ");
        JapFemaleNames.Names.Add("ゆうま");
        JapFemaleNames.Names.Add("ゆみ");
        JapFemaleNames.Names.Add("ゆみこ");
        JapFemaleNames.Names.Add("ゆりこ");
        JapFemaleNames.Names.Add("ゆうか");
        JapFemaleNames.Names.Add("ゆうな");
        JapFemaleNames.Names.Add("ゆづき");
        JapFemaleNames.Names.Add("あいか");
        JapFemaleNames.Names.Add("あかね");
        JapFemaleNames.Names.Add("あき");
        JapFemaleNames.Names.Add("あさみ");
        JapFemaleNames.Names.Add("あすが");
        JapFemaleNames.Names.Add("あすか");
        JapFemaleNames.Names.Add("あやめ");
        JapFemaleNames.Names.Add("あやね");
        JapFemaleNames.Names.Add("ちずえ");
        JapFemaleNames.Names.Add("まち");
        JapFemaleNames.Names.Add("ちゅや");
        JapFemaleNames.Names.Add("ちひろ");
        JapFemaleNames.Names.Add("えつど");
        JapFemaleNames.Names.Add("はなえ");
        JapFemaleNames.Names.Add("はなこ");
        JapFemaleNames.Names.Add("のぞみ");
        JapFemaleNames.Names.Add("ことは");
        JapFemaleNames.Names.Add("かえで");
        JapFemaleNames.Names.Add("みづき");
        JapFemaleNames.Names.Add("みお");
        JapFemaleNames.Names.Add("しの");
        JapFemaleNames.Names.Add("すず");
        JapFemaleNames.Names.Add("ひかり");
        JapFemaleNames.Names.Add("みゆう");
        JapFemaleNames.Names.Add("こころ");
        JapFemaleNames.Names.Add("さら");
        JapFemaleNames.Names.Add("ことね");
        JapFemaleNames.Names.Add("うた");
        JapFemaleNames.Names.Add("りお");
        JapFemaleNames.Names.Add("ゆいな");
        JapFemaleNames.Names.Add("いろは");
        JapFemaleNames.Names.Add("ももか");
        JapFemaleNames.Names.Add("しずく");
        JapFemaleNames.Names.Add("かのん");
        JapFemaleNames.Names.Add("ひなた");
        JapFemaleNames.Names.Add("すみれ");
        JapFemaleNames.Names.Add("わか");
        JapFemaleNames.Names.Add("のどか");
        JapFemaleNames.Names.Add("みおり");
        JapFemaleNames.Names.Add("あいな");
        JapFemaleNames.Names.Add("ななみ");
        JapFemaleNames.Names.Add("りの");
        JapFemaleNames.Names.Add("らん");
        JapFemaleNames.Names.Add("ひなの");
        JapFemaleNames.Names.Add("みこと");
        JapFemaleNames.Names.Add("なぎ");
        JapFemaleNames.Names.Add("しおり");
        JapFemaleNames.Names.Add("ゆずは");
        JapFemaleNames.Names.Add("かほ");
        JapFemaleNames.Names.Add("れいな");
        JapFemaleNames.Names.Add("れな");
        JapFemaleNames.Names.Add("りほ");
        JapFemaleNames.Names.Add("えま");
        JapFemaleNames.Names.Add("りな");
        JapFemaleNames.Names.Add("はづき");
        JapFemaleNames.Names.Add("わかな");
        JapFemaleNames.Names.Add("みう");
        JapFemaleNames.Names.Add("みわ");
        JapFemaleNames.Names.Add("ひさき");
        JapFemaleNames.Names.Add("ふみの");
      }
      if (ForceThis == -1)
        ForceThis = Game1.Rnd.Next(0, JapFemaleNames.Names.Count);
      Choice = ForceThis;
      return JapFemaleNames.Names[ForceThis % JapFemaleNames.Names.Count];
    }
  }
}

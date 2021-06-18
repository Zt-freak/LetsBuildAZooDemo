// Decompiled with JetBrains decompiler
// Type: TinyZoo.JapMaleNames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo
{
  internal class JapMaleNames
  {
    private static List<string> Names;

    internal static string GetMaleName(out int Choice, int ForceThis = -1)
    {
      if (JapMaleNames.Names == null)
      {
        JapMaleNames.Names = new List<string>();
        JapMaleNames.Names.Add("はると");
        JapMaleNames.Names.Add("りく");
        JapMaleNames.Names.Add("ひなた");
        JapMaleNames.Names.Add("かいと");
        JapMaleNames.Names.Add("そら");
        JapMaleNames.Names.Add("れお");
        JapMaleNames.Names.Add("ゆうと");
        JapMaleNames.Names.Add("とうま");
        JapMaleNames.Names.Add("れん ");
        JapMaleNames.Names.Add("みなと ");
        JapMaleNames.Names.Add("やまと");
        JapMaleNames.Names.Add("だいと ");
        JapMaleNames.Names.Add("だいや");
        JapMaleNames.Names.Add("なごみ");
        JapMaleNames.Names.Add("ひろと");
        JapMaleNames.Names.Add("おうが");
        JapMaleNames.Names.Add("たいが");
        JapMaleNames.Names.Add("たいし");
        JapMaleNames.Names.Add("あきと");
        JapMaleNames.Names.Add("はるひ");
        JapMaleNames.Names.Add("ひかる");
        JapMaleNames.Names.Add("ゆま");
        JapMaleNames.Names.Add("はるま ");
        JapMaleNames.Names.Add("ゆうま");
        JapMaleNames.Names.Add("ゆうしん");
        JapMaleNames.Names.Add("みき");
        JapMaleNames.Names.Add("いつき");
        JapMaleNames.Names.Add("いずき");
        JapMaleNames.Names.Add("たつき");
        JapMaleNames.Names.Add("ようた");
        JapMaleNames.Names.Add("はるた");
        JapMaleNames.Names.Add("あさひ");
        JapMaleNames.Names.Add("あきお");
        JapMaleNames.Names.Add("あきら");
        JapMaleNames.Names.Add("だいち");
        JapMaleNames.Names.Add("だいき");
        JapMaleNames.Names.Add("だいすけ");
        JapMaleNames.Names.Add("えいた");
        JapMaleNames.Names.Add("ごう");
        JapMaleNames.Names.Add("はるか");
        JapMaleNames.Names.Add("はやと");
        JapMaleNames.Names.Add("ひろし");
        JapMaleNames.Names.Add("いさむ");
        JapMaleNames.Names.Add("かん");
        JapMaleNames.Names.Add("かつみ");
        JapMaleNames.Names.Add("かずき");
        JapMaleNames.Names.Add("かずこ");
        JapMaleNames.Names.Add("かずみ");
        JapMaleNames.Names.Add("けいすけ");
        JapMaleNames.Names.Add("けんた");
        JapMaleNames.Names.Add("こたろう");
        JapMaleNames.Names.Add("こうへい");
        JapMaleNames.Names.Add("まこと");
        JapMaleNames.Names.Add("まなぶ");
        JapMaleNames.Names.Add("まさひろ");
        JapMaleNames.Names.Add("まさお");
        JapMaleNames.Names.Add("まさる");
        JapMaleNames.Names.Add("みなと");
        JapMaleNames.Names.Add("みのる");
        JapMaleNames.Names.Add("なおき");
        JapMaleNames.Names.Add("なつき");
        JapMaleNames.Names.Add("のぶ");
        JapMaleNames.Names.Add("おさむ");
        JapMaleNames.Names.Add("りょう");
        JapMaleNames.Names.Add("りゅうのすけ");
        JapMaleNames.Names.Add("さぶろ");
        JapMaleNames.Names.Add("しげる");
        JapMaleNames.Names.Add("しん");
        JapMaleNames.Names.Add("しんいち");
        JapMaleNames.Names.Add("しろ");
        JapMaleNames.Names.Add("しょう");
        JapMaleNames.Names.Add("しょうた");
        JapMaleNames.Names.Add("しゅん");
        JapMaleNames.Names.Add("そうた");
        JapMaleNames.Names.Add("そうま");
        JapMaleNames.Names.Add("すすむ");
        JapMaleNames.Names.Add("ただし");
        JapMaleNames.Names.Add("たかし");
        JapMaleNames.Names.Add("たいき");
        JapMaleNames.Names.Add("たけお");
        JapMaleNames.Names.Add("たける");
        JapMaleNames.Names.Add("たけし");
        JapMaleNames.Names.Add("たくみ");
        JapMaleNames.Names.Add("たくや");
        JapMaleNames.Names.Add("てつや");
        JapMaleNames.Names.Add("つばさ");
        JapMaleNames.Names.Add("つよし");
        JapMaleNames.Names.Add("よしお");
        JapMaleNames.Names.Add("ゆうた");
        JapMaleNames.Names.Add("ゆたか");
        JapMaleNames.Names.Add("ゆうだい");
        JapMaleNames.Names.Add("けい");
        JapMaleNames.Names.Add("ようだ");
        JapMaleNames.Names.Add("はくく");
        JapMaleNames.Names.Add("はく");
        JapMaleNames.Names.Add("はんすけ");
        JapMaleNames.Names.Add("じん");
        JapMaleNames.Names.Add("ちび");
        JapMaleNames.Names.Add("ちかふさ");
        JapMaleNames.Names.Add("ちかお");
        JapMaleNames.Names.Add("ちこ");
        JapMaleNames.Names.Add("えいと");
        JapMaleNames.Names.Add("えめい");
        JapMaleNames.Names.Add("ふみひろ");
        JapMaleNames.Names.Add("ぎち");
        JapMaleNames.Names.Add("はびき");
        JapMaleNames.Names.Add("かなた");
        JapMaleNames.Names.Add("いおり");
        JapMaleNames.Names.Add("はる");
        JapMaleNames.Names.Add("さく");
        JapMaleNames.Names.Add("はやて");
        JapMaleNames.Names.Add("あおい");
        JapMaleNames.Names.Add("がく");
        JapMaleNames.Names.Add("りくと");
        JapMaleNames.Names.Add("わたる");
        JapMaleNames.Names.Add("こう");
        JapMaleNames.Names.Add("あゆむ");
        JapMaleNames.Names.Add("あやと");
        JapMaleNames.Names.Add("けんと");
        JapMaleNames.Names.Add("かい");
        JapMaleNames.Names.Add("かえで");
        JapMaleNames.Names.Add("ゆうせい");
        JapMaleNames.Names.Add("ゆうき");
        JapMaleNames.Names.Add("はるき");
        JapMaleNames.Names.Add("たいせい");
        JapMaleNames.Names.Add("あおと");
        JapMaleNames.Names.Add("ゆいと");
        JapMaleNames.Names.Add("りつき");
        JapMaleNames.Names.Add("いぶき");
        JapMaleNames.Names.Add("いっさ");
        JapMaleNames.Names.Add("りょうま");
        JapMaleNames.Names.Add("すばる");
        JapMaleNames.Names.Add("こうだい");
        JapMaleNames.Names.Add("こうた");
        JapMaleNames.Names.Add("たすく");
        JapMaleNames.Names.Add("そうし");
        JapMaleNames.Names.Add("あおし");
        JapMaleNames.Names.Add("あらた");
        JapMaleNames.Names.Add("しゅう");
        JapMaleNames.Names.Add("しょうま");
        JapMaleNames.Names.Add("そうすけ");
        JapMaleNames.Names.Add("るい");
        JapMaleNames.Names.Add("りゅうせい");
        JapMaleNames.Names.Add("かずま");
        JapMaleNames.Names.Add("なぎ");
      }
      if (ForceThis == -1)
        ForceThis = Game1.Rnd.Next(0, JapMaleNames.Names.Count);
      Choice = ForceThis;
      return JapMaleNames.Names[ForceThis % JapMaleNames.Names.Count];
    }
  }
}

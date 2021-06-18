// Decompiled with JetBrains decompiler
// Type: TinyZoo.KoreanFemaleNames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo
{
  internal class KoreanFemaleNames
  {
    private static List<string> Names;

    internal static string GetName(out int Choice, int ForceThis = -1)
    {
      if (KoreanFemaleNames.Names == null)
      {
        KoreanFemaleNames.Names = new List<string>();
        KoreanFemaleNames.Names.Add("채원");
        KoreanFemaleNames.Names.Add("다은");
        KoreanFemaleNames.Names.Add("은경");
        KoreanFemaleNames.Names.Add("은지");
        KoreanFemaleNames.Names.Add("은주");
        KoreanFemaleNames.Names.Add("은전");
        KoreanFemaleNames.Names.Add("은서");
        KoreanFemaleNames.Names.Add("은영");
        KoreanFemaleNames.Names.Add("경희");
        KoreanFemaleNames.Names.Add("경미");
        KoreanFemaleNames.Names.Add("하은");
        KoreanFemaleNames.Names.Add("하윤");
        KoreanFemaleNames.Names.Add("희진");
        KoreanFemaleNames.Names.Add("희영");
        KoreanFemaleNames.Names.Add("혜진");
        KoreanFemaleNames.Names.Add("현주");
        KoreanFemaleNames.Names.Add("현정");
        KoreanFemaleNames.Names.Add("현영");
        KoreanFemaleNames.Names.Add("지아");
        KoreanFemaleNames.Names.Add("지혜");
        KoreanFemaleNames.Names.Add("지현");
        KoreanFemaleNames.Names.Add("지은");
        KoreanFemaleNames.Names.Add("지민");
        KoreanFemaleNames.Names.Add("지원");
        KoreanFemaleNames.Names.Add("지우");
        KoreanFemaleNames.Names.Add("지영");
        KoreanFemaleNames.Names.Add("지윤");
        KoreanFemaleNames.Names.Add("정희");
        KoreanFemaleNames.Names.Add("정화");
        KoreanFemaleNames.Names.Add("정현");
        KoreanFemaleNames.Names.Add("미경");
        KoreanFemaleNames.Names.Add("민지");
        KoreanFemaleNames.Names.Add("민정");
        KoreanFemaleNames.Names.Add("민서");
        KoreanFemaleNames.Names.Add("민선");
        KoreanFemaleNames.Names.Add("미영");
        KoreanFemaleNames.Names.Add("서현");
        KoreanFemaleNames.Names.Add("서연");
        KoreanFemaleNames.Names.Add("서윤");
        KoreanFemaleNames.Names.Add("수빈");
        KoreanFemaleNames.Names.Add("수진");
        KoreanFemaleNames.Names.Add("수민");
        KoreanFemaleNames.Names.Add("선영");
        KoreanFemaleNames.Names.Add("예은");
        KoreanFemaleNames.Names.Add("연아");
        KoreanFemaleNames.Names.Add("유진");
        KoreanFemaleNames.Names.Add("윤아");
        KoreanFemaleNames.Names.Add("윤서");
        KoreanFemaleNames.Names.Add("영희");
        KoreanFemaleNames.Names.Add("영미");
        KoreanFemaleNames.Names.Add("예지");
        KoreanFemaleNames.Names.Add("지수");
        KoreanFemaleNames.Names.Add("류진");
        KoreanFemaleNames.Names.Add("채령");
        KoreanFemaleNames.Names.Add("유나");
        KoreanFemaleNames.Names.Add("시현");
        KoreanFemaleNames.Names.Add("미아");
        KoreanFemaleNames.Names.Add("세림");
        KoreanFemaleNames.Names.Add("유림");
        KoreanFemaleNames.Names.Add("이런");
        KoreanFemaleNames.Names.Add("보라");
        KoreanFemaleNames.Names.Add("시연");
        KoreanFemaleNames.Names.Add("한동");
        KoreanFemaleNames.Names.Add("유현");
        KoreanFemaleNames.Names.Add("유빈");
        KoreanFemaleNames.Names.Add("가현");
        KoreanFemaleNames.Names.Add("혜원");
        KoreanFemaleNames.Names.Add("은비");
        KoreanFemaleNames.Names.Add("원영");
        KoreanFemaleNames.Names.Add("예나");
        KoreanFemaleNames.Names.Add("민주");
        KoreanFemaleNames.Names.Add("채연");
        KoreanFemaleNames.Names.Add("유리");
        KoreanFemaleNames.Names.Add("승연");
        KoreanFemaleNames.Names.Add("승희");
        KoreanFemaleNames.Names.Add("소은");
        KoreanFemaleNames.Names.Add("엘키");
        KoreanFemaleNames.Names.Add("은빈");
        KoreanFemaleNames.Names.Add("나연");
        KoreanFemaleNames.Names.Add("정연");
        KoreanFemaleNames.Names.Add("모모");
        KoreanFemaleNames.Names.Add("사나");
        KoreanFemaleNames.Names.Add("지효");
        KoreanFemaleNames.Names.Add("미나");
        KoreanFemaleNames.Names.Add("다현");
        KoreanFemaleNames.Names.Add("채영");
        KoreanFemaleNames.Names.Add("주희");
        KoreanFemaleNames.Names.Add("제니");
        KoreanFemaleNames.Names.Add("리사");
        KoreanFemaleNames.Names.Add("이린");
        KoreanFemaleNames.Names.Add("슬기");
        KoreanFemaleNames.Names.Add("웬디");
        KoreanFemaleNames.Names.Add("조이");
        KoreanFemaleNames.Names.Add("예림");
        KoreanFemaleNames.Names.Add("로제");
        KoreanFemaleNames.Names.Add("청하");
        KoreanFemaleNames.Names.Add("소정");
        KoreanFemaleNames.Names.Add("예린");
        KoreanFemaleNames.Names.Add("은하");
        KoreanFemaleNames.Names.Add("유주");
        KoreanFemaleNames.Names.Add("엄지");
        KoreanFemaleNames.Names.Add("용선");
        KoreanFemaleNames.Names.Add("별이");
        KoreanFemaleNames.Names.Add("휘인");
        KoreanFemaleNames.Names.Add("혜빈");
        KoreanFemaleNames.Names.Add("연우");
        KoreanFemaleNames.Names.Add("지연");
        KoreanFemaleNames.Names.Add("태하");
        KoreanFemaleNames.Names.Add("나윤");
        KoreanFemaleNames.Names.Add("정안");
        KoreanFemaleNames.Names.Add("주원");
        KoreanFemaleNames.Names.Add("아인");
        KoreanFemaleNames.Names.Add("그루");
      }
      if (ForceThis == -1)
        ForceThis = Game1.Rnd.Next(0, KoreanFemaleNames.Names.Count);
      Choice = ForceThis;
      return KoreanFemaleNames.Names[ForceThis % KoreanFemaleNames.Names.Count];
    }
  }
}

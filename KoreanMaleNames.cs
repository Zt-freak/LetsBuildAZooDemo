// Decompiled with JetBrains decompiler
// Type: TinyZoo.KoreanMaleNames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo
{
  internal class KoreanMaleNames
  {
    private static List<string> Names;

    internal static string GetMaleName(out int Choice, int ForceThis = -1)
    {
      if (KoreanMaleNames.Names == null)
      {
        KoreanMaleNames.Names = new List<string>();
        KoreanMaleNames.Names.Add("병철");
        KoreanMaleNames.Names.Add("대현");
        KoreanMaleNames.Names.Add("도현");
        KoreanMaleNames.Names.Add("동해");
        KoreanMaleNames.Names.Add("동현");
        KoreanMaleNames.Names.Add("동원");
        KoreanMaleNames.Names.Add("도윤");
        KoreanMaleNames.Names.Add("건우");
        KoreanMaleNames.Names.Add("경호");
        KoreanMaleNames.Names.Add("하준");
        KoreanMaleNames.Names.Add("현준");
        KoreanMaleNames.Names.Add("현우");
        KoreanMaleNames.Names.Add("재현");
        KoreanMaleNames.Names.Add("재영");
        KoreanMaleNames.Names.Add("지호");
        KoreanMaleNames.Names.Add("지후");
        KoreanMaleNames.Names.Add("지훈");
        KoreanMaleNames.Names.Add("진호");
        KoreanMaleNames.Names.Add("지섭");
        KoreanMaleNames.Names.Add("지원");
        KoreanMaleNames.Names.Add("주원");
        KoreanMaleNames.Names.Add("정호");
        KoreanMaleNames.Names.Add("정훈");
        KoreanMaleNames.Names.Add("준서");
        KoreanMaleNames.Names.Add("준우");
        KoreanMaleNames.Names.Add("준영");
        KoreanMaleNames.Names.Add("민규");
        KoreanMaleNames.Names.Add("민호");
        KoreanMaleNames.Names.Add("민재");
        KoreanMaleNames.Names.Add("민준");
        KoreanMaleNames.Names.Add("민석");
        KoreanMaleNames.Names.Add("민수");
        KoreanMaleNames.Names.Add("상훈");
        KoreanMaleNames.Names.Add("소준");
        KoreanMaleNames.Names.Add("승호");
        KoreanMaleNames.Names.Add("승훈");
        KoreanMaleNames.Names.Add("승현");
        KoreanMaleNames.Names.Add("승민");
        KoreanMaleNames.Names.Add("시우");
        KoreanMaleNames.Names.Add("성호");
        KoreanMaleNames.Names.Add("성훈");
        KoreanMaleNames.Names.Add("성진");
        KoreanMaleNames.Names.Add("성민");
        KoreanMaleNames.Names.Add("태현");
        KoreanMaleNames.Names.Add("우진");
        KoreanMaleNames.Names.Add("예준");
        KoreanMaleNames.Names.Add("용철");
        KoreanMaleNames.Names.Add("영재");
        KoreanMaleNames.Names.Add("윤우");
        KoreanMaleNames.Names.Add("석진");
        KoreanMaleNames.Names.Add("남준");
        KoreanMaleNames.Names.Add("윤기");
        KoreanMaleNames.Names.Add("호석");
        KoreanMaleNames.Names.Add("지민");
        KoreanMaleNames.Names.Add("태형");
        KoreanMaleNames.Names.Add("정국");
        KoreanMaleNames.Names.Add("진영");
        KoreanMaleNames.Names.Add("재범");
        KoreanMaleNames.Names.Add("잭슨");
        KoreanMaleNames.Names.Add("유겸");
        KoreanMaleNames.Names.Add("원필");
        KoreanMaleNames.Names.Add("도운");
        KoreanMaleNames.Names.Add("영현");
        KoreanMaleNames.Names.Add("방찬");
        KoreanMaleNames.Names.Add("현진");
        KoreanMaleNames.Names.Add("지성");
        KoreanMaleNames.Names.Add("창빈");
        KoreanMaleNames.Names.Add("용복");
        KoreanMaleNames.Names.Add("정인");
        KoreanMaleNames.Names.Add("정수");
        KoreanMaleNames.Names.Add("희철");
        KoreanMaleNames.Names.Add("예성");
        KoreanMaleNames.Names.Add("신동");
        KoreanMaleNames.Names.Add("혁재");
        KoreanMaleNames.Names.Add("시원");
        KoreanMaleNames.Names.Add("려욱");
        KoreanMaleNames.Names.Add("규현");
        KoreanMaleNames.Names.Add("조미");
        KoreanMaleNames.Names.Add("강인");
        KoreanMaleNames.Names.Add("헨리");
        KoreanMaleNames.Names.Add("한경");
        KoreanMaleNames.Names.Add("기범");
        KoreanMaleNames.Names.Add("후이");
        KoreanMaleNames.Names.Add("홍석");
        KoreanMaleNames.Names.Add("신원");
        KoreanMaleNames.Names.Add("여원");
        KoreanMaleNames.Names.Add("옌안");
        KoreanMaleNames.Names.Add("유토");
        KoreanMaleNames.Names.Add("키노");
        KoreanMaleNames.Names.Add("우석");
        KoreanMaleNames.Names.Add("태용");
        KoreanMaleNames.Names.Add("태일");
        KoreanMaleNames.Names.Add("영호");
        KoreanMaleNames.Names.Add("유태");
        KoreanMaleNames.Names.Add("전곤");
        KoreanMaleNames.Names.Add("도영");
        KoreanMaleNames.Names.Add("윤오");
        KoreanMaleNames.Names.Add("사성");
        KoreanMaleNames.Names.Add("정우");
        KoreanMaleNames.Names.Add("민형");
        KoreanMaleNames.Names.Add("인준");
        KoreanMaleNames.Names.Add("동혁");
        KoreanMaleNames.Names.Add("재민");
        KoreanMaleNames.Names.Add("윤형");
        KoreanMaleNames.Names.Add("준회");
        KoreanMaleNames.Names.Add("케이");
        KoreanMaleNames.Names.Add("닉쿤");
        KoreanMaleNames.Names.Add("택연");
        KoreanMaleNames.Names.Add("준호");
        KoreanMaleNames.Names.Add("찬성");
      }
      if (ForceThis == -1)
        ForceThis = Game1.Rnd.Next(0, KoreanMaleNames.Names.Count);
      Choice = ForceThis;
      return KoreanMaleNames.Names[ForceThis % KoreanMaleNames.Names.Count];
    }
  }
}

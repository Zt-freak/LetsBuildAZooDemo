// Decompiled with JetBrains decompiler
// Type: TinyZoo.Blance.ReqForPeople
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Blance
{
  internal class ReqForPeople
  {
    private static int PERSONINDEX;
    public PReq[] wantsbyperson;

    public ReqForPeople()
    {
      this.wantsbyperson = new PReq[70];
      ReqForPeople.PERSONINDEX = 0;
      this.DoPerson(1, 0, 0, 0, 0, 0, 0, 30, -1, 120);
      this.DoPerson(2, 0, 0, 0, 0, 0, 0, 40, 0, 200);
      this.DoPerson(2, 0, 0, 0, 0, 0, 0, 55, 0, 330);
      this.DoPerson(0, 3, 0, 0, 0, 0, 0, 63, 0, 438);
      this.DoPerson(3, 1, 0, 0, 0, 0, 0, 70, 0, 700);
      this.DoPerson(2, 3, 0, 0, 0, 0, 0, 83, 1, 1073);
      this.DoPerson(0, 0, 4, 0, 0, 0, 0, 90, 1, 1440);
      this.DoPerson(3, 2, 2, 0, 0, 0, 0, 98, 1, 1658);
      this.DoPerson(3, 3, 3, 0, 0, 0, 0, 103, 1, 2050);
      this.DoPerson(0, 0, 0, 5, 0, 0, 0, 108, 1, 2580);
      this.DoPerson(2, 1, 3, 5, 0, 0, 0, 113, 2, 3150);
      this.DoPerson(3, 4, 2, 3, 0, 0, 0, 118, 2, 3760);
      this.DoPerson(3, 3, 2, 4, 0, 0, 0, 120, 2, 4320);
      this.DoPerson(0, 0, 0, 0, 5, 0, 0, 122, 2, 4860);
      this.DoPerson(3, 0, 2, 3, 5, 0, 0, 125, 2, 5603);
      this.DoPerson(2, 2, 3, 3, 3, 0, 0, (int) sbyte.MaxValue, 3, 6350);
      this.DoPerson(1, 1, 1, 3, 7, 0, 0, 129, 3, 7095);
      this.DoPerson(0, 4, 0, 0, 0, 5, 0, 132, 3, 7890);
      this.DoPerson(2, 1, 2, 3, 1, 3, 0, 135, 3, 8877);
      this.DoPerson(2, 2, 1, 2, 1, 4, 0, 137, 3, 9828);
      this.DoPerson(1, 1, 1, 1, 5, 3, 0, 138, 4, 10764);
      this.DoPerson(1, 3, 4, 2, 1, 2, 0, 140, 4, 11760);
      this.DoPerson(1, 0, 0, 4, 0, 0, 5, 142, 4, 12780);
      this.DoPerson(1, 2, 2, 2, 1, 2, 3, 144, 4, 13968);
      this.DoPerson(2, 2, 3, 0, 4, 0, 2, 147, 4, 15236);
      this.DoPerson(0, 1, 3, 4, 1, 1, 3, 150, 5, 16595);
      this.DoPerson(0, 0, 4, 5, 4, 0, 0, 153, 5, 17995);
      this.DoPerson(0, 0, 0, 0, 5, 2, 5, 154, 5, 19341);
      this.DoPerson(1, 2, 2, 0, 2, 2, 4, 156, 5, 20904);
      this.DoPerson(2, 1, 3, 1, 3, 1, 1, 158, 5, 22365);
      this.DoPerson(0, 5, 1, 1, 4, 1, 0, 160, 6, 24000);
      this.DoPerson(2, 2, 0, 3, 0, 0, 5, 162, 6, 25596);
      this.DoPerson(1, 0, 3, 0, 0, 4, 4, 165, 6, 27390);
      this.DoPerson(0, 0, 3, 0, 4, 3, 2, 167, 6, 29058);
      this.DoPerson(0, 3, 0, 0, 3, 3, 3, 170, 6, 30849);
      this.DoPerson(0, 0, 0, 3, 0, 5, 5, 172, 7, 32585);
      this.DoPerson(2, 2, 2, 2, 2, 2, 2, 173, 7, 34600);
      this.DoPerson(3, 2, 1, 0, 1, 5, 1, 175, 7, 36750);
      this.DoPerson(2, 0, 3, 0, 0, 4, 4, 177, 7, 38830);
      this.DoPerson(3, 1, 1, 1, 3, 2, 1, 178, 7, 40825);
      this.DoPerson(4, 2, 2, 1, 1, 3, 0, 179, 8, 42960);
      this.DoPerson(0, 3, 0, 1, 1, 3, 3, 181, 8, 47060);
      this.DoPerson(3, 0, 0, 4, 0, 2, 3, 183, 8, 51100);
      this.DoPerson(2, 2, 0, 0, 0, 0, 4, 185, 8, 55500);
      this.DoPerson(2, 1, 1, 3, 2, 2, 1, 187, 8, 59680);
      this.DoPerson(2, 1, 2, 1, 2, 2, 2, 189, 9, 68040);
      this.DoPerson(1, 2, 1, 3, 3, 1, 1, 190, 9, 76000);
      this.DoPerson(2, 2, 2, 2, 2, 2, 2, 192, 9, 84480);
      this.DoPerson(2, 2, 2, 2, 2, 2, 2, 195, 9, 93600);
      this.DoPerson(2, 2, 2, 2, 2, 2, 2, 200, 9, 104000);
      this.DoPerson(0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
      this.DoPerson(2, 0, 0, 0, 0, 0, 0, 55, -1, 1000);
      this.DoPerson(1, 1, 3, 1, 0, 0, 0, 150, -1, 20000);
      this.DoPerson(0, 0, 0, 0, 0, 0, 0, 1, -1, 5000);
      this.DoPerson(2, 0, 2, 1, 1, 5, 0, 199, -1, 80000);
      this.DoPerson(0, 0, 0, 0, 5, 0, 5, 190, -1, 100000);
      this.DoPerson(1, 1, 1, 0, 0, 0, 0, 250, -1, 20000);
      this.DoPerson(1, 0, 0, 0, 0, 3, 0, 250, -1, 20000);
      this.DoPerson(0, 2, 2, 2, 4, 0, 1, 190, -1, 60000);
      this.DoPerson(0, 3, 3, 0, 0, 2, 2, 150, -1, 20000);
      this.DoPerson(1, 1, 0, 0, 0, 2, 0, 250, -1, 70000);
      this.DoPerson(0, 4, 2, 4, 0, 0, 0, 90, -1, 1000);
      this.DoPerson(2, 3, 3, 3, 0, 0, 0, 130, -1, 5000);
      this.DoPerson(3, 2, 3, 2, 2, 0, 0, 150, -1, 7000);
    }

    private void DoPerson(
      int LifeSupport,
      int Water,
      int Farm,
      int Kitchen,
      int Janitor,
      int Medical,
      int Security,
      int VPM,
      int CellBlock,
      int RecCost)
    {
      this.wantsbyperson[ReqForPeople.PERSONINDEX] = new PReq();
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[1] = LifeSupport * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[2] = Water * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[3] = Farm * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[4] = Kitchen * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[5] = Janitor * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[6] = Medical * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].Uses[7] = Security * 2;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].VPM.ForceSetNewValue(VPM);
      this.wantsbyperson[ReqForPeople.PERSONINDEX].CellRequirement = CellBlock;
      this.wantsbyperson[ReqForPeople.PERSONINDEX].TRCST.ForceSetNewValue(RecCost);
      ++ReqForPeople.PERSONINDEX;
    }
  }
}

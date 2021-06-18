// Decompiled with JetBrains decompiler
// Type: TinyZoo.Blance.PReq
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Objects;

namespace TinyZoo.Blance
{
  internal class PReq
  {
    public int[] Uses;
    public NumberObfiscatorV1 VPM;
    public NumberObfiscatorV1 TRCST;
    public int CellRequirement;

    public PReq()
    {
      this.VPM = new NumberObfiscatorV1();
      this.Uses = new int[10];
      this.TRCST = new NumberObfiscatorV1();
    }
  }
}

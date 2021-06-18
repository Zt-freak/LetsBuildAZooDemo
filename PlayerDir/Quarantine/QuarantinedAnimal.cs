// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Quarantine.QuarantinedAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PlayerDir.Quarantine
{
  internal class QuarantinedAnimal
  {
    public int AnimalUID;
    public int CellBlockUID;
    public int DaysInQuarantine;
    public int QuarantinePeriod_Days = -1;

    public QuarantinedAnimal(int _AnimalUID, int _CellBlockUID, int _QuarantinePeriod_Days)
    {
      this.AnimalUID = _AnimalUID;
      this.CellBlockUID = _CellBlockUID;
      this.DaysInQuarantine = 0;
      this.QuarantinePeriod_Days = _QuarantinePeriod_Days;
    }

    public void StartNewDay() => ++this.DaysInQuarantine;

    public bool GetIsAutoReadyToTransferOut() => this.QuarantinePeriod_Days != 0 && this.QuarantinePeriod_Days != -1 && this.DaysInQuarantine >= this.QuarantinePeriod_Days;

    public void SaveQuarantinedAnimal(Writer writer)
    {
      writer.WriteInt("q", this.AnimalUID);
      writer.WriteInt("q", this.CellBlockUID);
      writer.WriteInt("q", this.DaysInQuarantine);
      writer.WriteInt("q", this.QuarantinePeriod_Days);
    }

    public QuarantinedAnimal(Reader reader)
    {
      int num1 = (int) reader.ReadInt("q", ref this.AnimalUID);
      int num2 = (int) reader.ReadInt("q", ref this.CellBlockUID);
      int num3 = (int) reader.ReadInt("q", ref this.DaysInQuarantine);
      int num4 = (int) reader.ReadInt("q", ref this.QuarantinePeriod_Days);
    }
  }
}

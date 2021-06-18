// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.EVProgress
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir
{
  internal class EVProgress
  {
    public AnimalType enemytype;
    public int TargetValue;
    public int CurrentProgress;
    public DateTime endtime;
    public bool Claimed;
    public List<AnimalType> enemiescaught;

    public EVProgress(AnimalType _enemytype, int _TargetValue, DateTime _EndTime)
    {
      this.enemytype = _enemytype;
      this.TargetValue = _TargetValue;
      this.enemiescaught = new List<AnimalType>();
      this.endtime = _EndTime;
    }

    public void CapturedThis(AnimalType Capturedenemytype)
    {
      if (this.enemytype != AnimalType.None && this.enemytype != Capturedenemytype || this.CurrentProgress >= this.TargetValue)
        return;
      this.enemiescaught.Add(Capturedenemytype);
      ++this.CurrentProgress;
    }

    public bool GetClaimed() => this.Claimed;

    public void SetClained() => this.Claimed = true;

    public EVProgress(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("e", ref _out);
      this.enemytype = (AnimalType) _out;
      int num2 = (int) reader.ReadInt("e", ref this.TargetValue);
      int num3 = (int) reader.ReadInt("e", ref this.CurrentProgress);
      int num4 = (int) reader.ReadBool("e", ref this.Claimed);
      this.endtime = reader.ReadDateTime("e");
      this.enemiescaught = new List<AnimalType>();
      for (int index = 0; index < this.CurrentProgress; ++index)
      {
        int num5 = (int) reader.ReadInt("e", ref _out);
        this.enemiescaught.Add((AnimalType) _out);
      }
    }

    public void SaveEVProgress(Writer writer)
    {
      writer.WriteInt("e", (int) this.enemytype);
      writer.WriteInt("e", this.TargetValue);
      writer.WriteInt("e", this.enemiescaught.Count);
      writer.WriteBool("e", this.Claimed);
      writer.WriteDateTime("e", this.endtime);
      for (int index = 0; index < this.enemiescaught.Count; ++index)
        writer.WriteInt("e", (int) this.enemiescaught[index]);
    }
  }
}

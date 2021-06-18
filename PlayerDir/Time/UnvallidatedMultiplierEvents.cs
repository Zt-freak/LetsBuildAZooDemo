// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Time.UnvallidatedMultiplierEvents
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Objects;
using System;

namespace TinyZoo.PlayerDir.Time
{
  internal class UnvallidatedMultiplierEvents
  {
    public DateTime localtimeofevent;
    public NumberObfiscatorV1 TotalAddedOrRemoved;

    public UnvallidatedMultiplierEvents(int _TotalAddedOrRemoved)
    {
      this.TotalAddedOrRemoved = new NumberObfiscatorV1();
      this.TotalAddedOrRemoved.ForceSetNewValue(_TotalAddedOrRemoved);
      this.localtimeofevent = new DateTime(DateTime.UtcNow.Ticks);
    }

    public UnvallidatedMultiplierEvents(Reader reader)
    {
      this.localtimeofevent = reader.ReadDateTime("t");
      this.TotalAddedOrRemoved = new NumberObfiscatorV1(reader);
    }

    public void SaveUnvallidatedMultiplierEvents(Writer writer)
    {
      writer.WriteDateTime("t", this.localtimeofevent);
      this.TotalAddedOrRemoved.SaveObfiscator(writer);
    }
  }
}

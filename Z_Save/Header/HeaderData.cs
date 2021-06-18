// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Save.Header.HeaderData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_Save.Header
{
  internal class HeaderData
  {
    private List<HeaderInfo> headerinfo;
    private static int HeaderVersion;

    public HeaderData() => this.headerinfo = new List<HeaderInfo>();

    public HeaderInfo GetHeaderInfo(int SaveSlot)
    {
      for (int index = 0; index < this.headerinfo.Count; ++index)
      {
        if (this.headerinfo[index].SaveSlot == SaveSlot)
          return this.headerinfo[index];
      }
      return (HeaderInfo) null;
    }

    public HeaderData(Reader reader)
    {
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("h", ref _out1);
      int _out2 = 0;
      int num2 = (int) reader.ReadInt("h", ref _out2);
      this.headerinfo = new List<HeaderInfo>();
      for (int index = 0; index < _out2; ++index)
        this.headerinfo.Add(new HeaderInfo(reader));
    }

    public void SaveHeaderInfo(Writer writer)
    {
      writer.WriteInt("h", HeaderData.HeaderVersion);
      writer.WriteInt("h", this.headerinfo.Count);
      for (int index = 0; index < this.headerinfo.Count; ++index)
        this.headerinfo[index].SaveHeaderInfo(writer);
    }

    public HeaderInfo GetThisHeaderInfo(int SaveSlot)
    {
      for (int index = 0; index < this.headerinfo.Count; ++index)
      {
        if (this.headerinfo[index].SaveSlot == SaveSlot)
          return this.headerinfo[index];
      }
      return (HeaderInfo) null;
    }
  }
}

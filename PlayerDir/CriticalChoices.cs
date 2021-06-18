// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CriticalChoices
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PlayerDir
{
  internal class CriticalChoices
  {
    public int[] ChoiceIndexes;
    public int GoodCoicesMade;
    public int BadChoicesMade;

    public CriticalChoices()
    {
      this.ChoiceIndexes = new int[3];
      for (int index = 0; index < this.ChoiceIndexes.Length; ++index)
        this.ChoiceIndexes[index] = -1;
    }

    public void SaveCriticalChoices(Writer writer)
    {
      writer.WriteInt("c", this.ChoiceIndexes.Length);
      for (int index = 0; index < this.ChoiceIndexes.Length; ++index)
        writer.WriteInt("c", this.ChoiceIndexes[index]);
      writer.WriteInt("c", this.GoodCoicesMade);
      writer.WriteInt("c", this.BadChoicesMade);
    }

    public CriticalChoices(Reader reader, int VersionNumberForLoad)
    {
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("c", ref _out1);
      this.ChoiceIndexes = new int[3];
      for (int index = 0; index < _out1; ++index)
      {
        int _out2 = 0;
        int num2 = (int) reader.ReadInt("c", ref _out2);
        this.ChoiceIndexes[index] = _out2;
      }
      if (VersionNumberForLoad <= 28)
        return;
      int num3 = (int) reader.ReadInt("X", ref this.GoodCoicesMade);
      int num4 = (int) reader.ReadInt("X", ref this.BadChoicesMade);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Intakes
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.PlayerDir
{
  internal class Intakes
  {
    public List<IntakeInfo> intakeinfos;
    public int Wave;

    public Intakes(Player player, int _Wave = 0)
    {
      this.Wave = _Wave;
      this.intakeinfos = new List<IntakeInfo>();
      this.intakeinfos.Add(new IntakeInfo(player, ref this.Wave));
      this.intakeinfos.Add(new IntakeInfo(player, ref this.Wave));
      this.intakeinfos.Add(new IntakeInfo(player, ref this.Wave));
    }

    public void UseThis(IntakeInfo intakeinfo, Player player, bool WasHoldingCellTransfer = false)
    {
      for (int index = this.intakeinfos.Count - 1; index > -1; --index)
      {
        if (this.intakeinfos[index] == intakeinfo)
        {
          player.livestats.intakeUseForQuit = WasHoldingCellTransfer ? (IntakeInfo) null : intakeinfo;
          player.livestats.RemovedIndex = index;
          this.intakeinfos[index] = new IntakeInfo(player, ref this.Wave);
        }
      }
    }

    public void ResetForLanguage()
    {
      for (int index = this.intakeinfos.Count - 1; index > -1; --index)
        this.intakeinfos[index].ResetForLanguage();
    }

    public void SaveIntakes(Writer writer)
    {
      writer.WriteInt("i", this.Wave);
      writer.WriteInt("i", this.intakeinfos.Count);
      for (int index = 0; index < this.intakeinfos.Count; ++index)
        this.intakeinfos[index].SaveIntakeInfo(writer);
    }

    public Intakes(Reader reader)
    {
      int num1 = (int) reader.ReadInt("i", ref this.Wave);
      int _out = 0;
      int num2 = (int) reader.ReadInt("i", ref _out);
      this.intakeinfos = new List<IntakeInfo>();
      for (int index = 0; index < _out; ++index)
        this.intakeinfos.Add(new IntakeInfo(reader));
    }
  }
}

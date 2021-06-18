// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.UserKeyBindings
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Input;
using SEngine;

namespace TinyZoo.PlayerDir
{
  internal class UserKeyBindings
  {
    public Keys[] KeyUsed;

    public UserKeyBindings() => this.ResetToDefaults();

    public void SetKey()
    {
    }

    public void LoadUserKeyBindings(Reader reader)
    {
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("k", ref _out1);
      this.ResetToDefaults();
      int _out2 = 0;
      for (int index = 0; index < _out1; ++index)
      {
        int num2 = (int) reader.ReadInt("k", ref _out2);
        this.KeyUsed[index] = (Keys) _out2;
      }
    }

    public void SaveUserKeyBindings(Writer writer)
    {
      writer.WriteInt("k", this.KeyUsed.Length);
      for (int index = 0; index < this.KeyUsed.Length; ++index)
        writer.WriteInt("k", (int) this.KeyUsed[index]);
    }

    public void ResetToDefaults()
    {
      this.KeyUsed = new Keys[12];
      for (int index = 0; index < this.KeyUsed.Length; ++index)
      {
        Keys keys = Keys.None;
        switch (index)
        {
          case 0:
            keys = Keys.W;
            break;
          case 1:
            keys = Keys.A;
            break;
          case 2:
            keys = Keys.S;
            break;
          case 3:
            keys = Keys.D;
            break;
          case 4:
            keys = Keys.D1;
            break;
          case 5:
            keys = Keys.D2;
            break;
          case 6:
            keys = Keys.D3;
            break;
          case 7:
            keys = Keys.D4;
            break;
          case 8:
            keys = Keys.Escape;
            break;
          case 9:
            keys = Keys.E;
            break;
          case 10:
            keys = Keys.Q;
            break;
          case 11:
            keys = Keys.LeftShift;
            break;
        }
        this.KeyUsed[index] = keys;
      }
    }
  }
}

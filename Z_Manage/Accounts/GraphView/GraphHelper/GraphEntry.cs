// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper.GraphEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper
{
  internal class GraphEntry
  {
    public float ThisEntryValue;
    public int ColourIndex;
    public float BackBarEntryValue = -1f;
    public int BackColourIndex;

    public GraphEntry(float Value) => this.ThisEntryValue = Value;
  }
}

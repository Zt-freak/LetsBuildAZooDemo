// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper.ComaprerSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_Manage.Accounts.GraphView.GraphHelper
{
  internal class ComaprerSet
  {
    public GraphData[] ComparableGraphs;
    public string TableName;

    public ComaprerSet(string _TableName, params GraphData[] datta)
    {
      this.TableName = _TableName;
      this.ComparableGraphs = datta;
    }
  }
}

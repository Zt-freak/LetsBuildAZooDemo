// Decompiled with JetBrains decompiler
// Type: IBinaryHeapItem`1
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;

public interface IBinaryHeapItem<T> : IComparable<T>
{
  int binaryHeapIndex { get; set; }
}

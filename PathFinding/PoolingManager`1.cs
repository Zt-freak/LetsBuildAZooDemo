// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.PoolingManager`1
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo.PathFinding
{
  public class PoolingManager<T> where T : class, IPooledItem, new()
  {
    private static List<T> pool;
    private static int index;

    public PoolingManager(int size, T defaultValue = null)
    {
      PoolingManager<T>.index = 0;
      PoolingManager<T>.pool = new List<T>(size);
      if ((object) defaultValue != null)
      {
        for (int index = 0; index < size; ++index)
        {
          T obj = defaultValue;
          PoolingManager<T>.pool.Add(obj);
        }
      }
      else
      {
        for (int index = 0; index < size; ++index)
          PoolingManager<T>.pool.Add(new T());
      }
    }

    public T GetFreeItem()
    {
      for (int index = PoolingManager<T>.index; index < PoolingManager<T>.pool.Count; ++index)
      {
        T obj = PoolingManager<T>.pool[index];
        if (!obj.PooledItemInUse)
        {
          obj.PooledItemInUse = true;
          PoolingManager<T>.index = (index + 1) % PoolingManager<T>.pool.Count;
          return obj;
        }
      }
      for (int index = 0; index < PoolingManager<T>.index; ++index)
      {
        T obj = PoolingManager<T>.pool[index];
        if (!obj.PooledItemInUse)
        {
          obj.PooledItemInUse = true;
          PoolingManager<T>.index = (index + 1) % PoolingManager<T>.pool.Count;
          return obj;
        }
      }
      return default (T);
    }

    public void ReleaseItem(T item)
    {
      item.PooledItemReset();
      item.PooledItemInUse = false;
    }
  }
}

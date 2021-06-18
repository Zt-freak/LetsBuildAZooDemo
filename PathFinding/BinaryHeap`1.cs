// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.BinaryHeap`1
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.PathFinding
{
  public class BinaryHeap<T> : IPooledItem where T : IBinaryHeapItem<T>
  {
    private T[] items;
    private int count;
    private bool inUse;

    public BinaryHeap(int size) => this.items = new T[size];

    public void Add(T item)
    {
      item.binaryHeapIndex = this.count;
      this.items[this.count] = item;
      this.UpHeap(this.count);
      ++this.count;
    }

    public T PopRoot()
    {
      T obj = this.items[0];
      --this.count;
      this.items[0] = this.items[this.count];
      this.items[0].binaryHeapIndex = 0;
      this.DownHeap();
      return obj;
    }

    public void UpdateItem(int index)
    {
      T obj = this.items[index];
      this.UpHeap(obj.binaryHeapIndex);
      this.DownHeap(obj.binaryHeapIndex);
    }

    public bool Contains(T item) => item.binaryHeapIndex < this.count && object.Equals((object) item, (object) this.items[item.binaryHeapIndex]);

    private void UpHeap(int index)
    {
      T obj = this.items[index];
      for (T other = this.items[this.GetParentIndex(index)]; obj.CompareTo(other) < 0; other = this.items[this.GetParentIndex(obj.binaryHeapIndex)])
        this.Swap(obj.binaryHeapIndex, other.binaryHeapIndex);
      this.CheckHeap();
    }

    private void DownHeap(int index = 0)
    {
      T obj = this.items[index];
      while (true)
      {
        int rhs_index = -1;
        index = obj.binaryHeapIndex;
        int childIndex1 = this.GetChildIndex(index, true);
        int childIndex2 = this.GetChildIndex(index, false);
        if (childIndex1 < this.count)
        {
          T other1 = this.items[childIndex1];
          if (obj.CompareTo(other1) > 0)
            rhs_index = childIndex1;
          if (childIndex2 < this.count)
          {
            T other2 = this.items[childIndex2];
            if (obj.CompareTo(other2) > 0 && other1.CompareTo(other2) > 0)
              rhs_index = childIndex2;
          }
          if (rhs_index != -1)
            this.Swap(index, rhs_index);
          else
            break;
        }
        else
          goto label_10;
      }
      this.CheckHeap();
      return;
label_10:
      this.CheckHeap();
    }

    private void Swap(int lhs_index, int rhs_index)
    {
      if (lhs_index < 0 || lhs_index > this.count || (rhs_index < 0 || rhs_index > this.count))
        return;
      T obj1 = this.items[lhs_index];
      T obj2 = this.items[rhs_index];
      obj1.binaryHeapIndex = rhs_index;
      obj2.binaryHeapIndex = lhs_index;
      this.items[lhs_index] = obj2;
      this.items[rhs_index] = obj1;
    }

    private int GetChildIndex(int selfIndex, bool leftChild) => selfIndex * 2 + (leftChild ? 1 : 2);

    private int GetParentIndex(int selfIndex) => (selfIndex - 1) / 2;

    public int Count => this.count;

    private void CheckHeap()
    {
    }

    private bool RecCheckHeap(int index)
    {
      int childIndex1 = this.GetChildIndex(index, true);
      int childIndex2 = this.GetChildIndex(index, false);
      T obj = this.items[index];
      if (childIndex1 >= this.count)
        return true;
      if (childIndex2 >= this.count)
      {
        T other = this.items[childIndex1];
        return obj.CompareTo(other) <= 0 && this.RecCheckHeap(other.binaryHeapIndex);
      }
      T other1 = this.items[childIndex1];
      T other2 = this.items[childIndex2];
      return obj.CompareTo(other1) <= 0 && obj.CompareTo(other2) <= 0 && this.RecCheckHeap(other1.binaryHeapIndex) && this.RecCheckHeap(other2.binaryHeapIndex);
    }

    public void PooledItemReset() => this.count = 0;

    public bool PooledItemInUse
    {
      get => this.inUse;
      set => this.inUse = value;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.ResearchTree.ResearchTreeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Architcture.ResearchTree
{
  internal class ResearchTreeManager
  {
    private TreeDisplay[] treedisplays;
    private Vector2 Size;
    private Vector2 SCROLLOffset;

    public ResearchTreeManager(Player player)
    {
      this.treedisplays = new TreeDisplay[14];
      for (int index = 0; index < this.treedisplays.Length; ++index)
        this.treedisplays[index] = new TreeDisplay((CATEGORYTYPE) index, player, ref this.Size);
      this.SCROLLOffset = new Vector2(0.0f, 100f);
    }

    public void UpdateResearchTreeManager(Player player, float DeltaTime)
    {
      if (player.player.touchinput.DragActive)
      {
        this.SCROLLOffset += player.player.touchinput.DragVectorThisFrame;
        if ((double) this.SCROLLOffset.Y > 200.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
          this.SCROLLOffset.Y = 200f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        else if ((double) this.SCROLLOffset.Y < (double) this.Size.Y * -1.0 + 700.0)
          this.SCROLLOffset.Y = (float) ((double) this.Size.Y * -1.0 + 700.0);
        if ((double) this.SCROLLOffset.X > 0.0)
          this.SCROLLOffset.X = 0.0f;
        else if ((double) this.SCROLLOffset.X < (double) this.Size.X * -1.0 + 1024.0)
          this.SCROLLOffset.X = (float) ((double) this.Size.X * -1.0 + 1024.0);
      }
      for (int index1 = 0; index1 < this.treedisplays.Length; ++index1)
      {
        if (this.treedisplays[index1].UpdateTreeDisplay(player, DeltaTime, this.SCROLLOffset))
        {
          Vector2 Sizee = new Vector2();
          this.treedisplays[index1] = new TreeDisplay((CATEGORYTYPE) index1, player, ref Sizee);
          for (int index2 = 0; index2 < this.treedisplays.Length; ++index2)
          {
            if (index1 != index2)
              this.treedisplays[index2].Lock();
          }
        }
      }
    }

    public void DrawResearchTreeManager()
    {
      float Zoom = 0.5f;
      for (int index = 0; index < this.treedisplays.Length; ++index)
        this.treedisplays[index].DrawTreeDisplay(this.SCROLLOffset, Zoom);
    }
  }
}

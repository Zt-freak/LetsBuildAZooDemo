// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid.MoralityUnlockGridDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid
{
  internal class MoralityUnlockGridDisplay
  {
    public Vector2 location;
    private List<MoralityUnlockGridEntry> entries;
    private int numberPerRow;
    private Vector2 buffer;

    public MoralityUnlockGridDisplay(
      bool IsGoodNotEvil,
      Player player,
      float BaseScale,
      int _numberPerRow = 5)
    {
      this.numberPerRow = _numberPerRow;
      this.buffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.entries = new List<MoralityUnlockGridEntry>();
      List<MoralityUnlock> alignmentOrdered = MoralityUnlocksData.GetAllUnlocksForThisMoralityAlignment_Ordered(IsGoodNotEvil);
      for (int index = 0; index < alignmentOrdered.Count; ++index)
        this.entries.Add(new MoralityUnlockGridEntry(alignmentOrdered[index], player, BaseScale));
      for (int index = 0; index < this.entries.Count; ++index)
      {
        Vector2 size = this.entries[index].GetSize();
        this.entries[index].location.X = (size.X + this.buffer.X) * (float) (index % this.numberPerRow);
        this.entries[index].location.Y = (size.Y + this.buffer.Y) * (float) (index / this.numberPerRow);
        this.entries[index].location += size * 0.5f;
      }
    }

    public Vector2 GetSize(bool GetMaxExpectedWidth = true)
    {
      Vector2 size = this.entries[0].GetSize();
      int num1 = Math.Min(this.entries.Count, this.numberPerRow);
      if (GetMaxExpectedWidth)
        num1 = this.numberPerRow;
      float x = (float) ((double) size.X * (double) num1 + (double) this.buffer.X * (double) (num1 - 1));
      int num2 = Math.Max((int) Math.Ceiling((double) this.entries.Count / (double) this.numberPerRow), 1);
      float y = (float) ((double) size.Y * (double) num2 + (double) this.buffer.Y * (double) (num2 - 1));
      return new Vector2(x, y);
    }

    public void RefreshValues(Player player)
    {
      for (int index = 0; index < this.entries.Count; ++index)
        this.entries[index].RefreshValues(player);
    }

    public void UpdateMoralityUnlockGridDisplay(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.entries.Count; ++index)
        this.entries[index].UpdateMoralityUnlockGridEntry(player, DeltaTime, offset);
    }

    public void DrawMoralityUnlockGridDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.entries.Count; ++index)
        this.entries[index].DrawMoralityUnlockGridEntry(offset, spriteBatch);
    }
  }
}

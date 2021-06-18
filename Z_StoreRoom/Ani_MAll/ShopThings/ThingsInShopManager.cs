// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopThings.ThingsInShopManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopThings
{
  internal class ThingsInShopManager
  {
    public List<Animall_Entry> productentry;
    public int TotalThings;
    private float MinY;
    private Vector2 DragOffset;

    public ThingsInShopManager()
    {
      this.productentry = new List<Animall_Entry>();
      int num1 = 8;
      int num2 = 0;
      int num3 = 0;
      float _BaseScale = 0.666666f;
      for (int index = 0; index < 88; ++index)
      {
        this.productentry.Add(new Animall_Entry((AnimalFoodType) index, _BaseScale));
        float num4 = 150f * _BaseScale;
        this.productentry[index].Location = new Vector2(num4 * (float) num2, (float) (230.0 * (double) _BaseScale * (double) Sengine.ScreenRatioUpwardsMultiplier.Y + (double) num3 * (double) _BaseScale * 260.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
        this.productentry[index].Location.X += num4 * 0.5f;
        this.productentry[index].Location.X += 10f;
        ++num2;
        if (num2 >= num1)
        {
          num2 = 0;
          ++num3;
        }
      }
      this.MinY = this.productentry[this.productentry.Count - 1].Location.Y - 700f;
      this.MinY *= -1f;
      this.MinY -= _BaseScale * 260f * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void RemoveThis(AnimalFoodType animalFoodType, bool IsAll)
    {
      for (int index = 0; index < this.productentry.Count; ++index)
      {
        if (this.productentry[index].animalFoodType == animalFoodType && IsAll)
          this.productentry[index].stocknumber.Value = 0;
      }
    }

    public int GetTotalCost()
    {
      int num = 0;
      for (int index = 0; index < this.productentry.Count; ++index)
        num += this.productentry[index].CostPerUnit * this.productentry[index].stocknumber.Value;
      return num;
    }

    public void UpdateThingsInShopManager(Vector2 Offset, Player player, float DeltaTime)
    {
      float SpringWidth = 100f;
      if ((double) player.inputmap.momentumwheel.MovementThisFrame != 0.0)
      {
        SpringWidth = 0.0f;
        player.player.touchinput.DragActive = true;
        player.player.touchinput.DragVectorThisFrame.Y = player.inputmap.momentumwheel.MovementThisFrame * 0.75f;
      }
      this.DragOffset.Y += SpringDrag.UpdateSpringyDrag(player.player.touchinput.DragActive, player.player.touchinput.DragVectorThisFrame.Y, SpringWidth, this.MinY, 0.0f, this.DragOffset.Y);
      this.TotalThings = 0;
      Offset += this.DragOffset;
      for (int index = 0; index < this.productentry.Count; ++index)
      {
        this.productentry[index].UpdateAnimall_Entry(Offset, player, DeltaTime);
        this.TotalThings += this.productentry[index].stocknumber.Value;
      }
    }

    public void DrawThingsInShopManager(Vector2 Offset)
    {
      Offset += this.DragOffset;
      for (int index = 0; index < this.productentry.Count; ++index)
        this.productentry[index].DrawAnimall_Entry(Offset, AssetContainer.pointspritebatch03);
    }
  }
}

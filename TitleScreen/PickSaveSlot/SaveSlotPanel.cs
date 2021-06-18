// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSlotPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.TitleScreen.PickSaveSlot
{
  internal class SaveSlotPanel
  {
    private List<SaveSlot> slots;
    public Vector2 Location;
    public TScreenFrame tscreenframe;
    private MiniHeading miniheading;

    public SaveSlotPanel(float BaseScale)
    {
      this.slots = new List<SaveSlot>();
      int num1 = 0;
      int num2 = 0;
      float num3 = 0.0f;
      float num4 = 0.0f;
      for (int index = 0; index < 10; ++index)
      {
        this.slots.Add(new SaveSlot(BaseScale, index));
        if (index == 0)
        {
          num3 = this.slots[index].tscreenframe.VScale.X + BaseScale * 10f;
          num4 = this.slots[index].tscreenframe.VScale.Y + BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        }
        this.slots[index].Location = new Vector2(num3 * (float) num1, num4 * (float) num2);
        this.slots[index].Location.X -= 2f * num3;
        this.slots[index].Location.Y -= 0.5f * num4;
        ++num1;
        if (num1 >= 5)
        {
          num1 -= 5;
          ++num2;
        }
      }
      this.tscreenframe = new TScreenFrame(BaseScale);
      this.tscreenframe.VScale = new Vector2((float) ((double) num3 * 5.0 + (double) BaseScale * 20.0), (float) ((double) num4 * 2.0 + (double) BaseScale * 20.0));
      float num5 = 20f * BaseScale;
      this.tscreenframe.VScale.Y += num5 * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.tscreenframe.tframe.vLocation.Y -= num5;
      this.Location = new Vector2(412f, 300f);
      this.miniheading = new MiniHeading(this.tscreenframe.VScale, "Pick a save slot", BaseScale, BaseScale);
      this.miniheading.SetAllColours(0.0f, 0.0f, 0.0f);
    }

    public void UpdateSaveSlotPanel(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      for (int index = 0; index < this.slots.Count; ++index)
        this.slots[index].UpdateSaveSlot(player, DeltaTime, Offset);
    }

    public void DrawSaveSlotPanel(Vector2 Offset)
    {
      Offset += this.Location;
      this.tscreenframe.DrawTScreenFrame(Offset);
      this.miniheading.DrawMiniHeading(Offset + this.tscreenframe.tframe.vLocation);
      for (int index = 0; index < this.slots.Count; ++index)
        this.slots[index].DrawSaveSlot(Offset);
    }
  }
}

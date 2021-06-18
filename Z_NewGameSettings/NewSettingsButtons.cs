// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_NewGameSettings.NewSettingsButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_NewGameSettings
{
  internal class NewSettingsButtons
  {
    private List<NewSettingsOption> Buttons;
    private Vector2 DragOffset;
    private float Min;
    public int CurrentSelection;

    public NewSettingsButtons()
    {
      this.CurrentSelection = -1;
      this.Buttons = new List<NewSettingsOption>();
      for (int index = 0; index < 11; ++index)
      {
        this.Buttons.Add(new NewSettingsOption((NewGameButtonType) index));
        this.Buttons[index].Location = new Vector2(450f, (float) (100.0 + (double) (index * 30) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
      this.Min = this.Buttons[this.Buttons.Count - 1].Location.Y - 768f;
      this.Min *= -1f;
      this.Min -= 100f;
    }

    public bool AllAreDefault()
    {
      for (int index = 0; index < this.Buttons.Count; ++index)
      {
        if (this.Buttons[index].Index != 0)
          return false;
      }
      return true;
    }

    public bool UpdateNewSettingsButtons(Player player, float DeltaTime)
    {
      bool flag = false;
      if ((double) this.Min < 0.0)
        this.DragOffset.Y += SpringDrag.UpdateSpringyDrag(player.player.touchinput.DragActive, player.player.touchinput.DragVectorThisFrame.Y, 100f, this.Min, 0.0f, this.DragOffset.Y);
      for (int index = 0; index < this.Buttons.Count; ++index)
      {
        if (this.Buttons[index].UpdateNewSettingsOption(DeltaTime, player, this.DragOffset))
          flag = true;
        if (this.Buttons[index].button.MouseOver)
          this.CurrentSelection = index;
      }
      return flag;
    }

    public string GetSelectedString() => this.Buttons[this.CurrentSelection].GetDescription();

    public void DrawNewSettingsButtons()
    {
      for (int index = 0; index < this.Buttons.Count; ++index)
        this.Buttons[index].DrawNewSettingsOption(this.DragOffset);
    }
  }
}

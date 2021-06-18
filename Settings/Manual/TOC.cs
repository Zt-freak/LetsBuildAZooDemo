// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Manual.TOC
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Lerp;

namespace TinyZoo.Settings.Manual
{
  internal class TOC
  {
    private TextButton[] buttons;
    private LerpHandler_FloatArray lerparray;
    private bool Exiting;
    private int SelectedIndex;
    private ButtonRepeater repeater;

    public TOC()
    {
      this.Exiting = false;
      this.buttons = new TextButton[3];
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        this.buttons[index] = new TextButton(ManualPage.GetHeading((MANUALPAGETYPE) index, true), 80f);
        this.buttons[index].vLocation = new Vector2((float) (40.0 + (double) this.buttons[index].GetVScale().X * 0.5), (float) (150.0 + (double) (index * 70) * (double) Sengine.UltraWideSreenUpwardsMultiplier));
        this.buttons[index].SetLemonANdBlue();
      }
      this.lerparray = new LerpHandler_FloatArray(this.buttons.Length, 0.1f, -1f, 0.0f);
      this.repeater = new ButtonRepeater();
      this.SelectedIndex = -1;
      if (!GameFlags.IsUsingController)
        return;
      this.SelectedIndex = 0;
    }

    public MANUALPAGETYPE UpdateTOC(float DeltaTime, Player player)
    {
      MANUALPAGETYPE manualpagetype = MANUALPAGETYPE.Count;
      this.lerparray.UpdateArrayLerpers(DeltaTime);
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
      {
        switch (Direction)
        {
          case DirectionPressed.Up:
            if (this.SelectedIndex > 0)
            {
              --this.SelectedIndex;
              break;
            }
            break;
          case DirectionPressed.Down:
            if (this.SelectedIndex < this.buttons.Length - 1)
            {
              ++this.SelectedIndex;
              break;
            }
            break;
        }
        manualpagetype = (MANUALPAGETYPE) this.SelectedIndex;
      }
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        if (this.buttons[index].UpdateTextButton(player, new Vector2(this.lerparray.arrayoflerpers[index].Value * 200f, 0.0f), DeltaTime))
        {
          manualpagetype = (MANUALPAGETYPE) index;
          this.SelectedIndex = index;
        }
      }
      return manualpagetype;
    }

    public void Exit()
    {
      if (this.Exiting)
        return;
      this.Exiting = true;
      this.lerparray.LerpOff(0.05f, Target: -1f);
    }

    public bool ExitComplete() => this.Exiting && this.lerparray.IsComplete();

    public void DrawTOC(MANUALPAGETYPE CurrentPage)
    {
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        this.buttons[index].MouseOver = (MANUALPAGETYPE) index == CurrentPage;
        this.buttons[index].DrawTextButton(new Vector2(this.lerparray.arrayoflerpers[index].Value * 330f, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}

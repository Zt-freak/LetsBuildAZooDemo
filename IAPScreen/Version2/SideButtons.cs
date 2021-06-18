// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.SideButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.DragHandlers;
using TinyZoo.IAPScreen.Version2.SmallerButton;

namespace TinyZoo.IAPScreen.Version2
{
  internal class SideButtons
  {
    private SButton[] buttons;
    private int ControllerSelected;
    public OfferButtonType SelectedButton;
    private ButtonRepeater repeater;
    private SpringDrag_ZoneManager springdrag;

    public SideButtons(Player player)
    {
      int num1 = 0;
      int num2 = 0;
      this.SelectedButton = OfferButtonType.WatchTheAdvert;
      if (player.Stats.ADisabled(true, player))
      {
        num1 = 1;
        this.SelectedButton = !player.Stats.ADisabled(true, player) ? (player.Stats.Vortex() ? OfferButtonType.BuyTheGoat : OfferButtonType.TheVortexMind) : OfferButtonType.BuyTheGoat;
      }
      this.buttons = new SButton[4 - (num1 + num2)];
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        this.buttons[index] = new SButton((OfferButtonType) (index + num1));
        if (this.buttons[index].btntype == this.SelectedButton)
          this.buttons[index].IsSelected = true;
      }
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        this.buttons[index].vLocation = new Vector2(170f, 100f * Sengine.ScreenRatioUpwardsMultiplier.Y * (float) index);
        this.buttons[index].vLocation.Y += 350f * Sengine.UltraWideSreenDownardsMultiplier;
        this.buttons[index].vLocation.Y += 100f * Sengine.UltraWideSreenUpwardsMultiplier;
        this.buttons[index].vLocation.Y -= 100f;
      }
      this.repeater = new ButtonRepeater();
      this.ControllerSelected = 0;
      if ((double) this.buttons[this.buttons.Length - 1].vLocation.Y <= 600.0)
        return;
      this.springdrag = new SpringDrag_ZoneManager((float) (((double) this.buttons[this.buttons.Length - 1].vLocation.Y - 600.0) * -1.0), Vector2.Zero, new Vector2(320f, 768f));
    }

    public OfferButtonType UpdateSideButtons(Player player, float DeltaTime)
    {
      OfferButtonType offerButtonType = OfferButtonType.Count;
      if (GameFlags.IsUsingController)
      {
        DirectionPressed Direction;
        if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
        {
          switch (Direction)
          {
            case DirectionPressed.Up:
              if (this.ControllerSelected > 0)
              {
                --this.ControllerSelected;
                break;
              }
              break;
            case DirectionPressed.Down:
              if (this.ControllerSelected < this.buttons.Length - 1)
              {
                ++this.ControllerSelected;
                break;
              }
              break;
          }
        }
        if (!this.buttons[this.ControllerSelected].IsSelected)
        {
          for (int index = 0; index < this.buttons.Length; ++index)
            this.buttons[index].IsSelected = false;
          this.buttons[this.ControllerSelected].IsSelected = true;
          offerButtonType = this.buttons[this.ControllerSelected].btntype;
          this.SelectedButton = offerButtonType;
        }
      }
      Vector2 Offset = Vector2.Zero;
      if (this.springdrag != null)
      {
        this.springdrag.UpdateSpringDrag_ZoneManager(player.player.touchinput, 100f);
        Offset = this.springdrag.CurrentOffset;
      }
      for (int index1 = 0; index1 < this.buttons.Length; ++index1)
      {
        if (this.buttons[index1].UpdateSButton(player, DeltaTime, Offset) && !this.buttons[index1].IsSelected)
        {
          for (int index2 = 0; index2 < this.buttons.Length; ++index2)
            this.buttons[index2].IsSelected = false;
          this.buttons[index1].IsSelected = true;
          offerButtonType = this.buttons[index1].btntype;
          this.SelectedButton = offerButtonType;
        }
      }
      return offerButtonType;
    }

    public void DrawSideButtons()
    {
      Vector2 Offset = Vector2.Zero;
      if (this.springdrag != null)
        Offset = this.springdrag.CurrentOffset;
      for (int index = 0; index < this.buttons.Length; ++index)
        this.buttons[index].DrawSButton(Offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell.BullDozerButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell
{
  internal class BullDozerButton : GameObject
  {
    private TRC_ButtonDisplay ControllerButton;
    public GameObject dozer;
    private SEngine.ControllerButton usethisbutton;
    private bool Isleft;
    private bool IsRight;
    private Vector2 SidewaysOffset;

    public BullDozerButton(SEngine.ControllerButton _usethisbutton, bool IsTransfer = false)
    {
      this.usethisbutton = _usethisbutton;
      this.ControllerButton = new TRC_ButtonDisplay(2f);
      this.ControllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, this.usethisbutton);
      this.dozer = new GameObject();
      this.dozer.DrawRect = new Rectangle(991, 456, 22, 22);
      if (IsTransfer)
        this.dozer.DrawRect = new Rectangle(991, 64, 22, 22);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.dozer.scale = 2f;
      if (DebugFlags.IsPCVersion)
        this.dozer.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.dozer.SetDrawOriginToCentre();
      this.scale = 48f / this.dozer.scale;
    }

    public void SetAsResearchButton()
    {
      this.dozer.DrawRect = new Rectangle(910, 154, 72, 22);
      this.SetAllColours(0.1f, 0.4f, 0.9f);
    }

    public void SetRight()
    {
      this.IsRight = true;
      this.SidewaysOffset = new Vector2(30f, 0.0f);
      this.dozer.SetDrawOriginToCentre();
    }

    public void SetLeft()
    {
      this.Isleft = true;
      this.SidewaysOffset = new Vector2(-30f, 0.0f);
      this.dozer.SetDrawOriginToCentre();
    }

    public void SetAsReanimate() => this.dozer.DrawRect = new Rectangle(910, 246, 72, 22);

    public bool UpdateBullDozerButton(Vector2 Offset, Player player)
    {
      if (TinyZoo.GameFlags.IsUsingController && !player.inputmap.PressedThisFrame[15])
      {
        if (this.usethisbutton == SEngine.ControllerButton.XboxA)
        {
          if (player.inputmap.PressedThisFrame[14])
            return true;
        }
        else if (this.usethisbutton == SEngine.ControllerButton.XboxX)
        {
          if (player.inputmap.PressedThisFrame[15])
            return true;
        }
        else if (this.usethisbutton == SEngine.ControllerButton.XboxY && player.inputmap.PressedThisFrame[12])
          return true;
      }
      if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 || !MathStuff.CheckPointCollision(true, Offset + this.vLocation + this.SidewaysOffset, this.dozer.scale, (float) this.dozer.DrawRect.Width, (float) this.dozer.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
        return false;
      player.player.touchinput.ReleaseTapArray[0].X = -10000f;
      return true;
    }

    public void DrawBullDozerButton(Vector2 Offset, float ScaleMultiplier = 1f)
    {
    }
  }
}

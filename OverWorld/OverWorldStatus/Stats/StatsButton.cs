// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldStatus.Stats.StatsButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.OverWorld.OverWorldStatus.Stats
{
  internal class StatsButton
  {
    private GameObject WariningOK;
    private bool IsWarning;
    public Vector2 Location;
    private GenericBox box;
    private GameObject Frame;
    private bool IsArrow;
    private Vector2 FrameScale;
    internal static float YPosition;

    public StatsButton()
    {
      this.Frame = new GameObject();
      this.Frame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Frame.SetDrawOriginToCentre();
      this.Frame.SetAllColours(0.3f, 0.3f, 0.3f);
      this.FrameScale = new Vector2(55f, 60f);
      this.WariningOK = new GameObject();
      this.WariningOK.DrawRect = new Rectangle(363, 566, 32, 32);
      this.WariningOK.SetDrawOriginToCentre();
      this.WariningOK.scale = 1.5f;
      this.box = new GenericBox(new Vector2(50f, 50f));
      this.WariningOK.bActive = true;
      this.SetWarining();
    }

    private void SetWarining()
    {
      this.IsArrow = false;
      this.Frame.SetAllColours(0.8f, 0.2f, 0.2f);
      this.IsWarning = true;
      this.WariningOK.SetAllColours(1f, 1f, 1f);
      this.WariningOK.DrawRect = new Rectangle(363, 566, 32, 32);
    }

    private void SetOK()
    {
      this.IsArrow = false;
      this.Frame.SetAllColours(0.3f, 0.3f, 0.3f);
      this.IsWarning = false;
      this.WariningOK.SetAllColours(0.2f, 0.8f, 0.2f);
      this.WariningOK.SetAlpha(1f);
      this.WariningOK.DrawRect = new Rectangle(396, 566, 32, 32);
    }

    public void SwicthToArrow()
    {
      this.IsArrow = true;
      this.Frame.SetAllColours(0.3f, 0.3f, 0.3f);
      this.WariningOK.SetAllColours(1f, 1f, 1f);
      this.WariningOK.SetAlpha(1f);
      this.WariningOK.DrawRect = new Rectangle(429, 566, 32, 32);
    }

    public void SetInvisible() => this.WariningOK.bActive = false;

    public bool UpdateStatsButton(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      bool LerpIsComplete,
      bool IsActiveandPoppedOit)
    {
      if (!this.WariningOK.bActive && LerpIsComplete)
      {
        this.WariningOK.bActive = true;
        if (!this.IsWarning)
          this.SetOK();
        else
          this.SetWarining();
      }
      Offset += this.Location;
      if (this.IsWarning != player.livestats.consumptionstatus.SomethingIsBad)
      {
        if (player.livestats.consumptionstatus.SomethingIsBad)
          this.SetWarining();
        else
          this.SetOK();
      }
      if (this.IsWarning && !this.IsArrow)
        this.WariningOK.AlphaCycle(0.3f, 1f, 0.3f);
      this.WariningOK.UpdateColours(DeltaTime);
      if (GameFlags.IsUsingController && OverwoldMainButtons.Selected == -1)
      {
        if (OverwoldMainButtons.SelectedNeed == 0)
        {
          if (!IsActiveandPoppedOit && player.inputmap.PressedBackOnController() || player.inputmap.PressedThisFrame[0])
            return true;
        }
        else if (player.inputmap.PressedBackOnController())
        {
          OverwoldMainButtons.SelectedNeed = 0;
          return true;
        }
      }
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && (!GameFlags.IsUsingController || !player.inputmap.PressedThisFrame[15]) && MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawStatsButton(Vector2 Offset)
    {
      Offset += this.Location;
      StatsButton.YPosition = Offset.Y + this.Location.Y;
      if (!this.WariningOK.bActive)
        return;
      this.WariningOK.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
    }
  }
}

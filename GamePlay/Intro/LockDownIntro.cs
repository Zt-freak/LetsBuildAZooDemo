// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Intro.LockDownIntro
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;
using SEngine.Objects;
using TinyZoo.Tutorials;

namespace TinyZoo.GamePlay.Intro
{
  internal class LockDownIntro
  {
    private PopLerper[] Lerpers;
    private GameObject[] Objects;
    private Vector2 Location;
    private GameObject BackBar;
    private DualSinOscillator oscialltor;
    private Vector2 VSCALE;
    private float TMR;
    private LerpHandler_Float ExitLerp;
    private PopLerper poplerp;

    public LockDownIntro(TYPENAMETHING namething)
    {
      this.ExitLerp = new LerpHandler_Float();
      this.ExitLerp.SetLerp(true, 1f, 1f, 3f);
      Rectangle rectangle1 = new Rectangle(0, 456, 281, 39);
      this.VSCALE = new Vector2(900f, 200f);
      Rectangle rectangle2 = new Rectangle(0, 359, 276, 96);
      Rectangle rectangle3 = new Rectangle(0, 497, 175, 39);
      Rectangle rectangle4 = new Rectangle(146, 271, 214, 87);
      Rectangle rectangle5 = new Rectangle(325, 631, 261, 87);
      this.Location = new Vector2(512f, 300f);
      switch (namething)
      {
        case TYPENAMETHING.Win:
          rectangle1 = rectangle2;
          this.VSCALE.Y = 400f;
          this.Location.Y = 384f;
          if (GameFlags.IsArcadeMode)
          {
            rectangle1 = new Rectangle(176, 497, 218, 39);
            break;
          }
          break;
        case TYPENAMETHING.Dead:
          rectangle1 = rectangle5;
          this.VSCALE.Y = 400f;
          this.Location.Y = 384f;
          break;
        case TYPENAMETHING.NoBeam:
          rectangle1 = rectangle4;
          this.VSCALE.Y = 400f;
          this.Location.Y = 384f;
          break;
        case TYPENAMETHING.ArcadeFail:
          rectangle1 = rectangle3;
          this.VSCALE.Y = 400f;
          this.Location.Y = 384f;
          break;
      }
      int length = rectangle1.Width / 2;
      this.Objects = new GameObject[length];
      this.Lerpers = new PopLerper[length];
      float num = 4f * Sengine.UltraWideSreenDownardsMultiplier;
      for (int index = 0; index < length; ++index)
      {
        this.Objects[index] = new GameObject();
        this.Objects[index].DrawRect = rectangle1;
        this.Objects[index].DrawRect.X += index * 2;
        this.Objects[index].DrawRect.Width = 1;
        this.Objects[index].SetDrawOriginToCentre();
        this.Objects[index].vLocation.X = (float) index * num;
        this.Objects[index].scale = num;
        this.Lerpers[index] = new PopLerper();
        this.Lerpers[index].SetDelay((float) index * 0.005f);
        if (namething == TYPENAMETHING.Win)
          this.Objects[index].SetAllColours(0.3f, 0.7f, 0.3f);
        else
          this.Objects[index].SetAllColours(0.7f, 0.2f, 0.2f);
      }
      this.BackBar = new GameObject();
      this.BackBar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BackBar.SetDrawOriginToCentre();
      this.BackBar.vLocation = this.Location;
      this.oscialltor = new DualSinOscillator(1f, 1.3f);
      this.Location.X -= (float) ((double) num * (double) length * 0.5);
      this.poplerp = new PopLerper();
    }

    public bool UpdateLockDownIntro(float DeltaTime, Player player, bool lForceCOmplee = false)
    {
      if ((double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
        return false;
      bool flag = true;
      int num1 = (int) this.poplerp.OnUpdate(DeltaTime * 2f);
      for (int index = 0; index < this.Lerpers.Length; ++index)
      {
        int num2 = (int) this.Lerpers[index].OnUpdate(DeltaTime * 1.5f);
        if (!this.Lerpers[index].HasCompleted)
          flag = false;
      }
      if (flag && player != null)
      {
        this.TMR += DeltaTime;
        if (((double) this.TMR > 2.0 && TutorialManager.currenttutorial != TUTORIALTYPE.GamePlayIntro || ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[11]) || player.inputmap.PressedThisFrame[0]) && (double) this.ExitLerp.TargetValue != 0.0)
          this.ExitLerp.SetLerp(false, 1f, 0.0f, 3f, true);
      }
      else if (lForceCOmplee && (double) this.ExitLerp.TargetValue != 0.0)
        this.ExitLerp.SetLerp(false, 1f, 0.0f, 3f, true);
      this.ExitLerp.UpdateLerpHandler(DeltaTime);
      this.oscialltor.UpdateDualSinOscillator(DeltaTime * 0.2f);
      return (double) this.ExitLerp.Value == 0.0;
    }

    public void DrawLockDownIntro()
    {
      if (GameFlags.NoStrobe)
        this.BackBar.fAlpha = 0.5f;
      this.BackBar.SetAllColours(0.9f, 0.2f, 0.0f);
      this.BackBar.Draw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.oscialltor.CurrentOffset * 2f, this.VSCALE * this.poplerp.CurrentValue * this.ExitLerp.Value * Sengine.ScreenRatioUpwardsMultiplier);
      this.BackBar.SetAllColours(0.0f, 0.3f, 0.0f);
      this.BackBar.Draw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.oscialltor.CurrentOffset * -2f, this.VSCALE * this.poplerp.CurrentValue * this.ExitLerp.Value * Sengine.ScreenRatioUpwardsMultiplier);
      this.BackBar.SetAllColours(0.0f, 0.0f, 0.5f);
      this.BackBar.Draw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, new Vector2(this.oscialltor.CurrentOffset.X * -1f, this.oscialltor.CurrentOffset.Y) * 2f, this.VSCALE * this.poplerp.CurrentValue * this.ExitLerp.Value * Sengine.ScreenRatioUpwardsMultiplier);
      for (int index = 0; index < this.Lerpers.Length; ++index)
        this.Objects[index].Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Location, new Vector2(this.Objects[index].scale * 0.75f, this.Objects[index].scale * Sengine.ScreenRatioUpwardsMultiplier.Y * this.Lerpers[index].CurrentValue) * this.ExitLerp.Value);
    }
  }
}

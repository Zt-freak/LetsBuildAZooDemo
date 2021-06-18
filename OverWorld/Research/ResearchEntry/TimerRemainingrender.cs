// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Research.ResearchEntry.TimerRemainingrender
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;

namespace TinyZoo.OverWorld.Research.ResearchEntry
{
  internal class TimerRemainingrender
  {
    private GameObject BASEFRAME;
    private GameObject BASE;
    private GameObject Filling;
    private Vector2 VSCale;
    private float Fullness;
    private string TimeString;
    private TextButton Claim;
    private LerpHandler_Float ClaimerLerp;

    public TimerRemainingrender()
    {
      this.BASE = new GameObject();
      this.BASE.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BASE.SetAllColours(0.0f, 0.0f, 0.0f);
      this.VSCale = new Vector2(824f, 100f);
      this.Filling = new GameObject(this.BASE);
      this.Filling.SetAllColours(ColourData.FernLemon);
      this.BASE.vLocation = new Vector2(100f, 300f);
      this.BASEFRAME = new GameObject(this.BASE);
      this.BASEFRAME.SetAllColours(ColourData.FernLemon);
      this.Claim = new TextButton(SEngine.Localization.Localization.GetText(67), OverAllMultiplier: 1.5f);
      this.ClaimerLerp = new LerpHandler_Float();
      this.ClaimerLerp.SetLerp(true, 1f, 1f, 3f);
      this.Claim.vLocation = new Vector2(512f, 600f);
      this.Claim.CollisionEx = new Vector2(10f, 20f);
      this.Claim.AddControllerButton(ControllerButton.XboxA);
    }

    public void UpdateTimerRemainingrender(Player player, float DeltaTime)
    {
      this.Fullness = player.Stats.research.GetPercentComplete(player, out this.TimeString);
      if ((double) this.Fullness == 1.0)
      {
        this.Filling.SetAllColours(ColourData.FernGreen);
        if ((double) this.ClaimerLerp.TargetValue != 0.0)
          this.ClaimerLerp.SetLerp(true, 1f, 0.0f, 3f, true);
      }
      this.ClaimerLerp.UpdateLerpHandler(DeltaTime);
      if ((double) this.ClaimerLerp.Value != 0.0 || !this.Claim.UpdateTextButton(player, new Vector2(this.ClaimerLerp.Value * 1024f), DeltaTime))
        return;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
      TinyZoo.Game1.SetNextGameState(GAMESTATE.RewardSetUp);
      TinyZoo.Game1.screenfade.BeginFade(true);
    }

    public bool IsReadyToClaim() => (double) this.ClaimerLerp.TargetValue == 0.0;

    public void DrawTimerRemainingrender(Vector2 Offset)
    {
      this.BASEFRAME.vLocation = Vector2.Zero;
      this.BASEFRAME.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.BASE.vLocation + new Vector2(-5f, -5f), this.VSCale + new Vector2(10f, 10f));
      this.BASE.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCale);
      this.Filling.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.BASE.vLocation, this.VSCale * new Vector2(this.Fullness, 1f));
      if ((double) this.Fullness == 1.0)
        TextFunctions.DrawJustifiedText(this.TimeString, 4f, Offset + new Vector2(512f, this.BASE.vLocation.Y + 140f), this.Filling.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      else
        TextFunctions.DrawJustifiedText(this.TimeString + " " + SEngine.Localization.Localization.GetText(59), 4f, Offset + new Vector2(512f, this.BASE.vLocation.Y + 140f), this.Filling.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      if ((double) this.ClaimerLerp.Value >= 1.0)
        return;
      this.Claim.DrawTextButton(new Vector2(this.ClaimerLerp.Value * 1024f, 0.0f) + Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

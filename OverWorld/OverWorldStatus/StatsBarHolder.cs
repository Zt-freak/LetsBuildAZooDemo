// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldStatus.StatsBarHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldStatus.Stats;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.OverWorldStatus
{
  internal class StatsBarHolder
  {
    private Stats_HorizontalBar horizontalstatsbar;
    internal static LerpHandler_Float BarLerper;
    private StatsButton statsbuttn;
    private bool IsButton = true;
    private Vector2 Location;
    private Vector2 Offset;
    private bool HasUpdated;
    private TRC_ButtonDisplay contbuton;

    public StatsBarHolder(Player player)
    {
      this.horizontalstatsbar = new Stats_HorizontalBar(player);
      this.statsbuttn = new StatsButton();
      StatsBarHolder.BarLerper = new LerpHandler_Float();
      StatsBarHolder.BarLerper.SetLerp(true, 0.0f, 0.0f, 3f);
      this.Location = new Vector2(980f, 0.0f);
      this.Offset = new Vector2(StatsBarHolder.BarLerper.Value * (this.horizontalstatsbar.LengthOfBar + 20f), 0.0f);
      this.contbuton = new TRC_ButtonDisplay(2f);
      this.contbuton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxA);
    }

    public void UpdateStatsBarHolder(Player player, float DeltaTime)
    {
      if (FeatureFlags.BlockStats || OverWorldManager.IsGameIntro)
        return;
      this.HasUpdated = true;
      this.Location.Y = 100f;
      this.statsbuttn.Location = new Vector2(0.0f, 0.0f);
      StatsBarHolder.BarLerper.UpdateLerpHandler(DeltaTime);
      this.Offset = new Vector2(StatsBarHolder.BarLerper.Value * (this.horizontalstatsbar.LengthOfBar + 20f), 0.0f);
      this.Offset += this.Location;
      if ((double) StatsBarHolder.BarLerper.Value > 0.0)
        return;
      if (this.statsbuttn.UpdateStatsButton(DeltaTime, player, this.Offset + this.statsbuttn.Location, StatsBarHolder.BarLerper.IsComplete(), (double) StatsBarHolder.BarLerper.Value == 0.0) || FeatureFlags.ForceActivateStatsBar)
      {
        FeatureFlags.ForceActivateStatsBar = false;
        player.player.touchinput.ReleaseTapArray[0].X = -10000f;
        if ((double) StatsBarHolder.BarLerper.TargetValue == 0.0)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          this.statsbuttn.SwicthToArrow();
          StatsBarHolder.BarLerper.SetLerp(false, 0.0f, -1f, 3f);
          TinyZoo.GameFlags.StatsBarIsOnScreen = true;
        }
        else if ((double) StatsBarHolder.BarLerper.TargetValue == -1.0)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
          this.statsbuttn.SetInvisible();
          StatsBarHolder.BarLerper.SetLerp(false, 0.0f, 0.0f, 3f);
          TinyZoo.GameFlags.StatsBarIsOnScreen = false;
        }
      }
      this.horizontalstatsbar.UpdateStats_HorizontalBar(player, this.Offset, DeltaTime, (double) StatsBarHolder.BarLerper.TargetValue == -1.0);
    }

    public void LerpOff()
    {
      StatsBarHolder.BarLerper.SetLerp(false, 0.0f, 1f, 3f, true);
      this.statsbuttn.SetInvisible();
    }

    public void LerpOn() => StatsBarHolder.BarLerper.SetLerp(false, 0.0f, 0.0f, 3f, true);

    public void DrawStatsBarHolder()
    {
      if (!this.HasUpdated || FeatureFlags.BlockStats)
        return;
      this.horizontalstatsbar.DrawStats_HorizontalBar(this.Offset);
      this.statsbuttn.DrawStatsButton(this.Offset);
      if (!TinyZoo.GameFlags.IsUsingController || OverwoldMainButtons.Selected != -1 || OverwoldMainButtons.SelectedNeed != 0)
        return;
      this.contbuton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.statsbuttn.Location + this.Offset + new Vector2(-20f, -20f));
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.LowBeamsBeforeStart
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class LowBeamsBeforeStart
  {
    private SmartCharacterBox charactertextbox;
    private BlackOut blackout;
    private float Delay;
    private TextButton OK;
    private TextButton No;
    private LerpHandler_Float lerper;
    private bool Exiting;

    public LowBeamsBeforeStart(int BeamsAvailable)
    {
      this.Delay = 0.0f;
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.3f, 0.0f, 0.8f);
      string FirstText = "You only have " + (object) BeamsAvailable + " Lasers available for this cellblock, are you sure you want to start the Lockdown?";
      if (BeamsAvailable == 0)
        FirstText = "You have no lasers available, which means you cannot complete this LockDown. Buy more in the upgrade store, or optimise your other Cell Blocks by replaying them.";
      this.charactertextbox = new SmartCharacterBox(FirstText, AnimalType.Administrator);
      this.OK = new TextButton(SEngine.Localization.Localization.GetText(79));
      this.No = new TextButton(SEngine.Localization.Localization.GetText(80));
      this.OK.vLocation = new Vector2(662f, 300f);
      this.No.vLocation = new Vector2(362f, 300f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      this.No.CollisionEx = new Vector2(10f, 20f);
      if (BeamsAvailable == 0)
      {
        this.No.vLocation.X = 512f;
        this.OK.CollisionEx = new Vector2(10f, 20f);
        this.OK = (TextButton) null;
        this.No.SetText(nameof (OK));
      }
      if (this.OK != null)
        this.OK.AddControllerButton(ControllerButton.XboxA);
      if (this.No == null)
        return;
      this.No.AddControllerButton(ControllerButton.XboxB);
    }

    public bool UpdateLowBeamsBeforeStart(ref float DeltaTime, Player player)
    {
      if (TinyZoo.Game1.gamestate != GAMESTATE.OverWorld)
        return true;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.Delay > 0.0)
      {
        this.Delay -= DeltaTime;
      }
      else
      {
        this.blackout.UpdateColours(DeltaTime);
        if ((double) this.lerper.Value == 0.0 && !this.Exiting)
        {
          if (this.OK != null && this.OK.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
            TinyZoo.Game1.screenfade.BeginFade(true);
            TinyZoo.Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
            player.livestats.AddExtraEnemiesForNextPlay(player, player.livestats.SelectedPrisonID);
          }
          else if (this.No.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
            this.blackout.SetAlpha(true, 0.2f, 1f, 0.0f);
            this.Exiting = true;
            this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
          }
        }
        if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: this.Exiting))
        {
          player.inputmap.ClearAllInput(player);
          return true;
        }
      }
      player.inputmap.ClearAllInput(player);
      player.inputmap.Movementstick = Vector2.Zero;
      player.inputmap.ZoomValue = 0.0f;
      return false;
    }

    public void DrawLowBeamsBeforeStart()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawSmartCharacterBox();
      if (this.OK != null)
        this.OK.DrawTextButton(new Vector2(1024f * this.lerper.Value, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      this.No.DrawTextButton(new Vector2((float) (1024.0 * -(double) this.lerper.Value), 0.0f), 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

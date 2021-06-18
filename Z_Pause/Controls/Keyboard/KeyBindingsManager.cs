// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.Keyboard.KeyBindingsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SEngine;
using SEngine.Input;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_Pause.Controls.Keyboard
{
  internal class KeyBindingsManager
  {
    private KeyBindingEntry[] keybindings;
    private int WaitingToAssignKey;
    private TextButton Confirm;
    private TextButton Reset;
    private bool SomethingChanged;
    private bool resetdefaults;
    private BackButton back;

    public KeyBindingsManager(Player player)
    {
      this.SetUp(player);
      this.Confirm = new TextButton(SEngine.Localization.Localization.GetText(65));
      this.Reset = new TextButton("Default");
      this.Reset.vLocation = new Vector2(200f, 300f);
      this.resetdefaults = false;
      this.Confirm.vLocation = new Vector2(924f, 500f);
      this.back = new BackButton(true);
    }

    internal static string GetKeyboardActionsToString(KeyboardActions keyboardactions)
    {
      switch (keyboardactions)
      {
        case KeyboardActions.CameraUp:
          return "Pan Up";
        case KeyboardActions.CameraLeft:
          return "Pan Left";
        case KeyboardActions.CameraDown:
          return "Pan Down";
        case KeyboardActions.CameraRight:
          return "Pan Right";
        case KeyboardActions.Speed1:
          return "Simulation Speed x1";
        case KeyboardActions.Speed2:
          return "Simulation Speed x2";
        case KeyboardActions.Speed3:
          return "Simulation Speed x3";
        case KeyboardActions.Speed4:
          return "Pause Simulation Speed";
        case KeyboardActions.Pause:
          return "Back/Pause";
        case KeyboardActions.ZoomIn:
          return "Zoom In/Scroll";
        case KeyboardActions.ZoomOut:
          return "Zoom Out/Scroll";
        case KeyboardActions.DestroyBuildMode:
          return "Destroy~(Hold During Build)";
        default:
          return "nsnns";
      }
    }

    private void SetUp(Player player)
    {
      this.SomethingChanged = false;
      this.WaitingToAssignKey = -1;
      this.keybindings = new KeyBindingEntry[12];
      for (int index = 0; index < this.keybindings.Length; ++index)
        this.keybindings[index].VLocation = new Vector2(552f, (float) (130.0 * (double) Sengine.UltraWideSreenDownardsMultiplier + (double) (index * 40) * (double) Sengine.UltraWideSreenUpwardsMultiplier));
    }

    public bool UpdatekeyBindingsManager(Player player, float DeltaTime)
    {
      if (!this.resetdefaults && this.Reset.UpdateTextButton(player, Vector2.Zero, DeltaTime))
      {
        player.Stats.userkeybindings.ResetToDefaults();
        this.SetUp(player);
        this.resetdefaults = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
      }
      for (int index = 0; index < this.keybindings.Length; ++index)
      {
        bool Cancelled;
        if (this.keybindings[index].UpdateKeyBindingEntry(player, DeltaTime, Vector2.Zero, this.WaitingToAssignKey, this.WaitingToAssignKey == index, out Cancelled))
        {
          this.WaitingToAssignKey = index;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        }
        if (Cancelled)
        {
          this.WaitingToAssignKey = -1;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        }
      }
      if (this.WaitingToAssignKey > -1)
      {
        Keys keyPressed = WaitingForPCKeyPress.GetKeyPressed();
        bool flag = false;
        if (keyPressed != Keys.None)
        {
          for (int index = 0; index < this.keybindings.Length; ++index)
          {
            if (index != this.WaitingToAssignKey && this.keybindings[index].currentkey == keyPressed)
            {
              flag = true;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
            }
          }
          if (!flag)
          {
            if (keyPressed != this.keybindings[this.WaitingToAssignKey].currentkey)
            {
              this.SomethingChanged = true;
              this.resetdefaults = false;
            }
            this.keybindings[this.WaitingToAssignKey].SetNewKey(keyPressed);
            this.WaitingToAssignKey = -1;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          }
          else
            this.keybindings[this.WaitingToAssignKey].FlashRed();
        }
      }
      if (this.SomethingChanged && this.WaitingToAssignKey == -1 && this.Confirm.UpdateTextButton(player, Vector2.Zero, DeltaTime))
      {
        for (int index = 0; index < this.keybindings.Length; ++index)
        {
          player.Stats.userkeybindings.KeyUsed[index] = this.keybindings[index].currentkey;
          this.SomethingChanged = false;
        }
        player.SaveThisPlayer();
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        return true;
      }
      if (this.WaitingToAssignKey != -1 || !this.back.UpdateBackButton(player, DeltaTime))
        return false;
      if (this.resetdefaults || this.SomethingChanged)
        player.SaveThisPlayer();
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
      return true;
    }

    public void DrawkeyBindingsManager()
    {
      for (int index = 0; index < this.keybindings.Length; ++index)
        this.keybindings[index].DrawKeyBindingEntry(Vector2.Zero);
      if (this.SomethingChanged && this.WaitingToAssignKey == -1)
        this.Confirm.DrawTextButton(Vector2.Zero);
      if (this.WaitingToAssignKey == -1 && !this.resetdefaults)
      {
        this.Reset.vLocation = new Vector2(100f, 600f);
        this.Reset.DrawTextButton(Vector2.Zero);
      }
      this.back.DrawBackButton(Vector2.Zero, AssetContainer.pointspritebatchTop05);
    }
  }
}

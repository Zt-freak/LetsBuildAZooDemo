// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.Keyboard.KeyBindingEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SEngine;
using SEngine.Input;

namespace TinyZoo.Z_Pause.Controls.Keyboard
{
  internal class KeyBindingEntry
  {
    private TextButton textbutton;
    private GameObject BlackFrame;
    private GameObject ActionString;
    private Vector2 BlackVSCale;
    private string ActionText;
    public Vector2 VLocation;
    private string OldButtonSTring;
    private float FlashTimer;
    public Keys currentkey;
    private Keys StartingKey;

    public KeyBindingEntry(string Action, Keys _currentkey)
    {
      this.StartingKey = _currentkey;
      this.currentkey = _currentkey;
      this.ActionText = Action;
      this.BlackVSCale = new Vector2(300f, 26f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.BlackFrame = new GameObject();
      this.BlackFrame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BlackFrame.SetDrawOriginToCentre();
      this.BlackFrame.SetAllColours(0.0f, 0.0f, 0.0f);
      this.ActionString = new GameObject();
      this.ActionString.scale = 2f;
      this.BlackFrame.vLocation.X = -150f;
      this.textbutton = new TextButton(PCKeyToString.GetPCKeyToString(this.currentkey));
      this.textbutton.vLocation.X += this.textbutton.GetSize_True().X * 0.5f;
    }

    public void SetNewKey(Keys _currentkey)
    {
      this.currentkey = _currentkey;
      this.textbutton.SetText(PCKeyToString.GetPCKeyToString(this.currentkey));
    }

    public void FlashRed() => this.BlackFrame.SetColours(false, 0.3f, new Vector3(1f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f));

    public bool UpdateKeyBindingEntry(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      int WaitingToAssignKey,
      bool WaitingForThisKey,
      out bool Cancelled)
    {
      this.BlackFrame.UpdateColours(DeltaTime);
      Cancelled = false;
      if (WaitingForThisKey)
      {
        this.FlashTimer += DeltaTime;
        if ((double) this.FlashTimer > 0.200000002980232)
        {
          this.textbutton.SetText("_");
          if ((double) this.FlashTimer > 0.4)
            this.FlashTimer = 0.0f;
        }
        else
        {
          this.textbutton.SetText(" ");
          this.textbutton.MouseOver = true;
        }
        if (this.textbutton.UpdateTextButton(player, Offset + this.VLocation, DeltaTime))
        {
          Cancelled = true;
          this.textbutton.SetText(PCKeyToString.GetPCKeyToString(this.currentkey));
        }
      }
      if (WaitingToAssignKey != -1)
        return false;
      int num = this.textbutton.UpdateTextButton(player, Offset + this.VLocation, DeltaTime) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.OldButtonSTring = this.textbutton.GetText();
      return num != 0;
    }

    public void DrawKeyBindingEntry(Vector2 Offset)
    {
      this.BlackFrame.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.VLocation, this.BlackVSCale, this.BlackFrame.fAlpha);
      TextFunctions.DrawJustifiedText(this.ActionText, this.ActionString.scale, this.BlackFrame.vLocation + Offset + this.VLocation, this.ActionString.GetColour(), this.ActionString.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      this.textbutton.DrawTextButton(Offset + this.VLocation);
    }
  }
}

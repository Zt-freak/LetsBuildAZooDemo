// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.Selected.SelectedRentryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Research_.IconGrid;
using TinyZoo.Z_Research_.RData;

namespace TinyZoo.Z_Research_.Selected
{
  internal class SelectedRentryManager
  {
    public R_Icon REF_selectionIcon;
    private BigBrownPanel bigpoop;
    private Vector2 Location;
    private SimpleTextHandler simpletext;
    private TextButton textbutton;
    private TextButton COntrollertextbutton;
    private LerpHandler_Float lerper;
    private bool IsPostUnlock;
    public bool CanAfford;
    private BlackOut blackout;
    private BlackOut WhiteOutout;
    public float PercentUnlocked;
    private float ScrollerUnlockValue;
    private bool IsUnlocked;

    public SelectedRentryManager(R_Icon selection, Player player)
    {
      this.PercentUnlocked = 0.0f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Location = new Vector2(512f, 668f);
      this.REF_selectionIcon = selection;
      this.blackout = new BlackOut();
      this.bigpoop = new BigBrownPanel();
      this.bigpoop.vScale = new Vector2(800f, 150f);
      this.IsUnlocked = false;
      string TextToWrite = this.REF_selectionIcon.rentry.Heading + "~" + this.REF_selectionIcon.rentry.Description + "~Research points to unlock: " + (object) this.REF_selectionIcon.rentry.Cost;
      if (selection.unlockstate == UnlockState.Locked)
        TextToWrite = "Undicovered entry.";
      if (this.REF_selectionIcon.rentry.unlocktype == UnlockTYPE.Count)
        TextToWrite = "DEBUG - ~No data assigned";
      else if (player.unlocks.UnlockedThings[(int) this.REF_selectionIcon.rentry.unlocktype] > -1)
      {
        this.IsUnlocked = true;
        TextToWrite = this.REF_selectionIcon.rentry.Heading + "~" + this.REF_selectionIcon.rentry.Description + "~~You unlocked this on day: " + (object) (player.unlocks.UnlockedThings[(int) this.REF_selectionIcon.rentry.unlocktype] + 1);
      }
      this.simpletext = new SimpleTextHandler(TextToWrite, true, 0.7f, RenderMath.GetPixelSizeBestMatch(1.5f));
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.Location.Y = -50f;
      this.CanAfford = true;
      this.textbutton = new TextButton("Unlock", 50f);
      this.COntrollertextbutton = new TextButton("Hold to Unlock", 100f);
      this.textbutton.vLocation.Y = 30f;
      this.COntrollertextbutton.vLocation.Y = 30f;
      if (player.unlocks.ResearchPoints >= this.REF_selectionIcon.rentry.Cost)
        return;
      this.CanAfford = false;
      this.textbutton.SetButtonColour(BTNColour.Red);
      this.COntrollertextbutton.SetButtonColour(BTNColour.Red);
    }

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public bool BlockMouse(Player player) => MathStuff.CheckPointCollision(true, this.bigpoop.location + (this.Location + new Vector2(0.0f, this.lerper.Value * 200f)), 1f, this.bigpoop.vScale.X, this.bigpoop.vScale.Y, player.inputmap.PointerLocation);

    public bool UpdateSelectedRentryManager(
      Player player,
      float DeltaTime,
      R_IconGrid ricongrid,
      out bool RescanRanges)
    {
      RescanRanges = false;
      if (this.IsPostUnlock)
      {
        this.WhiteOutout.UpdateColours(DeltaTime);
        return (double) this.WhiteOutout.fAlpha == 0.0;
      }
      this.lerper.UpdateLerpHandler(DeltaTime);
      bool flag = false;
      if ((double) this.lerper.Value == 0.0 && player.unlocks.ResearchPoints >= this.REF_selectionIcon.rentry.Cost && this.textbutton.MouseOverUpdate(player, this.Location, DeltaTime))
        flag = true;
      if (this.REF_selectionIcon.BlockMouseDown && !player.inputmap.HeldButtons[0] && GameFlags.IsUsingController)
        this.REF_selectionIcon.BlockMouseDown = false;
      if (this.REF_selectionIcon.BlockMouseDown && GameFlags.IsUsingController)
      {
        this.ScrollerUnlockValue = 0.0f;
        this.PercentUnlocked = 0.0f;
      }
      else
      {
        if (player.inputmap.PressedThisFrame[0] && !this.CanAfford)
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ModeSelectButton);
        if (!this.IsUnlocked)
        {
          if (((player.inputmap.HeldButtons[0] ? 1 : (this.REF_selectionIcon.IsHeldOn ? 1 : 0)) | (flag ? 1 : 0)) != 0)
          {
            if (this.REF_selectionIcon.MouseOver | flag)
            {
              int NewOnesFound = 0;
              if (this.CanAfford)
              {
                this.ScrollerUnlockValue += DeltaTime;
                this.PercentUnlocked += DeltaTime;
                if ((double) this.PercentUnlocked > 1.0)
                  this.PercentUnlocked = 1f;
                if ((double) this.ScrollerUnlockValue > 2.0 && !this.IsPostUnlock)
                {
                  if (player.unlocks.UnlockedThings[(int) this.REF_selectionIcon.rentry.unlocktype] < 0)
                  {
                    this.REF_selectionIcon.Unlock(ref NewOnesFound);
                    player.unlocks.ResearchPoints -= this.REF_selectionIcon.rentry.Cost;
                    player.unlocks.UnlockThis(this.REF_selectionIcon.rentry, player);
                    Player.financialrecords.ResearchedSomething();
                  }
                  RescanRanges = true;
                  ricongrid.ScanForPreviews(true);
                  this.WhiteOutout = new BlackOut();
                  this.WhiteOutout.SetAllColours(1f, 1f, 1f);
                  this.IsPostUnlock = true;
                  this.WhiteOutout.SetAlpha(false, 0.5f, 1f, 0.0f);
                }
                CameraShake.BeginCameraShake(TinyZoo.Game1.Rnd, this.PercentUnlocked);
              }
            }
          }
          else
          {
            if ((double) this.PercentUnlocked > 0.0)
              CameraShake.BeginCameraShake(TinyZoo.Game1.Rnd, 0.0f, 0.0f);
            this.ScrollerUnlockValue = 0.0f;
            this.PercentUnlocked = 0.0f;
          }
        }
      }
      if (player.inputmap.PressedBackOnController())
      {
        player.inputmap.ReleasedThisFrame[7] = false;
        if ((double) this.lerper.Value == 0.0)
          return true;
      }
      return false;
    }

    public void DrawSelectedRentryManager()
    {
      if ((double) this.PercentUnlocked > 0.0 && !this.IsPostUnlock)
      {
        this.blackout.fAlpha = this.PercentUnlocked;
        this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
        this.REF_selectionIcon.UNLOCK_DrawR_Icon(this.PercentUnlocked, this.ScrollerUnlockValue);
      }
      else if (this.IsPostUnlock)
      {
        this.WhiteOutout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
        return;
      }
      Vector2 vector2 = this.Location + new Vector2(0.0f, this.lerper.Value * 200f);
      if (!GameFlags.IsUsingController)
        return;
      this.bigpoop.DrawBigBrownPanel(vector2, AssetContainer.pointspritebatch03);
      if (!this.IsUnlocked)
      {
        if (GameFlags.IsUsingController)
          this.COntrollertextbutton.DrawTextButton(vector2, 1f, AssetContainer.pointspritebatch03);
        else
          this.textbutton.DrawTextButton(vector2, 1f, AssetContainer.pointspritebatch03);
      }
      this.simpletext.DrawSimpleTextHandler(vector2, 1f, AssetContainer.pointspritebatch03);
    }
  }
}

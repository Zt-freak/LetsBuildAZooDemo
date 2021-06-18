// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Transfer.TransferScreen.Prisoners.PrisonerIconSwitcher
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.OverWorld.Intake.InmateSummary;
using TinyZoo.PlayerDir.Layout;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld.Transfer.TransferScreen.Prisoners
{
  internal class PrisonerIconSwitcher
  {
    public prisonerIcon prisonericon;
    private TransButton transbutton;
    public Vector2 Location;
    public PrisonerInfo prisonerinfo;
    private LerpHandler_Float PosLerper;
    private GameObject Maxxx;
    private GameObject WrongCell;
    private TRC_ButtonDisplay CtrollerButton;

    public PrisonerIconSwitcher(PrisonerInfo _prisonerinfo)
    {
      this.CtrollerButton = new TRC_ButtonDisplay(RenderMath.GetPixelSizeBestMatch(2f * Sengine.ScreenRationReductionMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier));
      this.CtrollerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxY);
      this.prisonerinfo = _prisonerinfo;
      this.prisonericon = new prisonerIcon(this.prisonerinfo.intakeperson);
      this.transbutton = new TransButton();
      this.PosLerper = new LerpHandler_Float();
      this.PosLerper.SetLerp(true, -1f, -1f, 3f);
      this.Maxxx = new GameObject();
      this.Maxxx.DrawRect = new Rectangle(910, 200, 72, 22);
      this.Maxxx.SetDrawOriginToCentre();
      this.Maxxx.scale = 3f;
      this.Maxxx.vLocation.X = 256f;
    }

    public bool IsTick() => this.transbutton.IsTick;

    public void AddUnhappyFace()
    {
      this.WrongCell = new GameObject();
      this.WrongCell.DrawRect = new Rectangle(103, 22, 7, 7);
      this.WrongCell.SetAllColours(ColourData.FernRed);
      this.WrongCell.SetDrawOriginToCentre();
    }

    public void UpdatePrisonerIconSwitcher(
      Vector2 Offset,
      float DeltaTime,
      Player player,
      bool BlockAssign,
      bool IsControllerSelected)
    {
      if (this.transbutton.UpdateTransButton(player, DeltaTime, this.Location + Offset, IsControllerSelected))
      {
        if (this.transbutton.IsTick)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuClose, 0.8f, 1f);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.Rotate, 0.6f, 0.2f);
          this.transbutton.SetTick(false);
          this.PosLerper.SetLerp(false, 0.0f, -1f, 3f, true);
        }
        else if (!BlockAssign)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.8f, 1f);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.Rotate, 0.6f, 0.8f);
          this.transbutton.SetTick(true);
          this.PosLerper.SetLerp(false, 0.0f, 1f, 3f, true);
        }
      }
      this.PosLerper.UpdateLerpHandler(DeltaTime);
      this.prisonericon.Location.X = this.PosLerper.Value * 256f;
    }

    public void DrawPrisonerIconSwitcher(Vector2 Offset, bool DrawMax, bool IsControllerSelected)
    {
      this.prisonericon.DrawprisonerIcon(this.Location + Offset, AssetContainer.pointspritebatchTop05);
      if (this.WrongCell != null && !TinyZoo.GameFlags.PhotoMode)
      {
        this.WrongCell.scale = 2f;
        this.WrongCell.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.prisonericon.Location + this.Location + Offset + new Vector2(65f, -25f));
      }
      this.transbutton.DrawTransButton(Offset + this.Location);
      if (DrawMax && !this.transbutton.IsTick)
        this.Maxxx.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.Location);
      if (!IsControllerSelected || this.CtrollerButton == null || !TinyZoo.GameFlags.IsUsingController)
        return;
      this.CtrollerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, Offset + this.Location + new Vector2(20f, -30f));
    }
  }
}

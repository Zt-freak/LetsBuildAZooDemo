// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.MainBar.PENControllerHint
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_PenInfo.MainBar
{
  internal class PENControllerHint
  {
    private TRC_ButtonDisplay DiagButton;
    private LerpHandler_Float lerper;
    private Vector2 Location;

    public PENControllerHint(bool IsRight, int TtalButtons)
    {
      this.DiagButton = new TRC_ButtonDisplay(TinyZoo.GameFlags.GetTRCButtonScale());
      ControllerButton controllerbutton = ControllerButton.LB;
      if (IsRight)
        controllerbutton = ControllerButton.RB;
      this.DiagButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, controllerbutton);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Location = new Vector2(25f, 668f);
      if (!IsRight)
        return;
      this.lerper.SetDelay((float) TtalButtons * 0.1f);
      this.Location.X = (float) (TtalButtons * 125);
    }

    public void UpdatePENControllerHint(float DeltaTime) => this.lerper.UpdateLerpHandler(DeltaTime);

    public void DrawPENControllerHint(Vector2 _Offset, float alpha = 1f)
    {
      if (!TinyZoo.GameFlags.IsUsingController)
        return;
      Vector2 location = this.Location;
      location.Y += 200f * this.lerper.Value;
      Vector2 Offset = location + _Offset;
      this.DiagButton.Draw(AssetContainer.pointspritebatch03, AssetContainer.TRC_Sprites, Offset, alpha);
    }
  }
}

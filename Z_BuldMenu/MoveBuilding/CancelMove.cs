// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.MoveBuilding.CancelMove
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_BuldMenu.MoveBuilding
{
  internal class CancelMove
  {
    private TextButton textbutton;
    private ScreenHeading screenherader;
    private LerpHandler_Float lerper;

    public CancelMove()
    {
      this.textbutton = new TextButton("Cancel", 45f);
      this.textbutton.stringinabox.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.screenherader = new ScreenHeading("Move Building", 60f);
      this.textbutton.vLocation = this.screenherader.header.vLocation + new Vector2(0.0f, 50f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
    }

    public bool UpdateCancelMove(float DeltaTime, Player player)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      return (double) this.lerper.Value == 0.0 && this.textbutton.UpdateTextButton(player, Vector2.Zero, DeltaTime);
    }

    public void DrawCancelMove()
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 500f, 0.0f);
      this.screenherader.DrawScreenHeading(Offset, AssetContainer.pointspritebatchTop05);
      this.textbutton.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

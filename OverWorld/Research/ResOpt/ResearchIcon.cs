// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Research.ResOpt.ResearchIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.OverWorld.Research.ResOpt
{
  internal class ResearchIcon : GameObject
  {
    public bool MouseOver;
    private GameObject MouseOverObj;

    public ResearchIcon()
    {
      this.scale = 2f;
      this.DrawRect = new Rectangle(983, 124, 30, 30);
      this.SetDrawOriginToCentre();
      this.MouseOverObj = new GameObject();
      this.MouseOverObj.DrawRect = this.DrawRect;
      this.MouseOverObj.DrawRect.Y += 31;
      this.MouseOverObj.SetDrawOriginToCentre();
    }

    public bool UpdateResearchIcon(Player player, Vector2 Offset, bool IsSelectedWIthController)
    {
      if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.MultiTouchTouchLocations[0]))
        this.MouseOver = true;
      else if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.inputmap.PointerLocation))
        this.MouseOver = true;
      return MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]) || IsSelectedWIthController && player.inputmap.PressedThisFrame[0];
    }

    public void DrawResearchIcon(Vector2 Offset)
    {
      if (this.MouseOver)
      {
        this.MouseOverObj.scale = this.scale;
        this.MouseOverObj.vLocation = this.vLocation;
        this.MouseOverObj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
        this.MouseOver = false;
      }
      else
        this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
    }
  }
}

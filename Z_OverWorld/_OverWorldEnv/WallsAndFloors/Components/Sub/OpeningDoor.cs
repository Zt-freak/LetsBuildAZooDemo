// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub.OpeningDoor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub
{
  internal class OpeningDoor : GameObject
  {
    private static Vector2 VSCALE = Vector2.One;

    public OpeningDoor(TileRenderer parent)
    {
      this.bActive = false;
      DoorInfo doorInfo = DoorData.GetDoorInfo(parent.tiletypeonconstruct);
      if (doorInfo.HasAnimationOnThisRotation[parent.RotationOnConstruct])
      {
        this.DrawRect = doorInfo.DrawRect[parent.RotationOnConstruct];
        this.DrawOrigin = doorInfo.Origin[parent.RotationOnConstruct];
        this.bActive = true;
      }
      else
        this.DrawRect = TinyZoo.Game1.WhitePixelRect;
    }

    public void UpdateOpeningDoor()
    {
    }

    public void DrawOpeningDoor(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale)
    {
      if (!this.bActive)
        return;
      this.QuickWorldOffsetDraw(spritebatch, drawWIthThis, ref parent.vLocation, ref OpeningDoor.VSCALE, parent.Rotation, this.DrawRect, parent.fAlpha * ALphaMod, parent.GetColour(), parent.scale, false, ref ThreadLoc, ref ThreadScale);
    }
  }
}

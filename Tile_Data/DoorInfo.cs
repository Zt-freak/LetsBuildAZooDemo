// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tile_Data.DoorInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Tile_Data
{
  internal class DoorInfo
  {
    public Rectangle[] DrawRect;
    public Vector2[] Origin;
    public bool[] HasAnimationOnThisRotation;

    public DoorInfo(
      Rectangle StartingRect,
      Vector2 _Origin,
      int RotationOfBuildingForThisAnimation = 0)
    {
      this.DrawRect = new Rectangle[4];
      this.Origin = new Vector2[4];
      this.HasAnimationOnThisRotation = new bool[4];
      this.HasAnimationOnThisRotation[RotationOfBuildingForThisAnimation] = true;
      this.DrawRect[RotationOfBuildingForThisAnimation] = StartingRect;
      this.Origin[RotationOfBuildingForThisAnimation] = _Origin;
    }

    public void AddRotation(
      Rectangle _RectangleForDoor,
      Vector2 _Origin,
      int RotationOfBuildingForThisAnimation)
    {
      this.HasAnimationOnThisRotation[RotationOfBuildingForThisAnimation] = true;
      this.DrawRect[RotationOfBuildingForThisAnimation] = _RectangleForDoor;
      this.Origin[RotationOfBuildingForThisAnimation] = _Origin;
    }
  }
}

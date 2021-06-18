// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Avatar.CircleAvatar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Avatar
{
  internal class CircleAvatar
  {
    private CirclePiece circlepiece;
    private float SegRotation;
    private int Segments;
    private LerpHandler_Float scalelerper;
    private bool ACTIVE;

    public CircleAvatar()
    {
      this.ACTIVE = false;
      this.circlepiece = new CirclePiece();
      this.Segments = 15;
      this.SegRotation = 0.418879f;
      this.scalelerper = new LerpHandler_Float();
      this.scalelerper.SetLerp(true, 0.0f, 0.0f, 0.0f);
      this.circlepiece.SetAllColours(new Vector3(0.4f, 0.9f, 0.8f));
      this.circlepiece.SetAlpha(0.6f);
    }

    public void UpdateCircleAvatar(float DeltaTime, bool IsActive)
    {
      if (this.ACTIVE != IsActive)
      {
        this.ACTIVE = IsActive;
        if (this.ACTIVE)
          this.scalelerper.SetLerp(false, 0.0f, 1f, 3f, true);
        else
          this.scalelerper.SetLerp(false, 1f, 0.0f, 3f, true);
      }
      this.scalelerper.UpdateLerpHandler(DeltaTime);
      this.circlepiece.Rotation += DeltaTime;
    }

    public void DrawCircleAvatar(Vector2 AvatarLocation)
    {
      AvatarLocation = RenderMath.TranslateWorldSpaceToScreenSpace(AvatarLocation);
      float rotation = this.circlepiece.Rotation;
      this.circlepiece.scale = Sengine.WorldOriginandScale.Z * 0.2f * this.scalelerper.Value;
      for (int index = 0; index < this.Segments; ++index)
      {
        this.circlepiece.DrawCirclePiece(AssetContainer.pointspritebatchTop05, AvatarLocation);
        this.circlepiece.Rotation += this.SegRotation;
      }
      this.circlepiece.Rotation = rotation;
    }
  }
}

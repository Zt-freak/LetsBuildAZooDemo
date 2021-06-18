// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Avatar.Z_AvatarUI
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_Avatar
{
  internal class Z_AvatarUI
  {
    private CircleAvatar circlemanager;

    public Z_AvatarUI() => this.circlemanager = new CircleAvatar();

    public void UpdateZ_AvatarUI(float DeltaTime, bool IsActive) => this.circlemanager.UpdateCircleAvatar(DeltaTime, IsActive);

    public void DrawZ_AvatarUI(Vector2 AvatarLoc) => this.circlemanager.DrawCircleAvatar(AvatarLoc);
  }
}

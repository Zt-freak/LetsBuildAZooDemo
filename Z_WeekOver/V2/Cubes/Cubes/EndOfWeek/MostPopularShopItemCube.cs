// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.Cubes.EndOfWeek.MostPopularShopItemCube
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyZoo.Z_WeekOver.V2.Cubes.Cubes.EndOfWeek
{
  internal class MostPopularShopItemCube : BaseCube
  {
    public MostPopularShopItemCube(float _BaseScale, Player player)
      : base(_BaseScale, true, new Vector3(0.2f, 0.3f, 0.2f))
    {
    }

    public override void UpdateBaseCube(float DeltaTime, Player player, Vector2 Offset) => base.UpdateBaseCube(DeltaTime, player, Offset);

    public override void DrawBaseCube(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      base.DrawBaseCube(Offset, spritebatch);
    }
  }
}

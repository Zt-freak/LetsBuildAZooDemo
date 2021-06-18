// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class VIPInfo
  {
    public Vector2 location;

    protected VIPInfo()
    {
    }

    public virtual Vector2 GetSize() => Vector2.Zero;

    public virtual bool UpdateVIPInfo(Player player, Vector2 offset, float DeltaTime) => false;

    public virtual void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
    }
  }
}

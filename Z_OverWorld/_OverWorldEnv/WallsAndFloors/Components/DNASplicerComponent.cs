// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.DNASplicerComponent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class DNASplicerComponent : RenderComponent
  {
    private bool isMakingBaby;
    private bool HasBabyReady;

    public DNASplicerComponent(TileRenderer parent)
      : base(parent, RenderComponentType.DNASplicer)
    {
    }

    public void TurnOn(bool TurnOn) => this.isMakingBaby = TurnOn;

    public void SetHasBabyReady(bool HasBaby) => this.HasBabyReady = HasBaby;

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (parent.RefTopRenderer != null)
        parent.RefTopRenderer.IsAnimating = this.isMakingBaby;
      return false;
    }

    public override void DrawRenderComponent(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      bool IsTopLayer)
    {
      base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
    }
  }
}

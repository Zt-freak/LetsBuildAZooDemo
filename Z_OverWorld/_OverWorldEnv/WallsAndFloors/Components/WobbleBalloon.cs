// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.WobbleBalloon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class WobbleBalloon : RenderComponent
  {
    private DualSinOscillator oscialltor;

    public WobbleBalloon(TileRenderer parent)
      : base(parent, RenderComponentType.WobbleBalloon)
    {
      this.oscialltor = new DualSinOscillator(MathStuff.getRandomFloat(0.2f, 0.5f), MathStuff.getRandomFloat(0.2f, 0.5f), _YScale: (3f * Sengine.ScreenRatioUpwardsMultiplier.Y));
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      this.oscialltor.UpdateDualSinOscillator(DeltaTime);
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
      if (parent.RefTopRenderer != null)
        parent.RefTopRenderer.vLocation = parent.vLocation + this.oscialltor.CurrentOffset;
      base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
    }
  }
}

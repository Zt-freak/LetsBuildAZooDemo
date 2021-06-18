// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.SpawnAnimNature
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class SpawnAnimNature : RenderComponent
  {
    private SinLerper sinlerper;

    public SpawnAnimNature(TileRenderer parent)
      : base(parent, RenderComponentType.SpawnAnimNature)
    {
      this.sinlerper = new SinLerper();
      if (parent.RefTopRenderer != null)
        parent.RefTopRenderer.scale = 0.0f;
      this.sinlerper.SetLerp(SinCurveType.EaseInAndOut, 0.2f, 0.0f, 1f, true);
      if (!TrailerDemoFlags.HasTrailerFlag)
        return;
      this.sinlerper.SetDelay(MathStuff.getRandomFloat(0.0f, 0.3f));
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      this.sinlerper.UpdateSinLerper(DeltaTime);
      if ((double) this.sinlerper.CurrentValue == 1.0 && parent.RefTopRenderer != null)
        parent.RefTopRenderer.scale = 1f;
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
      parent.scale = this.sinlerper.CurrentValue;
      if (parent.RefTopRenderer != null)
        parent.RefTopRenderer.scale = parent.scale;
      base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
      parent.scale = 1f;
    }
  }
}

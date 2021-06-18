// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.SpawnAnimator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class SpawnAnimator : RenderComponent
  {
    private SinLerper sinlerper;
    private TileRenderer underfloorRend;
    private bool IsGate;
    private RenderComponent Gate;
    private DustPoof dustpoof;

    public SpawnAnimator(TileRenderer parent, float Delay, TILETYPE underfloor, bool _IsGate)
      : base(parent, RenderComponentType.SpawnAnimator)
    {
      this.IsGate = _IsGate;
      this.sinlerper = new SinLerper();
      this.sinlerper.SetLerp(SinCurveType.EaseInAndOut, 0.2f, 0.0f, 1f, true);
      this.sinlerper.SetDelay(Delay);
      if (underfloor != TILETYPE.Count && underfloor != TILETYPE.None)
        this.underfloorRend = new TileRenderer(new LayoutEntry(underfloor), parent.TileLocation.X, parent.TileLocation.Y, false);
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
      {
        this.dustpoof = new DustPoof();
        this.dustpoof.vLocation = parent.vLocation;
      }
      if (!this.IsGate)
        return;
      for (int index = 0; index < parent.rendercomponent.Count; ++index)
      {
        if (parent.rendercomponent[index].componenttype == RenderComponentType.EnclosureGate)
        {
          this.Gate = parent.rendercomponent[index];
          this.Gate.TempDisableDraw = true;
        }
      }
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (this.underfloorRend != null)
        this.underfloorRend.HasDrawn = false;
      this.sinlerper.UpdateSinLerper(DeltaTime);
      if (this.Gate != null && (double) this.sinlerper.CurrentValue == 1.0)
        this.Gate.TempDisableDraw = false;
      if (this.dustpoof == null || (double) this.sinlerper.CurrentValue <= 0.400000005960464)
        return (double) this.sinlerper.CurrentValue == 1.0;
      this.dustpoof.UpdateDustPoof(DeltaTime);
      return (double) this.sinlerper.CurrentValue == 1.0 && (double) this.dustpoof.fAlpha == 0.0;
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
      if (this.underfloorRend != null)
        this.underfloorRend.DrawTileRenderer(spritebatch, ref ThreadLoc, ref ThreadScale);
      if ((double) this.sinlerper.CurrentValue == 0.0)
        return;
      parent.scale = this.sinlerper.CurrentValue;
      base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
      parent.scale = 1f;
      if (this.dustpoof == null || (double) this.sinlerper.CurrentValue <= 0.400000005960464)
        return;
      this.dustpoof.DrawDustPoof(spritebatch);
    }
  }
}

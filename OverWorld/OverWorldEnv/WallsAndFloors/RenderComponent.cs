// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.RenderComponent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_DayNight;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors
{
  internal class RenderComponent
  {
    public Rectangle OriginalRectangle;
    public RenderComponentType componenttype;
    public bool TempDisableDraw;
    public bool HasBeenDestroyed;
    private static Vector2 VSCALE = Vector2.One;

    public RenderComponent(TileRenderer parent, RenderComponentType _componenttype)
    {
      this.OriginalRectangle = parent.DrawRect;
      this.componenttype = _componenttype;
    }

    public virtual void SetUpAfterCreatingTopLayer(
      ZooBuildingTopRenderer toplayer,
      TileRenderer parent)
    {
    }

    public virtual bool UpdateRenderComponent(TileRenderer parent, float DeltaTime) => false;

    public virtual void StartNewDay(TileRenderer parent, Player player)
    {
    }

    public virtual void DrawRenderComponent(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      bool IsTopLayer)
    {
      if (this.TempDisableDraw)
        return;
      parent.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      parent.QuickWorldOffsetDraw(spritebatch, drawWIthThis, ref parent.vLocation, ref RenderComponent.VSCALE, parent.Rotation, parent.DrawRect, parent.fAlpha * ALphaMod, parent.GetColour(), parent.scale, false, ref ThreadLoc, ref ThreadScale);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.EnclosureGate
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class EnclosureGate : RenderComponent
  {
    private float FrameTime;
    private int Frame;
    private Rectangle StartRect;
    private GATESTATE gatestate;
    private OpeningDoor openingdoor;

    public EnclosureGate(TileRenderer parent)
      : base(parent, RenderComponentType.EnclosureGate)
    {
      this.StartRect = parent.DrawRect;
      this.gatestate = GATESTATE.None;
      if (!DoorData.ThisBuildingHasADoor(parent.tiletypeonconstruct))
        return;
      this.openingdoor = new OpeningDoor(parent);
      this.StartRect = this.openingdoor.DrawRect;
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (this.gatestate == GATESTATE.Opening)
      {
        this.FrameTime += DeltaTime;
        if ((double) this.FrameTime > 0.100000001490116)
        {
          this.FrameTime = 0.0f;
          ++this.Frame;
          if (this.openingdoor != null)
          {
            this.openingdoor.DrawRect = this.StartRect;
            this.openingdoor.DrawRect.X += (this.StartRect.Width + 1) * this.Frame;
          }
          else
          {
            parent.DrawRect = this.StartRect;
            parent.DrawRect.X += (parent.DrawRect.Width + 1) * this.Frame;
          }
          if (this.Frame >= 2)
            this.gatestate = GATESTATE.Open;
        }
      }
      else if (this.gatestate == GATESTATE.Closing)
      {
        this.FrameTime += DeltaTime;
        if ((double) this.FrameTime > 0.100000001490116)
        {
          this.FrameTime = 0.0f;
          --this.Frame;
          if (this.openingdoor != null)
          {
            this.openingdoor.DrawRect = this.StartRect;
            this.openingdoor.DrawRect.X += (this.StartRect.Width + 1) * this.Frame;
          }
          else
          {
            parent.DrawRect = this.StartRect;
            parent.DrawRect.X += (parent.DrawRect.Width + 1) * this.Frame;
          }
          if (this.Frame <= 0)
            this.gatestate = GATESTATE.None;
        }
      }
      return false;
    }

    public bool IsOpen() => this.gatestate == GATESTATE.Open;

    public bool IsClosed() => this.gatestate == GATESTATE.None;

    public void OpenGateNow() => this.gatestate = GATESTATE.Opening;

    public void CloseGate() => this.gatestate = GATESTATE.Closing;

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
      if (this.openingdoor != null)
        this.openingdoor.DrawOpeningDoor(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale);
      if (parent.rendercomponent.Count <= 1)
        return;
      for (int index = 0; index < parent.rendercomponent.Count; ++index)
      {
        if (parent.rendercomponent[index] != this)
          parent.rendercomponent[index].TempDisableDraw = true;
      }
    }
  }
}

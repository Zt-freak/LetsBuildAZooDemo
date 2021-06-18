// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components.ResearchDone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components
{
  internal class ResearchDone : RenderComponent
  {
    private bool IsOff;
    private GameObject ResearchDoneIcon;
    public bool Disabled;

    public ResearchDone(TileRenderer parent)
      : base(parent, RenderComponentType.ResearchDone)
    {
      this.ResearchDoneIcon = new GameObject();
      this.ResearchDoneIcon.DrawRect = new Rectangle(948, 496, 21, 21);
      this.ResearchDoneIcon.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.ResearchDoneIcon.DrawOrigin.Y -= 8f;
      this.ResearchDoneIcon.SetDrawOriginToCentre();
      this.ResearchDoneIcon.vLocation = parent.vLocation;
      this.ResearchDoneIcon.SetDrawOriginToCentre();
      this.ResearchDoneIcon.DrawOrigin.X += 8f;
      this.ResearchDoneIcon.DrawOrigin.Y += 28f;
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      if (!Researcher.IsCurrentlyResearching || Researcher.IsCurrentlyResearching && Researcher.ResearchComplete)
      {
        if ((double) OverWorldEnvironmentManager.FlashTimer > 0.600000023841858)
        {
          if (this.IsOff)
          {
            this.IsOff = false;
            this.ResearchDoneIcon.DrawRect = !Researcher.ResearchComplete ? new Rectangle(948, 496, 21, 21) : new Rectangle(948, 518, 21, 21);
          }
        }
        else if (!this.IsOff)
          this.IsOff = true;
      }
      else
        this.IsOff = true;
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
      if (!this.Disabled && (!Researcher.IsCurrentlyResearching || Researcher.IsCurrentlyResearching && Researcher.ResearchComplete) && (!Researcher.AllResearchIsComplete && !this.IsOff))
      {
        this.ResearchDoneIcon.vLocation = parent.vLocation;
        this.ResearchDoneIcon.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      }
      base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
    }
  }
}

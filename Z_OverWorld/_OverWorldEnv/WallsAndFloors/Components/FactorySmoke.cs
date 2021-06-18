// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.FactorySmoke
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class FactorySmoke : RenderComponent
  {
    private List<AnimatedGameObject> gobbos;
    private bool AddedToTop;
    public Vector2Int BuildLocation;
    private bool IsProcessingSomething;

    public FactorySmoke(TileRenderer parent)
      : base(parent, RenderComponentType.FactorySmoke)
    {
      this.BuildLocation = parent.TileLocation;
      LiveStats.AddComponentThatNeedsLink((RenderComponent) this);
      this.gobbos = new List<AnimatedGameObject>();
      this.AddedToTop = false;
      bool AddAnother = true;
      int Index = 0;
      while (AddAnother)
      {
        AnimatedGameObject animatedGameObject = new AnimatedGameObject();
        SMOKETYPE smoketype;
        animatedGameObject.vLocation = FactorySmokeData.GetSmokeOffset(parent.tiletypeonconstruct, parent.RotationOnConstruct, Index, out AddAnother, out smoketype) * Sengine.ScreenRatioUpwardsMultiplier;
        switch (smoketype)
        {
          case SMOKETYPE.IncineratorSmoke:
            animatedGameObject.DrawRect = new Rectangle(3041, 3262, 44, 59);
            animatedGameObject.SetUpSimpleAnimation(6, MathStuff.getRandomFloat(0.15f, 0.25f));
            break;
          case SMOKETYPE.RegularSmoke:
            animatedGameObject.DrawRect = new Rectangle(3951, 3655, 22, 30);
            animatedGameObject.SetUpSimpleAnimation(6, MathStuff.getRandomFloat(0.15f, 0.25f));
            break;
          case SMOKETYPE.SmallSmoke:
            animatedGameObject.DrawRect = new Rectangle(1644, 4049, 22, 30);
            animatedGameObject.SetUpSimpleAnimation(6, MathStuff.getRandomFloat(0.15f, 0.25f));
            break;
        }
        animatedGameObject.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
        this.gobbos.Add(animatedGameObject);
        animatedGameObject.Frame = TinyZoo.Game1.Rnd.Next(0, 5);
        animatedGameObject.SetAlpha(0.0f);
        ++Index;
      }
    }

    public void SetProductionState(bool _IsProcessingSomething)
    {
      this.IsProcessingSomething = _IsProcessingSomething;
      for (int index = 0; index < this.gobbos.Count; ++index)
      {
        if (!this.IsProcessingSomething)
          this.gobbos[index].SetAlpha(true, 3f, 1f, 0.0f);
        else
          this.gobbos[index].SetAlpha(true, 3f, 0.0f, 1f);
      }
    }

    public override bool UpdateRenderComponent(TileRenderer parent, float DeltaTime)
    {
      for (int index = 0; index < this.gobbos.Count; ++index)
        this.gobbos[index].UpdateAnimation(DeltaTime);
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
      if (!this.AddedToTop && parent.RefTopRenderer != null)
      {
        this.AddedToTop = true;
        parent.RefTopRenderer.TryToAddPostDrawComponent((RenderComponent) this);
      }
      if (IsTopLayer)
      {
        for (int index = 0; index < this.gobbos.Count; ++index)
          this.gobbos[index].WorldOffsetDraw(spritebatch, AssetContainer.EnvironmentSheet, this.gobbos[index].vLocation + parent.vLocation, this.gobbos[index].scale, 0.0f);
      }
      else
        base.DrawRenderComponent(parent, drawWIthThis, spritebatch, ALphaMod, ref ThreadLoc, ref ThreadScale, IsTopLayer);
    }
  }
}

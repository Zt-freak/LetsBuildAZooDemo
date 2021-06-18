// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.CorpseRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_AnimalsAndPeople.Corpse;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_AnimalsAndPeople
{
  internal class CorpseRenderer : GameObject
  {
    private Flies flies;
    private CorpseBlood corpseblood;
    private GameObject Shadow;
    private Vector3 baseColour;

    public CorpseRenderer(CauseOfDeath causeofdeath, AnimalType enemytype, int Variant)
    {
      this.Shadow = new GameObject();
      CorpseSize corpsesize;
      this.baseColour = CorpseData.GetCorpseInfo(enemytype, out corpsesize, Variant);
      TinyZoo.Game1.Rnd.Next(0, 3);
      switch (corpsesize)
      {
        case CorpseSize.Medium:
          this.DrawRect = new Rectangle(712, 47, 17, 12);
          this.Shadow.DrawRect = new Rectangle(712, 60, 17, 8);
          break;
        case CorpseSize.Big:
          this.DrawRect = new Rectangle(691, 47, 20, 14);
          this.Shadow.DrawRect = new Rectangle(691, 62, 20, 11);
          break;
        default:
          this.DrawRect = new Rectangle(730, 47, 12, 9);
          this.Shadow.DrawRect = new Rectangle(730, 57, 12, 6);
          break;
      }
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.Shadow.SetDrawOriginToCentre();
      switch (causeofdeath)
      {
        case CauseOfDeath.Hunger:
          this.flies = new Flies();
          break;
        case CauseOfDeath.KilledInAnimalFight:
        case CauseOfDeath.KilledByThePolice:
          this.corpseblood = new CorpseBlood();
          break;
      }
      this.SetAllColours(this.baseColour);
    }

    public void UpdateCorpseRenderer(float DeltaTime)
    {
      if (this.flies == null)
        return;
      this.flies.UpdateFlies(DeltaTime);
    }

    public void ScreenSpaceDrawCorpseRenderer(
      Vector2 Offset,
      SpriteBatch spritebatch,
      float ScaleMult = 1f)
    {
      this.Draw(spritebatch, AssetContainer.AnimalSheet, Offset, this.scale * ScaleMult, this.fAlpha);
      Flies flies = this.flies;
    }

    public void DrawCorpseRenderer(Vector2 WorldSpaceLocation)
    {
      this.vLocation = WorldSpaceLocation;
      if (this.corpseblood != null)
        this.corpseblood.DrawCorpseBlood(WorldSpaceLocation);
      this.Shadow.vLocation = WorldSpaceLocation;
      this.Shadow.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
      this.SetAllColours(this.baseColour.X * DayNightManager.SunShineValueR, this.baseColour.Y * DayNightManager.SunShineValueG, this.baseColour.Z * DayNightManager.SunShineValueB);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
      if (this.flies == null)
        return;
      this.flies.DrawFlies(WorldSpaceLocation);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.InmateSummary.Psners.PrisonerSprite
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.OverWorld.Intake.InmateSummary.Psners
{
  internal class PrisonerSprite : GameObject
  {
    public PrisonerSprite(AnimalType enemytype, int CLIndex)
    {
      this.scale = 2f;
      this.DrawRect = EnemyData.GetEnemyIdleRectangle(enemytype, CLIndex);
      this.SetDrawOriginToCentre();
      this.DrawOrigin.Y = EnemyData.GetEnemyRectangle(enemytype).Origin.Y;
    }

    public void UpdatePrisonerSprite()
    {
    }

    public void DrawPrisonerSprite(Vector2 Offset, SpriteBatch spritebatch) => this.Draw(spritebatch, AssetContainer.AnimalSheet, Offset);
  }
}

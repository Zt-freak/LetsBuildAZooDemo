// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.NewDiscoveryScreen.AnimalRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.OverWorld.NewDiscoveryScreen
{
  internal class AnimalRenderer
  {
    public EnemyRenderer enemy;

    public AnimalRenderer(
      AnimalType enemytype,
      int SkinIndex = 0,
      AnimalType ReplacementHead = AnimalType.None,
      int HeadVariant = -1)
    {
      this.enemy = new EnemyRenderer(enemytype, SkinIndex, ReplacementHead, HeadVariant);
      this.enemy.scale = 10f;
      this.enemy.vLocation = new Vector2(512f, 700f);
    }

    public Vector2 GetSize(out float OriginX, out float PixelsWide) => this.enemy.GetSize(out OriginX, out PixelsWide);

    public void UpdateAnimal(float DeltaTime) => this.enemy.UpdateAnimalRenderer(DeltaTime);

    public void DrawAnimal(Vector2 Offset)
    {
      this.enemy.ScreenSpaceDrawEnemyRendererShadow(Offset);
      this.enemy.ScreenSpaceDrawEnemyRenderer(Offset);
    }

    public void DrawAnimal() => this.DrawAnimal(Vector2.Zero);

    public void ScreenSpaceDraw(
      Vector2 Offset,
      SpriteBatch spritebatch,
      bool DrawShadow = true,
      float ScaleMultipier = 1f,
      float alphaMult = 1f)
    {
      if (DrawShadow)
        this.enemy.ScreenSpaceDrawEnemyRendererShadow(Offset, spritebatch, ScaleMultipier);
      this.enemy.ScreenSpaceDrawEnemyRenderer(Offset, spritebatch, ScaleMultipier, alphaMult);
    }
  }
}

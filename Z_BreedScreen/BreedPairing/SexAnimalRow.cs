// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedPairing.SexAnimalRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BreedScreen.BreedPairing
{
  internal class SexAnimalRow
  {
    private GameObject Chromosone;
    public AnimalRow animalrow;
    public Vector2 Location;
    private GameObjectNineSlice FrameForWHoleThing;
    private bool ShowHeldNumber_ForTradeSCreen;

    public SexAnimalRow(AnimalType animal, bool IsAGirl, Player player, bool IsForTradeScreen = false)
    {
      this.ShowHeldNumber_ForTradeSCreen = IsForTradeScreen;
      this.FrameForWHoleThing = new GameObjectNineSlice(new Rectangle(302, 128, 21, 21), 7);
      this.FrameForWHoleThing.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.Chromosone = new GameObject();
      this.Chromosone.DrawRect = new Rectangle(1008, 890, 16, 17);
      if (IsAGirl)
        this.Chromosone.DrawRect = new Rectangle(984, 851, 12, 19);
      this.Chromosone.SetDrawOriginToCentre();
      this.animalrow = new AnimalRow(animal, true, player, false, IsAGirl: IsAGirl, IsForTradeScreen: IsForTradeScreen);
      this.Chromosone.scale = 2f;
      this.Chromosone.vLocation.X = 100f;
      int num = IsForTradeScreen ? 1 : 0;
    }

    public bool UpdateSexAnimalRow(Player player, Vector2 Offset, float DeltaTime) => this.animalrow.UpdateAnimalRow(player, DeltaTime, this.Location + Offset);

    public void DrawSexAnimalRow(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      if (this.ShowHeldNumber_ForTradeSCreen)
      {
        float num = 40f;
        this.FrameForWHoleThing.DrawGameObjectNineSlice(DrawWithThis, AssetContainer.SpriteSheet, this.Location + Offset + new Vector2(482f, num * 0.5f), new Vector2(850f, (80f + num) * Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
      else
        this.FrameForWHoleThing.DrawGameObjectNineSlice(DrawWithThis, AssetContainer.SpriteSheet, this.Location + Offset + new Vector2(482f, 0.0f), new Vector2(850f, 80f * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.Chromosone.Draw(DrawWithThis, AssetContainer.AnimalSheet, this.Location + Offset);
      this.animalrow.DrawAnimalRow(Offset + this.Location, DrawWithThis);
    }
  }
}

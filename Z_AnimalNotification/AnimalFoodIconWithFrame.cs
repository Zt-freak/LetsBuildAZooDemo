// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalFoodIconWithFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalFoodIconWithFrame
  {
    private static float iconSize = 35f;
    private AnimalFoodIcon icon;
    private GameObjectNineSlice nineslice;
    private float basescale;
    private static Rectangle rect = new Rectangle(885, 546, 21, 21);
    public Vector2 location = Vector2.Zero;
    private bool hasActiveIcon;
    private ActiveIcon activeicon;

    public AnimalFoodIconWithFrame(
      AnimalFoodType foodtype,
      float basescale_,
      bool hasCheckOrCross = false,
      bool isTick = false)
    {
      this.basescale = basescale_;
      this.nineslice = new GameObjectNineSlice(AnimalFoodIconWithFrame.rect, 7);
      this.nineslice.scale = 2f * this.basescale;
      this.icon = new AnimalFoodIcon(foodtype, 1f, this.basescale);
      this.hasActiveIcon = hasCheckOrCross;
      if (!this.hasActiveIcon)
        return;
      this.activeicon = new ActiveIcon(isTick, this.basescale, true);
      this.activeicon.vLocation = 0.5f * this.GetSize() - 0.2f * this.activeicon.GetSize();
      this.activeicon.vLocation.Y *= -1f;
    }

    public Vector2 GetSize() => new Vector2(AnimalFoodIconWithFrame.iconSize) * Sengine.ScreenRatioUpwardsMultiplier * this.basescale;

    public void SetIsUndiscovered() => this.icon.SetIsUndiscovered();

    public void SetIsUnavailable() => this.icon.SetIsUnavailable();

    public void SetIsGreyedOut() => this.icon.SetIsGreyedOut();

    public void DrawAnimalFoodIconWithFrame(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.nineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, new Vector2(AnimalFoodIconWithFrame.iconSize) * Sengine.ScreenRatioUpwardsMultiplier * this.basescale);
      this.icon.DrawAnimalFoodIcon(offset, false, spritebatch);
      if (!this.hasActiveIcon)
        return;
      this.activeicon.DrawActiveIcon(spritebatch, offset);
    }
  }
}

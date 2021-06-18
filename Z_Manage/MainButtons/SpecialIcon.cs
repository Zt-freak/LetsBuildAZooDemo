// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.SpecialIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.MainButtons
{
  internal class SpecialIcon : GameObject
  {
    private Vector2 VScale;
    private GameObjectNineSlice nineslice;
    public Vector2 Location;
    private float basescale;
    private bool hasFrame;

    public SpecialIcon(
      ManageButtonType managebuttontype,
      bool hasFrame_ = false,
      float scale_ = 1f,
      float basescale_ = 1f)
    {
      this.basescale = basescale_;
      this.hasFrame = hasFrame_;
      this.scale = scale_ * this.basescale;
      switch (managebuttontype)
      {
        case ManageButtonType.Hiring:
          this.DrawRect = new Rectangle(456, 380, 61, 58);
          break;
        case ManageButtonType.Research:
          this.DrawRect = new Rectangle(389, 264, 64, 54);
          break;
        case ManageButtonType.Genomesequencing:
          this.DrawRect = new Rectangle(459, 318, 61, 61);
          break;
        case ManageButtonType.Accounts:
          this.DrawRect = new Rectangle(568, 384, 61, 54);
          break;
        case ManageButtonType.BusUpgrades:
          this.DrawRect = new Rectangle(521, 323, 53, 53);
          break;
        case ManageButtonType.BuyLand:
          this.DrawRect = new Rectangle(575, 323, 64, 60);
          break;
        case ManageButtonType.CleanPen:
          this.DrawRect = new Rectangle(264, 374, 59, 64);
          break;
        case ManageButtonType.MoveAnimals:
          this.DrawRect = new Rectangle(324, 381, 69, 57);
          break;
        case ManageButtonType.Feed:
          this.DrawRect = new Rectangle(264, 315, 59, 58);
          break;
        case ManageButtonType.AnimalShow:
          this.DrawRect = new Rectangle(324, 314, 72, 66);
          break;
        case ManageButtonType.UpgradePen:
          this.DrawRect = new Rectangle(394, 382, 61, 56);
          break;
        case ManageButtonType.CustomizePen:
          this.DrawRect = new Rectangle(397, 319, 61, 62);
          break;
        case ManageButtonType.FoodChain:
          this.DrawRect = new Rectangle(314, 256, 74, 57);
          break;
        case ManageButtonType.PenSummary:
          this.DrawRect = new Rectangle(454, 267, 61, 50);
          break;
      }
      this.SetDrawOriginToCentre();
      if (!this.hasFrame)
        return;
      this.nineslice = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out Vector3 _), 7);
      this.nineslice.scale = 2f * this.basescale;
      this.nineslice.SetAllColours(0.7490196f, 0.7098039f, 0.6117647f);
      this.VScale = new Vector2(70f, 70f);
    }

    public Vector2 GetSize() => this.VScale * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void UpdateSpecialIcon(float DeltaTime)
    {
    }

    public void DrawSpecialIcon(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      if (this.hasFrame)
        this.nineslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale * this.scale * Sengine.ScreenRatioUpwardsMultiplier);
      this.Draw(spritebatch, AssetContainer.UISheet, this.vLocation + Offset);
    }
  }
}

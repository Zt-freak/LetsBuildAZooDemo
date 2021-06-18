// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay.AnimalSexFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.Intake.InmateSummary.Psners;

namespace TinyZoo.Z_BreedScreen.BreedChamberSelect.ActiveBreedDisplay
{
  internal class AnimalSexFrame : GameObject
  {
    private PrisonerSprite alienentry;
    private GameObject WHITEY;

    public AnimalSexFrame(bool IsFemale, AnimalType animaltype, int VariantIndex)
    {
      this.WHITEY = new GameObject();
      this.WHITEY.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.WHITEY.SetDrawOriginToCentre();
      this.WHITEY.scale = 60f * this.scale;
      if (IsFemale)
      {
        this.WHITEY.SetAllColours(new Vector3(230f, 158f, 187f) / (float) byte.MaxValue);
        this.DrawRect = new Rectangle(0, 41, 100, 99);
      }
      else
      {
        this.WHITEY.SetAllColours(new Vector3(158f, 205f, 230f) / (float) byte.MaxValue);
        this.DrawRect = new Rectangle(0, 143, 88, 112);
      }
      this.SetDrawOriginToCentre();
      this.alienentry = new PrisonerSprite(animaltype, VariantIndex);
    }

    public void UpdateAnimalSexFrame()
    {
    }

    public void DrawAnimalSexFrame(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.WHITEY.scale = 67f * this.scale;
      this.WHITEY.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + new Vector2(0.0f, -2f));
      this.Draw(spritebatch, AssetContainer.UISheet, Offset);
      this.alienentry.DrawPrisonerSprite(Offset + this.vLocation + new Vector2(0.0f, 10f), spritebatch);
    }
  }
}

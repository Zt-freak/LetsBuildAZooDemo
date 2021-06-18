// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer.Animals.AnimalWithChromosone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer.Animals
{
  internal class AnimalWithChromosone
  {
    public Vector2 location;
    private AnimalInFrame animalWithoutFrame;
    private Chromosone chromosome;
    private Vector2 size;
    private bool AnimateAndAddSpaceForJump;

    public AnimalWithChromosone(
      AnimalRenderDescriptor animal,
      float BaseScale,
      bool _AnimateAndAddSpaceForJump = true,
      bool IncludeChromosone = true)
    {
      this.AnimateAndAddSpaceForJump = _AnimateAndAddSpaceForJump;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      if (this.AnimateAndAddSpaceForJump)
        this.size.Y += uiScaleHelper.ScaleY(30f);
      this.animalWithoutFrame = new AnimalInFrame(animal.bodyAnimalType, animal.headAnimalType, animal.variant, 50f * BaseScale, 6f * BaseScale, BaseScale, animal.headVariant);
      Vector2 size = this.animalWithoutFrame.GetSize();
      this.animalWithoutFrame.Location.Y += this.size.Y + size.Y * 0.5f;
      this.size += size;
      this.size.Y += num;
      if (!IncludeChromosone)
        return;
      this.chromosome = new Chromosone(animal.IsFemale, BaseScale);
      this.chromosome.vLocation.Y += this.size.Y;
      this.chromosome.vLocation.Y += this.chromosome.GetSize().Y * 0.5f;
      this.size.Y += this.chromosome.GetSize().Y;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateAnimalWithChromosone(float DeltaTime)
    {
      if (!this.AnimateAndAddSpaceForJump)
        return;
      this.animalWithoutFrame.UpdateForAnimation(DeltaTime);
    }

    public void DrawAnimalWithChromosone(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.animalWithoutFrame.JustDrawAnimal(offset, spriteBatch);
      if (this.chromosome == null)
        return;
      this.chromosome.DrawChromosone(offset, spriteBatch);
    }
  }
}

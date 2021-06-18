// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Shelter.SheltderedAnimals.ShelteredAnimalRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Shelter.SheltderedAnimals
{
  internal class ShelteredAnimalRow
  {
    private SplitFrame splitnineslice;
    public Vector2 Location;
    private Column1Animal column1;
    private CollumnXBuy columnX;
    private AnimalRenderDescriptor animal;
    private float topSplitRatio;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private bool isBlackMarket;
    private ShelterAnimal refShelterAnimal;

    public ShelteredAnimalRow(Player player, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 _VSCale = this.scaleHelper.ScaleVector2(new Vector2(350f, 50f));
      this.topSplitRatio = 0.3f;
      this.splitnineslice = new SplitFrame(_VSCale, ColourData.Z_FrameLightBrown, ColourData.Z_FrameDarkBrown, this.BaseScale, this.topSplitRatio);
      this.splitnineslice.SetAlpha();
    }

    public void PopulateAnimalRow(Player player, ShelterAnimal shelterAnimal)
    {
      this.refShelterAnimal = shelterAnimal;
      AnimalRenderDescriptor _animal = new AnimalRenderDescriptor(shelterAnimal.animal, shelterAnimal.variant);
      int Cost = 20 + AnimalData.GetPriceForAnimalFromSheler(_animal.bodyAnimalType, _animal.variant);
      this.PopulateAnimalRow(player, _animal, Cost: Cost);
    }

    public void PopulateAnimalRow(
      Player player,
      AnimalRenderDescriptor _animal,
      bool _isBlackMarket = false,
      int Cost = -1,
      bool isAlreadyPurchased = false)
    {
      this.isBlackMarket = _isBlackMarket;
      float HeightForText = (float) ((double) this.topSplitRatio * (double) this.splitnineslice.VSCale.Y * 0.5);
      float MidTextHeight = (this.topSplitRatio + (float) ((1.0 - (double) this.topSplitRatio) * 0.5)) * this.splitnineslice.VSCale.Y;
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      Vector2 vector2 = -this.splitnineslice.VSCale * 0.5f;
      this.animal = _animal;
      this.column1 = new Column1Animal(this.animal, this.BaseScale);
      this.column1.Location.X = defaultBuffer.X;
      this.column1.Location.Y = MidTextHeight;
      this.column1.Location += vector2;
      this.columnX = new CollumnXBuy(Cost, HeightForText, MidTextHeight, player, this.BaseScale, this.isBlackMarket, isAlreadyPurchased);
      this.columnX.Location.X = this.splitnineslice.VSCale.X - this.scaleHelper.ScaleX(160f);
      this.columnX.Location += vector2;
      this.splitnineslice.SetAlpha(1f);
    }

    public Vector2 GetSize() => this.splitnineslice.VSCale;

    public bool UpdateShelteredAnimalRow(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      if (this.columnX == null || !this.columnX.UpdateCollumnXBuy(player, Offset, DeltaTime))
        return false;
      IntakeInfo prisoners = new IntakeInfo();
      CityName cityName = CityName.Shelter;
      if (this.isBlackMarket)
        cityName = CityName.BlackMarket;
      prisoners.CameFromHere = cityName;
      prisoners.People.Add(new IntakePerson(this.animal.bodyAnimalType, _IsAGirl: this.animal.IsFemale, Variant: this.animal.variant, _HeadType: this.animal.headAnimalType, _HeadVariant: this.animal.headVariant));
      if (player.livestats.AnimalsJustTraded == null)
        player.livestats.AnimalsJustTraded = new WaveInfo(prisoners);
      else
        player.livestats.AnimalsJustTraded.MergeIntakeInfo(prisoners);
      FeatureFlags.NewAnimalGot = true;
      if (!this.isBlackMarket && this.refShelterAnimal != null)
        player.shelterstocks.shelteredanimal.Remove(this.refShelterAnimal);
      return true;
    }

    public void DrawShelteredAnimalRow(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.splitnineslice.DrawSplitFrame(Offset, spriteBatch);
      if (this.column1 == null)
        return;
      this.column1.DrawColumn1Animal(Offset, spriteBatch);
      this.columnX.DrawCollumnXBuy(Offset, spriteBatch);
    }
  }
}

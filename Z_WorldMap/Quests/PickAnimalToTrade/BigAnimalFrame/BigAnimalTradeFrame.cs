// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame.BigAnimalTradeFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame
{
  internal class BigAnimalTradeFrame
  {
    private GameObjectNineSlice gameobjectninslice;
    public Vector2 Location;
    private Vector2 VScale;
    private Chromosone chromosone;
    private AlienEntry animalrenerer;
    private float MasterAlpha = 1f;
    private bool UsingString;
    private Color StringColour;
    private string TEXTSTring;

    public BigAnimalTradeFrame(
      AnimalType animal,
      bool IsAGirl,
      bool IsQuestReward,
      int VariantIndex = 0)
    {
      this.gameobjectninslice = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out Vector3 _), 7);
      this.gameobjectninslice.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      this.gameobjectninslice.SetAllColours(0.7490196f, 0.7098039f, 0.6117647f);
      this.VScale = new Vector2(200f, 200f);
      this.chromosone = new Chromosone(IsAGirl);
      float num = 1f;
      if (DebugFlags.IsPCVersion)
      {
        num = 0.5f;
        this.VScale *= num;
        this.chromosone.scale *= num;
      }
      this.chromosone.vLocation = new Vector2(80f * num, -80f * Sengine.ScreenRatioUpwardsMultiplier.Y * num);
      this.animalrenerer = new AlienEntry(animal, true, true, VariantIndex, 8f * num);
      this.animalrenerer.SetDrawOriginToCentre();
    }

    public void DarkenAnimal()
    {
      this.animalrenerer.SetAllColours(0.0f, 0.0f, 0.0f);
      this.animalrenerer.SetAlpha(0.4f);
    }

    public void SetOverAllAlphaMultiplier(float _MasterAlpha) => this.MasterAlpha = _MasterAlpha;

    public void SetString(string _TEXTSTring, Color _StringColour)
    {
      this.TEXTSTring = _TEXTSTring;
      this.StringColour = _StringColour;
      this.UsingString = true;
    }

    public void UpdateBigAnimalTradeFrame(float DeltaTime, Player player) => this.animalrenerer.UpdateAlienEntry(new Vector2(100000f, 1000000f), DeltaTime, player);

    public void DrawBigAnimalTradeFrame(Vector2 Offset)
    {
      float num = 1f;
      if (DebugFlags.IsPCVersion)
        num = 0.5f;
      Offset += this.Location;
      this.gameobjectninslice.fAlpha = this.MasterAlpha;
      this.gameobjectninslice.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VScale * Sengine.ScreenRatioUpwardsMultiplier);
      this.animalrenerer.fAlpha = this.MasterAlpha;
      this.animalrenerer.DrawAlienEntry(Offset, AssetContainer.pointspritebatchTop05, true);
      this.chromosone.fAlpha = this.MasterAlpha;
      this.chromosone.DrawChromosone(Offset, AssetContainer.pointspritebatchTop05);
      if (!this.UsingString)
        return;
      TextFunctions.DrawTextWithDropShadow(this.TEXTSTring, 0.5f, Offset + new Vector2(-90f * num, -90f * Sengine.ScreenRatioUpwardsMultiplier.Y * num), this.StringColour, this.MasterAlpha, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
    }
  }
}

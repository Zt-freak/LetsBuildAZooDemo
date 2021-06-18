// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.DeveloperMenu.AnimalAdder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Research;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Utils.DeveloperMenu
{
  internal class AnimalAdder
  {
    private CollectionScreenManager ColScreen;
    private BackButton close;
    private PriceAdjuster priceadjust;
    private AnimalType SelectedAnimal;
    private TextButton Go;
    private TextButton RandomVariants;
    private bool Variants;
    private string TEXT;

    public AnimalAdder(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.close = new BackButton(true, BaseScale: baseScaleForUi);
      this.ColScreen = new CollectionScreenManager(player, BaseScale: baseScaleForUi, isCustomSelection_addEntriesLater: true);
      List<AnimalType> animalTypeList = new List<AnimalType>();
      animalTypeList.Add(AnimalType.Rabbit);
      animalTypeList.AddRange((IEnumerable<AnimalType>) ResearchData.GetAliensReseachedInOrder());
      List<AlienEntry> _alienEntries = new List<AlienEntry>(animalTypeList.Count);
      foreach (AnimalType _enemy in animalTypeList)
        _alienEntries.Add(new AlienEntry(_enemy, true, true, 0, baseScaleForUi));
      this.ColScreen.AddAndPositionEntries(_alienEntries, baseScaleForUi);
      this.ColScreen.location -= this.ColScreen.GetOffsetFromTopLeft();
      this.ColScreen.location += new Vector2(512f, 384f) - new Vector2(this.ColScreen.GetWidth(), this.ColScreen.GetHeight()) * 0.5f;
      this.priceadjust = new PriceAdjuster(0, 100, 0);
      this.priceadjust.Location = new Vector2(200f, 700f);
      this.SelectedAnimal = AnimalType.None;
      this.Go = new TextButton(RenderMath.GetPixelZoomOneToOne(), "MAKE ANIMALS", 100f);
      this.Go.vLocation = new Vector2(900f, 700f);
      this.RandomVariants = new TextButton(RenderMath.GetPixelZoomOneToOne(), "Random Variants- Yes!", 200f);
      this.RandomVariants.vLocation = new Vector2(600f, 700f);
      this.Variants = true;
    }

    public bool UpdateAnimalAdder(Player player, float DeltaTime)
    {
      AnimalType enemytype = this.ColScreen.UpdateCollectionScreenManager(Vector2.Zero, DeltaTime, player, out bool _, out bool _);
      if (enemytype != AnimalType.None)
      {
        this.SelectedAnimal = enemytype;
        this.TEXT = "MAKE MANY: " + EnemyData.GetEnemyTypeName(enemytype) + "s";
      }
      if (this.SelectedAnimal != AnimalType.None)
      {
        this.priceadjust.UpdatePriceAdjuster(player, Vector2.Zero, DeltaTime);
        if (this.priceadjust.CurrentValue > 0)
        {
          if (this.RandomVariants.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            this.Variants = !this.Variants;
            if (this.Variants)
              this.RandomVariants.SetText("Random Variants- Yes!");
            else
              this.RandomVariants.SetText("Random Variants- No!");
          }
          if (this.Go.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          {
            IntakeInfo prisoners = new IntakeInfo();
            for (int index = 0; index < this.priceadjust.CurrentValue; ++index)
            {
              int Variant = 0;
              if (this.Variants)
                Variant = TinyZoo.Game1.Rnd.Next(0, 10);
              prisoners.People.Add(new IntakePerson(this.SelectedAnimal, _IsAGirl: true, Variant: Variant));
            }
            if (player.livestats.AnimalsJustTraded == null)
              player.livestats.AnimalsJustTraded = new WaveInfo(prisoners);
            else
              player.livestats.AnimalsJustTraded.MergeIntakeInfo(prisoners);
            player.livestats.AnimalsJustTraded.CameFromHere = CityName.Count;
            FeatureFlags.NewAnimalGot = true;
            Z_DebugFlags.SimulationIsVerbose = false;
          }
        }
      }
      return this.close.UpdateBackButton(player, DeltaTime);
    }

    public void DrawAnimalAdder()
    {
      this.close.DrawBackButton(Vector2.Zero, AssetContainer.pointspritebatch07Final);
      this.ColScreen.DrawCollectionScreenManager(Vector2.Zero, AssetContainer.pointspritebatch07Final);
      if (this.SelectedAnimal == AnimalType.None)
        return;
      TextFunctions.DrawJustifiedText(this.TEXT, 1f, new Vector2(512f, 600f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch07Final);
      this.priceadjust.DrawPriceAdjuster(Vector2.Zero, AssetContainer.pointspritebatch07Final);
      if (this.priceadjust.CurrentValue > 0)
        this.Go.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatch07Final);
      this.RandomVariants.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatch07Final);
    }
  }
}

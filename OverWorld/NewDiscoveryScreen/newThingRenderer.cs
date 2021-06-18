// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.NewDiscoveryScreen.newThingRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Tile_Data;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame;

namespace TinyZoo.OverWorld.NewDiscoveryScreen
{
  internal class newThingRenderer
  {
    private StoreBGManager storebgmanager;
    private GameObject thingunlocked;
    private GameObject thingunlocked1;
    private GameObject thingunlocked2;
    private Texture2D drawtex;
    private DualSinOscillator oscialltor;
    private DualSinOscillator oscialltor2;
    private List<Vector2> Locations;
    private float Timer;
    private BlackOut blackout;
    private List<AnimalRenderer> animal;
    private bool WasNew;
    private List<Chromosone> chromasones;
    private TextButton AddToZooo;
    private GameObjectNineSlice framer;
    private ScreenHeading screenherader;
    private GameObject TextColourThing;
    private string AnimalGetStringOne;
    private string AnimalGetStringTwo;
    private SimpleTextBox textbox;

    public newThingRenderer(TILETYPE buildingunlocked)
    {
      this.storebgmanager = new StoreBGManager(IsYellow: true);
      this.thingunlocked = new GameObject();
      if (TileData.IsThisACellBlock(buildingunlocked))
      {
        this.thingunlocked.DrawRect = TileStats.GetBuildingIconRectangle(buildingunlocked);
        this.drawtex = AssetContainer.SpriteSheet;
      }
      else
      {
        TileInfo tileInfo = TileData.GetTileInfo(buildingunlocked);
        this.drawtex = tileInfo.DrawTexture.texture;
        this.thingunlocked.DrawRect = tileInfo.GetRect(0, ref this.thingunlocked.Rotation);
      }
      this.thingunlocked.SetDrawOriginToCentre();
      this.thingunlocked.vLocation = new Vector2(512f, 300f);
      this.thingunlocked.scale = 300f / (float) this.thingunlocked.DrawRect.Width;
      this.thingunlocked.scale *= Sengine.UltraWideSreenDownardsMultiplier;
      this.oscialltor = new DualSinOscillator(MathStuff.getRandomFloat(0.2f, 0.4f), MathStuff.getRandomFloat(0.2f, 0.4f));
      this.oscialltor2 = new DualSinOscillator(MathStuff.getRandomFloat(0.2f, 0.4f), MathStuff.getRandomFloat(0.2f, 0.5f));
      this.thingunlocked1 = new GameObject(this.thingunlocked);
      this.thingunlocked2 = new GameObject(this.thingunlocked);
      this.Locations = new List<Vector2>();
      for (int index = 0; index < 10; ++index)
        this.Locations.Add(this.thingunlocked.vLocation);
      this.MakeFrame();
    }

    private void MakeFrame()
    {
      this.screenherader = new ScreenHeading(SEngine.Localization.Localization.GetText(947), 100f);
      this.screenherader.header.vLocation = Vector2.Zero;
      Vector3 SecondaryColour;
      this.framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.framer.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      this.TextColourThing = new GameObject();
      this.TextColourThing.SetAllColours(SecondaryColour);
    }

    public void AddBlackOut(bool fadein = false)
    {
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(1f, 1f, 0.0f);
      if (!fadein)
        return;
      this.blackout.SetAlpha(false, 0.5f, 0.0f, 1f);
    }

    public newThingRenderer(
      AnimalType enemytype,
      bool ItsABoy = false,
      bool WasBreedingPair = false,
      int Typeskn = 0,
      bool _WasNew = false)
    {
      this.storebgmanager = new StoreBGManager(IsYellow: true);
      this.WasNew = _WasNew;
      string enemyTypeName = EnemyData.GetEnemyTypeName(enemytype);
      if (this.WasNew)
        this.AnimalGetStringTwo = !WasBreedingPair ? string.Format(SEngine.Localization.Localization.GetText(949), (object) enemyTypeName) : string.Format(SEngine.Localization.Localization.GetText(948), (object) enemyTypeName);
      this.AnimalGetStringOne = !WasBreedingPair ? (!ItsABoy ? string.Format(SEngine.Localization.Localization.GetText(952), (object) enemyTypeName) : string.Format(SEngine.Localization.Localization.GetText(951), (object) enemyTypeName)) : string.Format(SEngine.Localization.Localization.GetText(950), (object) enemyTypeName);
      this.textbox = new SimpleTextBox(enemyTypeName + SEngine.Localization.Localization.GetText(953) + "!", 700f, textScale: 5f);
      this.chromasones = new List<Chromosone>();
      if (WasBreedingPair)
      {
        this.chromasones.Add(new Chromosone(true));
        this.chromasones[0].vLocation = new Vector2(800f, 100f);
        this.chromasones.Add(new Chromosone(false));
        this.chromasones[1].vLocation = new Vector2(900f, 100f);
      }
      else
      {
        this.chromasones.Add(new Chromosone(!ItsABoy));
        this.chromasones[0].vLocation = new Vector2(900f, 100f);
      }
      this.thingunlocked = new GameObject();
      this.animal = new List<AnimalRenderer>();
      this.animal.Add(new AnimalRenderer(enemytype, Typeskn));
      if (WasBreedingPair)
      {
        this.animal[0].enemy.vLocation.X -= (float) TinyZoo.Game1.Rnd.Next(100, 250);
        this.animal.Add(new AnimalRenderer(enemytype));
        this.animal[1].enemy.vLocation.X += (float) TinyZoo.Game1.Rnd.Next(100, 250);
      }
      this.drawtex = AssetContainer.SpriteSheet;
      this.thingunlocked.DrawRect = EnemyData.GetEnemyIdleRectangle(enemytype);
      this.thingunlocked.SetDrawOriginToCentre();
      this.thingunlocked.vLocation = new Vector2(512f, 300f);
      this.thingunlocked.scale = 300f / (float) this.thingunlocked.DrawRect.Width;
      this.oscialltor = new DualSinOscillator(MathStuff.getRandomFloat(0.2f, 0.4f), MathStuff.getRandomFloat(0.2f, 0.4f));
      this.oscialltor2 = new DualSinOscillator(MathStuff.getRandomFloat(0.2f, 0.4f), MathStuff.getRandomFloat(0.2f, 0.5f));
      this.thingunlocked.scale *= Sengine.ScreenRationReductionMultiplier.Y;
      this.thingunlocked1 = new GameObject(this.thingunlocked);
      this.thingunlocked2 = new GameObject(this.thingunlocked);
      this.Locations = new List<Vector2>();
      for (int index = 0; index < 10; ++index)
        this.Locations.Add(this.thingunlocked.vLocation);
      this.AddToZooo = new TextButton("Add to zoo", 90f);
      this.AddToZooo.vLocation = new Vector2(512f, 620f);
      this.MakeFrame();
    }

    public bool UpdatenewThingRenderer(float DeltaTime, Player player)
    {
      this.storebgmanager.UpdateStoreBGManager(DeltaTime);
      if (this.blackout != null)
        this.blackout.UpdateColours(DeltaTime);
      if (this.animal != null)
      {
        for (int index = 0; index < this.animal.Count; ++index)
        {
          this.animal[index].enemy.vLocation.Y = 400f;
          this.animal[index].UpdateAnimal(DeltaTime);
        }
        this.textbox.UpdateSimpleTextBox(DeltaTime, player);
      }
      else
      {
        this.thingunlocked.vLocation = new Vector2(512f, 400f);
        this.oscialltor.UpdateDualSinOscillator(DeltaTime);
        this.oscialltor2.UpdateDualSinOscillator(DeltaTime);
        this.thingunlocked.vLocation = new Vector2(512f, 300f) + (this.oscialltor2.CurrentOffset + this.oscialltor.CurrentOffset) * 30f;
        this.Locations[this.Locations.Count - 1] = this.thingunlocked.vLocation;
        this.Timer += DeltaTime;
        if ((double) this.Timer > 0.100000001490116)
        {
          this.Timer = 0.0f;
          for (int index = 0; index < this.Locations.Count - 1; ++index)
            this.Locations[index] = this.Locations[index + 1];
        }
      }
      return this.AddToZooo != null && this.AddToZooo.UpdateTextButton(player, Vector2.Zero, DeltaTime);
    }

    public void DrawnewThingRenderer(bool DrawHalfAlpha)
    {
      this.storebgmanager.DrawStoreBGManager(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.blackout != null)
        this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.animal != null)
      {
        for (int index = 0; index < this.animal.Count; ++index)
          this.animal[index].DrawAnimal();
        this.framer.vLocation = new Vector2(512f, 600f);
        Vector2 TotalScale = new Vector2(552f, 150f);
        this.framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, TotalScale);
        this.screenherader.DrawScreenHeading(this.framer.vLocation + new Vector2(0.0f, (float) (-(double) TotalScale.Y * 0.5)), AssetContainer.pointspritebatchTop05);
        TextFunctions.DrawJustifiedText(this.AnimalGetStringOne, 1f, this.framer.vLocation + new Vector2(0.0f, (float) (-(double) TotalScale.Y * 0.180000007152557)), this.TextColourThing.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
        if (this.WasNew)
        {
          TextFunctions.DrawJustifiedText(this.AnimalGetStringTwo, 2f, this.framer.vLocation, this.TextColourThing.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
          this.AddToZooo.vLocation.Y = this.framer.vLocation.Y + 35f;
        }
        else
          this.AddToZooo.vLocation.Y = this.framer.vLocation.Y + 25f;
        this.AddToZooo.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatchTop05);
      }
      else if (DrawHalfAlpha)
      {
        this.thingunlocked2.Draw(AssetContainer.PointBlendBatch04, this.drawtex, this.thingunlocked.vLocation, 0.3f);
        this.thingunlocked1.Draw(AssetContainer.PointBlendBatch04, this.drawtex, this.thingunlocked.vLocation, 0.3f);
        this.thingunlocked.Draw(AssetContainer.PointBlendBatch04, this.drawtex, this.oscialltor.CurrentOffset * -40f, 0.3f);
        this.thingunlocked.Draw(AssetContainer.PointBlendBatch04, this.drawtex, this.oscialltor.CurrentOffset * -20f, 0.3f);
        this.thingunlocked.Draw(AssetContainer.PointBlendBatch04, this.drawtex, this.oscialltor.CurrentOffset * 40f, 0.3f);
      }
      else
      {
        this.thingunlocked1.vLocation = Vector2.Zero;
        for (int index = 0; index < this.Locations.Count - 1; ++index)
          this.thingunlocked1.Draw(AssetContainer.PointBlendBatch04, this.drawtex, this.Locations[index], 0.5f);
        this.thingunlocked.Draw(AssetContainer.pointspritebatchTop05, this.drawtex, Vector2.Zero, 1f);
      }
      if (this.WasNew)
        TextFunctions.DrawJustifiedText(SEngine.Localization.Localization.GetText(954) + "!", 1.2f, new Vector2(512f, 60f), Color.Black, 0.4f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
      if (this.chromasones == null)
        return;
      for (int index = 0; index < this.chromasones.Count; ++index)
      {
        this.chromasones[index].vLocation = Vector2.Zero;
        this.chromasones[index].DrawChromosone(this.animal[index].enemy.vLocation + new Vector2(0.0f, 50f), AssetContainer.pointspritebatchTop05);
      }
    }
  }
}

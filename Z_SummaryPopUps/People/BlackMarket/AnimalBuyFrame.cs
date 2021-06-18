// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.BlackMarket.AnimalBuyFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;

namespace TinyZoo.Z_SummaryPopUps.People.BlackMarket
{
  internal class AnimalBuyFrame
  {
    private TextButton Button;
    private Vector2 Location;
    private GameObjectNineSlice box;
    private AnimalInFrame animalinframe;
    private Vector2 MainFrameScale;
    private string AnimalName;
    private bool Purchased;
    public AnimalType Body;
    public AnimalType Head;
    public Vector2 Locations;
    public int COST;
    public int HeadVariant;
    public int BodyVariant;

    public AnimalBuyFrame(
      Vector2 _MainFrameScale,
      AnimalType _Body,
      AnimalType _Head,
      bool Purchased,
      int _BodyVariant,
      int _HeadVariant)
    {
      this.Body = _Body;
      this.Head = _Head;
      this.BodyVariant = _BodyVariant;
      this.HeadVariant = _HeadVariant;
      this.Button = new TextButton(SEngine.Localization.Localization.GetText(9), 40f);
      this.Button.SetButtonColour(BTNColour.BlackMarketBright);
      this.Button.vLocation = new Vector2(150f, 0.0f);
      this.animalinframe = new AnimalInFrame(this.Body, this.Head, this.BodyVariant, 80f * Sengine.ScreenRationReductionMultiplier.Y, HeadVariant: this.HeadVariant);
      this.AnimalName = "Buy this " + HybridNames.GetAnimalCombinedName(this.Body, this.Head);
      this.box = new GameObjectNineSlice(new Rectangle(992, 578, 21, 21), 7);
      this.box.scale = 3f;
      this.box.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.MainFrameScale = new Vector2(_MainFrameScale.X - AnimalPopUpManager.Space, 100f);
      this.animalinframe.Location.X = this.MainFrameScale.X * -0.5f;
      this.animalinframe.Location.X += AnimalPopUpManager.VerticalBuffer;
      this.animalinframe.Location.X += (float) (80.0 * (double) Sengine.ScreenRationReductionMultiplier.Y * 0.5);
      this.COST = 400;
      if (!Purchased)
        return;
      this.SetPurchased();
    }

    public bool UpdateAnimalBuyFrame(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Locations;
      if (this.Purchased)
        return false;
      this.Button.vLocation = new Vector2(60f, 25f);
      return this.Button.UpdateTextButton(player, this.Location + Offset, DeltaTime);
    }

    public void SetPurchased()
    {
      this.Purchased = true;
      this.Button.SetButtonColour(BTNColour.Red);
      this.Button = (TextButton) null;
    }

    public void DrawAnimalBuyFrame(Vector2 Offset)
    {
      Offset += this.Locations;
      this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.MainFrameScale);
      this.animalinframe.DrawAnimalInFrame(Offset);
      if (this.Button != null)
      {
        this.Button.vLocation = new Vector2(60f, 25f);
        this.Button.DrawTextButton(Offset + this.Location, 1f, AssetContainer.pointspritebatchTop05);
      }
      TextFunctions.DrawTextWithDropShadow(this.AnimalName, 0.5f, Offset + this.Location + new Vector2(-60f, -30f * Sengine.ScreenRatioUpwardsMultiplier.Y), new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false);
      string stringToDraw = "$" + (object) this.COST;
      if (this.Purchased)
        stringToDraw = SEngine.Localization.Localization.GetText(746);
      if (this.Button != null)
        TextFunctions.DrawTextWithDropShadow(stringToDraw, 0.5f, Offset + this.Location + new Vector2(0.0f, 20f), new Color(ColourData.Z_Cream), 0.8f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false, true);
      else
        TextFunctions.DrawTextWithDropShadow(stringToDraw, 0.5f, Offset + this.Location + new Vector2(50f, 20f), new Color(ColourData.Z_Cream), 0.8f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05, false, true);
    }
  }
}

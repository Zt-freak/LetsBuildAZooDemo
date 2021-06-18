// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.BigPersonFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_ManageShop.FoodIcon;

namespace TinyZoo.Z_Manage.Hiring
{
  internal class BigPersonFrame
  {
    private Vector2 VScale;
    private GameObjectNineSlice gameobjectninslice;
    private AlienEntry animalrenerer;
    public float MasterAlpha;
    public Vector2 Location;
    private GameObject GreenCross;
    private GameObject FoodThing;
    public float OverallScaleMod;

    public BigPersonFrame(
      AnimalType employeetyps,
      bool UseGreenCross,
      string Name = "",
      float _OverallScaleMod = 1f)
    {
      this.OverallScaleMod = _OverallScaleMod;
      this.MasterAlpha = 1f;
      this.gameobjectninslice = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out Vector3 _), 7);
      this.gameobjectninslice.scale = 2f;
      this.gameobjectninslice.SetAllColours(0.7490196f, 0.7098039f, 0.6117647f);
      this.VScale = new Vector2(100f, 100f);
      if (DebugFlags.IsPCVersion)
        this.VScale *= 0.5f;
      this.animalrenerer = new AlienEntry(employeetyps, true, true, 0, 8f);
      this.animalrenerer.SetDrawOriginToCentre();
      if (!UseGreenCross)
        return;
      this.GreenCross = new GameObject();
      this.GreenCross.DrawRect = new Rectangle(965, 623, 9, 9);
      this.GreenCross.SetDrawOriginToCentre();
      this.GreenCross.SetAllColours(ColourData.Z_TextGreen);
      this.GreenCross.scale = 50f;
      if (!DebugFlags.IsPCVersion)
        return;
      this.GreenCross.scale *= 0.5f;
    }

    public BigPersonFrame(
      FOODTYPE foodtype,
      bool UseGreenCross,
      string Name = "",
      float _OverallScaleMod = 1f,
      float BaseScale = -1f)
    {
      if ((double) BaseScale > -1.0)
        this.OverallScaleMod = BaseScale;
      this.OverallScaleMod = _OverallScaleMod;
      this.MasterAlpha = 1f;
      this.gameobjectninslice = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out Vector3 _), 7);
      this.gameobjectninslice.scale = this.OverallScaleMod;
      this.gameobjectninslice.SetAllColours(0.7490196f, 0.7098039f, 0.6117647f);
      this.VScale = new Vector2(100f, 100f) * this.OverallScaleMod;
      this.FoodThing = new GameObject();
      this.FoodThing.DrawRect = FoodIconData.GetFoodRectangle(foodtype);
      this.FoodThing.SetDrawOriginToCentre();
      this.FoodThing.scale = 1f * this.OverallScaleMod;
      if (!UseGreenCross)
        return;
      this.GreenCross = new GameObject();
      this.GreenCross.DrawRect = new Rectangle(965, 623, 9, 9);
      this.GreenCross.SetDrawOriginToCentre();
      this.GreenCross.SetAllColours(ColourData.Z_TextGreen);
      this.GreenCross.scale = 50f * this.OverallScaleMod;
    }

    public void GreyOut()
    {
      if (this.FoodThing != null)
      {
        this.FoodThing.SetAllColours(0.0f, 0.0f, 0.0f);
        this.FoodThing.SetAlpha(0.3f);
      }
      else
      {
        this.animalrenerer.SetAllColours(0.0f, 0.0f, 0.0f);
        this.animalrenerer.SetAlpha(0.5f);
        this.MasterAlpha = 0.3f;
      }
    }

    public void UpdateBigPersonFrame(float DeltaTime, Player player) => this.animalrenerer.UpdateAlienEntry(new Vector2(-10000f, 0.0f), DeltaTime, player);

    public void DrawBigPersonFrame(Vector2 Offset, SpriteBatch spritebatch, bool SkipFrame = false)
    {
      Offset += this.Location;
      this.gameobjectninslice.fAlpha = this.MasterAlpha;
      this.gameobjectninslice.scale = 2f;
      if (!SkipFrame)
        this.gameobjectninslice.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale * Sengine.ScreenRatioUpwardsMultiplier);
      if (this.GreenCross != null)
      {
        this.GreenCross.scale = 6f;
        if (DebugFlags.IsPCVersion)
          this.GreenCross.scale *= 0.5f;
        this.GreenCross.fAlpha = this.MasterAlpha;
        this.GreenCross.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      }
      else if (this.FoodThing != null)
      {
        this.FoodThing.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      }
      else
      {
        this.animalrenerer.fAlpha = this.MasterAlpha;
        this.animalrenerer.scale = 3.5f;
        if (DebugFlags.IsPCVersion)
          this.animalrenerer.scale *= 0.5f;
        this.animalrenerer.DrawAlienEntry(Offset, spritebatch, true);
      }
    }
  }
}

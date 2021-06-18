// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.StoreRoomShelf
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Text;
using System;
using TinyZoo.PlayerDir.StoreRooms;

namespace TinyZoo.Z_StoreRoom.Shelves
{
  internal class StoreRoomShelf : GameObject
  {
    private AnimalFoodIcon animalfoodicon;
    private int ShelfLife;
    private float InStock;
    private GameObjectNineSlice frame;
    private Vector2 MainFrameScale;
    private string TopString;
    private StringScroller scroller;
    private bool Selected;
    public StockEntry RefSTockEntry;
    private int RemainingSpace;

    public StoreRoomShelf(StockEntry stockentry)
    {
      this.RefSTockEntry = stockentry;
      this.Selected = false;
      if (stockentry.foodtype != AnimalFoodType.Count)
      {
        this.animalfoodicon = new AnimalFoodIcon(stockentry.foodtype, 1f);
        this.InStock = stockentry.GetTotalStock();
        this.ShelfLife = stockentry.ShelfLifeRemaining;
      }
      this.CreateShelf();
      this.scale = RenderMath.GetPixelSizeBestMatch(1f);
      if ((double) this.scale > 1.0)
        this.scale = 1f;
      this.scroller = new StringScroller(90f / this.scale, AnimalFoodData.GetAnimalFoodTypeToString(stockentry.foodtype), AssetContainer.springFont);
      this.TopString = "Empty Space";
    }

    public StoreRoomShelf(int _RemainingSpace)
    {
      this.RemainingSpace = _RemainingSpace;
      this.CreateShelf();
      this.TopString = "Empty Space";
      this.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.scroller = new StringScroller(90f / this.scale, "Empty Space", AssetContainer.springFont);
    }

    public void Select(bool SelectNow)
    {
      if (SelectNow == this.Selected)
        return;
      this.Selected = SelectNow;
      if (!this.Selected)
      {
        this.frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
        this.frame.scale = 2f;
      }
      else
      {
        this.frame = new GameObjectNineSlice(new Rectangle(969, 64, 21, 21), 7);
        this.frame.scale = 2f;
      }
    }

    private void CreateShelf()
    {
      this.SetAllColours(ColourData.Z_Cream);
      this.DrawRect = new Rectangle(0, 0, 100, 100);
      this.SetDrawOriginToCentre();
      this.frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.frame.scale = 2f;
      this.MainFrameScale = new Vector2(100f, 100f);
    }

    public bool UpdateStoreRoomShelf(float DeltaTime, Player player, Vector2 Offset)
    {
      Offset += this.vLocation;
      this.scroller.UpdateStringScroller(DeltaTime);
      return MathStuff.CheckPointCollision(true, Offset, 1f, this.MainFrameScale.X, this.MainFrameScale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawStoreRoomShelf(Vector2 Offset)
    {
      Offset += this.vLocation;
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.MainFrameScale * Sengine.ScreenRatioUpwardsMultiplier);
      TextFunctions.DrawTextWithDropShadow(this.scroller.GetString(), this.scale, Offset + new Vector2(-40f, -40f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
      if (this.RemainingSpace > 0)
      {
        TextFunctions.DrawJustifiedText(this.RemainingSpace.ToString(), this.scale * 1f, Offset + new Vector2(0.0f, 10f), this.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
        TextFunctions.DrawTextWithDropShadow("Fill your stores!", this.scale, Offset + new Vector2(40f, 35f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false, true);
      }
      else
      {
        TextFunctions.DrawTextWithDropShadow("Stock: " + (object) Math.Round((double) this.InStock, 1), this.scale, Offset + new Vector2(-40f, 25f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
        TextFunctions.DrawTextWithDropShadow("Shelf Life: " + (object) this.ShelfLife + "d", this.scale, Offset + new Vector2(-40f, 35f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false);
      }
      if (this.animalfoodicon == null)
        return;
      this.animalfoodicon.DrawAnimalFoodIcon(Offset, false);
    }
  }
}

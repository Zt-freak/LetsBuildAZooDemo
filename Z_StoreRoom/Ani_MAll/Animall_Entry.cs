// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.Animall_Entry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Input;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_StoreRoom.Ani_MAll
{
  internal class Animall_Entry
  {
    public Vector2 Location;
    private GameObjectNineSlice gameobjectnineslice;
    private Vector2 VSCale;
    public Vector2 vLocation;
    private GameObject Frame;
    public AnimalFoodIcon animalfoodicon;
    private StarBarStar star;
    public int CostPerUnit;
    private AddToCart addtocartbutton;
    public int DeleiveryTime;
    private ButtonInfoBox shelflife;
    private ButtonInfoBox DeliveryTime;
    public StockNumber stocknumber;
    public AnimalFoodType animalFoodType;
    private string Eater;
    private float BaseScale;
    private AddToCart Remove;
    private int Counter;

    public Animall_Entry(AnimalFoodType animalfoodtype, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.Eater = AnimalFoodData.GetEater(animalfoodtype);
      this.CostPerUnit = AnimalFoodData.GetAnimalFoodInfo(animalfoodtype).Cost;
      this.DeleiveryTime = AnimalFoodData.GetAnimalFoodInfo(animalfoodtype).DeliveryTime;
      this.animalFoodType = animalfoodtype;
      this.stocknumber = new StockNumber(_BaseScale);
      this.Frame = new GameObject();
      this.Frame.DrawRect = new Rectangle(629, 125, 74, 134);
      this.Frame.scale = 2f * this.BaseScale;
      this.Frame.SetDrawOriginToCentre();
      this.gameobjectnineslice = new GameObjectNineSlice(new Rectangle(895, 394, 21, 21), 7);
      this.gameobjectnineslice.scale = this.BaseScale * 2f;
      this.VSCale = new Vector2(170f, 200f) * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier;
      this.animalfoodicon = new AnimalFoodIcon(animalfoodtype, 2f * this.BaseScale);
      this.animalfoodicon.vLocation.Y = -65f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.BaseScale;
      this.star = new StarBarStar(1f, this.BaseScale);
      float num1 = 15f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num2 = uiScaleHelper.ScaleY(2f);
      this.shelflife = new ButtonInfoBox(this.BaseScale);
      this.shelflife.vLocation.Y = num1;
      this.shelflife.vLocation.Y += this.shelflife.GetSize().Y * 0.5f;
      float num3 = num1 + (this.shelflife.GetSize().Y + num2);
      this.DeliveryTime = new ButtonInfoBox(this.BaseScale);
      this.DeliveryTime.vLocation.Y = num3;
      this.DeliveryTime.SetAsDeleiveryTime(this.DeleiveryTime);
      this.DeliveryTime.vLocation.Y += this.DeliveryTime.GetSize().Y * 0.5f;
      float num4 = num3 + (this.DeliveryTime.GetSize().Y + num2);
      this.addtocartbutton = new AddToCart(this.BaseScale);
      this.addtocartbutton.vLocation.Y = num4;
      this.addtocartbutton.vLocation.Y += this.addtocartbutton.GetSize().Y * 0.5f;
      float num5 = num4 + this.addtocartbutton.GetSize().Y + num2 * 0.5f;
      this.Remove = new AddToCart(this.BaseScale, IsRemove: true);
      this.Remove.vLocation.Y = num5;
      this.Remove.vLocation.Y += this.Remove.GetSize().Y * 0.5f;
      this.shelflife.SetAsShelfLifeTime(AnimalFoodData.GetAnimalFoodInfo(animalfoodtype).ShelfLife);
      if (Z_GameFlags.StoreRoomGoToThisFood != animalfoodtype)
        return;
      this.Frame.SetAllColours(1f, 1f, 0.7f);
    }

    public Vector2 GetSize() => new Vector2((float) this.Frame.DrawRect.Width * this.Frame.scale, (float) this.Frame.DrawRect.Height * this.Frame.scale * Sengine.ScreenRatioUpwardsMultiplier.Y);

    public bool UpdateAnimall_Entry(Vector2 Offset, Player player, float DeltaTime)
    {
      this.animalfoodicon.UpdateStringScroll(DeltaTime);
      this.stocknumber.UpdateStockNumber(DeltaTime);
      if (this.stocknumber.Value > 0 && this.Remove.UpdateAddToCart(player, DeltaTime, Offset + this.Location))
        this.stocknumber.Clear();
      if (this.addtocartbutton.UpdateAddToCart(player, DeltaTime, Offset + this.Location))
      {
        if (MouseStatus.RMouseHeld)
        {
          if (this.stocknumber.Value > 0)
          {
            ++this.Counter;
            if (this.Counter < 10)
              this.stocknumber.AddValue(-1);
            else
              this.stocknumber.AddValue(-10);
            if (this.stocknumber.Value < 0)
              this.stocknumber.Value = 0;
          }
        }
        else
        {
          this.Counter = 0;
          if (this.stocknumber.Value < 999)
          {
            if (this.addtocartbutton.buttonrepeater.Counter >= 29)
            {
              this.stocknumber.AddValue(10);
              this.stocknumber.Value /= 10;
              this.stocknumber.Value *= 10;
            }
            else
              this.stocknumber.AddValue(1);
            if (this.stocknumber.Value > 999)
              this.stocknumber.Value = 999;
          }
        }
      }
      else
        this.Counter = 0;
      return false;
    }

    public void DrawAnimall_Entry(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      Offset += this.Location;
      this.Frame.Draw(DrawWithThis, AssetContainer.SpriteSheet, Offset);
      this.animalfoodicon.DrawAnimalFoodIcon(Offset, true, DrawWithThis);
      TextFunctions.DrawJustifiedText("$" + (object) this.CostPerUnit, RenderMath.GetPixelSizeBestMatch(2f * this.BaseScale), Offset + this.animalfoodicon.vLocation + new Vector2(0.0f, 50f * Sengine.ScreenRatioUpwardsMultiplier.Y), new Color(0.8392157f, 0.3568628f, 0.2980392f), 1f, AssetContainer.SpringFontX1AndHalf, DrawWithThis);
      TextFunctions.DrawTextWithDropShadow(this.Eater, RenderMath.GetPixelSizeBestMatch(1.333f * this.BaseScale), Offset + new Vector2(-23f * this.Frame.scale, -65f * this.Frame.scale * Sengine.ScreenRatioUpwardsMultiplier.Y), new Color(ColourData.Z_DarkTextGray), 1f, AssetContainer.SpringFontX1AndHalf, DrawWithThis, false);
      this.star.DrawStar(DrawWithThis, Offset + new Vector2(-30f * this.Frame.scale, -62f * this.Frame.scale * Sengine.ScreenRatioUpwardsMultiplier.Y));
      if (this.stocknumber.Value > 0)
      {
        this.stocknumber.Location = new Vector2(18f, -35f) * this.BaseScale;
        this.stocknumber.DrawStockNumber(Offset + new Vector2(30f, -40f) * this.BaseScale, DrawWithThis);
      }
      this.shelflife.DrawButtonInfoBox(Offset, DrawWithThis);
      this.DeliveryTime.DrawButtonInfoBox(Offset, DrawWithThis);
      this.addtocartbutton.DrawAddToCart(Offset, DrawWithThis);
      if (this.stocknumber.Value <= 0)
        return;
      this.Remove.DrawAddToCart(Offset, DrawWithThis);
    }
  }
}

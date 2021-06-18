// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView.ProductsInBuilding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView
{
  internal class ProductsInBuilding
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private string baseString;
    private List<MeatWithNumber> productList;
    private Z_ScrollHelper scrollHelper;
    private FactoryProductionLine reffactoryproduction;
    private int lastProductCount;
    private float BaseScale;
    private Vector2 contentsSize;

    public ProductsInBuilding(
      FactoryProductionLine factoryproduction,
      float _BaseScale,
      float ForcedHeight,
      float ForcedWidth)
    {
      this.BaseScale = _BaseScale;
      this.reffactoryproduction = factoryproduction;
      this.productList = new List<MeatWithNumber>();
      this.customerFrame = new CustomerFrame(new Vector2(ForcedWidth, ForcedHeight), CustomerFrameColors.DarkBrown, this.BaseScale);
      this.baseString = "Products Waiting For Collection: ";
      this.miniHeading = new MiniHeading(this.customerFrame.VSCale, this.baseString + (object) 0, 1f, this.BaseScale);
      this.SetUp();
    }

    private void SetUp()
    {
      this.productList.Clear();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.contentsSize += defaultBuffer;
      int num1 = 0;
      if (this.reffactoryproduction.additionfactoryproducts != null)
      {
        for (int index = 0; index < this.reffactoryproduction.additionfactoryproducts.Length; ++index)
        {
          if (this.reffactoryproduction.additionfactoryproducts[index] != null)
          {
            int Number = (int) Math.Ceiling((double) this.reffactoryproduction.additionfactoryproducts[index].TotalCompletedProductsHeld - (double) this.reffactoryproduction.additionfactoryproducts[index].TotalOutOnDelivery);
            if (Number > 0)
            {
              this.productList.Add(new MeatWithNumber((AnimalFoodType) index, this.BaseScale, Number, ForceConsistentSize: true));
              num1 += (int) Math.Floor((double) this.reffactoryproduction.additionfactoryproducts[index].TotalCompletedProductsHeld);
            }
          }
        }
      }
      List<DeadAnimal> animalsLeftOvers = this.reffactoryproduction.GetDeadAnimalsLeftOvers();
      if (animalsLeftOvers != null && animalsLeftOvers.Count > 0)
      {
        this.productList.Add(new MeatWithNumber(AnimalFoodType.DeadAnimal_Processed, this.BaseScale, animalsLeftOvers.Count, ForceConsistentSize: true));
        num1 += animalsLeftOvers.Count;
      }
      Vector2 vector2 = uiScaleHelper.ScaleVector2(new Vector2(70f, 30f));
      int num2 = (int) Math.Floor(((double) this.customerFrame.VSCale.X - (double) defaultBuffer.X) / ((double) vector2.X + (double) defaultBuffer.X));
      for (int index = 0; index < this.productList.Count; ++index)
      {
        this.productList[index].location = defaultBuffer;
        this.productList[index].location.Y += this.miniHeading.GetTextHeight(true) + defaultBuffer.Y * 0.5f;
        this.productList[index].location.X += (vector2.X + defaultBuffer.X) * (float) (index % num2);
        this.productList[index].location.Y += (vector2.Y + defaultBuffer.Y) * (float) (index / num2);
        this.productList[index].location.Y += this.productList[index].GetSize().Y * 0.5f;
        this.productList[index].location -= this.customerFrame.VSCale * 0.5f;
      }
      int num3 = (int) Math.Ceiling((double) this.productList.Count / (double) num2);
      this.contentsSize.X = this.customerFrame.VSCale.X;
      this.contentsSize.Y = (float) ((double) defaultBuffer.Y + (double) num3 * (double) vector2.Y + (double) (num3 - 1) * (double) defaultBuffer.Y) + defaultBuffer.Y;
      this.lastProductCount = num1;
      this.miniHeading.SetNewText(this.baseString + (object) num1);
      this.contentsSize.X = this.customerFrame.VSCale.X;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void AddScroll(float maxHeight)
    {
    }

    public void UpdateProductsInBuilding(Player player, Vector2 offset) => offset += this.location;

    public void DrawProductsInBuilding(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      for (int index = 0; index < this.productList.Count; ++index)
        this.productList[index].DrawMeatWithNumber(offset, spriteBatch);
    }
  }
}

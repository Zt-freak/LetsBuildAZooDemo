// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific.MeatMadeInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.MeatView;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific
{
  internal class MeatMadeInfo : InfoPopupFrameBase
  {
    private List<MeatWithNumber> meats;
    private SimpleTextHandler desc;

    public MeatMadeInfo(ShopEntry shopEntry, float BaseScale)
      : base(BaseScale)
    {
      this.meats = new List<MeatWithNumber>();
      Vector2 buffer = this.buffer;
      Vector2 size = new MeatWithNumber(AnimalFoodType.Pork, BaseScale, Game1.Rnd.Next(1, 5)).GetSize();
      int num1 = 3;
      int num2 = 0;
      float width_ = (float) ((double) size.X * (double) num1 + (double) this.buffer.X * (double) (num1 - 1));
      string TextToWrite = string.Empty;
      if (TileData.IsAVegetableProcessingPlant(shopEntry.tiletype))
      {
        TextToWrite = "Displaying vegetable products produced in this factory yesterday.";
        if (num2 == 0)
          TextToWrite = "You did not produce any vegetable products in this factory yesterday.";
      }
      else if (TileData.IsAMeatProcessingPlant(shopEntry.tiletype))
      {
        TextToWrite = "Displaying meat produced in this factory yesterday.";
        if (num2 == 0)
          TextToWrite = "You did not produce any meat in this factory yesterday.";
      }
      this.desc = new SimpleTextHandler(TextToWrite, width_, true, BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location.Y = buffer.Y;
      this.desc.Location.Y += this.desc.GetHeightOfOneLine() * 0.5f;
      buffer.Y += this.desc.GetHeightOfParagraph();
      buffer.Y += this.buffer.Y;
      for (int index = 0; index < num2; ++index)
      {
        MeatWithNumber meatWithNumber = new MeatWithNumber(AnimalFoodType.Pork, BaseScale, Game1.Rnd.Next(1, 5));
        meatWithNumber.location = buffer;
        meatWithNumber.location.X += (size.X + this.buffer.X) * (float) (index % num1);
        meatWithNumber.location.Y += (size.Y + this.buffer.Y) * (float) (index / num1);
        meatWithNumber.location.Y += meatWithNumber.GetSize().Y * 0.5f;
        this.meats.Add(meatWithNumber);
      }
      buffer.X += width_;
      int num3 = (int) Math.Ceiling((double) num2 / (double) num1);
      buffer.Y += (float) ((double) size.Y * (double) num3 + (double) this.buffer.Y * (double) (num3 - 1));
      buffer += this.buffer;
      this.customerFrame.Resize(buffer);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      for (int index = 0; index < this.meats.Count; ++index)
        this.meats[index].location += vector2;
      this.desc.Location.Y += vector2.Y;
    }

    public override void DrawInfoPopupFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      base.DrawInfoPopupFrame(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      for (int index = 0; index < this.meats.Count; ++index)
        this.meats[index].DrawMeatWithNumber(offset, spriteBatch);
    }
  }
}

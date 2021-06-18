// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific.AnimalsProcessedInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific
{
  internal class AnimalsProcessedInfo : InfoPopupFrameBase
  {
    private SimpleTextHandler simpleTextHandler;
    private List<AnimalInFrame> animals;

    public AnimalsProcessedInfo(ShopEntry shopEntry, float BaseScale)
      : base(BaseScale)
    {
      List<DeadAnimal> deadAnimalList = new List<DeadAnimal>();
      Vector2 buffer = this.buffer;
      int num1 = 4;
      Vector2 vector2_1 = this.scaleHelper.ScaleVector2(Vector2.One * 35f);
      float width_ = (float) ((double) vector2_1.X * (double) num1 + (double) this.buffer.X * (double) (num1 - 1));
      string empty = string.Empty;
      this.simpleTextHandler = new SimpleTextHandler(deadAnimalList.Count != 0 ? (shopEntry.tiletype != TILETYPE.Incinerator ? (!TileData.IsAVegetableProcessingPlant(shopEntry.tiletype) ? "Displaying animals processed in this factory yesterday." : "Displaying crops processed in this factory yesterday.") : "Displaying animals burnt in this incinerator yesterday.") : (!TileData.IsAnIncinerator(shopEntry.tiletype) ? (!TileData.IsAVegetableProcessingPlant(shopEntry.tiletype) ? "You did not process any animals in this factory yesterday." : "You did not process any crops in this factory yesterday.") : "You did not burn any animals in this incinerator yesterday."), width_, true, BaseScale, AutoComplete: true);
      this.simpleTextHandler.SetAllColours(ColourData.Z_Cream);
      this.simpleTextHandler.Location.Y = buffer.Y;
      this.simpleTextHandler.Location.Y += this.simpleTextHandler.GetHeightOfOneLine() * 0.5f;
      buffer.Y += this.simpleTextHandler.GetHeightOfParagraph();
      buffer.Y += this.buffer.Y;
      this.animals = new List<AnimalInFrame>();
      for (int index = 0; index < deadAnimalList.Count; ++index)
      {
        DeadAnimal deadAnimal = deadAnimalList[index];
        AnimalInFrame animalInFrame = new AnimalInFrame(deadAnimal.animalType, deadAnimal.headType, deadAnimal.variant, vector2_1.X, 6f * BaseScale, BaseScale, deadAnimal.headVariant, deadAnimal.cropType, true);
        animalInFrame.Location = buffer;
        animalInFrame.Location.X += (vector2_1.X + this.buffer.X) * (float) (index % num1);
        animalInFrame.Location.Y += (vector2_1.Y + this.buffer.Y) * (float) (index / num1);
        animalInFrame.Location += animalInFrame.GetSize() * 0.5f;
        this.animals.Add(animalInFrame);
      }
      buffer.X += width_;
      int num2 = (int) Math.Ceiling((double) deadAnimalList.Count / (double) num1);
      buffer.Y += (float) ((double) vector2_1.Y * (double) num2 + (double) this.buffer.Y * (double) (num2 - 1));
      buffer += this.buffer;
      this.customerFrame.Resize(buffer);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.simpleTextHandler.Location.Y += vector2_2.Y;
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].Location += vector2_2;
    }

    public override void UpdateInfoPopupFrame() => base.UpdateInfoPopupFrame();

    public override void DrawInfoPopupFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      base.DrawInfoPopupFrame(offset, spriteBatch);
      this.simpleTextHandler.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      for (int index = 0; index < this.animals.Count; ++index)
        this.animals[index].JustDrawAnimal(offset, spriteBatch);
    }
  }
}

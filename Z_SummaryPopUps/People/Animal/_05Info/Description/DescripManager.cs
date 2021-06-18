// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Description.DescripManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Description
{
  internal class DescripManager
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private SimpleTextHandler simpleTextHandler;
    private List<ZGenericText> infoRows;

    public DescripManager(AnimalType animalType, float width, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.customerframe = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerframe.AddMiniHeading("About");
      this.simpleTextHandler = new SimpleTextHandler("This will be a description describing the animal... like a bit of facts or traits about it!", width - defaultBuffer.X * 2f, _Scale: BaseScale, AutoComplete: true);
      this.simpleTextHandler.SetAllColours(ColourData.Z_Cream);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerframe.GetMiniHeadingHeight();
      Vector2 _vScale = zero + defaultBuffer;
      this.simpleTextHandler.Location = _vScale;
      _vScale.Y += this.simpleTextHandler.GetHeightOfParagraph();
      _vScale.Y += defaultBuffer.Y;
      this.infoRows = new List<ZGenericText>();
      for (int index = 0; index < 3; ++index)
      {
        ZGenericText zgenericText = new ZGenericText(BaseScale, false);
        switch (index)
        {
          case 0:
            zgenericText.textToWrite = string.Format("Life Expectancy: {0} days", (object) AnimalData.GetLifeExectancy(animalType, false, out int _));
            break;
          case 1:
            zgenericText.textToWrite = string.Format("Average Weight: {0} kg", (object) AnimalData.GetAnimalWeight(animalType));
            break;
          case 2:
            zgenericText.textToWrite = "Fertility Rate: ???";
            break;
        }
        zgenericText.vLocation = _vScale;
        _vScale.Y += zgenericText.GetSize().Y;
        this.infoRows.Add(zgenericText);
      }
      _vScale.Y += defaultBuffer.Y;
      _vScale.X = width;
      this.customerframe.Resize(_vScale);
      Vector2 vector2 = -this.customerframe.VSCale * 0.5f;
      this.simpleTextHandler.Location += vector2;
      for (int index = 0; index < this.infoRows.Count; ++index)
      {
        ZGenericText infoRow = this.infoRows[index];
        infoRow.vLocation = infoRow.vLocation + vector2;
      }
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void UpdateDescripManager()
    {
    }

    public void DrawDescripManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.simpleTextHandler.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      for (int index = 0; index < this.infoRows.Count; ++index)
        this.infoRows[index].DrawZGenericText(offset, spriteBatch);
    }
  }
}

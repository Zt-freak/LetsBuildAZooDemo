// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer.TextBox.NewThingTextBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer.TextBox
{
  internal class NewThingTextBox
  {
    public Vector2 location;
    private CustomerFrame frame;
    private ScreenHeading screenHeading;
    private SimpleTextHandler para;
    private SimpleTextHandler smallPara;
    private TextButton textButton;

    public NewThingTextBox(
      List<AnimalRenderDescriptor> animals,
      float BaseScale,
      float minWidth = -1f,
      bool IsForGenomeCompleted = false)
    {
      float defaultYbuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      float num1 = 0.0f;
      float num2 = 300f * BaseScale;
      if ((double) minWidth != -1.0)
        num2 = Math.Max(minWidth, num2);
      string empty1 = string.Empty;
      string text = SEngine.Localization.Localization.GetText(936);
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string str1 = animals[0].headAnimalType == AnimalType.None ? EnemyData.GetEnemyTypeName(animals[0].bodyAnimalType) : HybridNames.GetAnimalCombinedName(animals[0].bodyAnimalType, animals[0].headAnimalType);
      string str2;
      string TextToWrite1;
      string TextToWrite2;
      if (IsForGenomeCompleted)
      {
        str2 = "New Genome!";
        TextToWrite1 = string.Format("You have discovered all the {0} variants!", (object) str1);
        TextToWrite2 = string.Format("The {0} genome has been mapped!~You can now splice it at the CRIPSR Splicer!", (object) str1);
      }
      else if (animals.Count == 1)
      {
        str2 = "New Animal!";
        TextToWrite1 = string.Format("Received: {0}", (object) str1);
        TextToWrite2 = SEngine.Localization.Localization.GetText(949);
      }
      else
      {
        str2 = SEngine.Localization.Localization.GetText(947);
        TextToWrite1 = string.Format(SEngine.Localization.Localization.GetText(950), (object) str1, (object) animals.Count);
        TextToWrite2 = SEngine.Localization.Localization.GetText(948);
      }
      this.screenHeading = new ScreenHeading(str2.ToUpper(), 28f, BaseScale: BaseScale, UseSmallerOnePointFiveFont: true);
      this.screenHeading.header.vLocation = Vector2.Zero;
      float num3 = num1 + ((float) ((double) this.screenHeading.header.VScale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5) + defaultYbuffer);
      float width_ = num2 * 0.9f;
      this.para = new SimpleTextHandler(TextToWrite1, width_, true, BaseScale, true, true);
      this.para.SetAllColours(ColourData.Z_FrameMidBrown);
      this.para.Location.Y = num3;
      this.para.Location.Y += this.para.GetHeightOfOneLine() * 0.5f;
      float num4 = num3 + this.para.GetHeightOfParagraph();
      this.smallPara = new SimpleTextHandler(TextToWrite2, width_, true, BaseScale, AutoComplete: true);
      this.smallPara.SetAllColours(ColourData.Z_FrameMidBrown);
      this.smallPara.Location.Y = num4;
      this.smallPara.Location.Y += this.smallPara.GetHeightOfOneLine() * 0.5f;
      float num5 = num4 + (this.smallPara.GetHeightOfParagraph() + defaultYbuffer * 0.5f);
      this.textButton = new TextButton(BaseScale, text, 50f);
      this.textButton.vLocation.Y = num5 + this.textButton.GetSize_True().Y * 0.5f;
      float y = num5 + (this.textButton.GetSize_True().Y + defaultYbuffer);
      this.frame = new CustomerFrame(new Vector2(num2, y), CustomerFrameColors.CreamWithBorder, BaseScale);
      Vector2 vector2 = -this.frame.VSCale * 0.5f;
      this.screenHeading.header.vLocation.Y += vector2.Y;
      this.para.Location.Y += vector2.Y;
      this.smallPara.Location.Y += vector2.Y;
      this.textButton.vLocation.Y += vector2.Y;
    }

    public float GetExtraHeightOffset() => (float) ((double) this.screenHeading.header.VScale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y * 0.5);

    public Vector2 GetSize(bool includeHeaderOnTop = true)
    {
      Vector2 vsCale = this.frame.VSCale;
      if (includeHeaderOnTop)
        vsCale.Y += this.GetExtraHeightOffset();
      return vsCale;
    }

    public bool UpdateNewThingTextBox(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.textButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawNewThingTextBox(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spriteBatch);
      this.screenHeading.DrawScreenHeading(offset, spriteBatch);
      this.para.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.smallPara.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.textButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}

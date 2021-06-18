// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific.TotalIndustryRevenueInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_BuildingInfo.Generic.Summary.Header;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific
{
  internal class TotalIndustryRevenueInfo : InfoPopupFrameBase
  {
    private List<ZGenericText> textLines;
    private SimpleTextHandler desc;
    private Vector2 size;
    private float BaseScale;

    public TotalIndustryRevenueInfo(IndustryType industryType, float _BaseScale)
      : base(_BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.textLines = new List<ZGenericText>();
      float width_ = uiScaleHelper.ScaleX(175f);
      string TextToWrite = "I AM A DESCRIPTION";
      string text = "I AM A HEADER";
      switch (industryType)
      {
        case IndustryType.Industry:
          TextToWrite = "Displaying yesterday's report based on all industries in your zoo.";
          text = "Industries Overview";
          break;
        case IndustryType.Commerce:
          TextToWrite = "Displaying yesterday's report based on all shops in your zoo.";
          text = "Shops Overview";
          break;
      }
      this.customerFrame.AddMiniHeading(text);
      this.size.Y += this.customerFrame.GetMiniHeadingHeight();
      this.size.Y += defaultBuffer.Y;
      this.desc = new SimpleTextHandler(TextToWrite, width_, true, this.BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location = this.size;
      this.size.Y += this.desc.GetHeightOfParagraph();
      this.size.Y += defaultBuffer.Y;
      this.size.X = defaultBuffer.X;
      if (industryType == IndustryType.Industry)
      {
        this.AddTextLine("Total Animals Processed: 0");
        this.AddTextLine("Total Produce Made: 0");
        this.AddTextLine("Total Produce Used: 0");
        this.AddTextLine("Total Produce Sold: 0");
        this.AddTextLine("Total Produce Stockpiled: 0");
        this.AddTextLine("");
        this.AddTextLine("Total Money Saved: $0");
        this.AddTextLine("Total Sales Revenue: $0");
        this.AddTextLine("Total Net Value: $0");
      }
      this.size.X += width_;
      this.size.X += defaultBuffer.X;
      this.size.Y += defaultBuffer.Y;
      this.customerFrame.Resize(this.size);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.desc.Location.Y += vector2.Y;
      for (int index = 0; index < this.textLines.Count; ++index)
      {
        ZGenericText textLine = this.textLines[index];
        textLine.vLocation = textLine.vLocation + vector2;
      }
    }

    private void AddTextLine(string textToWrite, bool ConsistentLineHeights = true)
    {
      ZGenericText zgenericText = new ZGenericText(textToWrite, this.BaseScale, false);
      if (textToWrite == "")
        zgenericText.textToWrite = "Xg";
      zgenericText.vLocation = this.size;
      if (ConsistentLineHeights)
      {
        if (zgenericText.fontToUse == AssetContainer.springFont)
          this.size.Y += this.scaleHelper.ScaleY(10f);
        else
          this.size.Y += this.scaleHelper.ScaleY(14f);
      }
      else
        this.size.Y += zgenericText.GetSize().Y;
      this.textLines.Add(zgenericText);
      zgenericText.textToWrite = textToWrite;
    }

    public override void UpdateInfoPopupFrame() => base.UpdateInfoPopupFrame();

    public override void DrawInfoPopupFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      base.DrawInfoPopupFrame(offset, spriteBatch);
      for (int index = 0; index < this.textLines.Count; ++index)
        this.textLines[index].DrawZGenericText(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}

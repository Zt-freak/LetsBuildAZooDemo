// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific.OperationWorkloadInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.InformationPopup.Specific
{
  internal class OperationWorkloadInfo : InfoPopupFrameBase
  {
    private SimpleTextHandler desc;
    private OperationBar operationBar;
    private SimpleTextHandler specificDesc;

    public OperationWorkloadInfo(int BuildingUID, TILETYPE tileType, float BaseScale)
      : base(BaseScale)
    {
      Vector2 zero = Vector2.Zero;
      Vector2 buffer = this.buffer;
      string TextToWrite = "Keeping your building well utilized, but not overloaded, will help maximize your output.";
      float width_ = this.scaleHelper.ScaleX(200f);
      this.desc = new SimpleTextHandler(TextToWrite, width_, true, BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location.Y = buffer.Y;
      this.desc.Location.Y += this.desc.GetHeightOfOneLine() * 0.5f;
      buffer.Y += this.desc.GetHeightOfParagraph();
      buffer.X += width_;
      buffer.Y += this.buffer.Y;
      float randomFloat = MathStuff.getRandomFloat(0.0f, 1f);
      this.operationBar = new OperationBar(BaseScale, SmallTextBelow: true);
      this.operationBar.SetValueAndColour(randomFloat);
      this.operationBar.location.Y = buffer.Y;
      this.operationBar.location.X -= this.operationBar.GetSize().X * 0.5f;
      buffer.Y += this.operationBar.GetSize().Y;
      buffer.Y += this.buffer.Y;
      this.specificDesc = new SimpleTextHandler((double) randomFloat <= 1.0 ? ((double) randomFloat <= 0.75 ? ((double) randomFloat <= 0.5 ? string.Format("Your factory is underused, running at {0}%", (object) randomFloat) : string.Format("Your factory is not running very efficiently at {0}%", (object) randomFloat)) : string.Format("Your factory is running rather efficiently at {0}%.", (object) randomFloat)) : string.Format("Your building is receiving {0}% more items than it can process. Build more of this building to help process some of its load.", (object) (float) ((double) randomFloat - 1.0)), width_, true, BaseScale, AutoComplete: true);
      this.specificDesc.SetAllColours(ColourData.Z_Cream);
      this.specificDesc.Location.Y = buffer.Y;
      this.specificDesc.Location.Y += this.specificDesc.GetHeightOfOneLine() * 0.5f;
      buffer.Y += this.specificDesc.GetHeightOfParagraph();
      this.customerFrame.Resize(buffer + this.buffer);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.desc.Location.Y += vector2.Y;
      this.operationBar.location.Y += vector2.Y;
      this.specificDesc.Location.Y += vector2.Y;
    }

    public override void DrawInfoPopupFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      base.DrawInfoPopupFrame(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.specificDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.operationBar.DrawOperationBar(offset, spriteBatch);
    }
  }
}

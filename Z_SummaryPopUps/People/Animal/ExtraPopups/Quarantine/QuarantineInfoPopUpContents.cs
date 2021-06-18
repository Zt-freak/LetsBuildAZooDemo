// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.ExtraPopups.Quarantine.QuarantineInfoPopUpContents
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.ExtraPopups.Quarantine
{
  internal class QuarantineInfoPopUpContents
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText summary;
    private SimpleTextHandler desc;

    public QuarantineInfoPopUpContents(Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string TextToWrite;
      string _textToWrite;
      if (!player.Stats.research.BuildingsResearched.Contains(TILETYPE.QuarantineOffice))
      {
        TextToWrite = "You have not researched the Quarantine Office. Continue researching to discover this building.";
        _textToWrite = "Building Not Researched";
      }
      else if (player.animalquarantine.GetNumberOfBuildings() == 0)
      {
        TextToWrite = "You do not have any Quarantine Offices. Build one to be able to place animals in quarantine.";
        _textToWrite = "No Buildings Available";
      }
      else
      {
        _textToWrite = "Insufficient Space";
        TextToWrite = "You do not have any space left in your quarantine buildings. Build a new one, or free up some space in an existing one.";
      }
      this.summary = new ZGenericText(_textToWrite, BaseScale, _UseOnePointFiveFont: true);
      this.desc = new SimpleTextHandler(TextToWrite, uiScaleHelper.ScaleX(250f), true, BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      zero.Y += defaultBuffer.Y;
      this.summary.vLocation.Y = zero.Y;
      this.summary.vLocation.Y += this.summary.GetSize().Y * 0.5f;
      zero.Y += this.summary.GetSize().Y + defaultBuffer.Y;
      this.desc.Location.Y = zero.Y;
      this.desc.Location.Y += this.desc.GetHeightOfOneLine() * 0.5f;
      zero += this.desc.GetSize();
      zero.X += defaultBuffer.X * 2f;
      zero.Y += defaultBuffer.Y;
      this.customerFrame = new CustomerFrame(zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.SetAlertRed();
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.summary.vLocation.Y += vector2.Y;
      this.desc.Location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateQuarantineInfoPopUpContents(Player player, float DeltaTime, Vector2 offset) => offset += this.location;

    public void DrawQuarantineInfoPopUpContents(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.summary.DrawZGenericText(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.GeneralEmployees.NotHere.LocationSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Employees.GeneralEmployees.NotHere
{
  internal class LocationSummary
  {
    private CustomerFrame customerFrame;
    public Vector2 Location;
    public int TotalShops;
    private TextButton Track;
    private SimpleTextHandler simpletext;
    private MiniHeading locationsheading;

    public LocationSummary(
      EmployeeType employeetype,
      Player player,
      float Width,
      float BaseScale,
      int TotalShops)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.locationsheading = new MiniHeading(Vector2.Zero, "Locations", 1f, BaseScale);
      string TextToWrite = "You currently have " + (object) TotalShops + " locations for this type of employee.";
      Vector2 size = this.locationsheading.GetSize();
      size.Y += defaultBuffer.Y * 2f;
      if (TotalShops > 0)
        TextToWrite += "~Track these locations with waypoints?";
      float width_ = uiScaleHelper.ScaleX(Width) - defaultBuffer.X;
      this.simpletext = new SimpleTextHandler(TextToWrite, width_, true, BaseScale);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.Y = size.Y;
      this.simpletext.Location.Y += this.simpletext.GetHeightOfOneLine() * 0.5f;
      size.Y += this.simpletext.GetHeightOfParagraph();
      size.Y += defaultBuffer.Y;
      if (TotalShops > 0)
      {
        this.Track = new TextButton(BaseScale, nameof (Track));
        this.Track.vLocation.Y = size.Y;
        this.Track.vLocation.Y += this.Track.GetSize_True().Y * 0.5f;
        size.Y += this.Track.GetSize_True().Y;
        size.Y += defaultBuffer.Y;
      }
      size.X = uiScaleHelper.ScaleX(Width);
      this.customerFrame = new CustomerFrame(size, BaseScale: BaseScale);
      this.locationsheading.SetTextPosition(size);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.simpletext.Location.Y += vector2.Y;
      if (this.Track == null)
        return;
      this.Track.vLocation.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateLocationSummary(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      if (this.Track == null)
        return;
      this.Track.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawLocationSummary(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      this.locationsheading.DrawMiniHeading(Offset, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      if (this.Track == null)
        return;
      this.Track.DrawTextButton(Offset, 1f, spritebatch);
    }
  }
}

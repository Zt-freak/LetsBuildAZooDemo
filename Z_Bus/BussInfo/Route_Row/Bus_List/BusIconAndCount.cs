// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.Bus_List.BusIconAndCount
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row.Bus_List
{
  internal class BusIconAndCount
  {
    private MicroBusIcon busicon;
    private CustomerFrame surround;
    public Vector2 Location;
    private GameObject TextObject;
    private string DrawThisText;

    public BusIconAndCount(BUSTYPE bustype, float BaseScale, int Total)
    {
      this.busicon = new MicroBusIcon(bustype, BaseScale);
      this.surround = new CustomerFrame(new Vector2(BaseScale * 34f, BaseScale * 24f * Sengine.ScreenRatioUpwardsMultiplier.Y), true, BaseScale * 2f, true);
      this.busicon.vLocation.X = -6f * BaseScale;
      this.TextObject = new GameObject();
      this.TextObject.SetAllColours(ColourData.Z_Cream);
      this.TextObject.scale = BaseScale;
      this.TextObject.vLocation.X = 5f * BaseScale;
      this.DrawThisText = "x" + (object) Total;
    }

    public void SetZeroRed() => this.surround.SetDullAlertRed();

    public Vector2 GetSize() => this.surround.VSCale;

    public void DrawBusIconAndCount(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.surround.DrawCustomerFrame(Offset, spritebatch);
      this.busicon.DrawMicroBusIcon(spritebatch, Offset);
      TextFunctions.DrawTextWithDropShadow(this.DrawThisText, this.TextObject.scale, this.TextObject.vLocation + Offset, this.TextObject.GetColour(), this.TextObject.fAlpha, AssetContainer.springFont, spritebatch, false);
    }
  }
}

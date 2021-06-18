// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.Crop_Manager.SeedDescription
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_Farms.CropSum.SeedPicker;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Farms.CropSum.Crop_Manager
{
  internal class SeedDescription
  {
    public Vector2 Location;
    private CustomerFrame customerframe;
    private GameObject TextHeader;
    private string NameHead;
    private SeedIcon seedicon;
    private CROPTYPE croptype;
    private TextButton Plant;
    private float Delay;

    public SeedDescription(Vector2 Size, float BaseScale, CROPTYPE _croptype, Player player)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.croptype = _croptype;
      this.customerframe = new CustomerFrame(Size, BaseScale: BaseScale);
      this.Delay = 0.1f;
      this.seedicon = new SeedIcon(this.croptype, BaseScale);
      this.seedicon.scale = BaseScale * 2f;
      this.seedicon.vLocation = Size * -0.5f;
      SeedIcon seedicon = this.seedicon;
      seedicon.vLocation = seedicon.vLocation + this.seedicon.GetSize() * 0.5f;
      this.seedicon.vLocation.X += defaultBuffer.X;
      this.seedicon.vLocation.Y += defaultBuffer.Y;
      this.NameHead = CropData.GetCropTypeToString(this.croptype);
      this.TextHeader = new GameObject();
      this.TextHeader.SetAllColours(ColourData.Z_Cream);
      this.TextHeader.scale = BaseScale;
      this.TextHeader.vLocation = this.seedicon.vLocation;
      this.TextHeader.vLocation.X += this.seedicon.GetSize().X * 0.5f;
      this.TextHeader.vLocation.X += defaultBuffer.X;
      this.TextHeader.vLocation.Y -= defaultBuffer.Y;
      this.Plant = new TextButton(BaseScale, "Plant this crop", 120f * BaseScale);
      this.Plant.vLocation.Y = uiScaleHelper.ScaleY(35f);
    }

    public bool UpdteSeedDescription(Vector2 Offset, Player player, float DeltaTime)
    {
      if ((double) this.Delay > 0.0)
        this.Delay -= DeltaTime;
      Offset += this.Location;
      return this.croptype != CROPTYPE.None && this.Plant.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawSeedDescription(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.seedicon.DrawSeedIcon(spritebatch, Offset);
      TextFunctions.DrawTextWithDropShadow(this.NameHead, this.TextHeader.scale, this.TextHeader.vLocation + Offset, this.TextHeader.GetColour(), this.TextHeader.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false);
      if (this.croptype != CROPTYPE.None && (double) this.Delay <= 0.0)
        this.Plant.DrawTextButton(Offset, 1f, spritebatch);
      TextFunctions.DrawJustifiedText("Field Capacity: " + (object) 300 + " Plants", this.TextHeader.scale, Offset + new Vector2(0.0f, this.TextHeader.scale * 0.0f), this.TextHeader.GetColour(), this.TextHeader.fAlpha, AssetContainer.springFont, spritebatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.Crop_Manager.CurrentCropInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_Farms.CropSum.SeedPicker;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Farms.CropSum.Crop_Manager
{
  internal class CurrentCropInfo
  {
    private CustomerFrame customerFrame;
    private ZGenericText name;
    private SeedIcon seedIcon;
    private SimpleTextHandler desc;
    private TextButton clearFieldButton;

    public CurrentCropInfo(CropStatus cropstatus, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      CROPTYPE cropgrowinghere = cropstatus.cropgrowinghere;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Currently Growing");
      this.name = new ZGenericText(CropData.GetCropTypeToString(cropgrowinghere), BaseScale, false, _UseOnePointFiveFont: true);
      this.seedIcon = new SeedIcon(cropgrowinghere, BaseScale);
      this.desc = new SimpleTextHandler(cropstatus.GetPlantedFieldDescription(), uiScaleHelper.ScaleX(300f), _Scale: BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.clearFieldButton = new TextButton(BaseScale, "Clear Field", 65f);
      this.clearFieldButton.SetButtonColour(BTNColour.Red);
      Vector2 _vScale = defaultBuffer;
      _vScale.Y += this.customerFrame.GetMiniHeadingHeight();
      this.seedIcon.vLocation = _vScale;
      SeedIcon seedIcon1 = this.seedIcon;
      seedIcon1.vLocation = seedIcon1.vLocation + this.seedIcon.GetSize() * 0.5f;
      this.name.vLocation.X = this.seedIcon.vLocation.X + this.seedIcon.GetSize().X * 0.5f + defaultBuffer.X;
      this.name.vLocation.Y = this.seedIcon.vLocation.Y - this.name.GetSize().Y * 0.5f;
      _vScale.Y += this.seedIcon.GetSize().Y;
      _vScale.Y += defaultBuffer.Y;
      this.desc.Location = _vScale;
      _vScale.Y += this.desc.GetHeightOfParagraph();
      _vScale.Y += defaultBuffer.Y;
      this.clearFieldButton.vLocation.Y = _vScale.Y;
      this.clearFieldButton.vLocation.Y += this.clearFieldButton.GetSize_True().Y * 0.5f;
      _vScale.Y += this.clearFieldButton.GetSize_True().Y;
      _vScale.Y += defaultBuffer.Y;
      _vScale.X += this.desc.GetSize().X;
      _vScale.X += defaultBuffer.X;
      this.customerFrame.Resize(_vScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      SeedIcon seedIcon2 = this.seedIcon;
      seedIcon2.vLocation = seedIcon2.vLocation + vector2;
      this.desc.Location += vector2;
      this.clearFieldButton.vLocation.Y += vector2.Y;
      ZGenericText name = this.name;
      name.vLocation = name.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateCurrentCropInfo(Player player, float DeltaTime, Vector2 offset) => this.clearFieldButton.UpdateTextButton(player, offset, DeltaTime);

    public void DrawCurrentCropInfo(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.seedIcon.DrawSeedIcon(spriteBatch, offset);
      this.name.DrawZGenericText(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.clearFieldButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}

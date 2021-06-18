// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.CropManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_Farms.CropSum.Crop_Manager;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Farms.CropSum
{
  internal class CropManager
  {
    public Vector2 location;
    private BigBrownPanel brownpanel;
    private CurrentCropInfo cropInfo;
    private int refFieldUID;

    public CropManager(CropStatus cropstatus, int fieldUID, float BaseScale)
    {
      this.refFieldUID = fieldUID;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.brownpanel = new BigBrownPanel(Vector2.Zero, true, "Agriculture", BaseScale);
      Vector2 zero = Vector2.Zero;
      this.cropInfo = new CurrentCropInfo(cropstatus, BaseScale);
      this.brownpanel.Finalize(zero + this.cropInfo.GetSize());
    }

    public bool CheckMouseOver(Player player) => this.brownpanel.CheckMouseOver(player, this.location);

    public bool UpdateCropManager(Player player, float DeltaTime)
    {
      this.brownpanel.UpdateDragger(player, ref this.location, DeltaTime);
      if (!this.cropInfo.UpdateCurrentCropInfo(player, DeltaTime, this.location))
        return this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, this.location);
      player.farms.ClearFarmOfCrop(this.refFieldUID, player);
      return true;
    }

    public void DrawCropManager(SpriteBatch spriteBatch)
    {
      this.brownpanel.DrawBigBrownPanel(this.location, spriteBatch);
      this.cropInfo.DrawCurrentCropInfo(this.location, spriteBatch);
    }
  }
}

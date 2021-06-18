// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.CropSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_Farms.CropSum.SeedPicker;

namespace TinyZoo.Z_Farms.CropSum
{
  internal class CropSummary
  {
    private CropStatus cropstatus;
    private SeedPickManager seedpackmanager;
    private CropManager cropmanager;

    public CropSummary(Player player, int FarmFieldUID)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.cropstatus = player.farms.GetThisFarmFieldByUID(FarmFieldUID).cropsatus;
      if (this.cropstatus.cropgrowinghere == CROPTYPE.None)
      {
        this.seedpackmanager = new SeedPickManager(baseScaleForUi, player);
      }
      else
      {
        this.cropmanager = new CropManager(this.cropstatus, FarmFieldUID, baseScaleForUi);
        this.cropmanager.location = new Vector2(512f, 384f);
      }
    }

    public bool CheckMouseOver(Player player)
    {
      bool flag1 = false;
      if (this.seedpackmanager != null)
      {
        bool flag2 = flag1 | this.seedpackmanager.CheckMouseOver(player);
        Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag2;
      }
      else if (this.cropmanager != null)
      {
        bool flag3 = flag1 | this.cropmanager.CheckMouseOver(player);
      }
      return false;
    }

    public bool UpdateCropSummary(Player player, float DeltaTime) => this.seedpackmanager != null && this.seedpackmanager.UpdateSeedPickManager(player, DeltaTime) || this.cropmanager != null && this.cropmanager.UpdateCropManager(player, DeltaTime);

    public void DrawCropSummary(SpriteBatch spritebatch)
    {
      if (this.seedpackmanager != null)
        this.seedpackmanager.DrawSeedPickManager();
      if (this.cropmanager == null)
        return;
      this.cropmanager.DrawCropManager(spritebatch);
    }
  }
}

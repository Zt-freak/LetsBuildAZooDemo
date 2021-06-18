// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropSum.Crop_Manager.SeedDetailFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Farms.CropSum.Crop_Manager
{
  internal class SeedDetailFrame
  {
    public Vector2 location;
    private SeedDescription seeddesc_leftColumn;
    private CropSummary cropsummaryRightColumn;
    public CROPTYPE croptype;

    public SeedDetailFrame(
      Player player,
      bool IsBaseTypeUnlocked,
      CROPTYPE croptyp,
      float BaseScale,
      Vector2 ParentFrameSize)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.croptype = croptyp;
      Vector2 Size = ParentFrameSize;
      Size.X *= 0.5f;
      Size.Y -= uiScaleHelper.ScaleY(10f);
      Size.X -= uiScaleHelper.ScaleX(7.5f);
      this.seeddesc_leftColumn = new SeedDescription(Size, BaseScale, croptyp, player);
      this.seeddesc_leftColumn.Location.X = Size.X * -0.5f;
      this.cropsummaryRightColumn = new CropSummary(BaseScale, croptyp, player);
      this.cropsummaryRightColumn.Location.X = Size.X;
      this.cropsummaryRightColumn.Location.Y = Size.Y * -0.5f;
      this.cropsummaryRightColumn.Location.X *= 0.5f;
    }

    public bool UpdateSeedDetailFrame(Vector2 Offset, Player player, float DeltaTime) => this.seeddesc_leftColumn.UpdteSeedDescription(Offset, player, DeltaTime);

    public void DrawSeedDetailFrame(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.seeddesc_leftColumn.DrawSeedDescription(Offset, spritebatch);
      this.cropsummaryRightColumn.DrawCropSUmmary(Offset, spritebatch);
    }
  }
}

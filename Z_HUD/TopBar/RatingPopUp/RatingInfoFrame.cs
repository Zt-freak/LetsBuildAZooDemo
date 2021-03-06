// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.RatingPopUp.RatingInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;

namespace TinyZoo.Z_HUD.TopBar.RatingPopUp
{
  internal class RatingInfoFrame : GenericTopBarPopOutFrame
  {
    private RatingCategoryRows rows;

    public RatingInfoFrame(Player player, float BaseScale)
      : base(BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float x = defaultXbuffer;
      float y = defaultXbuffer;
      this.rows = new RatingCategoryRows(player, BaseScale);
      this.rows.location = new Vector2(x, y);
      this.rows.location.X += this.rows.GetSize().X * 0.5f;
      float num1 = x + this.rows.GetSize().X;
      float num2 = y + this.rows.GetSize().Y;
      this.FinalizeSize(new Vector2(num1 + defaultXbuffer, num2 + defaultYbuffer));
      this.rows.location += this.GetFrameOffset();
    }

    public void RefreshValues(Player player) => this.rows.RefreshValues(player);

    public void UpdateRatingInfoFrame(Player player, float DeltaTime, Vector2 offset)
    {
      this.UpdatePopOutFrame(player, DeltaTime, ref offset);
      if (!this.IsOffScreen() && this.NeedToRefreshValues)
      {
        this.rows.RefreshValues(player);
        this.NeedToRefreshValues = false;
      }
      this.rows.UpdateRatingCategoryRows(player, offset);
    }

    public void DrawRatingInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      this.rows.DrawRatingCategoryRows(offset, spriteBatch);
    }
  }
}

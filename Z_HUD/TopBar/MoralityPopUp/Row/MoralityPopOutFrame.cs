// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row.MoralityPopOutFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks;
using TinyZoo.Z_Morality;
using TinyZoo.Z_MoralitySummary;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row
{
  internal class MoralityPopOutFrame : GenericTopBarPopOutFrame
  {
    private MoralitySummaryRows rows;
    private MoralitySummaryCategory categoryPopOut;
    private MoralityUnlockPopOut unlocksPopOut;
    private new float BaseScale;
    private bool DrawPanelOnTop;

    public MoralityPopOutFrame(Player player, float _BaseScale)
      : base(_BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      float y1 = uiScaleHelper.DefaultBuffer.Y;
      this.rows = new MoralitySummaryRows(player, this.BaseScale);
      this.rows.location.Y = y1;
      float y2 = y1 + this.rows.GetSize().Y + uiScaleHelper.DefaultBuffer.Y;
      this.FinalizeSize(new Vector2(this.rows.GetSize().X + uiScaleHelper.DefaultBuffer.X * 2f, y2));
      this.rows.location.Y += this.GetFrameOffset().Y;
    }

    public override bool CheckMouseOver(Player player, Vector2 offset) => this.categoryPopOut != null && this.categoryPopOut.CheckMouseOver(player, offset + this.location) || this.unlocksPopOut != null && this.unlocksPopOut.CheckMouseOver(player, offset + this.location) || base.CheckMouseOver(player, offset);

    public override bool LerpOff()
    {
      if (this.unlocksPopOut != null)
        this.unlocksPopOut.LerpOff();
      if (this.categoryPopOut != null)
        this.categoryPopOut.LerpOff_DownUp();
      return base.LerpOff();
    }

    public void UpdatePopOutFrame(Player player, float DeltaTime, Vector2 offset)
    {
      this.UpdatePopOutFrame(player, DeltaTime, ref offset);
      MoralityCategory categoryClicked;
      bool ClickedOnUnlocks;
      if (this.IsDoneLerping() && (!this.DrawPanelOnTop || this.categoryPopOut == null && this.unlocksPopOut == null) && this.rows.UpdateMoralitySummaryRows(player, DeltaTime, offset, out categoryClicked, out ClickedOnUnlocks))
      {
        if (categoryClicked != MoralityCategory.Count)
          this.OnClickCategory(player, categoryClicked, offset);
        else if (ClickedOnUnlocks)
          this.OnClickUnlocks(player, offset);
      }
      if (!this.IsOffScreen() && this.NeedToRefreshValues)
      {
        this.rows.RefreshValues(player);
        if (this.unlocksPopOut != null)
          this.unlocksPopOut.RefreshValues(player);
        this.NeedToRefreshValues = false;
      }
      else if (this.IsOffScreen())
      {
        this.categoryPopOut = (MoralitySummaryCategory) null;
        this.unlocksPopOut = (MoralityUnlockPopOut) null;
      }
      if (this.categoryPopOut != null)
        this.categoryPopOut.UpdateMoralitySummaryCategory(DeltaTime);
      if (this.unlocksPopOut == null)
        return;
      if (this.unlocksPopOut.UpdateMoralityUnlockPopOut(player, DeltaTime, offset))
        this.unlocksPopOut.LerpOff();
      if (!this.unlocksPopOut.IsOffScreen())
        return;
      this.unlocksPopOut = (MoralityUnlockPopOut) null;
    }

    private void OnClickCategory(Player player, MoralityCategory categoryClicked, Vector2 offset)
    {
      if (this.categoryPopOut != null && this.categoryPopOut.category == categoryClicked)
        return;
      this.categoryPopOut = new MoralitySummaryCategory(player, categoryClicked, this.BaseScale, true);
      this.categoryPopOut.LerpIn_TopDown();
      this.categoryPopOut.location += this.categoryPopOut.GetSize() * 0.5f;
      this.categoryPopOut.location.X += this.GetSize().X * 0.5f;
      this.categoryPopOut.location.Y -= this.GetSize().Y * 0.5f;
      this.DrawPanelOnTop = false;
      this.unlocksPopOut = (MoralityUnlockPopOut) null;
    }

    private void OnClickUnlocks(Player player, Vector2 offset)
    {
      if (this.unlocksPopOut == null)
      {
        this.unlocksPopOut = new MoralityUnlockPopOut(player, this.BaseScale);
        Vector2 size = this.unlocksPopOut.GetSize();
        if ((double) size.X + (double) offset.X + (double) this.GetSize().X * 0.5 > 1024.0)
        {
          this.unlocksPopOut.AddPreviousButton();
          this.unlocksPopOut.location.Y += size.Y * 0.5f;
          this.unlocksPopOut.location.Y -= this.GetSize().Y * 0.5f;
          this.DrawPanelOnTop = true;
        }
        else
        {
          this.DrawPanelOnTop = false;
          MoralityUnlockPopOut unlocksPopOut = this.unlocksPopOut;
          unlocksPopOut.location = unlocksPopOut.location + size * 0.5f;
          this.unlocksPopOut.location.X += this.GetSize().X * 0.5f;
          this.unlocksPopOut.location.Y -= this.GetSize().Y * 0.5f;
        }
      }
      this.categoryPopOut = (MoralitySummaryCategory) null;
    }

    public void DrawPopOutFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      Vector2 vector2 = offset;
      if (this.IsOffScreen())
        return;
      if (!this.DrawPanelOnTop)
      {
        if (this.categoryPopOut != null)
          this.categoryPopOut.DrawMoralitySummaryCategory(offset + this.location, spriteBatch);
        if (this.unlocksPopOut != null)
          this.unlocksPopOut.DrawMoralityUnlockPopOut(offset + this.location, spriteBatch);
      }
      this.DrawPopOutFrame(ref offset, spriteBatch);
      this.rows.DrawMoralitySummaryRows(offset, spriteBatch);
      if (!this.DrawPanelOnTop)
        return;
      if (this.categoryPopOut != null)
        this.categoryPopOut.DrawMoralitySummaryCategory(vector2 + this.location, spriteBatch);
      if (this.unlocksPopOut == null)
        return;
      this.unlocksPopOut.DrawMoralityUnlockPopOut(vector2 + this.location, spriteBatch);
    }
  }
}

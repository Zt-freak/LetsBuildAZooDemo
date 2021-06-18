// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.LittleSummaryInfoCloseToggle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_GenericUI
{
  internal class LittleSummaryInfoCloseToggle
  {
    public Vector2 location;
    private LittleSummaryButton infoButton;
    private LittleSummaryButton closeButton;
    public bool isShowingCloseButton;

    public LittleSummaryInfoCloseToggle(float BaseScale)
    {
      this.infoButton = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: BaseScale);
      this.closeButton = new LittleSummaryButton(LittleSummaryButtonType.RedCloseCircle, _BaseScale: BaseScale);
    }

    public Vector2 GetSize() => this.infoButton.GetSize();

    public bool UpdateLittleSummaryInfoCloseToggle(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.isShowingCloseButton)
      {
        if (this.closeButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
        {
          this.isShowingCloseButton = false;
          return true;
        }
      }
      else if (this.infoButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
      {
        this.isShowingCloseButton = true;
        return true;
      }
      return false;
    }

    public void DrawLittleSummaryInfoCloseToggle(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.isShowingCloseButton)
        this.closeButton.DrawLittleSummaryButton(offset, spriteBatch);
      else
        this.infoButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}

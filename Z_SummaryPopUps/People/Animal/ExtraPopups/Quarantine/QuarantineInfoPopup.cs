// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.ExtraPopups.Quarantine.QuarantineInfoPopup
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.ExtraPopups.Quarantine
{
  internal class QuarantineInfoPopup
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private QuarantineInfoPopUpContents contents;

    public QuarantineInfoPopup(Player player, float BaseScale)
    {
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Quarantine", BaseScale);
      this.contents = new QuarantineInfoPopUpContents(player, BaseScale);
      this.bigBrownPanel.Finalize(this.contents.GetSize());
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateQuarantinePopup(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawQuarantineInfoPopup(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.contents.DrawQuarantineInfoPopUpContents(offset, spriteBatch);
    }
  }
}

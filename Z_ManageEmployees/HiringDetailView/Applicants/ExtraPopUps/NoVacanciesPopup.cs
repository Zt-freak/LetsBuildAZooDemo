// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ExtraPopUps.NoVacanciesPopup
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ExtraPopUps
{
  internal class NoVacanciesPopup
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private NoVacanciesFrame frame;

    public NoVacanciesPopup(float BaseScale)
    {
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "No Vacancies", BaseScale);
      this.frame = new NoVacanciesFrame(BaseScale);
      this.bigBrownPanel.Finalize(this.frame.GetSize());
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateNoVacanciesPopup(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      Vector2 localposition = offset;
      this.bigBrownPanel.UpdateDragger(player, ref localposition, DeltaTime);
      this.location = localposition - offset + this.location;
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawNoVacanciesPopup(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawNoVacanciesFrame(offset, spriteBatch);
    }
  }
}

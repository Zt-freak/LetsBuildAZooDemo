// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.ReportActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class ReportActionPopUp : CustomerActionPopUp
  {
    private ConfirmationDialog confirmationDialog;

    public ReportActionPopUp(float BaseScale)
      : base(BaseScale)
    {
      this.confirmationDialog = new ConfirmationDialog("", "Report this illegal animal trader to the police?", BaseScale, IncludeBrownPanel: false);
    }

    public override Vector2 GetSize() => this.confirmationDialog.GetSizeOfContentsFrame();

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      bool confirmed;
      if (!this.confirmationDialog.UpdateConfirmationDialog(player, offset, DeltaTime, out confirmed))
        return false;
      int num = confirmed ? 1 : 0;
      return true;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset) => this.confirmationDialog.DrawConfirmationDialog(spritebatch, offset);
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.AccountsButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_Manage.Accounts
{
  internal class AccountsButtons
  {
    private TextButton[] buttons;

    public AccountsButtons()
    {
      this.buttons = new TextButton[2];
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        string TextToDraw = "";
        switch ((AccountButtonType) index)
        {
          case AccountButtonType.AdjustTicketPrice:
            TextToDraw = "Set Ticket Price";
            break;
          case AccountButtonType.ViewGraphs:
            TextToDraw = "Show Performance";
            break;
        }
        this.buttons[index] = new TextButton(TextToDraw, 150f);
        this.buttons[index].vLocation = new Vector2(512f, (float) (150 + 100 * index));
      }
    }

    public AccountButtonType UpdateAccountsButtons(
      Player player,
      float DeltaTime,
      Vector2 Offset)
    {
      AccountButtonType accountButtonType = AccountButtonType.Count;
      for (int index = 0; index < this.buttons.Length; ++index)
      {
        if (this.buttons[index].UpdateTextButton(player, Vector2.Zero, DeltaTime))
          accountButtonType = (AccountButtonType) index;
      }
      return accountButtonType;
    }

    public void DrwAccountsButtons(Vector2 Offset)
    {
      for (int index = 0; index < this.buttons.Length; ++index)
        this.buttons[index].DrawTextButton(Offset);
    }
  }
}

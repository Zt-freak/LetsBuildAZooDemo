// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.AccountsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.Accounts.GraphView;
using TinyZoo.Z_TicketPrice;

namespace TinyZoo.Z_Manage.Accounts
{
  internal class AccountsManager
  {
    private SetTicketPriceManager setticketprice;
    private GraphManager graphmanager;
    private AccountsButtons accountbuuton;
    private AccountButtonType thisscreenstate;
    private ScreenHeading screenheading;

    public AccountsManager(Player player)
    {
      this.screenheading = new ScreenHeading("FINANCIAL ACCOUNTS", 90f);
      if (false)
      {
        this.SwitchBackToMain();
      }
      else
      {
        this.thisscreenstate = AccountButtonType.ViewGraphs;
        this.graphmanager = new GraphManager(player, false);
      }
    }

    public bool BlockExit()
    {
      if (true || this.thisscreenstate == AccountButtonType.MainMenu)
        return false;
      if (this.thisscreenstate == AccountButtonType.AdjustTicketPrice)
      {
        this.setticketprice.ForceExit();
        return true;
      }
      this.graphmanager.ForceExit();
      return true;
    }

    private void SwitchBackToMain()
    {
      this.thisscreenstate = AccountButtonType.MainMenu;
      this.accountbuuton = new AccountsButtons();
    }

    public void UpdateAccountsManager(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      out bool FlippedToNewState)
    {
      FlippedToNewState = false;
      if (this.thisscreenstate == AccountButtonType.MainMenu)
      {
        AccountButtonType accountButtonType = this.accountbuuton.UpdateAccountsButtons(player, DeltaTime, Offset);
        if (accountButtonType != AccountButtonType.Count)
        {
          FlippedToNewState = true;
          if (accountButtonType == AccountButtonType.AdjustTicketPrice)
          {
            this.setticketprice = new SetTicketPriceManager(player);
            this.thisscreenstate = AccountButtonType.AdjustTicketPrice;
          }
          else if (accountButtonType == AccountButtonType.ViewGraphs)
          {
            this.graphmanager = new GraphManager(player, false);
            this.thisscreenstate = AccountButtonType.ViewGraphs;
          }
        }
      }
      if (this.thisscreenstate == AccountButtonType.AdjustTicketPrice && this.setticketprice.UpdateSetTicketPriceManager(player, DeltaTime, Offset))
      {
        this.setticketprice = (SetTicketPriceManager) null;
        this.SwitchBackToMain();
      }
      if (this.thisscreenstate != AccountButtonType.ViewGraphs || !this.graphmanager.UpdateGraphManager(player, DeltaTime, Offset))
        return;
      this.graphmanager = (GraphManager) null;
      this.SwitchBackToMain();
    }

    public void DrawAccountsManager(Vector2 Offset)
    {
      if (this.screenheading != null)
        this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      if (this.thisscreenstate == AccountButtonType.MainMenu)
        this.accountbuuton.DrwAccountsButtons(Offset);
      if (this.thisscreenstate == AccountButtonType.AdjustTicketPrice)
        this.setticketprice.DrawSetTicketPriceManager(Offset);
      if (this.thisscreenstate != AccountButtonType.ViewGraphs)
        return;
      this.graphmanager.DrawGraphManager(Offset);
    }
  }
}

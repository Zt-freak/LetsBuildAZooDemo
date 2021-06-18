// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.IAPWaitManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpringIAP;
using SpringIAP.Purchase;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Utils;

namespace TinyZoo.IAPScreen
{
  internal class IAPWaitManager
  {
    public bool IsActive;
    private bool Waiting;
    private float Time;
    public bool isDone;
    public bool startRestore;
    private SimpleTextHandler text;
    private BlackOut blackout;
    private string purchaseIdentifier;

    public IAPWaitManager(string IdentifierToBuy, Player player)
    {
      this.IsActive = true;
      this.blackout = new BlackOut();
      this.blackout.fAlpha = 0.8f;
      this.purchaseIdentifier = IdentifierToBuy;
      if (IdentifierToBuy == IAPScreenManager.RESTORE_IDENTIFIER)
        SpringIAPManager.Instance.TryRestorePurchases(player.iapuser);
      else
        IAPHolder.TryToPurchase(SpringIAPManager.Instance, player, IdentifierToBuy);
      this.Waiting = true;
      this.isDone = false;
      this.startRestore = false;
      this.SetNewText("Please Wait...");
    }

    private void SetNewText(string texttowrite)
    {
      this.text = new SimpleTextHandler(texttowrite, true, 0.8f);
      this.text.AutoCompleteParagraph();
      this.text.Location = new Vector2(512f, 200f);
    }

    public void UpdateIAPWaitManager(float DeltaTime, Player player)
    {
      if (!this.IsActive)
        return;
      if (this.Waiting)
      {
        List<string> restoredPurchases = new List<string>();
        if (!(!(this.purchaseIdentifier == IAPScreenManager.RESTORE_IDENTIFIER) ? SpringIAPManager.Instance.GetisStillWaiting() : SpringIAPManager.Instance.IsStillTryingToRestorePurchases(out bool _, ref restoredPurchases)))
        {
          PurchaseState Result = PurchaseState.None;
          SpringIAPManager.Instance.GetisStillWaiting(out Result);
          this.Waiting = false;
          this.Time = 1f;
          switch (Result)
          {
            case PurchaseState.TimeOut:
              this.SetNewText("Time Out");
              break;
            case PurchaseState.PurchaseSuccess:
            case PurchaseState.NoServerConnection_UnvallidatedSuccess:
              if (this.purchaseIdentifier == IAPScreenManager.RESTORE_IDENTIFIER)
              {
                string texttowrite;
                if (restoredPurchases.Count == 0)
                {
                  texttowrite = "No purchases to restore!";
                }
                else
                {
                  texttowrite = restoredPurchases.Count != 1 ? "Purchases restored:~" : "Purchase restored:~";
                  foreach (string identifier in restoredPurchases)
                    texttowrite = texttowrite + "~" + IAPHolder.GetIAPDisplayName(identifier);
                }
                this.SetNewText(texttowrite);
                break;
              }
              this.SetNewText("Purchase Success!");
              this.isDone = true;
              break;
            case PurchaseState.RejectedBySpringServer:
              this.SetNewText("Server Error");
              break;
            case PurchaseState.PlayerCancellled:
              this.SetNewText("Purchase Cancelled!");
              break;
            case PurchaseState.AlreadyOwned:
              this.SetNewText("Purchase already owned... Tap to restore");
              this.startRestore = true;
              break;
            default:
              Console.WriteLine("NO STATE" + (object) Result);
              break;
          }
          IAPHolder.CheckPurchases(SpringIAPManager.Instance, player);
          player.OldSaveThisPlayer();
        }
      }
      else if ((double) this.Time > 0.0)
        this.Time -= DeltaTime;
      else
        this.IsActive = false;
      player.inputmap.ClearAllInput(player);
    }

    public void DrawIAPWaitManager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      this.blackout.DrawBlackOut(Offset, spriteBatch);
      if (this.text == null)
        return;
      this.text.DrawSimpleTextHandler(Offset, 1f, spriteBatch);
    }
  }
}

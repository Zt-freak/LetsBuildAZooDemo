// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.IAPHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SpringIAP;
using SpringIAP.IAP_User;
using SpringIAP.IAPData;
using SpringIAP.Purchase;
using System;
using System.Collections.Generic;

namespace TinyZoo.Utils
{
  internal class IAPHolder
  {
    public IAPHolder(SpringIAPManager springIAPmanager)
    {
      List<IAPIndentifier> _Identifiers = new List<IAPIndentifier>();
      for (int index = 0; index < 0; ++index)
        _Identifiers.Add(new IAPIndentifier(IAPHolder.GetIAPIDentifier((IAPTYPE) index), PurchaseType.NonConsumableOneTime));
      string _packageID = "WINDOWS_PACKAGE_ID";
      string _appID = "WINDOWS_APP_ID";
      springIAPmanager.SetUpIdentifiers(_Identifiers, _packageID, _appID);
    }

    internal static string GetIAPIDentifier(IAPTYPE iap)
    {
      switch (iap)
      {
        case IAPTYPE.DisableAdverts:
          return "prisonplanet.windows.disableadverts";
        case IAPTYPE.BuyVortex:
          return "prisonplanet.windows.vortex";
        case IAPTYPE.BuyFlower:
          return "prisonplanet.windows.flower";
        default:
          return "NON";
      }
    }

    internal static string GetIAPDisplayName(string identifier)
    {
      if (identifier == "com.springloaded.prisonplanet.google.removeads")
        return "Space Goat Pack";
      if (identifier == "com.springloaded.prisonplanet.google.vortex")
        return "Vortex Mind + Trash Compactor";
      if (identifier == "com.springloaded.prisonplanet.google.flower")
        return "Suppressia";
      int num = identifier.LastIndexOf('.');
      return num < 0 ? identifier : identifier.Substring(num + 1);
    }

    internal static string GetIAPDisplayName(IAPTYPE iapType) => IAPHolder.GetIAPDisplayName(IAPHolder.GetIAPIDentifier(iapType));

    internal static bool TryToPurchase(
      SpringIAPManager iapmanager,
      Player player,
      string Identifier)
    {
      bool wasAllowedToStart = false;
      if (iapmanager.CanMakePurchase(PurchaseType.Consumable))
        iapmanager.StartPurchase(Identifier, PurchaseType.Consumable, out wasAllowedToStart, player.iapuser);
      return wasAllowedToStart;
    }

    internal static void CheckPurchases(SpringIAPManager iapmanager, Player player)
    {
      List<PurchaseEntry> readyToConsumeByGame = iapmanager.GetPurchasesThatAreReadyToConsume_ByGame(player.iapuser);
      if (readyToConsumeByGame.Count <= 0)
        return;
      for (int index1 = 0; index1 < readyToConsumeByGame.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < 0; ++index2)
        {
          if (IAPHolder.GetIAPIDentifier((IAPTYPE) index2) == readyToConsumeByGame[index1].GetIdentifier())
          {
            flag = true;
            if (iapmanager.GiveStuffToPlayer_localConsume(readyToConsumeByGame[index1], player.iapuser))
            {
              player.livestats.Gv((IAPTYPE) index2, player);
              player.OldSaveThisPlayer();
            }
            player.OldSaveThisPlayer();
          }
        }
        int num = flag ? 1 : 0;
      }
      Console.WriteLine("Consume done!");
    }
  }
}

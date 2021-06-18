// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Customers.UseShop
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments;

namespace TinyZoo.Z_BalanceSystems.Customers
{
  internal class UseShop
  {
    public static void PurchasedSomething(
      ShopStockStatus shopstatucpurchased,
      MemberOfThePublic memberofthepublic,
      ShopEntry shopentry)
    {
      PersonAttachementType personAttachementType = PersonAttachmentManager.GetFOODTYPEToPersonAttachementType(shopstatucpurchased.REF_shopentry.MainItemForSale);
      if (personAttachementType != PersonAttachementType.Count)
        memberofthepublic.UnnequipedAttachments.Add(personAttachementType);
      NeedFulFillmentEntry needFulfillment = ShopData.GetNeedFulfillment(shopstatucpurchased.REF_shopentry.MainItemForSale, shopstatucpurchased);
      for (int index = 0; index < needFulfillment.SatisfactionModifiers.Length; ++index)
      {
        if ((double) needFulfillment.SatisfactionModifiers[index] != 0.0)
        {
          if (index == 13)
            memberofthepublic.customerneeds.CurrentWantValues[index] += needFulfillment.SatisfactionModifiers[index] * memberofthepublic.customerneeds.Multipliers[index];
          else
            memberofthepublic.customerneeds.CurrentWantValues[index] += needFulfillment.SatisfactionModifiers[index];
          memberofthepublic.customerneeds.CurrentWantValues[index] = MathHelper.Clamp(memberofthepublic.customerneeds.CurrentWantValues[index], 0.0f, 1f);
        }
      }
      if ((double) shopentry.SeasoningValue <= 0.0)
        return;
      switch (ShopData.GetShopInfo(shopentry.tiletype).Seasoning.MinFood_LeftFood)
      {
        case FOODTYPE.ChilliPowder:
          memberofthepublic.ChilliDose += shopentry.SeasoningValue;
          memberofthepublic.customerneeds.CurrentWantValues[10] += shopentry.SeasoningValue;
          memberofthepublic.customerneeds.CurrentWantValues[13] += shopentry.SeasoningValue * 0.25f;
          break;
        case FOODTYPE.CornSyrup:
          memberofthepublic.CornSyrupDose += shopentry.SeasoningValue;
          memberofthepublic.customerneeds.CurrentWantValues[0] = MathHelper.Clamp(memberofthepublic.customerneeds.CurrentWantValues[0] + shopentry.SeasoningValue, 0.0f, 1f);
          memberofthepublic.customerneeds.CurrentWantValues[1] = Math.Max(memberofthepublic.customerneeds.CurrentWantValues[1] - shopentry.SeasoningValue * 0.4f, 0.0f);
          memberofthepublic.customerneeds.CurrentWantValues[13] += shopentry.SeasoningValue * 0.55f * memberofthepublic.customerneeds.Multipliers[13];
          break;
        case FOODTYPE.Salt:
          memberofthepublic.SaltDose += shopentry.SeasoningValue;
          memberofthepublic.customerneeds.CurrentWantValues[2] += shopentry.SeasoningValue;
          if ((double) shopentry.SeasoningValue > 0.200000002980232)
          {
            memberofthepublic.customerneeds.CurrentWantValues[13] += (shopentry.SeasoningValue - 0.2f) * memberofthepublic.customerneeds.Multipliers[13];
            break;
          }
          break;
        case FOODTYPE.Sugar:
          memberofthepublic.SugarDose += shopentry.SeasoningValue;
          memberofthepublic.customerneeds.CurrentWantValues[0] = MathHelper.Clamp(memberofthepublic.customerneeds.CurrentWantValues[0] + shopentry.SeasoningValue, 0.0f, 1f);
          memberofthepublic.customerneeds.CurrentWantValues[13] += shopentry.SeasoningValue * 0.5f * memberofthepublic.customerneeds.Multipliers[13];
          break;
        case FOODTYPE.Caffeine:
          memberofthepublic.CaffieneDose += shopentry.SeasoningValue;
          memberofthepublic.customerneeds.CurrentWantValues[13] += shopentry.SeasoningValue * 0.2f * memberofthepublic.customerneeds.Multipliers[13] * memberofthepublic.customerneeds.Multipliers[13];
          memberofthepublic.customerneeds.CurrentWantValues[17] += shopentry.SeasoningValue * 0.35f;
          memberofthepublic.customerneeds.CurrentWantValues[0] = MathHelper.Clamp(memberofthepublic.customerneeds.CurrentWantValues[0] + shopentry.SeasoningValue, 0.0f, 1.5f);
          break;
      }
      if ((double) memberofthepublic.customerneeds.CurrentWantValues[17] <= 0.0 || (double) memberofthepublic.customerneeds.CurrentWantValues[0] >= 1.0)
        return;
      float num = 1f - memberofthepublic.customerneeds.CurrentWantValues[0];
      if ((double) num >= (double) memberofthepublic.customerneeds.CurrentWantValues[17])
      {
        memberofthepublic.customerneeds.CurrentWantValues[0] += memberofthepublic.customerneeds.CurrentWantValues[17];
        memberofthepublic.customerneeds.CurrentWantValues[17] = 0.0f;
      }
      else
      {
        memberofthepublic.customerneeds.CurrentWantValues[0] = 1f;
        memberofthepublic.customerneeds.CurrentWantValues[17] -= num;
      }
      if ((double) memberofthepublic.customerneeds.CurrentWantValues[17] > 0.0)
        return;
      memberofthepublic.customerneeds.CurrentWantValues[17] = 0.0001f;
    }
  }
}

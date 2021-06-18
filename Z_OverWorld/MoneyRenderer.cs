// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.MoneyRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_OverWorld.MoneyRendering;

namespace TinyZoo.Z_OverWorld
{
  internal class MoneyRenderer
  {
    private static List<BankNote> banknotes;

    public MoneyRenderer() => MoneyRenderer.banknotes = new List<BankNote>();

    internal static void SomethingIsTooExpensive(Vector2 Location)
    {
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
      {
        if (!MoneyRenderer.banknotes[index].bActive)
        {
          MoneyRenderer.banknotes[index].Spawnicon(NoteType.TooExpensive, "$!", Location);
          return;
        }
      }
      MoneyRenderer.banknotes.Add(new BankNote());
      MoneyRenderer.banknotes[MoneyRenderer.banknotes.Count - 1].Spawnicon(NoteType.TooExpensive, "", Location);
    }

    internal static string FormatCentsToDollars(int Money)
    {
      string str1 = "";
      string str2 = string.Concat((object) Money);
      if (str2.Length == 2)
        str1 = "0." + (object) Money;
      else if (str2.Length == 1)
      {
        str1 = "0.0" + (object) Money;
      }
      else
      {
        for (int index = 0; index < str2.Length; ++index)
        {
          if (index == str2.Length - 2)
            str1 += ".";
          str1 += str2[index].ToString();
        }
      }
      return str1;
    }

    internal static void DropCarbon(Vector2 Location, int Carbon)
    {
      IconPopType icontype = IconPopType.AddCO2;
      if (Carbon < 0)
      {
        Carbon *= -1;
        icontype = IconPopType.ReduceCO2;
      }
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
      {
        if (!MoneyRenderer.banknotes[index].bActive)
        {
          MoneyRenderer.banknotes[index].SpawnPopIconMoment(string.Concat((object) Carbon), Location, icontype);
          return;
        }
      }
      MoneyRenderer.banknotes.Add(new BankNote());
      MoneyRenderer.banknotes[MoneyRenderer.banknotes.Count - 1].SpawnPopIconMoment(string.Concat((object) Carbon), Location, icontype);
    }

    internal static void EarnMoney(Vector2 Location, int Money, bool IncludeCents)
    {
      string.Concat((object) Money);
      string str1;
      if (IncludeCents)
      {
        str1 = MoneyRenderer.FormatCentsToDollars(Money);
      }
      else
      {
        string str2 = string.Concat((object) Money);
        str1 = "";
        if (str2.Length > 2)
        {
          for (int index = 0; index < str2.Length - 2; ++index)
            str1 += str2[index].ToString();
        }
        else
          str1 = "0";
      }
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
      {
        if (!MoneyRenderer.banknotes[index].bActive)
        {
          MoneyRenderer.banknotes[index].SpawnBankNote((NoteType) Game1.Rnd.Next(0, 3), Money, "$" + str1, Location);
          return;
        }
      }
      MoneyRenderer.banknotes.Add(new BankNote());
      MoneyRenderer.banknotes[MoneyRenderer.banknotes.Count - 1].SpawnBankNote((NoteType) Game1.Rnd.Next(0, 3), Money, "$" + str1, Location);
    }

    internal static void DoDamage(Vector2 Location, IconPopType icontype, int Damage)
    {
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
      {
        if (!MoneyRenderer.banknotes[index].bActive)
        {
          MoneyRenderer.banknotes[index].SpawnPopIconMoment(string.Concat((object) Damage), Location, icontype);
          return;
        }
      }
      MoneyRenderer.banknotes.Add(new BankNote());
      MoneyRenderer.banknotes[MoneyRenderer.banknotes.Count - 1].SpawnPopIconMoment(string.Concat((object) Damage), Location, icontype);
    }

    internal static void PopIcon(Vector2 Location, IconPopType icontype)
    {
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
      {
        if (!MoneyRenderer.banknotes[index].bActive)
        {
          MoneyRenderer.banknotes[index].SpawnPopIconMoment(" ", Location, icontype);
          return;
        }
      }
      MoneyRenderer.banknotes.Add(new BankNote());
      MoneyRenderer.banknotes[MoneyRenderer.banknotes.Count - 1].SpawnPopIconMoment(" ", Location, icontype);
    }

    internal static void FeedAnimal(Vector2 Location)
    {
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
      {
        if (!MoneyRenderer.banknotes[index].bActive)
        {
          MoneyRenderer.banknotes[index].SpawnFoodMoment(" ", Location);
          return;
        }
      }
      MoneyRenderer.banknotes.Add(new BankNote());
      MoneyRenderer.banknotes[MoneyRenderer.banknotes.Count - 1].SpawnFoodMoment(" ", Location);
    }

    public void UpdateMoneyRenderer(float DeltaTime, Player player)
    {
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
        MoneyRenderer.banknotes[index].UpdateBankNote(DeltaTime, player);
    }

    public void DrawMoneyRenderer()
    {
      if (TrailerDemoFlags.HasTrailerFlag)
        return;
      for (int index = 0; index < MoneyRenderer.banknotes.Count; ++index)
        MoneyRenderer.banknotes[index].DrawBankNote();
    }
  }
}

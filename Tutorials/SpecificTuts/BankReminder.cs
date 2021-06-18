// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.BankReminder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class BankReminder
  {
    private SmartCharacterBox charactertextbox;
    private BlackOut blacout;

    public BankReminder(ref Arrow arrow, ref Vector2 ArrowLocation, Player player)
    {
      this.blacout = new BlackOut();
      this.blacout.SetAllColours(ColourData.IconYellow);
      this.blacout.SetAlpha(false, 0.4f, 0.0f, 1f);
      arrow = new Arrow();
      FeatureFlags.BlockCash = false;
      arrow = new Arrow(true);
      arrow.Rotation = -1.570796f;
      ArrowLocation = new Vector2(800f, 40f);
      FeatureFlags.BlockTimer = true;
      if (!player.Stats.research.BuildingsResearched.Contains(TILETYPE.Bank))
        throw new Exception("NOT POSSIBLE WITH CURRENT DESIGN");
      this.charactertextbox = player.livestats.GetCost(TILETYPE.Bank, player, true) <= player.Stats.GetCashHeld() ? new SmartCharacterBox("Your cash is at maximum, build a vault to store more!", AnimalType.Administrator) : new SmartCharacterBox(SEngine.Localization.Localization.GetText(0), AnimalType.Administrator);
      this.charactertextbox.SetNewHeight(300f);
    }

    public bool UpdateBankReminder(ref float DeltaTime, Player player)
    {
      this.blacout.UpdateColours(DeltaTime);
      if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
      {
        player.inputmap.ClearAllInput(player);
        FeatureFlags.BlockTimer = false;
        return true;
      }
      player.inputmap.ClearAllInput(player);
      DeltaTime = 0.0f;
      return false;
    }

    public void DrawBankReminder() => this.charactertextbox.DrawSmartCharacterBox();
  }
}

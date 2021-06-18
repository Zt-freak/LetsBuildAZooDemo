// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Store_Local.EntryDetail.EntryDetailPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.OverWorld.Store_Local.Entries;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.Store_Local.EntryDetail
{
  internal class EntryDetailPanel
  {
    public Vector2 Location;
    private GenericBox box;
    private SimpleTextHandler simpletext;
    private TextButton Button;
    private StoreIcon storeicon;
    private StringInBox InUse;
    private StringInBox Available;
    private StoreEntryType storeEntryType;
    private int Cost;
    private bool CanAfford;
    private bool AtMax;
    private DollarPanel dollarpanel;

    public EntryDetailPanel(StoreEntryType thisentry, Player player)
    {
      this.storeEntryType = thisentry;
      this.box = new GenericBox(new Vector2(450f, 400f));
      this.box.SetPurpleWithYellowFrame();
      this.storeicon = new StoreIcon(thisentry, 3f);
      this.storeicon.vLocation = new Vector2(-170f, -120f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      float _Scale = 2f;
      if (GameFlags.MobileUIScale)
        _Scale = 3f;
      this.simpletext = new SimpleTextHandler(this.GetText(thisentry, player), false, 0.4f, _Scale, false, false);
      this.simpletext.paragraph.linemaker.SetAllColours(1f, 1f, 1f);
      this.simpletext.Location = new Vector2(-200f, -50f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.CreateBuyButton(player);
      this.CreateButtons(player);
    }

    private void CreateBuyButton(Player player)
    {
      this.Cost = player.inventory.GetCost(this.storeEntryType, player);
      string str = "";
      int num = GameFlags.IsConsoleVersion ? 1 : 0;
      this.Button = new TextButton(str + SEngine.Localization.Localization.GetText(9), 50f);
      this.Button.AddControllerButton(ControllerButton.XboxA);
      this.AtMax = false;
      this.Button.SetButtonGreen();
      this.CanAfford = true;
      if (this.Cost > player.Stats.GetCashHeld())
      {
        this.CanAfford = false;
        this.Button.SetButtonRed();
      }
      if (this.storeEntryType == StoreEntryType.BeamSpeed && player.inventory.BeamSpdUpgrades.GetUnvallidatedValue() >= 50)
      {
        this.AtMax = true;
        this.Button = new TextButton(SEngine.Localization.Localization.GetText(63), 60f);
        this.Button.SetLemonANdBlue();
      }
      this.Button.vLocation.Y = 120f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.UltraWideSreenDownardsMultiplier;
      this.Button.vLocation.X = 90f;
      this.dollarpanel = new DollarPanel(player);
      this.dollarpanel.ForceCost(this.Cost);
      this.dollarpanel.SetBuy();
    }

    private void CreateButtons(Player player)
    {
      float _BaseScale = 3f;
      switch (this.storeEntryType)
      {
        case StoreEntryType.BeamSpeed:
          this.Available = new StringInBox("Power: " + (object) player.inventory.BeamSpdUpgrades.GetUnvallidatedValue(), _BaseScale, 80f);
          break;
        case StoreEntryType.BeamL2:
          this.Available = new StringInBox("Power: " + (object) player.inventory.RightBeamUpgrades.GetUnvallidatedValue(), _BaseScale, 80f);
          break;
        case StoreEntryType.InstaBeam:
          this.Available = new StringInBox("Available: " + (object) player.inventory.InstantBeamsRemaining, 2f, 80f);
          this.InUse = new StringInBox("In Use: " + (object) player.inventory.InstantBeamsBeamsInMaps, _BaseScale, 80f);
          break;
      }
      this.Available.SetGreen();
      this.Available.vLocation = new Vector2(-120f, -140f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      if (this.InUse == null)
        return;
      this.InUse.SetRed();
      this.InUse.vLocation = new Vector2(this.Available.vLocation.X, this.Available.vLocation.Y + 40f * Sengine.ScreenRatioUpwardsMultiplier.Y);
    }

    private string GetText(StoreEntryType thisentry, Player player)
    {
      string str = "";
      switch (thisentry)
      {
        case StoreEntryType.BasicBeam:
          str = "Deploy these during lockdown to constrain prisoners.";
          break;
        case StoreEntryType.BeamSpeed:
          str = "Increase laser speed.";
          break;
        case StoreEntryType.BeamL2:
          str = "Increase Right/Down beam speed";
          break;
        case StoreEntryType.InstaBeam:
          str = "These Deluxe Field Generators will instantly create a wall when deployed.";
          break;
        case StoreEntryType.BeamSpeedL2:
          str = "Placing a drop zone in an empty prison reserves a small area for connecting field generators to. You can only deploy a small number of these in a prison block.";
          break;
      }
      return str;
    }

    public void UpdateEntryDetailPanel(float DeltaTime, Player player, bool CanPress)
    {
      if (CanPress)
        this.simpletext.UpdateSimpleTextHandler(DeltaTime);
      if (this.Cost > player.Stats.GetCashHeld())
      {
        if (this.CanAfford)
        {
          this.CanAfford = false;
          this.Button.SetButtonRed();
        }
      }
      else if (!this.CanAfford)
      {
        this.Button.SetButtonGreen();
        this.CanAfford = true;
      }
      if (this.dollarpanel != null)
        this.dollarpanel.UpdateDollarPanel(DeltaTime, player);
      if ((this.Button.UpdateTextButton(player, this.Location, DeltaTime) || player.inputmap.PressedThisFrame[0]) && (!this.AtMax && player.Stats.SpendCash(player.inventory.GetCost(this.storeEntryType, player), SpendingCashOnThis.PPWeaponUpgrade, player)))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade);
        this.Button.DoWhiteFlash();
        player.inventory.PurchasedThis(this.storeEntryType);
        this.CreateButtons(player);
        this.Available.SetPrimaryColours(1f, Vector3.One);
        this.CreateBuyButton(player);
        player.OldSaveThisPlayer();
      }
      this.Available.UpdateColours(DeltaTime);
    }

    public void DrawEntryDetailPanel(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      this.box.DrawGenericBox(Offset + this.Location, DrawWithThis);
      this.Button.DrawTextButton(Offset + this.Location, 1f, DrawWithThis);
      this.simpletext.DrawSimpleTextHandler(Offset + this.Location, 1f, DrawWithThis);
      Vector2 zero = Vector2.Zero;
      zero.Y = 100f * Sengine.UltraWideSreenUpwardsMultiplier;
      zero.Y -= 100f;
      this.storeicon.DrawStoreIcon(Offset + this.Location + zero, DrawWithThis);
      this.Available.DrawStringInBox(Offset + this.Location + zero, DrawWithThis);
      if (this.InUse != null)
        this.InUse.DrawStringInBox(Offset + this.Location + zero, DrawWithThis);
      if (this.dollarpanel == null || this.AtMax)
        return;
      this.dollarpanel.DrawDollarPanel(Offset + this.Location + this.Button.vLocation - new Vector2(155f, 0.0f), false);
    }
  }
}

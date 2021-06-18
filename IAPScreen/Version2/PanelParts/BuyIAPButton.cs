// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.PanelParts.BuyIAPButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SpringIAP;
using TinyZoo.GenericUI;
using TinyZoo.Utils;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.IAPScreen.Version2.PanelParts
{
  internal class BuyIAPButton
  {
    private WatchVideoButton BuyIt;
    private bool AlreadyOwned;
    public Vector2 ExtraOffset;
    private IAPIConType iapicontype;
    private TRC_ButtonDisplay controllerButton;

    public BuyIAPButton(IAPIConType iapicon, Player player)
    {
      this.controllerButton = new TRC_ButtonDisplay(2f);
      this.controllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxA);
      this.iapicontype = iapicon;
      IAPTYPE iap = IAPTYPE.DisableAdverts;
      if (iapicon == IAPIConType.VortexMind)
        iap = IAPTYPE.BuyVortex;
      if (iapicon == IAPIConType.FlowerSuppressia)
        iap = IAPTYPE.BuyFlower;
      string costString = SpringIAPManager.Instance.GetCostString(AssetContainer.springFont, IAPHolder.GetIAPIDentifier(iap));
      this.BuyIt = new WatchVideoButton(SEngine.Localization.Localization.GetText(9) + " " + costString);
      if (iapicon == IAPIConType.VortexMind)
      {
        if (player.Stats.Vortex())
        {
          this.AlreadyOwned = true;
          this.BuyIt = new WatchVideoButton("Owned");
          this.controllerButton = (TRC_ButtonDisplay) null;
        }
      }
      else if (iapicon == IAPIConType.SpeedUpTime)
      {
        if (player.Stats.ADisabled(true, player))
        {
          this.AlreadyOwned = true;
          this.BuyIt = new WatchVideoButton("Owned");
        }
      }
      else if (iapicon == IAPIConType.FlowerSuppressia && player.Stats.GetFlower())
      {
        this.AlreadyOwned = true;
        this.BuyIt = new WatchVideoButton("Owned");
      }
      this.BuyIt.SetAsGreenButton();
      this.BuyIt.scale *= Sengine.UltraWideSreenDownardsMultiplier;
      this.BuyIt.TextScale *= Sengine.UltraWideSreenDownardsMultiplier;
      if (this.AlreadyOwned)
        return;
      this.BuyIt.AddControllerButton(ControllerButton.XboxA, true);
    }

    private void MakeAlreadyOwnedButton()
    {
      this.AlreadyOwned = true;
      this.BuyIt = new WatchVideoButton("Owned");
      this.BuyIt.SetAsGreenButton();
      this.BuyIt.scale *= Sengine.UltraWideSreenDownardsMultiplier;
      this.BuyIt.TextScale *= Sengine.UltraWideSreenDownardsMultiplier;
    }

    public bool UpdateBuyIAPButton(Player player, Vector2 Offset)
    {
      Offset.Y -= 50f;
      if (this.AlreadyOwned)
        return false;
      if (this.iapicontype == IAPIConType.VortexMind)
      {
        if (player.Stats.Vortex())
        {
          this.MakeAlreadyOwnedButton();
          this.controllerButton = (TRC_ButtonDisplay) null;
        }
      }
      else if (this.iapicontype == IAPIConType.SpeedUpTime)
      {
        if (player.Stats.ADisabled(true, player))
        {
          this.MakeAlreadyOwnedButton();
          this.controllerButton = (TRC_ButtonDisplay) null;
        }
      }
      else if (this.iapicontype == IAPIConType.FlowerSuppressia && player.Stats.GetFlower())
      {
        this.MakeAlreadyOwnedButton();
        this.controllerButton = (TRC_ButtonDisplay) null;
      }
      return this.controllerButton != null && player.inputmap.PressedThisFrame[14] || this.BuyIt.UpdateWatchVideoButton(player, Offset + this.ExtraOffset, true);
    }

    public void DrawBuyIAPButton(Vector2 Offset)
    {
      Offset.Y -= 50f;
      this.BuyIt.DrawWatchVideoButton(Offset + this.ExtraOffset, TinyZoo.GameFlags.IsUsingController && this.controllerButton != null);
      if (!TinyZoo.GameFlags.IsUsingController)
        return;
      TRC_ButtonDisplay controllerButton = this.controllerButton;
    }
  }
}

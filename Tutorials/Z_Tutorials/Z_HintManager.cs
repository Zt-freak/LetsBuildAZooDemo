// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.Z_HintManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Notification;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class Z_HintManager
  {
    private SmartCharacterBox charactertextbox;
    private int StateCounter;
    private Vector2 ArrowLocation;
    private Arrow arrow;

    public Z_HintManager(Player player)
    {
      this.charactertextbox = new SmartCharacterBox("You will get alerted when things need addressing in your zoo, try viewing an alert now!", AnimalType.Administrator, true, Sengine.UltraWideSreenDownardsMultiplier);
      Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.C_Population_BuyThing_GenericStore), player);
      this.arrow = new Arrow();
      this.ArrowLocation = new Vector2(50f, 100f);
      this.arrow.InvertPointer();
      FeatureFlags.BlockTicketPrice = true;
      FeatureFlags.BlockIntake = true;
      FeatureFlags.BlockManage = true;
      FeatureFlags.BlockSettings = true;
      FeatureFlags.BlockBreeding = true;
      FeatureFlags.BlockBuild = true;
    }

    public bool UpdateZ_Hints(ref float SimulationTime, ref float DeltaTime, Player player)
    {
      SimulationTime = 0.0f;
      if (this.StateCounter == 0)
      {
        FeatureFlags.BlockPersonInfo = true;
        this.arrow.UpdateArrow(DeltaTime);
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, DoNotClearInput: true);
        if (!OverWorldManager.zoopopupHolder.IsNull())
        {
          FeatureFlags.BlockBuild = false;
          if (OverWorldManager.zoopopupHolder.GetHasState(POPUPSTATE.Notification))
          {
            FeatureFlags.BlockPersonInfo = true;
            FeatureFlags.BlockCloseBuildMenu = true;
            FeatureFlags.BlockCloseNotifcation = true;
            this.StateCounter = 1;
            this.arrow = (Arrow) null;
          }
        }
      }
      if (this.StateCounter == 1)
      {
        if (OverWorldManager.overworldstate == OverWOrldState.Build)
        {
          this.StateCounter = 2;
          this.charactertextbox = (SmartCharacterBox) null;
        }
      }
      else if (this.StateCounter == 2 && player.shopstatus.HasShops())
      {
        FeatureFlags.BlockPersonInfo = false;
        FeatureFlags.BlockCloseBuildMenu = false;
        FeatureFlags.BlockCloseNotifcation = false;
        FeatureFlags.BlockIntake = false;
        FeatureFlags.BlockSettings = false;
        FeatureFlags.BlockTicketPrice = false;
        FeatureFlags.BlockTicketPrice = true;
        FeatureFlags.BlockSettings = false;
        FeatureFlags.BlockBreeding = true;
        FeatureFlags.BlockBuild = false;
        FeatureFlags.BlockPersonInfo = false;
        return true;
      }
      return false;
    }

    public void DrawZ_Hints()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null)
        return;
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}

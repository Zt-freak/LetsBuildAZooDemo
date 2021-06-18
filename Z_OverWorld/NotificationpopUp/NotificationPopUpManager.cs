// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.NotificationpopUp.NotificationPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_HUD.ControlHint;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_OverWorld.NotificationpopUp
{
  internal class NotificationPopUpManager
  {
    private SimpleTextBox simpletextbox;
    private BackButton closebutton;
    private TextButton textbutton;
    private BlackOut blackout;
    private float BoxY;
    private bool Exiting;
    private bool GoingToShortCut;
    private ForceToNewScreen willgohere;
    private int AnimalUIToLookAt;
    private Z_NotificationType notificationtype;

    public NotificationPopUpManager(
      NotificationPackage notification,
      out bool RemoveThisNotification)
    {
      RemoveThisNotification = false;
      this.notificationtype = notification.notificationtype;
      this.BoxY = 300f;
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.2f, 0.0f, 0.7f);
      this.closebutton = new BackButton(true);
      string TEXTTT = "";
      this.willgohere = ForceToNewScreen.None;
      switch (notification.notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          TEXTTT = "A new " + (object) notification.Animal + " has been born!";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.LookAtAnimal;
          this.AnimalUIToLookAt = notification.AnimalOrPenUID;
          break;
        case Z_NotificationType.A_AnimalBirthInBreedingRoom:
          TEXTTT = "A new " + (object) notification.Animal + " has been born as part of your breeding program!";
          RemoveThisNotification = true;
          break;
        case Z_NotificationType.A_AnimalTransferedFromBreedingRoom:
          TEXTTT = "A " + (object) notification.Animal + " born from the breeding program has been transfered to an public enclosure.";
          RemoveThisNotification = true;
          break;
        case Z_NotificationType.A_AnimalHunger:
          TEXTTT = "An animal has not had food for " + (object) notification.DaysWithoutFood + " days: " + (object) notification.Animal + ".";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.LookAtAnimal;
          this.AnimalUIToLookAt = notification.AnimalOrPenUID;
          break;
        case Z_NotificationType.A_AnimalDeath:
          string str = "";
          switch (notification.CauseOfDeath)
          {
            case CauseOfDeath.Hunger:
              str = "Hunger";
              break;
            case CauseOfDeath.OldAge:
              str = "Old Age";
              break;
            case CauseOfDeath.Sickness:
              str = "Disease";
              break;
          }
          TEXTTT = "A " + (object) notification.Animal + " has died of " + str + ".";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.LookAtAnimal;
          this.AnimalUIToLookAt = notification.AnimalOrPenUID;
          break;
        case Z_NotificationType.CannotReachGate:
          TEXTTT = "A zoo keeper cannot reach an enclosure gate to feed an animal.";
          RemoveThisNotification = true;
          break;
        case Z_NotificationType.C_Population_BuyThing_GenericStore:
          TEXTTT = "Your visitors could be spending money.~Build a shop or restaurant!";
          this.textbutton = new TextButton("View");
          this.willgohere = ForceToNewScreen.BuildShop;
          break;
        case Z_NotificationType.Dep_Population_OpenTheZoo:
          TEXTTT = "Open your zoo to begin a new day and start earning money!";
          break;
        case Z_NotificationType.F_BuildArchitect:
          TEXTTT = "Build a Research Hub to start researching new buildings.";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.BuildArchtect;
          break;
        case Z_NotificationType.A_AnimalSick:
          TEXTTT = "A sick " + (object) notification.Animal + " has been dignosed!";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.LookAtAnimal;
          this.AnimalUIToLookAt = notification.AnimalOrPenUID;
          break;
        case Z_NotificationType.A_CuredAnimal:
          TEXTTT = "A sick " + (object) notification.Animal + " has been cured!";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.LookAtAnimal;
          this.AnimalUIToLookAt = notification.AnimalOrPenUID;
          break;
        case Z_NotificationType.F_ResearchComplete:
          TEXTTT = "Research is complete! You can now build: " + TileData.GetTileStats(notification.tiletype).Name + ". Don't forget to start new research.";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.ResearchView;
          break;
        case Z_NotificationType.F_TicketPrice:
          TEXTTT = "Your ticket price is too low! To change it click on the ticket office.";
          if (notification.TipType == ZTipType.TicketPriceTooExpensive)
            TEXTTT = "Your ticket price is too High! To change it click on the ticket office.";
          this.textbutton = new TextButton("View");
          RemoveThisNotification = true;
          this.willgohere = ForceToNewScreen.TicketPrice;
          break;
      }
      this.simpletextbox = new SimpleTextBox(TEXTTT, 700f, textScale: 2f);
      if (this.textbutton == null)
        return;
      this.textbutton.vLocation = new Vector2(512f, (float) ((double) this.simpletextbox.GetScale().Y * 0.5 + 40.0));
    }

    public bool UpdateNotificationPopUpManager(float DeltaTime, Player player)
    {
      this.blackout.UpdateColours(DeltaTime);
      this.simpletextbox.UpdateSimpleTextBox(DeltaTime, player);
      if (this.Exiting)
        return this.simpletextbox.LerpOffComplete();
      if (this.textbutton != null && (this.textbutton.UpdateTextButton(player, new Vector2(0.0f, this.BoxY), DeltaTime) || player.inputmap.PressedThisFrame[0]))
      {
        Z_GameFlags.ForceToNewScreen = this.willgohere;
        this.GoingToShortCut = true;
        if (this.willgohere == ForceToNewScreen.LookAtAnimal)
        {
          Z_GameFlags.LookAtThisAnimal_UID = this.AnimalUIToLookAt;
          FeatureFlags.BlockAllUI = true;
        }
        else if (this.notificationtype == Z_NotificationType.C_Population_BuyThing_GenericStore)
          Z_GameFlags.ForceControllerHintsToThe = ControllerHintSummary.BuildStructure;
        this.Exit();
      }
      if (!FeatureFlags.BlockCloseNotifcation && this.closebutton.UpdateBackButton(player, DeltaTime))
        this.Exit();
      return false;
    }

    private void Exit()
    {
      if (this.Exiting)
        return;
      this.Exiting = true;
      this.simpletextbox.LerpOff();
      this.blackout.SetAlpha(true, 0.2f, 1f, 0.0f);
    }

    public void DrawNotificationPopUpManager()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.simpletextbox.DrawSimpleTextBox(new Vector2(512f, this.BoxY));
      this.closebutton.vLocation = new Vector2((float) (862.0 + (double) this.simpletextbox.lerpontoscreen.Value * 1024.0), this.BoxY - 100f);
      if (this.textbutton != null)
        this.textbutton.DrawTextButton(new Vector2(this.simpletextbox.lerpontoscreen.Value * 1024f, this.BoxY));
      if (FeatureFlags.BlockCloseNotifcation)
        return;
      this.closebutton.DrawBackButton(Vector2.Zero);
    }
  }
}

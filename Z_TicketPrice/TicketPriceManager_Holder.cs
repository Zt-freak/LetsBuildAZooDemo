// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.TicketPriceManager_Holder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_Notification;
using TinyZoo.Z_TicketPrice.Panel;

namespace TinyZoo.Z_TicketPrice
{
  internal class TicketPriceManager_Holder
  {
    private SetTicketPriceManager manager;
    private StoreBGManager storeBGManager;
    private BackButton backbutton;
    private TicketPriceMainPanel ticketPriceMainPanel;

    public TicketPriceManager_Holder(Player player)
    {
      this.ticketPriceMainPanel = new TicketPriceMainPanel(player, Z_GameFlags.GetBaseScaleForUI());
      this.ticketPriceMainPanel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.ticketPriceMainPanel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateTicketPriceManager_Holder(Player player, float DeltaTime, bool UpdateValues = false)
    {
      float SimulationTime = 0.0f;
      GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref SimulationTime, player);
      if (!this.ticketPriceMainPanel.UpdateTicketPriceMainPanel(player, DeltaTime, UpdateValues, Vector2.Zero))
        return false;
      if (this.ticketPriceMainPanel.SomethingChanged)
        Z_NotificationManager.RescrubTickets = true;
      return true;
    }

    public void DrawTicketPriceManager_Holder() => this.ticketPriceMainPanel.DrawTicketPriceMainPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleInParkManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark
{
  internal class PeopleInParkManager
  {
    private PeopleInParkPanel panel;

    public PeopleInParkManager()
    {
      this.panel = new PeopleInParkPanel(Z_GameFlags.GetBaseScaleForUI());
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdatePeopleInParkManager(Player player, float DeltaTime)
    {
      WalkingPerson goToThisPerson;
      int num = this.panel.UpdatePeopleInParkPanel(player, DeltaTime, Vector2.Zero, out goToThisPerson) ? 1 : 0;
      if (goToThisPerson == null)
        return num != 0;
      OverWorldManager.zoopopupHolder.CreateZooPopUps(goToThisPerson, player);
      return num != 0;
    }

    public void DrawPeopleInParkManager() => this.panel.DrawPeopleInParkPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}

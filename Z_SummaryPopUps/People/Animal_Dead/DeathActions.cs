// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal_Dead.DeathActions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_SummaryPopUps.People.Animal_Dead
{
  internal class DeathActions
  {
    private ButtonDethOption DisposeOfThisDead;
    private ButtonDethOption ConvertToSmallCarcass;
    private ButtonDethOption SendToFreezer;
    private ButtonDethOption Leave;
    public Vector2 Location;

    public DeathActions()
    {
      this.DisposeOfThisDead = new ButtonDethOption("Dispose", "Dispose of this animals body in a furnace");
      this.ConvertToSmallCarcass = new ButtonDethOption("Feed", "Convert this animal into a small carcass and add it to your animal feed");
      this.SendToFreezer = new ButtonDethOption("Freeze", "Use your freezer to store this animal for use in manufacturing.");
      this.Leave = new ButtonDethOption(nameof (Leave), "Leave this animal here to decay.");
      float num = 60f;
      this.DisposeOfThisDead.Location.Y = 0.0f;
      this.ConvertToSmallCarcass.Location.Y = num;
      this.SendToFreezer.Location.Y = num * 2f;
      this.Leave.Location.Y = num * 3f;
      this.Location.Y = 180f;
    }

    public bool UpdateDeathActions(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      bool flag = false;
      if (this.DisposeOfThisDead.UpdateButtonDethOption(Offset, player, DeltaTime))
        flag = true;
      if (this.ConvertToSmallCarcass.UpdateButtonDethOption(Offset, player, DeltaTime))
        flag = true;
      if (this.SendToFreezer.UpdateButtonDethOption(Offset, player, DeltaTime))
        flag = true;
      if (this.Leave.UpdateButtonDethOption(Offset, player, DeltaTime))
        flag = true;
      return flag;
    }

    public void DrawDeathActions(Vector2 Offset)
    {
      Offset += this.Location;
      this.DisposeOfThisDead.DrawButtonDethOption(Offset);
      this.ConvertToSmallCarcass.DrawButtonDethOption(Offset);
      this.SendToFreezer.DrawButtonDethOption(Offset);
      this.Leave.DrawButtonDethOption(Offset);
    }
  }
}

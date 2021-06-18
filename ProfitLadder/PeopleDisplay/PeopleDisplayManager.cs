// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.PeopleDisplay.PeopleDisplayManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.ProfitLadder.PeopleDisplay
{
  internal class PeopleDisplayManager
  {
    private PeopleLine InGraves;
    private PeopleLine AliveInCellBlocks;
    private PeopleLine InHoldingCells;
    private PeopleLine Paroled;

    public PeopleDisplayManager(Player player)
    {
      this.AliveInCellBlocks = new PeopleLine(player, 500);
      this.InHoldingCells = new PeopleLine(player, 600, true);
      this.InGraves = new PeopleLine(player, 700, IsGraveyard: true);
    }

    public void UpdatePeopleDisplayManager(Player player, float DeltaTime)
    {
      this.AliveInCellBlocks.UpdatePeopleLine(player.player.touchinput);
      this.InGraves.UpdatePeopleLine(player.player.touchinput);
      this.InHoldingCells.UpdatePeopleLine(player.player.touchinput);
    }

    public void DrawPeopleDisplayManager()
    {
      this.AliveInCellBlocks.DrawPeopleLine();
      this.InGraves.DrawPeopleLine();
      this.InHoldingCells.DrawPeopleLine();
    }
  }
}

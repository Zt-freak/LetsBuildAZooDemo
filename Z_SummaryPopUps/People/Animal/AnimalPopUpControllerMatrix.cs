// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.AnimalPopUpControllerMatrix
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_SummaryPopUps.People.Animal
{
  internal class AnimalPopUpControllerMatrix
  {
    private ButtonRepeater Shoulder_repeater;
    private ButtonRepeater MainRepeater;

    public AnimalPopUpControllerMatrix()
    {
      this.Shoulder_repeater = new ButtonRepeater();
      this.MainRepeater = new ButtonRepeater();
    }

    public void UpdateAnimalPopUpControllerMatrix(
      Player player,
      float DeltaTime,
      TabFrameManager tabframemanager)
    {
      DirectionPressed Direction;
      if (!this.Shoulder_repeater.UpdateMenuRepeats(DeltaTime, out Direction, false, false, player.inputmap.HeldButtons[9], player.inputmap.HeldButtons[10]))
        return;
      tabframemanager.ForceTabSwitch(Direction == DirectionPressed.Left);
    }
  }
}

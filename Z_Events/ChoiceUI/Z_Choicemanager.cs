// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.ChoiceUI.Z_Choicemanager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Events.ChoiceUI
{
  internal class Z_Choicemanager
  {
    private ChoiceEntry Left;
    private ChoiceEntry right;

    public Z_Choicemanager()
    {
      this.Left = new ChoiceEntry("Allow the Police to shoot the snake", "SHOOT!", false, true, AnimalType.PoliceWithGun);
      this.right = new ChoiceEntry("Bribe the Police and save your snake!", "BRIBE!", false, false, AnimalType.Snake, true);
      this.right.Delay = 11f;
      this.Left.Delay = 10.75f;
      this.Left.textbutton.stringinabox.SetAsButtonFrame(BTNColour.Red);
    }

    public void UpdateZ_Choicemanager(float DeltaTime)
    {
      this.Left.UpdateChoiceEntry(DeltaTime);
      this.right.UpdateChoiceEntry(DeltaTime);
    }

    public void DrawZ_Choicemanager()
    {
      this.Left.DrawChoiceEntry();
      this.right.DrawChoiceEntry();
    }
  }
}

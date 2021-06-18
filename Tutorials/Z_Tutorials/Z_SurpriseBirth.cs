// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.Z_SurpriseBirth
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class Z_SurpriseBirth
  {
    private SmartCharacterBox charactertextbox;
    private Vector2 ArrowLocation;
    private int StateCounter;
    private Arrow arrow;

    public Z_SurpriseBirth()
    {
      this.charactertextbox = new SmartCharacterBox("I just heard, your rabbits are multiplying!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbox.AddNewText(new textBoxPair("Keep an eye on the notifications, as new births are shown there.", AnimalType.Administrator));
      this.charactertextbox.AddNewText(new textBoxPair("More importantly, people love baby animals!~Expect more customers when you have babies in the zoo, you can even get away with increasing your ticket price!", AnimalType.Administrator));
    }

    public bool UpdateSurpriseBirth(ref float SimulationTime, ref float DeltaTime, Player player)
    {
      SimulationTime = 0.0f;
      if (this.StateCounter != 0 || !this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
        return false;
      FeatureFlags.BlockTicketPrice = false;
      return true;
    }

    public void DrawSurpriseBirth()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null)
        return;
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.NoBeams
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class NoBeams
  {
    private SmartCharacterBox charactertextbox;
    private BlackOut blackout;
    private float Delay;

    public NoBeams()
    {
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.3f, 0.0f, 0.8f);
      this.charactertextbox = new SmartCharacterBox("You only have a limited number of laser generators, to get more visit the upgrade store from the main screen.", AnimalType.Administrator);
      this.Delay = 2f;
    }

    public bool UpdateNoBeams(float DeltaTime, Player player)
    {
      if ((double) this.Delay > 0.0)
      {
        this.Delay -= DeltaTime;
      }
      else
      {
        this.blackout.UpdateColours(DeltaTime);
        if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
        {
          player.inputmap.ClearAllInput(player);
          return true;
        }
      }
      player.inputmap.ClearAllInput(player);
      return false;
    }

    public void DrawNoBeams()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawSmartCharacterBox();
    }
  }
}

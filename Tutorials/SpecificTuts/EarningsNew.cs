// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.EarningsNew
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class EarningsNew
  {
    private SmartCharacterBox charactertextbox;
    private BlackOut blacout;

    public EarningsNew(long FullMoney, int ACtuallyMoneyearned)
    {
      this.blacout = new BlackOut();
      this.blacout.SetAllColours(ColourData.IconYellow);
      this.blacout.SetAlpha(false, 0.4f, 0.0f, 0.8f);
      FeatureFlags.BlockTimer = true;
      string FirstText = "Our prison earned $" + (object) ACtuallyMoneyearned + " while you were away!";
      if (FullMoney > (long) ACtuallyMoneyearned)
        FirstText = FirstText + "~~We could have earned $" + (object) FullMoney + " if we had more Vaults!";
      this.charactertextbox = new SmartCharacterBox(FirstText, AnimalType.Administrator);
    }

    public bool UpdateEarningsNew(ref float DeltaTime, Player player)
    {
      this.blacout.UpdateColours(DeltaTime);
      if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
      {
        player.inputmap.ClearAllInput(player);
        FeatureFlags.BlockTimer = false;
        return true;
      }
      if (this.charactertextbox.Exiting && (double) this.blacout.fTargetAlpha != 0.0)
        this.blacout.SetAlpha(true, 0.2f, 1f, 0.0f);
      player.inputmap.ClearAllInput(player);
      DeltaTime = 0.0f;
      return false;
    }

    public void DrawEarningsNew()
    {
      this.blacout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawSmartCharacterBox();
    }
  }
}

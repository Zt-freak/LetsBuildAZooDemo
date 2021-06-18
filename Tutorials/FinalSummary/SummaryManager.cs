// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.FinalSummary.SummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;

namespace TinyZoo.Tutorials.FinalSummary
{
  internal class SummaryManager
  {
    private bool ContinueBlocked;
    private int INDEXCntr;

    public SummaryManager(ref SmartCharacterBox charactertextbox)
    {
      FeatureFlags.BlockCash = true;
      FeatureFlags.BlockTimer = true;
      FeatureFlags.BlockBuild = true;
      GameFlags.ForceExitBuildNow = true;
      charactertextbox = new SmartCharacterBox("SUMMARY:~Remember - Get more Prisoners from here", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      charactertextbox.AddNewText(new textBoxPair("SUMMARY:~Prisoners earn money, which you can spend on enlarging your prison.", AnimalType.Administrator));
      charactertextbox.AddNewText(new textBoxPair("SUMMARY:~You can also upgrade your drone and buy more lasers in the shop.", AnimalType.Administrator));
      charactertextbox.AddNewText(new textBoxPair("SUMMARY:~Just try and make the greatest prison you can!", AnimalType.Administrator));
      charactertextbox.AddNewText(new textBoxPair("SUMMARY:~And if you forget anything, go and read the warden's handbook found here.", AnimalType.Administrator));
      this.ContinueBlocked = false;
    }

    public bool UpdateSummaryManager(
      Player player,
      ref float DeltaTime,
      SmartCharacterBox charactertextbox,
      ref Arrow arrow,
      ref Vector2 ArrowLocation)
    {
      if (charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, this.ContinueBlocked))
      {
        player.inputmap.ClearAllInput(player);
        return true;
      }
      if (this.INDEXCntr == 0)
      {
        FeatureFlags.BlockIntake = false;
        arrow = new Arrow();
        ++this.INDEXCntr;
        ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[1].Location.Y);
      }
      else if (this.INDEXCntr == 1)
      {
        if (charactertextbox.ThisLine >= 1)
        {
          ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[3].Location.Y);
          FeatureFlags.BlockBuild = false;
          ++this.INDEXCntr;
        }
      }
      else if (this.INDEXCntr == 2)
      {
        if (charactertextbox.ThisLine >= 2)
        {
          ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[2].Location.Y);
          FeatureFlags.BlockBreeding = false;
          ++this.INDEXCntr;
        }
      }
      else if (this.INDEXCntr == 3 && charactertextbox.ThisLine >= 4)
      {
        ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[0].Location.Y);
        FeatureFlags.BlockSettings = false;
        ++this.INDEXCntr;
      }
      arrow.SetAllColours(ColourData.FernGreen);
      player.inputmap.ClearAllInput(player);
      return false;
    }
  }
}

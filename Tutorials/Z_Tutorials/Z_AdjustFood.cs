// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.Z_AdjustFood
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.MainButtons;
using TinyZoo.Z_ManagePen.Buttons;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class Z_AdjustFood
  {
    private SmartCharacterBox charactertextbox;
    private int StateCOunter;
    private BlackOut blackout;
    private LerpHandler_Float lerper;
    private Z_ManageButton BUTTON;

    public Z_AdjustFood()
    {
      FeatureFlags.AllowPenSelectOnly = true;
      FeatureFlags.DemolishEnabled = true;
      FeatureFlags.BlockIntake = true;
      this.charactertextbox = new SmartCharacterBox("You can adjust everything in your zoo, how about we make sure these rabbits are eating well?", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.StateCOunter = 0;
      this.charactertextbox.AddNewText(new textBoxPair("Click on the enclosure and select Animal Info.", AnimalType.Administrator));
    }

    public bool UpdateZ_AdjustFood(ref float SimulationTime, ref float DeltaTime, Player player)
    {
      if (this.StateCOunter == 0)
      {
        SimulationTime = 0.0f;
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
        if (this.charactertextbox.ThisLine > 0)
          ++this.StateCOunter;
      }
      if (this.StateCOunter == 1)
      {
        if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, DoNotClearInput: true))
        {
          this.charactertextbox = (SmartCharacterBox) null;
          ++this.StateCOunter;
        }
        if (TinyZoo.Game1.gamestate == GAMESTATE.ManagePen)
        {
          this.StateCOunter = 3;
          this.blackout = new BlackOut();
          this.BUTTON = new Z_ManageButton(ManageButtonType.Feed);
          this.BUTTON.Location = new Vector2(512f, 300f);
          this.blackout.SetAlpha(false, 0.4f, 0.0f, 0.6f);
          this.lerper = new LerpHandler_Float();
          this.lerper.SetLerp(true, 1f, 0.0f, 3f);
          this.charactertextbox = new SmartCharacterBox("Use this area to set the diet for your animals.", AnimalType.Administrator, true, Sengine.UltraWideSreenDownardsMultiplier);
          this.charactertextbox.AddNewText(new textBoxPair("Better food makes an animal live longer and feel happier.~Some animals like rabbits are quite hardy and can endure lower quality food for longer with less ill effects.", AnimalType.Administrator, SetToBottom: true));
          this.charactertextbox.AddNewText(new textBoxPair("You can run out of food, so use the store room to order more, or employ a storekeeper.", AnimalType.Administrator, SetToBottom: true));
        }
      }
      if (this.StateCOunter == 3 && this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
      {
        ++this.StateCOunter;
        this.charactertextbox = (SmartCharacterBox) null;
        this.lerper.SetLerp(true, 0.0f, -1f, 3f, true);
        this.blackout.SetAlpha(true, 0.3f, 1f, 0.0f);
      }
      if (this.StateCOunter == 4 && TinyZoo.Game1.gamestate == GAMESTATE.OverWorld)
      {
        FeatureFlags.AllowPenSelectOnly = false;
        FeatureFlags.DemolishEnabled = false;
        return true;
      }
      if (this.lerper != null)
        this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.BUTTON != null)
        this.BUTTON.UpdateManageButtons(player, Vector2.Zero);
      if (this.blackout != null)
        this.blackout.UpdateColours(DeltaTime);
      return false;
    }

    public void DrawZ_FirstNight()
    {
      if (this.blackout != null)
        this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.BUTTON != null)
      {
        this.BUTTON.MouseMover = false;
        this.BUTTON.DrawManageButtons(new Vector2(this.lerper.Value * 1024f, 0.0f), AssetContainer.pointspritebatchTop05);
      }
      if (this.charactertextbox == null)
        return;
      this.charactertextbox.DrawSmartCharacterBox();
    }
  }
}

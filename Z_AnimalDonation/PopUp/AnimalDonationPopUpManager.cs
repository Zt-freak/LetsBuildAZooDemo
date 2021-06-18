// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalDonation.PopUp.AnimalDonationPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection;

namespace TinyZoo.Z_AnimalDonation.PopUp
{
  internal class AnimalDonationPopUpManager
  {
    private Vector2 location;
    private AnimalSelectionManager animalSelectionManager;
    private BigBrownPanel bigBrownPanel;

    public AnimalDonationPopUpManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      Vector2 forcedSize = new UIScaleHelper(baseScaleForUi).ScaleVector2(new Vector2(500f, 400f));
      this.animalSelectionManager = new AnimalSelectionManager(AnimalSelectionUIType.Donation, player, baseScaleForUi, forcedSize, frameCount_X: 8);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Animal Donation", baseScaleForUi);
      this.bigBrownPanel.Finalize(this.animalSelectionManager.GetSize());
      this.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player)
    {
      Vector2 location = this.location;
      return this.bigBrownPanel.CheckMouseOver(player, location);
    }

    public bool UpdateAnimalDonationPopUpManager(Player player, float DeltaTime)
    {
      Vector2 location = this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      List<PrisonerInfo> animalsSelected;
      if (this.animalSelectionManager.UpdateAnimalSelectionManager(player, DeltaTime, location, out animalsSelected))
      {
        this.DonateAnimals(animalsSelected);
        return true;
      }
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location);
    }

    private void DonateAnimals(List<PrisonerInfo> animalsToDonate)
    {
    }

    public void DrawAnimalDonationPopUpManager()
    {
      Vector2 location = this.location;
      this.bigBrownPanel.DrawBigBrownPanel(location, AssetContainer.pointspritebatchTop05);
      this.animalSelectionManager.DrawAnimalSelectionManager(location, AssetContainer.pointspritebatchTop05);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.AnimalDeliveryUI.AnimalDeliveryPanelManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_HUD.AnimalDeliveryUI
{
  internal class AnimalDeliveryPanelManager
  {
    private AnimalDeliveryPanel panel;
    private List<AnimalOrder> REF_animalsonorder_ForThisEnclosure;

    public AnimalDeliveryPanelManager(Player player, PrisonZone prison)
    {
      this.REF_animalsonorder_ForThisEnclosure = new List<AnimalOrder>();
      for (int index = 0; index < player.animalsonorder.animalsonorder.Count; ++index)
      {
        if (player.animalsonorder.animalsonorder[index].PrisonUID == prison.Cell_UID)
          this.REF_animalsonorder_ForThisEnclosure.Add(player.animalsonorder.animalsonorder[index]);
      }
      this.panel = new AnimalDeliveryPanel(Z_GameFlags.GetBaseScaleForUI(), this.REF_animalsonorder_ForThisEnclosure);
      this.panel.location = Sengine.HalfReferenceScreenRes;
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateAnimalDeliveryPanelManager(Player player, float DeltaTime) => (0 | (this.panel.UpdateAnimalDeliveryPanel(player, Vector2.Zero, DeltaTime) ? 1 : 0)) != 0;

    public void DrawAnimalDeliveryPanelManager() => this.panel.DrawAnimalDeliveryPanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}

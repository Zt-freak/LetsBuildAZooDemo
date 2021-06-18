// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer.NewThingPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Shared.Grid;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer
{
  internal class NewThingPopUpManager
  {
    private NewThingPanel newThingPanel;

    public NewThingPopUpManager(AnimalType genomeUnlocked, bool isForGenomeComplete = true)
    {
      this.newThingPanel = new NewThingPanel(genomeUnlocked, isForGenomeComplete);
      this.SetUp();
    }

    public NewThingPopUpManager(List<AnimalRenderDescriptor> animals)
    {
      this.newThingPanel = new NewThingPanel(animals);
      this.SetUp();
    }

    public NewThingPopUpManager(List<PrisonerInfo> prisonerInfos)
    {
      this.newThingPanel = new NewThingPanel(prisonerInfos);
      this.SetUp();
    }

    public NewThingPopUpManager(List<AnimalType> animals)
    {
      this.newThingPanel = new NewThingPanel(animals);
      this.SetUp();
    }

    private void SetUp() => this.newThingPanel.location = new Vector2(512f, 384f);

    public bool CheckMouseOver(Player player) => this.newThingPanel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateNewThingPopUpManager(Player player, float DeltaTime) => this.newThingPanel.UpdateNewThingPanel(player, DeltaTime, Vector2.Zero, true);

    public void DrawNewThingPopUpManager() => this.newThingPanel.DrawNewThingPanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}

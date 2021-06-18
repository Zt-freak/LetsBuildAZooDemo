// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.SelectionGrid.CustomAnimalSelectionGridFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.SelectionGrid
{
  internal class CustomAnimalSelectionGridFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private CustomAnimalSelectionGrid animalSelectionGrid;

    public CustomAnimalSelectionGridFrame(
      List<PrisonerInfo> animals,
      Player player,
      float BaseScale,
      int frameCount_X = 7,
      int frameCount_Y = 5,
      bool AddSellPrice = false)
    {
      List<CustomAnimalSelectionEntry> animals1 = new List<CustomAnimalSelectionEntry>();
      for (int index = 0; index < animals.Count; ++index)
        animals1.Add(new CustomAnimalSelectionEntry(animals[index]));
      this.SetUp(animals1, player, BaseScale, frameCount_X, frameCount_Y, AddSellPrice);
    }

    public CustomAnimalSelectionGridFrame(
      List<CustomAnimalSelectionEntry> animals,
      Player player,
      float BaseScale,
      int frameCount_X = 7,
      int frameCount_Y = 5,
      bool AddSellPrice = false)
    {
      this.SetUp(animals, player, BaseScale, frameCount_X, frameCount_Y, AddSellPrice);
    }

    private void SetUp(
      List<CustomAnimalSelectionEntry> animals,
      Player player,
      float BaseScale,
      int frameCount_X = 7,
      int frameCount_Y = 5,
      bool AddSellPrice = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.animalSelectionGrid = new CustomAnimalSelectionGrid(animals, player, BaseScale, frameCount_X, frameCount_Y, AddSellPrice);
      this.animalSelectionGrid.location = new Vector2(defaultXbuffer, defaultYbuffer);
      this.customerFrame = new CustomerFrame(this.animalSelectionGrid.GetSize(true, true) + new Vector2(defaultXbuffer * 2f, defaultYbuffer * 2f), CustomerFrameColors.DarkBrown, BaseScale);
      this.animalSelectionGrid.location += -this.customerFrame.VSCale * 0.5f;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void AddTickToSelectedEntry(bool removeTick = false) => this.animalSelectionGrid.TickLastSelectedEntry(removeTick);

    public PrisonerInfo UpdateCustomAnimalSelectionGridFrame(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      return this.animalSelectionGrid.UpdateCustomAnimalSelectionGrid(player, DeltaTime, offset);
    }

    public void DrawCustomAnimalSelectionGridFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.animalSelectionGrid.DrawCustomAnimalSelectionGrid(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.DominantHybridsSummaryDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals.DetailFrame
{
  internal class DominantHybridsSummaryDisplay
  {
    public Vector2 location;
    private ZGenericText header;
    private AnimalInFrameGrid animals;

    public DominantHybridsSummaryDisplay(AnimalType animalType, Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = 0.0f;
      float num2 = 0.0f;
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.header = new ZGenericText("Dominant Hybrids", BaseScale, _UseOnePointFiveFont: true);
      Vector2 size1 = this.header.GetSize();
      this.header.vLocation.Y += size1.Y * 0.5f;
      float num3 = num1 + size1.Y + defaultYbuffer * 0.5f;
      int maxFramesToDisplay = 6;
      List<AnimalRenderDescriptor> forThisAnimalType = player.unlocks.GetAllHybridsDiscoveredForThisAnimalType(animalType);
      if (forThisAnimalType.Count <= 0)
        return;
      this.animals = new AnimalInFrameGrid(BaseScale, 3, defaultXbuffer, defaultYbuffer, forThisAnimalType, maxFramesToDisplay, true);
      this.animals.location.Y = num3;
      Vector2 size2 = this.animals.GetSize();
      float num4 = num2 + size2.X;
      float num5 = num3 + size2.Y;
    }

    public bool UpdateDominantHybridsSummaryDisplay(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.animals != null && this.animals.UpdateForPlusButton(player, DeltaTime, offset);
    }

    public void DrawDominantHybridsSummaryDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawZGenericText(offset, spriteBatch);
      if (this.animals == null)
        return;
      this.animals.DrawAnimalInFrameGrid(offset, spriteBatch);
    }
  }
}

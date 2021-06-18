// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals.EnrichmentOnAnimalInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals
{
  internal class EnrichmentOnAnimalInfo
  {
    public Vector2 location;
    private AnimalInFrame animalInFrame;
    private ZGenericText animalTypeName;
    private EnrichmentBar enrichmentBar;

    public EnrichmentOnAnimalInfo(
      PrisonZone pen,
      AnimalType showThisAnimal,
      TILETYPE tileType,
      float BaseScale,
      float forceWidth)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float x = 0.0f;
      float y = 0.0f;
      this.animalInFrame = new AnimalInFrame(showThisAnimal, AnimalType.None, TargetSize: uiScaleHelper.ScaleX(50f), BaseScale: BaseScale);
      Vector2 size = this.animalInFrame.GetSize();
      this.animalInFrame.Location += size * 0.5f + new Vector2(x, y);
      float num1 = x + size.X + defaultXbuffer;
      this.animalTypeName = new ZGenericText(EnemyData.GetEnemyTypeName(showThisAnimal), BaseScale, false, _UseOnePointFiveFont: true);
      this.animalTypeName.vLocation.X = num1;
      this.animalTypeName.vLocation.Y = y;
      float num2 = y + this.animalTypeName.GetSize().Y + defaultYbuffer;
      if (tileType == TILETYPE.None)
        return;
      this.enrichmentBar = new EnrichmentBar(pen, showThisAnimal, tileType, BaseScale);
      this.enrichmentBar.GetBarSize();
      this.enrichmentBar.location.X = forceWidth - num1 - this.enrichmentBar.GetExtraIconOffset();
      this.enrichmentBar.location.Y = size.Y * 0.5f + this.animalTypeName.GetSize().Y - this.enrichmentBar.GetExtraTextSpaceAtBottom();
    }

    public Vector2 GetSize() => this.animalInFrame.FrameVSCALE;

    public void RefreshBar() => this.enrichmentBar.RefreshBar();

    public void UpdateEnrichmentOnAnimalInfo()
    {
    }

    public void DrawEnrichmentOnAnimalInfo(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
      this.animalTypeName.DrawZGenericText(offset, spriteBatch);
      if (this.enrichmentBar == null)
        return;
      this.enrichmentBar.DrawEnrichmentBar(offset, spriteBatch);
    }
  }
}

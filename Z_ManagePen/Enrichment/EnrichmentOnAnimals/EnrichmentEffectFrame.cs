// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals.EnrichmentEffectFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals
{
  internal class EnrichmentEffectFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleArrowPageButtons arrows;
    private EnrichmentOnAnimalInfo animalInfo;
    private List<AnimalType> animalTypesAvailable;
    internal static int currentAnimalTypeIndex;
    private SimpleTextHandler noAnimalsText;
    private PrisonZone refPen;
    private TILETYPE refTileType;
    private float refBaseScale;
    private float refXbuffer;
    private float refYbuffer;
    private float refForceWidth;

    public EnrichmentEffectFrame(
      TILETYPE selectedItem,
      PrisonZone pen,
      float BaseScale,
      float ForceWidth,
      float Xbuffer,
      float Ybuffer)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.animalTypesAvailable = pen.prisonercontainer.GetAllTypesOfAnimal();
      this.refPen = pen;
      this.refBaseScale = BaseScale;
      this.refXbuffer = Xbuffer;
      this.refYbuffer = Ybuffer;
      this.refTileType = selectedItem;
      this.refForceWidth = ForceWidth;
      if (this.animalTypesAvailable.Count == 0)
      {
        this.noAnimalsText = new SimpleTextHandler("There are no animals in the pen!", true, (float) ((double) ForceWidth / 1024.0 * 0.899999976158142), BaseScale);
        this.noAnimalsText.AutoCompleteParagraph();
        this.noAnimalsText.SetAllColours(ColourData.Z_Cream);
      }
      else
      {
        this.animalInfo = new EnrichmentOnAnimalInfo(pen, this.animalTypesAvailable[EnrichmentEffectFrame.currentAnimalTypeIndex], selectedItem, BaseScale, ForceWidth);
        this.animalInfo.location = new Vector2(this.refXbuffer, this.refYbuffer);
        if (this.animalTypesAvailable.Count > 1)
        {
          this.arrows = new SimpleArrowPageButtons(BaseScale);
          Vector2 size = this.arrows.GetSize();
          this.arrows.Location.X = ForceWidth - size.X * 0.5f;
          this.arrows.Location.Y += size.Y * 0.5f;
          this.arrows.Location += new Vector2(uiScaleHelper.ScaleX(-5f), uiScaleHelper.ScaleY(5f));
        }
      }
      this.customerFrame = new CustomerFrame(new Vector2(ForceWidth, uiScaleHelper.ScaleY(50f) + Ybuffer * 2f), BaseScale: BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      if (this.animalInfo != null)
        this.animalInfo.location += vector2;
      if (this.arrows == null)
        return;
      this.arrows.Location += vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void RefreshBar()
    {
      if (this.animalInfo == null)
        return;
      this.animalInfo.RefreshBar();
    }

    public void UpdateEnrichmentEffectFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.arrows == null)
        return;
      int num = this.arrows.UpdateSimpleArrowPageButtons(DeltaTime, player, offset);
      if (num == 0)
        return;
      EnrichmentEffectFrame.currentAnimalTypeIndex += num;
      if (EnrichmentEffectFrame.currentAnimalTypeIndex < 0)
        EnrichmentEffectFrame.currentAnimalTypeIndex = this.animalTypesAvailable.Count - 1;
      else if (EnrichmentEffectFrame.currentAnimalTypeIndex > this.animalTypesAvailable.Count - 1)
        EnrichmentEffectFrame.currentAnimalTypeIndex = 0;
      this.animalInfo = new EnrichmentOnAnimalInfo(this.refPen, this.animalTypesAvailable[EnrichmentEffectFrame.currentAnimalTypeIndex], this.refTileType, this.refBaseScale, this.refForceWidth);
      this.animalInfo.location += -this.customerFrame.VSCale * 0.5f + new Vector2(this.refXbuffer, this.refYbuffer);
    }

    public void DrawEnrichmentEffectFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.animalInfo != null)
        this.animalInfo.DrawEnrichmentOnAnimalInfo(offset, spriteBatch);
      if (this.noAnimalsText != null)
        this.noAnimalsText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.arrows == null)
        return;
      this.arrows.DrawSimpleArrowPageButtons(offset, spriteBatch);
    }
  }
}

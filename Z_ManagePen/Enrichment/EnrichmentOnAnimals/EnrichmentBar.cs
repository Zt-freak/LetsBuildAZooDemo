// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals.EnrichmentBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Animals.Enrichment;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_ManagePen.Enrichment.EnrichmentOnAnimals
{
  internal class EnrichmentBar
  {
    public Vector2 location;
    private SatisfactionBar bar;
    private SmallEnrichmentIcon icon;
    private float extraIconSpace;
    private float extraTexSpace;
    private PrisonZone refPen;
    private AnimalType refAnimalType;
    private TILETYPE reftileType;
    private ZGenericText effectText;
    private string noEffectString;
    private string fullEnrichmentString;

    public EnrichmentBar(
      PrisonZone pen,
      AnimalType animalType,
      TILETYPE tileType,
      float BaseScale,
      bool IncludeText = true)
    {
      this.refPen = pen;
      this.refAnimalType = animalType;
      this.reftileType = tileType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = uiScaleHelper.GetDefaultXBuffer() * 0.5f;
      float num2 = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      this.bar = new SatisfactionBar(0.0f, BaseScale);
      this.bar.SetBarColours(ColourData.LogGreen);
      this.bar.AddNewBar(0.0f, ColourData.Z_BarBabyGreen, 1);
      Vector2 size1 = this.bar.GetSize();
      this.icon = new SmallEnrichmentIcon(BaseScale);
      this.icon.vLocation.X = (float) (-(double) size1.X * 0.5);
      this.extraIconSpace = this.icon.GetSize().X * 0.5f + num1;
      this.icon.vLocation.X -= this.extraIconSpace;
      if (IncludeText)
      {
        this.noEffectString = "No Effect";
        this.fullEnrichmentString = "Max";
        this.effectText = new ZGenericText(BaseScale);
        Vector2 size2 = this.effectText.GetSize();
        this.effectText.vLocation.Y = size1.Y;
        this.effectText.vLocation.Y += num2;
        this.extraTexSpace = size2.Y + num2;
      }
      this.RefreshBar();
    }

    public float GetExtraIconOffset() => this.extraIconSpace;

    public float GetExtraTextSpaceAtBottom() => this.extraTexSpace;

    public Vector2 GetBarSize() => this.bar.GetSize() + new Vector2(this.extraIconSpace, 0.0f);

    public void RefreshBar()
    {
      float BarFullnessWithOneMoreOfThese;
      float num = EnrichmentCalculator.GetBarForThis_Enichment(this.refPen, this.refAnimalType, this.reftileType, out BarFullnessWithOneMoreOfThese, true);
      if (double.IsNaN((double) BarFullnessWithOneMoreOfThese))
        BarFullnessWithOneMoreOfThese = 0.0f;
      if (double.IsNaN((double) num))
        num = 0.0f;
      float _Fullness1 = MathHelper.Clamp(BarFullnessWithOneMoreOfThese, 0.0f, 1f);
      float _Fullness2 = MathHelper.Clamp(num, 0.0f, 1f);
      this.bar.SetFullness(_Fullness1);
      this.bar.SetFullness(_Fullness2, 1);
      if (this.effectText == null)
        return;
      if ((double) _Fullness2 == 1.0)
        this.effectText.textToWrite = this.fullEnrichmentString;
      else if ((double) _Fullness1 - (double) _Fullness2 <= 0.0)
        this.effectText.textToWrite = this.noEffectString;
      else
        this.effectText.textToWrite = string.Empty;
    }

    public void UpdateEnrichmentBar()
    {
    }

    public void DrawEnrichmentBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bar.DrawSatisfactionBar(offset, spriteBatch);
      this.icon.DrawSmallEnrichmentIcon(offset, spriteBatch);
      if (this.effectText == null)
        return;
      this.effectText.DrawZGenericText(offset, spriteBatch);
    }
  }
}

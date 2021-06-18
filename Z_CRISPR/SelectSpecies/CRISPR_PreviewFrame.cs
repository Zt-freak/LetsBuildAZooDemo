// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.SelectSpecies.CRISPR_PreviewFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_CRISPR.SelectSpecies
{
  internal class CRISPR_PreviewFrame
  {
    public Vector2 location;
    public CustomerFrame customerFrame;
    private SimpleTextHandler selectionPromptText;
    private CRIPSRresultRow resultRow;
    private bool hasSelectedSomething;

    public CRISPR_PreviewFrame(float BaseScale, float forceWidth = 480f)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float x = forceWidth;
      float y = uiScaleHelper.ScaleY(60f);
      this.customerFrame = new CustomerFrame(new Vector2(x, y), CustomerFrameColors.Brown, BaseScale);
      this.selectionPromptText = new SimpleTextHandler("To proceed, choose 2 species whose genome you would like to edit.", true, (float) ((double) x / 1024.0 * 0.899999976158142), BaseScale);
      this.selectionPromptText.AutoCompleteParagraph();
      this.selectionPromptText.SetAllColours(ColourData.Z_Cream);
      this.selectionPromptText.Location.Y += (float) ((double) this.selectionPromptText.GetHeightOfOneLine() * 0.5 - (double) this.selectionPromptText.GetHeightOfParagraph() * 0.5);
      this.resultRow = new CRIPSRresultRow(BaseScale);
      this.resultRow.location.X = (float) (-(double) x * 0.5);
      this.hasSelectedSomething = false;
    }

    public void SetAnimals(AnimalType one, AnimalType two, Player player)
    {
      this.resultRow.SetAnimals(one, two, player);
      this.hasSelectedSomething = true;
    }

    public void UpdateCRISPR_PreviewFrame()
    {
    }

    public void DrawCRISPR_PreviewFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (!this.hasSelectedSomething)
        this.selectionPromptText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      else
        this.resultRow.DrawCRIPSRresultRow(offset, spriteBatch);
    }
  }
}

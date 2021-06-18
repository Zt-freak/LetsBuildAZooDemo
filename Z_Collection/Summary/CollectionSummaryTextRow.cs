// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Summary.CollectionSummaryTextRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_Collection.Summary
{
  internal class CollectionSummaryTextRow
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText text;
    private ZGenericText numbers;
    private SatisfactionBar satisfactionBar;

    public CollectionSummaryTextRow(
      CollectionSummaryRowType rowType,
      Player player,
      float BaseScale,
      float width)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num = uiScaleHelper.GetDefaultYBuffer() * 0.5f;
      float y1 = num;
      this.text = new ZGenericText(BaseScale, false, _UseOnePointFiveFont: true);
      this.text.vLocation = new Vector2(defaultXbuffer, y1);
      this.satisfactionBar = new SatisfactionBar(1f, BaseScale);
      this.satisfactionBar.vLocation.X = uiScaleHelper.ScaleX(200f);
      this.numbers = new ZGenericText(BaseScale, false, _UseOnePointFiveFont: true);
      this.numbers.vLocation = this.text.vLocation;
      this.numbers.vLocation.X += uiScaleHelper.ScaleX(250f);
      this.SetStringsForRows(rowType, player);
      Vector2 size = this.text.GetSize();
      float y2 = y1 + size.Y + num;
      this.customerFrame = new CustomerFrame(new Vector2(width, y2), CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      ZGenericText text = this.text;
      text.vLocation = text.vLocation + vector2;
      ZGenericText numbers = this.numbers;
      numbers.vLocation = numbers.vLocation + vector2;
      this.satisfactionBar.vLocation.X += vector2.X;
    }

    public void SetStringsForRows(CollectionSummaryRowType rowType, Player player)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (rowType)
      {
        case CollectionSummaryRowType.Buildings:
          str1 = "Buildings";
          str2 = "0/0";
          break;
        case CollectionSummaryRowType.Enrichment:
          str1 = "Enrichment Items";
          str2 = "0/0";
          break;
        case CollectionSummaryRowType.AnimalSpecies:
          int totalSpeciesInGame;
          int totalSpeciesFound = player.Stats.GetTotalSpeciesFound(out totalSpeciesInGame);
          str1 = "Animal Species";
          str2 = string.Format("{0}/{1}", (object) totalSpeciesFound, (object) totalSpeciesInGame);
          this.satisfactionBar.SetFullness((float) totalSpeciesFound / (float) totalSpeciesInGame);
          break;
        case CollectionSummaryRowType.AnimalVariants:
          int num = 560;
          int totalVaiantsFound = player.Stats.GetTotalVaiantsFound();
          str1 = "Animal Variants";
          str2 = string.Format("{0}/{1}", (object) totalVaiantsFound, (object) num);
          this.satisfactionBar.SetFullness((float) totalVaiantsFound / (float) num);
          break;
        case CollectionSummaryRowType.EmployeeJobs:
          str1 = "Jobs";
          str2 = "0/0";
          break;
        case CollectionSummaryRowType.EmployeeVariants:
          str1 = "Employees";
          str2 = "0/0";
          break;
        case CollectionSummaryRowType.Research:
          str1 = "Research";
          str2 = "0/0";
          break;
        case CollectionSummaryRowType.Tasks:
          str1 = "Tasks";
          str2 = "0/0";
          break;
        case CollectionSummaryRowType.Achievements:
          str1 = "Achievements";
          str2 = "0/0";
          break;
      }
      this.text.textToWrite = str1;
      this.numbers.textToWrite = str2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void DrawCollectionSummaryTextRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
      this.satisfactionBar.DrawSatisfactionBar(offset, spriteBatch);
      this.numbers.DrawZGenericText(offset, spriteBatch);
    }
  }
}

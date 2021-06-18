// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.RatingPopUp.RatingCategoryRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_BalanceSystems.Publicity;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.RatingPopUp.Row.Columns;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.RatingPopUp
{
  internal class RatingCategoryRow
  {
    public Vector2 location;
    private PerformanceTableRowFrame frame;
    private RatingIconAndName iconAndName;
    private ZGenericText score;
    private RatingPerformanceBarAndPercent performanceBar;
    private ZGenericText[] headerTexts;
    private RatingCategory refCategory;

    public RatingCategoryRow(
      RatingCategory category,
      Player player,
      float BaseScale,
      bool IsHeader = false)
    {
      this.refCategory = category;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultYbuffer = (double) uiScaleHelper.GetDefaultYBuffer();
      float num1 = uiScaleHelper.GetDefaultXBuffer() * 0.5f;
      float[] numArray = new float[3]{ 140f, 60f, 110f };
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = uiScaleHelper.ScaleX(numArray[index]);
      float num2 = 25f;
      if (IsHeader)
        num2 = 15f;
      this.frame = new PerformanceTableRowFrame(BaseScale, uiScaleHelper.ScaleY(num2), CustomerFrameColors.Brown, numArray);
      float num3 = (float) (-(double) this.frame.GetSize().X * 0.5);
      float num4 = 0.0f;
      for (int index = 0; index < 3; ++index)
      {
        float num5 = num4 + numArray[index] * 0.5f;
        if (IsHeader)
        {
          if (this.headerTexts == null)
            this.headerTexts = new ZGenericText[3];
          this.headerTexts[index] = new ZGenericText(RatingCategoryRow.GetHeaderText((RatingRowColumn) index), BaseScale);
          this.headerTexts[index].vLocation.X = num5 + num3;
        }
        else
        {
          switch (index)
          {
            case 0:
              this.iconAndName = new RatingIconAndName(category, BaseScale);
              this.iconAndName.location.X += num5 - numArray[index] * 0.5f + num1 + num3;
              break;
            case 1:
              this.score = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
              this.score.vLocation.X += num5 + num3;
              break;
            case 2:
              this.performanceBar = new RatingPerformanceBarAndPercent(category, player, BaseScale);
              this.performanceBar.location.X += num5 + num3;
              this.performanceBar.location.X -= this.performanceBar.GetSize().X * 0.5f;
              break;
          }
        }
        num4 = num5 + numArray[index] * 0.5f;
      }
      if (IsHeader)
        return;
      this.RefreshValues(player);
    }

    private static int GetRatingScore(RatingCategory category, Player player)
    {
      switch (category)
      {
        case RatingCategory.Facilities:
          return (int) Math.Round((double) FacilitiesCalulator.CalculateFacilities(player));
        case RatingCategory.Animals:
          float AnimalValue;
          double valueAndPopularity = (double) ParkPopularity.CalculateAnimalValueAndPopularity(player, out AnimalValue, out int _);
          return (int) Math.Round((double) AnimalValue);
        case RatingCategory.Decoration:
          return (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
        case RatingCategory.Publicity:
          return PublicityCalculator.CalculatePublicity(player);
        default:
          return -1;
      }
    }

    private static string GetHeaderText(RatingRowColumn column)
    {
      switch (column)
      {
        case RatingRowColumn.IconAndName:
          return "Category";
        case RatingRowColumn.Score:
          return "Rating";
        case RatingRowColumn.Performance:
          return "Performance";
        default:
          return "NA";
      }
    }

    public Vector2 GetSize() => this.frame.GetSize();

    public void RefreshValues(Player player)
    {
      this.score.textToWrite = RatingCategoryRow.GetRatingScore(this.refCategory, player).ToString();
      this.performanceBar.SetValues(this.refCategory, player);
    }

    public void UpdateRatingCategoryRow(Player player, Vector2 offset) => offset += this.location;

    public void DrawRatingCategoryRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      if (this.headerTexts != null)
      {
        for (int index = 0; index < this.headerTexts.Length; ++index)
          this.headerTexts[index].DrawZGenericText(offset, spriteBatch);
      }
      else
      {
        this.iconAndName.DrawRatingIconAndName(offset, spriteBatch);
        this.score.DrawZGenericText(offset, spriteBatch);
        this.performanceBar.DrawRatingPerformanceBarAndPercent(offset, spriteBatch);
      }
    }
  }
}

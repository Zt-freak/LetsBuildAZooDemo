// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row.MoralitySummaryRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.RatingPopUp;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row
{
  internal class MoralitySummaryRow
  {
    public Vector2 location;
    private PerformanceTableRowFrame frame;
    private RatingIconAndName iconAndName;
    private ZGenericText score;
    private MoralitySummaryBar bar;
    private ZGenericText[] headerTexts;
    private ZGenericText UnlockText;
    private MoralityCategory refCategory;
    private bool IsHeader;
    private bool IsUnlockRow;

    public MoralitySummaryRow(
      MoralityCategory category,
      Player player,
      float BaseScale,
      bool _IsHeader = false,
      bool _IsUnlockRow = false)
    {
      this.refCategory = category;
      this.IsHeader = _IsHeader;
      this.IsUnlockRow = _IsUnlockRow;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2 = uiScaleHelper.DefaultBuffer * 0.5f;
      float[] numArray = new float[2]{ 105f, 120f };
      float num1 = 0.0f;
      for (int index = 0; index < numArray.Length; ++index)
      {
        numArray[index] *= BaseScale;
        num1 += numArray[index];
      }
      float num2 = 25f;
      if (this.IsHeader)
        num2 = 15f;
      if (this.IsUnlockRow)
      {
        this.UnlockText = new ZGenericText("Morality Bonuses", BaseScale, _UseOnePointFiveFont: true);
        this.frame = new PerformanceTableRowFrame(BaseScale, uiScaleHelper.ScaleY(num2), CustomerFrameColors.Brown, new float[1]
        {
          this.UnlockText.GetSize().X + vector2.X * 2f
        });
      }
      else
      {
        this.frame = new PerformanceTableRowFrame(BaseScale, uiScaleHelper.ScaleY(num2), CustomerFrameColors.Brown, numArray);
        float num3 = (float) (-(double) this.frame.GetSize().X * 0.5);
        float num4 = 0.0f;
        for (int index = 0; index < 2; ++index)
        {
          float num5 = num4 + numArray[index] * 0.5f;
          if (this.IsHeader)
          {
            if (this.headerTexts == null)
              this.headerTexts = new ZGenericText[2];
            this.headerTexts[index] = new ZGenericText(this.GetHeaderText((MoralitySummaryColumn) index), BaseScale);
            this.headerTexts[index].vLocation.X = num5 + num3;
          }
          else
          {
            switch ((MoralitySummaryColumn) index)
            {
              case MoralitySummaryColumn.IconAndName:
                this.iconAndName = new RatingIconAndName(category, BaseScale);
                this.iconAndName.location.X += num5 - numArray[index] * 0.5f + vector2.X + num3;
                break;
              case MoralitySummaryColumn.Score:
                this.bar = new MoralitySummaryBar(category, player, BaseScale);
                this.bar.location.X += num5 + num3;
                this.bar.location.X -= this.bar.GetSize().X * 0.5f;
                break;
            }
          }
          num4 = num5 + numArray[index] * 0.5f;
        }
      }
    }

    public string GetHeaderText(MoralitySummaryColumn column)
    {
      if (column == MoralitySummaryColumn.IconAndName)
        return "Category";
      return column == MoralitySummaryColumn.Score ? "Points" : "NA_" + (object) column;
    }

    public Vector2 GetSize() => this.frame.GetSize();

    public void RefreshValues(Player player) => this.bar.RefreshValues(player);

    public bool UpdateMoralitySummaryRow(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out MoralityCategory category)
    {
      offset += this.location;
      category = MoralityCategory.Count;
      if (this.IsHeader || !this.frame.UpdateFrameForMouseOver(player, DeltaTime, offset))
        return false;
      category = this.refCategory;
      return true;
    }

    public void DrawMoralitySummaryRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      if (this.IsHeader)
      {
        for (int index = 0; index < this.headerTexts.Length; ++index)
          this.headerTexts[index].DrawZGenericText(offset, spriteBatch);
      }
      else if (this.IsUnlockRow)
      {
        this.UnlockText.DrawZGenericText(offset, spriteBatch);
      }
      else
      {
        this.iconAndName.DrawRatingIconAndName(offset, spriteBatch);
        this.bar.DrawSatisfactionBarWithBigNumber(offset, spriteBatch);
      }
      this.frame.PostDrawPerformanceTableRowFrame(offset, spriteBatch);
    }
  }
}

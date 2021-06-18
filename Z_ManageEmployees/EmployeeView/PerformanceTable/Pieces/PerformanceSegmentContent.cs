// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces.PerformanceSegmentContent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Text;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces
{
  internal class PerformanceSegmentContent
  {
    public Vector2 location;
    private AnimalInFrame animalInFrame;
    private LittleSummaryButton littleSummarybutton;
    private ZGenericText textObj;
    private ZGenericText textObj2;
    private bool UseRoundaboutFont;
    private BarWithAvgPercent bar;
    private bool isHeader;
    private bool isSummaryRow;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private PerformanceColumn columnType;
    private Vector2 refCellSize;
    private StringScroller stringScroller;

    public PerformanceSegmentContent(
      PerformanceColumn _columnType,
      bool _isHeader,
      float _BaseScale,
      Vector2 _refCellSize,
      bool _isSummaryRow = false)
    {
      this.isHeader = _isHeader;
      this.isSummaryRow = _isSummaryRow;
      this.BaseScale = _BaseScale;
      this.columnType = _columnType;
      this.refCellSize = _refCellSize;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
    }

    public void SetUp(Employee employee, PerformanceSummaryData data)
    {
      if (this.isHeader || this.isSummaryRow)
      {
        string _textToWrite1 = string.Empty;
        string _textToWrite2 = string.Empty;
        float percentFilled = -1f;
        switch (this.columnType)
        {
          case PerformanceColumn.Employee:
            if (this.isSummaryRow)
            {
              _textToWrite1 = "Yesterday's";
              _textToWrite2 = "Branch Averages";
              this.UseRoundaboutFont = true;
              break;
            }
            break;
          case PerformanceColumn.Efficiency:
            _textToWrite1 = "Efficiency";
            percentFilled = data.AverageEfficiency;
            break;
          case PerformanceColumn.Satisfaction:
            _textToWrite1 = "Satisfaction";
            percentFilled = data.AverageSatisfaction;
            break;
          case PerformanceColumn.Salary:
            _textToWrite1 = "Daily Salary";
            _textToWrite2 = "$" + data.AverageDailySalary.ToString();
            break;
        }
        if (!string.IsNullOrEmpty(_textToWrite1))
        {
          this.CreateText(_textToWrite1);
          if (this.UseRoundaboutFont)
            this.textObj.vLocation.Y -= this.textObj.GetSize().Y * 0.5f;
          else
            this.textObj.vLocation.Y -= this.refCellSize.Y * 0.25f;
        }
        if (!string.IsNullOrEmpty(_textToWrite2))
        {
          this.CreateText(_textToWrite2, 1);
          if (this.UseRoundaboutFont)
            this.textObj2.vLocation.Y += this.textObj2.GetSize().Y * 0.5f;
          else
            this.textObj2.vLocation.Y += this.refCellSize.Y * 0.25f;
        }
        if ((double) percentFilled == -1.0)
          return;
        double bar = (double) this.CreateBar(percentFilled);
        this.bar.location.Y += this.refCellSize.Y * 0.25f;
      }
      else
      {
        switch (this.columnType)
        {
          case PerformanceColumn.Employee:
            this.CreateText(employee.quickemplyeedescription.NAME, LeftJustified: true);
            this.animalInFrame = new AnimalInFrame(employee.quickemplyeedescription.thisemployee, AnimalType.None, TargetSize: (25f * this.BaseScale), FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale);
            float num1 = this.scaleHelper.DefaultBuffer.X * 0.5f;
            this.animalInFrame.Location.X = num1;
            this.animalInFrame.Location.X += this.animalInFrame.GetSize().X * 0.5f;
            this.animalInFrame.Location.X -= this.refCellSize.X * 0.5f;
            float num2 = num1 + (this.animalInFrame.GetSize().X + this.scaleHelper.DefaultBuffer.X * 0.5f);
            this.textObj.vLocation.X = num2;
            this.textObj.vLocation.X -= this.refCellSize.X * 0.5f;
            this.textObj.vLocation.Y -= this.textObj.GetSize().Y * 0.5f;
            this.stringScroller = new StringScroller(this.refCellSize.X - num2, this.textObj.textToWrite, this.textObj.fontToUse);
            break;
          case PerformanceColumn.Efficiency:
            double bar1 = (double) this.CreateBar(MathStuff.getRandomFloat(0.0f, 1f), data.AverageEfficiency);
            break;
          case PerformanceColumn.Satisfaction:
            double bar2 = (double) this.CreateBar(MathStuff.getRandomFloat(0.0f, 1f), data.AverageSatisfaction);
            break;
          case PerformanceColumn.Salary:
            this.CreateText("$" + (employee.quickemplyeedescription.CurrentSalary / 7).ToString());
            this.textObj.vLocation.X -= this.CreateBar((float) (employee.quickemplyeedescription.CurrentSalary / 7) * 0.01f, (float) data.AverageDailySalary * 0.01f, true, true) * 0.5f;
            break;
          case PerformanceColumn.ViewMoreInfo:
            this.littleSummarybutton = new LittleSummaryButton(LittleSummaryButtonType.Locate, _BaseScale: this.BaseScale);
            break;
        }
      }
    }

    private Vector2 CreateText(string _textToWrite, int index = 0, bool LeftJustified = false)
    {
      if (index == 0)
      {
        this.textObj = new ZGenericText(_textToWrite, this.BaseScale, !LeftJustified, _UseOnePointFiveFont: this.UseRoundaboutFont);
        return this.textObj.GetSize();
      }
      this.textObj2 = new ZGenericText(_textToWrite, this.BaseScale, _UseOnePointFiveFont: this.UseRoundaboutFont);
      return this.textObj2.GetSize();
    }

    private float CreateBar(
      float percentFilled,
      float avg = -1f,
      bool MoreIsBad = false,
      bool NoBar_JustArrowAndNumber = false)
    {
      this.bar = (double) avg == -1.0 ? new BarWithAvgPercent(percentFilled, this.BaseScale) : new BarWithAvgPercent(percentFilled, avg, this.BaseScale, MoreIsBad, NoBar_JustArrowAndNumber);
      if (NoBar_JustArrowAndNumber)
        return this.bar.GetExtraLengthFromText();
      this.bar.location.X -= this.bar.GetExtraLengthFromText() * 0.5f;
      return this.bar.GetSize_BarOnly().Y;
    }

    public bool UpdatePerformanceSegmentContent(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.littleSummarybutton != null && this.littleSummarybutton.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return true;
      if (this.stringScroller != null)
      {
        this.stringScroller.UpdateStringScroller(DeltaTime);
        this.textObj.textToWrite = this.stringScroller.GetString();
      }
      return false;
    }

    public void DrawPerformanceSegmentContent(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.textObj != null)
        this.textObj.DrawZGenericText(offset, spriteBatch);
      if (this.textObj2 != null)
        this.textObj2.DrawZGenericText(offset, spriteBatch);
      if (this.animalInFrame != null)
        this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
      if (this.bar != null)
        this.bar.DrawBarWithAvgPercent(offset, spriteBatch);
      if (this.littleSummarybutton == null)
        return;
      this.littleSummarybutton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}

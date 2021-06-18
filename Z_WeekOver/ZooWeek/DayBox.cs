// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.ZooWeek.DayBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.FinancialHistory;
using TinyZoo.Z_WeekOver.ZooWeek.DayBoxInternals;

namespace TinyZoo.Z_WeekOver.ZooWeek
{
  internal class DayBox : GameObject
  {
    private string TextHead;
    private GameObject Frame;
    private GameObject InnerFrame;
    private Vector2 VSCALE;
    private Vector2 INNERVSCALE;
    private SimpleTextHandler texticle;
    private InternalStaff internalstaff;
    private AnimalInternals internalanimals;

    public DayBox(SummaryListType thislist, int DayIndex, Player player)
    {
      if (DayIndex >= 0)
      {
        this.TextHead = DayOfWeekDisplay.GetDayOfTheWeek(DayIndex);
      }
      else
      {
        switch (thislist)
        {
          case SummaryListType.DayOfWeek:
            this.TextHead = "Day";
            break;
          case SummaryListType.NewAnimalsBorn:
            this.TextHead = "New Animals";
            break;
          case SummaryListType.AnimalsRemoved:
            this.TextHead = "Animals Removed";
            break;
          case SummaryListType.AnimalsDied:
            this.TextHead = "Animal Deaths";
            break;
          case SummaryListType.NewEmployees:
            this.TextHead = "New Employees";
            break;
          case SummaryListType.FiredEmployees:
            this.TextHead = "Fired Employees";
            break;
          case SummaryListType.EmployeeDeaths:
            this.TextHead = "Employee Deaths";
            break;
        }
      }
      this.Frame = new GameObject();
      this.Frame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Frame.SetAllColours(ColourData.Z_TextBrown);
      this.Frame.SetDrawOriginToCentre();
      this.VSCALE = new Vector2(100f, 100f);
      if (thislist == SummaryListType.DayOfWeek)
        this.VSCALE.Y = 30f;
      this.InnerFrame = new GameObject(this.Frame);
      this.InnerFrame.SetAllColours(ColourData.Z_Cream);
      this.INNERVSCALE = this.VSCALE;
      this.INNERVSCALE -= new Vector2(3f, 3f);
      this.VSCALE += new Vector2(3f, 3f);
      if (DayIndex == -1 && thislist != SummaryListType.DayOfWeek)
      {
        this.texticle = new SimpleTextHandler(this.TextHead, true, 0.1f, GameFlags.GetSmallTextScale());
        this.texticle.AutoCompleteParagraph();
        this.texticle.paragraph.linemaker.SetAllColours(ColourData.Z_TextBrown);
        this.texticle.Location = new Vector2(0.0f, this.texticle.GetHeightOfOneLine() * ((float) (this.texticle.paragraph.linemaker.GetNumberOfLines() - 1) * -0.5f));
      }
      if (DayIndex <= -1)
        return;
      WorldHistoryWeek weekBeforeThisOne = player.worldhistory.GetWeekBeforeThisOne();
      if (weekBeforeThisOne != null && weekBeforeThisOne.worldhistoryday.Count > DayIndex)
      {
        WorldHistoryDay worldHistoryDay = weekBeforeThisOne.worldhistoryday[DayIndex];
        switch (thislist)
        {
          case SummaryListType.NewAnimalsBorn:
            if (worldHistoryDay.NewAnimalsBornOrEarned.Count > 0)
            {
              this.internalanimals = new AnimalInternals(worldHistoryDay.NewAnimalsBornOrEarned);
              this.texticle = new SimpleTextHandler(worldHistoryDay.NewAnimalsBornOrEarned.Count.ToString() + " new!", true, 0.1f, GameFlags.GetSmallTextScale());
              this.texticle.AutoCompleteParagraph();
              this.texticle.paragraph.linemaker.SetAllColours(ColourData.Z_TextBrown);
              break;
            }
            break;
          case SummaryListType.NewEmployees:
            if (worldHistoryDay.NewEmployees.Count > 0)
            {
              this.internalstaff = new InternalStaff(worldHistoryDay.NewEmployees);
              this.texticle = new SimpleTextHandler(worldHistoryDay.NewEmployees.Count.ToString() + " new!", true, 0.1f, GameFlags.GetSmallTextScale());
              this.texticle.AutoCompleteParagraph();
              this.texticle.paragraph.linemaker.SetAllColours(ColourData.Z_TextBrown);
              break;
            }
            break;
        }
      }
      if (this.texticle == null)
        return;
      this.texticle.Location.Y = this.VSCALE.Y * 0.5f;
      this.texticle.Location.Y -= 15f;
    }

    public void SetUpTrash()
    {
    }

    public void UpdateDayBox(float DeltaTime)
    {
      if (this.internalstaff != null)
        this.internalstaff.UpdateInternalStaff(DeltaTime);
      if (this.internalanimals == null)
        return;
      this.internalanimals.UpdateInternalStaff(DeltaTime);
    }

    public void DrawDayBox(Vector2 Offset)
    {
      Offset += this.vLocation;
      this.Frame.SetAllColours(ColourData.Z_TextBrown);
      this.Frame.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.InnerFrame.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.INNERVSCALE);
      if (this.texticle != null)
        this.texticle.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatch03);
      else
        TextFunctions.DrawJustifiedText(this.TextHead, 0.5f, Offset, this.Frame.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
      if (this.internalstaff != null)
        this.internalstaff.DrawInternalStaff(Offset);
      if (this.internalanimals == null)
        return;
      this.internalanimals.DrawInternalStaff(Offset);
    }
  }
}

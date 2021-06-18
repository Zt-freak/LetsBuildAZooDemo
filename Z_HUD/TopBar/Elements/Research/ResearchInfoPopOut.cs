// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Research.ResearchInfoPopOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;
using TinyZoo.Z_Research_.ResearcherInfo;

namespace TinyZoo.Z_HUD.TopBar.Elements.Research
{
  internal class ResearchInfoPopOut : GenericTopBarPopOutFrame
  {
    private ResearcherSummary researchSummary;
    private SimpleTextHandler noResearchersText;
    private float reminderPopOutTime = 6f;
    private float reminderTimer;
    private bool IsReminderToSpendPoints;

    public ResearchInfoPopOut(
      float BaseScale,
      Player player,
      bool IsForResearchBuilding = false,
      bool _IsReminderToSpendPoints = false)
      : base(BaseScale)
    {
      Vector2 zero = Vector2.Zero;
      this.IsReminderToSpendPoints = _IsReminderToSpendPoints;
      this.researchSummary = new ResearcherSummary(player, BaseScale, true, IsForResearchBuilding);
      Vector2 vector2_1 = zero + this.scaleHelper.DefaultBuffer;
      if (this.IsReminderToSpendPoints)
        this.noResearchersText = new SimpleTextHandler("Research point earned!", this.scaleHelper.ScaleX(150f), true, BaseScale, AutoComplete: true);
      else if (this.researchSummary.GetNumberOfResearchersShown() > 0)
      {
        this.researchSummary.location = vector2_1;
        vector2_1 += this.researchSummary.GetSize();
      }
      else
        this.noResearchersText = new SimpleTextHandler("You do not have any researchers, hire some to get research points!", this.scaleHelper.ScaleX(150f), true, BaseScale, AutoComplete: true);
      if (this.noResearchersText != null)
      {
        this.noResearchersText.SetAllColours(ColourData.Z_Cream);
        this.noResearchersText.Location.Y = vector2_1.Y;
        this.noResearchersText.Location.Y += this.noResearchersText.GetHeightOfOneLine() * 0.5f;
        vector2_1 += this.noResearchersText.GetSize(true);
      }
      Vector2 _frameSize = vector2_1 + this.scaleHelper.DefaultBuffer;
      if (IsForResearchBuilding)
        this.FinalizeSize(_frameSize, ColourData.Z_FrameBluePale - new Vector3(25f) / (float) byte.MaxValue);
      else
        this.FinalizeSize(_frameSize);
      Vector2 vector2_2 = -_frameSize * 0.5f;
      if (this.noResearchersText != null)
        this.noResearchersText.Location.Y += vector2_2.Y;
      if (this.researchSummary == null)
        return;
      this.researchSummary.location += vector2_2;
    }

    public override bool CheckMouseOver(Player player, Vector2 offset) => base.CheckMouseOver(player, offset);

    public bool UpdateResearchInfoPopOut(Player player, float DeltaTime, Vector2 offset)
    {
      if (this.IsReminderToSpendPoints)
      {
        this.reminderTimer += DeltaTime;
        if ((double) this.reminderTimer > (double) this.reminderPopOutTime)
          this.LerpOff();
      }
      int num = this.UpdatePopOutFrame(player, DeltaTime, ref offset) ? 1 : 0;
      if (this.researchSummary == null)
        return num != 0;
      this.researchSummary.UpdateResearcherSummary(player, DeltaTime);
      return num != 0;
    }

    public void DrawResearchInfoPopOut(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      if (this.noResearchersText != null)
        this.noResearchersText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      else
        this.researchSummary.DrawResearcherSummary(offset, spriteBatch);
    }
  }
}

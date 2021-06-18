// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.V2.PostingInfoFrame_V2
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.V2
{
  internal class PostingInfoFrame_V2
  {
    public Vector2 location;
    private SplitFrame customerFrame;
    private ZCheckBox checkBox;
    private SimpleTextHandler header;
    private RecruitmentInfoIcon icon;
    private ZGenericText costString;
    private JobPostingModifiers refmodifier;
    private CustomerFrameMouseOverBox mouseOverBox;

    public PostingInfoFrame_V2(
      JobPostingModifiers modifier,
      OpenPositions _TEMPOPENPOSITIONS,
      float BaseScale)
    {
      this.refmodifier = modifier;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      bool _CentreJustify = modifier == JobPostingModifiers.Totals;
      float width_ = 50f * BaseScale;
      if (modifier != JobPostingModifiers.Totals)
        this.icon = new RecruitmentInfoIcon(modifier, BaseScale);
      this.header = new SimpleTextHandler(PostingInfoFrame.GetHeaderTextForThisModifier(modifier), width_, _CentreJustify, BaseScale, true, true);
      this.header.SetAllColours(ColourData.Z_Cream);
      if (modifier != JobPostingModifiers.AdminCost && modifier != JobPostingModifiers.Totals)
        this.checkBox = new ZCheckBox(BaseScale);
      this.costString = new ZGenericText("0", BaseScale, _UseOnePointFiveFont: true);
      Vector2 _VSCale = defaultBuffer;
      if (this.icon != null)
      {
        this.icon.vLocation = _VSCale;
        RecruitmentInfoIcon icon = this.icon;
        icon.vLocation = icon.vLocation + this.icon.GetSize() * 0.5f;
        _VSCale.X += this.icon.GetSize().X;
        _VSCale.X += defaultBuffer.X;
      }
      if (!_CentreJustify)
      {
        this.header.Location = _VSCale;
        if (this.icon != null)
          this.icon.vLocation.Y = this.header.Location.Y + this.header.GetHeightOfParagraph() * 0.5f;
      }
      else
      {
        this.header.Location.Y = _VSCale.Y;
        this.header.Location.Y += this.header.GetHeightOfOneLine() * 0.5f;
      }
      _VSCale.X += uiScaleHelper.ScaleX(70f);
      float num = uiScaleHelper.ScaleY(50f);
      _VSCale.Y += num;
      this.costString.vLocation.Y = _VSCale.Y;
      this.costString.vLocation.Y += this.costString.GetSize().Y * 0.5f;
      this.costString.vLocation.X = _VSCale.X * 0.5f;
      _VSCale.Y += this.costString.GetSize().Y;
      _VSCale.Y += defaultBuffer.Y;
      if (this.checkBox != null)
      {
        this.checkBox.location.Y = this.costString.vLocation.Y;
        this.checkBox.location.X = (float) ((double) _VSCale.X - (double) defaultBuffer.X - (double) this.checkBox.GetSize().X * 0.5);
      }
      Vector3 zFrameLightBrown = ColourData.Z_FrameLightBrown;
      Vector3 bottomColour = ColourData.Z_FrameMidBrown;
      if (modifier == JobPostingModifiers.Totals)
        bottomColour = ColourData.Z_FrameGold;
      this.customerFrame = new SplitFrame(_VSCale, zFrameLightBrown, bottomColour, BaseScale, num / _VSCale.Y);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      if (_CentreJustify)
        this.header.Location.Y += vector2.Y;
      else
        this.header.Location += vector2;
      if (this.icon != null)
      {
        RecruitmentInfoIcon icon = this.icon;
        icon.vLocation = icon.vLocation + vector2;
      }
      ZGenericText costString = this.costString;
      costString.vLocation = costString.vLocation + vector2;
      if (this.checkBox != null)
        this.checkBox.location += vector2;
      if (this.icon == null)
        return;
      this.mouseOverBox = new CustomerFrameMouseOverBox(BaseScale, PostingInfoFrame.GetDescriptionTextForThisModifier(this.refmodifier), uiScaleHelper.ScaleX(200f));
      this.mouseOverBox.location.Y += vector2.Y;
      this.mouseOverBox.location.Y += uiScaleHelper.ScaleY(15f);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void SetActive(bool _isPanelActive, bool _IsCheckboxActive = false)
    {
      if (this.checkBox != null)
        this.checkBox.SetActive(_IsCheckboxActive);
      this.customerFrame.SetActive(_isPanelActive);
    }

    public void SetNewCostString(string newString) => this.costString.textToWrite = newString;

    public bool GetIsTicked() => this.checkBox.GetIsTicked();

    public void SetCheckbox(bool isTicked) => this.checkBox.SetTicked(isTicked);

    public bool UpdatePostingInfoFrame_V2(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.checkBox != null && this.checkBox.UpdateCheckBox(player, offset))
        return true;
      if (this.icon != null)
      {
        this.icon.UpdateRecruitmentInfoIcon(player, DeltaTime, offset);
        if (this.icon.GetIsMouseOver())
          this.mouseOverBox.Active = true;
      }
      return false;
    }

    private void OnMouseOver_CreateMouseOverBox()
    {
    }

    public void DrawPostingInfoFrame_V2(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawSplitFrame(offset, spriteBatch);
      this.header.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.checkBox != null && !this.checkBox.isActive)
        this.checkBox.DrawCheckBox(spriteBatch, offset);
      if (this.icon != null)
        this.icon.DrawRecruitmentInfoIcon(offset, spriteBatch);
      this.costString.DrawZGenericText(offset, spriteBatch);
      this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
      if (this.checkBox == null || !this.checkBox.isActive)
        return;
      this.checkBox.DrawCheckBox(spriteBatch, offset);
    }

    public void PostDrawPostingInfoFrame_v2(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.mouseOverBox == null)
        return;
      this.mouseOverBox.DrawCustomerFrameMouseOverBoc(offset, spriteBatch);
    }
  }
}

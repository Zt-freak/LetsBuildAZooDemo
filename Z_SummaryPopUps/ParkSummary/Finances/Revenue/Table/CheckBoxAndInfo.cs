// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table.CheckBoxAndInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table
{
  internal class CheckBoxAndInfo
  {
    public Vector2 location;
    private LabelledCheckbox checkbox;
    private LittleSummaryButton infoButton;
    private SimpleTextHandler explanation;
    private bool ShowExplanation;
    private float width;
    private float BaseScale;

    public CheckBoxAndInfo(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.checkbox = new LabelledCheckbox("Estimated Values", false, this.BaseScale, true);
      this.infoButton = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: this.BaseScale);
      this.width = 0.0f;
      this.infoButton.vLocation.X = this.width;
      this.infoButton.vLocation.X += this.infoButton.GetSize().X * 0.5f;
      this.width += this.infoButton.GetSize().X;
      this.width += uiScaleHelper.DefaultBuffer.X;
      this.checkbox.location.X = this.width;
      this.checkbox.location.X += this.checkbox.GetSize().X - this.checkbox.GetBoxSize().X * 0.5f;
    }

    public void SetPara(float maxWidth)
    {
      this.explanation = new SimpleTextHandler("Estimated values tries to estimate your running costs per day, such as staff wages and animal food.", maxWidth - this.width, _Scale: this.BaseScale, AutoComplete: true);
      this.explanation.SetAllColours(ColourData.Z_Cream);
      this.explanation.Location.X = this.width;
      this.explanation.Location.Y -= this.explanation.GetHeightOfParagraph() * 0.5f;
    }

    public void SetTick(bool ticked) => this.checkbox.ForceSetTickStatus(ticked);

    public bool GetIsTicked() => this.checkbox.IsTicked;

    public bool UpdateCheckBoxAndInfo(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.infoButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
        this.ShowExplanation = !this.ShowExplanation;
      return !this.ShowExplanation && this.checkbox.UpdateLabelledCheckbox(player, offset, DeltaTime);
    }

    public void DrawCheckBoxAndInfo(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.ShowExplanation)
        this.explanation.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      else
        this.checkbox.DrawLabelledCheckbox(spriteBatch, offset);
      this.infoButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}

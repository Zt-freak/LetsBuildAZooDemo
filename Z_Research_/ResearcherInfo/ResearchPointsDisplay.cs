// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.ResearcherInfo.ResearchPointsDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.Elements.Research;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Research_.ResearcherInfo
{
  internal class ResearchPointsDisplay
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private TopBarResearch topBarResearch;
    private BigNumberInFrame bigNumber;

    public ResearchPointsDisplay(Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, addHeaderText: "Research", _BaseScale: BaseScale, frameType: PanelFrameType.Black);
      Vector2 zero = Vector2.Zero;
      this.topBarResearch = new TopBarResearch(BaseScale, uiScaleHelper.ScaleY(30f), player, true);
      Vector2 FullPanelSize = uiScaleHelper.ScaleVector2(new Vector2(8f, 15f));
      this.topBarResearch.location = FullPanelSize;
      this.topBarResearch.location += this.topBarResearch.GetSize() * 0.5f;
      FullPanelSize.X += this.topBarResearch.GetSize().X;
      FullPanelSize.X += uiScaleHelper.ScaleX(20f);
      this.bigNumber = new BigNumberInFrame(BaseScale, "999", frameColor: CustomerFrameColors.BlueWithLighterBlueBorder);
      this.bigNumber.location = this.topBarResearch.location;
      this.bigNumber.location.X += this.topBarResearch.GetSize().X * 0.5f + defaultBuffer.X;
      this.bigNumber.location.X += this.bigNumber.GetSize().X * 0.5f;
      FullPanelSize.X = uiScaleHelper.ScaleX(210f);
      FullPanelSize.Y += this.topBarResearch.GetSize().Y;
      FullPanelSize.Y += defaultBuffer.Y;
      this.bigBrownPanel.Finalize(FullPanelSize);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.topBarResearch.location += frameOffsetFromTop;
      this.bigNumber.location += frameOffsetFromTop;
      this.bigBrownPanel.SetNewHeading("");
      this.SetData(player);
      this.topBarResearch.SetToCropWhenAboveThis(this.bigBrownPanel.GetFrameOffsetFromTop().Y);
    }

    public Vector2 GetSize() => this.bigBrownPanel.vScale;

    public void OnTryingToSpendResearchPoints(bool CanAfford)
    {
      if (CanAfford)
        return;
      this.topBarResearch.DoRedFlash();
    }

    private void SetData(Player player) => this.bigNumber.SetText(player.unlocks.ResearchPoints.ToString());

    public void UpdateResearchPointsDisplay(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      offset -= this.bigBrownPanel.InternalOffset;
      this.topBarResearch.UpdateTopBarResearch(player, DeltaTime, offset);
      this.SetData(player);
    }

    public void DrawResearchPointsDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset -= this.bigBrownPanel.InternalOffset;
      this.topBarResearch.PreDrawTopBarRating(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.topBarResearch.DrawTopBarResearch(offset, spriteBatch);
      this.bigNumber.DrawBigNumberInFrame(offset, spriteBatch);
    }
  }
}

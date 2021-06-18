// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.LoansFrames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class LoansFrames
  {
    private CharacterTextBox speechBox;
    private LoansPaper paper;
    private Vector2 size;

    public LoansFrames(
      float BaseScale,
      FeatureUnlockDisplayType displayType,
      FeatureUnlockSpeechPack speechPack = null)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      float OverrideWidth_Scaled = uiScaleHelper.ScaleX(500f);
      if (displayType == FeatureUnlockDisplayType.Loans)
      {
        this.paper = new LoansPaper(BaseScale);
        OverrideWidth_Scaled = this.paper.GetSize().X;
        speechPack = new FeatureUnlockSpeechPack(FeatureUnlockDisplayData.GetTextForSpeech(displayType), AnimalType.SpecialEvent_Banker);
      }
      this.speechBox = new CharacterTextBox(speechPack.textToSay, speechPack.person, BaseScale, OverrideWidth_Scaled: OverrideWidth_Scaled);
      this.speechBox.Location.Y += this.speechBox.GetSize().Y * 0.5f;
      this.size.Y += this.speechBox.GetSize().Y;
      if (this.paper != null)
      {
        this.size.Y += defaultBuffer.Y;
        this.paper.location.Y = this.size.Y;
        this.paper.location.Y += this.paper.GetSize().Y * 0.5f;
        this.size.Y += this.paper.GetSize().Y;
      }
      this.size.X = this.speechBox.GetSize().X;
      Vector2 vector2 = -this.size * 0.5f;
      this.speechBox.Location.Y += vector2.Y;
      if (this.paper == null)
        return;
      this.paper.location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateLoansFrames()
    {
    }

    public void DrawLoansFrames(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.speechBox.DrawCharacterTextBox(offset, spriteBatch);
      if (this.paper == null)
        return;
      this.paper.DrawLoansPaper(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.FeatureUnlockFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class FeatureUnlockFrame
  {
    public Vector2 location;
    private HorizonHeraldPaper news;
    private LoansFrames loan_orSpeech;
    private MoralityPaper moralityPaper;

    public FeatureUnlockFrame(
      float BaseScale,
      FeatureUnlockDisplayType type,
      FeatureUnlockSpeechPack speechPack = null)
    {
      switch (type)
      {
        case FeatureUnlockDisplayType.ResearchHubUnlocked:
        case FeatureUnlockDisplayType.LandExpansion:
        case FeatureUnlockDisplayType.CRIPSRUnlocked:
          this.news = new HorizonHeraldPaper(BaseScale, type);
          break;
        case FeatureUnlockDisplayType.Loans:
        case FeatureUnlockDisplayType.Speech:
          this.loan_orSpeech = new LoansFrames(BaseScale, type, speechPack);
          break;
        case FeatureUnlockDisplayType.MoralityUsed:
        case FeatureUnlockDisplayType.VIPIntro:
          this.moralityPaper = new MoralityPaper(BaseScale, type);
          break;
      }
    }

    public Vector2 GetSize()
    {
      if (this.news != null)
        return this.news.GetSize();
      if (this.loan_orSpeech != null)
        return this.loan_orSpeech.GetSize();
      return this.moralityPaper != null ? this.moralityPaper.GetSize() : Vector2.Zero;
    }

    public void UpdateFeatureUnlockFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.news != null)
        this.news.UpdateFeatureNewsFrame(player, DeltaTime, offset);
      else if (this.loan_orSpeech != null)
      {
        this.loan_orSpeech.UpdateLoansFrames();
      }
      else
      {
        if (this.moralityPaper == null)
          return;
        this.moralityPaper.UpdateMoralityPaper();
      }
    }

    public void DrawFeatureUnlockFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.news != null)
        this.news.DrawFeatureNewsFrame(offset, spriteBatch);
      else if (this.loan_orSpeech != null)
      {
        this.loan_orSpeech.DrawLoansFrames(offset, spriteBatch);
      }
      else
      {
        if (this.moralityPaper == null)
          return;
        this.moralityPaper.DrawMoralityPaper(offset, spriteBatch);
      }
    }
  }
}

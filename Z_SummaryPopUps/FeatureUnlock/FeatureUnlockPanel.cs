// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.FeatureUnlockPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class FeatureUnlockPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private FeatureUnlockFrame frame;
    private LerpHandler_Float lerper;

    public FeatureUnlockPanel(
      float BaseScale,
      FeatureUnlockDisplayType unlockType,
      FeatureUnlockSpeechPack speechPack = null)
    {
      string addHeaderText = FeatureUnlockDisplayData.GetPanelHeaderForThis(unlockType);
      if (speechPack != null && !string.IsNullOrEmpty(speechPack.panelHeader))
        addHeaderText = speechPack.panelHeader;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, addHeaderText, BaseScale);
      this.frame = new FeatureUnlockFrame(BaseScale, unlockType, speechPack);
      this.bigBrownPanel.Finalize(this.frame.GetSize());
      this.lerper = new LerpHandler_Float();
      this.LerpIn_Top();
    }

    public void LerpIn_Top() => this.lerper.SetLerp(true, 0.0f, 1f, 3f);

    public void LerpOff_Top() => this.lerper.SetLerp(false, 1f, 0.0f, 3f);

    public bool IsOffScreen() => (double) this.lerper.Value == 0.0;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      offset.Y += (float) ((1.0 - (double) this.lerper.Value) * -768.0);
      return (double) this.lerper.Value != 0.0 && this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateFeatureUnlockPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      offset.Y += (float) ((1.0 - (double) this.lerper.Value) * -768.0);
      this.lerper.UpdateLerpHandler(DeltaTime);
      bool flag = false;
      if ((double) this.lerper.Value == 1.0)
      {
        this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
        flag = this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
        this.frame.UpdateFeatureUnlockFrame(player, DeltaTime, offset);
      }
      return flag;
    }

    public void DrawFeatureUnlockPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += (float) ((1.0 - (double) this.lerper.Value) * -768.0);
      if ((double) this.lerper.Value == 0.0)
        return;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawFeatureUnlockFrame(offset, spriteBatch);
    }
  }
}

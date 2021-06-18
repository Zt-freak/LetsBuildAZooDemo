// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.FeatureUnlockManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock
{
  internal class FeatureUnlockManager
  {
    private FeatureUnlockPanel panel;
    private FeatureUnlockDisplayType refDisplayType;
    private bool IsExiting;

    public FeatureUnlockManager(
      FeatureUnlockDisplayType featureunllocktype,
      FeatureUnlockSpeechPack speechPack = null)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.refDisplayType = featureunllocktype;
      this.panel = new FeatureUnlockPanel(baseScaleForUi, this.refDisplayType, speechPack);
      this.panel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.panel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateFeatureUnlockManager(Player player, float DeltaTime)
    {
      if (this.panel.UpdateFeatureUnlockPanel(player, DeltaTime, Vector2.Zero))
      {
        this.panel.LerpOff_Top();
        this.IsExiting = true;
      }
      return this.IsExiting && this.panel.IsOffScreen();
    }

    public void DrawFeatureUnlockManager() => this.panel.DrawFeatureUnlockPanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}

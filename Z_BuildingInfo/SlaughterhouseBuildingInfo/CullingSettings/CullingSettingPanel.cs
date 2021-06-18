// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.CullingSettingPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings
{
  internal class CullingSettingPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private CullRowsHolderFrame frame;

    public CullingSettingPanel(Player player, float BaseScale)
    {
      Vector2 vector2 = new UIScaleHelper(BaseScale).DefaultBuffer * 0.5f;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Culling", BaseScale);
      this.frame = new CullRowsHolderFrame(player, BaseScale);
      this.bigBrownPanel.Finalize(this.frame.GetSize());
    }

    public void SetIsActive(bool isActive) => this.bigBrownPanel.Active = isActive;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateCullingSettingPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out AnimalType editThisAnimal)
    {
      offset += this.location;
      editThisAnimal = AnimalType.None;
      if (!this.bigBrownPanel.Active)
        return false;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      editThisAnimal = this.frame.UpdateCullRowsHolderFrame(player, DeltaTime, offset);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawCullingSettingPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawCullRowsHolderFrame(offset, spriteBatch);
      this.bigBrownPanel.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}

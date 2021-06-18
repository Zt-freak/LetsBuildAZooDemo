// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup.EditCullingPopup
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup
{
  internal class EditCullingPopup
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private EditCullingFrame frame;

    public EditCullingPopup(AnimalType animalType, float BaseScale)
    {
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Edit", BaseScale);
      this.frame = new EditCullingFrame(animalType, BaseScale);
      this.bigBrownPanel.Finalize(this.frame.GetSize());
    }

    public bool UpdateEditCullingPopup(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.frame.UpdateEditCullingFrame(player, DeltaTime, offset);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawEditCullingPopup(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawEditCullingFrame(offset, spriteBatch);
    }
  }
}

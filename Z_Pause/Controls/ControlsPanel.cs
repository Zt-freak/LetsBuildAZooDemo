// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.ControlsPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Pause.Controls
{
  internal class ControlsPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private ControlsFrame frame;

    public ControlsPanel(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.frame = new ControlsFrame(baseScaleForUi, player);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Hot Keys", baseScaleForUi);
      this.bigBrownPanel.Finalize(this.frame.GetSize());
    }

    public bool UpdateControlsPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.frame.UpdateControlsFrame(player, DeltaTime, offset);
      return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawControlsPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawControlsFrame(offset, spriteBatch);
    }
  }
}

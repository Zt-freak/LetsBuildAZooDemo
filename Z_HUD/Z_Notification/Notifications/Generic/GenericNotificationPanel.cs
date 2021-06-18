// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Generic.GenericNotificationPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Generic
{
  internal class GenericNotificationPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private GenericNotificationInfoFrame infoFrame;

    public GenericNotificationPanel(
      string Header,
      string BodyParagraph,
      string textButtonText,
      float BaseScale)
    {
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, Header, BaseScale);
      this.infoFrame = new GenericNotificationInfoFrame(BodyParagraph, textButtonText, BaseScale);
      this.bigBrownPanel.Finalize(this.infoFrame.GetSize());
      this.bigBrownPanel.GetFrameOffsetFromTop();
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateGenericNotificationPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool TrackThis)
    {
      offset += this.location;
      TrackThis = false;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return true;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      if (!this.infoFrame.UpdateGenericNotificationInfoFrame(player, DeltaTime, offset))
        return false;
      TrackThis = true;
      return true;
    }

    public void DrawGenericNotificationPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.infoFrame.DrawGenericNotificationInfoFrame(offset, spriteBatch);
    }
  }
}

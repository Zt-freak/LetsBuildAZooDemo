// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.AvatarViewer.AvatarViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.AvatarViewer
{
  internal class AvatarViewManager
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private AvatarThoughts avatarThoughts;
    private AvatarAppearanceSelection avatarAppearance;
    private AvatarDirectControlSelect directControl;

    public AvatarViewManager(Player player, WalkingPerson walkingPerson, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Avatar", BaseScale);
      Vector2 zero = Vector2.Zero;
      this.avatarAppearance = new AvatarAppearanceSelection(player, walkingPerson, BaseScale);
      this.avatarThoughts = new AvatarThoughts(player, BaseScale, this.avatarAppearance.GetSize().X);
      if (Z_DebugFlags.AllowAvatarDirectControl)
        this.directControl = new AvatarDirectControlSelect(BaseScale);
      this.avatarThoughts.location += this.avatarThoughts.GetSize() * 0.5f;
      zero.Y += this.avatarThoughts.GetSize().Y;
      zero.Y += uiScaleHelper.DefaultBuffer.Y;
      this.avatarAppearance.location = zero;
      this.avatarAppearance.location += this.avatarAppearance.GetSize() * 0.5f;
      zero.Y += this.avatarAppearance.GetSize().Y;
      if (this.directControl != null)
      {
        zero.Y += uiScaleHelper.DefaultBuffer.Y;
        this.directControl.location.Y = zero.Y;
        this.directControl.location.Y += this.directControl.GetSize().Y * 0.5f;
        zero.Y += this.directControl.GetSize().Y;
      }
      zero.X = Math.Max(this.avatarAppearance.GetSize().X, this.avatarThoughts.GetSize().X);
      this.bigBrownPanel.Finalize(zero);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.avatarThoughts.location += frameOffsetFromTop;
      this.avatarAppearance.location += frameOffsetFromTop;
      if (this.directControl == null)
        return;
      this.directControl.location.Y += frameOffsetFromTop.Y;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateAvatarViewManager(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool controlAvatar)
    {
      controlAvatar = false;
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      this.avatarThoughts.UpdateAvatarThoughts();
      if (this.avatarAppearance.UpdateAvatarAppearanceSelection(player, DeltaTime, offset))
        this.avatarThoughts.RefreshImage(player);
      if (this.directControl == null || !this.directControl.UpdateAvatarDirectControlSelect(player, DeltaTime, offset))
        return this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
      controlAvatar = true;
      return true;
    }

    public void DrawAvatarViewManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.avatarThoughts.DrawAvatarThoughts(offset, spriteBatch);
      this.avatarAppearance.DrawAvatarAppearanceSelection(offset, spriteBatch);
      if (this.directControl == null)
        return;
      this.directControl.DrawAvatarDirectControlSelect(offset, spriteBatch);
    }
  }
}

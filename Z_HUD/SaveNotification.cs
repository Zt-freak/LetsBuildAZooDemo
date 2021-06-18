// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.SaveNotification
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Save_Notification;

namespace TinyZoo.Z_HUD
{
  internal class SaveNotification
  {
    private BigBrownPanel BigFrame;
    private float BaseScale;
    private DescAndBar descandbar;
    private bool HasRendered;
    private bool HasSaved;
    private Vector2 Location;
    private BlackOut blackout;

    public SaveNotification(Player player)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.BigFrame = new BigBrownPanel(Vector2.Zero, true, "Saving", this.BaseScale);
      this.descandbar = new DescAndBar(player, this.BaseScale);
      this.Location = new Vector2(512f, 300f);
      this.BigFrame.Finalize(this.descandbar.GetSize());
      this.BigFrame.BlockCloseButton = true;
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(0.5f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return this.BigFrame.CheckMouseOver(player, offset);
    }

    public bool UpdateSaveNotification(float DeltaTime, Player player, Vector2 offset)
    {
      if (this.HasRendered && !this.HasSaved)
      {
        player.SaveThisPlayer(DelayUntilNextFrame: false, IsEndOfDay: true);
        this.HasSaved = true;
        this.BigFrame.BlockCloseButton = false;
        OverWorldManager.z_daynightmanager.StartNewDay(player);
      }
      return this.HasSaved && this.BigFrame.UpdatePanelCloseButton(player, DeltaTime, offset + this.Location);
    }

    public void DrawSaveNotification()
    {
      this.HasRendered = true;
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.BigFrame.DrawBigBrownPanel(this.Location, AssetContainer.pointspritebatchTop05);
      this.descandbar.DrawDescAndBar(this.Location, AssetContainer.pointspritebatchTop05);
    }
  }
}

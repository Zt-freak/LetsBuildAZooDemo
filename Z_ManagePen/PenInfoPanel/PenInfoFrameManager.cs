// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.PenInfoPanel.PenInfoFrameManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.PenInfoPanel.Damage;

namespace TinyZoo.Z_ManagePen.PenInfoPanel
{
  internal class PenInfoFrameManager
  {
    private BigBrownPanel BigFrame;
    private DamagePanel damagepanel;
    public Vector2 Location;
    private LerpHandler_Float lerper;
    private bool Exiting;
    private float BaseScale;
    private PrisonZone Ref_prisonzone;

    public PenInfoFrameManager(PrisonZone prisonzone, Player player)
    {
      this.Ref_prisonzone = prisonzone;
      this.Exiting = false;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.BigFrame = new BigBrownPanel(Vector2.Zero, true, "Enclosure", this.BaseScale);
      this.damagepanel = new DamagePanel(this.BaseScale, prisonzone, 400f, player);
      this.BigFrame.Finalize(this.damagepanel.GetSize());
    }

    public bool CheckMouseOver(Player player, Vector2 Offset)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      return this.BigFrame.CheckCollision(player.inputmap.PointerLocation, Offset);
    }

    public bool UpdatePenInfoManager(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      out bool SkipMainBar)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      SkipMainBar = false;
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      SkipMainBar = this.BigFrame.CheckCollision(player.player.touchinput.ReleaseTapArray[0], Offset);
      if ((double) this.lerper.Value != 0.0 || this.Exiting)
        return this.Exiting && (double) this.lerper.Value == 1.0;
      if (this.BigFrame.UpdatePanelCloseButton(player, DeltaTime, Offset))
      {
        this.Exiting = true;
        this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
      }
      this.BigFrame.UpdatePanelCloseButton(player, DeltaTime, Offset);
      bool RemakeThis;
      this.damagepanel.UpdateDamagePanel(DeltaTime, player, Offset, out RemakeThis);
      if (RemakeThis)
        this.damagepanel = new DamagePanel(this.BaseScale, this.Ref_prisonzone, 400f, player);
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0)
        SkipMainBar = this.BigFrame.CheckCollision(player.player.touchinput.MultiTouchTouchLocations[0], Offset);
      else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
        SkipMainBar = this.BigFrame.CheckCollision(player.player.touchinput.ReleaseTapArray[0], Offset);
      return false;
    }

    public void DrawPenInfoManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset.X += this.Location.X;
      Offset.Y += this.Location.Y;
      Offset.X += this.lerper.Value * 400f;
      this.BigFrame.DrawBigBrownPanel(Offset, spritebatch);
      this.damagepanel.DrawDamagePanel(Offset, spritebatch);
    }
  }
}

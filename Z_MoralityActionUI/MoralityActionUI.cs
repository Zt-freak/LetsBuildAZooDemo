// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralityActionUI.MoralityActionUI
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_MoralityActionUI
{
  internal class MoralityActionUI
  {
    private MoralityActionPanel good;
    private MoralityActionPanel evil;
    private UIScaleHelper uiScale;
    private BigBrownPanel panel;
    private Vector2 panelscale;
    private static bool lockeddd;

    public MoralityActionUI(float basescale)
    {
      this.uiScale = new UIScaleHelper(basescale);
      this.good = new MoralityActionPanel(true, MoralityActionUI.lockeddd, "Break up the fight", "INTERVENE", basescale);
      this.evil = new MoralityActionPanel(false, !MoralityActionUI.lockeddd, "Place bets on animals", "BET", basescale);
      float num = this.uiScale.ScaleX(10f);
      this.panelscale.X = this.good.GetSize().X + this.evil.GetSize().X + num;
      this.panelscale.Y = Math.Max(this.good.GetSize().Y, this.evil.GetSize().Y);
      this.panel = new BigBrownPanel(this.panelscale, true, "Actions", basescale);
      this.good.location = Sengine.ReferenceScreenRes;
      this.good.location -= this.uiScale.ScaleVector2(new Vector2(350f, 100f));
      this.evil.location = this.good.location;
      this.evil.location.X += (float) ((double) num + 0.5 * (double) this.good.GetSize().X + 0.5 * (double) this.evil.GetSize().X);
      this.panel.location = (this.good.location + this.evil.location) / 2f;
      this.panel.Finalize(this.panelscale);
      MoralityActionUI.lockeddd = !MoralityActionUI.lockeddd;
    }

    public bool UpdateMoralityActionUIManager(Player player, float DeltaTime) => (0 | (this.good.UpdateMoralityActionPanel(player, Vector2.Zero, DeltaTime) ? 1 : 0) | (this.evil.UpdateMoralityActionPanel(player, Vector2.Zero, DeltaTime) ? 1 : 0) | (this.panel.UpdatePanelCloseButton(player, DeltaTime, Vector2.Zero) ? 1 : 0)) != 0;

    public void DrawMoralityActionUIManager(Vector2 offset, SpriteBatch spritebatch)
    {
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.good.DrawMoralityActionPanel(offset, spritebatch);
      this.evil.DrawMoralityActionPanel(offset, spritebatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.JobButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.IntakeStuff;

namespace TinyZoo.OverWorld.Intake
{
  internal class JobButton : GameObject
  {
    private GenericBox box;
    public Vector2 Location;
    private IntakeInfo REF_intakeinfo;
    private JobButtonInfo info;
    private GameObjectNineSlice Frame;
    private Vector2 VSCALE;
    private GameObjectNineSlice MouseOverFrame;
    private bool MouseOver;

    public JobButton(IntakeInfo intakeinfo)
    {
      this.REF_intakeinfo = intakeinfo;
      this.box = new GenericBox(new Vector2(150f, 55f));
      this.info = new JobButtonInfo(intakeinfo);
      this.VSCALE = new Vector2(150f, 55f);
      this.Frame = new GameObjectNineSlice(new Rectangle(917, 372, 21, 21), 7);
      this.MouseOverFrame = new GameObjectNineSlice(new Rectangle(917, 394, 21, 21), 7);
      this.Frame.scale = 3f;
      this.MouseOverFrame.scale = 3f;
    }

    public bool UpdateJobButton(Vector2 Offset, Player player)
    {
      this.MouseOver = this.box.CheckForMouseOver(player, this.Location + Offset);
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && this.box.CheckForTaps(player, this.Location + Offset);
    }

    public void DrawJobButton(Vector2 Offset, bool IsSelected, SpriteBatch spritebatch)
    {
      if (IsSelected)
      {
        Offset.X += 2f;
        Offset.Y -= 2f;
        this.box.DrawGenericBox(this.Location + Offset, spritebatch);
      }
      if (this.MouseOver)
      {
        this.MouseOverFrame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset + this.Location, this.VSCALE * Sengine.ScreenRatioUpwardsMultiplier);
        this.MouseOver = false;
      }
      else
        this.Frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset + this.Location, this.VSCALE * Sengine.ScreenRatioUpwardsMultiplier);
      this.info.DrawJobButtonInfo(Offset + this.Location, spritebatch);
      int num = IsSelected ? 1 : 0;
    }
  }
}

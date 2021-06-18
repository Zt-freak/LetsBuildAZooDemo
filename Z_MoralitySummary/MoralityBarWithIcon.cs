// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralityBarWithIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralityBarWithIcon
  {
    private GoodEvilIcon icon;
    private SatisfactionBar bar;
    private Vector2 pad;
    public Vector2 location;
    private static float iconScaleMult = 1f;
    private CustomerFrame frame;
    private UIScaleHelper scalehelper;
    private float basescale;
    private Vector2 framescale;

    public MoralityBarWithIcon(bool isGoodNotEvil, float basescale_ = 1f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = 0.5f * this.scalehelper.DefaultBuffer;
      this.icon = new GoodEvilIcon(isGoodNotEvil);
      this.bar = new SatisfactionBar(0.0f, this.basescale);
      this.bar.SetBarColours(isGoodNotEvil ? ColourData.GoodYellow : ColourData.EvilPurple);
      this.icon.SetDrawOriginToCentre();
      this.icon.scale = MoralityBarWithIcon.iconScaleMult * this.basescale;
      this.framescale = Vector2.Zero;
      this.framescale.X = this.icon.GetSize().X + this.bar.GetSize().X + this.pad.X;
      this.framescale.Y = Math.Max(this.icon.GetSize().Y, this.bar.GetSize().Y);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.icon.vLocation.X = (float) (-0.5 * (double) this.framescale.X + 0.5 * (double) this.icon.GetSize().X);
      this.bar.vLocation.X = (float) ((double) this.icon.vLocation.X + 0.5 * (double) this.icon.GetSize().X + 0.5 * (double) this.bar.GetSize().X) + this.pad.X;
    }

    public void ChangeGoodness(bool isGoodNotEvil)
    {
      this.icon.SetAlignment(isGoodNotEvil);
      this.bar.SetBarColours(isGoodNotEvil ? ColourData.GoodYellow : ColourData.EvilPurple);
    }

    public void SetFullness(float zeroToOne) => this.bar.SetFullness(zeroToOne);

    public void DrawMoralityBarWithIcon(Vector2 offset) => this.DrawMoralityBarWithIcon(offset, AssetContainer.pointspritebatch03);

    public void DrawMoralityBarWithIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawGoodEvilIcon(offset, spriteBatch);
      this.bar.DrawSatisfactionBar(offset, spriteBatch);
    }

    public Vector2 GetSize() => this.framescale;
  }
}

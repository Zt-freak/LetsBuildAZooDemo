// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.DecoCondensedPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HeatMaps
{
  internal class DecoCondensedPanel
  {
    private string DecoVal;
    private int Efficiency;
    public Vector2 location;
    private CustomerFrame frame;
    private UIScaleHelper scalehelper;
    private float basescale;
    private Vector2 framescale;
    private ZGenericText decovaltext;
    private ZGenericText efficiencytext;

    public DecoCondensedPanel(int XLoc, int YLoc, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.DecoVal = string.Concat((object) Math.Round((double) DecoCalculator.GetThisValue(XLoc, YLoc), 1));
      this.Efficiency = (int) Math.Round((double) DecoCalculator.GetThisEfficiency(XLoc, YLoc) * 100.0, 1);
      this.decovaltext = new ZGenericText("Deco: " + this.DecoVal, this.basescale, _UseOnePointFiveFont: true);
      this.efficiencytext = new ZGenericText(this.Efficiency.ToString() + "%", this.basescale, _UseOnePointFiveFont: true);
      this.framescale = 2f * defaultBuffer;
      this.framescale += this.efficiencytext.GetSize();
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.efficiencytext.vLocation = -0.5f * this.framescale + defaultBuffer + 0.5f * this.efficiencytext.GetSize();
      this.efficiencytext.vLocation.X = 0.0f;
    }

    public Vector2 GetSize() => this.framescale;

    public void UpdateDecoDisplay()
    {
    }

    public void DrawDecoDisplay(SpriteBatch spritebatch, Vector2 offset, float alpha = 1f)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.frame.SetAlphaed(alpha);
      this.efficiencytext.DrawZGenericText(offset, spritebatch);
      this.efficiencytext.SetAlpha(alpha);
    }
  }
}

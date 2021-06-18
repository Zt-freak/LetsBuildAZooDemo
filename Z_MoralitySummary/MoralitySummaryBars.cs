// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralitySummaryBars
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralitySummaryBars
  {
    private MoralityBarWithIcon goodbar;
    private MoralityBarWithIcon evilbar;
    public Vector2 location;
    private bool hasGood;
    private bool hasEvil;
    private float pad;
    private float scale;

    public MoralitySummaryBars(bool hasGoodBar, bool hasEvilBar, float basescale = 1f)
    {
      this.scale = basescale;
      this.pad = 20f * Sengine.ScreenRatioUpwardsMultiplier.Y * basescale;
      this.hasGood = hasGoodBar;
      this.hasEvil = hasEvilBar;
      if (this.hasGood)
        this.goodbar = new MoralityBarWithIcon(true, basescale);
      if (this.hasEvil)
        this.evilbar = new MoralityBarWithIcon(false, basescale);
      if (this.hasGood && this.hasEvil)
      {
        this.goodbar.location.Y = -0.5f * this.pad;
        this.evilbar.location.Y = 0.5f * this.pad;
      }
      else
      {
        if (!this.hasGood && !this.hasEvil)
          throw new Exception("no bar here?");
        if (this.hasGood)
          return;
        int num = this.hasEvil ? 1 : 0;
      }
    }

    public void SetScore(float score, float scoreMax)
    {
      if ((double) scoreMax <= 0.0)
        return;
      ((double) score >= 0.0 ? this.goodbar : this.evilbar).SetFullness(Math.Abs(score) / scoreMax);
    }

    public void DrawMoralitySummaryBars(Vector2 offset, SpriteBatch spriteBatch)
    {
      if (this.hasGood)
        this.goodbar.DrawMoralityBarWithIcon(this.location + offset, spriteBatch);
      if (!this.hasEvil)
        return;
      this.evilbar.DrawMoralityBarWithIcon(this.location + offset, spriteBatch);
    }

    public Vector2 GetSize()
    {
      Vector2 vector2 = new Vector2();
      if (this.hasGood && this.hasEvil)
      {
        vector2.X = Math.Max(this.goodbar.GetSize().X, this.evilbar.GetSize().X);
        vector2.Y = this.goodbar.GetSize().Y + this.pad + this.evilbar.GetSize().Y;
      }
      else
      {
        if (!this.hasGood && !this.hasEvil)
          throw new Exception("no bar");
        if (this.hasGood)
          vector2 = this.goodbar.GetSize();
        else if (this.hasEvil)
          vector2 = this.evilbar.GetSize();
      }
      return vector2;
    }
  }
}

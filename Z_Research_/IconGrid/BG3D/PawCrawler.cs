// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.BG3D.PawCrawler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_Research_.IconGrid.BG3D
{
  internal class PawCrawler
  {
    private List<Paw> paws;
    private float Mult;
    private float Fullheight;
    private float FootDelay;
    private int ThisStep;
    private float MoveSpeed;
    private float MinY;
    private bool WillWalk;

    public PawCrawler(bool _WillWalk)
    {
      this.WillWalk = _WillWalk;
      this.paws = new List<Paw>();
      float Z_Distance = (float) TinyZoo.Game1.Rnd.Next(10, 90) * 0.01f;
      this.Mult = (float) (1.0 / (1.0 - (double) Z_Distance));
      this.MoveSpeed = (float) TinyZoo.Game1.Rnd.Next(30, 100);
      this.MoveSpeed *= 0.01f;
      for (int index = 0; index < 10; ++index)
      {
        this.paws.Add(new Paw());
        this.paws[index].SetZDepth(Z_Distance);
        if (index == 0 || !this.WillWalk)
        {
          this.paws[index].vLocation = new Vector2((float) TinyZoo.Game1.Rnd.Next(-2000, 2000), (float) TinyZoo.Game1.Rnd.Next(-2000, 2000));
        }
        else
        {
          this.paws[index].vLocation = this.paws[0].vLocation;
          if (index % 2 == 1)
            this.paws[index].vLocation.X += 20f * this.paws[index].scale;
          this.paws[index].vLocation.Y += (float) (index * 10) * Sengine.ScreenRatioUpwardsMultiplier.Y * this.paws[index].scale;
        }
      }
      for (int index = 0; index < 10; ++index)
      {
        Paw paw = this.paws[index];
        paw.vLocation = paw.vLocation * this.Mult;
        if (this.WillWalk)
          this.paws[index].SetAlpha(0.0f);
      }
      this.Fullheight = 100f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.paws[0].scale * this.Mult;
      this.FootDelay = this.MoveSpeed;
      if (this.WillWalk)
      {
        this.paws[0].SetAlpha(false, 2f, 0.5f, 0.0f);
        this.paws[0].SetColourDelay(2f);
        this.paws[1].SetAlpha(false, 2f, 0.5f, 0.0f);
        this.paws[1].SetColourDelay(2f - this.FootDelay);
        this.paws[2].SetAlpha(false, 2f, 0.5f, 0.0f);
        this.paws[2].SetColourDelay((float) (2.0 - (double) this.FootDelay * 2.0));
        this.paws[3].SetAlpha(false, 2f, 0.5f, 0.0f);
        this.paws[3].SetColourDelay((float) (2.0 - (double) this.FootDelay * 3.0));
      }
      this.ThisStep = 9;
      this.MinY = 2000f * this.Mult;
    }

    public void UpdatePawCrawler(float DeltaTime)
    {
      if (!this.WillWalk)
      {
        for (int index = 0; index < this.paws.Count; ++index)
        {
          this.paws[index].AlphaCycle(this.MoveSpeed * 10f, 0.5f, -1f);
          this.paws[index].UpdateColours(DeltaTime);
        }
      }
      else
      {
        this.FootDelay -= DeltaTime;
        if ((double) this.FootDelay < 0.0)
        {
          this.FootDelay = this.MoveSpeed;
          this.paws[this.ThisStep].SetAlpha(false, 2f, 0.5f, 0.0f);
          this.paws[this.ThisStep].SetColourDelay(2f);
          this.paws[this.ThisStep].vLocation.Y -= this.Fullheight;
          --this.ThisStep;
          if (this.ThisStep < 0)
            this.ThisStep = 9;
          if ((double) this.paws[this.ThisStep].vLocation.Y < -(double) this.MinY)
          {
            for (int index = 0; index < this.paws.Count; ++index)
              this.paws[index].vLocation.Y += this.MinY * 2f;
          }
        }
        for (int index = 0; index < this.paws.Count; ++index)
          this.paws[index].UpdateColours(DeltaTime);
      }
    }

    public void DrawPawCrawler()
    {
      for (int index = 0; index < this.paws.Count; ++index)
      {
        if ((double) this.paws[index].fAlpha > 0.0)
          this.paws[index].PawDraw();
      }
    }
  }
}

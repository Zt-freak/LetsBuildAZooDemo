// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Panel.PieChart
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_TicketPrice.Panel
{
  internal class PieChart
  {
    public Vector2 location;
    private Rectangle hiResCircleRect;
    private Rectangle ringFrameRect;
    private GameObject ringFrame;
    private GameObject[] SliceA;
    private GameObject[] SliceB;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private List<PieChartLegendPair> legendRows;
    private Vector2 framescale;
    private bool isMoreThanHalf;

    public PieChart(
      string[] legendText = null,
      Vector3[] _colours = null,
      float BaseScale = 1f,
      float textSizeScaleMult = 1f)
    {
      this.uiscale = new UIScaleHelper(BaseScale);
      this.ringFrameRect = new Rectangle(334, 375, 32, 32);
      this.hiResCircleRect = new Rectangle(2, 426, 256, 256);
      float num1 = 2f;
      this.ringFrame = new GameObject();
      this.ringFrame.DrawRect = this.ringFrameRect;
      this.ringFrame.scale = num1 * BaseScale;
      this.ringFrame.SetDrawOriginToCentre();
      this.ringFrame.SetAllColours(ColourData.Z_Cream);
      Vector3[] vector3Array = new Vector3[2]
      {
        ColourData.FernRed,
        ColourData.ACDarkerBlue
      };
      if (_colours != null)
      {
        vector3Array[0] = _colours[0];
        vector3Array[1] = _colours[1];
      }
      this.SliceA = this.CreateHalves(vector3Array[0]);
      this.SliceB = this.CreateHalves(vector3Array[1]);
      Vector2 vector2 = num1 * this.uiscale.ScaleVector2(new Vector2((float) this.ringFrameRect.Width, (float) this.ringFrameRect.Height));
      this.framescale = vector2;
      this.framescale.X = this.uiscale.ScaleX(320f);
      float num2 = this.uiscale.ScaleY(AssetContainer.SpringFontX1AndHalf.MeasureString("arbitrary string").Y);
      this.framescale.Y += (float) (2.0 * (double) num2 + 1.0 * (double) this.uiscale.DefaultBuffer.Y);
      this.frame = new CustomerFrame(this.framescale, true, BaseScale);
      Vector2 zero = Vector2.Zero;
      zero.Y = -0.5f * this.framescale.Y;
      zero.Y += 0.5f * vector2.Y;
      this.ringFrame.vLocation.Y = zero.Y;
      foreach (BObject bobject in this.SliceA)
        bobject.vLocation.Y = zero.Y;
      foreach (BObject bobject in this.SliceB)
        bobject.vLocation.Y = zero.Y;
      zero.Y += 0.5f * vector2.Y + this.uiscale.DefaultBuffer.Y;
      this.legendRows = new List<PieChartLegendPair>();
      if (legendText == null || legendText.Length == 0)
        return;
      float num3 = zero.Y + 0.5f * num2;
      float num4 = num2;
      for (int index = 0; index < legendText.Length; ++index)
        this.legendRows.Add(new PieChartLegendPair(legendText[index], vector3Array[index], BaseScale * textSizeScaleMult)
        {
          location = new Vector2(0.0f, num3 + num4 * (float) index)
        });
    }

    public Vector2 GetSize() => this.framescale;

    private GameObject[] CreateHalves(Vector3 colour)
    {
      GameObject gameObject1 = new GameObject();
      gameObject1.DrawRect = new Rectangle(this.hiResCircleRect.X + this.hiResCircleRect.Width / 2, this.hiResCircleRect.Y, this.hiResCircleRect.Width / 2, this.hiResCircleRect.Height);
      gameObject1.scale = this.ringFrame.scale * 0.12f;
      gameObject1.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      gameObject1.SetAllColours(colour);
      GameObject gameObject2 = new GameObject();
      gameObject2.DrawRect = new Rectangle(this.hiResCircleRect.X, this.hiResCircleRect.Y, this.hiResCircleRect.Width / 2, this.hiResCircleRect.Height);
      gameObject2.scale = gameObject1.scale;
      gameObject2.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
      gameObject2.SetAllColours(colour);
      return new GameObject[2]{ gameObject1, gameObject2 };
    }

    public void SetValues(float[] percentages_0to1) => this.SetPercentage(percentages_0to1);

    private void SetPercentage(float[] percentages_0to1)
    {
      if ((double) percentages_0to1[0] <= 0.5)
      {
        this.isMoreThanHalf = false;
        this.SliceB[0].Rotation = (float) (3.14159274101257 * (double) percentages_0to1[0] * 2.0);
        this.SliceA[1].Rotation = 0.0f;
      }
      else
      {
        this.isMoreThanHalf = true;
        this.SliceA[1].Rotation = (float) (3.14159274101257 * (double) percentages_0to1[0] * 2.0);
        this.SliceB[0].Rotation = 0.0f;
      }
    }

    public float GetExtraTextHeight()
    {
      float num = 0.0f;
      if (this.legendRows.Count > 0)
        num = num + (this.legendRows[this.legendRows.Count - 1].location.Y - this.ringFrame.vLocation.Y) - this.GetPieChartHeight() * 0.5f + this.legendRows[0].GetHeight() * 0.5f;
      return num;
    }

    public float GetPieChartHeight() => (float) this.ringFrame.DrawRect.Height * this.ringFrame.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;

    public void DrawPieChart(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.SliceA.Length; ++index)
      {
        if (this.isMoreThanHalf)
        {
          this.SliceB[index].Draw(spriteBatch, AssetContainer.UISheet, offset);
          this.SliceA[index].Draw(spriteBatch, AssetContainer.UISheet, offset);
        }
        else
        {
          this.SliceA[index].Draw(spriteBatch, AssetContainer.UISheet, offset);
          this.SliceB[index].Draw(spriteBatch, AssetContainer.UISheet, offset);
        }
      }
      for (int index = 0; index < this.legendRows.Count; ++index)
        this.legendRows[index].DrawPieChartLegendPair(spriteBatch, offset);
      this.ringFrame.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}

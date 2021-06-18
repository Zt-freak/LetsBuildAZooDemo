// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.Cubes.EndOfWeek.IncomeCube
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents;

namespace TinyZoo.Z_WeekOver.V2.Cubes.Cubes.EndOfWeek
{
  internal class IncomeCube : BaseCube
  {
    private BarPair barpair;
    private TrendingArrow trendingarrow;
    private LerpHandler_Float Alphalerper;
    private IncomeCubeType incometype;
    private List<SubIncomePage> subincomepages;
    private LerpHandler_Float ValueLerper;
    private int ThisWeek;
    private int WeekBefore;
    private MoreButton morebutton;
    private int Page;

    public IncomeCube(float _BaseScale, Player player, IncomeCubeType _incometype)
      : base(_BaseScale, true, new Vector3(0.2f, 0.3f, 0.2f))
    {
      this.incometype = _incometype;
      bool HasData1;
      this.ThisWeek = Player.financialrecords.GetWeekSummaryValue(_incometype, false, out HasData1);
      bool HasData2;
      this.WeekBefore = Player.financialrecords.GetWeekSummaryValue(_incometype, true, out HasData2);
      float RFullness = 0.0f;
      float LFullness = 0.0f;
      if (this.ThisWeek > 0)
      {
        if (this.WeekBefore > 0)
        {
          LFullness = 0.5f;
          RFullness = MathHelper.Clamp(((float) this.ThisWeek / (float) this.WeekBefore - 1f) * 4f + 0.5f, 0.0f, 1f);
        }
        else
        {
          LFullness = 0.0f;
          RFullness = 0.5f;
        }
      }
      else if (this.WeekBefore > 0)
        LFullness = 0.5f;
      this.barpair = new BarPair(_BaseScale, LFullness, RFullness, Vector3.One);
      this.ValueLerper = new LerpHandler_Float();
      this.ValueLerper.SetLerp(true, 0.0f, 1f, 1f);
      if (HasData2)
        this.barpair.SetBarValueText("$" + (object) this.WeekBefore, true);
      else
        this.barpair.SetBarValueText("No Data", true);
      if (HasData1)
        this.barpair.SetBarValueText("$??");
      else
        this.barpair.SetBarValueText("No Data");
      this.barpair.RightBar.SetLerpScale(0.0f);
      this.trendingarrow = new TrendingArrow(_BaseScale);
      this.trendingarrow.SetLocationAt(PositionInFrame.TopRight);
      this.Alphalerper = new LerpHandler_Float();
      this.Alphalerper.SetLerp(true, 0.0f, 1f, 3f);
      switch (this.incometype)
      {
        case IncomeCubeType.Income:
          this.AddMiniHeading("Income", new Vector3(0.8392157f, 0.4313726f, 0.172549f));
          this.customerframe.SetColour(new Vector3(0.9098039f, 0.772549f, 0.4470588f));
          this.barpair = new BarPair(_BaseScale, LFullness, RFullness, new Vector3(0.9568627f, 0.9411765f, 0.8784314f));
          this.trendingarrow.SetColour(new Vector3(0.8392157f, 0.4313726f, 0.172549f));
          break;
        case IncomeCubeType.Profit:
          this.AddMiniHeading("Profit", new Vector3(0.2588235f, 0.4235294f, 0.3803922f));
          this.customerframe.SetColour(new Vector3(0.9568627f, 0.9411765f, 0.8784314f));
          this.barpair = new BarPair(_BaseScale, LFullness, RFullness, new Vector3(0.4235294f, 0.5490196f, 0.4235294f));
          this.trendingarrow.SetColour(new Vector3(0.2588235f, 0.4235294f, 0.3803922f));
          break;
        case IncomeCubeType.Expanditure:
          this.AddMiniHeading("Expenditure", new Vector3(0.6392157f, 0.3803922f, 0.345098f));
          this.customerframe.SetColour(new Vector3(0.9568627f, 0.9411765f, 0.8784314f));
          this.barpair = new BarPair(_BaseScale, LFullness, RFullness, new Vector3(0.7529412f, 0.4941176f, 0.4588235f));
          this.trendingarrow.SetColour(new Vector3(0.6392157f, 0.3803922f, 0.345098f));
          break;
      }
      this.subincomepages = new List<SubIncomePage>();
      this.morebutton = new MoreButton(this.BaseScale, true);
      this.morebutton.SetToBottomRightHandCorner(this.BaseScale);
      this.morebutton.location.Y -= 20f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.BaseScale;
      this.Page = 0;
    }

    public override bool LerpComplete(CurrentFinances currentfinances) => (double) this.ValueLerper.Value == 1.0;

    public override void UpdateBaseCube(float DeltaTime, Player player, Vector2 Offset)
    {
      base.UpdateBaseCube(DeltaTime, player, Offset);
      if ((double) this.lerperBaseCube.Value != 1.0)
        return;
      this.barpair.UpdateBarPair();
      this.Alphalerper.UpdateLerpHandler(DeltaTime);
      if (this.Alphalerper.IsComplete() && !this.ValueLerper.IsComplete())
      {
        Offset += this.Location;
        this.ValueLerper.UpdateLerpHandler(DeltaTime);
        this.barpair.SetBarValueText("$" + (object) Math.Round((double) this.ThisWeek * (double) this.ValueLerper.Value));
        this.barpair.RightBar.SetLerpScale(this.ValueLerper.Value);
      }
      if (!this.ValueLerper.IsComplete())
        return;
      Offset += this.Location;
      if (!this.morebutton.UpdateMoreButton(player, Offset, DeltaTime))
        return;
      ++this.Page;
      switch (this.incometype)
      {
      }
    }

    public override void DrawBaseCube(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      base.DrawBaseCube(Offset, spritebatch);
      if ((double) this.lerperBaseCube.Value != 1.0)
        return;
      this.barpair.DrawBarPair(Offset, spritebatch, this.Alphalerper.Value);
      this.trendingarrow.DrawTrendingArrow(spritebatch, Offset, this.Alphalerper.Value);
    }
  }
}

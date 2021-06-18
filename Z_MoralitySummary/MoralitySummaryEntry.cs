// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralitySummaryEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralitySummaryEntry
  {
    private static Vector2 REFERENCE_SCREEN_RES = new Vector2(Sengine.ReferenceScreenRes.X, Sengine.ReferenceScreenRes.Y);
    private MiniHeading miniheading;
    private MoralityType type;
    private CustomerFrame frame;
    public Vector2 location;
    private MoralityTypeIcon icon;
    private SimpleTextHandler texthandler;
    private MoralitySummaryBars bars;
    private MoralityScoreDisplay moralityScoreDisplay;
    private float barscale = 1f;
    private float moralityScore;
    private float moralityMax;
    private static Random rand = new Random();
    private Vector2 frameScale;
    private UIScaleHelper scalehelper;

    public MoralitySummaryEntry(Player player, MoralityType moralityType, float basescale)
    {
      this.scalehelper = new UIScaleHelper(basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.type = moralityType;
      this.moralityScore = MoralityData.GetMoralityScore(player, moralityType, out this.moralityMax);
      this.icon = new MoralityTypeIcon(moralityType, basescale);
      this.bars = new MoralitySummaryBars(true, true, this.barscale * basescale);
      this.bars.SetScore(this.moralityScore, this.moralityMax);
      float width_ = this.scalehelper.ScaleX(160f);
      if (this.type == MoralityType.Beta)
        width_ += this.bars.GetSize().X;
      this.texthandler = new SimpleTextHandler(MoralityData.GetDescription(moralityType), width_, _Scale: basescale);
      this.texthandler.AutoCompleteParagraph();
      this.texthandler.SetAllColours(ColourData.Z_Cream);
      this.miniheading = new MiniHeading(Vector2.Zero, MoralityData.GetHeading(moralityType), 1f, basescale);
      this.moralityScoreDisplay = new MoralityScoreDisplay(basescale);
      this.frameScale = 2f * defaultBuffer;
      this.frameScale.Y += this.miniheading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.frameScale.Y += (float) (0.5 * (double) this.icon.GetSize().Y + 0.5 * (double) this.moralityScoreDisplay.GetSize().Y);
      this.frameScale.X = this.scalehelper.ScaleX(340f) + 2f * defaultBuffer.X;
      this.frame = new CustomerFrame(this.frameScale, true, basescale);
      this.miniheading.SetTextPosition(this.frameScale);
      Vector2 vector2 = -0.5f * this.frameScale + defaultBuffer;
      vector2.Y += this.miniheading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.icon.vLocation = vector2 + 0.5f * this.icon.GetSize();
      vector2.X += this.icon.GetSize().X + defaultBuffer.X;
      if (this.type != MoralityType.Beta)
      {
        this.bars.location.Y = vector2.Y + 0.5f * this.icon.GetSize().Y;
        this.bars.location.X = vector2.X + 0.5f * this.bars.GetSize().X;
        vector2.X += this.bars.GetSize().X + defaultBuffer.X;
      }
      this.texthandler.Location.X = vector2.X;
      this.texthandler.Location.Y = vector2.Y;
      this.moralityScoreDisplay.SetScore(this.moralityScore);
      this.moralityScoreDisplay.location.X = (float) (0.5 * (double) this.frameScale.X - 0.5 * (double) this.moralityScoreDisplay.GetSize().X - 10.0 * (double) basescale);
      this.moralityScoreDisplay.location.Y = this.icon.vLocation.Y;
    }

    public float GetMoralityScore() => this.moralityScore;

    public void DrawMoralitySummaryEntry(Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, AssetContainer.pointspritebatch03);
      this.icon.DrawMoralityTypeIcon(offset);
      this.miniheading.DrawMiniHeading(offset, AssetContainer.pointspritebatch03);
      this.texthandler.DrawSimpleTextHandler(offset);
      if (this.type == MoralityType.Beta)
        return;
      this.bars.DrawMoralitySummaryBars(offset, AssetContainer.pointspritebatch03);
      this.moralityScoreDisplay.DrawMoralityScoreDisplay(offset);
    }

    public Vector2 GetSize() => this.frameScale;
  }
}

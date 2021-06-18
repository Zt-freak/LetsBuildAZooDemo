// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralitySummary.MoralitySummaryCategory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_MoralitySummary
{
  internal class MoralitySummaryCategory
  {
    private static Vector2 REFERENCE_SCREEN_RES = new Vector2(Sengine.ReferenceScreenRes.X, Sengine.ReferenceScreenRes.Y);
    private Vector2 currloc;
    private CustomerFrame frame;
    private List<MoralityType> moralityTypes;
    private List<MoralitySummaryEntry> entries = new List<MoralitySummaryEntry>();
    private MoralityHeader header;
    public Vector2 location;
    private float moralityScore;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;
    private LerpHandler_Float lerper;
    private Vector2 extraOffsetToHideEdges;

    public MoralityCategory category { get; private set; }

    public MoralitySummaryCategory(
      Player player,
      MoralityCategory category_,
      float basescale,
      bool AddExtraOffsetToHideEdges = false)
    {
      this.scalehelper = new UIScaleHelper(basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.category = category_;
      this.moralityTypes = MoralityData.GetMaralityEntres(category_);
      this.moralityScore = 0.0f;
      this.header = new MoralityHeader(this.category.ToString(), basescale, this.scalehelper.ScaleX(360f));
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += this.header.GetSize().Y;
      this.framescale.Y += defaultBuffer.Y;
      foreach (MoralityType moralityType in this.moralityTypes)
      {
        MoralitySummaryEntry moralitySummaryEntry = new MoralitySummaryEntry(player, moralityType, basescale);
        this.entries.Add(moralitySummaryEntry);
        this.moralityScore += moralitySummaryEntry.GetMoralityScore();
        this.framescale.X = Math.Max(this.framescale.X, moralitySummaryEntry.GetSize().X + 2f * defaultBuffer.X);
        this.framescale.Y += moralitySummaryEntry.GetSize().Y + defaultBuffer.Y;
      }
      if (this.moralityTypes.Count > 0)
        this.framescale.Y -= defaultBuffer.Y;
      if (AddExtraOffsetToHideEdges)
      {
        this.extraOffsetToHideEdges = this.scalehelper.ScaleVector2(new Vector2(2f, 2f));
        this.framescale += this.extraOffsetToHideEdges;
      }
      this.header.SetScore(this.moralityScore);
      this.frame = new CustomerFrame(this.framescale, BaseScale: basescale);
      this.frame.location -= this.extraOffsetToHideEdges;
      this.currloc = -0.5f * this.framescale;
      this.currloc.X += defaultBuffer.X;
      this.currloc.Y += defaultBuffer.Y;
      this.header.location.Y = (float) (-0.5 * (double) this.framescale.Y + (double) defaultBuffer.Y + 0.5 * (double) this.header.GetSize().Y);
      this.header.location.X = 0.0f;
      this.currloc.Y += this.header.GetSize().Y + defaultBuffer.Y;
      foreach (MoralitySummaryEntry entry in this.entries)
      {
        entry.location = this.currloc;
        entry.location += 0.5f * entry.GetSize();
        this.currloc.Y += entry.GetSize().Y + defaultBuffer.Y;
      }
      this.lerper = new LerpHandler_Float();
    }

    public void LerpIn_TopDown() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff_DownUp() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public float GetMoralityScore() => this.moralityScore;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return MathStuff.CheckPointCollision(true, offset, 1f, this.GetSize().X, this.GetSize().Y, player.inputmap.PointerLocation);
    }

    public void UpdateMoralitySummaryCategory(float DeltaTime) => this.lerper.UpdateLerpHandler(DeltaTime);

    public void DrawMoralitySummaryCategory(Vector2 offset) => this.DrawMoralitySummaryCategory(offset, AssetContainer.pointspritebatch03);

    public void DrawMoralitySummaryCategory(Vector2 offset, SpriteBatch spriteBatch)
    {
      if (this.entries.Count < 1)
        return;
      offset += this.location;
      offset.Y += this.lerper.Value * -this.frame.VSCale.Y;
      this.frame.DrawCustomerFrame(offset, spriteBatch);
      foreach (MoralitySummaryEntry entry in this.entries)
        entry.DrawMoralitySummaryEntry(offset);
      this.header.DrawMoralityHeader(spriteBatch, offset);
    }

    public Vector2 GetSize() => this.framescale;
  }
}

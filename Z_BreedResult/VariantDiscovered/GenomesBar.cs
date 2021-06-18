// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.VariantDiscovered.GenomesBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedResult.VariantDiscovered
{
  internal class GenomesBar
  {
    private CustomerFrame frame;
    private List<GenomesBarSegment> segments;
    public Vector2 location;
    private Vector2 size;
    private Vector2 segmentOffset = Vector2.Zero;
    private DNAIcon dnaIcon;
    private string progressStr;
    private string titleStr = "Genome Map Progress";
    private Vector2 titleLoc;
    private Vector2 progressLoc;
    private int numUnlocked;
    private int numSegments;
    private static Vector2 extraPad;
    private static float padX10;
    private static float padY10;
    private Color cream = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private float basescale;

    public GenomesBar(Player player, AnimalType type, int variantIndex, float basescale_)
    {
      this.basescale = basescale_;
      GenomesBar.padX10 = 10f * this.basescale;
      GenomesBar.padY10 = 30f * this.basescale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      GenomesBar.extraPad.X = 1f * GenomesBar.padX10;
      GenomesBar.extraPad.Y = 1f * GenomesBar.padY10;
      float num = 0.5f * GenomesBar.padX10;
      this.segments = new List<GenomesBarSegment>();
      this.size = Vector2.Zero;
      this.numSegments = EnemyData.GetEnemyRectangle(type).GetTotalVariants();
      this.dnaIcon = new DNAIcon(this.basescale);
      this.size = this.dnaIcon.GetSize();
      if (this.numSegments <= 0)
        throw new Exception("no bar??");
      if (this.numSegments == 1)
      {
        this.segments.Add(new GenomesBarSegment(player, GenomesBarSegment.SegmentType.Single, type, 0, variantIndex, this.basescale));
        this.size.X += this.segments[0].GetSize().X;
        this.size.Y = MathHelper.Max(this.size.Y, this.segments[0].GetSize().Y);
      }
      else
      {
        this.segments.Add(new GenomesBarSegment(player, GenomesBarSegment.SegmentType.LeftCap, type, 0, variantIndex, this.basescale));
        this.size.X += this.segments[0].GetSize().X;
        this.size.Y = MathHelper.Max(this.size.Y, this.segments[0].GetSize().Y);
        for (int index = 1; index < this.numSegments - 1; ++index)
        {
          this.segments.Add(new GenomesBarSegment(player, GenomesBarSegment.SegmentType.Middle, type, index, variantIndex, this.basescale));
          this.size.X += this.segments[index].GetSize().X;
          this.size.Y = MathHelper.Max(this.size.Y, this.segments[index].GetSize().Y);
        }
        this.segments.Add(new GenomesBarSegment(player, GenomesBarSegment.SegmentType.RightCap, type, this.numSegments - 1, variantIndex, this.basescale));
        this.size.X += this.segments[this.numSegments - 1].GetSize().X;
        this.size.Y = MathHelper.Max(this.size.Y, this.segments[this.segments.Count - 1].GetSize().Y);
      }
      this.numUnlocked = 0;
      foreach (GenomesBarSegment segment in this.segments)
      {
        if (segment.IsUnlocked)
          ++this.numUnlocked;
      }
      this.progressStr = this.numUnlocked.ToString() + "/" + this.numSegments.ToString();
      this.size.X += num;
      this.segmentOffset.X = -0.5f * this.size.X;
      this.segmentOffset.Y = -0.5f * this.size.Y;
      this.segmentOffset.Y += 0.15f * GenomesBar.padY10;
      this.dnaIcon.vLocation = this.segmentOffset;
      this.titleLoc = this.segmentOffset;
      this.titleLoc.Y -= (float) (0.5 * (double) this.dnaIcon.GetSize().Y + 0.100000001490116 * (double) GenomesBar.padY10);
      this.segmentOffset.X += this.dnaIcon.GetSize().X + num;
      this.segmentOffset.Y += 0.25f * this.dnaIcon.GetSize().Y;
      foreach (GenomesBarSegment segment in this.segments)
      {
        segment.location = this.segmentOffset;
        this.segmentOffset.X += segment.GetSize().X;
      }
      this.progressLoc.X = this.segmentOffset.X;
      this.progressLoc.Y = this.titleLoc.Y;
      this.size += GenomesBar.extraPad;
      this.frame = new CustomerFrame(new Vector2(180f * this.basescale, this.size.Y), BaseScale: this.basescale);
    }

    public void UpdateGenomesBar(Player player, Vector2 offset, float DeltaTime)
    {
      foreach (GenomesBarSegment segment in this.segments)
        segment.UpdateGenomeBarSegment(player, this.location + offset, DeltaTime);
    }

    public Vector2 GetSize() => this.size;

    public void DrawGenomesBar(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.dnaIcon.DrawDNAIcon(offset, AssetContainer.pointspritebatchTop05);
      foreach (GenomesBarSegment segment in this.segments)
        segment.DrawGenomesBarSegment(offset, spritebatch);
      TextFunctions.DrawTextWithDropShadow(this.titleStr, this.basescale, offset + this.titleLoc, this.cream, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      TextFunctions.DrawTextWithDropShadow(this.progressStr, this.basescale, offset + this.progressLoc, this.cream, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false, true);
    }
  }
}

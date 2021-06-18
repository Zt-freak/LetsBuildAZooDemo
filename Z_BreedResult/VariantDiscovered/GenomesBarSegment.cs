// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.VariantDiscovered.GenomesBarSegment
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BreedResult.VariantDiscovered
{
  internal class GenomesBarSegment
  {
    private static Rectangle leftCapRect = new Rectangle(0, 279, 17, 8);
    private static Rectangle segmentRect = new Rectangle(18, 279, 16, 8);
    private static Rectangle rightCapRect = new Rectangle(35, 279, 17, 8);
    private static Rectangle singleRect = new Rectangle(31, 205, 18, 8);
    private static Rectangle leftCapRectFill = new Rectangle(884, 430, 17, 8);
    private static Rectangle segmentRectFill = new Rectangle(902, 430, 16, 8);
    private static Rectangle rightCapRectFill = new Rectangle(919, 430, 17, 8);
    private static Rectangle singleRectFill = new Rectangle(50, 205, 18, 8);
    private VariantMouseoverPanel mouseoverPanel;
    private GameObject segmentObj = new GameObject();
    private GameObject fillObj = new GameObject();
    private GameObject tinyline = new GameObject();
    private GenomesBarSegment.SegmentType type;
    public Vector2 location;
    private bool unlocked;
    private Vector3 colour;
    private int variant;
    private static Color cream = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private static Vector3 colourUnlocked = ColourData.EvilPurple;
    private static Vector3 colourNew = ColourData.GoodYellow;
    private Vector2 tinylineScale;
    private bool mouseover;
    private static float padY10;
    private float basescale;
    private static float extraHeight = 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;

    public bool IsUnlocked => this.unlocked;

    public GenomesBarSegment(
      Player player,
      GenomesBarSegment.SegmentType segmentType_,
      AnimalType animalType_,
      int segmentVariant,
      int newVariant,
      float baseScale,
      float scale = 1f)
    {
      this.basescale = baseScale;
      this.tinyline.DrawRect = TinyZoo.Game1.WhitePixelRect;
      GenomesBarSegment.padY10 = 10f * baseScale * scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.tinyline.scale = scale * baseScale;
      this.tinyline.SetAllColours(ColourData.Z_Cream);
      this.segmentObj.scale = scale * baseScale;
      this.fillObj.scale = scale * baseScale;
      this.type = segmentType_;
      this.variant = segmentVariant;
      this.mouseover = false;
      this.unlocked = player.Stats.GetTotalOfThisVariantFound(animalType_, segmentVariant) > 0;
      if (segmentVariant == newVariant)
        this.unlocked = true;
      if (this.unlocked)
        this.colour = segmentVariant != newVariant ? GenomesBarSegment.colourUnlocked : GenomesBarSegment.colourNew;
      this.fillObj.SetAllColours(this.colour);
      switch (this.type)
      {
        case GenomesBarSegment.SegmentType.Middle:
          this.segmentObj.DrawRect = GenomesBarSegment.segmentRect;
          this.fillObj.DrawRect = GenomesBarSegment.segmentRectFill;
          break;
        case GenomesBarSegment.SegmentType.LeftCap:
          this.segmentObj.DrawRect = GenomesBarSegment.leftCapRect;
          this.fillObj.DrawRect = GenomesBarSegment.leftCapRectFill;
          break;
        case GenomesBarSegment.SegmentType.RightCap:
          this.segmentObj.DrawRect = GenomesBarSegment.rightCapRect;
          this.fillObj.DrawRect = GenomesBarSegment.rightCapRectFill;
          break;
        case GenomesBarSegment.SegmentType.Single:
          this.segmentObj.DrawRect = GenomesBarSegment.singleRect;
          this.fillObj.DrawRect = GenomesBarSegment.singleRectFill;
          break;
      }
      Vector2 location_ = new Vector2();
      location_.X = 0.5f * this.GetSize().X;
      location_.Y = (float) (-0.5 * (double) this.GetSize().Y - 2.5 * (double) GenomesBarSegment.padY10);
      this.mouseoverPanel = new VariantMouseoverPanel(location_, animalType_, segmentVariant, baseScale);
      this.tinyline.vLocation = location_;
      this.tinyline.vLocation.X -= 1f * baseScale;
      this.tinyline.vLocation.Y += 0.5f * this.mouseoverPanel.GetSize().Y;
      this.tinylineScale = new Vector2(2f, 13f) * this.basescale * Sengine.ScreenRatioUpwardsMultiplier;
    }

    public void UpdateGenomeBarSegment(Player player, Vector2 offset, float DeltaTime) => this.mouseover = MathStuff.CheckPointCollision(false, this.location + offset + new Vector2(0.0f, -0.5f * GenomesBarSegment.extraHeight), this.segmentObj.scale, (float) this.segmentObj.DrawRect.Width, (float) this.segmentObj.DrawRect.Height + GenomesBarSegment.extraHeight, player.inputmap.PointerLocation);

    public void DrawGenomesBarSegment(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.segmentObj.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
      if (this.unlocked)
        this.fillObj.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
      if (!this.mouseover)
        return;
      this.mouseover = false;
      if (!this.unlocked)
        return;
      this.tinyline.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.tinylineScale, 0.5f);
      this.mouseoverPanel.DrawVariantMouseoverPanel(offset);
    }

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      Vector2 vector2 = new Vector2((float) this.segmentObj.DrawRect.Width, (float) this.segmentObj.DrawRect.Height);
      switch (this.type)
      {
        case GenomesBarSegment.SegmentType.Middle:
          vector2.X -= 2f;
          break;
        case GenomesBarSegment.SegmentType.LeftCap:
          vector2.X -= 2f;
          break;
        case GenomesBarSegment.SegmentType.RightCap:
          vector2.X -= 0.0f;
          break;
      }
      vector2 *= this.segmentObj.scale;
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }

    internal enum SegmentType
    {
      Middle,
      LeftCap,
      RightCap,
      Single,
      Count,
    }
  }
}

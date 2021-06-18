// Decompiled with JetBrains decompiler
// Type: TinyZoo.Gene_Data.GeneDrawBodyInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Gene_Data
{
  internal class GeneDrawBodyInfo
  {
    private BodyRectEntry[] BodyData_ByVariant;
    private Vector2 DefaultOffsetFromBody;

    public GeneDrawBodyInfo(Rectangle BodyZero, Vector2 DefaultOffsetFromBody_ForAllBodies)
    {
      this.DefaultOffsetFromBody = DefaultOffsetFromBody_ForAllBodies;
      this.BodyData_ByVariant = new BodyRectEntry[10];
      this.BodyData_ByVariant[0] = new BodyRectEntry(BodyZero, DefaultOffsetFromBody_ForAllBodies);
    }

    public void SetVariant(int BodyVariantIndex, Rectangle BodyRectangle) => this.BodyData_ByVariant[BodyVariantIndex] = new BodyRectEntry(BodyRectangle);

    public void SetVariant(
      int BodyVariantIndex,
      Rectangle BodyRectangle,
      Vector2 DefaultOffsetFromBody_ForAllHeadsOnThisVariant)
    {
      this.BodyData_ByVariant[BodyVariantIndex] = new BodyRectEntry(BodyRectangle, DefaultOffsetFromBody_ForAllHeadsOnThisVariant);
    }

    public void AddVariantOffset(
      int BodyVariant,
      AnimalType HeadAnimal,
      Vector2 HeadOffset,
      int HeadVariant = -1)
    {
      if (HeadVariant == -1)
        this.BodyData_ByVariant[BodyVariant].AddDefaultVariantOffsetForAllHeadAnimal(HeadAnimal, HeadOffset);
      else
        this.BodyData_ByVariant[BodyVariant].AddVariantOffset(HeadAnimal, HeadOffset, HeadVariant);
    }

    public void GetBodyData(
      AnimalType BodyAnimal,
      AnimalType HeadAnimal,
      int BodyVariant,
      int HeadVariant,
      out Rectangle BodyRect,
      out Vector2 HeadOffsetFromBody)
    {
      HeadOffsetFromBody = this.DefaultOffsetFromBody;
      this.BodyData_ByVariant[BodyVariant].GetBodyData(BodyAnimal, HeadAnimal, BodyVariant, HeadVariant, out BodyRect, ref HeadOffsetFromBody);
    }
  }
}

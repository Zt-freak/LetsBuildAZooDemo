// Decompiled with JetBrains decompiler
// Type: TinyZoo.Gene_Data.BodyRectEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Gene_Data
{
  internal class BodyRectEntry
  {
    private HeadOffsetsFromBodyByVariant[] headoffsetdata;
    private Rectangle BodyRectangle;
    private Vector2 DefaultOffsetFromBody;
    private bool HasDefaultOrigin;

    public BodyRectEntry(Rectangle bodyrect, Vector2 DefaultOffsetFromBody_ForVariantZero)
    {
      this.BodyRectangle = bodyrect;
      this.DefaultOffsetFromBody = DefaultOffsetFromBody_ForVariantZero;
      this.headoffsetdata = new HeadOffsetsFromBodyByVariant[70];
      this.HasDefaultOrigin = true;
    }

    public BodyRectEntry(Rectangle bodyrect)
    {
      this.HasDefaultOrigin = false;
      this.BodyRectangle = bodyrect;
      this.headoffsetdata = new HeadOffsetsFromBodyByVariant[70];
    }

    public void AddDefaultVariantOffsetForAllHeadAnimal(
      AnimalType HeadAnimal,
      Vector2 DefaultOffsetForThisVariant)
    {
      if (this.headoffsetdata[(int) HeadAnimal] == null)
        this.headoffsetdata[(int) HeadAnimal] = new HeadOffsetsFromBodyByVariant();
      this.headoffsetdata[(int) HeadAnimal].SetDefaultValueForAllHeadsOfThisAnimal(DefaultOffsetForThisVariant);
    }

    public void AddVariantOffset(AnimalType HeadAnimal, Vector2 HeadOffset, int HeadVariant = -1)
    {
      if (this.headoffsetdata[(int) HeadAnimal] == null)
        this.headoffsetdata[(int) HeadAnimal] = new HeadOffsetsFromBodyByVariant();
      this.headoffsetdata[(int) HeadAnimal].AddVariantOffset(HeadOffset, HeadVariant);
    }

    public void GetBodyData(
      AnimalType BodyAnimal,
      AnimalType HeadAnimal,
      int BodyVariant,
      int HeadVariant,
      out Rectangle BodyRect,
      ref Vector2 HeadOffsetFromBody)
    {
      BodyRect = this.BodyRectangle;
      if (this.HasDefaultOrigin)
        HeadOffsetFromBody = this.DefaultOffsetFromBody;
      if (this.headoffsetdata[(int) HeadAnimal] == null)
        return;
      this.headoffsetdata[(int) HeadAnimal].GetHeadOffsetFromBody(HeadVariant, ref HeadOffsetFromBody);
    }
  }
}

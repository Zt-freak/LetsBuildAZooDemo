// Decompiled with JetBrains decompiler
// Type: TinyZoo.Gene_Data.HeadOffsetsFromBodyByVariant
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Gene_Data
{
  internal class HeadOffsetsFromBodyByVariant
  {
    private Vector2[] HeadOffsetsFromBody;
    private bool[] ThisIsCustom;
    private Vector2 Default;
    private bool HasDefault;

    public HeadOffsetsFromBodyByVariant(Vector2 DefaultOffsetForThisVariant)
    {
      this.HasDefault = true;
      this.Default = DefaultOffsetForThisVariant;
      this.Create();
    }

    public HeadOffsetsFromBodyByVariant()
    {
      this.HasDefault = false;
      this.Create();
    }

    public void SetDefaultValueForAllHeadsOfThisAnimal(Vector2 DefaultOffsetForThisVariant)
    {
      this.HasDefault = true;
      this.Default = DefaultOffsetForThisVariant;
    }

    public void AddVariantOffset(Vector2 HeadOffset, int HeadVariant = -1)
    {
      this.ThisIsCustom[HeadVariant] = true;
      this.HeadOffsetsFromBody[HeadVariant] = HeadOffset;
    }

    private void Create()
    {
      this.ThisIsCustom = new bool[10];
      this.HeadOffsetsFromBody = new Vector2[10];
    }

    public void GetHeadOffsetFromBody(int HeadVariant, ref Vector2 HeadOffsetFromBody)
    {
      if (this.HasDefault)
        HeadOffsetFromBody = this.Default;
      if (!this.ThisIsCustom[HeadVariant])
        return;
      HeadOffsetFromBody = this.HeadOffsetsFromBody[HeadVariant];
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Gene_Data.GeneDrawHeadInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;

namespace TinyZoo.Gene_Data
{
  internal class GeneDrawHeadInfo
  {
    private Rectangle[] DrawRectHeads;
    private Vector2[] HeadOrigins;

    public GeneDrawHeadInfo(Rectangle HeadZero, Vector2 OffsetZero)
    {
      this.DrawRectHeads = new Rectangle[10];
      this.HeadOrigins = new Vector2[10];
      this.HeadOrigins[0] = OffsetZero;
      this.DrawRectHeads[0] = HeadZero;
    }

    public void AddHead(int Variant, Rectangle HeadRect, Vector2 Offset)
    {
      this.HeadOrigins[Variant] = Offset;
      this.DrawRectHeads[Variant] = HeadRect;
    }

    public void GetHead(out Rectangle HeadRect, out Vector2 HeadOrigin, int HeadVariant)
    {
      HeadRect = HeadVariant != -1 ? this.DrawRectHeads[HeadVariant] : throw new Exception("NOT WORKING");
      HeadOrigin = this.HeadOrigins[HeadVariant];
    }
  }
}

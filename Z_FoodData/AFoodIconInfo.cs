// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_FoodData.AFoodIconInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_FoodData
{
  internal class AFoodIconInfo
  {
    public Rectangle[] rectangles;

    public AFoodIconInfo(Rectangle Many, Rectangle Some, Rectangle Few)
    {
      this.rectangles = new Rectangle[3];
      this.rectangles[0] = Few;
      this.rectangles[1] = Some;
      this.rectangles[2] = Many;
    }
  }
}

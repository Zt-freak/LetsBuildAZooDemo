// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.NoAnimal.NoAnimalManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_ManagePen.NoAnimal
{
  internal class NoAnimalManager
  {
    private SimpleTextHandler simpletext;
    private Vector2 Vscale;
    private GameObjectNineSlice Frame;

    public NoAnimalManager()
    {
      this.Vscale = new Vector2(720f, 200f);
      this.Frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.simpletext = new SimpleTextHandler("Add animals to your enclosures, and then adjust the settings here!", true, 0.6f, GameFlags.GetSmallTextScale());
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
    }

    public void DrawNoAnimals()
    {
      Vector2 Offset = new Vector2(512f, 300f);
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.Vscale);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

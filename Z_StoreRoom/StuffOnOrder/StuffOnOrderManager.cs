// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.StuffOnOrder.StuffOnOrderManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_StoreRoom.StuffOnOrder
{
  internal class StuffOnOrderManager
  {
    private SimpleTextHandler Stuff;
    private SimpleTextHandler StuffDays;
    public GameObjectNineSlice InnerFrameHole;
    private Vector2 InnerFrameHoleVScale;

    public StuffOnOrderManager(Player player, Vector2 OtherLocation, Vector2 OtherVSCALE)
    {
      string RightString;
      this.Stuff = new SimpleTextHandler(player.storerooms.GetStuffOnOrderString(out RightString), false, 0.8f, GameFlags.GetSmallTextScale(), false, false);
      this.StuffDays = new SimpleTextHandler(RightString, false, 0.8f, GameFlags.GetSmallTextScale(), false, false);
      this.Stuff.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.StuffDays.paragraph.linemaker.SetAllColours(ColourData.Z_CreamFADED);
      this.StuffDays.AutoCompleteParagraph();
      this.Stuff.AutoCompleteParagraph();
      this.InnerFrameHole = new GameObjectNineSlice(new Rectangle(948, 528, 21, 21), 7);
      this.InnerFrameHole.scale = 2f;
      this.InnerFrameHole.vLocation.Y = OtherLocation.Y;
      this.InnerFrameHole.vLocation.X = 825f;
      this.InnerFrameHoleVScale.Y = OtherVSCALE.Y;
      this.InnerFrameHoleVScale.X = 330f;
      this.Stuff.Location = new Vector2(this.InnerFrameHoleVScale.X * -0.5f, this.InnerFrameHoleVScale.Y * -0.5f);
      this.Stuff.Location.X += 20f;
      this.Stuff.Location.Y += 20f;
      this.StuffDays.Location = new Vector2(this.InnerFrameHoleVScale.X * 0.2f, this.InnerFrameHoleVScale.Y * -0.5f);
      this.StuffDays.Location.Y += 20f;
    }

    public void UpdateStuffOnOrderManager()
    {
    }

    public void DrawStuffOnOrderManager(Vector2 Offset)
    {
      this.InnerFrameHole.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.InnerFrameHoleVScale);
      Offset += this.InnerFrameHole.vLocation;
      this.Stuff.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatch03);
      this.StuffDays.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatch03);
      TextFunctions.DrawJustifiedText("Incoming Purchases", RenderMath.GetPixelSizeBestMatch(1f) * 0.5f, Offset + new Vector2(this.InnerFrameHoleVScale.X * -0.0f, this.InnerFrameHoleVScale.Y * -0.5f) + new Vector2(0.0f, -10f), this.Stuff.paragraph.linemaker.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.HeroQuests.QuestList.NameAndQuest
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Quests.HeroQuests.QuestList
{
  internal class NameAndQuest : GameObject
  {
    private string Heading;
    private bool UseBigFont;
    private SimpleTextHandler simpletext;

    public NameAndQuest(string WriteThis, float BaseScale, bool _UseBigFont = false, float ScreenWidth = 0.5f)
    {
      this.UseBigFont = _UseBigFont;
      this.Heading = WriteThis;
      this.scale = BaseScale;
      this.SetAllColours(ColourData.Z_Cream);
      this.simpletext = new SimpleTextHandler(WriteThis, false, ScreenWidth, BaseScale, _UseBigFont, false);
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.AutoCompleteParagraph();
    }

    public Vector2 GetSize() => this.simpletext.GetSize(true);

    public void SetForBrownPanel(Vector2 VSCaleForPanel, float BaseScale)
    {
      this.vLocation = VSCaleForPanel * -0.5f;
      this.vLocation.X += 10f * BaseScale;
      this.vLocation.Y += 40f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void DrawNameAndQuest(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (!this.UseBigFont)
        this.simpletext.DrawSimpleTextHandler(Offset + this.vLocation, 1f, spritebatch);
      else
        this.simpletext.DrawSimpleTextHandler(Offset + this.vLocation, 1f, spritebatch);
    }
  }
}

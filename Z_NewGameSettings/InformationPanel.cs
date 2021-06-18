// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_NewGameSettings.InformationPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_NewGameSettings
{
  internal class InformationPanel
  {
    private GameObjectNineSlice Framer;
    private SimpleTextHandler simpletextbox;
    private Vector3 SecondaryColour;
    public Vector2 Location;

    public InformationPanel()
    {
      this.Framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out this.SecondaryColour), 7);
      this.Framer.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.SetText("Set up how you want to play the game");
    }

    public void SetText(string TEXT)
    {
      this.simpletextbox = new SimpleTextHandler(TEXT, false, 0.3f, GameFlags.GetSmallTextScale(), false, false);
      this.simpletextbox.paragraph.linemaker.SetAllColours(this.SecondaryColour);
      this.simpletextbox.AutoCompleteParagraph();
    }

    public void UpdateInformationPanel(float DeltaTime)
    {
    }

    public void DrawInformationPanel()
    {
      this.Location.X = 550f;
      this.Location.Y = 300f;
      this.Framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location, new Vector2(350f, 300f));
      this.simpletextbox.DrawSimpleTextHandler(this.Location + new Vector2(-150f, -100f * Sengine.ScreenRatioUpwardsMultiplier.Y));
    }
  }
}

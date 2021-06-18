// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_NewGameSettings.PlayPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_NewGameSettings
{
  internal class PlayPanel
  {
    private TextButton PlayButton;
    private GameObjectNineSlice Framer;
    private SimpleTextHandler simpletextbox;
    private Vector3 SecondaryColour;
    public Vector2 Location;

    public PlayPanel()
    {
      this.PlayButton = new TextButton(SEngine.Localization.Localization.GetText(60));
      this.PlayButton.vLocation = new Vector2(980f, 0.0f);
      this.PlayButton.stringinabox.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.Framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out this.SecondaryColour), 7);
      this.Framer.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.SetMode(true);
      this.PlayButton.vLocation = new Vector2(400f, 0.0f);
    }

    public void SetText(string TEXT)
    {
      this.simpletextbox = new SimpleTextHandler(TEXT, false, 0.85f, GameFlags.GetSmallTextScale(), false, false);
      this.simpletextbox.paragraph.linemaker.SetAllColours(this.SecondaryColour);
      this.simpletextbox.AutoCompleteParagraph();
    }

    public void SetMode(bool Default)
    {
      if (Default)
      {
        this.SetText("Begin a new game using the default settings.~Your progress will be logged on the global leaderboards, and your zoo will be viewable by other players whenever you upload it.");
        this.PlayButton.SetButtonColour(BTNColour.Green);
      }
      else
      {
        this.PlayButton.SetButtonColour(BTNColour.Red);
        this.SetText("Begin a new game using the Custom settings.~Note, Your progress will not be logged on the global leaderboards, and your zoo will only be viewable in the unranked listings. Playing without the default settings can result in not being able to access some of the game's content.");
      }
      this.PlayButton.stringinabox.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public bool UpdatePlayPanel(Player player, float DeltaTime) => this.PlayButton.UpdateTextButton(player, this.Location, DeltaTime) || player.inputmap.PressedThisFrame[0];

    public void DrawPlayPanel()
    {
      this.Location.X = 512f;
      this.Location.Y = 728f;
      this.Framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location, new Vector2(1024f, 80f));
      this.simpletextbox.DrawSimpleTextHandler(this.Location + new Vector2(-470f, -20f * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.PlayButton.DrawTextButton(this.Location);
    }
  }
}

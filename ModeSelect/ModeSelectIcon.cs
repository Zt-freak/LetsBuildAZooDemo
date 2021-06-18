// Decompiled with JetBrains decompiler
// Type: TinyZoo.ModeSelect.ModeSelectIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using SEngine.Localization;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.ModeSelect
{
  internal class ModeSelectIcon : GameObject
  {
    private SimpleTextHandler simpletext;
    private GameObjectNineSlice nineslicer;
    private GameObjectNineSlice nineslicerAlt;
    public Vector2 Location;
    private bool MoseOver;
    private bool IsArcadeMode;
    private TextButton button;
    private Vector2 VSCALE;
    private string Header;

    public ModeSelectIcon(bool _IsArcadeMode)
    {
      this.IsArcadeMode = _IsArcadeMode;
      this.DrawRect = new Rectangle(466, 304, 122, 155);
      string str = "Build and manage your own space prison!";
      this.Header = "BUILD MODE";
      if (this.IsArcadeMode)
      {
        this.Header = "ARCADE MODE";
        this.DrawRect = new Rectangle(589, 304, 89, 86);
        str = "Battle through a group of preset challenges, and master the art of drone controlled prisoner management.";
      }
      float num = 1f;
      if (PlayerStats.language == Language.Spanish)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.Japanese)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      this.SetDrawOriginToCentre();
      this.button = new TextButton(SEngine.Localization.Localization.GetText(60));
      this.button.SetButtonYellow();
      this.button.AddControllerButton(ControllerButton.XboxA);
      this.nineslicer = new GameObjectNineSlice(new Rectangle(895, 372, 21, 21), 7);
      this.nineslicerAlt = new GameObjectNineSlice(new Rectangle(895, 394, 21, 21), 7);
      this.simpletext = new SimpleTextHandler(str, false, 0.35f, 3f * Sengine.UltraWideSreenDownardsMultiplier * num, false, false);
      this.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      this.simpletext.AutoCompleteParagraph();
      this.nineslicer.scale = 2f;
      this.nineslicerAlt.scale = 2f;
      this.VSCALE = new Vector2(400f, 600f);
    }

    public bool UpdateModeSelectIcon(ref int MouseOver, Player player)
    {
      if (GameFlags.IsUsingMouse)
      {
        if (MathStuff.CheckPointCollision(true, this.Location, 1f, this.VSCALE.X, this.VSCALE.Y, player.inputmap.PointerLocation))
        {
          MouseOver = !this.IsArcadeMode ? 0 : 1;
          if (MouseStatus.LMouseClicked)
            return true;
        }
      }
      else if (MathStuff.CheckPointCollision(true, this.Location, 1f, this.VSCALE.X, this.VSCALE.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
        MouseOver = !this.IsArcadeMode ? 0 : 1;
      return (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.Location, 1f, this.VSCALE.X, this.VSCALE.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawModeSelectIcon(bool Selected)
    {
      this.nineslicer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location, this.VSCALE);
      if (this.MoseOver | Selected)
      {
        this.nineslicerAlt.vLocation = Vector2.Zero;
        this.nineslicerAlt.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location, this.VSCALE);
        this.button.vLocation = new Vector2(0.0f, (float) byte.MaxValue);
        this.button.DrawTextButton(this.Location);
        this.simpletext.paragraph.linemaker.SetAllColours(ColourData.FernLemon);
      }
      else
        this.simpletext.paragraph.linemaker.SetAllColours(ColourData.IconYellow);
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Location + new Vector2(0.0f, -80f));
      float num = 1f;
      if (PlayerStats.language == Language.Spanish)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.Header, "XXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.Portuguese)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.Header, "XXXXXXXXXXXX", AssetContainer.springFont, true);
      TextFunctions.DrawJustifiedText(this.Header, 4f * num, this.Location + new Vector2(0.0f, -260f), this.simpletext.paragraph.linemaker.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      float y = 90f;
      if (this.IsArcadeMode)
        y = 70f * Sengine.ScreenRationReductionMultiplier.Y;
      this.simpletext.DrawSimpleTextHandler(this.Location + new Vector2(-170f, y), 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

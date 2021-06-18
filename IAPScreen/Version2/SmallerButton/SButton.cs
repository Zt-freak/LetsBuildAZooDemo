// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.SmallerButton.SButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.IAPScreen.Version2.SmallerButton
{
  internal class SButton : GameObject
  {
    public bool IsSelected;
    private Rectangle BaseRect = new Rectangle(463, 573, 88, 28);
    private Rectangle HighlightRect = new Rectangle(463, 602, 88, 28);
    private GameObject TextBar;
    private Vector2 VScaleForSelectedText;
    public OfferButtonType btntype;
    private SimpleTextHandler simpletext;

    public SButton(OfferButtonType _btntype)
    {
      this.btntype = _btntype;
      this.IsSelected = false;
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.scale = 3f;
      this.TextBar = new GameObject();
      this.TextBar.scale = 3f;
      this.TextBar.SetAllColours(ColourData.IconYellow);
      this.TextBar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.TextBar.SetDrawOriginToCentre();
      this.VScaleForSelectedText = new Vector2(230f, 3f);
      string str = "Time Travel:~Watch Ad";
      if (this.btntype == OfferButtonType.BuyTheGoat)
        str = "Space Goat Pack:~No Ads";
      else if (this.btntype == OfferButtonType.TheVortexMind)
        str = "Vortex Mind:~Faster Research";
      else if (this.btntype == OfferButtonType.BuyTheFlower)
        str = string.Format("Suppressia:~Less Buildings", (object) "50");
      float num = 1f;
      if (PlayerStats.language == Language.Russian)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.French)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.Korean)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.German)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      if (PlayerStats.language == Language.Spanish || PlayerStats.language == Language.Portuguese)
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      this.simpletext = new SimpleTextHandler(str, true, _Scale: (3f * num));
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.IconYellow);
    }

    public bool UpdateSButton(Player player, float DeltaTime, Vector2 Offset) => (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);

    public void DrawSButton(Vector2 Offset)
    {
      if (this.IsSelected)
        this.DrawRect = this.HighlightRect;
      else
        this.DrawRect = this.BaseRect;
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      this.simpletext.DrawSimpleTextHandler(Offset + this.vLocation + new Vector2(0.0f, -12f * Sengine.UltraWideSreenUpwardsMultiplier), 1f, AssetContainer.pointspritebatchTop05);
      if (!this.IsSelected)
        return;
      this.TextBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.vLocation + new Vector2(0.0f, -28f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.VScaleForSelectedText * Sengine.ScreenRatioUpwardsMultiplier);
      this.TextBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + this.vLocation + new Vector2(0.0f, 28f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.VScaleForSelectedText * Sengine.ScreenRatioUpwardsMultiplier);
    }
  }
}

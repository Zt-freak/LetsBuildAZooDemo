// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Offer.SignInOffer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using Spring.Comms;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.Settings.Offer
{
  internal class SignInOffer
  {
    private LerpHandler_Float lerper;
    private GameObjectNineSlice ninslicer;
    private SimpleTextHandler simpletext;
    private GameObject Guy;

    public SignInOffer(Player player)
    {
      string str = "Sign in, and connect with Tiny Dice Dungeon to unlock a new alien!";
      bool HasThisSocial1;
      player.socialmanager.GetUser().GetThisSocialPair(SocialType.Pixona, out HasThisSocial1);
      if (HasThisSocial1)
      {
        bool HasThisSocial2;
        player.socialmanager.GetUser().GetThisSocialPair(SocialType.TDD1_UID, out HasThisSocial2);
        if (HasThisSocial2)
        {
          if (!player.inventory.SecretAliensAvailable[0])
          {
            player.inventory.SecretAliensAvailable[0] = true;
            player.Stats.TDDLink = true;
            str = "Congratulations! The Triclops has been discovered!";
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.Menu_Splash);
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.Menu_Splash, Pitch: 1f, Pan: 1f);
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.Menu_Splash, Pitch: -1f, Pan: -1f);
            player.OldSaveThisPlayer();
          }
        }
        else
          str = SEngine.Localization.Localization.GetText(72);
      }
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      this.ninslicer = new GameObjectNineSlice(new Rectangle(895, 372, 21, 21), 7);
      float num = 1f;
      if (PlayerStats.language == Language.English)
        this.simpletext = new SimpleTextHandler(str, true, 0.375f, 3f * num);
      if (PlayerStats.language == Language.Russian)
      {
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        this.simpletext = new SimpleTextHandler(str, true, 0.375f, 3f * num);
      }
      if (PlayerStats.language == Language.Japanese)
      {
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        this.simpletext = new SimpleTextHandler(str, true, 0.35f, 6f * num);
      }
      if (PlayerStats.language == Language.French)
        this.simpletext = new SimpleTextHandler(str, true, 0.375f, 3f * num);
      if (PlayerStats.language == Language.Chinese_Simplified || PlayerStats.language == Language.Korean)
      {
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        this.simpletext = new SimpleTextHandler(str, true, 0.35f, 5.5f * num);
      }
      if (PlayerStats.language == Language.Chinese_Traditional)
      {
        num = TextFunctions.GetStringPercentageReScaledToSpecificLength(str, "XXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        this.simpletext = new SimpleTextHandler(str, true, 0.35f, 5f * num);
      }
      if (PlayerStats.language == Language.German)
        this.simpletext = new SimpleTextHandler(str, true, 0.35f, 2.8f);
      if (PlayerStats.language == Language.Spanish)
        this.simpletext = new SimpleTextHandler(str, true, 0.375f, 3f * num);
      if (PlayerStats.language == Language.Portuguese)
        this.simpletext = new SimpleTextHandler(str, true, 0.375f, 3f * num);
      this.simpletext.AutoCompleteParagraph();
      this.Guy = new GameObject();
      this.Guy.DrawRect = EnemyData.GetEnemyPortraitIcon(AnimalType.Triclops, 0);
      this.Guy.SetDrawOriginToCentre();
      this.ninslicer.scale = 3f;
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.YellowHighlight);
    }

    public void Exit() => this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);

    public void UpdateSignInOffer(float DeltaTime) => this.lerper.UpdateLerpHandler(DeltaTime);

    public void DrawSignInOffer()
    {
      float y = 300f;
      Vector2 Offset = new Vector2((float) (200.0 + (double) this.lerper.Value * 512.0), (float) (768.0 - (double) y * 0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      if (GameFlags.HasNotch)
        Offset.X += GameFlags.NotchSize;
      Offset.Y -= 30f;
      this.Guy.scale = 2f;
      this.ninslicer.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, new Vector2(400f, y) * Sengine.ScreenRatioUpwardsMultiplier);
      this.simpletext.DrawSimpleTextHandler(Offset + new Vector2(0.0f, -100f * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
      this.Guy.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(0.0f, 80f));
    }
  }
}

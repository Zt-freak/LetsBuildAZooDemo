// Decompiled with JetBrains decompiler
// Type: TinyZoo.FullScreenAdvert.FullScreenAdvertManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;

namespace TinyZoo.FullScreenAdvert
{
  internal class FullScreenAdvertManager
  {
    private BlackOut blackout;
    private LerpHandler_Float lerper;
    private GameObject Goat;
    private SimpleTextHandler simpletext;
    private SimpleTextHandler simpletextHead;
    internal static bool ShownPopUp;
    private TextButton Yes;
    private BackButton back;
    private bool Exiting;
    public bool WillGoToStore;

    public FullScreenAdvertManager(bool IsForGoats, bool IsForSuppressia = false)
    {
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(ColourData.FernVeryDarkBlue);
      this.blackout.SetAlpha(0.8f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Goat = new GameObject();
      this.Goat.DrawRect = new Rectangle(794, 591, 93, 90);
      this.Goat.scale = 2f;
      this.Goat.vLocation = new Vector2(700f, 400f);
      this.Goat.SetDrawOriginToCentre();
      this.Goat.scale = 3f;
      string TextToWrite1 = SEngine.Localization.Localization.GetText(362);
      string TextToWrite2 = SEngine.Localization.Localization.GetText(362);
      if (!IsForGoats && !IsForSuppressia)
      {
        TextToWrite2 = "Faster Research";
        TextToWrite1 = SEngine.Localization.Localization.GetText(362);
        this.Goat.DrawRect = new Rectangle(589, 391, 93, 82);
      }
      else if (IsForSuppressia)
      {
        TextToWrite2 = "Faster Research";
        TextToWrite1 = string.Format(SEngine.Localization.Localization.GetText(362), (object) "50");
        this.Goat.DrawRect = new Rectangle(387, 304, 78, 76);
      }
      this.simpletext = new SimpleTextHandler(TextToWrite1, true, 0.8f);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.paragraph.linemaker.SetAllColours(ColourData.IconYellow);
      this.simpletextHead = new SimpleTextHandler(TextToWrite2, true, _Scale: 4f);
      this.simpletextHead.AutoCompleteParagraph();
      this.simpletextHead.paragraph.linemaker.SetAllColours(ColourData.IconYellow);
      this.Yes = new TextButton(SEngine.Localization.Localization.GetText(82), OverAllMultiplier: 1.4f);
      this.Yes.AddControllerButton(ControllerButton.XboxA);
      this.Yes.vLocation = new Vector2(800f, 600f);
      this.back = new BackButton();
    }

    public bool UpdateFullScreenAdvertManager(ref float DeltaTime, Player player)
    {
      if (OverWorldManager.IsGameIntro)
        return false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      if (!this.Exiting)
      {
        if (this.Yes.UpdateTextButton(player, Offset, DeltaTime) || player.inputmap.PressedThisFrame[0])
        {
          this.Exiting = true;
          this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
          this.WillGoToStore = true;
        }
        if (this.back.UpdateBackButton(player, DeltaTime))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
          this.Exiting = true;
          this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
        }
      }
      player.inputmap.Movementstick = Vector2.Zero;
      return this.Exiting && (double) this.lerper.Value == -1.0;
    }

    public void DrawFullScreenAdvertManager()
    {
      if (OverWorldManager.IsGameIntro)
        return;
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.blackout.DrawBlackOut(Offset, AssetContainer.pointspritebatchTop05);
      this.simpletext.Location = new Vector2(512f, 160f);
      this.simpletextHead.Location = new Vector2(512f, 100f);
      this.simpletextHead.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.Goat.SetDrawOriginToCentre();
      this.Goat.vLocation.X = 512f;
      this.Goat.vLocation.Y = 450f;
      this.Goat.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      this.Yes.vLocation.X = 900f;
      this.Yes.vLocation.Y = 650f;
      this.Goat.scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
      this.Yes.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      if ((double) this.lerper.Value > 0.0)
        Offset *= -1f;
      this.back.DrawBackButton(Offset);
    }
  }
}

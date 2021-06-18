// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.PopUps.PopUpPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Tutorials;
using TinyZoo.Z_SummaryPopUps.SellStructure;

namespace TinyZoo.OverWorld.PopUps
{
  internal class PopUpPanel
  {
    private SmartCharacterBox charactertextbox;
    private SimpleTextHandler text;
    private TextButton OK;
    private TextButton Cancel;
    private BlackOut blackout;
    private LerpHandler_Float lerper;
    internal static int LastButtonPressed;
    private SellOnBlackMarket sellonblackmarket;
    private GameObject SpeakingCharacter;
    private ReturnToWild returntothewild;
    private Z_SellStructureManager zsellstructuremanager;

    public PopUpPanel(
      string TextToWrite,
      Player player,
      bool HasTwoButtons = true,
      bool SellOnBlackMarket = false,
      bool IsSellBuilding = false,
      bool IsEnclosure = false)
    {
      if (IsSellBuilding)
        this.zsellstructuremanager = new Z_SellStructureManager(IsEnclosure, player);
      if (SellOnBlackMarket)
      {
        this.sellonblackmarket = new SellOnBlackMarket();
        this.returntothewild = new ReturnToWild();
      }
      PopUpPanel.LastButtonPressed = -1;
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(ColourData.IconYellow);
      this.charactertextbox = new SmartCharacterBox(TextToWrite, AnimalType.Administrator);
      this.text = new SimpleTextHandler(TextToWrite, true, 0.8f);
      this.text.paragraph.linemaker.SetAllColours(ColourData.FernDarkBlue);
      string str = "";
      int num = GameFlags.IsConsoleVersion ? 1 : 0;
      this.OK = new TextButton(str + SEngine.Localization.Localization.GetText(65), 50f);
      this.text.Location = new Vector2(512f, 250f);
      this.Cancel = new TextButton(str + SEngine.Localization.Localization.GetText(66), 50f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.OK.vLocation = new Vector2(768f, 400f);
      this.Cancel.vLocation = new Vector2(256f, 400f);
      this.OK.AddControllerButton(ControllerButton.XboxA);
      this.Cancel.AddControllerButton(ControllerButton.XboxB);
      if (!HasTwoButtons)
      {
        this.OK.vLocation = new Vector2(512f, 400f);
        this.Cancel = (TextButton) null;
      }
      if (!SellOnBlackMarket)
        return;
      this.OK.vLocation = new Vector2(900f, 700f);
      this.OK.SetText(nameof (Cancel));
      this.Cancel = (TextButton) null;
    }

    public void SetAdvertPopUp(Player player) => throw new Exception("NO WAY MAN");

    public bool UpdatePopUpPanel(float DeltaTime, Player player)
    {
      if (this.zsellstructuremanager != null && this.zsellstructuremanager.UpdateZ_SellStructureManager(Vector2.Zero, DeltaTime, player, ref PopUpPanel.LastButtonPressed))
      {
        this.zsellstructuremanager = (Z_SellStructureManager) null;
        return true;
      }
      if (OverWorldManager.fulladvertmanager != null && (TinyZoo.Game1.gamestate == GAMESTATE.OverWorld || TinyZoo.Game1.gamestate == GAMESTATE.IAPStore) || TinyZoo.Game1.gamestate == GAMESTATE.IAPStore)
        return false;
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      this.lerper.UpdateLerpHandler(DeltaTime);
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.text.UpdateSimpleTextHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        if (this.sellonblackmarket != null)
        {
          if (this.sellonblackmarket.UpdatSellOnBlackMarket(DeltaTime, player, Offset))
          {
            PopUpPanel.LastButtonPressed = 1;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
            this.lerper.SetLerp(false, 1f, 1f, 3f, true);
          }
          if (this.returntothewild.UpdatSellOnBlackMarket(DeltaTime, player, Offset))
          {
            PopUpPanel.LastButtonPressed = 2;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
            this.lerper.SetLerp(false, 1f, 1f, 3f, true);
          }
        }
        bool flag = false;
        if (this.Cancel == null && player.inputmap.PressedThisFrame[7])
          flag = true;
        if (this.OK.UpdateTextButton(player, Offset, DeltaTime) | flag)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          this.lerper.SetLerp(false, 1f, 1f, 3f, true);
          PopUpPanel.LastButtonPressed = this.Cancel != null ? 1 : 0;
        }
        if (this.Cancel != null && (this.Cancel.UpdateTextButton(player, Offset, DeltaTime) || player.inputmap.PressedThisFrame[7]))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
          this.lerper.SetLerp(false, 1f, 1f, 3f, true);
          PopUpPanel.LastButtonPressed = 0;
        }
      }
      return (double) this.lerper.TargetValue == 1.0 && (double) this.lerper.Value == 1.0;
    }

    public void DrawPopUpPanel()
    {
      if (this.zsellstructuremanager != null)
      {
        this.zsellstructuremanager.DrawZ_SellStructureManager(Vector2.Zero);
      }
      else
      {
        if (OverWorldManager.fulladvertmanager != null && TinyZoo.Game1.gamestate == GAMESTATE.OverWorld || TinyZoo.Game1.gamestate == GAMESTATE.IAPStore)
          return;
        this.blackout.fAlpha = 0.9f;
        Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
        this.blackout.DrawBlackOut(Offset, AssetContainer.pointspritebatchTop05);
        this.charactertextbox.DrawSmartCharacterBox(Offset);
        this.OK.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
        if (this.Cancel != null)
          this.Cancel.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
        if (this.sellonblackmarket == null)
          return;
        this.sellonblackmarket.DrawSellOnBlackMarket(Offset);
        this.returntothewild.DrawSellOnBlackMarket(Offset);
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.NewGameOptions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New
{
  internal class NewGameOptions
  {
    private List<TextButton> textbuttons;
    public GameCustomizationScreen gamecustomizationscreen;
    public Vector2 Location;
    private GameObject KeySprite;
    private GameObject CustoneGameKeySprite;

    public NewGameOptions(float BaseScale)
    {
      this.gamecustomizationscreen = new GameCustomizationScreen(BaseScale);
      float num = 30f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.textbuttons = new List<TextButton>();
      for (int index = 0; index < 3; ++index)
      {
        this.textbuttons.Add(new TextButton(BaseScale, this.GetNewGameOptionsToString((NewGameOptionsEnum) index), 100f * BaseScale));
        this.textbuttons[index].vLocation = new Vector2(0.0f, (float) (0.0 + (double) index * (double) num));
        if (index == 2)
          this.textbuttons[index].vLocation.Y += num;
        else
          this.textbuttons[index].SetButtonColour(BTNColour.Grey);
        this.textbuttons[index].vLocation.Y -= num * 1.5f;
      }
      this.Location.Y = 50f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.KeySprite = new GameObject();
      this.KeySprite.DrawRect = new Rectangle(838, 414, 186, 81);
      this.KeySprite.scale = BaseScale;
      this.KeySprite.SetDrawOriginToCentre();
      this.KeySprite.vLocation.Y = BaseScale * -120f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CustoneGameKeySprite = new GameObject(this.KeySprite);
      this.CustoneGameKeySprite.DrawRect.X = 651;
    }

    private string GetNewGameOptionsToString(NewGameOptionsEnum gameoption)
    {
      switch (gameoption)
      {
        case NewGameOptionsEnum.Customize:
          return SEngine.Localization.Localization.GetText(783);
        case NewGameOptionsEnum.RestoreDefaults:
          return SEngine.Localization.Localization.GetText(784);
        case NewGameOptionsEnum.Start:
          return SEngine.Localization.Localization.GetText(60);
        default:
          return "Not FOund";
      }
    }

    public void UpdateNewGameOptions(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      MiniHeading minheading)
    {
      Offset += this.Location;
      if (this.gamecustomizationscreen.Active)
      {
        this.gamecustomizationscreen.UpdateGameCustomizationScreen(Vector2.Zero, player, DeltaTime);
      }
      else
      {
        for (int index = 0; index < this.textbuttons.Count; ++index)
        {
          if (this.textbuttons[index].UpdateTextButton(player, Offset, DeltaTime) && index == 0)
            this.gamecustomizationscreen.Activate();
        }
      }
    }

    public void DrawNewGameOptions(Vector2 Offset)
    {
      Offset += this.Location;
      if (this.gamecustomizationscreen.Active)
      {
        this.gamecustomizationscreen.DrawGameCustomizationScreen(Vector2.Zero);
      }
      else
      {
        if (this.gamecustomizationscreen.IsCustomGame)
          this.CustoneGameKeySprite.Draw(AssetContainer.pointspritebatch03, AssetContainer.UISheet, Offset);
        else
          this.KeySprite.Draw(AssetContainer.pointspritebatch03, AssetContainer.UISheet, Offset);
        for (int index = 0; index < this.textbuttons.Count; ++index)
          this.textbuttons[index].DrawTextButton(Offset, 1f, AssetContainer.pointspritebatch03);
      }
    }
  }
}

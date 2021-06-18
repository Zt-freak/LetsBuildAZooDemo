// Decompiled with JetBrains decompiler
// Type: TinyZoo.ProfitLadder.ProfitLadderManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.ProfitLadder.LevelSummary;
using TinyZoo.ProfitLadder.PeopleDisplay;

namespace TinyZoo.ProfitLadder
{
  internal class ProfitLadderManager
  {
    private StoreBGManager storebg;
    private BackButton back;
    private LevelDisplayManager leveldisplaymanager;
    private BlackOut blackout;
    private BlackOut Strop;
    private GameObject HEading;
    private string HEadingText;
    private PeopleDisplayManager peopledisplaymanager;

    public ProfitLadderManager(Player player)
    {
      this.storebg = new StoreBGManager(true);
      this.back = new BackButton();
      this.blackout = new BlackOut();
      this.Strop = new BlackOut();
      this.Strop.SetAlpha(0.5f);
      this.Strop.VScale.Y = 370f;
      this.blackout.SetAlpha(0.3f);
      this.leveldisplaymanager = new LevelDisplayManager(player);
      this.HEading = new GameObject();
      this.HEading.SetAllColours(ColourData.FernLemon);
      this.HEading.vLocation = new Vector2(512f, 40f);
      this.HEading.scale = 4f;
      this.peopledisplaymanager = new PeopleDisplayManager(player);
      this.HEadingText = SEngine.Localization.Localization.GetText(84) + " (Rank " + (object) (int) (this.leveldisplaymanager.rank + 1) + ")";
    }

    public void UpdateProfitLadderManager(float DeltaTime, Player player)
    {
      this.storebg.UpdateStoreBGManager(DeltaTime);
      if (this.back.UpdateBackButton(player, DeltaTime))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
      this.leveldisplaymanager.UpdateLevelDisplayManager(player, DeltaTime);
      this.peopledisplaymanager.UpdatePeopleDisplayManager(player, DeltaTime);
    }

    public void DrawProfitLadderManager()
    {
      this.storebg.DrawStoreBGManager(Vector2.Zero);
      this.blackout.SetAllColours(0.1f, 0.1f, 0.4f);
      this.blackout.SetAlpha(0.5f);
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.Strop.VScale.Y = 260f;
      this.Strop.DrawBlackOut(new Vector2(0.0f, 135f), AssetContainer.pointspritebatchTop05);
      this.HEading.vLocation.Y = 69f;
      TextFunctions.DrawJustifiedText(this.HEadingText, 5f * Sengine.UltraWideSreenDownardsMultiplier, this.HEading.vLocation, this.HEading.GetColour(), this.HEading.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      this.leveldisplaymanager.DrawLevelDisplayManager();
      this.peopledisplaymanager.DrawPeopleDisplayManager();
      this.back.DrawBackButton(Vector2.Zero);
    }
  }
}

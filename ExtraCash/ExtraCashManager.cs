// Decompiled with JetBrains decompiler
// Type: TinyZoo.ExtraCash.ExtraCashManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.ExtraCash
{
  internal class ExtraCashManager
  {
    private StoreBGManager storeBG;
    private CharacterTextBox charactertextbox;
    private SimpleTextHandler text;
    private BackButton back;
    private TextButton textbutton;
    private StringInBox[] results;
    private StringInBox[] resultsReward;
    private bool Blocked;
    private bool IsCOuntDown;
    private string COuntDownTime;
    private GameObject HEading;
    private string HEadingText;

    public ExtraCashManager(Player player) => this.Create(player);

    private void Create(Player player)
    {
      player.Stats.bounty.SetReward(player);
      this.back = new BackButton();
      this.storeBG = new StoreBGManager(IsAutumnal: true);
      string TextToSay = "You have a job offer! If you can go to a nearby planet and help lockdown some priosners you will be rewarded!";
      this.Blocked = false;
      if (!player.Stats.bounty.bountyevent.TryToCollect(player.Stats.datetimemanager, false))
      {
        this.Blocked = true;
        TextToSay = "Please connect to the internet, so that we can search for employment opportunities";
      }
      if (player.Stats.bounty.bountyevent.HasBeenClaimed(player.Stats.datetimemanager))
      {
        this.Blocked = true;
        this.IsCOuntDown = true;
        TextToSay = "There are no jobs available right now";
      }
      int num = player.Stats.bounty.CAAP.SmartGetValue(false, 10000);
      this.charactertextbox = new CharacterTextBox(AnimalType.ShopKeeper, TextToSay, Sengine.UltraWideSreenDownardsMultiplier);
      this.resultsReward = new StringInBox[3];
      this.results = new StringInBox[3];
      for (int index = 0; index < 3; ++index)
      {
        string Text1 = string.Format("REWARD 1: No casualties:");
        string Text2 = "$";
        switch (index)
        {
          case 0:
            Text2 += (string) (object) (num * 4);
            break;
          case 1:
            Text1 = string.Format("REWARD 2: Complete Lock down:");
            Text2 += (string) (object) (num * 2);
            break;
          case 2:
            Text1 = string.Format("REWARD 3: Simply turn up and give it your best!");
            Text2 += (string) (object) num;
            break;
        }
        this.results[index] = new StringInBox(Text1, 3f, 270f, true);
        this.results[index].vLocation = new Vector2(422f, (float) (400 + index * 80));
        this.resultsReward[index] = new StringInBox(Text2, 3f, 40f, true);
        this.resultsReward[index].vLocation = new Vector2(912f, (float) (400 + index * 80));
        this.results[index].SetButtonGreen();
        if (index > 1)
          this.results[index].SetButtonRed();
        this.resultsReward[index].SetButtonPurple();
      }
      this.textbutton = new TextButton(SEngine.Localization.Localization.GetText(60));
      this.textbutton.vLocation = new Vector2(900f, 650f);
      this.HEading = new GameObject();
      this.HEading.SetAllColours(ColourData.FernDarkBlue);
      this.HEading.vLocation = new Vector2(512f, 40f);
      this.HEading.scale = 4f;
      this.HEadingText = "EVENT";
    }

    public void UpdateExtraCashManager(Player player, float DeltaTime)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (this.IsCOuntDown)
      {
        bool IsReady;
        this.COuntDownTime = player.Stats.bounty.bountyevent.GetTimeUntilNextCheckIn(player.Stats.datetimemanager, out IsReady);
        if (IsReady)
          this.Create(player);
      }
      this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
      if (this.back.UpdateBackButton(player, DeltaTime))
      {
        TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
      if (this.Blocked || !this.textbutton.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        return;
      GameFlags.BountyMode = true;
      TinyZoo.Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
      TinyZoo.Game1.screenfade.BeginFade(true);
    }

    public void DrawExtraCashManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      this.back.DrawBackButton(Vector2.Zero);
      for (int index = 0; index < this.results.Length; ++index)
      {
        if (!this.Blocked)
        {
          this.results[index].DrawStringInBox(Vector2.Zero, AssetContainer.pointspritebatchTop05);
          this.resultsReward[index].DrawStringInBox(Vector2.Zero, AssetContainer.pointspritebatchTop05);
        }
      }
      if (this.IsCOuntDown)
        TextFunctions.DrawJustifiedText("Time to next job: " + this.COuntDownTime, 4f, new Vector2(512f, 400f), Color.PaleGoldenrod, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawCharacterTextBox(new Vector2(512f, 200f), AssetContainer.pointspritebatchTop05);
      if (!this.Blocked)
        this.textbutton.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatchTop05);
      this.HEading.vLocation.Y = 69f;
      TextFunctions.DrawJustifiedText(this.HEadingText, 5f * Sengine.UltraWideSreenDownardsMultiplier, this.HEading.vLocation, this.HEading.GetColour(), this.HEading.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}

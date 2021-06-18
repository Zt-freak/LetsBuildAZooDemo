// Decompiled with JetBrains decompiler
// Type: TinyZoo.BountyResults.BountyResultsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.BountyResults
{
  internal class BountyResultsManager
  {
    private StoreBGManager storeBG;
    private CharacterTextBox charactertextbox;
    private TextButton textbutton;
    private StringInBox earnings;

    public BountyResultsManager(Player player)
    {
      int GiveThis = player.Stats.bounty.CAAP.SmartGetValue(false);
      string Text = string.Format("Earnings: ${0}", (object) (GiveThis * 4));
      this.storeBG = new StoreBGManager(IsAutumnal: true);
      string TextToSay = string.Format("You completed the job! Well done, you have been paid ${0}", (object) (GiveThis * 4));
      if (player.Stats.bounty.bountyevent.TryToCollect(player.Stats.datetimemanager, ForceConsume: true))
      {
        if (player.livestats.WasNotPerfectButFinishedBounty)
        {
          player.Stats.GiveCash(GiveThis * 2, player);
          Text = string.Format("Earnings: ${0}", (object) (GiveThis * 2));
          TextToSay = string.Format("While there were some casualties, you still did a great job, you earned ${0}", (object) (GiveThis * 2));
        }
        else if (player.livestats.GaveUpBounty)
        {
          player.Stats.GiveCash(GiveThis, player);
          Text = string.Format("Earnings {0}", (object) GiveThis);
          TextToSay = string.Format("Thanks for giving the job a try! I guess vapourising every prisoner is one way of dealing with the issue! you earned ${0}" + (object) GiveThis);
        }
        else
          player.Stats.GiveCash(GiveThis * 4, player);
        this.earnings = new StringInBox(Text, 3f, 310f, true);
        this.earnings.vLocation = new Vector2(512f, 400f);
        this.earnings.SetGreen();
      }
      else
        TextToSay = SEngine.Localization.Localization.GetText(362);
      player.OldSaveThisPlayer();
      this.charactertextbox = new CharacterTextBox(AnimalType.ShopKeeper, TextToSay, Sengine.UltraWideSreenDownardsMultiplier);
      GameFlags.BountyMode = false;
      this.textbutton = new TextButton(SEngine.Localization.Localization.GetText(14));
      this.textbutton.vLocation = new Vector2(900f, 650f);
    }

    public void UpdateBountyResultsManager(float DeltaTime, Player player)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
      if (!this.textbutton.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        return;
      GameFlags.BountyMode = true;
      TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
      TinyZoo.Game1.screenfade.BeginFade(true);
    }

    public void DrawBountyResultsManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      if (this.earnings != null)
        this.earnings.DrawStringInBox(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.charactertextbox.DrawCharacterTextBox(new Vector2(512f, 200f), AssetContainer.pointspritebatchTop05);
      this.textbutton.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

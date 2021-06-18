// Decompiled with JetBrains decompiler
// Type: TinyZoo.Event.EventScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.Event
{
  internal class EventScreenManager
  {
    private StoreBGManager storeBG;
    private bool Exiting;
    private BackButton backbtn;
    private CharacterTextBox charactertextbox;
    private List<EventProgressBar> eventprogressbars;
    private GameObject HEading;
    private string HEadingText;

    public EventScreenManager(Player player)
    {
      if (player.Stats.currentEvent.CheckEvent(player))
        return;
      this.storeBG = new StoreBGManager(true);
      this.backbtn = new BackButton(true);
      string TextToSay;
      if (player.Stats.currentEvent.IsComplete_AndClaimed(player))
        TextToSay = "We managed to get the information we needed, the new species has been added to our inmate list.";
      else if (player.Stats.currentEvent.Event_Enemytype == AnimalType.None)
      {
        TextToSay = "We don't have any leads on new alien species at the moment.";
      }
      else
      {
        switch (TinyZoo.Game1.Rnd.Next(0, 3))
        {
          case 0:
            TextToSay = "We need to capture and interogate new inmates, to discover the whereabouts of an undiscovered alien species known for their high crime rate!";
            break;
          case 1:
            TextToSay = "I interrogate new inmates, to discover the secret locations of aliens on the most wanted list. But to do that, I need fresh inmates!";
            break;
          default:
            TextToSay = "The prisoners here fear me, as I have ways of extracting information on whatever we need. Right now we are trying to discover the location of an unknown criminal species";
            break;
        }
      }
      this.eventprogressbars = new List<EventProgressBar>();
      for (int index = 0; index < player.Stats.currentEvent.evprogress.Count; ++index)
        this.eventprogressbars.Add(new EventProgressBar(player.Stats.currentEvent.evprogress[index], player));
      this.charactertextbox = new CharacterTextBox(AnimalType.ShopKeeper, TextToSay, Sengine.UltraWideSreenDownardsMultiplier);
      this.HEading = new GameObject();
      this.HEading.SetAllColours(ColourData.FernLemon);
      this.HEading.vLocation = new Vector2(512f, 40f);
      this.HEading.scale = 4f;
      this.HEadingText = "EVENT";
    }

    public void UpdateEventScreenManager(float DeltaTime, Player player)
    {
      this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (this.eventprogressbars != null && this.eventprogressbars.Count > 0)
        this.eventprogressbars[0].UpdateEventProgressBar(player);
      if (!this.backbtn.UpdateBackButton(player, DeltaTime) && !player.inputmap.PressedBackOnController())
        return;
      TinyZoo.Game1.screenfade.BeginFade(true);
      TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
    }

    public void DrawEventScreenManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      this.backbtn.DrawBackButton(Vector2.Zero);
      if (this.eventprogressbars != null && this.eventprogressbars.Count > 0)
        this.eventprogressbars[0].DrawEventProgressBar();
      this.charactertextbox.DrawCharacterTextBox(new Vector2(512f, 200f), AssetContainer.pointspritebatchTop05);
      this.HEading.vLocation.Y = 69f;
      TextFunctions.DrawJustifiedText(this.HEadingText, 5f * Sengine.UltraWideSreenDownardsMultiplier, this.HEading.vLocation, this.HEading.GetColour(), this.HEading.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}

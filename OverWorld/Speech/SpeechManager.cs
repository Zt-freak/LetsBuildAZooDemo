// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Speech.SpeechManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.FakeMouse;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Tutorials;
using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Z_Notification;
using TinyZoo.Z_OverWorld.Speech;

namespace TinyZoo.OverWorld.Speech
{
  internal class SpeechManager
  {
    private bool Active;
    private float Timer;
    private bool IsDrawingPopUp;
    private Enemy enemy;
    private SpeechBubble speechbubble;
    private static List<PersonBubble> speakingpeoplebubbles;
    private bool IsDrawingForDrone;
    private static float LastMessage;
    internal static float LastMessageAutoPop;
    internal static bool SomeoneShouldSaySomething;

    public SpeechManager() => SpeechManager.speakingpeoplebubbles = new List<PersonBubble>();

    internal static bool CustomerCanSpeak() => (double) SpeechManager.LastMessage <= 0.0;

    internal static void AddPersonAndComment(
      WalkingPerson person,
      SpeechEvent speechevent,
      string SayThis = "",
      Z_NotificationType notificationtype = Z_NotificationType.Count,
      ZTipType TipType = ZTipType.None)
    {
      if (SayThis == "")
        SayThis = notificationtype != Z_NotificationType.Count ? SpeechData.GetThisNotificationToSpeech(notificationtype, TipType) : SpeechData.GetThisSpeech(speechevent);
      if ((double) SpeechManager.LastMessage >= 0.0)
        return;
      for (int index = 0; index < SpeechManager.speakingpeoplebubbles.Count; ++index)
      {
        if (!SpeechManager.speakingpeoplebubbles[index].Active)
        {
          SpeechManager.speakingpeoplebubbles[index].Activate(person, SayThis);
          SpeechManager.LastMessage = 19f;
          return;
        }
      }
      SpeechManager.speakingpeoplebubbles.Add(new PersonBubble());
      SpeechManager.speakingpeoplebubbles[SpeechManager.speakingpeoplebubbles.Count - 1].Activate(person, SayThis);
      SpeechManager.LastMessage = 19f;
      SpeechManager.LastMessageAutoPop = 19f;
    }

    public void UpdateSpeechManager(float DeltaTime, OverWorldEnvironmentManager overworldmanager)
    {
      if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
      {
        if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
          this.Active = false;
        else if (OWHUDManager.popuppanel != null)
        {
          this.Active = false;
        }
        else
        {
          if ((double) DeltaTime > (double) GameFlags.RefDeltaTime)
            DeltaTime = GameFlags.RefDeltaTime;
          SpeechManager.LastMessage -= DeltaTime;
          SpeechManager.LastMessageAutoPop -= DeltaTime;
          if ((double) SpeechManager.LastMessageAutoPop < -20.0 && Player.financialrecords.GetDaysPassed() > 1L)
            SpeechManager.SomeoneShouldSaySomething = true;
          for (int index = SpeechManager.speakingpeoplebubbles.Count - 1; index > -1; --index)
            SpeechManager.speakingpeoplebubbles[index].UpdatePersonBubble(DeltaTime);
          if (!this.Active)
          {
            this.IsDrawingPopUp = false;
            this.Active = true;
            this.Timer = (float) TinyZoo.Game1.Rnd.Next(10, 20);
          }
          if (!this.IsDrawingPopUp)
          {
            if ((double) this.Timer > 0.0)
              this.Timer -= DeltaTime;
            if ((double) this.Timer > 0.0)
              return;
            if (TinyZoo.Game1.Rnd.Next(0, 3) == 10)
            {
              this.IsDrawingForDrone = true;
              this.IsDrawingPopUp = true;
              this.Timer = 8f;
              this.speechbubble = new SpeechBubble(PrisonerSpeech.GetSpeech((Enemy) null, true), 0.25f);
              this.speechbubble.SetAllColours(new Vector3(1f, 0.8f, 0.8f));
            }
            else
            {
              this.IsDrawingForDrone = false;
              this.enemy = overworldmanager.GetRandomPerson();
              if (this.enemy == null || this.enemy.enemyrenderere.IsDead)
                return;
              this.speechbubble = new SpeechBubble(PrisonerSpeech.GetSpeech(this.enemy, false), 0.25f);
              this.IsDrawingPopUp = true;
              this.Timer = 8f;
              this.speechbubble.SetAllColours(new Vector3(1f, 1f, 1f));
            }
          }
          else
          {
            this.Timer -= DeltaTime;
            if ((double) this.Timer < 0.0)
            {
              this.IsDrawingPopUp = false;
              this.Timer = (float) TinyZoo.Game1.Rnd.Next(10, 20);
            }
            this.speechbubble.UpdateSpeechBubble(DeltaTime);
          }
        }
      }
      else
        this.Active = false;
    }

    public void DrawSpeechManager()
    {
      if (OverWorldManager.overworldstate != OverWOrldState.MainMenu || TutorialManager.currenttutorial != TUTORIALTYPE.None || (OWHUDManager.popuppanel != null || !this.Active))
        return;
      if ((this.enemy != null || this.IsDrawingForDrone) && this.IsDrawingPopUp)
      {
        Vector2 droneLocation = DroneAI.DroneLocation;
        Vector2 screenSpace;
        if (this.IsDrawingForDrone)
        {
          droneLocation.Y -= 15f;
          screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(droneLocation);
        }
        else
        {
          screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(this.enemy.enemyrenderere.vLocation);
          screenSpace.Y -= (float) (this.enemy.enemyrenderere.DrawRect.Height + 1) * this.enemy.enemyrenderere.scale * Sengine.WorldOriginandScale.Z;
        }
        this.speechbubble.DrawSpeechBubble(AssetContainer.pointspritebatch01, screenSpace);
      }
      for (int index = 0; index < SpeechManager.speakingpeoplebubbles.Count; ++index)
        SpeechManager.speakingpeoplebubbles[index].DrawPersonBubble();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Credits.CredistsTextManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Localization;
using SEngine.Objects;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Settings.Credits.GridOfRidiculousDeath.Credits;

namespace TinyZoo.Settings.Credits
{
  internal class CredistsTextManager
  {
    private List<CreditsDisplayEntry> credits;
    private float FullHeight;
    private float StartY;
    private float Speed;
    private float DefaultSpeed;
    private float EX;
    public float CurrentOffset;
    private float DragLastFrame;
    private float DragTwoFramesAgo;
    private float AutoSpeed;
    private bool WillWrap;
    private bool HasStartedFades;

    public CredistsTextManager(bool _WillWrap = true, bool StartOffScreen = false)
    {
      this.HasStartedFades = false;
      this.WillWrap = _WillWrap;
      this.Build();
      this.SetPositionsOnRotate();
      this.CurrentOffset = 750f;
      this.CurrentOffset = 200f;
      for (int index = 0; index < this.credits.Count; ++index)
        this.credits[index].SetAlpha(false, 0.5f, 0.0f, 1f);
      if (!StartOffScreen)
        return;
      this.CurrentOffset = 768f;
    }

    public bool UpdateCreditsManager(Player player, float DeltaTime, Vector2 Offset)
    {
      if ((double) Math.Abs(player.inputmap.Movementstick.Y) > 0.200000002980232)
        this.CurrentOffset += (float) ((double) player.inputmap.Movementstick.Y * (double) DeltaTime * -300.0);
      else if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0)
      {
        if (player.player.touchinput.DragActive)
        {
          this.CurrentOffset += player.player.touchinput.DragVectorThisFrame.Y;
          this.DragTwoFramesAgo = this.DragLastFrame;
          this.DragLastFrame = player.player.touchinput.DragVectorThisFrame.Y;
          this.AutoSpeed = this.DragTwoFramesAgo;
        }
      }
      else
      {
        float num = -32f;
        this.DragLastFrame = 0.0f;
        this.DragTwoFramesAgo = 0.0f;
        if ((double) this.AutoSpeed != 0.0)
          this.CurrentOffset += num * DeltaTime;
        else
          this.CurrentOffset += num * DeltaTime;
      }
      this.EX = 0.0f;
      if ((double) this.CurrentOffset < -(double) this.FullHeight)
      {
        if (this.WillWrap)
        {
          this.CurrentOffset += this.FullHeight + 768f;
        }
        else
        {
          this.CurrentOffset = -this.FullHeight;
          return true;
        }
      }
      else if ((double) this.CurrentOffset > 768.0)
      {
        if (this.WillWrap)
          this.CurrentOffset -= this.FullHeight + 768f;
        else
          this.CurrentOffset = 768f;
      }
      for (int index = 0; index < this.credits.Count; ++index)
        this.credits[index].UpdateColours(DeltaTime);
      for (int index = 0; index < this.credits.Count; ++index)
      {
        if (this.credits[index].UpdateCreditsDisplayEntry(Offset + new Vector2(0.0f, this.CurrentOffset + this.EX), player) && SEngine.Flags.PlatformIsMobile)
        {
          TinyZoo.Game1.SetNextGameState(GAMESTATE.ModeSelectSetUp);
          TinyZoo.Game1.screenfade.BeginFade(true);
          GameFlags.IsArcadeMode = true;
        }
      }
      return false;
    }

    public void DrawCreditsManager(Vector2 Offset)
    {
      for (int index = 0; index < this.credits.Count; ++index)
        this.credits[index].DrawCreditsDisplayEntry(Offset + new Vector2(0.0f, this.CurrentOffset + this.EX));
    }

    public void SetPositionsOnRotate()
    {
      float Y = 0.0f;
      for (int index = 0; index < this.credits.Count; ++index)
        this.credits[index].SetPositionsOnRotate(ref Y);
      this.CurrentOffset = 0.0f;
      this.StartY = this.CurrentOffset;
      this.FullHeight = Y + this.CurrentOffset;
    }

    private void Build()
    {
      this.credits = new List<CreditsDisplayEntry>();
      List<CreditsDisplayEntry> TempEntries = new List<CreditsDisplayEntry>();
      this.credits.Add(new CreditsDisplayEntry("Springloaded Are:", true));
      TempEntries.Add(new CreditsDisplayEntry("James Barnard"));
      TempEntries.Add(new CreditsDisplayEntry("Cindy Lee"));
      TempEntries.Add(new CreditsDisplayEntry("Lee Shu Yun"));
      TempEntries.Add(new CreditsDisplayEntry("Christine Lim"));
      TempEntries.Add(new CreditsDisplayEntry("Yvonne Goh"));
      TempEntries.Add(new CreditsDisplayEntry("Lim Ying Jin"));
      CredistsTextManager.ShuffleList(TempEntries, ref this.credits);
      this.credits.Add(new CreditsDisplayEntry("Platform: Eggone Zainal"));
      this.credits.Add(new CreditsDisplayEntry("Intern: Nisha"));
      List<CreditsDisplayEntry> creditsDisplayEntryList = new List<CreditsDisplayEntry>();
      this.credits.Add(new CreditsDisplayEntry("Spring Business:", true));
      this.credits.Add(new CreditsDisplayEntry("Loo Jian De"));
      this.credits.Add(new CreditsDisplayEntry("Lydia Lok"));
      this.credits.Add(new CreditsDisplayEntry(" "));
      this.credits.Add(new CreditsDisplayEntry("TRANSLATION:", true));
      this.credits.Add(new CreditsDisplayEntry("Portuguese: Íris Monteiro"));
      if (PlayerStats.language == Language.Korean)
        this.credits.Add(new CreditsDisplayEntry("Korean: Lee Seung Joon (이승준)"));
      else
        this.credits.Add(new CreditsDisplayEntry("Korean: Lee Seung Joon"));
      this.credits.Add(new CreditsDisplayEntry("Japanese: Yuko Matsuoka"));
      this.credits.Add(new CreditsDisplayEntry("Japanese: Mei Konishi"));
      this.credits.Add(new CreditsDisplayEntry("German: Fabian Kubsch"));
      this.credits.Add(new CreditsDisplayEntry("French: David Rey"));
      this.credits.Add(new CreditsDisplayEntry("Russian: Alexander Yarovoy / Brightsiderus"));
      this.credits.Add(new CreditsDisplayEntry("Chinese: Yvonne Goh"));
      this.credits.Add(new CreditsDisplayEntry("Spanish: Luiggi Hidalg"));
      this.credits.Add(new CreditsDisplayEntry("TO HELP TRANSLATE OUR GAMES"));
      this.credits.Add(new CreditsDisplayEntry("GET IN TOUCH!"));
      this.credits.Add(new CreditsDisplayEntry("THANKS TO:", true));
      this.credits.Add(new CreditsDisplayEntry("Jesus Hormigo"));
      this.credits.Add(new CreditsDisplayEntry("Julien Girard-Buttoz"));
      this.credits.Add(new CreditsDisplayEntry("Justin Monteforte"));
      this.credits.Add(new CreditsDisplayEntry("Mark Cronin"));
      this.credits.Add(new CreditsDisplayEntry("eBoy"));
      this.credits.Add(new CreditsDisplayEntry("Lambda Mu"));
      this.credits.Add(new CreditsDisplayEntry("Ming Long"));
      this.credits.Add(new CreditsDisplayEntry("Kai"));
      this.credits.Add(new CreditsDisplayEntry("Mike Gordon"));
      this.credits.Add(new CreditsDisplayEntry("Zack Huntley"));
      this.credits.Add(new CreditsDisplayEntry("Banjamin Marsh"));
      this.credits.Add(new CreditsDisplayEntry("Florian Raoult"));
      this.credits.Add(new CreditsDisplayEntry("Lemon The Dog"));
      this.credits.Add(new CreditsDisplayEntry("Sian Yue"));
      this.credits.Add(new CreditsDisplayEntry("RayTeo Active"));
      this.credits.Add(new CreditsDisplayEntry("KJ Poh"));
      this.credits.Add(new CreditsDisplayEntry("Kongregate"));
      this.credits.Add(new CreditsDisplayEntry("Peter Ekymans"));
      this.credits.Add(new CreditsDisplayEntry("MonoGame"));
      this.credits.Add(new CreditsDisplayEntry("Our discord community"));
      this.credits.Add(new CreditsDisplayEntry("ENGINE:", true));
      this.credits.Add(new CreditsDisplayEntry("Proudly made with XNA"));
      this.credits.Add(new CreditsDisplayEntry(" "));
      this.credits.Add(new CreditsDisplayEntry("Code Version: " + (object) TinyZoo.Game1.VersionNumber));
      this.credits.Add(new CreditsDisplayEntry("  ", true));
      this.credits.Add(new CreditsDisplayEntry("  ", true));
      this.credits.Add(new CreditsDisplayEntry("in memory of", true));
      this.credits.Add(new CreditsDisplayEntry("Coco the dog", true, false));
    }

    private static void ShuffleList(
      List<CreditsDisplayEntry> TempEntries,
      ref List<CreditsDisplayEntry> MainEntries)
    {
      List<CreditsDisplayEntry> creditsDisplayEntryList = new List<CreditsDisplayEntry>();
      RandomizeRepeatLimiter randomizeRepeatLimiter = new RandomizeRepeatLimiter(TempEntries.Count, TempEntries.Count);
      for (int index = 0; index < TempEntries.Count; ++index)
        creditsDisplayEntryList.Add(TempEntries[randomizeRepeatLimiter.GetNextIndex(TinyZoo.Game1.Rnd)]);
      for (int index = 0; index < creditsDisplayEntryList.Count; ++index)
        MainEntries.Add(creditsDisplayEntryList[index]);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Manual.ManualPage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Settings.Manual
{
  internal class ManualPage
  {
    private SimpleTextHandler textthing;
    private SimpleTextHandler Header;
    private LerpHandler_Float Exitlerper;

    public ManualPage(MANUALPAGETYPE page)
    {
      this.Exitlerper = new LerpHandler_Float();
      this.SetHeader(ManualPage.GetHeading(page));
      switch (page)
      {
        case MANUALPAGETYPE.TicketPrices:
          this.SetBody("You can adjust the price of a ticket to your zoo!~If you forget to do this, then you are just a bad manager, and a bad business person...So don't forget!~Having healthy animals, lots of animals, and some park decoration will really help with the value of the zoo!~But mostly, it's the animals that brings the crowd, for it is a zoo after all.");
          break;
        case MANUALPAGETYPE.Breeding:
          this.SetBody("Selective breeding is very important!~Just like how mankind shaped chickens into birds that can no longer fly, or wolves into dogs with flat faces prone to breathing disorders and an early death, you too can do the same!~In the breeding screen, try out different combinations of an animal. If you select an animal, anything it can breed with is highlighted in pink.~~Once you start breeding, you will need to wait a few days before the babies are born. Remember to actually press the 'Start Day' button to progress, otherwise no baby will be born!~As you may have realized, you can control time as well as the deformities of God's animals.");
          break;
        case MANUALPAGETYPE.Genomes:
          this.SetBody("CRISPR, what an amazing thing!~~Through your exploration of cross-breeding, you will eventually map the genome of an animal! Once complete, you can head over to the lab, and set things in motion with some good old DNA modification! Splice together a cow with a duck and see what comes out! Sometimes things don't go exactly to plan, but don't worry, you can always try again! Does life created in a laboratory have any rights anyways? You created it, so you can do what you want with it!");
          break;
        case MANUALPAGETYPE.LockDown:
          this.SetBody("");
          break;
        case MANUALPAGETYPE.Bulding:
          this.SetBody("");
          break;
        case MANUALPAGETYPE.Upgrades:
          this.SetBody("");
          break;
        case MANUALPAGETYPE.HoldingCells:
          this.SetBody("");
          break;
        case MANUALPAGETYPE.Money:
          this.SetBody("");
          break;
        case MANUALPAGETYPE.SignIn:
          this.SetBody("Signing in to our server allows you to use the cloud save feature.~~If you link your account with our other games, you might unlock a secret! so why wait!");
          break;
      }
    }

    internal static string GetHeading(MANUALPAGETYPE page, bool UseShortForm = false)
    {
      switch (page)
      {
        case MANUALPAGETYPE.TicketPrices:
          return "Ticket Prices";
        case MANUALPAGETYPE.Breeding:
          return "Breeding";
        case MANUALPAGETYPE.Genomes:
          return "Genome Breeds";
        case MANUALPAGETYPE.LockDown:
          return "Lock Down";
        case MANUALPAGETYPE.Bulding:
          return UseShortForm ? "Construction" : "Building Your Prison";
        case MANUALPAGETYPE.Upgrades:
          return UseShortForm ? "Upgrades" : "Upgrading your Drone";
        case MANUALPAGETYPE.HoldingCells:
          return "Holding Cells";
        case MANUALPAGETYPE.Money:
          return SEngine.Localization.Localization.GetText(46);
        case MANUALPAGETYPE.SignIn:
          return "Sign In";
        case MANUALPAGETYPE.Solitary:
          return UseShortForm ? "Solitary" : "Solitary Confinement";
        case MANUALPAGETYPE.LandingPad:
          return UseShortForm ? "Construction" : "Landing Pads";
        default:
          return "NO NAME";
      }
    }

    private void SetHeader(string Heading)
    {
      this.Header = new SimpleTextHandler(Heading, false, 0.65f, 4f * Sengine.UltraWideSreenDownardsMultiplier, false, false);
      this.Header.Location = new Vector2(340f, 100f);
      this.Header.AutoCompleteParagraph();
      this.Header.SetAllColours(ColourData.FernLemon);
    }

    private void SetBody(string Heading)
    {
      this.textthing = new SimpleTextHandler(Heading, false, 0.65f, 3f * Sengine.UltraWideSreenDownardsMultiplier, false, false);
      this.textthing.Location = new Vector2(340f, 170f);
      this.textthing.SetAllColours(ColourData.FernLemon);
    }

    public void UpdateManualPage(float DeltaTime, Player player)
    {
      this.Exitlerper.UpdateLerpHandler(DeltaTime);
      this.textthing.UpdateSimpleTextHandler(DeltaTime);
      if ((double) player.player.touchinput.ReleaseTapArray[0].X < 0.0)
        return;
      this.textthing.TryToCompleteParagraph();
    }

    public void Exit()
    {
      if ((double) this.Exitlerper.TargetValue == 1.0)
        return;
      this.Exitlerper.SetLerp(true, 0.0f, 1f, 3f);
    }

    public void DrawManualPage()
    {
      Vector2 Offset = new Vector2(this.Exitlerper.Value * 1024f, 0.0f);
      this.Header.DrawSimpleTextHandler(Offset);
      this.textthing.DrawSimpleTextHandler(Offset);
    }
  }
}

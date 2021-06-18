// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.BuildFirstThing
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldStatus.Stats;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials.BuildThing;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Tutorials
{
  internal class BuildFirstThing
  {
    private int StateCounter;
    private float Timer;
    private SmartCharacterBox charactertextbox;
    private bool IsIntObuildBit;
    private BuiodHereFootPrint buildfootprint;
    private FingerPointer fingerpointer;

    public BuildFirstThing(ref Arrow arrow, ref Vector2 ArrowLocation, Player player)
    {
      OverwoldMainButtons.Selected = -1;
      if (player.Stats.GetCashHeld(false) < player.livestats.GetCost(TILETYPE.LifeSupport, player, true))
        throw new Exception("NO WAY - cant affod to buy the thing");
      this.IsIntObuildBit = false;
      arrow = new Arrow(true);
      FeatureFlags.BlockCash = false;
      arrow.Rotation = -1.570796f;
      ArrowLocation = new Vector2(800f, 40f);
      this.Timer = 0.0f;
      FeatureFlags.BlockCash = false;
      this.charactertextbox = new SmartCharacterBox("", AnimalType.Administrator, true);
      this.charactertextbox.AddNewText(new textBoxPair("", AnimalType.Administrator, SetToBottom: true));
      this.charactertextbox.AddNewText(new textBoxPair("", AnimalType.Administrator, SetToBottom: true));
      FeatureFlags.BlockCloseBuildMenu = true;
      FeatureFlags.OnlyALlowTisThingsToBeBuilt = new List<TILETYPE>();
      FeatureFlags.OnlyALlowTisThingsToBeBuilt.Add(TILETYPE.LifeSupport);
      FeatureFlags.BlockPageCycleInBuild = true;
      this.StateCounter = 0;
    }

    public void UpdateBuildFirstThing(
      ref float DeltaTime,
      Player player,
      ref Arrow arrow,
      ref Vector2 ArrowLocation)
    {
      if (!this.IsIntObuildBit)
      {
        if (this.StateCounter == 0)
        {
          this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
          if (this.charactertextbox.ThisLine == 1)
          {
            FeatureFlags.BlockStats = false;
            this.StateCounter = 1;
            player.player.touchinput.ReleaseTapArray[0].X = -10000f;
            arrow = new Arrow();
            ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, -1000f);
            FeatureFlags.BlockStats = false;
          }
        }
        if (this.StateCounter == 1)
        {
          if (TinyZoo.GameFlags.StatsBarIsOnScreen)
          {
            this.Timer += DeltaTime;
            if (arrow != null)
              arrow = (Arrow) null;
          }
          this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, !TinyZoo.GameFlags.StatsBarIsOnScreen, (double) this.Timer > 0.5);
          ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, StatsButton.YPosition);
          if (this.charactertextbox.ThisLine >= 2)
          {
            player.player.touchinput.ReleaseTapArray[0].X = -10000f;
            this.StateCounter = 2;
            arrow = (Arrow) null;
          }
        }
        if (this.StateCounter != 2 || !this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
          return;
        player.player.touchinput.ReleaseTapArray[0].X = -10000f;
        this.charactertextbox = new SmartCharacterBox("", AnimalType.Administrator, true);
        this.charactertextbox.tinytext = new TinyTextAndButton(ControllerButton.None, "Cycle options", new List<ControllerAnim>()
        {
          new ControllerAnim(ControllerButton.DpadUp, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.3f, _ScaleMultiplier: 0.75f),
          new ControllerAnim(ControllerButton.DpadNeutral, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.2f, _ScaleMultiplier: 0.75f),
          new ControllerAnim(ControllerButton.DpadDown, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.3f, _ScaleMultiplier: 0.75f),
          new ControllerAnim(ControllerButton.DpadNeutral, TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, 0.2f, _ScaleMultiplier: 0.75f)
        });
        this.IsIntObuildBit = true;
        this.StateCounter = 0;
        FeatureFlags.BlockBuild = false;
        arrow = new Arrow();
        ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[3].Location.Y);
      }
      else
      {
        if (!this.IsIntObuildBit)
          return;
        if (this.StateCounter == 0)
        {
          if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, OverWorldManager.overworldstate != OverWOrldState.Build, OverWorldManager.overworldstate == OverWOrldState.Build))
          {
            this.charactertextbox.tinytext = new TinyTextAndButton(ControllerButton.None, SEngine.Localization.Localization.GetText(362));
            player.player.touchinput.ReleaseTapArray[0].X = -10000f;
            this.StateCounter = 1;
            FeatureFlags.BlockBuild = false;
            arrow = (Arrow) null;
            ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[3].Location.Y);
          }
        }
        else if (this.StateCounter == 1)
        {
          if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, TinyZoo.GameFlags.SelectedBuildTILETYPE != TILETYPE.LifeSupport, TinyZoo.GameFlags.SelectedBuildTILETYPE == TILETYPE.LifeSupport))
          {
            this.StateCounter = 2;
            this.fingerpointer = new FingerPointer();
            FeatureFlags.BlockPlayerMoveCamera = false;
            this.buildfootprint = new BuiodHereFootPrint();
            FeatureFlags.BlockBuyPanel = true;
            this.charactertextbox = (SmartCharacterBox) null;
          }
        }
        else if (this.StateCounter == 2)
        {
          if (TinyZoo.GameFlags.SelectedBuildTILETYPE == TILETYPE.LifeSupport)
          {
            if (this.charactertextbox != null)
              this.charactertextbox = (SmartCharacterBox) null;
            this.fingerpointer.UpdateFingerPointer(DeltaTime);
            this.fingerpointer.scale = 3f;
            this.fingerpointer.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(TileMath.GetTileToWorldSpace(new Vector2Int(106, 94)) - new Vector2(8f, 8f));
            this.buildfootprint.UpdateBuiodHereFootPrint(DeltaTime);
            if (ThingToBuildManager.LastLocation != null && ThingToBuildManager.LastLocation.X == 106 && ThingToBuildManager.LastLocation.Y == 94)
            {
              FeatureFlags.BlockPlayerMoveCamera = true;
              FeatureFlags.LockToBuildToCurrentLocation = true;
              FeatureFlags.BlockBuyPanel = false;
            }
          }
          else
          {
            if (this.charactertextbox == null)
              this.charactertextbox = new SmartCharacterBox("Select the Life-Support", AnimalType.Administrator);
            this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
          }
        }
        if (!FeatureFlags.LockToBuildToCurrentLocation)
          return;
        player.player.touchinput.DragVectorThisFrame = Vector2.Zero;
      }
    }

    public void BuiltaThing()
    {
    }

    internal static void CheckTapForSnap(Player player)
    {
      Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(new Vector2Int(106, 94));
      Vector2 worldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.ReleaseTapArray[0]);
      float num = (tileToWorldSpace - worldSpace).Length();
      if ((double) num < 60.0)
      {
        Console.WriteLine("SNAPPED");
        player.player.touchinput.DragVectorThisFrame = Vector2.Zero;
        player.player.touchinput.ReleaseTapArray[0] = RenderMath.TranslateWorldSpaceToScreenSpace(tileToWorldSpace);
      }
      else
        Console.WriteLine("SNAP FAILED length:" + (object) num);
    }

    public void DrawBuildFirstThing()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.buildfootprint == null || TinyZoo.GameFlags.SelectedBuildTILETYPE != TILETYPE.LifeSupport)
        return;
      this.buildfootprint.DrawBuiodHereFootPrint();
      if (TinyZoo.GameFlags.IsUsingController)
        return;
      this.fingerpointer.DrawFingerPointer(Vector2.Zero);
    }
  }
}

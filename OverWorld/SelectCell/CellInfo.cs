// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.CellInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.SelectCell.Orders;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.OverWorld.SelectCell
{
  internal class CellInfo
  {
    private CellInfoButtonType cellInfoButtonType;
    private Vector2 LocationInWorldSpace;
    private GameObject Frame;
    private Vector2 VSCale;
    private Vector2 VScaleSmall;
    private SimpleTextHandler simpletext;
    private SimpleTextHandler SmallText;
    private LerpHandler_Float SelectionLerper;
    private TextButton UseButton;
    private int Cell_UID;
    private GameObjectNineSlice gameobjectninslice;
    private GameObject TouchIcon;
    private float TIconScale;
    private BackButton cellclose;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private PrisonZone REF_prisozone;
    private ExtraCellInfo extraCellInfo;
    private HoldingCellInfo holdingcell;
    private bool HasEnoughSpaceInHoldingCell;

    public CellInfo(PrisonZone prisozone)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.cellInfoButtonType = CellInfoButtonType.PlayGame;
      this.LocationInWorldSpace = prisozone.GetPlaceToDisplayCellInfoButton(out bool _);
      this.REF_prisozone = prisozone;
      this.Cell_UID = prisozone.Cell_UID;
      string cellBlockName = TileData.GetCellBlockName(prisozone.CellBLOCKTYPE);
      string Description = cellBlockName + " " + SEngine.Localization.Localization.GetText(452);
      this.Create(cellBlockName, Description);
      this.TouchIcon = new GameObject();
      this.TouchIcon.DrawRect = new Rectangle(386, 662, 25, 22);
      this.TouchIcon.SetDrawOriginToCentre();
      this.TIconScale = RenderMath.GetPixelZoomOneToOne() * 2f;
      this.TouchIcon.scale = this.TIconScale;
    }

    public CellInfo(HoldingCellInfo _holdingcell, bool _HasEnoughSpace)
    {
      this.HasEnoughSpaceInHoldingCell = _HasEnoughSpace;
      this.holdingcell = _holdingcell;
      this.LocationInWorldSpace = TileMath.GetTileToWorldSpace(this.holdingcell.HoldingCellRoot + new Vector2Int(0, -1));
      this.LocationInWorldSpace.X -= 8f;
      this.LocationInWorldSpace.Y += 8f;
      string Name = "";
      string Description = string.Format(SEngine.Localization.Localization.GetText(362), (object) (GameFlags.MaxHoldngCell - this.holdingcell.prisonercontainer.prisoners.Count));
      this.cellInfoButtonType = CellInfoButtonType.StorePrisoners;
      if (this.holdingcell.prisonercontainer.prisoners.Count == GameFlags.MaxHoldngCell)
      {
        this.cellInfoButtonType = CellInfoButtonType.Full;
        Name = SEngine.Localization.Localization.GetText(68);
        Description = SEngine.Localization.Localization.GetText(362);
      }
      else if (!_HasEnoughSpace)
      {
        this.cellInfoButtonType = CellInfoButtonType.Full;
        Name = SEngine.Localization.Localization.GetText(69);
        Description = SEngine.Localization.Localization.GetText(362);
      }
      this.Create(Name, Description);
      this.Frame.SetAllColours(ColourData.FernDarkGreen);
      if (this.holdingcell.prisonercontainer.prisoners.Count == GameFlags.MaxHoldngCell)
      {
        this.UseButton.SetText(SEngine.Localization.Localization.GetText(68));
        this.UseButton.SetButtonRed();
      }
      else if (!this.HasEnoughSpaceInHoldingCell)
      {
        this.UseButton.SetText(SEngine.Localization.Localization.GetText(68));
        this.UseButton.SetButtonRed();
      }
      else
        this.UseButton.SetText(SEngine.Localization.Localization.GetText(70));
    }

    private void Create(string Name, string Description)
    {
      this.Frame = new GameObject();
      this.Frame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Frame.SetDrawOriginToCentre();
      this.Frame.SetAllColours(ColourData.FernDarkBlue);
      this.VSCale = this.scaleHelper.ScaleVector2(new Vector2(155f, 130f));
      this.VSCale = this.scaleHelper.ScaleVector2(new Vector2(155f, 115f));
      this.VScaleSmall = this.scaleHelper.ScaleVector2(new Vector2(15f, 15f));
      Vector2 vector2 = this.VScaleSmall + this.VSCale;
      double baseScale = (double) this.BaseScale;
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.Y = (float) (-(double) vector2.Y * 0.5);
      zero.Y += this.scaleHelper.ScaleY(45f);
      this.simpletext = new SimpleTextHandler(Description, this.VSCale.X - defaultBuffer.X, true, this.BaseScale, true, true);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.SetAllColours(ColourData.Z_TextBrown);
      this.simpletext.Location.Y = zero.Y;
      this.simpletext.Location.Y = 0.0f;
      this.simpletext.Location.Y += this.simpletext.GetHeightOfOneLine() * 0.5f;
      zero.Y += this.simpletext.GetHeightOfParagraph();
      this.SelectionLerper = new LerpHandler_Float();
      this.UseButton = new TextButton(this.BaseScale, "Use", 45f);
      this.UseButton.AddControllerButton(GameFlags.GetCorrectButtonFace(ButtonPressed.Confirm));
      this.UseButton.CollisionEx = this.scaleHelper.ScaleVector2(new Vector2(10f, 20f));
      this.UseButton.vLocation.Y = (float) ((double) vector2.Y * 0.5 - (double) defaultBuffer.Y - (double) this.UseButton.GetSize_True().Y * 0.5);
      this.cellclose = new BackButton(true, BaseScale: this.BaseScale);
      this.cellclose.vLocation.X = (float) ((double) vector2.X * 0.5 - (double) defaultBuffer.X - (double) this.cellclose.GetSize().X * 0.5);
      this.cellclose.vLocation.Y = (float) (-(double) vector2.Y * 0.5 + (double) defaultBuffer.Y + (double) this.cellclose.GetSize().Y * 0.5);
      this.gameobjectninslice = new GameObjectNineSlice(new Rectangle(877, 350, 21, 21), 7);
    }

    public void SetNewExtraCellInfo(NewAnimalsInCellInfo info) => this.extraCellInfo.SetData(info);

    public void Select()
    {
      Z_GameFlags.SelectedCellInfo = this.REF_prisozone;
      if ((double) this.SelectionLerper.TargetValue == 1.0)
        return;
      this.SelectionLerper.SetLerp(false, 1f, 1f, 3f, true);
    }

    public void Deselect()
    {
      if ((double) this.SelectionLerper.TargetValue == 0.0)
        return;
      this.SelectionLerper.SetLerp(false, 1f, 0.0f, 3f, true);
    }

    public bool UpdateCellInfo(
      Player player,
      float DeltaTime,
      ref bool ExitBackToGame,
      out bool DeselectThis)
    {
      DeselectThis = false;
      this.SelectionLerper.UpdateLerpHandler(DeltaTime);
      this.Frame.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.LocationInWorldSpace);
      if ((double) this.SelectionLerper.Value == 0.0)
      {
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
          return MathStuff.CheckPointCollision(true, this.Frame.vLocation, 1f, this.VScaleSmall.X + this.scaleHelper.ScaleX(60f), this.VScaleSmall.Y + this.scaleHelper.ScaleY(60f), player.player.touchinput.ReleaseTapArray[0]);
      }
      else if ((double) this.SelectionLerper.Value == 1.0)
      {
        if (this.UseButton.UpdateTextButton(player, this.Frame.vLocation, DeltaTime))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          if (this.cellInfoButtonType != CellInfoButtonType.Full)
          {
            if (this.cellInfoButtonType == CellInfoButtonType.StorePrisoners)
            {
              ExitBackToGame = true;
              player.prisonlayout.cellblockcontainer.TransferPrisonersToHoldingCell_FromIntake(player.livestats.waveinfo, this.holdingcell, player);
              player.intakes.UseThis(player.livestats.intakefornextlevel, player, true);
              player.livestats.waveinfo = (WaveInfo) null;
              player.prisonlayout.cellblockcontainer.SetConsumption(player.livestats.consumptionstatus, player);
              player.OldSaveThisPlayer();
            }
            else if (TinyZoo.Game1.GetNextGameState() != GAMESTATE.GamePlaySetUp)
            {
              player.livestats.SelectedPrisonID = this.Cell_UID;
              player.livestats.AddNewAnimalsToEnclosure(player, player.livestats.SelectedPrisonID);
              ExitBackToGame = true;
              player.OldSaveThisPlayer();
              FeatureFlags.NewAnimalGot = true;
              Z_GameFlags.TopBarIsBlockedForTutorial = false;
            }
          }
        }
        else if (this.cellclose.UpdateBackButton(player, DeltaTime, this.Frame.vLocation))
        {
          DeselectThis = true;
          return true;
        }
      }
      return false;
    }

    public float GetDistanceFromScreenCenter()
    {
      this.Frame.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.LocationInWorldSpace);
      return (this.Frame.vLocation - Sengine.ReferenceScreenRes * 0.5f).Length();
    }

    public void DrawCellInfo()
    {
      this.Frame.vLocation = RenderMath.TranslateWorldSpaceToScreenSpace(this.LocationInWorldSpace);
      this.gameobjectninslice.scale = this.BaseScale;
      if ((double) this.SelectionLerper.Value > 0.0)
        this.gameobjectninslice.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Frame.vLocation, this.VScaleSmall + this.VSCale * this.SelectionLerper.Value);
      if ((double) this.SelectionLerper.Value > 0.0)
      {
        this.simpletext.paragraph.linemaker.SetAllColours(new Vector3(124f, 81f, 18f) / (float) byte.MaxValue);
        float num = MathHelper.Clamp((float) (((double) this.SelectionLerper.Value - 0.5) * 2.0), 0.0f, 1f);
        this.simpletext.DrawSimpleTextHandler(this.Frame.vLocation, num, AssetContainer.pointspritebatchTop05);
        this.UseButton.DrawTextButton(this.Frame.vLocation, num, AssetContainer.pointspritebatchTop05);
        this.cellclose.DrawBackButton(this.Frame.vLocation, AssetContainer.pointspritebatchTop05, num);
      }
      if ((double) this.SelectionLerper.Value >= 1.0)
        return;
      this.TouchIcon.scale = Math.Min(this.TIconScale, Sengine.WorldOriginandScale.Z * 2f);
      this.TouchIcon.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Frame.vLocation + new Vector2(0.0f, FlashingAlpha.MediumSin * 5f), 1f - this.SelectionLerper.Value);
    }
  }
}

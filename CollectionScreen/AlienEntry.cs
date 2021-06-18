// Decompiled with JetBrains decompiler
// Type: TinyZoo.CollectionScreen.AlienEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Objects;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Shelves;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.CollectionScreen
{
  internal class AlienEntry : GameObject
  {
    private AnimalInFrame animalInFrame;
    private bool UseNewRendering;
    private AnimalFoodIcon animalFoodIcon;
    public BaseFrame baseframe;
    public AnimalType anaimaltype;
    public CROPTYPE cropType = CROPTYPE.Count;
    public AnimalFoodType animalFoodType = AnimalFoodType.Count;
    public LerpHandler_Float existencelerper;
    private float LerpValue;
    private bool BlockDrawAlien;
    public bool BreedPartner;
    public bool IsUnlocked;
    public AnimalsForBreedInfo Ref_AnimalsForBreedInfo;
    private GameObject star;
    private ActiveIcon tick;
    private CustomerFrame extraFrame;
    private List<ZGenericText> textBelow_new;
    private MiniPixelBar miniPixelBar;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    public bool isMouseOver;
    private bool haSStringBelow;
    private string BelowString;
    private Color DrawThisColour;

    public AlienEntry(
      AnimalType _enemy,
      bool _IsUnlocked,
      bool Disovered,
      int VariantIndex = -1,
      float SCALEs = 3f,
      AnimalType animalHeadType = AnimalType.None,
      int headVariant = 0,
      CROPTYPE _cropType = CROPTYPE.Count,
      bool DrawGrownPlant = false,
      AnimalFoodType _animalFoodType = AnimalFoodType.Count)
    {
      this.BaseScale = SCALEs;
      this.IsUnlocked = _IsUnlocked;
      this.UseNewRendering = true;
      this.anaimaltype = _enemy;
      this.cropType = _cropType;
      this.animalFoodType = _animalFoodType;
      this.baseframe = new BaseFrame();
      this.baseframe.scale = SCALEs;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      if (this.UseNewRendering)
      {
        if (this.animalFoodType != AnimalFoodType.Count)
          this.animalFoodIcon = new AnimalFoodIcon(this.animalFoodType, 1f, this.BaseScale);
        else
          this.animalInFrame = new AnimalInFrame(_enemy, animalHeadType, VariantIndex, this.baseframe.GetSize().X * 0.8f, 6f * this.BaseScale, this.BaseScale, headVariant, this.cropType, DrawGrownPlant);
      }
      else
      {
        EnemyInfoPack enemyRectangle = EnemyData.GetEnemyRectangle(this.anaimaltype);
        if (VariantIndex == -1)
          VariantIndex = 0;
        if (enemyRectangle != null)
          this.DrawRect = enemyRectangle.GetIdleFrame(VariantIndex);
        else
          this.BlockDrawAlien = true;
        if (_enemy > AnimalType.SecretAnimalsCount)
        {
          this.DrawRect = enemyRectangle.WalkDown;
          this.SetDrawOriginToCentre();
        }
        this.scale = SCALEs;
        this.SetDrawOriginToCentre();
      }
      if (!_IsUnlocked)
      {
        if (Disovered)
          this.SetDiscovered();
        else
          this.SetLock();
      }
      this.existencelerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void SetLock()
    {
      this.IsUnlocked = false;
      if (this.UseNewRendering)
      {
        if (this.animalInFrame != null)
          this.animalInFrame.SetAnimalGreyedOut(true);
        else
          this.animalFoodIcon.SetIsUndiscovered();
        this.baseframe.SetAllColours(Color.Gray.ToVector3());
        if (this.extraFrame == null)
          return;
        this.extraFrame.Active = false;
      }
      else
      {
        this.SetAllColours(0.0f, 0.0f, 0.0f);
        this.SetAlpha(0.4f);
      }
    }

    public void SetUnlocked()
    {
      this.IsUnlocked = true;
      if (this.UseNewRendering)
      {
        if (this.animalInFrame != null)
          this.animalInFrame.SetAnimalGreyedOut(false, false);
        this.baseframe.SetAllColours(Color.White.ToVector3());
        if (this.extraFrame == null)
          return;
        this.extraFrame.Active = true;
      }
      else
      {
        this.SetAllColours(1f, 1f, 1f);
        this.SetAlpha(1f);
      }
    }

    public void SetDiscovered()
    {
      if (!this.UseNewRendering)
        return;
      if (this.animalInFrame != null)
        this.animalInFrame.Darken(true);
      this.baseframe.SetAllColours(Color.Gray.ToVector3());
      if (this.extraFrame == null)
        return;
      this.extraFrame.Active = false;
    }

    public void AddStar()
    {
      this.star = new GameObject();
      this.star.DrawRect = new Rectangle(65, 10, 15, 14);
      this.star.SetDrawOriginToCentre();
      this.star.scale = this.BaseScale * 0.5f;
      this.star.vLocation = new Vector2(10f, -10f * Sengine.ScreenRatioUpwardsMultiplier.Y) * this.BaseScale;
    }

    public void AddTick(bool removeTick)
    {
      if (removeTick)
      {
        this.tick = (ActiveIcon) null;
      }
      else
      {
        this.tick = new ActiveIcon(true, this.BaseScale, true);
        this.tick.vLocation = new Vector2(this.GetWidth() * 0.5f, (float) (-(double) this.GetHeight() * 0.5));
        ActiveIcon tick = this.tick;
        tick.vLocation = tick.vLocation + new Vector2((float) (-(double) this.tick.GetSize().X * 0.5), this.tick.GetSize().Y * 0.5f);
      }
    }

    public void AddMiniPixelBar(float barProgress, float barWidth, float barHeight)
    {
      this.miniPixelBar = new MiniPixelBar(this.BaseScale, barWidth, barHeight);
      this.miniPixelBar.SetBarValue(barProgress);
      this.miniPixelBar.SetEmpyBarColor(Color.Black.ToVector3());
      this.miniPixelBar.SetFillColor(ColourData.ThreeBluesForBabies[0]);
      this.miniPixelBar.location.Y = this.GetSize().Y * 0.5f;
      this.miniPixelBar.location.Y -= this.miniPixelBar.GetSize().Y * 0.5f;
      this.miniPixelBar.location.Y -= this.scaleHelper.ScaleY(2f);
      this.miniPixelBar.location.X -= this.miniPixelBar.GetSize().X * 0.5f;
    }

    public void SetUpLerper(int LerpIndex)
    {
      this.LerpValue = (float) LerpIndex * 0.05f;
      this.existencelerper.SetDelay(this.LerpValue);
    }

    public void Exit(int HighestLerpvalue)
    {
      this.existencelerper.SetLerp(true, 1f, 0.0f, 3f);
      this.existencelerper.SetDelay(this.LerpValue);
    }

    public void LerpIn() => this.existencelerper.SetLerp(true, 0.0f, 1f, 3f, true);

    public void SkipLerpIn() => this.existencelerper.Value = 1f;

    public bool UpdateAlienEntry(Vector2 Offset, float DeltaTime, Player player)
    {
      this.isMouseOver = false;
      if (this.extraFrame != null)
      {
        bool mouseOvered;
        int num = this.extraFrame.UpdateForMouseOverAndClick(player, DeltaTime, Offset + this.vLocation, out mouseOvered) ? 1 : 0;
        this.isMouseOver |= mouseOvered;
        if (num != 0)
          return true;
      }
      this.existencelerper.UpdateLerpHandler(DeltaTime);
      int num1 = this.baseframe.UpdateBaseFrame(Offset + this.vLocation, player) ? 1 : 0;
      this.isMouseOver |= this.baseframe.MouseOver;
      return num1 != 0;
    }

    public void AddStringBelow(string _BelowString, Color _DrawThisColour)
    {
      this.DrawThisColour = _DrawThisColour;
      this.haSStringBelow = true;
      this.BelowString = _BelowString;
    }

    public void AddStringBelow_NEW(
      string text,
      Vector3 color,
      float basescale,
      SpringFont useThisFont,
      int rowLayer = 0,
      bool RecreateIfExists_ElseUpdateString = true)
    {
      if (this.textBelow_new == null)
        this.textBelow_new = new List<ZGenericText>();
      this.haSStringBelow = true;
      if (this.textBelow_new.Count < rowLayer + 1 | RecreateIfExists_ElseUpdateString)
      {
        ZGenericText zgenericText = new ZGenericText(text, basescale, useThisFont);
        zgenericText.SetAllColours(color);
        zgenericText.vLocation.Y = this.GetHeight(true) * 0.5f;
        zgenericText.vLocation.Y += zgenericText.GetSize().Y * 0.5f;
        float num = 0.0f;
        for (int index = 0; index < rowLayer; ++index)
          num += this.textBelow_new[index].GetSize().Y;
        zgenericText.vLocation.Y += num;
        if (this.textBelow_new.Count < rowLayer + 1)
          this.textBelow_new.Add(zgenericText);
        else
          this.textBelow_new[rowLayer] = zgenericText;
      }
      else
        this.textBelow_new[rowLayer].textToWrite = text;
    }

    public void AddExtraFrame(
      Vector2 extraVScaleMargin_raw,
      Vector3 color,
      float customBuffer_X_raw = -1f,
      float customBuffer_Y_raw = -1f)
    {
      Vector2 size = this.GetSize(true);
      Vector2 vector2 = this.scaleHelper.ScaleVector2(extraVScaleMargin_raw);
      this.extraFrame = new CustomerFrame(size + vector2, color, this.BaseScale);
      this.extraFrame.location += this.extraFrame.VSCale * 0.5f;
      this.extraFrame.location -= size * 0.5f;
      if ((double) customBuffer_X_raw != -1.0 && (double) customBuffer_Y_raw != -1.0)
      {
        this.extraFrame.location -= this.scaleHelper.ScaleVector2(new Vector2(customBuffer_X_raw, customBuffer_Y_raw));
      }
      else
      {
        float num = Math.Min(extraVScaleMargin_raw.X, extraVScaleMargin_raw.Y);
        this.extraFrame.location -= this.scaleHelper.ScaleVector2(new Vector2(num, num)) * 0.5f;
      }
      this.extraFrame.OverrideDarkOverlayAlpha(0.15f);
    }

    public Vector2 GetOffsetFromCenter() => this.extraFrame != null ? this.extraFrame.location : this.vLocation;

    public Vector2 GetSize(bool GetAnimalFrameOnly = false) => new Vector2(this.GetWidth(GetAnimalFrameOnly), this.GetHeight(GetAnimalFrameOnly));

    public float GetHeight(bool GetAnimalFrameOnly = false) => this.extraFrame != null && !GetAnimalFrameOnly ? this.extraFrame.VSCale.Y : this.baseframe.GetSize().Y;

    public float GetWidth(bool GetAnimalFrameOnly = false) => this.extraFrame != null && !GetAnimalFrameOnly ? this.extraFrame.VSCale.X : this.baseframe.GetSize().X;

    public void DrawAlienEntry(Vector2 Offset, SpriteBatch DrawWithThis, bool SkipDrawFrame = false)
    {
      if (this.isMouseOver)
        Offset.Y += 2f;
      if (this.extraFrame != null)
      {
        this.extraFrame.DrawCustomerFrame(Offset + this.vLocation, DrawWithThis);
        this.extraFrame.DrawDarkOverlay(Offset + this.vLocation, DrawWithThis);
      }
      if (!SkipDrawFrame)
        this.baseframe.DrawBaseFrame(Offset + this.vLocation, DrawWithThis, this.existencelerper.Value);
      if (this.star != null)
      {
        this.star.fAlpha = FlashingAlpha.Medium.fAlpha * 0.5f;
        this.star.fAlpha += 0.5f;
        this.star.Draw(DrawWithThis, AssetContainer.SpriteSheet, Offset + this.vLocation, this.fAlpha * this.existencelerper.Value);
      }
      if (this.tick != null)
      {
        this.tick.fAlpha = this.fAlpha * this.existencelerper.Value;
        this.tick.DrawActiveIcon(DrawWithThis, Offset + this.vLocation);
      }
      if (!this.BlockDrawAlien)
      {
        if (this.UseNewRendering)
        {
          if (this.animalInFrame != null)
            this.animalInFrame.JustDrawAnimal(Offset + this.vLocation, DrawWithThis, this.fAlpha * this.existencelerper.Value);
          else if (this.animalFoodIcon != null)
            this.animalFoodIcon.DrawAnimalFoodIcon(Offset + this.vLocation, false, DrawWithThis);
        }
        else
          this.Draw(DrawWithThis, AssetContainer.AnimalSheet, Offset, this.fAlpha * this.existencelerper.Value);
      }
      if (this.haSStringBelow)
      {
        if (this.textBelow_new != null)
        {
          for (int index = 0; index < this.textBelow_new.Count; ++index)
            this.textBelow_new[index].DrawZGenericText(Offset + this.vLocation, DrawWithThis, this.fAlpha * this.existencelerper.Value);
        }
        else
          TextFunctions.DrawJustifiedText(this.BelowString, 3f, this.vLocation + Offset + new Vector2(0.0f, 30f * this.scale), this.DrawThisColour, 1f, AssetContainer.springFont, DrawWithThis);
      }
      if (this.miniPixelBar != null)
        this.miniPixelBar.DrawMiniPixelBar(Offset + this.vLocation, DrawWithThis);
      if (this.extraFrame == null)
        return;
      this.extraFrame.PostDrawMouseoverOverlay(Offset + this.vLocation, DrawWithThis);
    }
  }
}

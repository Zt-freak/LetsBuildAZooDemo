// Decompiled with JetBrains decompiler
// Type: TinyZoo.CollectionScreen.CollectionScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.DragHandlers;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection;
using TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectionPanel;
using TinyZoo.OverWorld.Research;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;

namespace TinyZoo.CollectionScreen
{
  internal class CollectionScreenManager
  {
    public Vector2 location;
    private List<AlienEntry> alienentries;
    private StoreBGManager storeBG;
    private BackButton backbutton;
    private SpringDrag_ZoneManager springdrag;
    private int HighestLerpvalue;
    private bool Exiting;
    private LerpHandler_Float EXlerper;
    private SelectedPersonInfo selectedperson;
    public AnimalType SelectedAnimal = AnimalType.None;
    public AnimalType SecondSelectedAnimal = AnimalType.None;
    private CharacterNeeds characterneed;
    private BuildMatrix buildmatrix;
    private float Gap_X;
    private float Gap_Y;
    private bool skipFirstSound;
    private bool IsPetSelect;
    private bool IsBreedSelector;
    private bool IsCRISPR_Selector;
    private bool IsCustomSelection;
    private int MaxThisRow;
    public int mouseOveredIndex;
    public Z_ScrollHelper scrollHelper;
    private UIScaleHelper scaleHelper;
    public AnimalType enemy = AnimalType.None;

    public CollectionScreenManager(
      Player player,
      bool HasBG = false,
      bool _IsPetSelect = false,
      bool _IsBreedSelector = false,
      bool _IsCRISPR_Selector = false,
      float BaseScale = -1f,
      bool isCustomSelection_addEntriesLater = false)
    {
      this.IsCRISPR_Selector = _IsCRISPR_Selector;
      this.IsBreedSelector = _IsBreedSelector;
      this.IsCustomSelection = isCustomSelection_addEntriesLater;
      if (HasBG)
        this.storeBG = new StoreBGManager();
      this.IsPetSelect = _IsPetSelect;
      this.selectedperson = (SelectedPersonInfo) null;
      this.Exiting = false;
      this.backbutton = new BackButton();
      this.alienentries = new List<AlienEntry>();
      float num = RenderMath.GetPixelSizeBestMatch(2f);
      if ((double) BaseScale != -1.0)
        num = BaseScale;
      this.scaleHelper = new UIScaleHelper(num);
      if (isCustomSelection_addEntriesLater)
        return;
      List<AlienEntry> _alienEntries = new List<AlienEntry>();
      List<AnimalType> animalTypeList = new List<AnimalType>();
      animalTypeList.Add(AnimalType.Rabbit);
      animalTypeList.AddRange((IEnumerable<AnimalType>) ResearchData.GetAliensReseachedInOrder());
      for (int index = 0; index < animalTypeList.Count; ++index)
      {
        if (!this.IsPetSelect || player.Stats.GetAliensCaptured(animalTypeList[index]) > 0)
        {
          if (this.IsCRISPR_Selector)
          {
            int totalVaiantsFound = player.Stats.GetTotalVaiantsFound(animalTypeList[index]);
            float barProgress = (float) totalVaiantsFound / 10f;
            _alienEntries.Add(new AlienEntry(animalTypeList[index], player.Stats.IsThisGenomeMapped(animalTypeList[index]), totalVaiantsFound > 0, SCALEs: num));
            if ((double) barProgress > 0.0 && (double) barProgress != 1.0)
              _alienEntries[_alienEntries.Count - 1].AddMiniPixelBar(barProgress, _alienEntries[_alienEntries.Count - 1].GetSize().X - this.scaleHelper.ScaleX(5f), this.scaleHelper.ScaleY(2f));
          }
          else
            _alienEntries.Add(new AlienEntry(animalTypeList[index], player.Stats.GetAliensCaptured(animalTypeList[index]) > 0, false, SCALEs: num));
        }
      }
      for (int Index = 0; Index < player.inventory.SecretAliensAvailable.Length; ++Index)
      {
        if (player.inventory.SecretAliensAvailable[Index])
        {
          AnimalType thisSecretAlien = player.inventory.GetThisSecretAlien(Index);
          _alienEntries.Add(new AlienEntry(thisSecretAlien, player.Stats.GetAliensCaptured(thisSecretAlien) > 0, true, SCALEs: num));
        }
      }
      int _MaxThisRow = 8;
      if (this.IsPetSelect)
        _MaxThisRow = 7;
      this.AddAndPositionEntries(_alienEntries, num, this.IsBreedSelector || this.IsCRISPR_Selector, _MaxThisRow, this.IsCRISPR_Selector);
    }

    public void AddAndPositionEntries(
      List<AlienEntry> _alienEntries,
      float SCALE,
      bool AlphaFrame = false,
      int _MaxThisRow = 8,
      bool SkipExistenceLerp = false,
      float overrideBufferX = -1f,
      float overrideBufferY = -1f)
    {
      this.alienentries = _alienEntries;
      int num1 = 0;
      int num2 = 0;
      this.MaxThisRow = _MaxThisRow;
      Vector2 vector2 = this.scaleHelper.ScaleVector2(Vector2.One * 5f);
      if ((double) overrideBufferX != -1.0)
        vector2.X = overrideBufferX;
      if ((double) overrideBufferY != -1.0)
        vector2.Y = overrideBufferY;
      this.Gap_X = this.alienentries[0].GetWidth() + vector2.X;
      this.Gap_Y = this.alienentries[0].GetHeight() + vector2.Y;
      int num3 = 0;
      for (int index = 0; index < this.alienentries.Count; ++index)
      {
        this.alienentries[index].vLocation = new Vector2((float) num2 * this.Gap_X, (float) num1 * this.Gap_Y);
        if (SkipExistenceLerp)
          this.alienentries[index].SkipLerpIn();
        else
          this.alienentries[index].SetUpLerper(num3 + num2);
        this.HighestLerpvalue = num3 + num2;
        ++num2;
        if (num2 >= this.MaxThisRow)
        {
          num2 = 0;
          ++num1;
          ++num3;
        }
        this.alienentries[index].vLocation.X += 70f;
        if (AlphaFrame)
          this.alienentries[index].baseframe.SetAlpha(0.4f);
      }
      this.EXlerper = new LerpHandler_Float();
      this.buildmatrix = new BuildMatrix(this.MaxThisRow, this.alienentries.Count, 0);
      this.springdrag = new SpringDrag_ZoneManager((float) (((double) this.alienentries[this.alienentries.Count - 1].vLocation.Y - 650.0) * -1.0), new Vector2(70f, 0.0f), new Vector2(this.Gap_X * 10f, 768f));
      this.skipFirstSound = true;
    }

    public void SetUpForBreedSelect(AnimalsForBreedInfo breed, bool IsNew)
    {
      for (int index = 0; index < this.alienentries.Count; ++index)
      {
        if (this.alienentries[index].anaimaltype == breed.animaltype)
        {
          this.alienentries[index].Ref_AnimalsForBreedInfo = breed;
          this.alienentries[index].SetUnlocked();
          this.alienentries[index].baseframe.SetAlpha(1f);
          if (IsNew)
            this.alienentries[index].AddStar();
        }
      }
    }

    public AnimalsForBreedInfo GetBreed(AnimalType animaltyps)
    {
      for (int index = 0; index < this.alienentries.Count; ++index)
      {
        if (this.alienentries[index].anaimaltype == animaltyps)
          return this.alienentries[index].Ref_AnimalsForBreedInfo;
      }
      return (AnimalsForBreedInfo) null;
    }

    public void AddScroll(float maxHeight) => this.scrollHelper = new Z_ScrollHelper(new Vector2(this.GetWidth(), this.GetHeight()), maxHeight);

    public float GetHeight() => (float) (0.0 + ((double) this.alienentries[this.alienentries.Count - 1].vLocation.Y - (double) this.alienentries[0].vLocation.Y)) + this.alienentries[0].GetHeight();

    public float GetWidth() => (float) (0.0 + ((double) this.alienentries[Math.Min(this.MaxThisRow, this.alienentries.Count) - 1].vLocation.X - (double) this.alienentries[0].vLocation.X)) + this.alienentries[0].GetWidth();

    public Vector2 GetOffsetFromTopLeft() => new Vector2(this.alienentries[0].vLocation.X - this.alienentries[0].GetWidth() * 0.5f, this.alienentries[0].vLocation.Y - this.alienentries[0].GetHeight() * 0.5f + this.alienentries[0].GetOffsetFromCenter().Y);

    public List<AlienEntry> GetAllEntries() => this.alienentries;

    public AlienEntry GetThisEntry(int index) => this.alienentries[index];

    public AlienEntry GetMouseOverEntry() => this.mouseOveredIndex == -1 ? (AlienEntry) null : this.alienentries[this.mouseOveredIndex];

    public AnimalType UpdateCollectionScreenManager(
      Vector2 Offset,
      float DeltaTime,
      Player player,
      out bool ExitDone,
      out bool JustConfirmedSelection)
    {
      Offset += this.location;
      JustConfirmedSelection = false;
      ExitDone = false;
      this.mouseOveredIndex = -1;
      bool flag = true;
      if (this.scrollHelper != null)
      {
        this.scrollHelper.UpdateZ_ScrollHelper(player, Offset + this.GetOffsetFromTopLeft());
        Offset.Y += this.scrollHelper.YscrollOffset;
        flag = this.scrollHelper.PointerInZone;
      }
      if (this.storeBG != null)
        this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (GameFlags.IsUsingController && !GameFlags.IsUsingMouse)
      {
        int num = (int) this.enemy / 5;
        if (num > 0)
          --num;
        if (Math.Round((double) num * -(double) this.Gap_Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y) != (double) this.springdrag.CurrentOffset.Y && (double) this.EXlerper.TargetValue != Math.Round((double) num * -(double) this.Gap_Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y))
          this.EXlerper.SetLerp(true, this.springdrag.CurrentOffset.Y, (float) Math.Round((double) num * -(double) this.Gap_Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y), 3f, true);
        this.EXlerper.UpdateLerpHandler(DeltaTime);
        if (!this.IsBreedSelector && !this.IsCRISPR_Selector && !this.IsCustomSelection)
          this.springdrag.CurrentOffset.Y = this.EXlerper.Value;
      }
      else if (!this.IsBreedSelector && !this.IsCRISPR_Selector && !this.IsCustomSelection)
        this.springdrag.UpdateSpringDrag_ZoneManager(player.player.touchinput, 100f);
      if (GameFlags.IsUsingController && this.buildmatrix.UpdateBuildMatrix(player, DeltaTime))
      {
        if (this.skipFirstSound)
          this.skipFirstSound = false;
        else
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
        this.enemy = this.alienentries[this.buildmatrix.Selected].anaimaltype;
        this.selectedperson = new SelectedPersonInfo(this.enemy, player);
        for (int index = 0; index < this.alienentries.Count; ++index)
          this.alienentries[index].isMouseOver = false;
        this.alienentries[this.buildmatrix.Selected].isMouseOver = false;
        if (player.Stats.research.HasThisAnimalBeenResearched(this.enemy) || this.enemy == AnimalType.Rabbit)
          this.characterneed = new CharacterNeeds(this.enemy);
        else if (this.enemy < AnimalType.SecretAnimalsCount && this.enemy > AnimalType.None)
        {
          for (int Index = 0; Index < player.inventory.SecretAliensAvailable.Length; ++Index)
          {
            if (player.inventory.SecretAliensAvailable[Index] && player.inventory.GetThisSecretAlien(Index) == this.enemy)
            {
              this.characterneed = new CharacterNeeds(this.enemy);
              break;
            }
          }
        }
        else
          this.characterneed = (CharacterNeeds) null;
      }
      if (!this.IsPetSelect && this.backbutton.UpdateBackButton(player, DeltaTime) && !this.Exiting)
      {
        if (this.storeBG != null)
          ExitDone = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuClose);
        this.Exiting = true;
        for (int index = 0; index < this.alienentries.Count; ++index)
          this.alienentries[index].Exit(this.HighestLerpvalue);
        if (this.selectedperson != null)
          this.selectedperson.LerpOff(true);
        this.backbutton.Exit();
      }
      for (int index = 0; index < this.alienentries.Count; ++index)
      {
        if (this.alienentries[index].UpdateAlienEntry(Offset + this.springdrag.CurrentOffset, DeltaTime, player))
        {
          if (this.IsCRISPR_Selector)
          {
            if (this.alienentries[index].IsUnlocked)
            {
              if (this.SelectedAnimal == AnimalType.None)
                this.SelectedAnimal = this.alienentries[index].anaimaltype;
              else if (this.SecondSelectedAnimal == AnimalType.None)
              {
                this.SecondSelectedAnimal = this.alienentries[index].anaimaltype;
              }
              else
              {
                this.SelectedAnimal = this.alienentries[index].anaimaltype;
                this.SecondSelectedAnimal = AnimalType.None;
              }
              this.buildmatrix.ForceSelect(index);
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
              JustConfirmedSelection = true;
            }
          }
          else if (this.enemy != this.alienentries[index].anaimaltype)
          {
            if (!this.IsCustomSelection || this.IsCustomSelection && this.alienentries[index].IsUnlocked)
            {
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
              this.enemy = this.alienentries[index].anaimaltype;
              this.buildmatrix.ForceSelect(index);
            }
            if (this.IsCRISPR_Selector)
            {
              if (this.SelectedAnimal == AnimalType.None)
                this.SelectedAnimal = this.enemy;
              else if (this.SecondSelectedAnimal == AnimalType.None)
              {
                this.SecondSelectedAnimal = this.enemy;
              }
              else
              {
                this.SelectedAnimal = this.enemy;
                this.SecondSelectedAnimal = AnimalType.None;
              }
              JustConfirmedSelection = true;
            }
            else if (!this.IsBreedSelector && !this.IsCustomSelection)
              this.selectedperson = new SelectedPersonInfo(this.enemy, player);
            else
              JustConfirmedSelection = true;
            if (player.Stats.GetTotalVaiantsFound(this.enemy) > 0)
            {
              if (this.IsPetSelect)
              {
                ExitDone = true;
                this.SelectedAnimal = this.enemy;
              }
              else
                this.characterneed = new CharacterNeeds(this.enemy);
            }
            else if (this.enemy < AnimalType.SecretAnimalsCount && this.enemy > AnimalType.None)
            {
              for (int Index = 0; Index < player.inventory.SecretAliensAvailable.Length; ++Index)
              {
                if (player.inventory.SecretAliensAvailable[Index] && player.inventory.GetThisSecretAlien(Index) == this.enemy)
                {
                  if (!this.IsPetSelect)
                  {
                    this.characterneed = new CharacterNeeds(this.enemy);
                    break;
                  }
                  break;
                }
              }
            }
            else
              this.characterneed = (CharacterNeeds) null;
          }
        }
        if (this.alienentries[index].isMouseOver)
          this.mouseOveredIndex = index;
      }
      if (this.Exiting && !this.IsPetSelect && (!this.IsBreedSelector && !this.IsCRISPR_Selector) && (!this.IsCustomSelection && this.backbutton.ExitComplete()))
      {
        ExitDone = true;
        for (int index = 0; index < this.alienentries.Count; ++index)
        {
          if ((double) this.alienentries[index].existencelerper.Value != 0.0 && this.storeBG == null)
            ExitDone = false;
        }
      }
      if (this.selectedperson != null)
        this.selectedperson.UpdateSelectedPersonInfo(DeltaTime, player, out bool _, out bool _);
      if (!flag)
      {
        this.enemy = AnimalType.None;
        this.mouseOveredIndex = -1;
      }
      return this.enemy;
    }

    public void DrawCollectionScreenManager(Vector2 Offset) => this.DrawCollectionScreenManager(Offset, AssetContainer.pointspritebatchTop05);

    public void DrawCollectionScreenManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      if (this.scrollHelper != null)
        Offset.Y += this.scrollHelper.YscrollOffset;
      if (this.storeBG != null)
        this.storeBG.DrawStoreBGManager(Vector2.Zero);
      for (int index = 0; index < this.alienentries.Count; ++index)
      {
        bool flag = false;
        if (this.scrollHelper != null && !this.scrollHelper.CheckIfShouldDrawThis(this.alienentries[index].vLocation.Y - this.alienentries[index].GetOffsetFromCenter().Y, (float) ((double) this.alienentries[index].vLocation.Y + (double) this.alienentries[index].GetSize(true).Y * 0.5 - (double) this.alienentries[index].GetOffsetFromCenter().Y + (double) this.alienentries[index].GetSize().Y * 0.5)))
          flag = true;
        if (!flag)
        {
          if (this.selectedperson != null && this.selectedperson.ThisPerson == this.alienentries[index].anaimaltype)
            this.alienentries[index].isMouseOver = true;
          this.alienentries[index].DrawAlienEntry(Offset + this.springdrag.CurrentOffset, spritebatch);
        }
      }
      if (this.selectedperson != null && !this.IsPetSelect && (!this.IsBreedSelector && !this.IsCRISPR_Selector) && !this.IsCustomSelection)
      {
        Vector2 ESOffset = new Vector2(0.0f, -100f * Sengine.ScreenRatioUpwardsMultiplier.Y);
        ESOffset.Y += 100f;
        this.selectedperson.DrawSelectedPersonInfo(ESOffset);
        CharacterNeeds characterneed = this.characterneed;
      }
      if (this.IsPetSelect || this.IsBreedSelector || (this.IsCRISPR_Selector || this.IsCustomSelection))
        return;
      this.backbutton.DrawBackButton(Offset);
    }
  }
}

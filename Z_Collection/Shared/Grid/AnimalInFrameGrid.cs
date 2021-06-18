// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Shared.Grid.AnimalInFrameGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Shared.Grid
{
  internal class AnimalInFrameGrid
  {
    public Vector2 location;
    public List<AnimalRenderDescriptor> refAnimals;
    private PlusInFrameButton plusButton;
    private AnimalInFrame plusNumberFrame;
    private int numberPerRow;
    private int numberExceeded;
    private float BaseScale;
    private bool DrawWithoutFrames;

    public List<AnimalInFrame> animalFrames { get; private set; }

    public AnimalInFrameGrid(
      float BaseScale,
      int numberPerRow,
      float Xbuffer,
      float Ybuffer,
      List<AnimalRenderDescriptor> animals,
      int maxFramesToDisplay = -1,
      bool centerJustify = false,
      bool UseNumberFrameWhenMaxFrames_NotButton = false,
      float rawFrameSize = 25f,
      bool _DrawWithoutFrames = false)
    {
      this.Create(BaseScale, numberPerRow, Xbuffer, Ybuffer, animals, maxFramesToDisplay, centerJustify, UseNumberFrameWhenMaxFrames_NotButton, rawFrameSize, _DrawWithoutFrames);
    }

    public AnimalInFrameGrid(
      float BaseScale,
      int numberPerRow,
      float Xbuffer,
      float Ybuffer,
      List<AnimalType> animalTypes,
      int maxFramesToDisplay = -1,
      bool centerJustify = false,
      bool UseNumberFrameWhenMaxFrames_NotButton = false,
      float rawFrameSize = 25f,
      bool _DrawWithoutFrames = false)
    {
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      for (int index = 0; index < animalTypes.Count; ++index)
        animals.Add(new AnimalRenderDescriptor(animalTypes[index]));
      this.Create(BaseScale, numberPerRow, Xbuffer, Ybuffer, animals, maxFramesToDisplay, centerJustify, UseNumberFrameWhenMaxFrames_NotButton, rawFrameSize, _DrawWithoutFrames);
    }

    public AnimalInFrameGrid(
      float BaseScale,
      int numberPerRow,
      float Xbuffer,
      float Ybuffer,
      List<PrisonerInfo> prisonerInfos,
      int maxFramesToDisplay = -1,
      bool centerJustify = false,
      bool UseNumberFrameWhenMaxFrames_NotButton = false,
      float rawFrameSize = 25f,
      bool _DrawWithoutFrames = false)
    {
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      foreach (PrisonerInfo prisonerInfo in prisonerInfos)
      {
        if (prisonerInfo == null)
          animals.Add(new AnimalRenderDescriptor(AnimalType.None));
        else
          animals.Add(new AnimalRenderDescriptor(prisonerInfo.intakeperson.animaltype, prisonerInfo.intakeperson.CLIndex, prisonerInfo.intakeperson.HeadType, prisonerInfo.intakeperson.HeadVariant, _IsFemale: prisonerInfo.intakeperson.IsAGirl));
      }
      this.Create(BaseScale, numberPerRow, Xbuffer, Ybuffer, animals, maxFramesToDisplay, centerJustify, UseNumberFrameWhenMaxFrames_NotButton, rawFrameSize, _DrawWithoutFrames);
    }

    private void Create(
      float _BaseScale,
      int _numberPerRow,
      float Xbuffer,
      float Ybuffer,
      List<AnimalRenderDescriptor> animals,
      int maxFramesToDisplay = -1,
      bool centerJustify = false,
      bool UseNumberFrameWhenMaxFrames_NotButton = false,
      float rawFrameSize = 25f,
      bool _DrawWithoutFrames = false)
    {
      this.BaseScale = _BaseScale;
      this.numberPerRow = _numberPerRow;
      this.DrawWithoutFrames = _DrawWithoutFrames;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      bool flag = false;
      this.numberExceeded = animals.Count - maxFramesToDisplay;
      if (maxFramesToDisplay != -1 && this.numberExceeded > 0)
      {
        flag = true;
        animals.RemoveRange(maxFramesToDisplay - 1, animals.Count - (maxFramesToDisplay - 1));
      }
      this.refAnimals = animals;
      this.animalFrames = new List<AnimalInFrame>();
      Vector2 vector2_1 = new Vector2(rawFrameSize, rawFrameSize);
      Vector2 vector2_2 = uiScaleHelper.ScaleVector2(vector2_1);
      for (int index = 0; index < animals.Count; ++index)
      {
        AnimalType animal = animals[index].bodyAnimalType;
        if (animal == AnimalType.None)
          animal = AnimalType.Rabbit;
        AnimalInFrame animalInFrame = new AnimalInFrame(animal, animals[index].headAnimalType, animals[index].variant, rawFrameSize * this.BaseScale, BaseScale: this.BaseScale, HeadVariant: animals[index].headVariant, croptype: animals[index].cropType, DrawGrownPlant: true);
        if (!animals[index].IsUnlocked)
          animalInFrame.SetAnimalGreyedOut(true);
        else if (!animals[index].IsAvailable)
          animalInFrame.Darken(true);
        animalInFrame.Location.X = (vector2_2.X + Xbuffer) * (float) (index % this.numberPerRow);
        animalInFrame.Location.Y = (vector2_2.Y + Ybuffer) * (float) (index / this.numberPerRow);
        animalInFrame.Location += vector2_2 * 0.5f;
        this.animalFrames.Add(animalInFrame);
      }
      if (flag)
      {
        Vector2 zero = Vector2.Zero;
        zero.X = (vector2_2.X + Xbuffer) * (float) (animals.Count % this.numberPerRow);
        zero.Y = (vector2_2.Y + Ybuffer) * (float) (animals.Count / this.numberPerRow);
        zero += vector2_2 * 0.5f;
        if (UseNumberFrameWhenMaxFrames_NotButton)
        {
          this.plusNumberFrame = new AnimalInFrame(AnimalType.Rabbit, AnimalType.None, TargetSize: (25f * this.BaseScale), FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale);
          this.plusNumberFrame.Location = zero;
        }
        else
        {
          this.plusButton = new PlusInFrameButton(this.BaseScale);
          this.plusButton.location = zero;
        }
      }
      if (!centerJustify)
        return;
      int num1 = (int) Math.Ceiling((double) animals.Count / (double) this.numberPerRow);
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int num2 = Math.Min(animals.Count - this.numberPerRow * index1, this.numberPerRow);
        if (this.plusButton != null & index1 == num1 - 1)
          ++num2;
        float num3 = (float) ((double) num2 * (double) vector2_2.X + (double) Xbuffer * (double) (num2 - 1));
        for (int index2 = 0; index2 < num2; ++index2)
        {
          if (this.plusButton != null && index1 == num1 - 1 && index2 == num2 - 1)
            this.plusButton.location.X -= num3 * 0.5f;
          else
            this.animalFrames[index2 + this.numberPerRow * index1].Location.X -= num3 * 0.5f;
        }
      }
    }

    public Vector2 GetSize()
    {
      Vector2 size = this.animalFrames[0].GetSize();
      float y = this.animalFrames[this.animalFrames.Count - 1].Location.Y - this.animalFrames[0].Location.Y + size.Y;
      return new Vector2(this.animalFrames[Math.Min(this.animalFrames.Count, this.numberPerRow) - 1].Location.X - this.animalFrames[0].Location.X + size.X, y);
    }

    public Vector2 GetFrameRelativePosition(int index) => this.animalFrames[index].Location;

    public int UpdateForMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.animalFrames.Count; ++index)
      {
        if (this.animalFrames[index].UpdateForMouseOver(player, offset))
          return index;
      }
      return -1;
    }

    public int UpdateForMouseOverAndClicks(Player player, Vector2 offset)
    {
      offset += this.location;
      int num = this.UpdateForMouseOver(player, offset);
      return num != -1 && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 ? num : -1;
    }

    public bool UpdateForPlusButton(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.plusButton != null && this.plusButton.UpdatePlusInFrameButton(player, DeltaTime, offset);
    }

    public void DrawAnimalInFrameGrid(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.animalFrames.Count; ++index)
      {
        if (this.refAnimals[index].bodyAnimalType == AnimalType.None && this.refAnimals[index].cropType == CROPTYPE.Count)
          this.animalFrames[index].DrawPlusMore(offset, spriteBatch, -1, this.BaseScale, AssetContainer.SpringFontX1AndHalf, customString: "?");
        else if (this.DrawWithoutFrames)
        {
          this.animalFrames[index].JustDrawAnimal(offset, spriteBatch);
          this.animalFrames[index].DrawMouseOver(offset, spriteBatch);
        }
        else
        {
          this.animalFrames[index].DrawAnimalInFrame(offset, spriteBatch, true);
          this.animalFrames[index].DrawMouseOver(offset, spriteBatch);
        }
      }
      if (this.plusButton != null)
        this.plusButton.DrawPlusInFrameButton(offset, spriteBatch);
      if (this.plusNumberFrame == null)
        return;
      this.plusNumberFrame.DrawPlusMore(offset, spriteBatch, this.numberExceeded, this.BaseScale, AssetContainer.SpringFontX1AndHalf);
    }
  }
}

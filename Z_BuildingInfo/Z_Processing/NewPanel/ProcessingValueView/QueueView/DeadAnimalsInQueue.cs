// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView.DeadAnimalsInQueue
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Animals.Incineration;
using TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.QueueView
{
  internal class DeadAnimalsInQueue
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;
    private List<DeadAnimal> refDeadAnimals;
    private AnimalInFrameGrid animalGrid;
    private UIScaleHelper scaleHelper;
    private float BaseScale;
    private Z_ScrollHelper scrollHelper;
    private Vector2 contentsSize;
    private string baseString;
    private FactoryProductionLine reffactoryproduction;
    private float lastStockCount;
    private int lastMouseOverIndex;
    private CustomerFrameMouseOverBox mouseOverBox;
    private FireIcon fireIcon;

    public DeadAnimalsInQueue(
      FactoryProductionLine factoryproduction,
      float _BaseScale,
      float ForcedHeight,
      float ForcedWidth,
      CustomerFrameColors frameColor = CustomerFrameColors.DarkBrown)
    {
      this.BaseScale = _BaseScale;
      this.reffactoryproduction = factoryproduction;
      this.lastMouseOverIndex = -1;
      this.customerFrame = new CustomerFrame(new Vector2(ForcedWidth, ForcedHeight), frameColor, this.BaseScale);
      this.baseString = "Waiting for Processing Queue: ";
      if (TileData.IsAnIncinerator(factoryproduction.factorytiletype))
        this.baseString = "Queue: ";
      this.miniHeading = new MiniHeading(this.customerFrame.VSCale, this.baseString + (object) 0, 1f, this.BaseScale);
      this.SetUp();
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void SetUp()
    {
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      Vector2 vector2 = this.scaleHelper.ScaleVector2(Vector2.One * 35f);
      int numberPerRow = (int) Math.Floor(((double) this.customerFrame.VSCale.X - (double) defaultBuffer.X) / ((double) vector2.X + (double) defaultBuffer.X));
      int num = (int) Math.Floor(((double) this.customerFrame.VSCale.Y - (double) this.miniHeading.GetTextHeight(true) - (double) defaultBuffer.Y) / ((double) vector2.Y + (double) defaultBuffer.Y));
      this.refDeadAnimals = this.reffactoryproduction.GetDeadAnimalQueue();
      List<AnimalInFrame> animalInFrameList = new List<AnimalInFrame>();
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      if (this.refDeadAnimals != null && this.refDeadAnimals.Count > 0)
      {
        for (int index = 0; index < this.refDeadAnimals.Count; ++index)
        {
          bool flag = false;
          AnimalRenderDescriptor renderDescriptor = new AnimalRenderDescriptor(this.refDeadAnimals[index].animalType, this.refDeadAnimals[index].variant, this.refDeadAnimals[index].headType, this.refDeadAnimals[index].headVariant, _IsAvailable: (!flag), _IsFemale: this.refDeadAnimals[index].IsAGirl, _cropType: this.refDeadAnimals[index].cropType);
          animals.Add(renderDescriptor);
        }
      }
      this.animalGrid = new AnimalInFrameGrid(this.BaseScale, numberPerRow, defaultBuffer.X, defaultBuffer.Y, animals, numberPerRow * num, UseNumberFrameWhenMaxFrames_NotButton: true, rawFrameSize: 35f, _DrawWithoutFrames: true);
      this.animalGrid.location.Y += this.miniHeading.GetTextHeight(true) + defaultBuffer.Y;
      this.animalGrid.location.X = defaultBuffer.X;
      this.animalGrid.location -= this.customerFrame.VSCale * 0.5f;
      this.lastStockCount = (float) this.GetStockCountIncludingCurrent();
      this.miniHeading.SetNewText(this.baseString + (object) this.GetStockCountIncludingCurrent());
      this.fireIcon = (FireIcon) null;
      if (this.refDeadAnimals.Count <= 0)
        return;
      this.fireIcon = new FireIcon(this.BaseScale);
      this.fireIcon.vLocation = this.animalGrid.animalFrames[0].Location + this.animalGrid.location;
      FireIcon fireIcon = this.fireIcon;
      fireIcon.vLocation = fireIcon.vLocation - this.animalGrid.animalFrames[0].GetSize(true) * 0.5f;
    }

    private int GetStockCountIncludingCurrent()
    {
      int stockToDisplay = this.reffactoryproduction.GetStockToDisplay();
      if (this.reffactoryproduction.IsCurrentlyManufacturing())
        ++stockToDisplay;
      return stockToDisplay;
    }

    public void AddScroll(float maxHeight)
    {
    }

    public void UpdateDeadAnimalsInQueue(Player player, Vector2 offset)
    {
      offset += this.location;
      if ((double) this.GetStockCountIncludingCurrent() != (double) this.lastStockCount)
        this.SetUp();
      int index = this.animalGrid.UpdateForMouseOver(player, offset);
      if (index != -1)
      {
        if (this.lastMouseOverIndex == -1 || index != this.lastMouseOverIndex)
        {
          this.CreateMouseOver(this.animalGrid.animalFrames[index], this.refDeadAnimals[index]);
          this.lastMouseOverIndex = index;
        }
        this.mouseOverBox.Active = true;
      }
      else
      {
        if (this.mouseOverBox == null)
          return;
        this.mouseOverBox.Active = false;
      }
    }

    private void CreateMouseOver(AnimalInFrame entry, DeadAnimal deadAnimal)
    {
      string str = "Weight: " + (object) deadAnimal.weight + "kg";
      string stringFomMinutes = Z_GameFlags.GetTimeAsStringFomMinutes((int) Math.Floor((double) IncinerationCalculator.GetMinutesToProcess(deadAnimal, this.reffactoryproduction.EmployeeProductivityMultiplier, TileData.IsAnIncinerator(this.reffactoryproduction.factorytiletype))));
      this.mouseOverBox = new CustomerFrameMouseOverBox(this.BaseScale, !TileData.IsAnIncinerator(this.reffactoryproduction.factorytiletype) ? str + "~EST. Processing Time: " + stringFomMinutes : str + "~EST. Burning Time: " + stringFomMinutes, 250f * this.BaseScale);
      this.mouseOverBox.location = entry.Location + this.animalGrid.location;
      this.mouseOverBox.location.Y -= this.scaleHelper.ScaleY(5f);
    }

    public void DrawDeadAnimalsInQueue(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.animalGrid.DrawAnimalInFrameGrid(offset, spriteBatch);
      if (this.mouseOverBox != null)
        this.mouseOverBox.DrawCustomerFrameMouseOverBoc(offset, spriteBatch);
      if (this.fireIcon == null)
        return;
      this.fireIcon.DrawFireIcon(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup.EditCullingFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Popup
{
  internal class EditCullingFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalInFrame animal;
    private ZGenericText animalName;
    private SimpleTextHandler desc;
    private List<CullingOption> cullingOptions;
    private CullingType currentlySelectedOption;

    public EditCullingFrame(AnimalType animalType, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 _VSCale = defaultBuffer;
      float num1 = uiScaleHelper.ScaleX(200f);
      this.animal = new AnimalInFrame(animalType, AnimalType.None, TargetSize: (25f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
      this.animalName = new ZGenericText(EnemyData.GetEnemyTypeName(animalType), BaseScale, false, _UseOnePointFiveFont: true);
      this.desc = new SimpleTextHandler("Edit how you would like this animal to be culled in this building's zones.", num1 - defaultBuffer.X * 2f, true, BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location.Y = _VSCale.Y;
      this.desc.Location.Y += this.desc.GetHeightOfOneLine() * 0.5f;
      _VSCale.Y += this.desc.GetHeightOfParagraph();
      _VSCale.Y += defaultBuffer.Y;
      this.animal.Location.Y = _VSCale.Y;
      this.animal.Location.Y += this.animal.GetSize().Y * 0.5f;
      this.animalName.vLocation.X = this.animal.Location.X + this.animal.GetSize().X * 0.5f + defaultBuffer.X;
      this.animalName.vLocation.Y = this.animal.Location.Y - this.animalName.GetSize().Y * 0.5f;
      float num2 = this.animalName.GetSize().X + this.animal.GetSize().X;
      this.animalName.vLocation.X -= num2 * 0.5f;
      this.animal.Location.X -= num2 * 0.5f;
      _VSCale.Y += this.animal.GetSize().Y;
      _VSCale.Y += defaultBuffer.Y;
      this.cullingOptions = new List<CullingOption>();
      for (int index = 0; index < 3; ++index)
      {
        CullingOption cullingOption = new CullingOption((CullingType) index, BaseScale);
        cullingOption.location = _VSCale;
        _VSCale.Y += cullingOption.GetSize().Y;
        _VSCale.Y += defaultBuffer.Y;
        this.cullingOptions.Add(cullingOption);
      }
      _VSCale.X = num1;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      for (int index = 0; index < this.cullingOptions.Count; ++index)
        this.cullingOptions[index].location += vector2;
      this.desc.Location.Y += vector2.Y;
      this.animal.Location.Y += vector2.Y;
      this.animalName.vLocation.Y += vector2.Y;
      this.currentlySelectedOption = CullingType.Count;
      this.SetActiveOption(CullingType.None);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void SetActiveOption(CullingType cullingType)
    {
      if (this.currentlySelectedOption == cullingType)
        return;
      this.currentlySelectedOption = cullingType;
      for (int index = 0; index < this.cullingOptions.Count; ++index)
        this.cullingOptions[index].SetIsActive(this.cullingOptions[index].refCullingType == this.currentlySelectedOption);
    }

    private void OnClickCheckbox(CullingType cullingType) => this.SetActiveOption(cullingType);

    public void UpdateEditCullingFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.cullingOptions.Count; ++index)
      {
        if (this.cullingOptions[index].UpdateCullingOption(player, DeltaTime, offset))
          this.OnClickCheckbox(this.cullingOptions[index].refCullingType);
      }
    }

    public void DrawEditCullingFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      for (int index = 0; index < this.cullingOptions.Count; ++index)
        this.cullingOptions[index].DrawCullingOption(offset, spriteBatch);
      this.animal.DrawAnimalInFrame(offset, spriteBatch);
      this.animalName.DrawZGenericText(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}

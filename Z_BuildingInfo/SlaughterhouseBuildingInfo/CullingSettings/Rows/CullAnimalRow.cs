// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows.CullAnimalRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings.Rows
{
  internal class CullAnimalRow
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalInFrame animalInFrame;
    private ZGenericText name;
    private DragAndBar dragAndBar;
    private TextButton textButton;
    private ZGenericText dragBarLabel;
    private bool DrawDragBar;

    public AnimalType refAnimalType { get; private set; }

    public CullAnimalRow(AnimalType animalType, Player player, float BaseScale)
    {
      this.refAnimalType = animalType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      Vector2 _VSCale = defaultBuffer;
      this.animalInFrame = new AnimalInFrame(animalType, AnimalType.None, TargetSize: (25f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
      this.name = new ZGenericText(EnemyData.GetEnemyTypeName(animalType), BaseScale, false, _UseOnePointFiveFont: true);
      this.textButton = new TextButton(BaseScale, "Edit");
      CullingType cullingType = (CullingType) Game1.Rnd.Next(0, 3);
      string _textToWrite = string.Empty;
      this.DrawDragBar = true;
      switch (cullingType)
      {
        case CullingType.None:
          _textToWrite = "Culling Disabled";
          this.DrawDragBar = false;
          break;
        case CullingType.Weight:
          _textToWrite = "Cull By Weight: 15kg";
          break;
        case CullingType.Age:
          _textToWrite = "Cull By Age: 50 Days";
          break;
      }
      this.dragBarLabel = new ZGenericText(_textToWrite, BaseScale, false);
      this.dragAndBar = new DragAndBar(false, 0.5f, uiScaleHelper.ScaleX(100f), BaseScale);
      this.dragAndBar.HideHandle = true;
      this.animalInFrame.Location = _VSCale;
      this.animalInFrame.Location += this.animalInFrame.GetSize() * 0.5f;
      _VSCale.X += this.animalInFrame.GetSize().X;
      _VSCale.X += defaultBuffer.X;
      this.name.vLocation = _VSCale;
      _VSCale.Y += this.name.GetSize().Y;
      this.dragBarLabel.vLocation = _VSCale;
      _VSCale.Y += this.dragBarLabel.GetSize().Y;
      this.dragAndBar.Location = _VSCale;
      this.dragAndBar.Location += this.dragAndBar.GetSize() * 0.5f;
      _VSCale += this.dragAndBar.GetSize();
      _VSCale.Y += defaultBuffer.Y;
      _VSCale.X += defaultBuffer.X;
      this.textButton.vLocation.X = _VSCale.X;
      this.textButton.vLocation.Y = _VSCale.Y * 0.5f;
      this.textButton.vLocation.X += this.textButton.GetSize().X * 0.5f;
      _VSCale.X += this.textButton.GetSize().X;
      _VSCale.X += defaultBuffer.X;
      this.animalInFrame.Location.Y = _VSCale.Y * 0.5f;
      this.customerFrame = new CustomerFrame(_VSCale, CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.animalInFrame.Location += vector2;
      ZGenericText name = this.name;
      name.vLocation = name.vLocation + vector2;
      this.dragAndBar.Location += vector2;
      ZGenericText dragBarLabel = this.dragBarLabel;
      dragBarLabel.vLocation = dragBarLabel.vLocation + vector2;
      TextButton textButton = this.textButton;
      textButton.vLocation = textButton.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateCullAnimalRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.textButton.UpdateTextButton(player, offset, DeltaTime);
    }

    public void DrawCullAnimalRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
      this.name.DrawZGenericText(offset, spriteBatch);
      if (this.DrawDragBar)
        this.dragAndBar.DrawDragAndBar(spriteBatch, offset);
      this.dragBarLabel.DrawZGenericText(offset, spriteBatch);
      this.textButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}

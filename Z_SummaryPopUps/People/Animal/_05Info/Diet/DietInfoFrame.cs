// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Diet.DietInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._05Info.Diet
{
  internal class DietInfoFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private AnimalDietIcons foodIcons;
    private ZGenericText text;

    public DietInfoFrame(AnimalType animalType, Player player, float width, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Diet");
      this.text = new ZGenericText("Type: " + AnimalData.GetDietTypeToString(AnimalData.GetAnimalStat(animalType).diettype), BaseScale, false);
      this.foodIcons = new AnimalDietIcons(animalType, BaseScale, player, false);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.X += defaultBuffer.X;
      this.text.vLocation = zero;
      zero.Y += this.text.GetSize().Y;
      this.foodIcons.location = zero;
      this.foodIcons.location += this.foodIcons.GetSize() * 0.5f;
      this.foodIcons.location.X -= uiScaleHelper.ScaleX(5f);
      zero.Y += this.foodIcons.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      zero.X = width;
      this.customerFrame.Resize(zero);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.foodIcons.location += vector2;
      ZGenericText text = this.text;
      text.vLocation = text.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateDietInfoFrame()
    {
    }

    public void DrawDietInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.foodIcons.DrawAnimalDietIcons(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}

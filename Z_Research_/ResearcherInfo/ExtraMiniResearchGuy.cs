// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.ResearcherInfo.ExtraMiniResearchGuy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.RatingPopUp.Row.Columns;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Research_.ResearcherInfo
{
  internal class ExtraMiniResearchGuy
  {
    private CustomerFrame customerFrame;
    private ZGenericText nameText;
    private SatisfactionBarWithBigNumber bar;
    private AnimalInFrame animalInFrame;
    private Employee employee;

    public ExtraMiniResearchGuy(Employee _employee, float BaseScale, bool IsForResearchBuilding = false)
    {
      this.employee = _employee;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      defaultBuffer.X *= 0.5f;
      string name = this.employee.quickemplyeedescription.NAME;
      this.animalInFrame = new AnimalInFrame(this.employee.quickemplyeedescription.thisemployee, AnimalType.None, TargetSize: (25f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
      this.nameText = new ZGenericText(name, BaseScale, false, _UseOnePointFiveFont: true);
      this.bar = new SatisfactionBarWithBigNumber(BaseScale);
      Vector2 zero = Vector2.Zero;
      zero.X += defaultBuffer.X;
      zero.Y += defaultBuffer.Y;
      this.animalInFrame.Location = zero;
      this.animalInFrame.Location += this.animalInFrame.GetSize() * 0.5f;
      zero.X += this.animalInFrame.GetSize().X;
      zero.X += defaultBuffer.X;
      Vector2 vector2_1 = zero;
      this.nameText.vLocation = vector2_1;
      vector2_1.Y += this.nameText.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y;
      this.bar.location = vector2_1;
      Vector2 vector2_2 = vector2_1 + this.bar.GetSize();
      zero.Y += Math.Max(this.animalInFrame.GetSize().Y, vector2_2.Y);
      zero.X += vector2_2.X;
      zero.Y += defaultBuffer.Y;
      Vector3 color = ColourData.Z_FrameMidBrown;
      if (IsForResearchBuilding)
        color = ColourData.Z_FrameBluePale;
      this.customerFrame = new CustomerFrame(zero, color, BaseScale);
      Vector2 vector2_3 = -this.customerFrame.VSCale * 0.5f;
      this.animalInFrame.Location += vector2_3;
      ZGenericText nameText = this.nameText;
      nameText.vLocation = nameText.vLocation + vector2_3;
      this.bar.location += vector2_3;
      this.SetData();
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private void SetData() => this.bar.SetBarValues((float) this.employee.ResearchProgress / 100f);

    public void UpdateExtraMiniResearchGuy() => this.SetData();

    public void DrawExtraMiniResearchGuy(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
      this.nameText.DrawZGenericText(offset, spriteBatch);
      this.bar.DrawSatisfactionBarWithBigNumber(offset, spriteBatch);
    }
  }
}

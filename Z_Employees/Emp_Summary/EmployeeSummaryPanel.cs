// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.EmployeeSummaryPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Employees.Emp_Summary.Hiring;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_Employees.Emp_Summary
{
  internal class EmployeeSummaryPanel
  {
    private AnimalInFrame animalInFrame;
    private SatisfactionBarManager satisfactionBarManager;
    public CustomerFrame brownFrame;
    public Vector2 location;
    private string Name;
    private GameObject nameObject;
    private Vector2 topleft;
    private bool AllowSalaryChange;
    private SalarySliderBarWithText paySlider;
    private float totalHeight;
    private float totalWidth;

    public EmployeeSummaryPanel(
      QuickEmployeeDescription emplyeedescription,
      bool HasSalarySlider,
      bool _AllowSalaryChange = true,
      float BaseScale = 1f,
      bool darkerFrame = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.totalWidth += defaultXbuffer;
      this.totalHeight += defaultYbuffer * 0.5f;
      this.Name = emplyeedescription.NAME;
      this.nameObject = new GameObject();
      this.nameObject.SetAllColours(ColourData.Z_Cream);
      this.nameObject.scale = BaseScale;
      this.nameObject.vLocation = new Vector2(this.totalWidth, this.totalHeight);
      this.totalHeight += (SpringFontUtil.MeasureString(this.Name, AssetContainer.SpringFontX1AndHalf) * this.nameObject.scale * Sengine.ScreenRatioUpwardsMultiplier.Y).Y;
      this.totalHeight += defaultYbuffer * 0.5f;
      this.animalInFrame = new AnimalInFrame(emplyeedescription.thisemployee, AnimalType.None, TargetSize: (50f * BaseScale), BaseScale: (BaseScale * 3f));
      this.animalInFrame.Location = new Vector2(this.totalWidth, this.totalHeight);
      this.animalInFrame.Location += this.animalInFrame.FrameVSCALE * 0.5f * Sengine.ScreenRatioUpwardsMultiplier;
      this.totalWidth += this.animalInFrame.FrameVSCALE.X;
      this.totalWidth += defaultXbuffer * 2f;
      this.satisfactionBarManager = new SatisfactionBarManager(emplyeedescription, BaseScale);
      this.satisfactionBarManager.Location = new Vector2(this.totalWidth, this.totalHeight);
      Vector2 size1 = this.satisfactionBarManager.GetSize();
      this.satisfactionBarManager.Location += this.satisfactionBarManager.GetOffsetFromTopLeft();
      this.totalHeight += size1.Y;
      this.totalWidth += size1.X;
      this.totalWidth += defaultXbuffer;
      this.totalHeight += defaultYbuffer * 2f;
      if (HasSalarySlider)
      {
        this.paySlider = new SalarySliderBarWithText(emplyeedescription, BaseScale, this.totalWidth, defaultXbuffer, defaultYbuffer);
        this.paySlider.location.Y = this.totalHeight;
        Vector2 size2 = this.paySlider.GetSize();
        this.paySlider.location.Y += this.paySlider.GetOffsetFromTop();
        this.totalHeight += size2.Y;
        this.totalHeight += defaultYbuffer;
      }
      this.topleft = new Vector2((float) (-(double) this.totalWidth * 0.5) + AnimalPopUpManager.VerticalBuffer, (float) (-(double) this.totalWidth * 0.5) + AnimalPopUpManager.VerticalBuffer);
      this.AllowSalaryChange = _AllowSalaryChange;
      this.brownFrame = new CustomerFrame(new Vector2(this.totalWidth, this.totalHeight), darkerFrame, BaseScale);
      Vector2 vector2 = new Vector2((float) (-(double) this.brownFrame.VSCale.X * 0.5), (float) (-(double) this.brownFrame.VSCale.Y * 0.5));
      GameObject nameObject = this.nameObject;
      nameObject.vLocation = nameObject.vLocation + vector2;
      this.animalInFrame.Location += vector2;
      this.satisfactionBarManager.Location += vector2;
      if (this.paySlider == null)
        return;
      this.paySlider.location += vector2;
    }

    public void PreviewStatChange(int index, float newvalue) => this.satisfactionBarManager.PreviewChange(index, newvalue, ColourData.LogGreen, ColourData.Z_BarRed);

    public void ApplyChange(int index) => this.satisfactionBarManager.ApplyChange(index);

    public float GetStatValue(int index) => this.satisfactionBarManager.GetValue(index);

    public Vector2 GetSize() => new Vector2(this.totalWidth, this.totalHeight);

    public int GetSalarySet() => this.paySlider.GetPaySliderSalaryValue();

    public void UpdateEmployeeSummary(float DeltaTime, Player player, Vector2 offset)
    {
      offset += this.location;
      if (this.paySlider != null && this.AllowSalaryChange)
        this.paySlider.UpdateSalarySliderBarWithText(player, DeltaTime, offset);
      this.satisfactionBarManager.UpdateSatisfactionBarManager(offset, player);
    }

    public void DrawEmployeeSummary(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.brownFrame.DrawCustomerFrame(offset, spriteBatch);
      TextFunctions.DrawTextWithDropShadow(this.Name, this.nameObject.scale, this.nameObject.vLocation + offset, this.nameObject.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false);
      this.animalInFrame.DrawAnimalInFrame(offset);
      if (this.paySlider != null)
        this.paySlider.DrawSalarySliderBarWithText(offset, spriteBatch);
      this.satisfactionBarManager.DrawSatisfactionBarManager_InverseOrder(offset);
    }
  }
}

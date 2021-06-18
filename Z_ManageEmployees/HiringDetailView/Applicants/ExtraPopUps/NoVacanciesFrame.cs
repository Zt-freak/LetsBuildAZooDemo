// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ExtraPopUps.NoVacanciesFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ExtraPopUps
{
  internal class NoVacanciesFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler text;

    public NoVacanciesFrame(float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.customerFrame = new CustomerFrame(Vector2.Zero, ColourData.Z_FrameRedPale, BaseScale);
      this.text = new SimpleTextHandler("You do not have any vacancies to hire a new employee! Fire an employee to free up a vacancy.", 300f * BaseScale, true, BaseScale, AutoComplete: true);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.Location.Y -= this.text.GetHeightOfOneLine() * 0.5f;
      this.customerFrame.Resize(this.text.GetSize(true) + uiScaleHelper.DefaultBuffer * 2f);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateNoVacanciesFrame(Player player, float DeltaTime, Vector2 offset) => offset += this.location;

    public void DrawNoVacanciesFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}

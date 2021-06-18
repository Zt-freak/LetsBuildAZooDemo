// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.GeneralEmployees.NotHere.NotHereManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Employees.GeneralEmployees.NotHere
{
  internal class NotHereManager
  {
    private CustomerFrame customerFrame;
    public Vector2 Location;
    private MiniHeading miniheading;
    private SimpleTextHandler simpletext;
    private AnimalInFrame _animalinframe;
    private TextButton back;

    public NotHereManager(
      Player player,
      EmployeeType employeetype,
      float BaseScale,
      float UnmultipliedWidth,
      AnimalInFrame animalinframe,
      int TotalSHopsBuild)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this._animalinframe = animalinframe;
      this.customerFrame = new CustomerFrame(new Vector2(UnmultipliedWidth * BaseScale, 150f), BaseScale: BaseScale);
      string text = SEngine.Localization.Localization.GetText(955);
      string TextToWrite = Employees.GetEmployeeThypeToString(employeetype) + ":" + SEngine.Localization.Localization.GetText(956);
      if (TotalSHopsBuild == 0)
      {
        text = SEngine.Localization.Localization.GetText(957);
        TextToWrite = SEngine.Localization.Localization.GetText(958);
      }
      this.miniheading = new MiniHeading(this.customerFrame.VSCale, text, 1f, BaseScale);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.miniheading.GetTextHeight(true);
      Vector2 vector2_1 = zero + defaultBuffer;
      vector2_1.Y += defaultBuffer.Y * 2f;
      vector2_1.X += this._animalinframe.GetSize().X;
      vector2_1.X += defaultBuffer.X;
      float width_ = (float) ((double) uiScaleHelper.ScaleX(UnmultipliedWidth) - (double) vector2_1.X - (double) defaultBuffer.X * 2.0);
      this.simpletext = new SimpleTextHandler(TextToWrite, width_, true, BaseScale);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location = vector2_1;
      this.simpletext.Location.X += width_ * 0.5f;
      this.simpletext.Location.Y += this.simpletext.GetHeightOfOneLine() * 0.5f;
      vector2_1.Y += this.simpletext.GetHeightOfParagraph();
      vector2_1.Y += defaultBuffer.Y;
      this.back = new TextButton(BaseScale, SEngine.Localization.Localization.GetText(936));
      this.back.vLocation.Y = vector2_1.Y;
      this.back.vLocation.Y += this.back.GetSize_True().Y * 0.5f;
      this.back.vLocation.X = this.simpletext.Location.X;
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.simpletext.Location += vector2_2;
      TextButton back = this.back;
      back.vLocation = back.vLocation + vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    internal static bool ShouldDisplayNotHere(EmployeeType employeetype)
    {
      switch (employeetype)
      {
        case EmployeeType.Keeper:
        case EmployeeType.Vet:
        case EmployeeType.Architect:
        case EmployeeType.ShopKeeper:
        case EmployeeType.Breeder:
        case EmployeeType.DNAResearcher:
        case EmployeeType.MeatProcessorWorker:
        case EmployeeType.SlaughterhouseEmployee:
        case EmployeeType.FactoryWorker:
        case EmployeeType.Farmer:
        case EmployeeType.VegProcessorWorker:
        case EmployeeType.WarehouseWorker:
          return true;
        default:
          return false;
      }
    }

    public bool UpdateNotHereManager(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      return this.back.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawNotHereManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerFrame.DrawCustomerFrame(Offset, spritebatch);
      this.miniheading.DrawMiniHeading(Offset);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      Vector2 location = this._animalinframe.Location;
      this._animalinframe.DrawAnimalInFrame(Offset, spritebatch);
      this._animalinframe.Location = location;
      this.back.DrawTextButton(Offset, 1f, spritebatch);
    }
  }
}

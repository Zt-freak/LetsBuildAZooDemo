// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.AnimalDeliveryUI.AnimalDeliveryListEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.AnimalDeliveryUI
{
  internal class AnimalDeliveryListEntry
  {
    private static float rawXSize = 260f;
    private static float rawheadingallowance = 100f;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private MouseoverHandler mouseoverhandler;
    private AnimalInFrame portrait;
    private ZGenericText headingText;
    private ZGenericText etaText;
    private IntakePerson intakeperson;
    private bool disableinput;
    private static Color cream = new Color(ColourData.Z_Cream);

    public IntakePerson AnimalInfo => this.intakeperson;

    public AnimalOrder Ref_AnimalsOnOrder { get; private set; }

    public AnimalDeliveryListEntry(
      float basescale_,
      IntakePerson _intakeperson,
      AnimalOrder animalsonorder,
      bool darker = false)
    {
      this.Ref_AnimalsOnOrder = animalsonorder;
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.intakeperson = _intakeperson;
      this.portrait = new AnimalInFrame(_intakeperson.animaltype, _intakeperson.HeadType, _intakeperson.CLIndex, 20f * this.basescale, BaseScale: this.basescale, HeadVariant: _intakeperson.HeadVariant);
      string _textToWrite = EnemyData.GetEnemyTypeName(_intakeperson.animaltype);
      if (_intakeperson.HeadType != AnimalType.None)
        _textToWrite = HybridNames.GetAnimalCombinedName(_intakeperson.animaltype, _intakeperson.HeadType);
      this.headingText = new ZGenericText(_textToWrite, this.basescale, false, _UseOnePointFiveFont: true);
      this.etaText = new ZGenericText("XX", this.basescale, false);
      this.framescale.X = this.uiscale.ScaleX(AnimalDeliveryListEntry.rawXSize);
      this.framescale.Y = this.portrait.GetSize().Y + 1f * defaultBuffer.Y;
      Vector2 size1 = this.etaText.GetSize();
      Vector2 size2 = this.headingText.GetSize();
      size2.X = this.uiscale.ScaleX(AnimalDeliveryListEntry.rawheadingallowance);
      this.frame = new CustomerFrame(this.framescale, darker, this.basescale);
      this.mouseoverhandler = new MouseoverHandler(this.framescale.X, this.framescale.Y, this.basescale);
      Vector2 vector2 = new Vector2();
      vector2.X = (float) (-0.5 * (double) this.framescale.X + 0.5 * (double) defaultBuffer.X);
      this.portrait.Location = vector2;
      this.portrait.Location.X += 0.5f * this.portrait.GetSize().X;
      vector2.X += this.portrait.GetSize().X + 0.5f * defaultBuffer.X;
      this.headingText.vLocation = vector2;
      this.headingText.vLocation.Y = (float) (0.0 - 0.5 * (double) size2.Y);
      vector2.X += size2.X + defaultBuffer.X;
      this.etaText.vLocation = vector2;
      this.etaText.vLocation.Y = (float) (0.0 - 0.5 * (double) size1.Y);
      this.SetString();
    }

    private void SetString() => this.etaText.textToWrite = AnimalDeliveryListEntry.GetETAString(this.Ref_AnimalsOnOrder);

    public static string GetETAString(AnimalOrder Ref_AnimalsOnOrder)
    {
      string empty = string.Empty;
      string str1;
      if (Ref_AnimalsOnOrder.DaysToArrival > 0)
      {
        string str2 = string.Concat((object) Ref_AnimalsOnOrder.DaysToArrival);
        str1 = Ref_AnimalsOnOrder.DaysToArrival != 1 ? str2 + " " + SEngine.Localization.Localization.GetText(975) : str2 + " Day";
      }
      else
      {
        int untilThisInHours = Z_GameFlags.GetTimeUntilThisInHours((float) Ref_AnimalsOnOrder.SecondInDayOrArrival);
        if (untilThisInHours < 1)
        {
          int untilThisInMinutes = Z_GameFlags.GetTimeUntilThisInMinutes((float) Ref_AnimalsOnOrder.SecondInDayOrArrival);
          str1 = untilThisInMinutes > 0 ? (untilThisInMinutes != 1 ? untilThisInMinutes.ToString() + " mins" : untilThisInMinutes.ToString() + " min") : SEngine.Localization.Localization.GetText(976);
        }
        else
          str1 = untilThisInHours != 1 ? untilThisInHours.ToString() + " hrs" : untilThisInHours.ToString() + " hr";
      }
      return str1;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateAnimalDeliveryListEntry(
      Player player,
      Vector2 offset,
      float DeltaTime,
      bool disableinput_ = false)
    {
      this.disableinput = disableinput_;
      offset += this.location;
      this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      int num = this.mouseoverhandler.Clicked ? 1 : 0;
      this.SetString();
      return num != 0 && !this.disableinput;
    }

    public void DrawAnimalDeliveryListEntry(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.portrait.DrawAnimalInFrame(offset, spritebatch);
      this.etaText.DrawZGenericText(offset, spritebatch);
      this.headingText.DrawZGenericText(offset, spritebatch);
      if (this.disableinput)
        return;
      this.mouseoverhandler.DrawMouseOverHandler(spritebatch, offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.PredictionTable
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Predict;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage
{
  internal class PredictionTable
  {
    public Vector2 Location;
    public CustomerFrame customerframe;
    private MiniHeading miniHeading;
    private PredictionTableType tabletype;
    private SimpleTextHandler simpletext;
    private Animal_Bar_Button animalBarAndButton;

    public PredictionTable(
      PredictionTableType _tabletype,
      Player player,
      ActiveBreed babyBreed,
      PrisonerInfo Mother,
      float forceWidth,
      float BaseScale,
      string CustomText = "",
      Parents_AndChild parents_and_child_FORNURSING = null)
    {
      this.tabletype = _tabletype;
      float defaultYbuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      float num1 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, PredictionTable.GetPredictionTableTypeToHeaderText(this.tabletype), 1f, BaseScale);
      float num2 = num1 + (this.miniHeading.GetTextHeight(true) + defaultYbuffer);
      string TextToWrite = "";
      if (this.tabletype == PredictionTableType.Abortion)
      {
        if (player.unlocks.UnlockedThings[202] > -1)
        {
          if (babyBreed != null)
            TextToWrite = "This animal will be born with no defects, and is a new variant for your zoo!";
        }
        else
          TextToWrite = "Unlock the Ultrasound upgrade to view the offspring.";
        this.animalBarAndButton = new Animal_Bar_Button(PredictionTable.GetPredictionTableTypeToButtonText(this.tabletype), player, babyBreed, Mother, parents_and_child_FORNURSING, BaseScale);
      }
      else if (this.tabletype == PredictionTableType.Nursing)
      {
        TextToWrite = CustomText;
        this.animalBarAndButton = new Animal_Bar_Button(PredictionTable.GetPredictionTableTypeToButtonText(this.tabletype), player, babyBreed, Mother, parents_and_child_FORNURSING, BaseScale);
      }
      else if (this.tabletype == PredictionTableType.ReturnToPen)
      {
        num2 += defaultYbuffer * 0.5f;
        TextToWrite = CustomText;
        this.animalBarAndButton = new Animal_Bar_Button(PredictionTable.GetPredictionTableTypeToButtonText(this.tabletype), player, baseScale: BaseScale);
      }
      this.animalBarAndButton.location.Y = num2 + this.animalBarAndButton.GetHeight() * 0.5f;
      float y = num2 + this.animalBarAndButton.GetHeight() + defaultYbuffer;
      if (!string.IsNullOrEmpty(TextToWrite))
      {
        this.simpletext = new SimpleTextHandler(TextToWrite, true, (float) ((double) forceWidth / (double) Sengine.ReferenceScreenRes.X * 0.899999976158142), BaseScale);
        this.simpletext.AutoCompleteParagraph();
        this.simpletext.SetAllColours(ColourData.Z_Cream);
        this.simpletext.Location.Y = y;
        y += this.simpletext.GetHeightOfParagraph();
      }
      this.customerframe = new CustomerFrame(new Vector2(forceWidth, y), CustomerFrameColors.Brown, BaseScale);
      this.miniHeading.SetTextPosition(this.customerframe.VSCale);
      Vector2 vector2 = -this.customerframe.VSCale * 0.5f;
      this.animalBarAndButton.location.Y += vector2.Y;
      this.simpletext.Location.Y += vector2.Y;
    }

    public static string GetPredictionTableTypeToButtonText(PredictionTableType type)
    {
      switch (type)
      {
        case PredictionTableType.Abortion:
          return "Abort";
        case PredictionTableType.Nursing:
          return "Skip";
        case PredictionTableType.ReturnToPen:
          return "Return";
        default:
          return "NA";
      }
    }

    public static string GetPredictionTableTypeToHeaderText(PredictionTableType type)
    {
      switch (type)
      {
        case PredictionTableType.Abortion:
          return "Ultrasound Preview";
        case PredictionTableType.Nursing:
          return "Nursing";
        case PredictionTableType.ReturnToPen:
          return "Return To Enclosure";
        default:
          return "NA";
      }
    }

    public bool UpdatePredictionTable(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      return this.animalBarAndButton.UpdateAnimal_Bar_Button(player, DeltaTime, Offset);
    }

    public void DrawPredictionTable(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.miniHeading.DrawMiniHeading(Offset, spritebatch);
      this.animalBarAndButton.DrawAnimal_Bar_Button(Offset, spritebatch);
      if (this.simpletext == null)
        return;
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}

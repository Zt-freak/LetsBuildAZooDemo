// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.AbortConfirm.AbortConfirmationManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_BreedScreen.AbortConfirm
{
  internal class AbortConfirmationManager
  {
    private Parents_AndChild REF_parents_and_child;
    public BigBrownPanel bigBrownPanel;
    private AnimalInFrame animalframe;
    public Vector2 Location;
    private TextButton Abort;
    private SimpleTextHandler simpletext;
    private LerpHandler_Float Lerper;

    public AbortConfirmationManager(Parents_AndChild _REF_parents_and_child)
    {
      this.REF_parents_and_child = _REF_parents_and_child;
      float num1 = 0.0f;
      float x = 400f;
      this.Lerper = new LerpHandler_Float();
      this.Lerper.SetLerp(true, 1f, 0.0f, 3f, true);
      float num2 = num1 + MiniHeading.GetHeight(BigBrownPanel.GetHeaderScale()) + AnimalPopUpManager.VerticalBuffer;
      this.animalframe = new AnimalInFrame(_REF_parents_and_child.animaltype, AnimalType.None, _REF_parents_and_child.ChildVariant, 50f);
      this.animalframe.Location.Y = num2 + this.animalframe.FrameVSCALE.Y * 0.5f;
      float num3 = num2 + this.animalframe.FrameVSCALE.Y + AnimalPopUpManager.VerticalBuffer + AnimalPopUpManager.VerticalBuffer;
      this.simpletext = new SimpleTextHandler("Are you sure you would like to abort this baby?", true, (float) ((double) x / (double) Sengine.ReferenceScreenRes.X * 0.899999976158142), 2f);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.Location.Y = num3;
      float num4 = num3 + this.simpletext.GetHeightOfParagraph();
      this.Abort = new TextButton("Confirm", 50f, OverAllMultiplier: 0.75f);
      this.Abort.SetButtonColour(BTNColour.Red);
      this.Abort.vLocation.Y = num4 + this.Abort.GetVScale().Y * 0.5f;
      float y = num4 + this.Abort.GetVScale().Y + AnimalPopUpManager.VerticalBuffer + AnimalPopUpManager.VerticalBuffer;
      this.bigBrownPanel = new BigBrownPanel(new Vector2(x, y), true, "Confirm Abort");
    }

    public bool BlockUnderPanelUpdate() => (double) this.Lerper.Value != 1.0;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset.X += this.Lerper.Value * BreedPopUp.LerpDistance;
      offset += this.Location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateAbortConfirmationManager(float DeltaTime, Player player, Vector2 Offset)
    {
      this.Lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.Lerper.Value == 0.0 && (double) this.Lerper.TargetValue == 0.0)
      {
        Offset += this.Location;
        bool flag = this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, Offset);
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !MathStuff.CheckPointCollision(true, Offset, 1f, this.bigBrownPanel.vScale.X, this.bigBrownPanel.vScale.Y, player.player.touchinput.ReleaseTapArray[0]))
          flag = true;
        if (flag)
          this.LerpOff();
        Offset.Y -= this.bigBrownPanel.vScale.Y * 0.5f;
        if (this.Abort != null && this.Abort.UpdateTextButton(player, Offset, DeltaTime))
        {
          this.LerpOff();
          player.breeds.AbortThisBaby(this.REF_parents_and_child, player);
          return true;
        }
      }
      return false;
    }

    public void LerpOff() => this.Lerper.SetLerp(false, 0.0f, 1f, 3f, true);

    public void DrawAbortConfirmationManager(SpriteBatch spritebatch, Vector2 Offset)
    {
      if ((double) this.Lerper.Value == 1.0)
        return;
      Offset.X += this.Lerper.Value * BreedPopUp.LerpDistance;
      Offset += this.Location;
      this.bigBrownPanel.DrawBigBrownPanel(Offset, spritebatch);
      Offset.Y -= this.bigBrownPanel.vScale.Y * 0.5f;
      if (this.animalframe != null)
      {
        this.animalframe.DrawAnimalInFrame(Offset);
        this.Abort.DrawTextButton(Offset, 1f, spritebatch);
      }
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}

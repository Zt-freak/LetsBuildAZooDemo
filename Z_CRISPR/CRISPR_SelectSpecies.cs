// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CRISPR.CRISPR_SelectSpecies
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_BreedScreen;
using TinyZoo.Z_CRISPR.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_CRISPR
{
  internal class CRISPR_SelectSpecies
  {
    public Vector2 location;
    private LerpHandler_Float lerper;
    private CollectionScreenManager collection;
    private BigBrownPanel bigBrownPanel;
    private TextButton textButton;
    private AnimalType[] animalsSelected;
    private CRISPR_PreviewFrame previewFrame;
    private SpeciesMouseOverGenomeInfo mouseOverBox;
    private float BaseScale;
    private UIScaleHelper scaleHelper;

    public int refselectedBreedSlot { get; private set; }

    public CRISPR_SelectSpecies(Player player, float _BaseScale, int selectedBreedSlot)
    {
      this.BaseScale = _BaseScale;
      this.refselectedBreedSlot = selectedBreedSlot;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      float num1 = 0.0f;
      float num2 = 0.0f;
      float y1 = this.scaleHelper.DefaultBuffer.Y;
      this.collection = new CollectionScreenManager(player, _IsCRISPR_Selector: true, BaseScale: this.BaseScale);
      this.collection.location -= this.collection.GetOffsetFromTopLeft();
      this.collection.location.Y += num1;
      float num3 = num1 + this.collection.GetHeight();
      float num4 = num2 + this.collection.GetWidth();
      float num5 = num3 + y1;
      this.previewFrame = new CRISPR_PreviewFrame(this.BaseScale, num4);
      this.previewFrame.location.Y = num5;
      this.previewFrame.location.Y += this.previewFrame.customerFrame.VSCale.Y * 0.5f;
      this.previewFrame.location.X += this.previewFrame.customerFrame.VSCale.X * 0.5f;
      float num6 = num5 + this.previewFrame.customerFrame.VSCale.Y + y1;
      this.textButton = new TextButton(this.BaseScale, "Confirm", 45f, _OverrideFrameScale: this.BaseScale);
      Vector2 sizeTrue = this.textButton.GetSize_True();
      this.textButton.vLocation.Y = num6 + sizeTrue.Y * 0.5f;
      this.textButton.vLocation.X += num4 * 0.5f;
      float y2 = num6 + sizeTrue.Y;
      this.textButton.SetButtonColour(BTNColour.Grey);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "DNA Selection", this.BaseScale);
      this.bigBrownPanel.Finalize(new Vector2(num4, y2));
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.collection.location += frameOffsetFromTop;
      TextButton textButton = this.textButton;
      textButton.vLocation = textButton.vLocation + frameOffsetFromTop;
      this.previewFrame.location += frameOffsetFromTop;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
      this.animalsSelected = new AnimalType[2];
      this.animalsSelected[0] = AnimalType.None;
      this.animalsSelected[1] = AnimalType.None;
    }

    public void LerpIn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public bool CheckMouseOver(Player player) => this.bigBrownPanel.CheckMouseOver(player, this.location);

    public AnimalType[] UpdateCRISPR_SelectSpecies(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool Cancel)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      offset += this.location;
      Cancel = false;
      if ((double) this.lerper.Value == 0.0)
      {
        this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
          Cancel = true;
        bool JustConfirmedSelection;
        int num = (int) this.collection.UpdateCollectionScreenManager(offset, DeltaTime, player, out bool _, out JustConfirmedSelection);
        if (JustConfirmedSelection)
        {
          this.animalsSelected[0] = this.collection.SelectedAnimal;
          this.animalsSelected[1] = this.collection.SecondSelectedAnimal;
          this.previewFrame.SetAnimals(this.animalsSelected[0], this.animalsSelected[1], player);
          if (this.collection.SelectedAnimal != AnimalType.None && this.collection.SecondSelectedAnimal != AnimalType.None)
            this.textButton.SetButtonColour(BTNColour.Green);
          else
            this.textButton.SetButtonColour(BTNColour.Grey);
        }
        AlienEntry mouseOverEntry = this.collection.GetMouseOverEntry();
        AnimalType animalType = AnimalType.None;
        if (mouseOverEntry != null)
          animalType = this.collection.GetMouseOverEntry().anaimaltype;
        if (animalType != AnimalType.None)
        {
          if (this.mouseOverBox == null || animalType != this.mouseOverBox.refAnimalType)
            this.CreateNewMouseOverBox(mouseOverEntry, player);
        }
        else
          this.mouseOverBox = (SpeciesMouseOverGenomeInfo) null;
        if (this.animalsSelected[0] != AnimalType.None && this.animalsSelected[1] != AnimalType.None && this.textButton.UpdateTextButton(player, offset, DeltaTime))
          return this.animalsSelected;
      }
      return (AnimalType[]) null;
    }

    private void CreateNewMouseOverBox(AlienEntry mouseOverEntry, Player player)
    {
      this.mouseOverBox = new SpeciesMouseOverGenomeInfo(mouseOverEntry.anaimaltype, player, this.BaseScale);
      Vector2 vLocation = mouseOverEntry.vLocation;
      vLocation.Y -= mouseOverEntry.GetSize().Y * 0.5f;
      Vector2 vector2 = vLocation + this.collection.location;
      vector2.Y -= this.mouseOverBox.GetSize().Y * 0.5f;
      vector2.Y -= this.scaleHelper.ScaleY(5f);
      this.mouseOverBox.location = vector2;
    }

    public void DrawCRISPR_SelectSpecies(Vector2 offset, SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      offset += this.location;
      offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      if ((double) this.lerper.Value == 1.0)
        return;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.collection.DrawCollectionScreenManager(offset);
      this.textButton.DrawTextButton(offset, 1f, spriteBatch);
      this.previewFrame.DrawCRISPR_PreviewFrame(offset, spriteBatch);
      if (this.mouseOverBox == null)
        return;
      this.mouseOverBox.DrawSpeciesMouseOverGenomeInfo(offset, spriteBatch);
    }
  }
}

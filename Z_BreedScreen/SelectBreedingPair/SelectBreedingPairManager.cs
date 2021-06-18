// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectBreedingPair.SelectBreedingPairManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.SelectBreedingPair
{
  internal class SelectBreedingPairManager
  {
    public BigBrownPanel bigBrownPanel;
    public Vector2 location;
    private BreedingRowsDisplay breedingRowsDisplay;
    private LerpHandler_Float lerper;

    public SelectBreedingPairManager(AnimalsForBreedInfo selectedanimal, float BaseScale)
    {
      this.breedingRowsDisplay = new BreedingRowsDisplay(selectedanimal, BaseScale);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Breeding Pair", BaseScale);
      this.bigBrownPanel.Finalize(this.breedingRowsDisplay.GetSize());
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public float GetHeight() => this.bigBrownPanel.vScale.Y;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public Parents_AndChild UpdateSelectBreedingPairManager(
      Player player,
      float DeltaTime,
      out bool Cancel)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      Cancel = false;
      if ((double) this.lerper.Value != 0.0)
        return (Parents_AndChild) null;
      Vector2 location = this.location;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
        Cancel = true;
      return this.breedingRowsDisplay.UpdateBreedingRowsDisplay(player, DeltaTime, location);
    }

    public void DrawSelectBreedingPairManager(SpriteBatch spriteBatch)
    {
      Vector2 location = this.location;
      location.X += this.lerper.Value * BreedPopUp.LerpDistance;
      if ((double) this.lerper.Value == 1.0)
        return;
      this.bigBrownPanel.DrawBigBrownPanel(location, spriteBatch);
      this.breedingRowsDisplay.DrawBreedingRowsDisplay(location, spriteBatch);
    }
  }
}

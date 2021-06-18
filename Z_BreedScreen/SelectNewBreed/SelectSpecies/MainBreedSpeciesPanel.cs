// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies.MainBreedSpeciesPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies
{
  internal class MainBreedSpeciesPanel
  {
    private CollectionScreenManager collection;
    public Vector2 Location;
    private Vector2 OffsetForGridFromLocation;
    private BigBrownPanel bigBrownPanel;
    private LerpHandler_Float lerper;

    public MainBreedSpeciesPanel(Player player, float BaseScale)
    {
      this.collection = new CollectionScreenManager(player, _IsBreedSelector: true, BaseScale: BaseScale);
      this.collection.location -= this.collection.GetOffsetFromTopLeft();
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Species Selection", BaseScale);
      this.bigBrownPanel.Finalize(new Vector2(this.collection.GetWidth(), this.collection.GetHeight()));
      this.collection.location += this.bigBrownPanel.GetFrameOffsetFromTop();
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public void SetUpThis(AnimalsForBreedInfo breed, bool IsNew) => this.collection.SetUpForBreedSelect(breed, IsNew);

    public Vector2 GetSize() => this.bigBrownPanel.vScale;

    public void ResetSelection() => this.collection.enemy = AnimalType.None;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      offset += this.Location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public AnimalsForBreedInfo UpdateMainBreedSpeciesPanel(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      out bool Cancel)
    {
      Cancel = false;
      Offset += this.Location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, Offset))
          Cancel = true;
        bool JustConfirmedSelection;
        AnimalType animaltyps = this.collection.UpdateCollectionScreenManager(Offset, DeltaTime, player, out bool _, out JustConfirmedSelection);
        if (animaltyps != AnimalType.None & JustConfirmedSelection)
          return this.collection.GetBreed(animaltyps);
      }
      return (AnimalsForBreedInfo) null;
    }

    public void DrawMainBreedSpeciesPanel(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      Offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      if ((double) this.lerper.Value == 1.0)
        return;
      this.bigBrownPanel.DrawBigBrownPanel(Offset, spriteBatch);
      this.collection.DrawCollectionScreenManager(Offset);
    }
  }
}

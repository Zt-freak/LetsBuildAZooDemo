// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.HabitatTabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.CurrentHab;
using TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat.OtherAnimals;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._02Habitat
{
  internal class HabitatTabManager
  {
    public Vector2 location;
    private HabitatManager habitatmanager;
    private EnclosureCapacityManager enclosuremanager;
    private OtherAnimalsManager otheranimals;
    private Vector2 size;

    public HabitatTabManager(PrisonerInfo animal, Player player, float width, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      int CellBoockUID;
      player.prisonlayout.GetThisAnimal(animal.intakeperson.UID, out CellBoockUID);
      PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(CellBoockUID);
      int floorSpaceVolume = thisCellBlock.GetFloorSpaceVolume();
      this.size = Vector2.Zero;
      this.habitatmanager = new HabitatManager(width, player, animal, thisCellBlock, floorSpaceVolume, BaseScale);
      this.habitatmanager.location.Y = this.size.Y;
      this.habitatmanager.location.Y += this.habitatmanager.GetSize().Y * 0.5f;
      this.size.Y += this.habitatmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.enclosuremanager = new EnclosureCapacityManager(width, thisCellBlock, floorSpaceVolume, BaseScale);
      this.enclosuremanager.location.Y = this.size.Y;
      this.enclosuremanager.location.Y += this.enclosuremanager.GetSize().Y * 0.5f;
      this.size.Y += this.enclosuremanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.otheranimals = new OtherAnimalsManager(thisCellBlock, animal, width, BaseScale);
      this.otheranimals.location.Y = this.size.Y;
      this.otheranimals.location.Y += this.otheranimals.GetSize().Y * 0.5f;
      this.size.Y += this.otheranimals.GetSize().Y;
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateHabitatTabManager(
      Vector2 offset,
      Player player,
      float DeltaTime,
      ref bool PrssedClose)
    {
      offset += this.location;
    }

    public void DrawHabitatTabManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.habitatmanager.DrawHabitatManager(offset, spriteBatch);
      this.enclosuremanager.DrawEnclosureCapacityManager(offset, spriteBatch);
      this.otheranimals.DrawOtherAnimalsManager(offset, spriteBatch);
    }
  }
}

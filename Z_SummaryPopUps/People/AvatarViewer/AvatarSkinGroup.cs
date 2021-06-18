// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.AvatarViewer.AvatarSkinGroup
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_CharacterSelect;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame;

namespace TinyZoo.Z_SummaryPopUps.People.AvatarViewer
{
  internal class AvatarSkinGroup
  {
    public Vector2 location;
    private List<SelectablePerson> selectablePeople;
    private Chromosone chromosone;
    private Vector2 size;

    public AvatarSkinGroup(
      List<AnimalType> animalTypes,
      bool IsGirl,
      float BaseScale,
      int numberPerRow = 3)
    {
      bool flag = false;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.selectablePeople = new List<SelectablePerson>();
      Vector2 vector2 = Vector2.Zero;
      for (int index = 0; index < animalTypes.Count; ++index)
      {
        SelectablePerson selectablePerson = new SelectablePerson(0.0f, animalTypes[index], BaseScale);
        vector2 = selectablePerson.GetSize();
        selectablePerson.Location.X = (vector2.X + defaultBuffer.X) * (float) (index % numberPerRow);
        selectablePerson.Location.Y = (vector2.Y + defaultBuffer.Y) * (float) (index / numberPerRow);
        selectablePerson.Location.X += vector2.X * 0.5f;
        selectablePerson.Location.Y += vector2.Y;
        this.selectablePeople.Add(selectablePerson);
      }
      int num1 = (int) Math.Ceiling((double) this.selectablePeople.Count / (double) numberPerRow);
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int num2 = Math.Min(this.selectablePeople.Count - numberPerRow * index1, numberPerRow);
        float num3 = (float) ((double) num2 * (double) vector2.X + (double) defaultBuffer.X * (double) (num2 - 1));
        for (int index2 = 0; index2 < num2; ++index2)
          this.selectablePeople[index2 + numberPerRow * index1].Location.X -= num3 * 0.5f;
      }
      float num4 = this.selectablePeople[this.selectablePeople.Count - 1].Location.Y - this.selectablePeople[0].Location.Y + vector2.Y;
      this.size.X += this.selectablePeople[Math.Min(this.selectablePeople.Count, numberPerRow) - 1].Location.X - this.selectablePeople[0].Location.X + vector2.X;
      this.size.Y = num4;
      this.size.Y += defaultBuffer.Y;
      if (!flag)
        return;
      this.chromosone = new Chromosone(IsGirl, BaseScale);
      this.chromosone.vLocation.X = 0.0f;
      this.chromosone.vLocation.Y = this.size.Y;
      this.chromosone.vLocation.Y += this.chromosone.GetSize().Y * 0.5f;
      this.size.Y += this.chromosone.GetSize().Y;
    }

    public Vector2 GetSize() => this.size;

    public AnimalType GetAnimalTypeOfThisIndex(int index) => this.selectablePeople[index].GetAnimalType();

    public int GetNumberInThisGroup() => this.selectablePeople.Count;

    public void SetIsActiveSkin(int index, bool IsActive) => this.selectablePeople[index].Darken(IsActive);

    public int UpdateAvatarSkinGroup(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.selectablePeople.Count; ++index)
      {
        if (this.selectablePeople[index].UpdateSelectablePerson(player, DeltaTime, offset))
          return index;
      }
      return -1;
    }

    public void DrawAvatarSkinGroup(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.chromosone != null)
        this.chromosone.DrawChromosone(offset, spriteBatch);
      for (int index = 0; index < this.selectablePeople.Count; ++index)
        this.selectablePeople[index].DrawSelectablePerson(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity.NameSexBreed
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity
{
  internal class NameSexBreed
  {
    private Chromosone chomosone;
    private ZGenericText ObjName;
    private ZGenericText ObjBreed;
    public Vector2 Location;

    public NameSexBreed(PrisonerInfo animal, Vector2 VSCale, float BaseScale, bool IsDead = false)
    {
      string name = animal.intakeperson.Name;
      if (IsDead)
        name += " (Deceased)";
      string _textToWrite = EnemyData.GetEnemyTypeName(animal.intakeperson.animaltype);
      if (animal.intakeperson.HeadType != AnimalType.None)
        _textToWrite = HybridNames.GetAnimalCombinedName(animal.intakeperson.animaltype, animal.intakeperson.HeadType);
      if (animal.IsPainted)
        _textToWrite = EnemyData.GetEnemyTypeName(animal.GetAnimalPainted()) + "?";
      this.chomosone = new Chromosone(animal.intakeperson.IsAGirl, BaseScale);
      this.ObjName = new ZGenericText(name, BaseScale, false, _UseOnePointFiveFont: true);
      this.ObjBreed = new ZGenericText(_textToWrite, BaseScale, false);
      this.ObjBreed.vLocation.Y = this.ObjName.vLocation.Y;
      this.ObjBreed.vLocation.Y += this.ObjName.GetSize().Y;
      this.chomosone.vLocation.Y = this.GetHeight() * 0.5f;
      this.chomosone.vLocation.X = Math.Max(this.ObjBreed.GetSize().X, this.ObjName.GetSize().X);
      this.chomosone.vLocation.X += this.chomosone.GetSize().X;
    }

    public float GetHeight() => Math.Max(this.ObjBreed.GetSize().Y + this.ObjName.GetSize().Y, this.chomosone.GetSize().Y);

    public void UpdateNameSexBreed()
    {
    }

    public void DrawNameSexBreed(Vector2 Offset)
    {
      Offset += this.Location;
      this.chomosone.DrawChromosone(Offset, AssetContainer.pointspritebatchTop05);
      this.ObjName.DrawZGenericText(Offset, AssetContainer.pointspritebatchTop05);
      this.ObjBreed.DrawZGenericText(Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}

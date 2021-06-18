// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CharacterSelect.SelectablePersonSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_CharacterSelect
{
  internal class SelectablePersonSet
  {
    private GameObject Chromosone;
    private List<SelectablePerson> People;
    private Vector2 Location;
    private LerpHandler_Float lerper_chromosone;
    private float LerpDistance;

    public SelectablePersonSet(
      bool IsMale_Left,
      List<AnimalType> ListOfPeople,
      Vector2 _Location,
      float Offset,
      float BaseScale)
    {
      this.Location = _Location;
      this.People = new List<SelectablePerson>();
      for (int index = 0; index < ListOfPeople.Count; ++index)
      {
        this.People.Add(new SelectablePerson(0.0f, ListOfPeople[index], BaseScale));
        this.People[index].Location = this.Location;
        this.People[index].Location.X += Offset * (float) index;
      }
      this.Chromosone = new GameObject();
      this.Chromosone.DrawRect = new Rectangle(1008, 890, 16, 17);
      if (!IsMale_Left)
        this.Chromosone.DrawRect = new Rectangle(984, 851, 12, 19);
      this.Chromosone.scale = 2f * BaseScale;
      this.Chromosone.SetDrawOriginToCentre();
      this.Chromosone.vLocation.Y = 40f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y + this.People[0].Location.Y;
      this.Chromosone.vLocation.X = this.People[0].Location.X + (float) (((double) this.People[this.People.Count - 1].Location.X - (double) this.People[0].Location.X) * 0.5);
      this.lerper_chromosone = new LerpHandler_Float();
    }

    public int UpdateSelectablePersonSet(
      Player player,
      float DeltaTime,
      bool DisableClickOrMouseOver = false)
    {
      this.lerper_chromosone.UpdateLerpHandler(DeltaTime);
      int num = -1;
      this.Chromosone.UpdateColours(DeltaTime);
      for (int index = 0; index < this.People.Count; ++index)
      {
        if (this.People[index].UpdateSelectablePerson(player, DeltaTime, Vector2.Zero, DisableClickOrMouseOver))
          num = index;
      }
      return num;
    }

    public int GetNumberOfPeople() => this.People.Count;

    public Vector2 GetThisPersonPosition(int index) => this.People[index].Location;

    public AnimalType GetAnimalType(int personIndex) => this.People[personIndex].GetAnimalType();

    public void Exit(int selectedIndex = -1, bool DelayHumanLerp = false)
    {
      if (selectedIndex == -1)
        this.Chromosone.SetAlpha(false, 0.3f, 1f, 0.0f);
      for (int index = 0; index < this.People.Count; ++index)
      {
        bool _WasSelected = selectedIndex == index;
        if (_WasSelected)
        {
          this.lerper_chromosone.SetLerp(true, 0.0f, 1f, 3f);
          this.LerpDistance = this.People[index].Location.X - this.Chromosone.vLocation.X;
        }
        this.People[index].Exit(_WasSelected, DelayHumanLerp);
      }
    }

    public void ForceMouseOver(int INDEX) => this.People[INDEX].IsMouseover = true;

    public void DrawSelectablePersonSet(Vector2 Offset)
    {
      for (int index = 0; index < this.People.Count; ++index)
        this.People[index].DrawSelectablePerson(Offset, AssetContainer.pointspritebatch03);
    }
  }
}

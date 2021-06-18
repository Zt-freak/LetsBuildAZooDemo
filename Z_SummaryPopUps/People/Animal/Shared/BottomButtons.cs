// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.Shared.BottomButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_BarMenu.Pen.Animals;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.Shared
{
  internal class BottomButtons
  {
    public Vector2 location;
    private List<LittleSummaryButton> littlebuttons;
    private PrisonerInfo animal;
    private Vector2 size;

    public BottomButtons(
      AnimalViewTabType animalviewtabtype,
      float width,
      PrisonerInfo _animal,
      Player player,
      float BaseScale)
    {
      this.animal = _animal;
      List<LittleSummaryButton> littleSummaryButtonList1 = new List<LittleSummaryButton>();
      List<LittleSummaryButton> littleSummaryButtonList2 = new List<LittleSummaryButton>();
      this.littlebuttons = new List<LittleSummaryButton>();
      littleSummaryButtonList1.Add(new LittleSummaryButton(LittleSummaryButtonType.Remove, true, BaseScale));
      littleSummaryButtonList2.Add(new LittleSummaryButton(LittleSummaryButtonType.Quarantine, true, BaseScale));
      littleSummaryButtonList2.Add(new LittleSummaryButton(LittleSummaryButtonType.Move, true, BaseScale));
      if (player.animalquarantine.FindABuildingWithFreeSlotForAnimals() == null)
        littleSummaryButtonList2[littleSummaryButtonList2.Count - 1].SetUnavailable();
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      for (int index = 0; index < littleSummaryButtonList1.Count; ++index)
      {
        littleSummaryButtonList1[index].vLocation.X += (littleSummaryButtonList1[index].GetSize().X + defaultBuffer.X) * (float) index;
        littleSummaryButtonList1[index].vLocation.X += littleSummaryButtonList1[index].GetSize().X * 0.5f;
        this.size.Y = littleSummaryButtonList1[index].GetSize().Y;
      }
      for (int index = 0; index < littleSummaryButtonList2.Count; ++index)
      {
        littleSummaryButtonList2[index].vLocation.X = width;
        littleSummaryButtonList2[index].vLocation.X -= (littleSummaryButtonList2[index].GetSize().X + defaultBuffer.X) * (float) index;
        littleSummaryButtonList2[index].vLocation.X -= littleSummaryButtonList2[index].GetSize().X * 0.5f;
        this.size.Y = littleSummaryButtonList2[index].GetSize().Y;
      }
      this.littlebuttons.AddRange((IEnumerable<LittleSummaryButton>) littleSummaryButtonList1);
      this.littlebuttons.AddRange((IEnumerable<LittleSummaryButton>) littleSummaryButtonList2);
      this.size.X = width;
      for (int index = 0; index < this.littlebuttons.Count; ++index)
        this.littlebuttons[index].vLocation.X -= width * 0.5f;
    }

    public Vector2 GetSize() => this.size;

    public bool UpdateBottomButtons(
      Vector2 offset,
      Player player,
      float DeltaTime,
      out bool PopupQuarantineInfo)
    {
      offset += this.location;
      PopupQuarantineInfo = false;
      for (int index = 0; index < this.littlebuttons.Count; ++index)
      {
        if (this.littlebuttons[index].UpdateLittleSummaryButton(DeltaTime, player, offset))
          return this.OnClickButton(this.littlebuttons[index].Buttontype, player, out PopupQuarantineInfo);
      }
      return false;
    }

    private bool OnClickButton(
      LittleSummaryButtonType buttonType,
      Player player,
      out bool PopupQuarantineInfo)
    {
      PopupQuarantineInfo = false;
      switch (buttonType)
      {
        case LittleSummaryButtonType.Move:
          if (player.prisonlayout.cellblockcontainer.RemoveThisAnimalFromCellBlock(this.animal, player))
          {
            if (player.livestats.AnimalsJustTraded == null)
              player.livestats.AnimalsJustTraded = new WaveInfo(new List<IntakePerson>());
            player.livestats.AnimalsJustTraded.AddPrisonerInfo(this.animal);
            GameFlags.CellBlockContentsChanged = true;
            FeatureFlags.NewAnimalGot = true;
            return true;
          }
          break;
        case LittleSummaryButtonType.Quarantine:
          if (player.animalquarantine.TryAddAnimalToAQuarantineBuilding(this.animal, player, true))
          {
            if (player.prisonlayout.cellblockcontainer.RemoveThisAnimalFromCellBlock(this.animal, player))
            {
              GameFlags.CellBlockContentsChanged = true;
              AnimalsMenu.AnimalListChanged = true;
              return true;
            }
            break;
          }
          PopupQuarantineInfo = true;
          return true;
        case LittleSummaryButtonType.Remove:
          if (!this.animal.IsDead)
          {
            this.animal.IsDead = true;
            this.animal.causeofdeath = CauseOfDeath.euthanized;
            Z_GameFlags.CheckDeaths = true;
            return true;
          }
          break;
      }
      return false;
    }

    public void DrawBottomButtons(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.littlebuttons.Count; ++index)
        this.littlebuttons[index].DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}

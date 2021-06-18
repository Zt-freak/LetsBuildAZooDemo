// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.SingleAnimalManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.LeftANimalSummary;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary
{
  internal class SingleAnimalManager
  {
    private CustomerFrame customerframe;
    public Vector2 Location;
    private MainAnimalFoodManager mainanimalfoodmanager;
    private MiniAnimalSummary Micro_anaimalSummary;
    public bool HasNoAnimals;
    private AnimalType animaltype;
    public BigBrownPanel brownpanel;

    public SingleAnimalManager(
      Vector2 MasterScale,
      PrisonZone SelectedEnclosure,
      Player player,
      float MasterMult,
      AnimalType animal = AnimalType.None,
      AnimalType animalhead = AnimalType.None)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.animaltype = SelectedEnclosure.prisonercontainer.prisoners[0].intakeperson.animaltype;
      if (animal != AnimalType.None)
        this.animaltype = animal;
      float y = 520f;
      this.customerframe = new CustomerFrame(new Vector2(MasterScale.X - AnimalPopUpManager.Space, y));
      this.customerframe.frame.vLocation.Y = y * 0.5f;
      this.customerframe.frame.vLocation.Y += PenManager.HeadingBuffer;
      player.prisonlayout.SetUpAllStock(player);
      if (false)
        return;
      this.mainanimalfoodmanager = new MainAnimalFoodManager(new Vector2(baseScaleForUi * 150f, baseScaleForUi * 190f), SelectedEnclosure, this.animaltype, player, MasterMult, baseScaleForUi);
      this.Micro_anaimalSummary = new MiniAnimalSummary(this.customerframe.VSCale, SelectedEnclosure, this.animaltype, this.mainanimalfoodmanager.TotaBabies, this.mainanimalfoodmanager.TotalAnimals - this.mainanimalfoodmanager.TotaBabies, baseScaleForUi);
      this.Micro_anaimalSummary.SetSatiation(this.mainanimalfoodmanager.GetSatiation());
      this.Micro_anaimalSummary.setNutrition(SelectedEnclosure.prisonercontainer.FoodForAnimals.GetNutrition(SelectedEnclosure.prisonercontainer.prisoners[0].intakeperson.animaltype));
      Vector2 size1 = this.mainanimalfoodmanager.GetSize();
      Vector2 size2 = this.Micro_anaimalSummary.GetSize();
      float x = size1.X + 30f * baseScaleForUi + size2.X;
      this.mainanimalfoodmanager.SetPosition(new Vector2((float) ((double) x * -0.5 + (double) size2.X + (double) baseScaleForUi * 20.0 + (double) size1.X * 0.5), 0.0f));
      this.Micro_anaimalSummary.Location = new Vector2((float) ((double) x * -0.5 + (double) baseScaleForUi * 10.0 + (double) size2.X * 0.5), 0.0f);
      this.Micro_anaimalSummary.Location.Y -= (float) (((double) size1.Y - (double) size2.Y) * 0.5);
      this.brownpanel = new BigBrownPanel(new Vector2(0.0f, 0.0f), true, "Feeding Summary", baseScaleForUi, true);
      Vector2 _VSCale = new Vector2(x, Math.Max(size2.Y, size1.Y) + 20f * baseScaleForUi * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.brownpanel.Finalize(_VSCale + new Vector2(10f * baseScaleForUi, 10f * baseScaleForUi * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.customerframe = new CustomerFrame(_VSCale);
    }

    public bool CheckMouseOver(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      return this.brownpanel.CheckMouseOver(player, Offset);
    }

    public bool UpdateSingleAnimalManager(
      Vector2 TopMiddle,
      Player player,
      float DeltaTime,
      PrisonZone SelectedEnclosure,
      out bool GoBack)
    {
      bool SetSatiationAndNutrition;
      this.mainanimalfoodmanager.UpdateMainAnimalFoodManager(this.Location, DeltaTime, player, out SetSatiationAndNutrition);
      if (SetSatiationAndNutrition)
      {
        this.Micro_anaimalSummary.SetSatiation(this.mainanimalfoodmanager.GetSatiation());
        this.Micro_anaimalSummary.setNutrition(SelectedEnclosure.prisonercontainer.FoodForAnimals.GetNutrition(SelectedEnclosure.prisonercontainer.prisoners[0].intakeperson.animaltype));
      }
      this.brownpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      GoBack = this.brownpanel.UpdatePanelpreviousButton(player, DeltaTime, this.Location);
      return this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, this.Location);
    }

    public void DrawSingleAnimalManager(Vector2 TopMiddle)
    {
      this.brownpanel.DrawBigBrownPanel(this.Location, AssetContainer.pointspritebatchTop05);
      this.customerframe.DrawCustomerFrame(this.Location, AssetContainer.pointspritebatchTop05);
      this.Micro_anaimalSummary.DrawMiniAnimalSummary(this.Location);
      this.mainanimalfoodmanager.DrawMainAnimalFoodManager(this.Location);
    }
  }
}

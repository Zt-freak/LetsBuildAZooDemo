// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalFoodSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder;
using TinyZoo.Z_HUD.StoreRoom.AnimalStuff.SummaryPanel;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_HUD.StoreRoom
{
  internal class AnimalFoodSummary
  {
    private float BaseScale;
    private Vector2 Location;
    private AnimalFoodSummaryPanel mainPanel;
    private SingleAnimalManager animalfoodmanager;
    private QuickOrderManager quickordermanager;
    private PrisonZone TempSelectedPrison;
    private float MouseScrollOffset;
    private float MinMouseY;
    private float MaxMouseY;

    public AnimalFoodSummary(Player player)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.Create(player);
    }

    private void Create(Player player)
    {
      this.mainPanel = new AnimalFoodSummaryPanel(player, this.BaseScale);
      this.MaxMouseY = 0.0f;
      this.mainPanel.GetScrollLimits(ref this.MinMouseY);
      this.MaxMouseY = 0.0f;
      this.mainPanel.Location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player)
    {
      if (this.quickordermanager != null)
        return this.quickordermanager.CheckMouseOver(player, Vector2.Zero);
      return this.animalfoodmanager != null ? this.animalfoodmanager.CheckMouseOver(player, Vector2.Zero) : this.mainPanel.CheckMouseOver(player, this.Location);
    }

    public bool UpdateAnimalFoodSummary(Player player, float DeltaTIme, ref bool WillClearInput)
    {
      Vector2 location = this.Location;
      if (this.quickordermanager != null)
      {
        if (this.quickordermanager.CheckMouseOver(player, location))
          WillClearInput = true;
        bool GoBack;
        if (this.quickordermanager.UpdateQuickOrderManager(player, location, DeltaTIme, out GoBack))
          return true;
        if (GoBack)
        {
          this.Create(player);
          this.quickordermanager = (QuickOrderManager) null;
        }
      }
      if (this.animalfoodmanager != null)
      {
        if (this.animalfoodmanager.CheckMouseOver(player, location))
          WillClearInput = true;
        bool GoBack;
        if (this.animalfoodmanager.UpdateSingleAnimalManager(location, player, DeltaTIme, this.TempSelectedPrison, out GoBack))
          return true;
        if (!GoBack)
          return false;
        this.Create(player);
        this.animalfoodmanager = (SingleAnimalManager) null;
      }
      this.MouseScrollOffset += player.inputmap.momentumwheel.MovementThisFrame * 0.3f;
      this.MouseScrollOffset = MathHelper.Clamp(this.MouseScrollOffset, this.MinMouseY, this.MaxMouseY);
      float YPos = 0.0f;
      bool GoToDiet;
      bool OrderFood;
      PrisonZone prisonZone;
      TempAnimalInfo lookAtThisAnimal;
      if (this.mainPanel.UpdateAnimalFoodSummaryPanel(player, DeltaTIme, location, out GoToDiet, out OrderFood, out prisonZone, out lookAtThisAnimal, ref YPos, this.MouseScrollOffset))
      {
        if (GoToDiet)
        {
          this.TempSelectedPrison = prisonZone;
          this.animalfoodmanager = new SingleAnimalManager(Vector2.Zero, prisonZone, player, 1f, lookAtThisAnimal.animaltype, lookAtThisAnimal.animalHead);
          this.animalfoodmanager.Location = new Vector2(512f, 384f);
        }
        else
        {
          if (!OrderFood)
            return true;
          if (lookAtThisAnimal.CriticalFood != AnimalFoodType.Count)
          {
            this.quickordermanager = new QuickOrderManager(player, lookAtThisAnimal, this.BaseScale);
            this.quickordermanager.Location = new Vector2(512f, 384f);
          }
        }
      }
      return false;
    }

    public void DrawAnimalFoodSummary(SpriteBatch spritebatch)
    {
      Vector2 location = this.Location;
      if (this.quickordermanager != null)
        this.quickordermanager.DrawQuickOrderManager(location, spritebatch);
      else if (this.animalfoodmanager != null)
      {
        this.animalfoodmanager.DrawSingleAnimalManager(location);
      }
      else
      {
        float YPos = 0.0f;
        this.mainPanel.DrawAnimalFoodSummaryPanel(location, this.MouseScrollOffset, spritebatch, ref YPos);
      }
    }
  }
}

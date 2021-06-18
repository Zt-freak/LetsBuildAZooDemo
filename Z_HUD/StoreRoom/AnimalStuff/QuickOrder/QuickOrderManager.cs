// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder.QuickOrderManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom.Ani_MAll;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder
{
  internal class QuickOrderManager
  {
    private float DailyUse;
    private BigBrownPanel brownpanel;
    private float BaseScale;
    private Animall_Entry animalentry;
    public Vector2 Location;
    private DaysAndCheckOutPanel daysandceckout;
    private TempAnimalInfo REF_animalinfo;

    public QuickOrderManager(Player player, TempAnimalInfo animalinfo, float _BaseScale)
    {
      this.REF_animalinfo = animalinfo;
      this.BaseScale = _BaseScale;
      this.DailyUse = FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[(int) animalinfo.CriticalFood];
      this.brownpanel = new BigBrownPanel(Vector2.Zero, true, "Quick Order", this.BaseScale, true);
      this.Create(player);
      this.brownpanel.Finalize(this.animalentry.GetSize() + new Vector2(this.animalentry.GetSize().X + this.BaseScale * 10f, 0.0f));
    }

    private void Create(Player player)
    {
      this.animalentry = new Animall_Entry(this.REF_animalinfo.CriticalFood, this.BaseScale);
      this.daysandceckout = new DaysAndCheckOutPanel(this.BaseScale, this.animalentry.GetSize(), this.REF_animalinfo, player);
      this.daysandceckout.Location.X = this.animalentry.GetSize().X * 0.5f;
      this.daysandceckout.Location.X += this.BaseScale * 5f;
      this.animalentry.Location.X = this.daysandceckout.Location.X * -1f;
    }

    public bool CheckMouseOver(Player player, Vector2 Offset)
    {
      Offset += this.Location;
      return this.brownpanel.CheckMouseOver(player, Offset);
    }

    public bool UpdateQuickOrderManager(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      out bool GoBack)
    {
      Offset += this.Location;
      this.animalentry.UpdateAnimall_Entry(Offset, player, DeltaTime);
      if (this.daysandceckout.UpdateDaysAndCheckOutPanel(player, Offset, DeltaTime, this.animalentry.stocknumber.Value))
        this.Create(player);
      this.brownpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      GoBack = this.brownpanel.UpdatePanelpreviousButton(player, DeltaTime, Offset);
      return this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, Offset);
    }

    public void DrawQuickOrderManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.brownpanel.DrawBigBrownPanel(Offset, spritebatch);
      this.animalentry.DrawAnimall_Entry(Offset, spritebatch);
      this.daysandceckout.DrawDaysAndCheckOutPanel(Offset, spritebatch);
    }
  }
}

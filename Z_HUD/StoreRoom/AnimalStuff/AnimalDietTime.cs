// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.AnimalDietTime
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff
{
  internal class AnimalDietTime
  {
    private CustomerFrame customerframe;
    private AnimalInFrame animalinframe;
    public Vector2 Location;
    public LittleSummaryButton OrderMore;
    public LittleSummaryButton Diet;
    public LittleSummaryButton ExpandCollapse;
    private AnimalNameAndCount animalnameandcount;
    private FoodDayDisplay fooddaydisplay;
    public TempAnimalInfo REF_tempanimalinfo;
    public float FullHeight;
    public float ThisBarDrawHeight;

    public AnimalDietTime(
      float WidthOfParentPane,
      Player player,
      float BaseScale,
      TempAnimalInfo tempanimalinfo,
      PrisonZone prison,
      bool IsDark = false,
      bool EveryoneIsDeadOrCellIsEmpty = false)
    {
      this.REF_tempanimalinfo = tempanimalinfo;
      Vector3 zFrameMidBrown = ColourData.Z_FrameMidBrown;
      int num1 = IsDark ? 1 : 0;
      if (prison != null)
      {
        this.customerframe = new CustomerFrame(new Vector2((float) ((double) BaseScale * (double) WidthOfParentPane - 20.0 * (double) BaseScale), 30f * BaseScale), zFrameMidBrown, BaseScale);
        this.animalnameandcount = new AnimalNameAndCount(AnimalType.None, 0, BaseScale, AnimalType.None, prison);
        this.animalnameandcount.vLocation.X = (float) ((double) WidthOfParentPane * (double) BaseScale * -0.5);
        this.animalnameandcount.vLocation.X += 20f * BaseScale;
        this.ExpandCollapse = new LittleSummaryButton(LittleSummaryButtonType.ExpandCollapse, _BaseScale: BaseScale);
        this.ExpandCollapse.vLocation.X = 170f * BaseScale;
      }
      else
      {
        float num2 = 30f;
        this.customerframe = new CustomerFrame(new Vector2((float) ((double) BaseScale * (double) WidthOfParentPane - 20.0 * (double) BaseScale), 60f * BaseScale), zFrameMidBrown, BaseScale);
        this.animalinframe = new AnimalInFrame(tempanimalinfo.animaltype, tempanimalinfo.animalHead, tempanimalinfo.BodyVariant, num2 * BaseScale, 6f * BaseScale, BaseScale, tempanimalinfo.HeadVariant);
        this.animalinframe.Location.X = (float) ((double) WidthOfParentPane * (double) BaseScale * -0.5);
        this.animalinframe.Location.X += (float) ((double) num2 * 0.5 + 10.0) * BaseScale;
        this.animalnameandcount = new AnimalNameAndCount(tempanimalinfo.animaltype, tempanimalinfo.AllOfThese.Count, BaseScale, tempanimalinfo.animalHead);
        this.animalnameandcount.vLocation.X = this.animalinframe.Location.X;
        this.animalnameandcount.vLocation.X += (float) ((double) num2 * 0.5 + 10.0) * BaseScale;
        this.animalinframe.Location.X += 5f * BaseScale;
        this.fooddaydisplay = new FoodDayDisplay(tempanimalinfo, player, BaseScale);
        this.fooddaydisplay.Location.X = (float) (135.0 * -(double) BaseScale);
        this.OrderMore = new LittleSummaryButton(LittleSummaryButtonType.Restock, _BaseScale: BaseScale);
        this.Diet = new LittleSummaryButton(LittleSummaryButtonType.ChangeDiet, _BaseScale: BaseScale);
        this.Diet.vLocation.X = this.customerframe.VSCale.X * 0.5f;
        this.Diet.vLocation.X -= (float) ((double) this.Diet.DrawRect.Width * (double) BaseScale * 1.5);
        this.OrderMore.vLocation.X = this.customerframe.VSCale.X * 0.5f;
        this.OrderMore.vLocation.X -= (float) ((double) this.OrderMore.DrawRect.Width * (double) BaseScale * 0.5);
        this.OrderMore.vLocation.X -= BaseScale * 10f;
        this.Diet.vLocation.X -= BaseScale * 20f;
        if (EveryoneIsDeadOrCellIsEmpty)
        {
          this.animalnameandcount.SetAllDead();
          this.OrderMore = (LittleSummaryButton) null;
          this.Diet = (LittleSummaryButton) null;
          this.fooddaydisplay = (FoodDayDisplay) null;
          this.animalinframe.SetAnimalAlpha(0.0f);
        }
      }
      if (!IsDark)
        return;
      this.customerframe.frame.SetAlpha(0.6f);
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool UpdateAnimalDietTime(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      out bool GoToDiet,
      ref float YPos,
      float MaxDraw,
      float MinDraw)
    {
      Offset.X += this.Location.X;
      Offset.Y += YPos + this.ThisBarDrawHeight;
      YPos += this.FullHeight;
      GoToDiet = false;
      if ((double) Offset.Y + (double) this.ThisBarDrawHeight < (double) MinDraw || (double) Offset.Y - (double) this.ThisBarDrawHeight > (double) MaxDraw)
        return false;
      if (this.ExpandCollapse != null && this.ExpandCollapse.UpdateLittleSummaryButton(DeltaTime, player, Offset))
        return true;
      if (this.OrderMore != null)
      {
        GoToDiet = this.Diet.UpdateLittleSummaryButton(DeltaTime, player, Offset);
        if (this.REF_tempanimalinfo.CriticalFood != AnimalFoodType.Count)
          return this.OrderMore.UpdateLittleSummaryButton(DeltaTime, player, Offset);
      }
      return false;
    }

    public void DrawAnimalDietTime(
      Vector2 Offset,
      SpriteBatch spritebatch,
      ref float YPos,
      float MaxDraw,
      float MinDraw,
      float LerpInMultiplier)
    {
      Offset.X += this.Location.X;
      Offset.Y += YPos + this.ThisBarDrawHeight;
      YPos += this.FullHeight * LerpInMultiplier;
      if ((double) Offset.Y - (double) this.ThisBarDrawHeight < (double) MinDraw || (double) Offset.Y + (double) this.ThisBarDrawHeight > (double) MaxDraw || (double) LerpInMultiplier < 1.0)
        return;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      if (this.animalinframe != null)
        this.animalinframe.DrawAnimalInFrame(Offset, spritebatch);
      this.animalnameandcount.DrawAnimalNameAndCount(Offset, spritebatch);
      if (this.fooddaydisplay != null)
      {
        this.fooddaydisplay.DrawFoodDayDisplay(Offset, spritebatch);
        if (this.REF_tempanimalinfo.CriticalFood != AnimalFoodType.Count)
          this.OrderMore.DrawLittleSummaryButton(Offset, spritebatch);
        this.Diet.DrawLittleSummaryButton(Offset, spritebatch);
      }
      if (this.ExpandCollapse == null)
        return;
      this.ExpandCollapse.DrawLittleSummaryButton(Offset, spritebatch);
    }
  }
}

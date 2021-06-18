// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPair.TopLevelSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_BreedScreen.ConfirmBreed;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPair
{
  internal class TopLevelSummaryManager
  {
    private PregnancyBar pregnancybar;
    private NursingBar nursingbar;
    private ParentsDisplay parents;
    private TextButton Manage;
    public float totalHeight;
    public Vector2 location;
    private ActiveIcon activeicon;

    public TopLevelSummaryManager(
      Parents_AndChild parents_and_child,
      ActiveBreed breed,
      Player player,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.totalHeight = 0.0f;
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.parents = new ParentsDisplay(parents_and_child.FemaleParentVariant, parents_and_child.MaleParentVariant, parents_and_child.animaltype, BaseScale, false, parents_and_child.FatherDead, parents_and_child.MotherIsDead);
      float num = uiScaleHelper.ScaleX(40f);
      this.parents.Dad.Location.X = num;
      this.parents.Mum.Location.X = -num;
      this.parents.Location.Y = this.parents.GetHeight() * 0.5f;
      this.totalHeight += this.parents.GetHeight();
      if (parents_and_child.HeldBaby != null)
      {
        this.totalHeight += defaultYbuffer * 0.5f;
        this.nursingbar = new NursingBar(parents_and_child, player, BaseScale);
        this.nursingbar.Location.Y = this.totalHeight;
        this.nursingbar.Location.Y += this.nursingbar.GetOffsetFromTop();
        this.totalHeight += this.nursingbar.GetSize().Y - this.nursingbar.GetOffsetFromTop();
      }
      else if (breed != null)
      {
        this.totalHeight += defaultYbuffer * 0.5f;
        this.pregnancybar = new PregnancyBar(player.prisonlayout.GetThisNotInPenAnimal(parents_and_child.FemaleUID), player, BaseScale);
        this.pregnancybar.Location.Y = this.totalHeight;
        this.pregnancybar.Location.Y += this.pregnancybar.GetOffsetFromTop();
        this.totalHeight += this.pregnancybar.GetHeight() - this.pregnancybar.GetOffsetFromTop();
      }
      this.totalHeight += defaultYbuffer;
      this.Manage = new TextButton(BaseScale, nameof (Manage), 50f);
      this.Manage.vLocation.Y = this.totalHeight + this.Manage.GetSize_True().Y * 0.5f;
      this.totalHeight += defaultYbuffer;
      if (parents_and_child.GetIsActive(player))
        return;
      this.activeicon = new ActiveIcon(false, BaseScale);
      this.activeicon.vLocation = new Vector2(uiScaleHelper.ScaleX(100f), 0.0f);
    }

    public bool UpdateTopLevelSummaryManager(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.location;
      if (this.pregnancybar != null)
        this.pregnancybar.UpdatePregnancyBar();
      return this.Manage.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawTopLevelSummaryManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      this.parents.DrawParentsDisplay(Offset, spritebatch);
      if (this.pregnancybar != null)
        this.pregnancybar.DrawPregnancyBar(Offset, spritebatch);
      else if (this.nursingbar != null)
        this.nursingbar.DrawNursingBar(Offset, spritebatch);
      this.Manage.DrawTextButton(Offset, 1f, spritebatch);
      if (this.activeicon == null)
        return;
      this.activeicon.DrawActiveIcon(spritebatch, Offset);
    }
  }
}

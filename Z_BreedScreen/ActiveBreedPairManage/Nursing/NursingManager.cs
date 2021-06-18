// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing.NursingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BreedScreen.ActiveBreedPair;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage.Nursing
{
  internal class NursingManager
  {
    public Vector2 location;
    private PredictionTable predictiontable;

    public NursingManager(
      PrisonerInfo HeldBaby,
      Player player,
      Parents_AndChild parents_and_child,
      float XScale,
      ActiveBreed breed,
      float BaseScale)
    {
      int nursingDayOptionToDays = TimeWithParents.GetNursingDayOptionToDays((TIMEWITHPARENTS) parents_and_child.NursingDaysOption);
      double num = (double) Math.Min((float) parents_and_child.NursingDays / (float) nursingDayOptionToDays, 1f);
      string CustomText = "The baby is nursing, and will be transferred to a public enclosure in " + (object) (nursingDayOptionToDays - parents_and_child.NursingDays) + " days.";
      this.predictiontable = new PredictionTable(PredictionTableType.Nursing, player, (ActiveBreed) null, (PrisonerInfo) null, XScale, BaseScale, CustomText, parents_and_child);
    }

    public float GetHeight() => this.predictiontable.customerframe.VSCale.Y;

    public bool UpdateNursingManager(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.location;
      return this.predictiontable.UpdatePredictionTable(Offset, player, DeltaTime);
    }

    public void DrawNursingManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      this.predictiontable.DrawPredictionTable(Offset, spritebatch);
    }
  }
}

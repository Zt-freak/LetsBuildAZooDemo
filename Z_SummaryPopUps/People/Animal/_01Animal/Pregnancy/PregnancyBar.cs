// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy.PregnancyBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy
{
  internal class PregnancyBar
  {
    private ProgressBarWithPointer progressBar;
    public Vector2 Location;

    public PregnancyBar(
      PrisonerInfo animal,
      Player player,
      float BaseScale,
      bool drawHeader = true,
      float BarWidth = 158f)
    {
      ActiveBreed thisBreed = player.breeds.GetThisBreed(animal.intakeperson.UID);
      if (thisBreed == null)
        return;
      int daysForpregnancy = ActiveBreed.GetDaysForpregnancy(animal.intakeperson.animaltype);
      int daysLeft = thisBreed.DaysLeft;
      if (!Z_GameFlags.HasStartedFirstDay)
        --daysLeft;
      int num = Math.Max(daysForpregnancy - daysLeft, 0);
      string pointerText = string.Empty;
      if (thisBreed != null)
        pointerText = num.ToString() + " days";
      float progress = (float) num / (float) daysForpregnancy;
      string headerText = string.Empty;
      if (drawHeader)
        headerText = "Pregnancy Progress".ToUpper();
      this.progressBar = new ProgressBarWithPointer(pointerText, progress, BaseScale, headerText, BarWidth);
    }

    public float GetHeight() => this.progressBar != null ? this.progressBar.GetHeight() : 0.0f;

    public float GetOffsetFromTop() => this.progressBar != null ? this.progressBar.GetExtraOffsetFromTop() : 0.0f;

    public float GetWidth() => this.progressBar != null ? this.progressBar.GetWidth() : 0.0f;

    public void UpdatePregnancyBar()
    {
    }

    public void DrawPregnancyBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      this.progressBar.DrawProgressBar(offset, spriteBatch);
    }
  }
}

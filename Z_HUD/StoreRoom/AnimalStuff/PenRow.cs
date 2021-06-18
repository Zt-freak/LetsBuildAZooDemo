// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.PenRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff
{
  internal class PenRow
  {
    private CustomerFrame customerframe;
    private List<AnimalDietTime> animalanddiet;
    public Vector2 Location;
    public PrisonZone REF_prison;
    public float OffsetToCenter;
    public float FullHeight;
    private float EXHeightForFirstRow;
    private bool Hidden;
    private LerpHandler_Float HideLerper;
    private float BaseBuffer;
    public bool GoToDiet;

    public PenRow(
      PrisonZone prison,
      float BaseScale,
      ref float Height,
      Player player,
      bool IsTopRow,
      float Width)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      if (IsTopRow)
        this.EXHeightForFirstRow = Height;
      this.REF_prison = prison;
      this.HideLerper = new LerpHandler_Float();
      this.HideLerper.SetLerp(true, 1f, 1f, 3f);
      this.animalanddiet = new List<AnimalDietTime>();
      float num1 = Height;
      Height += BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.animalanddiet.Add(new AnimalDietTime(Width * 0.5f, player, BaseScale, (TempAnimalInfo) null, prison));
      float y1 = this.animalanddiet[0].GetSize().Y;
      this.animalanddiet[0].Location.Y = Height;
      this.animalanddiet[0].Location.X -= Width * 0.25f * BaseScale;
      this.animalanddiet[0].Location.Y += y1 * 0.5f;
      this.animalanddiet[0].ThisBarDrawHeight = y1 * 0.5f;
      this.animalanddiet[0].FullHeight = y1;
      if (IsTopRow)
      {
        this.animalanddiet[0].ThisBarDrawHeight += y1 * 0.5f;
        this.animalanddiet[0].FullHeight += y1 * 0.5f;
      }
      Height += y1;
      if (!prison.prisonercontainer.EveryoneIsDeadOrCellIsEmpty())
      {
        for (int index = 0; index < prison.prisonercontainer.tempAnimalInfo.Count; ++index)
        {
          this.animalanddiet.Add(new AnimalDietTime(Width, player, BaseScale, prison.prisonercontainer.tempAnimalInfo[index], (PrisonZone) null, index % 2 == 0, prison.prisonercontainer.EveryoneIsDeadOrCellIsEmpty()));
          float y2 = this.animalanddiet[index + 1].GetSize().Y;
          this.animalanddiet[index + 1].ThisBarDrawHeight = y2 * 0.5f;
          this.animalanddiet[index + 1].FullHeight = y2;
          this.animalanddiet[index + 1].Location.Y = Height;
          if (index == 0)
            this.animalanddiet[index + 1].Location.Y += y2 * 0.5f;
          else
            this.animalanddiet[index + 1].Location.Y += y2 * 0.5f;
          Height += y2;
        }
      }
      this.animalanddiet[this.animalanddiet.Count - 1].FullHeight += BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      Height += BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.customerframe = new CustomerFrame(new Vector2(Width + defaultBuffer.X * 2f, Height - num1), true, BaseScale);
      if (IsTopRow)
      {
        int num2 = 0;
        while (num2 < this.animalanddiet.Count)
          ++num2;
      }
      this.customerframe.location.Y = num1;
      this.customerframe.location.Y += (float) (((double) Height - (double) num1) * 0.5);
      this.FullHeight = this.customerframe.location.Y;
      this.OffsetToCenter = num1 * 0.5f;
      int num3 = IsTopRow ? 1 : 0;
      this.BaseBuffer = BaseScale * 10f;
    }

    public TempAnimalInfo GetClickedAnimal(int PressedThis) => this.animalanddiet[PressedThis + 1].REF_tempanimalinfo;

    public void UpdateLerpersOnly(float DeltaTime) => this.HideLerper.UpdateLerpHandler(DeltaTime);

    public int UpdatePenRow(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      ref float YPos,
      float MaxDraw,
      float MinDraw)
    {
      Offset += this.Location;
      this.HideLerper.UpdateLerpHandler(DeltaTime);
      int num = -1;
      for (int index = 0; index < this.animalanddiet.Count; ++index)
      {
        if (this.Hidden && index > 0)
        {
          YPos += this.BaseBuffer;
          return -1;
        }
        bool GoToDiet;
        if (this.animalanddiet[index].UpdateAnimalDietTime(Offset, player, DeltaTime, out GoToDiet, ref YPos, MaxDraw, MinDraw))
        {
          if (index == 0)
          {
            this.Hidden = !this.Hidden;
            if (this.Hidden)
            {
              this.animalanddiet[index].ExpandCollapse.DoToggle(false);
              this.HideLerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
            }
            else
            {
              this.animalanddiet[index].ExpandCollapse.DoToggle(true);
              this.HideLerper.SetLerp(false, 1f, 1f, 3f, true);
            }
          }
          else
          {
            this.GoToDiet = false;
            num = index - 1;
          }
        }
        else if (GoToDiet)
        {
          this.GoToDiet = true;
          num = index - 1;
        }
      }
      return num;
    }

    public void DrawPenRow(
      Vector2 Offset,
      SpriteBatch spritebatch,
      ref float YPos,
      float MaxDraw,
      float MinDraw)
    {
      Offset += this.Location;
      this.Location.Y = 0.0f;
      this.animalanddiet[0].Location.Y = this.animalanddiet[0].GetSize().Y * 0.5f;
      for (int index = 0; index < this.animalanddiet.Count; ++index)
      {
        if (index == 0)
          this.animalanddiet[index].DrawAnimalDietTime(Offset, spritebatch, ref YPos, MaxDraw, MinDraw, 1f);
        else
          this.animalanddiet[index].DrawAnimalDietTime(Offset, spritebatch, ref YPos, MaxDraw, MinDraw, this.HideLerper.Value);
        if (this.Hidden && index == 0)
          YPos += this.BaseBuffer * (1f - this.HideLerper.Value);
      }
    }
  }
}

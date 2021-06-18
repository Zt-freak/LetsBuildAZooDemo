// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.R_IconGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Input;
using System.Collections.Generic;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_Research_.Selected;

namespace TinyZoo.Z_Research_.IconGrid
{
  internal class R_IconGrid
  {
    private List<R_Icon> ricons;
    private R_Icon[,] ArrayOfIcons;
    private MouseOverInfoBox mousoverinfobox;
    private ControllerGridNavMatrix navmatrix;
    private ButtonRepeater repeater;
    private Vector2Int SelectedIndex;
    private int BiggestRing;

    public R_IconGrid(
      ref float MinX,
      ref float MaxX,
      ref float MinY,
      ref float MaxY,
      Player player)
    {
      this.ricons = new List<R_Icon>();
      List<REntry> researchData = RGrid_Data.GetResearchData();
      int SprialIndex = 0;
      int Ring = 1;
      int NextRingValue = 4;
      int SideSize = 0;
      int MaxForArray = 0;
      int NewOnesFound = 0;
      for (int index = 0; index < researchData.Count; ++index)
      {
        this.ricons.Add(new R_Icon(ref SprialIndex, ref Ring, ref NextRingValue, ref SideSize, researchData[index], ref MaxForArray));
        if (index < 4)
        {
          this.ricons[index].Unlock(ref NewOnesFound, true);
          if (this.BiggestRing < this.ricons[index].MyRing)
            this.BiggestRing = this.ricons[index].MyRing;
        }
        if (researchData[index].unlocktype != UnlockTYPE.Count && (player.unlocks.UnlockedThings[(int) researchData[index].unlocktype] > -1 || Z_DebugFlags.UnlockAllResearchOnGridRenderer))
        {
          this.ricons[index].Unlock(ref NewOnesFound);
          if (this.BiggestRing < this.ricons[index].MyRing)
            this.BiggestRing = this.ricons[index].MyRing;
        }
        this.ricons[index].CheckAndAddStar();
        if ((double) this.ricons[index].vLocation.X < (double) MinX)
          MinX = this.ricons[index].vLocation.X;
        if ((double) this.ricons[index].vLocation.X > (double) MaxX)
          MaxX = this.ricons[index].vLocation.X;
        if ((double) this.ricons[index].vLocation.Y < (double) MinY)
          MinY = this.ricons[index].vLocation.Y;
        if ((double) this.ricons[index].vLocation.Y > (double) MaxY)
          MaxY = this.ricons[index].vLocation.Y;
      }
      this.ArrayOfIcons = new R_Icon[MaxForArray + 1, MaxForArray + 1];
      for (int index = 0; index < this.ricons.Count; ++index)
        this.ArrayOfIcons[this.ricons[index].ArrayX + MaxForArray / 2, this.ricons[index].ArrayY + MaxForArray / 2] = this.ricons[index];
      this.repeater = new ButtonRepeater();
      this.navmatrix = new ControllerGridNavMatrix(MaxForArray, MaxForArray * MaxForArray, MaxForArray / 2);
      this.SelectedIndex = new Vector2Int(MaxForArray / 2, MaxForArray / 2);
      this.ScanForPreviews();
    }

    public void GetRingScrollLimit(out float MinX, out float MaxX, out float MinY, out float MaxY)
    {
      MaxX = 0.0f;
      MinX = 0.0f;
      MinY = 0.0f;
      MaxY = 0.0f;
      for (int index = 0; index < this.ricons.Count; ++index)
      {
        if (this.ricons[index].unlockstate != UnlockState.Locked)
        {
          if ((double) MinX > (double) this.ricons[index].vLocation.X || index == 0)
            MinX = this.ricons[index].vLocation.X;
          if ((double) MaxX < (double) this.ricons[index].vLocation.X || index == 0)
            MaxX = this.ricons[index].vLocation.X;
          if ((double) MinY > (double) this.ricons[index].vLocation.Y || index == 0)
            MinY = this.ricons[index].vLocation.Y;
          if ((double) MaxY < (double) this.ricons[index].vLocation.Y || index == 0)
            MaxY = this.ricons[index].vLocation.Y;
        }
      }
    }

    public void ScanForPreviews(bool LerpInNew = false)
    {
      int NewOnesFound = 0;
      for (int index1 = 0; index1 < this.ArrayOfIcons.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.ArrayOfIcons.GetLength(0); ++index2)
        {
          if (this.ArrayOfIcons[index1, index2].unlockstate == UnlockState.Unlocked)
          {
            if (index1 > 0 && this.ArrayOfIcons[index1 - 1, index2].unlockstate == UnlockState.Locked)
              this.ArrayOfIcons[index1 - 1, index2].Unlock(ref NewOnesFound, true, LerpInNew);
            if (index1 < this.ArrayOfIcons.GetLength(0) - 1 && this.ArrayOfIcons[index1 + 1, index2].unlockstate == UnlockState.Locked)
              this.ArrayOfIcons[index1 + 1, index2].Unlock(ref NewOnesFound, true, LerpInNew);
            if (index2 > 0 && this.ArrayOfIcons[index1, index2 - 1].unlockstate == UnlockState.Locked)
              this.ArrayOfIcons[index1, index2 - 1].Unlock(ref NewOnesFound, true, LerpInNew);
            if (index2 < this.ArrayOfIcons.GetLength(1) - 1 && this.ArrayOfIcons[index1, index2 + 1].unlockstate == UnlockState.Locked)
              this.ArrayOfIcons[index1, index2 + 1].Unlock(ref NewOnesFound, true, LerpInNew);
          }
        }
      }
    }

    public Vector2 GetLerpTarget()
    {
      for (int index = 0; index < this.ricons.Count; ++index)
      {
        if (this.ricons[index].MouseOver)
          return this.ricons[index].vLocation;
      }
      return Vector2.Zero;
    }

    public void CreateMouseOverBox(R_Icon icon, Player player) => this.mousoverinfobox = new MouseOverInfoBox(icon.rentry, icon.vLocation, icon, player, Z_GameFlags.GetBaseScaleForUI() * 2f);

    public R_Icon UpdateR_IconGrid(
      Player player,
      float DeltaTime,
      out bool NewControllerSelect,
      SelectedRentryManager SelectionItem,
      bool BlockMouse)
    {
      NewControllerSelect = false;
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], player.inputmap.HeldButtons[18], player.inputmap.HeldButtons[19]))
      {
        NewControllerSelect = true;
        switch (Direction)
        {
          case DirectionPressed.Up:
            --this.SelectedIndex.Y;
            if (this.SelectedIndex.Y < 0)
              this.SelectedIndex.Y = 0;
            if (this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].unlockstate == UnlockState.Locked)
            {
              ++this.SelectedIndex.Y;
              break;
            }
            break;
          case DirectionPressed.Right:
            ++this.SelectedIndex.X;
            if (this.SelectedIndex.X > this.ArrayOfIcons.GetLength(0) - 1)
              this.SelectedIndex.X = this.ArrayOfIcons.GetLength(0) - 1;
            if (this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].unlockstate == UnlockState.Locked)
            {
              --this.SelectedIndex.X;
              break;
            }
            break;
          case DirectionPressed.Down:
            ++this.SelectedIndex.Y;
            if (this.SelectedIndex.Y >= this.ArrayOfIcons.GetLength(1))
              this.SelectedIndex.Y = this.ArrayOfIcons.GetLength(1) - 1;
            if (this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].unlockstate == UnlockState.Locked)
            {
              --this.SelectedIndex.Y;
              break;
            }
            break;
          case DirectionPressed.Left:
            --this.SelectedIndex.X;
            if (this.SelectedIndex.X < 0)
              this.SelectedIndex.X = 0;
            if (this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].unlockstate == UnlockState.Locked)
            {
              ++this.SelectedIndex.X;
              break;
            }
            break;
        }
      }
      if (this.mousoverinfobox != null)
        this.mousoverinfobox.bActive = false;
      if (GameFlags.IsUsingController)
      {
        for (int index = 0; index < this.ricons.Count; ++index)
          this.ricons[index].MouseOver = false;
        this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].MouseOver = true;
        if (this.mousoverinfobox == null || this.mousoverinfobox.rentry != this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].rentry)
          this.CreateMouseOverBox(this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y], player);
        this.mousoverinfobox.bActive = true;
        if (player.inputmap.PressedThisFrame[0] && SelectionItem == null)
          return this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y];
        if (player.inputmap.PressedThisFrame[0] && SelectionItem != null && SelectionItem.REF_selectionIcon.rentry != this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].rentry)
        {
          this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y].BlockMouseDown = true;
          return this.ArrayOfIcons[this.SelectedIndex.X, this.SelectedIndex.Y];
        }
      }
      else
      {
        Vector2 worldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation);
        if (!BlockMouse)
        {
          for (int index = 0; index < this.ricons.Count; ++index)
          {
            this.ricons[index].UpdateR_Icon(player, DeltaTime, worldSpace);
            if (this.ricons[index].MouseOver)
            {
              if (this.mousoverinfobox == null || this.mousoverinfobox.rentry != this.ricons[index].rentry)
                this.CreateMouseOverBox(this.ricons[index], player);
              this.mousoverinfobox.bActive = true;
              if (true)
              {
                if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0 && (SelectionItem == null || SelectionItem.REF_selectionIcon.rentry != this.ricons[index].rentry))
                  return this.ricons[index];
              }
              else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && (SelectionItem == null || SelectionItem.REF_selectionIcon.rentry != this.ricons[index].rentry))
              {
                this.ricons[index].IsHeldOn = false;
                return this.ricons[index];
              }
            }
          }
        }
      }
      return (R_Icon) null;
    }

    public void DrawR_IconGrid(SelectedRentryManager SelectionItem)
    {
      for (int index = 0; index < this.ricons.Count; ++index)
        this.ricons[index].DrawR_Icon();
      if (this.mousoverinfobox == null)
        return;
      this.mousoverinfobox.DrawMouseOverInfoBox();
    }
  }
}

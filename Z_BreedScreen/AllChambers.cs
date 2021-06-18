// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.AllChambers
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Breeding;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedScreen.BreedChambers;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen
{
  internal class AllChambers
  {
    public List<BreedChamberPanel> panels;
    public Vector2 location;
    public BigBrownPanel mainPanel;
    private LerpHandler_Float lerper;

    public AllChambers(
      BreedingBuilding building,
      Player player,
      int TotalChambers = 4,
      float BaseScale = 1f)
    {
      this.SetUp(player, TotalChambers, building, (CRISPRBuilding) null, BaseScale);
    }

    public AllChambers(CRISPRBuilding building, Player player, int TotalChambers = 4, float BaseScale = 1f) => this.SetUp(player, TotalChambers, (BreedingBuilding) null, building, BaseScale);

    public void SetUp(
      Player player,
      int TotalChambers,
      BreedingBuilding breedingBuilding,
      CRISPRBuilding crisprBuilding,
      float BaseScale)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      string addHeaderText = "Breeding Slots";
      this.panels = new List<BreedChamberPanel>();
      if (breedingBuilding != null)
      {
        for (int index = 0; index < breedingBuilding.ParentsAndChildrenhere.Count; ++index)
          this.panels.Add(new BreedChamberPanel(breedingBuilding.ParentsAndChildrenhere[index], breedingBuilding.GetThisBreed(breedingBuilding.ParentsAndChildrenhere[index]), player, BaseScale));
        for (int count = this.panels.Count; count < TotalChambers; ++count)
          this.panels.Add(new BreedChamberPanel((Parents_AndChild) null, (ActiveBreed) null, player, BaseScale));
      }
      else if (crisprBuilding != null)
      {
        addHeaderText = "CRISPR";
        for (int index = 0; index < crisprBuilding.crisprBreeds.Length; ++index)
          this.panels.Add(new BreedChamberPanel(crisprBuilding.crisprBreeds[index], player, BaseScale, index + 1));
      }
      int num3 = 2;
      float num4 = 5f * BaseScale;
      float num5 = 5f * Sengine.ScreenRatioUpwardsMultiplier.Y * BaseScale;
      int num6 = this.panels.Count / num3;
      float num7 = (float) ((double) this.panels[0].customerframe.VSCale.Y * (double) num6 + (double) (num6 - 1) * (double) num5);
      float num8 = (float) ((double) num3 * (double) this.panels[0].customerframe.VSCale.X + (double) (num3 - 1) * (double) num4);
      for (int index = 0; index < this.panels.Count; ++index)
      {
        this.panels[index].Location.X += this.panels[index].customerframe.VSCale.X * 0.5f;
        this.panels[index].Location.X -= num8 / 2f;
        this.panels[index].Location.Y += this.panels[index].customerframe.VSCale.Y * 0.5f;
        this.panels[index].Location.Y += num1;
        this.panels[index].Location += new Vector2((float) (index / num3) * (this.panels[index].customerframe.VSCale.X + num4), (float) (index % num3) * (this.panels[index].customerframe.VSCale.Y + num5));
      }
      float y = num1 + num7;
      float x = num2 + num8;
      this.mainPanel = new BigBrownPanel(Vector2.Zero, true, addHeaderText, BaseScale);
      this.mainPanel.Finalize(new Vector2(x, y));
      Vector2 vector2 = new Vector2(0.0f, (float) (-(double) this.mainPanel.vScale.Y * 0.5 + (double) this.mainPanel.GetEdgeBuffer() * (double) BaseScale)) - this.mainPanel.InternalOffset;
      for (int index = 0; index < this.panels.Count; ++index)
        this.panels[index].Location += vector2;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.mainPanel.CheckMouseOver(player, offset);
    }

    public int UpdateAllChambers(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool Cancel,
      out bool GoToManageBreedPair)
    {
      Cancel = false;
      GoToManageBreedPair = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        offset += this.location;
        this.mainPanel.UpdateDragger(player, ref this.location, DeltaTime);
        if (this.mainPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
          Cancel = true;
        for (int index = 0; index < this.panels.Count; ++index)
        {
          if (this.panels[index].UpdateBreedChamberPanel(offset, player, DeltaTime, out GoToManageBreedPair))
            return index;
        }
      }
      return -1;
    }

    public void DrawAllChamber(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      if ((double) this.lerper.Value == 1.0)
        return;
      this.mainPanel.DrawBigBrownPanel(offset, spriteBatch);
      for (int index = 0; index < this.panels.Count; ++index)
        this.panels[index].DrawBreedChamberPanel(offset, spriteBatch);
    }
  }
}

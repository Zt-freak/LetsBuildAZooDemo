// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.BreedChambers.BreedChamberPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.Z_BreedScreen.ActiveBreedPair;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_CRISPR.ChamberView;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedScreen.BreedChambers
{
  internal class BreedChamberPanel
  {
    public Vector2 Location;
    public CustomerFrame customerframe;
    private TextButton textbutton;
    private MiniHeading miniheading;
    private TopLevelSummaryManager toplevelsummary;
    public Parents_AndChild REF_parents_and_child;
    public ActiveBreed REF_breed;
    private bool isCRISPR_UI;
    private CRISPRChamberSummary crisprSummary;

    public BreedChamberPanel(
      Parents_AndChild parents_and_child,
      ActiveBreed breed,
      Player player,
      float BaseScale)
    {
      this.SetUp(parents_and_child, breed, player, (CrisprActiveBreed) null, BaseScale);
    }

    public BreedChamberPanel(
      CrisprActiveBreed crisprBreed,
      Player player,
      float BaseScale,
      int Index)
    {
      this.isCRISPR_UI = true;
      this.SetUp((Parents_AndChild) null, (ActiveBreed) null, player, crisprBreed, BaseScale, Index);
    }

    public void SetUp(
      Parents_AndChild parents_and_child,
      ActiveBreed breed,
      Player player,
      CrisprActiveBreed crisprBreed,
      float BaseScale,
      int Index = -1)
    {
      this.REF_parents_and_child = parents_and_child;
      this.REF_breed = breed;
      float num1 = 0.0f;
      float x = 250f * BaseScale;
      float num2 = 75f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      string text = "Breeding Pair";
      Vector2 vector2_1 = new Vector2(10f, 10f);
      if (this.isCRISPR_UI)
        text = "#" + (object) Index;
      this.miniheading = new MiniHeading(Vector2.Zero, text, 1f, BaseScale);
      float num3 = num1 + num2;
      if (this.isCRISPR_UI)
      {
        if (crisprBreed == null)
        {
          this.textbutton = new TextButton(BaseScale, "Select DNA Pair", 80f, _OverrideFrameScale: BaseScale);
          this.textbutton.SetButtonColour(BTNColour.Blue);
          this.textbutton.vLocation.Y = num3;
        }
        else
        {
          this.crisprSummary = new CRISPRChamberSummary(crisprBreed, BaseScale);
          this.crisprSummary.location.Y = num3 - this.crisprSummary.GetHeight() * 0.5f;
        }
      }
      else if (parents_and_child == null)
      {
        this.textbutton = new TextButton(BaseScale, "Pick Breeding Pair", 100f);
        this.textbutton.SetButtonColour(BTNColour.Blue);
        this.textbutton.vLocation.Y = num3;
      }
      else
      {
        this.toplevelsummary = new TopLevelSummaryManager(parents_and_child, breed, player, BaseScale);
        this.toplevelsummary.location.Y = num3 - this.toplevelsummary.totalHeight * 0.5f;
      }
      float y = num3 + num2;
      this.customerframe = new CustomerFrame(new Vector2(x, y), BaseScale: BaseScale);
      this.miniheading.SetTextPosition(this.customerframe.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_2 = new Vector2(0.0f, (float) (-(double) this.customerframe.VSCale.Y * 0.5));
      if (this.textbutton != null)
      {
        TextButton textbutton = this.textbutton;
        textbutton.vLocation = textbutton.vLocation + vector2_2;
      }
      if (this.toplevelsummary != null)
        this.toplevelsummary.location += vector2_2;
      if (this.crisprSummary == null)
        return;
      this.crisprSummary.location += vector2_2;
    }

    public bool UpdateBreedChamberPanel(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      out bool GoToManage)
    {
      GoToManage = false;
      Offset += this.Location;
      if (this.textbutton != null && this.textbutton.UpdateTextButton(player, Offset, DeltaTime))
        return true;
      if (this.toplevelsummary != null)
      {
        if (this.toplevelsummary.UpdateTopLevelSummaryManager(player, DeltaTime, Offset))
        {
          GoToManage = true;
          return true;
        }
      }
      else if (this.crisprSummary != null && this.crisprSummary.UpdateActiveSummaryChamber(player, DeltaTime, Offset))
      {
        GoToManage = true;
        return true;
      }
      return false;
    }

    public void DrawBreedChamberPanel(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spriteBatch);
      this.miniheading.DrawMiniHeading(Offset, spriteBatch);
      if (this.toplevelsummary != null)
        this.toplevelsummary.DrawTopLevelSummaryManager(Offset, spriteBatch);
      else if (this.crisprSummary != null)
      {
        this.crisprSummary.DrawActiveSummaryChamber(Offset, spriteBatch);
      }
      else
      {
        if (this.textbutton == null)
          return;
        this.textbutton.DrawTextButton(Offset, 1f, spriteBatch);
      }
    }
  }
}

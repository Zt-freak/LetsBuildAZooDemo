// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer.NewThingPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer
{
  internal class NewThingPanel
  {
    public Vector2 location;
    private ScrollingBGPanel bigBrownPanel;
    private NewThingMainFrame mainFrame;
    private LerpHandler_Float lerper;
    private Vector2 lerpDistance;

    public NewThingPanel(List<PrisonerInfo> prisonerInfos)
    {
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>(prisonerInfos.Count);
      foreach (PrisonerInfo prisonerInfo in prisonerInfos)
        animals.Add(new AnimalRenderDescriptor(prisonerInfo.intakeperson.animaltype, prisonerInfo.intakeperson.CLIndex, prisonerInfo.intakeperson.HeadType, prisonerInfo.intakeperson.HeadVariant, _IsFemale: prisonerInfo.intakeperson.IsAGirl));
      this.SetUp(animals);
    }

    public NewThingPanel(
      List<AnimalRenderDescriptor> animalRenderDescriptors,
      bool IsCripsr = false)
    {
      this.SetUp(animalRenderDescriptors, IsCripsr: IsCripsr);
    }

    public NewThingPanel(List<AnimalType> animalsTypes)
    {
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>(animalsTypes.Count);
      for (int index = 0; index < animalsTypes.Count; ++index)
        animals.Add(new AnimalRenderDescriptor(animalsTypes[index]));
      this.SetUp(animals);
    }

    public NewThingPanel(AnimalType animalType, bool IsForGenomeComplete = true)
    {
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      if (IsForGenomeComplete)
      {
        for (int _variant = 0; _variant < 10; ++_variant)
          animals.Add(new AnimalRenderDescriptor(animalType, _variant));
      }
      else
        animals.Add(new AnimalRenderDescriptor(animalType));
      this.SetUp(animals, IsForGenomeComplete);
    }

    private void SetUp(
      List<AnimalRenderDescriptor> animals,
      bool IsForGenomeCompleted = false,
      bool IsCripsr = false)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.mainFrame = new NewThingMainFrame(animals, baseScaleForUi, IsForGenomeCompleted, IsCripsr);
      string addHeaderText = "New Discovery!";
      if (IsForGenomeCompleted)
        addHeaderText = "Genome Mapped!";
      this.bigBrownPanel = new ScrollingBGPanel(this.mainFrame.GetSize(), addHeaderText: addHeaderText, _BaseScale: baseScaleForUi);
      this.lerper = new LerpHandler_Float();
      this.lerper.Value = 1f;
    }

    public void LerpIn(float lerpDistance_x = 0.0f, float lerpDistance_y = -600f)
    {
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.lerpDistance = new Vector2(lerpDistance_x, lerpDistance_y);
    }

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateNewThingPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool AllowDrag = false)
    {
      offset += this.location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.bigBrownPanel.UpdateScrollingBG(DeltaTime, offset);
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return true;
      if (AllowDrag)
        this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      return this.mainFrame.UpdateNewThingMainFrame(player, DeltaTime, offset);
    }

    public void DrawNewThingPanel(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      offset += this.lerper.Value * this.lerpDistance;
      this.bigBrownPanel.DrawScrollingBGPanel(offset, spriteBatch);
      this.mainFrame.DrawNewThingMainFrame(offset, spriteBatch);
    }
  }
}

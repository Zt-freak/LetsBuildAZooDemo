// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame.TabFrameManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame
{
  internal class TabFrameManager
  {
    private BigBrownPanel bigBrownPanel;
    public Vector2 Vscale;
    private LerpHandler_Float lerper;
    public Vector2 location;
    private AnimalTabmanager animaltabmanager;
    private LookAtThisThingButton lookAtThisButton;
    private float BaseScale;
    private UIScaleHelper scaleHelper;

    public TabFrameManager(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.Vscale = this.scaleHelper.ScaleVector2(new Vector2(300f, 500f));
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Animal", this.BaseScale);
      this.bigBrownPanel.Finalize(this.Vscale);
      TabType[] tabTypeArray = new TabType[6]
      {
        TabType.A_Animal,
        TabType.A_Habitat,
        TabType.A_FamilyTree,
        TabType.A_Profitability,
        TabType.A_Info,
        TabType.A_LifeTimeStats
      };
      this.animaltabmanager = new AnimalTabmanager(this.BaseScale, this.bigBrownPanel.GetEdgeBuffers().X * 2f + this.Vscale.X, tabTypeArray);
      this.animaltabmanager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public void SetNewHeading(string heading) => this.bigBrownPanel.SetNewHeading(heading);

    public Vector2 GetMiniHeadingSize() => this.bigBrownPanel.GetMiniHeadingSize(false);

    public void SetSize(Vector2 contentsSize, Vector2 topCenterLocationDEP)
    {
      float num = this.location.Y + this.bigBrownPanel.GetFrameOffsetFromTop().Y;
      this.bigBrownPanel.Finalize(contentsSize);
      this.animaltabmanager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
      this.location.Y = num;
      this.location.Y += contentsSize.Y * 0.5f;
    }

    public void LockForBeta(bool RemoveLock = false) => this.bigBrownPanel.LockForBeta(RemoveLock);

    public void ForceTabSwitch(bool IsLeft)
    {
      if ((double) this.lerper.Value != 0.0)
        return;
      this.animaltabmanager.ForceTabSwitch(IsLeft);
    }

    public Vector2 GetFrameOffsetForContents() => this.bigBrownPanel.GetFrameOffsetFromTop();

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return (0 | (this.animaltabmanager.CheckMouseOver(player, offset) ? 1 : 0) | (this.bigBrownPanel.CheckMouseOver(player, offset) ? 1 : 0)) != 0;
    }

    public bool UpdateTabFrameManager(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      out AnimalViewTabType animalviewtab)
    {
      animalviewtab = AnimalViewTabType.Count;
      Offset += this.location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        animalviewtab = this.GetTabTypeToAnimalViewTabType(this.animaltabmanager.UpdateAnimalTabmanager(Offset, player, DeltaTime));
        this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, Offset))
          return true;
      }
      return false;
    }

    private AnimalViewTabType GetTabTypeToAnimalViewTabType(TabType tabType)
    {
      switch (tabType)
      {
        case TabType.A_Animal:
          return AnimalViewTabType.Animal;
        case TabType.A_Habitat:
          return AnimalViewTabType.Habitat;
        case TabType.A_FamilyTree:
          return AnimalViewTabType.FamilyTree;
        case TabType.A_Profitability:
          return AnimalViewTabType.Profitability;
        case TabType.A_Info:
          return AnimalViewTabType.Info;
        case TabType.A_LifeTimeStats:
          return AnimalViewTabType.LifeTimeStats;
        default:
          return AnimalViewTabType.Count;
      }
    }

    public void DrawTabFrameManager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.location;
      this.animaltabmanager.PreDrawAnimalTabmanager(Offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(Offset, spriteBatch);
      this.animaltabmanager.DrawAnimalTabmanager(Offset, spriteBatch);
    }

    public void PostDrawTabFrameManager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.location;
      this.bigBrownPanel.DrawDarkOverlay(Offset, spriteBatch);
    }
  }
}

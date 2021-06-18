// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.Shelter.AnimalShelterPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.Shelter
{
  internal class AnimalShelterPopUpManager
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private ShelterMainFrame mainFrame;
    private float BaseScale;

    public AnimalShelterPopUpManager(Player player)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Animal Shelter", this.BaseScale);
      this.mainFrame = new ShelterMainFrame(player, this.BaseScale);
      this.bigBrownPanel.Finalize(this.mainFrame.GetSize());
      this.bigBrownPanel.GetFrameOffsetFromTop();
    }

    public Vector2 GetSize() => this.bigBrownPanel.vScale;

    public Vector2 GetPositionOffset() => this.bigBrownPanel.InternalOffset;

    public bool CheckMouseOver(Player player)
    {
      Vector2 location = this.location;
      return this.bigBrownPanel.CheckMouseOver(player, location);
    }

    public bool UpdateAnimalShelterPopUpManager(Player player, float DeltaTime)
    {
      Vector2 location = this.location;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
        return true;
      this.mainFrame.UpdateShelterMainFrame(player, DeltaTime, location);
      return false;
    }

    public void DrawAnimalShelterPopUpManager()
    {
      Vector2 location = this.location;
      SpriteBatch pointspritebatch03 = AssetContainer.pointspritebatch03;
      this.bigBrownPanel.DrawBigBrownPanel(location, pointspritebatch03);
      this.mainFrame.DrawShelterMainFrame(location, pointspritebatch03);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.QuarantineAnimalsMainPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Z_Collection.Animals;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo
{
  internal class QuarantineAnimalsMainPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private AnimalCollectionPage frame;
    public static bool SomethingChanged_RefreshQuarantineAnimalList;

    public QuarantineAnimalsMainPanel(
      QuarantineBuilding quarantineBuilding,
      Player player,
      float BaseScale)
    {
      Vector2 FullPanelSize = new UIScaleHelper(BaseScale).ScaleVector2(new Vector2(415f, 400f));
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Quarantined Animals", BaseScale);
      this.bigBrownPanel.Finalize(FullPanelSize);
      float y = this.bigBrownPanel.GetEdgeBuffers().Y;
      this.frame = new AnimalCollectionPage(CollectionType.QuarantineAnimals, player, BaseScale, FullPanelSize + new Vector2(0.0f, y), 3, quarantineBuilding.UID);
      this.frame.location.Y -= y * 0.5f;
      QuarantineAnimalsMainPanel.SomethingChanged_RefreshQuarantineAnimalList = false;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateQuarantineAnimalsMainPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      bool ForceClosePanel;
      this.frame.UpdateAnimalCollectionPage(player, DeltaTime, offset, out ForceClosePanel);
      if (QuarantineAnimalsMainPanel.SomethingChanged_RefreshQuarantineAnimalList)
      {
        this.frame.RefreshGridContents(player);
        QuarantineAnimalsMainPanel.SomethingChanged_RefreshQuarantineAnimalList = false;
      }
      return ForceClosePanel || this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset);
    }

    public void DrawQuarantineAnimalsMainPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawAnimalCollectionPage(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.CollectionManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;

namespace TinyZoo.Z_Collection
{
  internal class CollectionManager
  {
    private CollectionMainPanel collectionMainPanel;

    public CollectionManager(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.collectionMainPanel = new CollectionMainPanel(player, baseScaleForUi);
      this.collectionMainPanel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player) => this.collectionMainPanel.CheckMouseOver(player, Vector2.Zero);

    public bool UpdateCollectionManager(
      Player player,
      float DeltaTime,
      Vector2 offset,
      ref bool WillClearInput)
    {
      return this.collectionMainPanel.UpdateCollectionMainPanel(player, DeltaTime, offset, ref WillClearInput);
    }

    public void DrawCollectionManager(Vector2 offset) => this.collectionMainPanel.DrawCollectionMainPanel(offset, AssetContainer.pointspritebatchTop05);
  }
}

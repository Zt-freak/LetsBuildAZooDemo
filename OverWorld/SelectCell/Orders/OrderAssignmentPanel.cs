// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.Orders.OrderAssignmentPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.OverWorld.SelectCell.Orders
{
  internal class OrderAssignmentPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private OrderAssignmentFrame frame;

    public OrderAssignmentPanel(Player player)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      List<IntakePerson> people = player.livestats.AnimalsJustTraded.People;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, addHeaderText: "Place These Animals", _BaseScale: baseScaleForUi);
      this.frame = new OrderAssignmentFrame(baseScaleForUi, people);
      this.bigBrownPanel.Finalize(this.frame.GetSize());
    }

    public Vector2 GetSize() => this.bigBrownPanel.vScale;

    public void SetData() => this.frame.SetData(Z_GameFlags.SelectedCellInfo);

    public void UnSetData() => this.frame.SetData((PrisonZone) null);

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      if (!this.bigBrownPanel.CheckMouseOver(player, offset))
        return false;
      Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = true;
      return true;
    }

    public void UpdateOrderAssignmentPanel(Player player, float DeltaTime, Vector2 offset)
    {
      if (Z_GameFlags.SelectedCellInfo != null)
      {
        this.SetData();
        Z_GameFlags.SelectedCellInfo = (PrisonZone) null;
      }
      if (Z_GameFlags.DeselectPenInPenSelect)
      {
        this.UnSetData();
        Z_GameFlags.DeselectPenInPenSelect = false;
      }
      offset += this.location;
      this.frame.UpdateOrderAssignmentFrame(player, DeltaTime, offset);
    }

    public NewAnimalsInCellInfo GetDataChanged_ForCellInfo() => this.frame.GetDataChanged_ForCellInfo();

    public List<IntakePerson> GetAnimalListSelected(bool SetAsAssigned) => this.frame.GetAnimalListSelected(SetAsAssigned);

    public void DrawOrderAssignmentPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.frame.DrawOrderAssignmentFrame(offset, spriteBatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.BottomFrame.AnimalBottomFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Quarantine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Collection.Animals.BottomFrame
{
  internal class AnimalBottomFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    public QuarantineBottomFrame quarantineFrame;

    public AnimalBottomFrame(
      CollectionType collectionType,
      float BaseScale,
      float ForcedWidth,
      int buildingUID = -1)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      if (collectionType == CollectionType.QuarantineAnimals)
        this.quarantineFrame = new QuarantineBottomFrame(BaseScale, ForcedWidth, buildingUID);
      else
        this.customerFrame = new CustomerFrame(new Vector2(ForcedWidth, uiScaleHelper.ScaleY(38f)), CustomerFrameColors.DarkBrown, BaseScale);
    }

    public Vector2 GetSize() => this.quarantineFrame != null ? this.quarantineFrame.GetSize() : this.customerFrame.VSCale;

    public void UpdateAnimalBottomFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ForceClosePanel,
      out bool SelectAllClicked)
    {
      offset += this.location;
      ForceClosePanel = false;
      SelectAllClicked = false;
      if (this.quarantineFrame == null)
        return;
      this.quarantineFrame.UpdateQuarantineBottomFrame(player, DeltaTime, offset, out ForceClosePanel, out SelectAllClicked);
    }

    public bool AddToQuarantineAnimalsList(PrisonerInfo prisonerInfo, bool DoNotRemove = false)
    {
      if (this.quarantineFrame != null)
        return this.quarantineFrame.AddOrRemoveAnimalToList(prisonerInfo, DoNotRemove);
      throw new Exception("wrong function!");
    }

    public void DrawAnimalBottomFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.quarantineFrame != null)
        this.quarantineFrame.DrawQuarantineBottomFrame(offset, spriteBatch);
      else
        this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
    }
  }
}

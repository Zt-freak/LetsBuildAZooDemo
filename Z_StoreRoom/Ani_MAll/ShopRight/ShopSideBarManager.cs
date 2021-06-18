// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopRight.ShopSideBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopRight
{
  internal class ShopSideBarManager
  {
    private GameObject Frame;
    private Vector2 FrameScale;
    private SideBarButton[] sidebarbuttons;

    public ShopSideBarManager()
    {
      this.Frame = new GameObject();
      this.FrameScale = new Vector2(196f, 768f);
      this.Frame.vLocation = new Vector2(1024f - this.FrameScale.X, 0.0f);
      this.Frame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Frame.SetAllColours(0.9372549f, 0.7019608f, 0.6f);
      this.sidebarbuttons = new SideBarButton[4];
      for (int index = 0; index < this.sidebarbuttons.Length; ++index)
      {
        this.sidebarbuttons[index] = new SideBarButton((SideBarButtonTYPE) index, this.FrameScale.X);
        this.sidebarbuttons[index].Location.Y = (float) (150 + index * 70);
      }
    }

    public SideBarButtonTYPE UpdateShopSideBarManager(
      Player player,
      Vector2 Offset,
      int TotalThings)
    {
      SideBarButtonTYPE sideBarButtonType = SideBarButtonTYPE.Count;
      this.sidebarbuttons[1].TotalThingsInCart = TotalThings;
      Offset += this.Frame.vLocation;
      for (int index1 = 0; index1 < this.sidebarbuttons.Length; ++index1)
      {
        if (this.sidebarbuttons[index1].UpdateSideBarButton(Offset, player))
        {
          for (int index2 = 0; index2 < this.sidebarbuttons.Length; ++index2)
            this.sidebarbuttons[index2].Selected = false;
          this.sidebarbuttons[index1].Selected = true;
          sideBarButtonType = (SideBarButtonTYPE) index1;
        }
      }
      return sideBarButtonType;
    }

    public void DrawShopSideBarManager(Vector2 Offset)
    {
      this.Frame.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.FrameScale);
      Offset += this.Frame.vLocation;
      for (int index = 0; index < this.sidebarbuttons.Length; ++index)
        this.sidebarbuttons[index].DrawSideBarButton(Offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.MainPanelman
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.IAPScreen.Version2.PanelParts;
using TinyZoo.IAPScreen.Version2.SmallerButton;

namespace TinyZoo.IAPScreen.Version2
{
  internal class MainPanelman
  {
    private Vector2 Location;
    private Vector2 VSCAle;
    private GameObjectNineSlice frame;
    private List<PanelIcon> panelicons;

    public MainPanelman(OfferButtonType btn, Player player)
    {
      this.frame = new GameObjectNineSlice(new Rectangle(961, 540, 21, 21), 7);
      this.frame.scale = 3f;
      this.Location = new Vector2(640f, 400f);
      this.panelicons = new List<PanelIcon>();
      switch (btn)
      {
        case OfferButtonType.WatchTheAdvert:
          this.panelicons.Add(new PanelIcon(IAPIConType.WatchAdvertForTimeTravel, player));
          break;
        case OfferButtonType.BuyTheGoat:
          this.panelicons.Add(new PanelIcon(IAPIConType.NoAdverts, player));
          this.panelicons.Add(new PanelIcon(IAPIConType.Goat, player));
          this.panelicons.Add(new PanelIcon(IAPIConType.SpeedUpTime, player));
          break;
        case OfferButtonType.TheVortexMind:
          this.panelicons.Add(new PanelIcon(IAPIConType.VortexMind, player));
          this.panelicons.Add(new PanelIcon(IAPIConType.TrashCompactor, player));
          break;
        case OfferButtonType.BuyTheFlower:
          this.panelicons.Add(new PanelIcon(IAPIConType.FlowerSuppressia, player));
          break;
      }
      if (this.panelicons.Count != 1)
      {
        if (this.panelicons.Count == 3)
        {
          this.panelicons[0].vLocation.X = -200f;
          this.panelicons[1].vLocation.X = 200f;
        }
        else
        {
          if (this.panelicons.Count != 2)
            throw new Exception("WRITE THE CODE TO HANDLE THIS");
          this.panelicons[0].vLocation.X = -150f;
          this.panelicons[1].vLocation.X = 150f;
        }
      }
      this.VSCAle = new Vector2(600f, 400f);
      this.Location.Y = 507f;
    }

    public bool UpdateMainPanelman(Player player, float DeltaTime)
    {
      bool flag = false;
      for (int index = 0; index < this.panelicons.Count; ++index)
      {
        if (this.panelicons[index].UpdatePanelIcon(player, DeltaTime, this.Location, this.VSCAle))
          flag = true;
      }
      return flag;
    }

    public void DrawMainPanelman()
    {
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Location, this.VSCAle);
      for (int index = 0; index < this.panelicons.Count; ++index)
        this.panelicons[index].DrawPanelIcon(this.Location, this.VSCAle);
      for (int index = 0; index < this.panelicons.Count; ++index)
        this.panelicons[index].PostDrawPanelIcon(this.Location, this.VSCAle);
    }
  }
}

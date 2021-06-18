// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame.TabFrameIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;

namespace TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame
{
  internal class TabFrameIcon : GameObject
  {
    public TabFrameIcon(TabType tabType, float BaseScale)
    {
      switch (tabType)
      {
        case TabType.A_Animal:
          this.DrawRect = new Rectangle(910, 122, 19, 24);
          break;
        case TabType.A_Habitat:
          this.DrawRect = new Rectangle(930, 122, 27, 23);
          break;
        case TabType.A_FamilyTree:
          this.DrawRect = new Rectangle(910, 147, 29, 23);
          break;
        case TabType.A_Profitability:
          this.DrawRect = new Rectangle(971, 86, 19, 22);
          break;
        case TabType.A_Info:
          this.DrawRect = new Rectangle(958, 122, 24, 21);
          break;
        case TabType.A_LifeTimeStats:
          this.DrawRect = new Rectangle(940, 146, 21, 21);
          break;
        case TabType.ParkFinances:
          this.DrawRect = new Rectangle(971, 86, 19, 22);
          break;
        case TabType.ParkReputation:
          this.DrawRect = new Rectangle(910, 122, 19, 24);
          break;
        case TabType.Collection_Animal:
          this.DrawRect = new Rectangle(898, 36, 22, 25);
          break;
        case TabType.Collection_Employees:
          this.DrawRect = new Rectangle(887, 231, 22, 23);
          break;
        case TabType.Collection_Achievements:
          this.DrawRect = new Rectangle(467, 375, 23, 21);
          break;
        case TabType.Collection_Summary:
          this.DrawRect = new Rectangle(360, 669, 21, 19);
          break;
        case TabType.People_VIPs:
          this.DrawRect = new Rectangle(887, 190, 19, 19);
          break;
        case TabType.People_Customers:
          this.DrawRect = new Rectangle(887, 210, 20, 20);
          break;
        case TabType.Employees_View:
          this.DrawRect = new Rectangle(887, 231, 22, 23);
          break;
        case TabType.Employees_Hire:
          this.DrawRect = new Rectangle(884, 441, 22, 20);
          break;
        default:
          throw new Exception("nsohf");
      }
      this.SetDrawOriginToCentre();
      if ((double) BaseScale == -1.0)
        this.scale = 1f;
      else
        this.scale = BaseScale;
    }

    public void DrawTabFrameIcon(Vector2 Offset, SpriteBatch spritebatch) => this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
  }
}

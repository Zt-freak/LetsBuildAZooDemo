// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.PanelParts.PanelIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.IAPScreen.Version2.PanelParts
{
  internal class PanelIcon : GameObject
  {
    private PanelIconHeading heading;
    private TimeToNextAdvert timetonextadvert;
    private BuyIAPButton buyIAPButton;

    public PanelIcon(IAPIConType iapicontype, Player player)
    {
      switch (iapicontype)
      {
        case IAPIConType.Goat:
          this.DrawRect = new Rectangle(794, 591, 93, 90);
          break;
        case IAPIConType.NoAdverts:
          this.DrawRect = new Rectangle(636, 612, 76, 69);
          break;
        case IAPIConType.SpeedUpTime:
          this.DrawRect = new Rectangle(713, 603, 80, 78);
          this.buyIAPButton = new BuyIAPButton(iapicontype, player);
          break;
        case IAPIConType.VortexMind:
          this.DrawRect = new Rectangle(589, 391, 93, 82);
          this.buyIAPButton = new BuyIAPButton(iapicontype, player);
          this.buyIAPButton.ExtraOffset.X = 150f;
          break;
        case IAPIConType.WatchAdvertForTimeTravel:
          this.DrawRect = new Rectangle(888, 612, 76, 69);
          this.timetonextadvert = new TimeToNextAdvert();
          break;
        case IAPIConType.TrashCompactor:
          this.DrawRect = new Rectangle(468, 719, 93, 82);
          break;
        case IAPIConType.FlowerSuppressia:
          this.DrawRect = new Rectangle(387, 304, 78, 76);
          this.buyIAPButton = new BuyIAPButton(IAPIConType.FlowerSuppressia, player);
          break;
      }
      this.SetDrawOriginToCentre();
      this.scale = 2f;
      this.heading = new PanelIconHeading(iapicontype);
    }

    public bool UpdatePanelIcon(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      Vector2 VSCaleOfFrame)
    {
      if (this.timetonextadvert != null)
        return this.timetonextadvert.UpdateTimeToNextAdvert(player, Offset + this.vLocation + new Vector2(0.0f, VSCaleOfFrame.Y * 0.5f));
      return this.buyIAPButton != null && this.buyIAPButton.UpdateBuyIAPButton(player, Offset + this.vLocation + new Vector2(0.0f, VSCaleOfFrame.Y * 0.5f));
    }

    public void DrawPanelIcon(Vector2 Offset, Vector2 VSCaleOfFrame)
    {
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      this.heading.DrawPanelIconHeading(Offset + this.vLocation + new Vector2(0.0f, VSCaleOfFrame.Y * -0.5f));
    }

    public void PostDrawPanelIcon(Vector2 Offset, Vector2 VSCaleOfFrame)
    {
      if (this.timetonextadvert != null)
        this.timetonextadvert.DrawTimeToNextAdvert(Offset + this.vLocation + new Vector2(0.0f, VSCaleOfFrame.Y * 0.5f));
      if (this.buyIAPButton == null)
        return;
      this.buyIAPButton.DrawBuyIAPButton(Offset + this.vLocation + new Vector2(0.0f, VSCaleOfFrame.Y * 0.5f));
    }
  }
}

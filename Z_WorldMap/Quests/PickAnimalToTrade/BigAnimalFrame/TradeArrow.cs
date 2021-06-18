// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame.TradeArrow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_WorldMap.Quests.PickAnimalToTrade.BigAnimalFrame
{
  internal class TradeArrow : GameObject
  {
    private string TEXT;
    private Color TextColour;

    public TradeArrow(int TotalHeld, int TargetValue, bool CanBreed)
    {
      this.DrawRect = new Rectangle(180, 30, 78, 76);
      this.scale = 2f;
      this.SetDrawOriginToCentre();
      this.TEXT = TotalHeld.ToString() + "/" + (object) TargetValue;
      this.TextColour = new Color(0.2392157f, 0.5686275f, 0.4235294f);
      if (CanBreed)
        return;
      this.TextColour = new Color(0.7607843f, 0.3568628f, 0.3568628f);
      this.DrawRect = new Rectangle(101, 30, 78, 76);
    }

    public void UpdateTradeArrow()
    {
    }

    public void DrawTradeArrow(Vector2 Offset)
    {
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.UISheet, Offset);
      TextFunctions.DrawJustifiedText(this.TEXT, 1f, this.vLocation + Offset + new Vector2(-8f, 5f), this.TextColour, 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
    }
  }
}

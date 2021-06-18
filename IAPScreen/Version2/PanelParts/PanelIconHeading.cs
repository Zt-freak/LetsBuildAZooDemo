// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.Version2.PanelParts.PanelIconHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using TinyZoo.PlayerDir;

namespace TinyZoo.IAPScreen.Version2.PanelParts
{
  internal class PanelIconHeading : GameObject
  {
    private string TextToWrite = "";
    private string TextToWrite2 = "";
    private string TextToWrite3 = "";

    public PanelIconHeading(IAPIConType iapicontype)
    {
      this.DrawRect = new Rectangle(463, 557, 91, 15);
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      switch (iapicontype)
      {
        case IAPIConType.Goat:
          this.TextToWrite = "Cash +5%";
          break;
        case IAPIConType.NoAdverts:
          this.TextToWrite = "No Ads";
          break;
        case IAPIConType.SpeedUpTime:
          this.TextToWrite = "Speed-Up";
          break;
        case IAPIConType.VortexMind:
          this.TextToWrite = "The Vortex Mind";
          break;
        case IAPIConType.WatchAdvertForTimeTravel:
          this.TextToWrite = "10 Minutes Time Travel";
          break;
        case IAPIConType.TrashCompactor:
          this.TextToWrite = "Trash Compactor";
          break;
        case IAPIConType.FlowerSuppressia:
          this.TextToWrite = "10 Minutes Time Travel";
          break;
      }
    }

    public void UpdatePanelIconHeading()
    {
    }

    public void DrawPanelIconHeading(Vector2 Offset)
    {
      Offset.Y += 9f;
      this.scale = 1.9f;
      float num1 = 1f;
      float num2 = 1f;
      float num3 = 1f;
      if (PlayerStats.language == Language.Russian)
      {
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite, "XXXXXXXXXX", AssetContainer.springFont, true);
        num2 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite, "XXXXXXXXXX", AssetContainer.springFont, true);
      }
      if (PlayerStats.language == Language.French || PlayerStats.language == Language.Spanish || PlayerStats.language == Language.Portuguese)
      {
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite, "XXXXXXXXXXXX", AssetContainer.springFont, true);
        num2 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite2, "XXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        num3 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite3, "XXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      }
      if (PlayerStats.language == Language.German)
      {
        num1 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite, "XXXXXXXXXXX", AssetContainer.springFont, true);
        num2 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite2, "XXXXXXXXXXXXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
        num3 = TextFunctions.GetStringPercentageReScaledToSpecificLength(this.TextToWrite3, "XXXXXXXXXXXXXXXX", AssetContainer.springFont, true);
      }
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawJustifiedText(this.TextToWrite, 3f * num1, Offset + new Vector2(0.0f, 15f * Sengine.ScreenRatioUpwardsMultiplier.Y), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText(this.TextToWrite2, 3f * num2, Offset + new Vector2(0.0f, 15f * Sengine.ScreenRatioUpwardsMultiplier.Y), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawJustifiedText(this.TextToWrite3, 3f * num3, Offset + new Vector2(0.0f, 15f * Sengine.ScreenRatioUpwardsMultiplier.Y), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}

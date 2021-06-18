// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.Morality.MoralityRequirementDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Research_.IconGrid.Morality
{
  internal class MoralityRequirementDisplay
  {
    public Vector2 location;
    private ZGenericText header;
    private MoralityBar moralityBar;
    private ZGenericText moralityExtraText;
    private CustomerFrame customerFrame;

    public MoralityRequirementDisplay(
      TILETYPE building,
      Player player,
      float BaseScale,
      float forceThisWidth,
      Vector3 frameColor,
      bool UseBigHeader = true,
      bool UseSmallerBuffer = false)
    {
      this.SetUp(new List<TILETYPE>() { building }, player, BaseScale, forceThisWidth, frameColor, UseBigHeader, UseSmallerBuffer);
    }

    public MoralityRequirementDisplay(
      List<TILETYPE> buildingsForThisUnlock,
      Player player,
      float BaseScale,
      float forceThisWidth,
      Vector3 frameColor)
    {
      this.SetUp(buildingsForThisUnlock, player, BaseScale, forceThisWidth, frameColor);
    }

    private void SetUp(
      List<TILETYPE> buildingsForThisUnlock,
      Player player,
      float BaseScale,
      float forceThisWidth,
      Vector3 frameColor,
      bool UseBigHeader = false,
      bool UseSmallerBuffers = true)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      if (UseSmallerBuffers)
        defaultBuffer *= 0.5f;
      float num1 = 0.0f;
      int num2 = 0;
      for (int index = 0; index < buildingsForThisUnlock.Count; ++index)
      {
        if (MoralityUnlocksData.IsAMoralityBuilding(buildingsForThisUnlock[index]))
        {
          int toUseThisBuilding = MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(buildingsForThisUnlock[index]);
          if (Math.Abs(toUseThisBuilding) > Math.Abs(num2))
            num2 = toUseThisBuilding;
        }
      }
      if (num2 == 0)
        return;
      float y1 = num1 + defaultBuffer.Y;
      this.header = new ZGenericText(SEngine.Localization.Localization.GetText(992), BaseScale, false, _UseOnePointFiveFont: UseBigHeader);
      this.header.vLocation = new Vector2(defaultBuffer.X, y1);
      float y2 = y1 + (this.header.GetSize().Y + defaultBuffer.Y);
      this.moralityBar = new MoralityBar(num2 > 0, BaseScale, true, true);
      float fullness = (double) player.livestats.MoralityScore > 0.0 == num2 > 0 ? player.livestats.MoralityScore / (float) num2 : 0.0f;
      this.moralityBar.SetScoreValue(fullness, (float) Math.Abs(num2));
      this.moralityBar.location = new Vector2(defaultBuffer.X, y2);
      this.moralityBar.location += this.moralityBar.GetSize() * 0.5f;
      float y3 = y2 + this.moralityBar.GetSize().Y;
      if ((double) fullness < 1.0)
      {
        float y4 = y3 + defaultBuffer.Y * 0.5f;
        this.moralityExtraText = new ZGenericText(SEngine.Localization.Localization.GetText(993), BaseScale, false);
        this.moralityExtraText.vLocation = new Vector2(defaultBuffer.X, y4);
        if (frameColor == ColourData.Z_FrameDarkBrown)
        {
          if (num2 < 0)
            this.moralityExtraText.SetAllColours(ColourData.EvilPurple);
          else
            this.moralityExtraText.SetAllColours(ColourData.GoodYellow);
        }
        else
          this.moralityExtraText.SetAllColours(ColourData.Z_Cream);
        y3 = y4 + this.moralityExtraText.GetSize().Y + defaultBuffer.Y;
      }
      this.customerFrame = new CustomerFrame(new Vector2(forceThisWidth, y3), frameColor, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      ZGenericText header = this.header;
      header.vLocation = header.vLocation + vector2;
      this.moralityBar.location += vector2;
      if (this.moralityExtraText == null)
        return;
      ZGenericText moralityExtraText = this.moralityExtraText;
      moralityExtraText.vLocation = moralityExtraText.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame != null ? this.customerFrame.VSCale : Vector2.Zero;

    public void DrawMoralityRequirementDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.customerFrame == null)
        return;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.header.DrawZGenericText(offset, spriteBatch);
      this.moralityBar.DrawMoralityBar(offset, spriteBatch);
      if (this.moralityExtraText == null)
        return;
      this.moralityExtraText.DrawZGenericText(offset, spriteBatch);
    }
  }
}

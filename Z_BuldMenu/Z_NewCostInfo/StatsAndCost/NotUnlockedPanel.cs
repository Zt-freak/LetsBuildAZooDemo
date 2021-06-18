// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost.NotUnlockedPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuldMenu.Z_NewCostInfo.StatsAndCost
{
  internal class NotUnlockedPanel
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler desc;
    private LittleSummaryButton littleSummaryButton;

    public NotUnlockedPanel(float BaseScale, float ForcedWith, Player player, TILETYPE tileType)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      float y1 = defaultBuffer.Y;
      bool flag1 = player.shopstatus.ArchitectOffice.Count > 0;
      bool flag2 = MainBarManager.CannotBuildAnymoreOfThisBuilding(tileType, player);
      bool flag3 = false;
      if (Z_DebugFlags.IsBetaVersion)
      {
        flag3 = true;
        List<REntry> researchData = RGrid_Data.GetResearchData();
        for (int index1 = 0; index1 < researchData.Count; ++index1)
        {
          List<TILETYPE> willUnlockThese = researchData[index1].WillUnlockThese;
          for (int index2 = 0; index2 < willUnlockThese.Count; ++index2)
          {
            if (willUnlockThese[index2] == tileType)
            {
              flag3 = false;
              break;
            }
          }
        }
      }
      string text = "Locked";
      if (flag2)
        text = "Max Built";
      this.customerFrame = new CustomerFrame(Vector2.Zero, ColourData.Z_FrameRedPale, BaseScale);
      this.customerFrame.AddMiniHeading(text);
      float y2 = y1 + this.customerFrame.GetMiniHeadingHeight();
      string empty = string.Empty;
      float width_ = ForcedWith - defaultBuffer.X * 2f;
      string TextToWrite;
      if (flag2)
        TextToWrite = "You cannot build more than 1 of this type of item.";
      else if (flag3)
        TextToWrite = "Unavailable in Beta";
      else if (flag1)
      {
        TextToWrite = "Not yet researched. Continue researching to discover more about this item!";
        width_ = ForcedWith * 0.75f;
      }
      else
        TextToWrite = "Not yet researched.~You must build an Architect Office to start researching new items.";
      this.desc = new SimpleTextHandler(TextToWrite, width_, _Scale: BaseScale, AutoComplete: true);
      this.desc.SetAllColours(ColourData.Z_Cream);
      this.desc.Location = new Vector2(defaultBuffer.X, y2);
      float y3 = y2 + this.desc.GetHeightOfParagraph() + defaultBuffer.Y;
      if (((flag3 ? 0 : (!flag2 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
      {
        this.littleSummaryButton = new LittleSummaryButton(LittleSummaryButtonType.Action_Neutral, _BaseScale: BaseScale);
        this.littleSummaryButton.vLocation.X = (float) ((double) ForcedWith - (double) defaultBuffer.X - (double) this.littleSummaryButton.GetSize().X * 0.5);
        this.littleSummaryButton.vLocation.Y = this.desc.Location.Y + this.desc.GetHeightOfParagraph() * 0.5f;
      }
      this.customerFrame.Resize(new Vector2(ForcedWith, y3));
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.desc.Location += vector2;
      if (this.littleSummaryButton == null)
        return;
      LittleSummaryButton littleSummaryButton = this.littleSummaryButton;
      littleSummaryButton.vLocation = littleSummaryButton.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool UpdateNotUnlockedPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.littleSummaryButton == null || !this.littleSummaryButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return false;
      Game1.SetNextGameState(GAMESTATE.ArchitectResearchSetUp);
      Game1.screenfade.BeginFade(true);
      return true;
    }

    public void DrawNotUnlockedPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.desc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.littleSummaryButton == null)
        return;
      this.littleSummaryButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}

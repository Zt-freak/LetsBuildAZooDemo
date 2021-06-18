// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.Elements.PackInformation.PackInfoDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Research_.IconGrid.Elements.PackInformation
{
  internal class PackInfoDisplay
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private SimpleTextHandler secondtext;
    private float ParaWidth;
    private List<BuildingIcon> unlockedBuildings;
    private ProgressBarWithPointer progressBar;
    private SegmentedBarV2 segmentedBar;

    public PackInfoDisplay(
      REntry _rentry,
      R_Icon ricon,
      Player player,
      float BaseScale,
      float forcedWidth)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = uiScaleHelper.DefaultBuffer * 0.5f;
      float num1 = vector2_1.Y;
      this.unlockedBuildings = new List<BuildingIcon>();
      float num2 = uiScaleHelper.ScaleX(5f);
      for (int index = 0; index < _rentry.WillUnlockThese.Count; ++index)
      {
        BuildingIcon buildingIcon = new BuildingIcon(_rentry.WillUnlockThese[index], BaseScale);
        Vector2 size = buildingIcon.GetSize();
        buildingIcon.vLocation.Y = num1;
        buildingIcon.vLocation.X = vector2_1.X;
        buildingIcon.vLocation.X += size.X * 0.5f;
        buildingIcon.vLocation.Y += size.Y * 0.5f;
        buildingIcon.vLocation.X += (float) index * (size.X + num2);
        this.unlockedBuildings.Add(buildingIcon);
        if (index == _rentry.WillUnlockThese.Count - 1)
          num1 = num1 + size.Y + vector2_1.Y;
      }
      string str1 = "";
      string str2 = ricon.unlockstate != UnlockState.Preview ? (ricon.unlockstate != UnlockState.Locked ? str1 + "You have unlocked this!" : str1 + "LOCKED! Unlock more items to reach this location!") : str1 + "Research points to unlock: " + (object) _rentry.Cost;
      int numberUnlocked;
      int totalInSet;
      ResearchUpgradeInfoSet tiers;
      int nextTierIndex;
      string bonusSetsString = RGrid_Data.GetBonusSetsString(_rentry.upgradeCategory, player, out numberUnlocked, out totalInSet, out tiers, out nextTierIndex);
      this.secondtext = new SimpleTextHandler(string.IsNullOrEmpty(bonusSetsString) ? str2 + "~~" + _rentry.Description : str2 + "~~" + bonusSetsString, forcedWidth - vector2_1.X, _Scale: BaseScale, AutoComplete: true);
      float heightOfParagraph = this.secondtext.GetHeightOfParagraph();
      this.secondtext.SetAllColours(ColourData.Z_Cream);
      this.secondtext.Location.Y = num1;
      this.secondtext.Location.X = vector2_1.X;
      float y1 = num1 + heightOfParagraph + vector2_1.Y;
      if (tiers != null && tiers.researchinfo.Count != 0)
      {
        this.segmentedBar = new SegmentedBarV2(totalInSet, numberUnlocked, BaseScale, forcedWidth);
        for (int index = 0; index < tiers.researchinfo.Count; ++index)
        {
          string str3 = string.Empty;
          if (index == nextTierIndex)
            str3 = "Bonus";
          this.segmentedBar.SetPointer(tiers.researchinfo[index].TotalInSet, str3.ToUpper());
        }
        float y2 = this.segmentedBar.GetBarSize().Y;
        this.segmentedBar.Location.Y = y1;
        this.segmentedBar.Location.Y += y2 * 0.5f;
        this.segmentedBar.Location.X = vector2_1.X;
        this.segmentedBar.Location.X += this.segmentedBar.GetBarSize().X * 0.5f;
        y1 = y1 + (y2 + this.segmentedBar.GetExtraHeightFromText()) + vector2_1.Y;
      }
      this.customerframe = new CustomerFrame(new Vector2(forcedWidth, y1), CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2_2 = -this.customerframe.VSCale * 0.5f;
      this.secondtext.Location += vector2_2;
      for (int index = 0; index < this.unlockedBuildings.Count; ++index)
      {
        BuildingIcon unlockedBuilding = this.unlockedBuildings[index];
        unlockedBuilding.vLocation = unlockedBuilding.vLocation + vector2_2;
      }
      if (this.segmentedBar == null)
        return;
      this.segmentedBar.Location += vector2_2;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool GetHasSegmentedBarDisplay() => this.segmentedBar != null;

    public void DrawPackInfoDisplay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      for (int index = 0; index < this.unlockedBuildings.Count; ++index)
        this.unlockedBuildings[index].DrawBuildingIcon(offset, 1f, spriteBatch);
      this.secondtext.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.segmentedBar == null)
        return;
      this.segmentedBar.DrawSegmentedBar(offset, spriteBatch);
    }
  }
}

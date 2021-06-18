// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.CurrentJobPostingFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment
{
  internal class CurrentJobPostingFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private OpenPositionsFrame openJobPositionsFrame;
    private List<PostingInfoFrame> modifiersFrames;
    private CampaignSummaryFrame campaignSummaryFrame;
    private TextButton applyButton;
    internal static bool UseNewLayout = true;
    private OpenPositions ORIGINALOPENPOSITION;
    private OpenPositions TEMPOPENPOSITIONS;

    public CurrentJobPostingFrame(
      ShopEntry shopEntry,
      OpenPositions _TEMPOPENPOSITIONS,
      OpenPositions _ORIGINALOPENPOSITION,
      Player player,
      float BaseScale,
      EmployeeType _RoamingEmplyeeType = EmployeeType.None)
    {
      this.ORIGINALOPENPOSITION = _ORIGINALOPENPOSITION;
      this.TEMPOPENPOSITIONS = _TEMPOPENPOSITIONS;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      Vector2 _vScale = Vector2.Zero;
      _vScale = new Vector2(defaultXbuffer, defaultYbuffer);
      this.customerFrame = new CustomerFrame(Vector2.Zero, true, BaseScale);
      string str = "Recruitment Campaign: ";
      this.customerFrame.AddMiniHeading(_RoamingEmplyeeType == EmployeeType.None ? str + TileData.GetTileStats(shopEntry.tiletype).Name : str + Employees.GetEmployeeThypeToString(_RoamingEmplyeeType));
      _vScale.Y += this.customerFrame.GetMiniHeadingHeight();
      float forceWidth = 325f * BaseScale;
      if (CurrentJobPostingFrame.UseNewLayout)
        forceWidth = 0.0f;
      List<JobPostingModifiers> postingModifiersList = new List<JobPostingModifiers>();
      for (int index = 0; index < 3; ++index)
        postingModifiersList.Add((JobPostingModifiers) index);
      if (CurrentJobPostingFrame.UseNewLayout)
        postingModifiersList.Add(JobPostingModifiers.Totals);
      this.modifiersFrames = new List<PostingInfoFrame>();
      for (int index = 0; index < postingModifiersList.Count; ++index)
      {
        PostingInfoFrame postingInfoFrame = new PostingInfoFrame(postingModifiersList[index], this.TEMPOPENPOSITIONS, forceWidth, BaseScale, CurrentJobPostingFrame.UseNewLayout);
        forceWidth += postingInfoFrame.GetSize().X;
        if (index != postingModifiersList.Count - 1)
          forceWidth += defaultXbuffer;
        this.modifiersFrames.Add(postingInfoFrame);
      }
      this.openJobPositionsFrame = new OpenPositionsFrame(this.TEMPOPENPOSITIONS, this.ORIGINALOPENPOSITION, forceWidth, BaseScale, shopEntry, player, _RoamingEmplyeeType);
      this.openJobPositionsFrame.location.Y += _vScale.Y + this.openJobPositionsFrame.GetSize().Y * 0.5f;
      _vScale.Y += this.openJobPositionsFrame.GetSize().Y;
      _vScale.Y += defaultYbuffer;
      for (int index = 0; index < this.modifiersFrames.Count; ++index)
      {
        PostingInfoFrame modifiersFrame = this.modifiersFrames[index];
        if (CurrentJobPostingFrame.UseNewLayout)
        {
          modifiersFrame.location = _vScale;
          modifiersFrame.location += modifiersFrame.GetSize() * 0.5f;
          _vScale.X += modifiersFrame.GetSize().X;
          _vScale.X += defaultXbuffer;
        }
        else
        {
          modifiersFrame.location.Y += _vScale.Y + modifiersFrame.GetSize().Y * 0.5f;
          _vScale.Y += modifiersFrame.GetSize().Y;
          _vScale.Y += defaultYbuffer;
        }
      }
      if (CurrentJobPostingFrame.UseNewLayout)
      {
        _vScale.Y += this.modifiersFrames[0].GetSize().Y;
        _vScale.Y += defaultYbuffer;
      }
      this.campaignSummaryFrame = new CampaignSummaryFrame(this.TEMPOPENPOSITIONS, this.ORIGINALOPENPOSITION, forceWidth, BaseScale, player);
      this.campaignSummaryFrame.location.Y += _vScale.Y + this.campaignSummaryFrame.GetSize().Y * 0.5f;
      _vScale.Y += this.campaignSummaryFrame.GetSize().Y;
      _vScale.Y += defaultYbuffer;
      this.applyButton = new TextButton(BaseScale, "Apply Changes", 75f);
      this.applyButton.vLocation.Y = defaultYbuffer + this.customerFrame.GetMiniHeadingHeight(false) * 0.5f;
      this.applyButton.vLocation.X = _vScale.X - defaultXbuffer;
      this.applyButton.vLocation.X -= this.applyButton.GetSize().X * 0.5f;
      this.applyButton.Disable(true);
      this.customerFrame.Resize(_vScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.openJobPositionsFrame.location.Y += vector2.Y;
      for (int index = 0; index < this.modifiersFrames.Count; ++index)
        this.modifiersFrames[index].location += vector2;
      this.campaignSummaryFrame.location.Y += vector2.Y;
      TextButton applyButton = this.applyButton;
      applyButton.vLocation = applyButton.vLocation + vector2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool GetHasUnsavedChanges() => !this.applyButton.IsDisabledAndDarkened;

    public bool UpdateCurrentJobPostingFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      if (this.openJobPositionsFrame.UpdateOpenPositionsFrame(player, DeltaTime, offset))
      {
        flag2 = true;
        this.campaignSummaryFrame.ReflectNewData(player);
        flag3 = true;
      }
      for (int index = 0; index < this.modifiersFrames.Count; ++index)
      {
        if (this.modifiersFrames[index].UpdatePostingInfoFrame(player, DeltaTime, offset))
        {
          flag1 = true;
          this.modifiersFrames[index].ReflectNewData();
          this.campaignSummaryFrame.ReflectNewData(player);
          flag3 = true;
        }
      }
      if (flag1 | flag2)
      {
        for (int index = 0; index < this.modifiersFrames.Count; ++index)
        {
          if (flag2 || flag1 && this.modifiersFrames[index].refInfoType == JobPostingModifiers.Totals)
            this.modifiersFrames[index].ReflectNewData();
        }
      }
      this.campaignSummaryFrame.UpdateCampaignSummaryFrame(player, DeltaTime, offset);
      if (flag3)
      {
        if (this.TEMPOPENPOSITIONS.CompareIfChanged(this.ORIGINALOPENPOSITION))
          this.applyButton.Disable(false);
        else
          this.applyButton.Disable(true);
      }
      if (!this.applyButton.UpdateTextButton(player, offset, DeltaTime))
        return false;
      this.applyButton.Disable(true);
      return true;
    }

    public void DrawCurrentJobPostingFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.openJobPositionsFrame.DrawOpenPositionsFrame(offset, spriteBatch);
      for (int index = 0; index < this.modifiersFrames.Count; ++index)
        this.modifiersFrames[index].DrawPostingInfoFrame(offset, spriteBatch);
      this.campaignSummaryFrame.DrawCampaignSummaryFrame(offset, spriteBatch);
      this.applyButton.DrawTextButton(offset, 1f, spriteBatch);
      for (int index = 0; index < this.modifiersFrames.Count; ++index)
        this.modifiersFrames[index].PostDrawPostingInfoFrame(offset, spriteBatch);
    }
  }
}

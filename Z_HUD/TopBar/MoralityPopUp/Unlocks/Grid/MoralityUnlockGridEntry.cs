// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid.MoralityUnlockGridEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine.Text;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.Grid
{
  internal class MoralityUnlockGridEntry
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MouseoverHandler mouseOverHandler;
    private MoralityUnlockIcon icon;
    private ZGenericText Name;
    private StringScroller nameScroller;
    private MoralityBar bar;
    private MoralityUnlock refMoralityUnlockType;
    private bool IsGoodUnlocksDisplay;

    public MoralityUnlockGridEntry(MoralityUnlock moralityUnlock, Player player, float BaseScale)
    {
      this.refMoralityUnlockType = moralityUnlock;
      int pointsNeededToUnlock = MoralityUnlocksData.GetPointsNeededToUnlock(moralityUnlock);
      this.IsGoodUnlocksDisplay = pointsNeededToUnlock >= 0;
      Math.Abs(pointsNeededToUnlock);
      int num1 = MoralityUnlocksData.PlayerHasResearchUnlockedForThis(moralityUnlock, player) ? 1 : 0;
      bool useThis = MoralityUnlocksData.PlayerHasEnoughPointsToUseThis(moralityUnlock, player);
      double moralityScore = (double) player.livestats.MoralityScore;
      string _textToWrite = "Unknown";
      if (num1 != 0)
        _textToWrite = MoralityUnlocksData.GetMoralityUnlockToNameString(moralityUnlock);
      this.icon = new MoralityUnlockIcon(moralityUnlock, BaseScale);
      if (!useThis)
        this.icon.AddSlash();
      this.Name = new ZGenericText(_textToWrite, BaseScale, false, _UseOnePointFiveFont: true);
      this.bar = new MoralityBar(this.IsGoodUnlocksDisplay, BaseScale, true, true);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      float num2 = uiScaleHelper.ScaleX(170f);
      Vector2 vector2_1 = defaultBuffer;
      this.icon.vLocation = vector2_1;
      MoralityUnlockIcon icon1 = this.icon;
      icon1.vLocation = icon1.vLocation + this.icon.GetSize() * 0.5f;
      vector2_1.X += this.icon.GetSize().X + defaultBuffer.X;
      float num3 = num2 - vector2_1.X - defaultBuffer.X;
      this.Name.vLocation = vector2_1;
      vector2_1.Y += this.Name.GetSize().Y;
      this.bar.location = vector2_1;
      this.bar.location += this.bar.GetSize() * 0.5f;
      vector2_1.Y += this.bar.GetSize().Y;
      vector2_1.Y = Math.Max(this.icon.GetSize().Y + defaultBuffer.Y, vector2_1.Y);
      vector2_1.Y += defaultBuffer.Y;
      vector2_1.X = num2;
      this.customerFrame = new CustomerFrame(vector2_1, ColourData.Z_FrameMidBrown, BaseScale);
      this.mouseOverHandler = new MouseoverHandler(vector2_1, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      MoralityUnlockIcon icon2 = this.icon;
      icon2.vLocation = icon2.vLocation + vector2_2;
      ZGenericText name = this.Name;
      name.vLocation = name.vLocation + vector2_2;
      this.bar.location += vector2_2;
      this.nameScroller = new StringScroller(num3 / BaseScale, this.Name.textToWrite, this.Name.fontToUse);
      this.RefreshValues(player);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void RefreshValues(Player player)
    {
      int pointsNeededToUnlock = MoralityUnlocksData.GetPointsNeededToUnlock(this.refMoralityUnlockType);
      float moralityScore = player.livestats.MoralityScore;
      bool flag = (double) moralityScore >= 0.0;
      int num = Math.Abs(pointsNeededToUnlock);
      this.bar.SetScoreValue(MathHelper.Clamp(this.IsGoodUnlocksDisplay != flag ? 0.0f : Math.Abs(moralityScore) / (float) num, 0.0f, 1f), (float) num);
    }

    public bool UpdateMoralityUnlockGridEntry(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.nameScroller.UpdateStringScroller(DeltaTime);
      this.Name.textToWrite = this.nameScroller.GetString();
      return this.mouseOverHandler.Clicked;
    }

    public void DrawMoralityUnlockGridEntry(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.icon.DrawMoralityUnlockIcon(offset, spriteBatch);
      this.Name.DrawZGenericText(offset, spriteBatch);
      this.mouseOverHandler.DrawMouseOverHandler(spriteBatch, offset);
      this.bar.DrawMoralityBar(offset, spriteBatch);
    }
  }
}

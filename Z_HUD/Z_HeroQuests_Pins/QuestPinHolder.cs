// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_HeroQuests_Pins.QuestPinHolder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.Z_HeroQuests_Pins.PinNotification;

namespace TinyZoo.Z_HUD.Z_HeroQuests_Pins
{
  internal class QuestPinHolder
  {
    private PinNotificationFrame bubble;
    private LerpHandler_Float lerper;
    private float WIDTH;
    private int Index;
    public HeroQuestDescription thisquest;
    public OffscreenPointerType offscreenpointertype;
    public bool TaskIsComplete;
    private UIScaleHelper scaleHelper;
    private bool AllowLerpAlways;
    private float Height;
    public bool IsFromRbbon;
    private int ProgressPrior;
    private float LastPercent;

    public QuestPinHolder(
      string HEading,
      string Body,
      int Count,
      HeroQuestDescription _thisquest,
      Player player,
      bool _AllowLerpAlways,
      OffscreenPointerType _offscreenpointertype = OffscreenPointerType.Count,
      bool _IsFromRbbon = false)
    {
      this.IsFromRbbon = _IsFromRbbon;
      this.offscreenpointertype = _offscreenpointertype;
      this.AllowLerpAlways = _AllowLerpAlways;
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.thisquest = _thisquest;
      bool AllowCloseButton = player.Stats.TutorialsComplete[29];
      this.bubble = new PinNotificationFrame(HEading, Body, baseScaleForUi, this.GetQuestProgressFloat(player), AllowCloseButton, this.offscreenpointertype);
      if (this.IsFromRbbon)
        this.bubble.customerFrame.SetColour(new Vector3(1f, 1f, 0.7f));
      this.scaleHelper = new UIScaleHelper(baseScaleForUi);
      this.WIDTH = this.bubble.GetSize().X;
      this.bubble.location.X = (float) (1024.0 - (double) this.WIDTH * 0.5);
      this.SetYLocation(Count);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Index = Count;
      this.Height = this.bubble.GetSize().Y - this.scaleHelper.ScaleY(2f);
    }

    private void SetYLocation(int _Index)
    {
    }

    private float GetQuestProgressFloat(Player player)
    {
      if (this.thisquest != null)
      {
        int ProgressStart;
        int Progressend;
        this.thisquest.GetObjectiveHeading(out string _, player, out ProgressStart, out Progressend);
        if (Progressend != 0)
        {
          this.ProgressPrior = Progressend;
          this.LastPercent = (float) ProgressStart / (float) Progressend;
        }
        this.LastPercent = MathHelper.Clamp(this.LastPercent, 0.0f, 1f);
      }
      return this.LastPercent;
    }

    public bool UpdateQuestPinHolder(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      int _Index,
      ref float DrawHeight)
    {
      Offset.Y += DrawHeight;
      Offset.Y += this.Height * 0.5f;
      if (this.Index != _Index)
      {
        this.SetYLocation(_Index);
        this.Index = _Index;
      }
      if ((double) this.lerper.TargetValue == 0.0 && !OverWorldManager.zoopopupHolder.IsNull() && !this.AllowLerpAlways)
        return false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0 && this.AllowLerpAlways)
        this.AllowLerpAlways = false;
      float questProgressFloat = this.GetQuestProgressFloat(player);
      this.bubble.SetBarValues(questProgressFloat);
      if ((double) questProgressFloat >= 1.0)
      {
        this.bubble.SetHeaderText("TASK COMPLETED");
        if (!this.TaskIsComplete)
        {
          this.TaskIsComplete = true;
          if (this.thisquest != null)
            Z_QuestPinManager.DoubleCheckTaskNotifications = true;
        }
      }
      else
        this.TaskIsComplete = false;
      bool ClickedOnCross;
      if (this.bubble.UpdatePinNotificationFrame(player, DeltaTime, Offset, out ClickedOnCross, this.offscreenpointertype != OffscreenPointerType.Count) && this.offscreenpointertype == OffscreenPointerType.Count && (this.thisquest == null || this.thisquest.herocharacter != HeroCharacter.Investor || this.thisquest.heroquesttype != HEROQUESTTYPE.ViewTasks))
        OverWorldManager.zoopopupHolder.CreateZooPopUps((HeroQuestDescription) null, player, POPUPSTATE.HeroQuests, false);
      if (ClickedOnCross)
      {
        if (this.offscreenpointertype != OffscreenPointerType.Count)
        {
          PointOffScreenManager.RemovePointer(this.offscreenpointertype);
          ZHudManager.zquestpins.UnPinQuest(this.offscreenpointertype, player);
        }
        else
          ZHudManager.zquestpins.UnPinQuest(this.thisquest, player);
      }
      if (this.bubble.CheckMouseOver(player, Offset))
      {
        DrawHeight += this.Height;
        return true;
      }
      DrawHeight += this.Height;
      return false;
    }

    public void DrawQuestPinHolder(Vector2 Offset, ref float DrawHeight)
    {
      Offset.X += this.lerper.Value * this.WIDTH;
      Offset.Y += DrawHeight;
      Offset.Y += this.Height * 0.5f;
      this.bubble.DrawPinNotificationFrame(Offset, AssetContainer.pointspritebatch03);
      DrawHeight += this.Height;
    }
  }
}

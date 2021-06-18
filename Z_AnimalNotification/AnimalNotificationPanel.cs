// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalNotificationPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalNotificationPanel
  {
    private BigBrownPanel panel;
    private AnimalNotificationInfoBox infoBox;
    public Vector2 location;
    private MovingTray tray;
    private AnimalNotificationFixBox fixbox;
    private bool needFixBox;
    private AnimalNotificationType notificationType;
    private int reason;
    public bool ConstructionFailed;

    public AnimalNotificationPanel(
      Player player,
      AnimalNotificationType _notificationType,
      List<PrisonerInfo> infolist,
      float basescale,
      int reason_ = 0,
      List<AnimalRenderDescriptor> hybridAnimals = null)
    {
      PrisonerInfo info = (PrisonerInfo) null;
      this.notificationType = _notificationType;
      this.reason = reason_;
      if (infolist != null && infolist.Count > 0)
      {
        info = infolist[0];
        if (infolist.Count > 1)
        {
          PrisonerInfo prisonerInfo = infolist[1];
        }
      }
      UIScaleHelper uiScaleHelper = new UIScaleHelper(basescale);
      double num = (double) uiScaleHelper.ScaleY(10f);
      this.infoBox = new AnimalNotificationInfoBox(this.notificationType, basescale, infolist, player, hybridAnimals: hybridAnimals);
      if (this.infoBox.ConstructionFailed)
        this.ConstructionFailed = true;
      Vector2 vector2 = new Vector2();
      vector2.X = MathHelper.Max(this.infoBox.GetSize().X, 0.0f);
      vector2.Y = this.infoBox.GetSize().Y;
      string addHeaderText;
      switch (this.notificationType)
      {
        case AnimalNotificationType.Birth:
          addHeaderText = "Birth";
          this.needFixBox = false;
          break;
        case AnimalNotificationType.Death:
          addHeaderText = "Death";
          this.needFixBox = false;
          break;
        case AnimalNotificationType.Hunger:
          addHeaderText = "Hunger";
          this.needFixBox = true;
          break;
        case AnimalNotificationType.Fight:
          addHeaderText = "Fight!";
          this.needFixBox = false;
          break;
        case AnimalNotificationType.Breakout:
          addHeaderText = "Breakout";
          this.needFixBox = false;
          break;
        case AnimalNotificationType.CRIPSRBirth:
          addHeaderText = "CRIPSR";
          this.needFixBox = false;
          break;
        default:
          throw new Exception("invalid notification type!");
      }
      if (this.needFixBox)
      {
        this.fixbox = new AnimalNotificationFixBox(this.notificationType, info, vector2, basescale, player, this.reason);
        Vector2 sizeFrameless = this.fixbox.GetSize_Frameless();
        if ((double) sizeFrameless.Y > (double) vector2.Y)
        {
          vector2.Y = sizeFrameless.Y;
          this.infoBox = new AnimalNotificationInfoBox(this.notificationType, basescale, infolist, player, sizeFrameless.Y);
          if (this.infoBox.ConstructionFailed)
            this.ConstructionFailed = true;
        }
        this.tray = new MovingTray(vector2, new Vector2(vector2.X + uiScaleHelper.ScaleX(15f), 0.0f), MovingTrayDirection.Right, basescale, "Details");
        this.tray.location = this.infoBox.location;
        this.fixbox.location = this.tray.GetTruePosition();
      }
      if (this.infoBox != null)
      {
        this.infoBox.location = this.location;
        this.infoBox.location.Y = (float) (-0.5 * (double) vector2.Y + 0.5 * (double) this.infoBox.GetSize().Y);
      }
      this.panel = new BigBrownPanel(vector2, true, addHeaderText, basescale);
      this.panel.Finalize(vector2);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      bool flag = false;
      offset += this.location;
      if (this.tray != null)
        flag |= this.tray.CheckMouseOver(player, offset);
      return flag | this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateAnimalNotificationPanel(Player player, float DeltaTime, out bool track)
    {
      bool flag1 = false;
      track = false;
      if (this.ConstructionFailed)
        return true;
      if (this.needFixBox)
      {
        this.tray.UpdateMovingTray(player, DeltaTime, this.location);
        this.fixbox.location = this.tray.GetTruePosition();
        if (this.tray.IsOpen)
          flag1 |= this.fixbox.UpdateAnimalNotificationFixBox(player, this.location + this.tray.GetTruePosition(), DeltaTime);
      }
      bool flag2 = flag1 | this.panel.UpdatePanelCloseButton(player, DeltaTime, this.location);
      track = this.infoBox.UpdateAnimalNotificationInfoBox(player, this.location, DeltaTime);
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      return flag2 | track;
    }

    public void SetbreakOutData(int HumanDeaths, int AnimalDeaths, int AnimalsLoose)
    {
      if (this.notificationType != AnimalNotificationType.Breakout)
        throw new Exception("CANNOT CALL HERE");
      this.infoBox.AddOrUpdateStatsLister("Animals loose", AnimalsLoose);
      this.infoBox.AddOrUpdateStatsLister("Animals lost", AnimalDeaths);
      this.infoBox.AddOrUpdateStatsLister("Human casualties", HumanDeaths);
    }

    public void DrawAnimalNotificationPanel(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      if (this.ConstructionFailed)
        return;
      if (this.needFixBox)
      {
        this.tray.DrawMovingTray(offset, spritebatch);
        this.fixbox.DrawAnimalNotificationFixBox(offset, spritebatch);
      }
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.infoBox.DrawAnimalNotificationInfoBox(offset, spritebatch);
      if (!this.needFixBox)
        return;
      this.tray.DrawOpenCloseButton(offset, spritebatch);
    }
  }
}

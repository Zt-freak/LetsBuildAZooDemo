// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.LittleSummaryButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_WorldMap.Maps.MapLocations.MapMarkers;

namespace TinyZoo.Z_SummaryPopUps.People
{
  internal class LittleSummaryButton : GameObject
  {
    private GameObject mouseover;
    private bool MOUSEOVER;
    private StringInBox objname;
    public LittleSummaryButtonType Buttontype;
    private RedLight redLight;
    private float BaseScale;
    private bool IsToggle;

    public bool isDisabled { get; private set; }

    public LittleSummaryButton(
      LittleSummaryButtonType _Buttontype,
      bool AllowTextOnMouseOver = false,
      float _BaseScale = 1f)
    {
      this.BaseScale = _BaseScale;
      this.Buttontype = _Buttontype;
      this.mouseover = new GameObject();
      string Text = "TEMP";
      string empty = string.Empty;
      switch (this.Buttontype)
      {
        case LittleSummaryButtonType.Move:
          Text = "Move";
          this.DrawRect = new Rectangle(627, 416, 28, 28);
          this.mouseover.DrawRect = new Rectangle(627, 445, 28, 28);
          break;
        case LittleSummaryButtonType.Quarantine:
          Text = "Quarantine";
          this.DrawRect = new Rectangle(910, 64, 28, 28);
          this.mouseover.DrawRect = new Rectangle(910, 93, 28, 28);
          break;
        case LittleSummaryButtonType.Remove:
          Text = "Euthanize";
          this.DrawRect = new Rectangle(939, 64, 28, 28);
          this.mouseover.DrawRect = new Rectangle(939, 93, 28, 28);
          break;
        case LittleSummaryButtonType.AssignMoreStaff:
          this.DrawRect = new Rectangle(985, 110, 28, 28);
          this.mouseover.DrawRect = new Rectangle(985, 139, 28, 28);
          break;
        case LittleSummaryButtonType.CallSomeone:
          this.DrawRect = new Rectangle(985, 372, 28, 28);
          this.mouseover.DrawRect = new Rectangle(985, 401, 28, 28);
          break;
        case LittleSummaryButtonType.BlueInfoCircle:
          this.DrawRect = new Rectangle(943, 354, 17, 17);
          this.mouseover.DrawRect = new Rectangle(970, 484, 17, 17);
          break;
        case LittleSummaryButtonType.ExpandCollapse:
          this.IsToggle = true;
          this.DoToggle(true);
          break;
        case LittleSummaryButtonType.Restock:
          this.DrawRect = new Rectangle(855, 434, 28, 28);
          this.mouseover.DrawRect = new Rectangle(915, 484, 28, 28);
          break;
        case LittleSummaryButtonType.Manage:
          this.DrawRect = new Rectangle(917, 604, 28, 28);
          this.mouseover.DrawRect = new Rectangle(917, 633, 28, 28);
          break;
        case LittleSummaryButtonType.Locate:
          this.DrawRect = new Rectangle(885, 612, 28, 28);
          this.mouseover.DrawRect = new Rectangle(885, 641, 28, 28);
          break;
        case LittleSummaryButtonType.MoveGate:
          Text = "Move Gate";
          this.DrawRect = new Rectangle(269, 155, 28, 28);
          this.mouseover.DrawRect = new Rectangle(269, 184, 28, 28);
          break;
        case LittleSummaryButtonType.TrashBin:
          this.DrawRect = new Rectangle(855, 463, 28, 28);
          this.mouseover.DrawRect = new Rectangle(915, 513, 28, 28);
          break;
        case LittleSummaryButtonType.Hire:
          this.DrawRect = new Rectangle(466, 413, 28, 28);
          this.mouseover.DrawRect = new Rectangle(466, 442, 28, 28);
          break;
        case LittleSummaryButtonType.FinishQuest:
          this.DrawRect = new Rectangle(324, 735, 28, 28);
          this.mouseover.DrawRect = new Rectangle(324, 764, 28, 28);
          break;
        case LittleSummaryButtonType.ChangeDiet:
          this.DrawRect = new Rectangle(881, 62, 28, 28);
          this.mouseover.DrawRect = new Rectangle(881, 91, 28, 28);
          break;
        case LittleSummaryButtonType.RedCloseCircle:
          this.DrawRect = new Rectangle(172, 28, 17, 17);
          this.mouseover.DrawRect = new Rectangle(63, 733, 17, 17);
          break;
        case LittleSummaryButtonType.BuildingRequired:
          this.DrawRect = new Rectangle(324, 793, 28, 28);
          this.mouseover.DrawRect = new Rectangle(324, 822, 28, 28);
          break;
        case LittleSummaryButtonType.Action_Good:
          this.DrawRect = new Rectangle(546, 967, 28, 28);
          this.mouseover.DrawRect = new Rectangle(546, 996, 28, 28);
          break;
        case LittleSummaryButtonType.Action_Neutral:
          this.DrawRect = new Rectangle(575, 967, 28, 28);
          this.mouseover.DrawRect = new Rectangle(575, 996, 28, 28);
          break;
        case LittleSummaryButtonType.Action_Evil:
          this.DrawRect = new Rectangle(604, 967, 28, 28);
          this.mouseover.DrawRect = new Rectangle(604, 996, 28, 28);
          break;
        case LittleSummaryButtonType.MoralityEvil:
          this.DrawRect = new Rectangle(949, 959, 64, 28);
          this.mouseover.DrawRect = new Rectangle(949, 988, 64, 28);
          break;
        case LittleSummaryButtonType.MoralityNeutral:
          this.DrawRect = new Rectangle(821, 959, 63, 28);
          this.mouseover.DrawRect = new Rectangle(821, 988, 63, 28);
          break;
        case LittleSummaryButtonType.MoralityGood:
          this.DrawRect = new Rectangle(885, 959, 63, 28);
          this.mouseover.DrawRect = new Rectangle(885, 988, 63, 28);
          break;
        case LittleSummaryButtonType.ReleaseToWild:
          this.DrawRect = new Rectangle(572, 920, 28, 28);
          this.mouseover.DrawRect = new Rectangle(601, 920, 28, 28);
          Text = "Release To Wild";
          break;
        case LittleSummaryButtonType.ReturnToPen:
          this.DrawRect = new Rectangle(514, 920, 28, 28);
          this.mouseover.DrawRect = new Rectangle(543, 920, 28, 28);
          Text = "Return To Pen";
          break;
        case LittleSummaryButtonType.Processing_Queue:
          this.DrawRect = new Rectangle(527, 862, 28, 28);
          this.mouseover.DrawRect = new Rectangle(527, 891, 28, 28);
          break;
        case LittleSummaryButtonType.Processing_Animals:
          this.DrawRect = new Rectangle(556, 862, 28, 28);
          this.mouseover.DrawRect = new Rectangle(556, 891, 28, 28);
          break;
        case LittleSummaryButtonType.Processing_Meat:
          this.DrawRect = new Rectangle(585, 862, 28, 28);
          this.mouseover.DrawRect = new Rectangle(585, 891, 28, 28);
          break;
        case LittleSummaryButtonType.Processing_StoreRoom:
          this.DrawRect = new Rectangle(614, 862, 28, 28);
          this.mouseover.DrawRect = new Rectangle(614, 891, 28, 28);
          break;
        case LittleSummaryButtonType.Processing_Crops:
          this.DrawRect = new Rectangle(468, 862, 28, 28);
          this.mouseover.DrawRect = new Rectangle(468, 891, 28, 28);
          break;
        case LittleSummaryButtonType.Processing_Veggies:
          this.DrawRect = new Rectangle(498, 862, 29, 28);
          this.mouseover.DrawRect = new Rectangle(498, 891, 29, 28);
          break;
        case LittleSummaryButtonType.Camera:
          this.DrawRect = new Rectangle(64, 409, 24, 26);
          this.mouseover.DrawRect = new Rectangle(64, 436, 24, 26);
          break;
        case LittleSummaryButtonType.DestroyBuilding:
          this.DrawRect = new Rectangle(855, 463, 28, 28);
          this.mouseover.DrawRect = new Rectangle(915, 513, 28, 28);
          Text = "Delete Mode";
          break;
      }
      this.SetDrawOriginToCentre();
      this.scale = this.BaseScale;
      this.mouseover.SetDrawOriginToCentre();
      this.mouseover.scale = this.BaseScale;
      if (!AllowTextOnMouseOver)
        return;
      this.objname = new StringInBox(Text, RenderMath.GetPixelSizeBestMatch(1f), 130f, true);
      this.objname.SetAsButtonFrame(BTNColour.Blue);
      this.objname.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public void DoToggle(bool StateIsOn)
    {
      if (this.Buttontype != LittleSummaryButtonType.ExpandCollapse)
        throw new Exception("CANNOT TOGGLE");
      if (StateIsOn)
      {
        this.DrawRect = new Rectangle(239, 445, 17, 17);
        this.mouseover.DrawRect = new Rectangle(275, 426, 17, 17);
      }
      else
      {
        this.DrawRect = new Rectangle(275, 444, 17, 17);
        this.mouseover.DrawRect = new Rectangle(257, 444, 17, 17);
      }
    }

    public bool UpdateLittleSummaryButton(float DeltaTime, Player player, Vector2 Offset)
    {
      if (this.isDisabled)
        return false;
      this.MOUSEOVER = MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      if (!this.MOUSEOVER || (double) player.player.touchinput.ReleaseTapArray[0].X < 0.0 && !player.inputmap.PressedThisFrame[0])
        return false;
      player.inputmap.PressedThisFrame[0] = false;
      player.player.touchinput.ReleaseTapArray[0].X = -1000f;
      return true;
    }

    public bool CheckMouseOver(Vector2 Offset, Player player) => MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void SetDisabled(bool _isDisabled)
    {
      this.isDisabled = _isDisabled;
      if (this.isDisabled)
      {
        this.SetAllColours(Color.LightGray.ToVector3());
        this.SetAlpha(0.5f);
      }
      else
      {
        this.SetAllColours(Color.White.ToVector3());
        this.SetAlpha(1f);
      }
    }

    public void AddRedLight()
    {
      this.redLight = new RedLight(true, BaseScale: (this.BaseScale * 0.5f));
      RedLight redLight = this.redLight;
      redLight.vLocation = redLight.vLocation - this.GetSize() * 0.5f;
    }

    public void RemoveRedLight() => this.redLight = (RedLight) null;

    public void SetUnavailable()
    {
    }

    public void DrawLittleSummaryButton(Vector2 Offset, SpriteBatch spriteBatch)
    {
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset);
      Offset += this.vLocation;
      if (this.MOUSEOVER)
      {
        this.mouseover.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset);
        if (this.objname != null)
          this.objname.DrawStringInBox(Offset + new Vector2(0.0f, (float) ((double) this.DrawRect.Height * (double) this.scale * -0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y - 10.0)), spriteBatch);
      }
      if (this.redLight != null)
      {
        if (this.MOUSEOVER)
          Offset.Y += 2f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.redLight.DrawRedLight(spriteBatch, Offset);
      }
      this.MOUSEOVER = false;
    }

    public void DrawLittleSummaryButton(Vector2 Offset) => this.DrawLittleSummaryButton(Offset, AssetContainer.pointspritebatchTop05);
  }
}

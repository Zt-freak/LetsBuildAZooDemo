// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.DayOfWeek.TopBarDayOfWeek
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;

namespace TinyZoo.Z_HUD.TopBar.Elements.DayOfWeek
{
  internal class TopBarDayOfWeek
  {
    public Vector2 location;
    private TopBarHeaderBase headerBase;
    private LerpHandler_Float lerper;
    private ZGenericText dayText;
    private ZGenericText dateText;

    public TopBarDayOfWeek(float BaseScale, float FrameHeight, bool IsForTime_NotDayOfWeek = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float BarWidth = uiScaleHelper.ScaleX(182.5f);
      if (IsForTime_NotDayOfWeek)
        BarWidth = uiScaleHelper.ScaleX(165f);
      this.headerBase = new TopBarHeaderBase(BaseScale, FrameHeight, BarWidth, true);
      Vector2 vector2 = -this.headerBase.GetSize() * 0.5f;
      this.dayText = new ZGenericText("X", BaseScale, false, _UseOnePointFiveFont: true);
      this.dateText = new ZGenericText("X", BaseScale, false, true);
      Vector2 size = this.dayText.GetSize();
      this.dateText.GetSize();
      this.dayText.vLocation = new Vector2(defaultXbuffer + vector2.X, (float) (-(double) size.Y * 0.5));
      this.dateText.vLocation = new Vector2(-vector2.X - defaultXbuffer, 0.0f);
      this.dateText.textToWrite = "";
      this.lerper = new LerpHandler_Float();
      if (IsForTime_NotDayOfWeek)
        this.headerBase.SetPopOutFrame(TopBarPopOutType.Time);
      else
        this.headerBase.SetPopOutFrame(TopBarPopOutType.Day);
    }

    public void LerpIn() => this.lerper.SetLerp(false, -1f, 0.0f, 3f, true);

    public void LerpOff() => this.lerper.SetLerp(false, -1f, -1f, 3f, true);

    public Vector2 GetSize() => this.headerBase.GetSize();

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.headerBase.CheckMouseOver(player, offset);
    }

    public void SetDayText(string newText) => this.dayText.textToWrite = newText;

    public void SetDateOrExtraText(string newText) => this.dateText.textToWrite = newText;

    public void UpdateTopBarDayOfWeek(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.headerBase.UpdateTopBarHeaderBase(player, DeltaTime, offset);
    }

    public void PreDrawTopBarDayOfWeek(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerBase.PreDrawTopBarHeaderBase(offset, spriteBatch);
    }

    public void DrawTopBarDayOfWeek(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerBase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.dayText.DrawZGenericText(offset, spriteBatch);
      this.dateText.DrawZGenericText(offset, spriteBatch);
      this.headerBase.PostDrawTopBarHeaderBase(offset, spriteBatch);
    }
  }
}

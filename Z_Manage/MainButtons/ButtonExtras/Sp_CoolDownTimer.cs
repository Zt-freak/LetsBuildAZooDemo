// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.ButtonExtras.Sp_CoolDownTimer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.MainButtons.ButtonExtras
{
  internal class Sp_CoolDownTimer : GameObject
  {
    private DateTime EndTime;
    private string DRAWTHIS;
    private string SecomdaryString;
    private bool IgnoreThisTime;

    public Sp_CoolDownTimer(DateTime _EndTime, string _SecomdaryString, bool _IgnoreThisTime = false)
    {
      this.IgnoreThisTime = _IgnoreThisTime;
      this.SecomdaryString = _SecomdaryString;
      this.EndTime = _EndTime;
      Vector3 SecondaryColour;
      StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour);
      this.SetAllColours(SecondaryColour);
      if (!this.IgnoreThisTime)
        return;
      this.DRAWTHIS = this.SecomdaryString;
    }

    public void UpdateSp_CoolDownTimer(Player player)
    {
      if (this.IgnoreThisTime)
        return;
      this.DRAWTHIS = TimeUtils.GetDifferenceInTimeAsString(this.EndTime, player.Stats.datetimemanager.GetDateTimeNow(out bool _));
    }

    public void DrawSp_CoolDownTimer(Vector2 Offset) => TextFunctions.DrawJustifiedText(this.DRAWTHIS, 3f * Sengine.ScreenRationReductionMultiplier.Y, this.vLocation + Offset + new Vector2(60f, -40f), this.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
  }
}

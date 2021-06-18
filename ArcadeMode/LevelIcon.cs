// Decompiled with JetBrains decompiler
// Type: TinyZoo.ArcadeMode.LevelIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.CollectionScreen;

namespace TinyZoo.ArcadeMode
{
  internal class LevelIcon : GameObject
  {
    public BaseFrame baseframe;
    public LerpHandler_Float existencelerper;
    private float LerpValue;
    private int LevelNumber;
    private bool isNew;

    public LevelIcon(int _LevelNumber, int UnlockLevel, bool _isNew)
    {
      this.LevelNumber = _LevelNumber;
      this.baseframe = new BaseFrame();
      this.baseframe.scale = 3f;
      this.isNew = _isNew;
      if (UnlockLevel < 0)
      {
        this.SetAllColours(0.0f, 0.0f, 0.0f);
        this.baseframe.SetAlpha(0.4f);
        this.baseframe.SetAllColours(0.5f, 0.5f, 0.5f);
        this.SetAllColours(0.5f, 0.5f, 0.5f);
        this.SetAlpha(0.3f);
      }
      else
      {
        this.SetAllColours(ColourData.IconOrange);
        if (this.isNew)
        {
          this.baseframe.SetAllColours(1f, 0.7f, 0.5f);
          this.SetAllColours(ColourData.IconYellow);
        }
        if (UnlockLevel == 2)
        {
          this.baseframe.SetAllColours(0.6f, 0.0f, 1f);
          this.baseframe.MouseOverObj.SetAllColours(0.7f, 0.3f, 1f);
        }
      }
      this.scale = 3f;
      this.SetDrawOriginToCentre();
      this.existencelerper = new LerpHandler_Float();
      this.existencelerper.SetLerp(true, 0.0f, 1f, 3f);
    }

    public void SetUpLerper(int LerpIndex)
    {
      this.LerpValue = (float) LerpIndex * 0.05f;
      this.existencelerper.SetDelay(this.LerpValue);
    }

    public void Exit(int HighestLerpvalue)
    {
      this.existencelerper.SetLerp(true, 1f, 0.0f, 3f);
      this.existencelerper.SetDelay(this.LerpValue);
    }

    public bool UpdateLevelIcon(Vector2 Offset, float DeltaTime, Player player)
    {
      this.existencelerper.UpdateLerpHandler(DeltaTime);
      return this.baseframe.UpdateBaseFrame(Offset + this.vLocation, player);
    }

    public void DrawLevelIcon(Vector2 Offset)
    {
      int num = this.baseframe.MouseOver ? 1 : 0;
      this.baseframe.DrawBaseFrame(Offset + this.vLocation, AssetContainer.pointspritebatchTop05, this.existencelerper.Value);
      if (num != 0)
        Offset.Y += 2f;
      TextFunctions.DrawJustifiedText(string.Concat((object) (this.LevelNumber + 1)), 5f, Offset + this.vLocation + new Vector2(6f, 0.0f), this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
      if (!this.isNew)
        return;
      TextFunctions.DrawJustifiedText("NEW", 2f, Offset + this.vLocation + new Vector2(18f, -28f * Sengine.ScreenRatioUpwardsMultiplier.Y), this.GetColour(), this.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
    }
  }
}

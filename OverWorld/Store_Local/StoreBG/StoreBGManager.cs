// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Store_Local.StoreBG.StoreBGManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;

namespace TinyZoo.OverWorld.Store_Local.StoreBG
{
  internal class StoreBGManager
  {
    private List<Strip> strips;
    private GameObject BG;
    private BlackOut blackout;
    private Vector2 BGScale;
    private float ALphaMult;

    public StoreBGManager(bool IsPeach = false, bool IsAutumnal = false, bool IsBlue = false, bool IsYellow = false)
    {
      this.BG = new GameObject();
      this.BG.DrawRect = new Rectangle(1019, 0, 1, 1024);
      this.BGScale = new Vector2(1024f, 0.75f);
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(ColourData.ACPaleGreen);
      if (IsPeach)
        this.blackout.SetAllColours(ColourData.ACPalePeach);
      else if (IsBlue)
        this.blackout.SetAllColours(ColourData.ACPaleBlue);
      else if (IsAutumnal)
        this.blackout.SetAllColours(ColourData.ACPaleAutumn);
      else if (IsYellow)
        this.blackout.SetAllColours(ColourData.ACPaleYellow);
      this.strips = new List<Strip>();
      for (int index1 = 0; index1 < 4; ++index1)
      {
        float num1 = 256f;
        float num2 = (float) TinyZoo.Game1.Rnd.Next(0, 408);
        float x = (float) ((double) index1 * (double) num1 + (double) num1 * 0.5);
        float ScrollSpeed = (float) TinyZoo.Game1.Rnd.Next(15, 30);
        for (int index2 = 0; index2 < 6; ++index2)
        {
          this.strips.Add(new Strip(ScrollSpeed));
          this.strips[this.strips.Count - 1].vLocation = new Vector2(x, num2 + (float) (index2 * 60));
          if (index2 % 2 == 0)
            this.strips[this.strips.Count - 1].vLocation.X -= 50f;
          else
            this.strips[this.strips.Count - 1].vLocation.X += 50f;
          if (IsPeach)
            this.strips[this.strips.Count - 1].SetAllColours(ColourData.ACDarkerPeach);
          else if (IsBlue)
            this.strips[this.strips.Count - 1].SetAllColours(ColourData.ACDarkerBlue);
          else if (IsAutumnal)
            this.strips[this.strips.Count - 1].SetAllColours(ColourData.ACDarkerAutumn);
          else if (IsYellow)
            this.strips[this.strips.Count - 1].SetAllColours(ColourData.ACDarkerYellow);
        }
      }
    }

    public void SetDark() => this.BG.SetAllColours(0.3f, 0.3f, 0.3f);

    public void BlendToNewColour(bool Green, bool Peach, bool Blue = false, bool Autumnal = false, bool Yellow = false)
    {
      float blendTime = 0.2f;
      if (Peach)
      {
        this.blackout.SetColours(true, blendTime, ColourData.ACPalePeach, ColourData.ACPalePeach);
        for (int index = 0; index < this.strips.Count; ++index)
          this.strips[index].SetColours(true, blendTime, ColourData.ACDarkerPeach, ColourData.ACDarkerPeach);
      }
      else if (Green)
      {
        this.blackout.SetColours(true, blendTime, ColourData.ACPaleGreen, ColourData.ACPaleGreen);
        for (int index = 0; index < this.strips.Count; ++index)
          this.strips[index].SetColours(true, blendTime, ColourData.ACDarkerGreen, ColourData.ACDarkerGreen);
      }
      else if (Blue)
      {
        this.blackout.SetColours(true, blendTime, ColourData.ACPaleGreen, ColourData.ACPaleBlue);
        for (int index = 0; index < this.strips.Count; ++index)
          this.strips[index].SetColours(true, blendTime, ColourData.ACDarkerGreen, ColourData.ACDarkerBlue);
      }
      else if (Autumnal)
      {
        this.blackout.SetColours(true, blendTime, ColourData.ACPaleGreen, ColourData.ACPaleAutumn);
        for (int index = 0; index < this.strips.Count; ++index)
          this.strips[index].SetColours(true, blendTime, ColourData.ACDarkerGreen, ColourData.ACDarkerAutumn);
      }
      else
      {
        if (!Yellow)
          return;
        this.blackout.SetColours(true, blendTime, ColourData.ACPaleGreen, ColourData.ACPaleYellow);
        for (int index = 0; index < this.strips.Count; ++index)
          this.strips[index].SetColours(true, 1f, ColourData.ACDarkerGreen, ColourData.ACDarkerYellow);
      }
    }

    public void SetSpecial()
    {
      for (int index = 0; index < 140; ++index)
        this.strips[index].SetSpinning();
    }

    public void UpdateStoreBGManager(float DeltaTime)
    {
      this.blackout.UpdateColours(DeltaTime);
      this.ALphaMult = 1f;
      if (GameFlags.NoStrobe)
      {
        DeltaTime *= 0.2f;
        this.ALphaMult = 0.5f;
      }
      for (int index = 0; index < this.strips.Count; ++index)
        this.strips[index].UpdateStrip(DeltaTime);
    }

    public void SetSpecialRed()
    {
      this.BG.DrawRect.X = 1020;
      this.BG.SetAllColours(1f, 1f, 0.0f);
      for (int index = 0; index < this.strips.Count; ++index)
        this.strips[index].SetAllColours(1f, 0.4f, 0.0f);
    }

    public void DrawStoreBGManager(Vector2 Offset) => this.DrawStoreBGManager(Offset, AssetContainer.pointspritebatch03);

    public void DrawStoreBGManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.blackout.DrawBlackOut(Offset, spritebatch);
      for (int index = 0; index < this.strips.Count; ++index)
        this.strips[index].DrawStrip(Offset, this.ALphaMult, spritebatch);
    }
  }
}

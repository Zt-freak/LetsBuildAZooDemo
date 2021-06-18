// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.ControlHint.ControlHintManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.ControlHint
{
  internal class ControlHintManager
  {
    private List<MouseHintRow> mousehintrows;
    public Vector2 Location;
    private MicroOpenClose Skip;
    private Vector2 totalSize;
    private SplitFrame splitFrame;

    public ControlHintManager(ControllerHintSummary hinttype, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.mousehintrows = new List<MouseHintRow>();
      switch (hinttype)
      {
        case ControllerHintSummary.BaseNavigation:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(1021)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1022)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_MiddleHeld, SEngine.Localization.Localization.GetText(1023)));
          break;
        case ControllerHintSummary.BuildPen:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(863)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1024)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_MiddleHeld, SEngine.Localization.Localization.GetText(1023)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_RightHeld, SEngine.Localization.Localization.GetText(1022), SEngine.Localization.Localization.GetText(1025)));
          break;
        case ControllerHintSummary.BuildStructure:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(1026)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1024), SEngine.Localization.Localization.GetText(1027)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_MiddleHeld, SEngine.Localization.Localization.GetText(1023)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_RightHeld, SEngine.Localization.Localization.GetText(1022), SEngine.Localization.Localization.GetText(1028)));
          break;
        case ControllerHintSummary.UseBuildMenu:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(1029)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1030), SEngine.Localization.Localization.GetText(1031)));
          break;
        case ControllerHintSummary.PlaceGate:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(747)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_Movement4Way, SEngine.Localization.Localization.GetText(1032)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1033), SEngine.Localization.Localization.GetText(1034)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_MiddleHeld, SEngine.Localization.Localization.GetText(1023)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_RightHeld, SEngine.Localization.Localization.GetText(1022)));
          break;
        case ControllerHintSummary.MovePen:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(747)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_Movement4Way, SEngine.Localization.Localization.GetText(1032)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1035)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_MiddleHeld, SEngine.Localization.Localization.GetText(1023)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_RightHeld, SEngine.Localization.Localization.GetText(1022)));
          break;
        case ControllerHintSummary.CellSelect:
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.None, SEngine.Localization.Localization.GetText(1036)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1037), SEngine.Localization.Localization.GetText(1038)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_LeftHeld, SEngine.Localization.Localization.GetText(1022)));
          this.mousehintrows.Add(new MouseHintRow(BaseScale, ControllerButton.Mouse_MiddleHeld, SEngine.Localization.Localization.GetText(1023)));
          break;
      }
      this.Skip = new MicroOpenClose(BaseScale);
      this.Skip.SetAsSkip();
      this.totalSize = Vector2.Zero;
      for (int index = 0; index < this.mousehintrows.Count; ++index)
      {
        this.mousehintrows[index].Location.Y = this.totalSize.Y;
        this.totalSize.Y += this.mousehintrows[index].GetSize().Y;
      }
      this.totalSize.X = this.mousehintrows[0].GetSize().X;
      this.splitFrame = new SplitFrame(this.totalSize, ColourData.Z_CreamFADED, ColourData.Z_Cream, BaseScale, this.mousehintrows[0].GetSize().Y / this.totalSize.Y);
      this.Skip.vLocation = this.totalSize;
      this.Skip.vLocation.X -= this.Skip.GetSize().X * 0.5f;
      this.Skip.vLocation.Y += (float) ((double) this.Skip.GetSize().Y * 0.5 + (double) uiScaleHelper.DefaultBuffer.Y * 0.5);
      Vector2 vector2 = -this.totalSize * 0.5f;
      for (int index = 0; index < this.mousehintrows.Count; ++index)
        this.mousehintrows[index].Location += vector2;
      MicroOpenClose skip = this.Skip;
      skip.vLocation = skip.vLocation + vector2;
    }

    public Vector2 GetOffset() => this.mousehintrows[0].Location + this.Location;

    public Vector2 GetHeaderSize() => this.mousehintrows[0].GetSize();

    public Vector2 GetSize() => this.totalSize;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return (0 | (this.splitFrame.CheckMouseOver(player, offset) ? 1 : 0) | (this.Skip.CheckMouseOver(player, offset) ? 1 : 0)) != 0;
    }

    public bool UpdateControlHintManager(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      return this.Skip.UpdateMicroOpenClose(player, DeltaTime, Offset);
    }

    public void DrawControlHintManager(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.splitFrame.DrawSplitFrame(Offset, spriteBatch);
      for (int index = 0; index < this.mousehintrows.Count; ++index)
        this.mousehintrows[index].DrawMouseHintRow(Offset, spriteBatch);
      this.Skip.DrawMicroOpenClose(Offset, spriteBatch);
    }
  }
}

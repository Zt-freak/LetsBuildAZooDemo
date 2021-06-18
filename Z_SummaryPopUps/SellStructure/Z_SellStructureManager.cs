// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.SellStructure.Z_SellStructureManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_SummaryPopUps.SellStructure
{
  internal class Z_SellStructureManager
  {
    private GameObjectNineSlice Frame;
    public Vector2 Vscale;
    private TextButton LeftBtn;
    private TextButton RightBtn;
    private Vector2 Location;
    private LerpHandler_Float lerper;
    private SimpleTextHandler text;
    private CoinAndString coinandstring;
    private BlackOut blackout;
    private LittleSummaryButton move;
    private LittleSummaryButton MoveGate;
    internal static bool IsMove;
    private bool InstantDelete;
    private bool InstantMove;
    private bool IsCellBlock;

    public Z_SellStructureManager(bool _IsCellBlock, Player player, float MasterMult = 1f)
    {
      this.IsCellBlock = _IsCellBlock;
      Z_SellStructureManager.IsMove = false;
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.2f, 0.0f, 0.5f);
      this.Vscale = new Vector2(350f, 130f) * MasterMult;
      this.Frame = new GameObjectNineSlice(new Rectangle(939, 416, 21, 21), 7);
      this.Frame.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.LeftBtn = new TextButton("CANCEL", 55f, OverAllMultiplier: 0.6666f);
      this.RightBtn = new TextButton("DEMOLISH", 55f, OverAllMultiplier: 0.6666f);
      this.LeftBtn.SetButtonColour(BTNColour.Green);
      this.RightBtn.SetButtonColour(BTNColour.Blue);
      this.LeftBtn.vLocation.X = -60f;
      this.LeftBtn.vLocation.Y = 37f;
      this.RightBtn.vLocation.X = 60f;
      this.RightBtn.vLocation.Y = 37f;
      this.LeftBtn.stringinabox.Frame.scale *= 0.5f;
      this.RightBtn.stringinabox.Frame.scale *= 0.5f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Location = new Vector2(768f, 300f);
      this.text = new SimpleTextHandler("Demolish this and gain:", true, 0.4f, RenderMath.GetPixelSizeBestMatch(1f));
      this.text.AutoCompleteParagraph();
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.text.Location.Y = -20f;
      this.coinandstring = new CoinAndString(100);
      this.move = new LittleSummaryButton(LittleSummaryButtonType.Move, true);
      this.move.vLocation = this.RightBtn.vLocation;
      this.move.vLocation.X += 80f;
      this.InstantMove = player.inputmap.PressedThisFrame[23];
      if (this.IsCellBlock)
      {
        this.MoveGate = new LittleSummaryButton(LittleSummaryButtonType.MoveGate, true);
        this.MoveGate.vLocation = this.RightBtn.vLocation;
        this.MoveGate.vLocation.X += 90f;
        this.RightBtn.vLocation.X -= 60f;
        this.LeftBtn.vLocation.X -= 60f;
        this.move.vLocation.X -= 40f;
      }
      this.InstantDelete = player.inputmap.PressedThisFrame[22];
      if (!this.InstantMove && !this.InstantDelete)
        return;
      this.lerper.SetLerp(true, 0.0f, 0.0f, 3f);
    }

    public bool UpdateZ_SellStructureManager(
      Vector2 Offset,
      float DeltaTime,
      Player player,
      ref int LastButtonPressed)
    {
      this.blackout.UpdateColours(DeltaTime);
      Offset += this.Location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset.X += this.lerper.Value * 1024f;
      if ((double) this.lerper.Value == 0.0)
      {
        if (this.RightBtn.UpdateTextButton(player, Offset, DeltaTime) || this.InstantDelete)
        {
          LastButtonPressed = 1;
          this.lerper.SetLerp(false, 1f, 1f, 3f, true);
          this.blackout.SetAlpha(true, 0.3f, 1f, 0.0f);
        }
        if (this.LeftBtn.UpdateTextButton(player, Offset, DeltaTime))
        {
          LastButtonPressed = 2;
          this.lerper.SetLerp(false, 1f, 1f, 3f, true);
          this.blackout.SetAlpha(true, 0.3f, 1f, 0.0f);
        }
        if (this.move != null && (this.move.UpdateLittleSummaryButton(DeltaTime, player, Offset) || this.InstantMove))
        {
          if (this.IsCellBlock)
            Z_GameFlags.ForceToNewScreen = ForceToNewScreen.MovePen;
          else
            Z_SellStructureManager.IsMove = true;
          return true;
        }
        if (this.MoveGate != null && this.MoveGate.UpdateLittleSummaryButton(DeltaTime, player, Offset))
        {
          Z_GameFlags.ForceToNewScreen = ForceToNewScreen.MoveGate;
          return true;
        }
      }
      if (this.InstantMove || this.InstantDelete)
        return true;
      return (double) this.lerper.Value == 1.0 && (double) this.lerper.TargetValue == 1.0;
    }

    public void DrawZ_SellStructureManager(Vector2 Offset)
    {
      Offset += this.Location;
      Offset.X += this.lerper.Value * 1024f;
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.Vscale);
      TextFunctions.DrawJustifiedText("DELETE STRUCTURE CONFIRMATION", RenderMath.GetPixelSizeBestMatch(1f) * 0.5f, Offset + new Vector2(0.0f, -40f), new Color(ColourData.Z_Cream), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.coinandstring.DrawCoinAndStringSmall(AssetContainer.pointspritebatchTop05, Offset);
      this.RightBtn.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.LeftBtn.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      if (this.move != null)
        this.move.DrawLittleSummaryButton(Offset);
      if (this.MoveGate == null)
        return;
      this.MoveGate.DrawLittleSummaryButton(Offset);
    }
  }
}

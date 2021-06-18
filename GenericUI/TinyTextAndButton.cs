// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.TinyTextAndButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.GenericUI
{
  internal class TinyTextAndButton
  {
    private TRC_ButtonDisplay ControllerButton;
    private SEngine.ControllerButton controllerbutton;
    private string text;
    private GameObject tt;
    private GameObjectNineSlice niner;

    public TinyTextAndButton(
      SEngine.ControllerButton controllerbutton,
      string ttt,
      List<ControllerAnim> anims = null)
    {
      this.text = ttt;
      this.ControllerButton = new TRC_ButtonDisplay(4f * Sengine.ScreenRationReductionMultiplier.Y);
      if (anims != null)
        this.ControllerButton.SetUpAnimation(anims);
      else
        this.ControllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, controllerbutton);
      this.tt = new GameObject();
      this.tt.SetAllColours(ColourData.FernLemon);
      this.niner = new GameObjectNineSlice(new Rectangle(895, 372, 21, 21), 7);
      this.niner.scale = 2f;
    }

    public void UpdateTinyTextAndButton(float DeltaTime) => this.ControllerButton.UpdateTRC_ButtonDisplay(DeltaTime);

    public void DrawTinyTextAndButton(Vector2 Offset)
    {
      if (this.ControllerButton == null || !TinyZoo.GameFlags.IsUsingController)
        return;
      this.ControllerButton.scale = 4f * Sengine.ScreenRationReductionMultiplier.Y;
      this.niner.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, new Vector2(250f, 90f));
      TextFunctions.DrawJustifiedText(this.text, 3f, Offset + new Vector2(0.0f, -20f), this.tt.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, Offset + new Vector2(0.0f, 18f));
    }
  }
}

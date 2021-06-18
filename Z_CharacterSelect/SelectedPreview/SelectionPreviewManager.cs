// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_CharacterSelect.SelectedPreview.SelectionPreviewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using SEngine.Objects;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_CharacterSelect.SelectedPreview
{
  internal class SelectionPreviewManager
  {
    private BigBrownPanel bigbrownpanel;
    private CustomerFrame frame;
    private SimpleTextHandler text;
    private Vector2 Location;
    private float BaseScale;
    private GameObject obj;
    private TextButton Confirm;

    public SelectionPreviewManager(float _BaseScale, float Width)
    {
      this.BaseScale = _BaseScale;
      this.obj = new GameObject();
      this.obj.scale = _BaseScale;
      this.obj.SetAllColours(ColourData.Z_Cream);
      this.obj.vLocation.Y = this.BaseScale * 7f;
      Keyboard_String.CurrentString = "";
      this.bigbrownpanel = new BigBrownPanel(Vector2.Zero, addHeaderText: SEngine.Localization.Localization.GetText(755), _BaseScale: this.BaseScale);
      this.text = new SimpleTextHandler(SEngine.Localization.Localization.GetText(756), 400f * this.BaseScale, true, this.BaseScale * 1.5f, true);
      this.text.Location.Y -= this.BaseScale * 15f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.text.SetAllColours(ColourData.Z_Cream);
      this.frame = new CustomerFrame(new Vector2(Width, 100f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y), BaseScale: this.BaseScale);
      this.bigbrownpanel.Finalize(this.frame.VSCale);
      this.Location = new Vector2(512f, 550f);
      this.Confirm = new TextButton(this.BaseScale, SEngine.Localization.Localization.GetText(60), 60f * this.BaseScale);
      this.Confirm.UseButtonPressed = ButtonPressed.Enter_PresetConfirm;
      this.Confirm.vLocation = new Vector2(this.frame.VSCale.X * 0.3f, this.frame.VSCale.Y * 0.35f);
      this.Confirm.AddControllerButton(ControllerButton.XboxA);
      if (!GameFlags.IsUsingController)
        return;
      Keyboard_String.CurrentString = "MyZoo";
    }

    public void UpdateSelectionPreviewManager(float DeltaTime, Player player)
    {
      this.text.UpdateSimpleTextHandler(DeltaTime);
      if (GameFlags.IsUsingController && Keyboard_String.CurrentString.Length == 0)
        Keyboard_String.CurrentString = "MyZoo";
      string currentString1 = Keyboard_String.CurrentString;
      Keyboard_String.UpdateKeyboard_String(true);
      int length = Keyboard_String.CurrentString.Length;
      string currentString2 = Keyboard_String.CurrentString;
      if (currentString1 != currentString2)
      {
        string str = "";
        for (int index = 0; index < Keyboard_String.CurrentString.Length; ++index)
        {
          if (index < 12)
            str = Keyboard_String.CurrentString[index] != ' ' ? str + Keyboard_String.CurrentString[index].ToString() : str + "_";
        }
        Keyboard_String.CurrentString = str;
      }
      if (Keyboard_String.CurrentString.Length <= 0 || !this.Confirm.UpdateTextButton(player, this.Location, DeltaTime))
        return;
      TinyZoo.Game1.screenfade.BeginFade(true);
      TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
    }

    public void DrawSelectionPreviewManager()
    {
      this.bigbrownpanel.DrawBigBrownPanel(this.Location, AssetContainer.pointspritebatch01);
      this.frame.DrawCustomerFrame(this.Location, AssetContainer.pointspritebatch01);
      this.text.DrawSimpleTextHandler(this.Location, 1f, AssetContainer.pointspritebatch01);
      string str = "";
      if ((double) FlashingAlpha.Medium.fAlpha > 0.5 && Keyboard_String.CurrentString.Length < 12)
        str = "_";
      this.obj.vLocation.Y = this.BaseScale * 5f;
      TextFunctions.DrawTextWithDropShadow(Keyboard_String.CurrentString + str, this.obj.scale * 2f, this.obj.vLocation + this.Location + new Vector2((float) (-60.0 * (double) this.obj.scale * 2.0), 0.0f), this.obj.GetColour(), this.obj.fAlpha, AssetContainer.SinglePixelFontX1AndHalf, AssetContainer.pointspritebatch01, false);
      if (Keyboard_String.CurrentString.Length <= 0)
        return;
      this.Confirm.DrawTextButton(this.Location);
    }
  }
}

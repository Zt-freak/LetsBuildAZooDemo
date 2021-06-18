// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Transfer.TransferScreen.TransferConfirmPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.OverWorld.Transfer.TransferScreen
{
  internal class TransferConfirmPanel
  {
    private GenericBox uxframe;
    private TextButton button;
    private SimpleTextHandler simpletext;
    private bool ButtonEneabled;

    public TransferConfirmPanel()
    {
      this.uxframe = new GenericBox(new Vector2(1024f, 150f));
      this.uxframe.Location = new Vector2(512f, 693f);
      this.button = new TextButton(SEngine.Localization.Localization.GetText(936));
      this.button.AddControllerButton(ControllerButton.XboxA);
      this.button.vLocation.X = 400f;
    }

    public void ForceText(string TextToWrite, bool _ButtonEneabled)
    {
      this.ButtonEneabled = _ButtonEneabled;
      this.simpletext = new SimpleTextHandler(TextToWrite, false, 0.8f, 3f, false, false);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.Location = new Vector2(-450f, -40f);
    }

    public bool UpdateTransferConfirmPanel(Player player, Vector2 Offset, float DeltaTime) => this.ButtonEneabled && this.button.UpdateTextButton(player, Offset + this.uxframe.Location, DeltaTime);

    public void DrawTransferConfirmPanel(Vector2 Offset)
    {
      this.uxframe.DrawGenericBox(Offset);
      if (this.ButtonEneabled)
        this.button.DrawTextButton(Offset + this.uxframe.Location, 1f, AssetContainer.pointspritebatchTop05);
      if (this.simpletext == null)
        return;
      this.simpletext.DrawSimpleTextHandler(Offset + this.uxframe.Location, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

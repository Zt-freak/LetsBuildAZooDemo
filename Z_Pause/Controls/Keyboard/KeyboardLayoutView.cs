// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Pause.Controls.Keyboard.KeyboardLayoutView
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Pause.Controls.Keyboard
{
  internal class KeyboardLayoutView
  {
    public Vector2 location;
    private List<DescriptionString> descandkey;
    private Vector2 size;

    public KeyboardLayoutView(Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.descandkey = new List<DescriptionString>();
      int val1 = 7;
      Vector2 vector2 = uiScaleHelper.ScaleVector2(new Vector2(385f, 50f) * 0.5f);
      for (int index = 0; index < player.Stats.userkeybindings.KeyUsed.Length; ++index)
      {
        DescriptionString descriptionString = new DescriptionString(player.Stats.userkeybindings.KeyUsed[index], KeyBindingsManager.GetKeyboardActionsToString((KeyboardActions) index), BaseScale, vector2.X);
        descriptionString.vLocation.X = (vector2.X + defaultBuffer.X) * (float) (index / val1);
        descriptionString.vLocation.Y = (vector2.Y + defaultBuffer.Y) * (float) (index % val1);
        descriptionString.vLocation.Y += vector2.Y * 0.5f;
        this.descandkey.Add(descriptionString);
      }
      int num1 = (int) Math.Ceiling((double) player.Stats.userkeybindings.KeyUsed.Length / (double) val1);
      this.size.X = (float) ((double) num1 * (double) vector2.X + (double) (num1 - 1) * (double) defaultBuffer.X);
      int num2 = Math.Min(val1, player.Stats.userkeybindings.KeyUsed.Length);
      this.size.Y = (float) ((double) num2 * (double) vector2.Y + (double) (num2 - 1) * (double) defaultBuffer.Y);
    }

    public Vector2 GetSize() => this.size;

    public void UpdateKeyboardLayoutViewManager(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.location;
      for (int index = 0; index < this.descandkey.Count; ++index)
        this.descandkey[index].UpdateDescriptionString();
    }

    public void DrawKeyboardLayoutViewManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.location;
      for (int index = 0; index < this.descandkey.Count; ++index)
        this.descandkey[index].DrawDescriptionString(Offset, spritebatch);
    }
  }
}

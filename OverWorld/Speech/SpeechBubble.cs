// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Speech.SpeechBubble
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.Speech
{
  internal class SpeechBubble
  {
    private LerpHandler_Float lerper;
    private GameObjectNineSlice Frame;
    private GameObject Tail;
    private SimpleTextHandler simpletext;
    private Vector2 VSCALE;
    private bool DrawFromTail;
    private Vector2 TailOffset;

    public SpeechBubble(string SayThis, float TargetWidthAsPercent, bool _DrawFromTail = true)
    {
      this.DrawFromTail = _DrawFromTail;
      float num1 = Z_GameFlags.GetBaseScaleForUI();
      if (PlayerStats.language == Language.Japanese)
        num1 = 4f;
      float num2 = num1;
      this.lerper = new LerpHandler_Float();
      this.Frame = new GameObjectNineSlice(new Rectangle(168, 235, 12, 12), 4);
      this.Frame.scale = num2;
      this.simpletext = new SimpleTextHandler(SayThis, false, TargetWidthAsPercent, num2, false, false);
      this.VSCALE = this.simpletext.paragraph.GetSize(true);
      this.simpletext.Location.X = this.VSCALE.X * -0.5f;
      this.simpletext.Location.Y = this.VSCALE.Y * -0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.simpletext.Location += new Vector2(num2, num2 * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.VSCALE += new Vector2(num2, num2) * 5f;
      this.Tail = new GameObject();
      this.Tail.DrawRect = new Rectangle(190, 235, 15, 7);
      this.Tail.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      this.Tail.DrawOrigin.X -= 8f;
      this.Tail.DrawOrigin.Y = 2f;
      this.Tail.scale = num2;
      if (!this.DrawFromTail)
        return;
      this.TailOffset.Y = 5f * num2 * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.TailOffset.X = this.Tail.DrawOrigin.X * -num2;
    }

    public void SetAllColours(Vector3 CLY)
    {
      this.Tail.SetAllColours(CLY);
      this.Frame.SetAllColours(CLY);
    }

    public bool AutoCompleteParagraph()
    {
      if (this.simpletext.paragraph.ParagraphIsComplete)
        return true;
      this.simpletext.AutoCompleteParagraph();
      return false;
    }

    public void UpdateSpeechBubble(float DeltaTime) => this.simpletext.UpdateSimpleTextHandler(DeltaTime);

    public void DrawSpeechBubble(SpriteBatch spritebatch, Vector2 Offset)
    {
      if (this.DrawFromTail)
      {
        Offset.X += this.VSCALE.X * 0.5f;
        Offset.Y -= this.VSCALE.Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        Offset.Y -= this.TailOffset.Y;
        Offset.X -= this.TailOffset.X;
      }
      this.Frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCALE * Sengine.ScreenRatioUpwardsMultiplier);
      this.Tail.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + new Vector2(this.VSCALE.X * -0.5f, this.VSCALE.Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.simpletext.paragraph.linemaker.SetAllColours(0.0f, 0.0f, 0.0f);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Speech.PersonBubble
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.Speech;

namespace TinyZoo.Z_OverWorld.Speech
{
  internal class PersonBubble
  {
    private SpeechBubble speechbubble;
    private Vector2 Loc;
    private WalkingPerson Speaker;
    public bool Active;
    private float Timer;

    public void Activate(WalkingPerson person, string SayThis)
    {
      this.speechbubble = new SpeechBubble(SayThis, 0.25f);
      this.Speaker = person;
      this.Active = true;
      this.Timer = 8f;
      this.speechbubble.SetAllColours(new Vector3(1f, 1f, 1f));
    }

    public void UpdatePersonBubble(float DeltaTime)
    {
      if (!this.Active)
        return;
      this.Timer -= DeltaTime;
      if ((double) this.Timer < 0.0)
        this.Active = false;
      this.speechbubble.UpdateSpeechBubble(DeltaTime);
    }

    public void DrawPersonBubble()
    {
      if (!this.Active || !this.Speaker.IsAtive)
        return;
      this.Loc = RenderMath.TranslateWorldSpaceToScreenSpace(this.Speaker.vLocation);
      this.Loc.Y -= (float) (this.Speaker.DrawRect.Height + 1) * this.Speaker.scale * Sengine.WorldOriginandScale.Z;
      this.speechbubble.DrawSpeechBubble(AssetContainer.pointspritebatch01, this.Loc);
    }
  }
}

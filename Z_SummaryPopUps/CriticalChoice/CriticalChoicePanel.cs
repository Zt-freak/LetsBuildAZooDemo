// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.CriticalChoice.CriticalChoicePanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData;

namespace TinyZoo.Z_SummaryPopUps.CriticalChoice
{
  internal class CriticalChoicePanel
  {
    public Vector2 location;
    protected Vector2 framescale;
    protected float basescale;
    protected UIScaleHelper uiscale;
    protected Vector2 pad;
    protected BigBrownPanel panel;
    private CriticalChoiceFrame criticalchoice;
    private float timer;

    public CriticalChoicePanel(CriticalChoiceSet criticalchoiceset, float basescale)
    {
      this.timer = 0.0f;
      switch (criticalchoiceset.criticalcharacter)
      {
        case CriticalChoiceCharacter.Scientist:
          this.criticalchoice = (CriticalChoiceFrame) new CriticalChoiceGenericFrame(criticalchoiceset, basescale);
          break;
        case CriticalChoiceCharacter.Painter:
          this.criticalchoice = (CriticalChoiceFrame) new CriticalChoiceGenericFrame(criticalchoiceset, basescale);
          break;
        case CriticalChoiceCharacter.GenomeGuy:
          this.criticalchoice = (CriticalChoiceFrame) new CriticalChoiceGenericFrame(criticalchoiceset, basescale);
          break;
        default:
          this.criticalchoice = (CriticalChoiceFrame) null;
          break;
      }
      this.framescale = this.criticalchoice.GetSize();
      this.panel = new BigBrownPanel(this.framescale, addHeaderText: "Limited-Time Quest", _BaseScale: basescale);
      this.panel.Finalize(this.framescale);
    }

    public virtual Vector2 GetSize() => this.framescale;

    public bool UpdateCriticalChoicePanel(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out int choice)
    {
      offset += this.location;
      bool flag1 = false;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      bool flag2 = flag1 | this.criticalchoice.UpdateCriticalChoiceFrame(player, offset, DeltaTime, out choice);
      if ((double) this.timer < 0.5)
      {
        this.timer += DeltaTime;
        flag2 = false;
      }
      return flag2;
    }

    public void DrawCriticalChoicePanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.criticalchoice.DrawCriticalChoiceFrame(spritebatch, offset);
    }
  }
}

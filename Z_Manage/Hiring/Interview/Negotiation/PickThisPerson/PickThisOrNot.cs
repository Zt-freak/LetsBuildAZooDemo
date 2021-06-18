// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Negotiation.PickThisPerson.PickThisOrNot
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.Hiring.Interview.Negotiation.PickThisPerson
{
  internal class PickThisOrNot
  {
    private SimpleTextBox tbox;
    private TextButton Yes;
    private TextButton No;
    private LerpHandler_Float lerper;

    public PickThisOrNot()
    {
      this.tbox = new SimpleTextBox("Would you like to make an offer to this candidate?", WillLrp: false, textScale: GameFlags.GetSmallTextScale());
      this.Yes = new TextButton(nameof (Yes));
      this.No = new TextButton(nameof (No));
      this.Yes.vLocation = new Vector2(712f, 300f);
      this.No.vLocation = new Vector2(312f, 300f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public bool UpdatemakePickThisOrNot(float DeltaTime, Player player, out bool WasYes)
    {
      WasYes = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        if (this.No.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          return true;
        if (this.Yes.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        {
          WasYes = true;
          return true;
        }
      }
      return false;
    }

    public void DrawPickThisOrNot(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      this.tbox.Location = new Vector2(512f, 200f);
      this.tbox.DrawSimpleTextBox(Offset);
      this.Yes.DrawTextButton(Offset);
      this.No.DrawTextButton(Offset);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.NeedAthing.NeedSomeArch
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Architcture.NeedAthing
{
  internal class NeedSomeArch
  {
    private SimpleTextHandler simpletexthandler;
    private TextButton simpletextbutton;

    public NeedSomeArch(bool NeedBuilding)
    {
      string str = "Architects can design new buildings for your zoo.";
      this.simpletexthandler = new SimpleTextHandler(!NeedBuilding ? str + "~~To design new buildings you need employees! Keep an eye on your applicants and try to employ an architect." : str + "~~Before you can employ any architects you will need to build an Archtects Office", true, 0.8f, GameFlags.GetSmallTextScale());
      this.simpletextbutton = new TextButton("View");
      this.simpletextbutton.vLocation = new Vector2(512f, 500f);
    }

    public void UpdateNeedSomeArch(float DeltaTime, Player player)
    {
      this.simpletexthandler.UpdateSimpleTextHandler(DeltaTime);
      this.simpletextbutton.UpdateTextButton(player, Vector2.Zero, DeltaTime);
    }

    public void DrawNeedSomeArch()
    {
      this.simpletexthandler.DrawSimpleTextHandler(new Vector2(512f, 150f));
      this.simpletextbutton.DrawTextButton(Vector2.Zero);
    }
  }
}

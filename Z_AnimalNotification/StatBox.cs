// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.StatBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_AnimalNotification
{
  internal class StatBox
  {
    private CustomerFrame frame;
    private float basescale;
    private UIScaleHelper uiScale;

    public StatBox(float basescale_)
    {
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(this.basescale);
    }
  }
}

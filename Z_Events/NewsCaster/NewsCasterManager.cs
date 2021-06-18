// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.NewsCaster.NewsCasterManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_Notification;

namespace TinyZoo.Z_Events.NewsCaster
{
  internal class NewsCasterManager
  {
    private NewsTruck newstruck;
    private CasterAndCamera casterlady;
    private NewsText newstext;

    public NewsCasterManager(string Heading, string Body)
    {
      this.newstruck = new NewsTruck();
      this.casterlady = new CasterAndCamera();
      this.newstext = new NewsText(Heading, Body);
    }

    public void SetNewBodyText(string Body) => this.newstext.SetNewBodyText(Body);

    public void UpdateNewsCasterManager(float DeltaTime)
    {
      this.newstext.UpdateNewsText(DeltaTime);
      this.casterlady.UpdateCasterAndCamera(DeltaTime);
    }

    public bool RemoveThisEvent(Z_NotificationType thiseventjustended) => true;

    public void DrawNewsCasterManager()
    {
      this.newstruck.DrawNewsTruck();
      this.casterlady.DrawCasterAndCamera();
      if (GameFlags.PhotoMode)
        return;
      this.newstext.DrawNewsText();
    }
  }
}

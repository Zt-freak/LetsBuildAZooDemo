// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.DataManage.NewsEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Graphics;

namespace TinyZoo.Z_TitleScreen.NewsFeed.DataManage
{
  internal class NewsEntry
  {
    public string ImageName;
    public string TextName;
    public string BaseName;
    public Texture2D image;
    public string TextBody;
    public string Heading;

    public NewsEntry(string _BaseName) => this.BaseName = _BaseName;

    public void UpdateNewsEntry()
    {
    }

    public void DrawNewsEntry()
    {
    }
  }
}

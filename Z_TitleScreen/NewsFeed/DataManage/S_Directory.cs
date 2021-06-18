// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.DataManage.S_Directory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace TinyZoo.Z_TitleScreen.NewsFeed.DataManage
{
  internal class S_Directory
  {
    internal static List<NewsEntry> newentries = new List<NewsEntry>();

    internal static void ClearCache(ContentManager contentManager, string contentFolder)
    {
      if (Z_DebugFlags.IsBetaVersion || !Directory.Exists(contentManager.RootDirectory + "/" + contentFolder))
        return;
      Directory.Delete(contentManager.RootDirectory + "/" + contentFolder, true);
    }

    internal static void LoadListContent(ContentManager contentManager, string contentFolder)
    {
      S_Directory.newentries = new List<NewsEntry>();
      DirectoryInfo directoryInfo = new DirectoryInfo(contentManager.RootDirectory + "/" + contentFolder);
      if (!directoryInfo.Exists)
      {
        Directory.CreateDirectory(contentManager.RootDirectory + "/" + contentFolder);
        directoryInfo = new DirectoryInfo(contentManager.RootDirectory + "/" + contentFolder);
      }
      contentFolder += "/";
      foreach (FileInfo file in directoryInfo.GetFiles("*.*"))
      {
        NewsEntry newsEntry = (NewsEntry) null;
        string _BaseName = "";
        string str = "";
        for (int index = 0; index < file.Name.Length && file.Name[index] != '.'; ++index)
        {
          if (index > 1)
            _BaseName += file.Name[index].ToString();
          str += file.Name[index].ToString();
        }
        for (int index = 0; index < S_Directory.newentries.Count; ++index)
        {
          if (S_Directory.newentries[index].BaseName == _BaseName)
            newsEntry = S_Directory.newentries[index];
        }
        int num = file.Name[0] == 'i' ? 1 : 0;
        if (newsEntry == null)
        {
          newsEntry = new NewsEntry(_BaseName);
          S_Directory.newentries.Add(newsEntry);
        }
        if (num != 0)
        {
          newsEntry.ImageName = file.Name;
          newsEntry.image = contentManager.Load<Texture2D>(contentFolder + str);
        }
        else
        {
          newsEntry.TextName = file.Name;
          StreamReader streamReader = new StreamReader("Content/" + contentFolder + newsEntry.TextName);
          newsEntry.Heading = streamReader.ReadLine();
          newsEntry.TextBody = streamReader.ReadLine();
        }
      }
    }
  }
}

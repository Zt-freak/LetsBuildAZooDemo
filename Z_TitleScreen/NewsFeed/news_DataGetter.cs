// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.news_DataGetter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Utils.FileDownloader;

namespace TinyZoo.Z_TitleScreen.NewsFeed
{
  internal class news_DataGetter
  {
    private bool GettingText;
    private bool GettingEntries;

    public news_DataGetter()
    {
      FileGetManager.GetThisText("https://springloadedsoftware.com/ZNWS/DLData.txt");
      this.GettingText = true;
    }

    public bool UpdateDataGetter()
    {
      if (this.GettingText)
      {
        if (!FileGetManager.IsGetting)
        {
          if (FileGetManager.errortype == WebGetErrorType.NoError)
          {
            this.GettingEntries = true;
            this.GettingText = false;
            List<string> WebAddress = new List<string>();
            List<string> SaveToThisDir = new List<string>();
            for (int index = 0; index < FileGetManager.TextResult.Count; ++index)
            {
              WebAddress.Add("i_" + FileGetManager.TextResult[index] + ".xnb");
              SaveToThisDir.Add("Content\\News\\" + WebAddress[WebAddress.Count - 1]);
              WebAddress.Add("t_" + FileGetManager.TextResult[index] + ".txt");
              SaveToThisDir.Add("Content\\News\\" + WebAddress[WebAddress.Count - 1]);
            }
            for (int index = 0; index < WebAddress.Count; ++index)
              WebAddress[index] = "https://springloadedsoftware.com/ZNWS/" + WebAddress[index];
            FileGetManager.GetThisFile(WebAddress, SaveToThisDir);
          }
          else
            FileGetManager.GetThisText("https://springloadedsoftware.com/ZNWS/DLData.txt");
        }
      }
      else if (this.GettingEntries && !FileGetManager.IsGetting && (FileGetManager.errortype == WebGetErrorType.NoError && FileGetManager.HasCompletedQueue()))
      {
        this.GettingEntries = false;
        return true;
      }
      return false;
    }

    public void DrawDataGetter()
    {
    }
  }
}

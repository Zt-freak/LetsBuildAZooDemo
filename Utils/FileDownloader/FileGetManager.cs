// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.FileDownloader.FileGetManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using System.IO;
using System.Net;

namespace TinyZoo.Utils.FileDownloader
{
  internal class FileGetManager
  {
    internal static List<string> TextResult;
    internal static WebGetErrorType errortype;
    internal static bool IsGetting;
    private static FileGetter filegetter;
    private static string saveToThisDir;
    internal static List<StringPair> FileQueue = new List<StringPair>();
    internal static List<StringPair> CompletedFiles = new List<StringPair>();

    public FileGetManager() => FileGetManager.filegetter = new FileGetter();

    internal static void GetThisText(string WebAddress)
    {
      FileGetManager.IsGetting = true;
      new FileGetter().GetFile(WebAddress, new FileGetter.WebGetCallback(FileGetManager.CallBackOnTextGot));
    }

    internal static void CallBackOnTextGot(WebGetErrorType weberrortype, WebResponse response)
    {
      FileGetManager.errortype = weberrortype;
      FileGetManager.TextResult = FileGetManager.ReadTextFile(response);
      FileGetManager.IsGetting = false;
    }

    internal static void GetThisFile(List<string> WebAddress, List<string> SaveToThisDir)
    {
      FileGetManager.FileQueue = new List<StringPair>();
      for (int index = 0; index < WebAddress.Count; ++index)
        FileGetManager.FileQueue.Add(new StringPair(WebAddress[index], SaveToThisDir[index]));
      FileGetManager.GetThisFile(FileGetManager.FileQueue[0].WebAddress, FileGetManager.FileQueue[0].FileNameAndPath);
      FileGetManager.IsGetting = true;
    }

    internal static void GetThisFile(string WebAddress, string SaveToThisDir)
    {
      FileGetManager.IsGetting = true;
      FileGetManager.saveToThisDir = SaveToThisDir;
      new FileGetter().GetFile(WebAddress, new FileGetter.WebGetCallback(FileGetManager.CallBackOnFileGot));
    }

    internal static void CallBackOnFileGot(WebGetErrorType weberrortype, WebResponse response)
    {
      FileGetManager.errortype = weberrortype;
      FileGetManager.SaveFileToDir(response, FileGetManager.saveToThisDir);
      FileGetManager.IsGetting = false;
    }

    internal static bool HasCompletedQueue() => FileGetManager.FileQueue.Count == 0;

    internal static void UpdateFileGetManager()
    {
      if (FileGetManager.FileQueue == null || FileGetManager.FileQueue.Count <= 0 || (FileGetManager.IsGetting || FileGetManager.errortype != WebGetErrorType.NoError))
        return;
      FileGetManager.CompletedFiles.Add(FileGetManager.FileQueue[0]);
      FileGetManager.FileQueue.RemoveAt(0);
      if (FileGetManager.FileQueue.Count <= 0)
        return;
      FileGetManager.GetThisFile(FileGetManager.FileQueue[0].WebAddress, FileGetManager.FileQueue[0].FileNameAndPath);
    }

    public static List<string> ReadTextFile(WebResponse response)
    {
      List<string> stringList = new List<string>();
      using (Stream responseStream = response.GetResponseStream())
      {
        StreamReader streamReader = new StreamReader(responseStream);
        while (!streamReader.EndOfStream)
          stringList.Add(streamReader.ReadLine());
      }
      return stringList;
    }

    public static void SaveFileToDir(WebResponse response, string savepath)
    {
      using (Stream responseStream = response.GetResponseStream())
      {
        using (FileStream fileStream = System.IO.File.Create(savepath))
          responseStream.CopyTo((Stream) fileStream);
      }
    }
  }
}

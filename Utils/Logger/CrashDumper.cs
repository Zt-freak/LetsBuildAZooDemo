// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.Logger.CrashDumper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.IO;

namespace TinyZoo.Utils.Logger
{
  internal class CrashDumper
  {
    private static CrashDumper crashdumper;

    internal static CrashDumper Instance
    {
      get
      {
        if (CrashDumper.crashdumper == null)
          CrashDumper.crashdumper = new CrashDumper();
        return CrashDumper.crashdumper;
      }
    }

    public void InitCrashDumperFromMAIN() => AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => this.WriteToFile(e.ExceptionObject.ToString());

    private void WriteToFile(string stringToWrite)
    {
      string str = "Z_CrashDump_" + DateTime.UtcNow.ToString("yyyyMMdd_hhmmss") + ".txt";
      string path = Directory.GetCurrentDirectory() + "\\CrashLogs";
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      StreamWriter streamWriter;
      using (streamWriter = new StreamWriter(path + "\\" + str))
      {
        streamWriter.WriteLine(stringToWrite);
        streamWriter.Close();
      }
    }
  }
}

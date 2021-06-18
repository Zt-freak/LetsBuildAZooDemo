// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.FileDownloader.FileGetter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.ComponentModel;
using System.Net;

namespace TinyZoo.Utils.FileDownloader
{
  internal class FileGetter
  {
    private int index;
    private WebResponse response;
    private FileGetter.WebGetCallback webgetcallback;

    public void GetFile(string webpath, FileGetter.WebGetCallback callback)
    {
      this.webgetcallback = callback;
      this.BeginDownload(webpath);
    }

    private void BeginDownload(string uri)
    {
      WebClient webClient = new WebClient();
      webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.webClient_DownloadProgressChanged);
      webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.webClient_DownloadFileCompleted);
      webClient.Credentials = CredentialCache.DefaultCredentials;
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      this.GetFromWeb(new Uri(uri));
    }

    private void GetFromWeb(Uri uri)
    {
      WebRequest request = WebRequest.Create(uri);
      request.BeginGetResponse((AsyncCallback) (i =>
      {
        try
        {
          this.response = request.EndGetResponse(i);
        }
        catch (WebException ex)
        {
          this.webgetcallback(WebGetErrorType.Error, (WebResponse) null);
        }
        try
        {
          this.webgetcallback(WebGetErrorType.NoError, this.response);
        }
        catch (Exception ex)
        {
        }
        this.webgetcallback = (FileGetter.WebGetCallback) null;
      }), (object) null);
    }

    private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
    }

    private void webClient_DownloadProgressChanged(
      object sender,
      DownloadProgressChangedEventArgs e)
    {
    }

    public delegate void WebGetCallback(WebGetErrorType errortype, WebResponse response);
  }
}

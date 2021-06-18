// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TitleScreen.NewsFeed.NewsFeedManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Content;
using TinyZoo.Z_TitleScreen.NewsFeed.DataManage;
using TinyZoo.Z_TitleScreen.NewsFeed.Rendering;

namespace TinyZoo.Z_TitleScreen.NewsFeed
{
  internal class NewsFeedManager
  {
    private RenderManager rendermanager;
    private news_DataGetter newsdatagetter;

    public NewsFeedManager(ContentManager news_contentmanager)
    {
      if (Z_DebugFlags.IsBetaVersion)
        return;
      S_Directory.LoadListContent(news_contentmanager, "News");
      this.rendermanager = new RenderManager();
      this.newsdatagetter = new news_DataGetter();
    }

    public void UpdateNewsFeedManager(ContentManager content, Player player, float DeltaTime)
    {
      if (Z_DebugFlags.IsBetaVersion)
        return;
      if (this.newsdatagetter.UpdateDataGetter())
      {
        S_Directory.LoadListContent(Game1.News_ContentManager, "News");
        this.rendermanager = new RenderManager();
      }
      this.rendermanager.UpdateRenderManager(player, DeltaTime);
    }

    public void DrawNewsFeedManager()
    {
      if (Z_DebugFlags.IsBetaVersion)
        return;
      this.rendermanager.DrawRenderManager();
    }
  }
}

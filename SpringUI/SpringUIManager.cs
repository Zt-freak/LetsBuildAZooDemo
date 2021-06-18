// Decompiled with JetBrains decompiler
// Type: TinyZoo.SpringUI.SpringUIManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Spring.UI;
using TinyZoo.Utils;

namespace TinyZoo.SpringUI
{
  internal class SpringUIManager
  {
    public SpringUIManager(Player player)
    {
      SpringUI_Main.Instance.InitPanels(player.socialmanager);
      CloudSaveUtil.REF_player = player;
      SpringUI_Main.SetCloudSaveInformation(new SpringUI_Main.Del_GetCloudSaveInformation(CloudSaveUtil.GetCloudSave));
      SpringUI_Main.RetrieveCloudSaveInformation(new SpringUI_Main.Del_SendCloudSaveInformation(CloudSaveUtil.LoadFromCloud));
    }

    public bool UpdateSpringUI(Player player, float DeltaTime)
    {
      bool exit;
      SpringUI_Main.Instance.Update(player.player, DeltaTime, player.socialmanager, out exit, ref player.inputmap.ReleasedThisFrame[7], out bool _);
      return exit;
    }

    public void DrawSpringUIManager(Vector2 offset) => SpringUI_Main.Instance.Draw(offset, TinyZoo.AssetContainer.pointspritebatchTop05);
  }
}

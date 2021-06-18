// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.OldWorldMapManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_WorldMap.Maps;
using TinyZoo.Z_WorldMap.Quests;

namespace TinyZoo.Z_WorldMap
{
  internal class OldWorldMapManager
  {
    private MapRenderer mparenderer;
    private QuestViewManager questviewmanager;
    private BlackOut blackout;

    public OldWorldMapManager(Player player) => this.mparenderer = new MapRenderer(player);

    public void UpdateWorldMapManager(Player player, float DeltaTime, out bool BlockExit)
    {
      BlockExit = false;
      float SimulationTime = DeltaTime;
      GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref SimulationTime, player);
      this.blackout.UpdateColours(DeltaTime);
      if ((double) this.blackout.fAlpha == 1.0)
      {
        if (this.questviewmanager.Active)
          this.questviewmanager = (QuestViewManager) null;
        else
          this.questviewmanager.Active = true;
        this.blackout.SetAlpha(false, 0.3f, 1f, 0.0f);
      }
      if (this.questviewmanager != null)
      {
        if (!this.questviewmanager.Active || !this.questviewmanager.UpdateQuestViewManager(player, DeltaTime, (double) this.blackout.fAlpha == 0.0))
          return;
        this.blackout.SetAlpha(false, 0.3f, 0.0f, 1f);
      }
      else
      {
        CityName _city = this.mparenderer.UpdateMapRenderer(DeltaTime, player, false, out Vector2 _);
        switch (_city)
        {
          case CityName.Shelter:
            Game1.SetNextGameState(GAMESTATE.ShelterSetUp);
            Game1.screenfade.BeginFade(true);
            break;
          case CityName.Count:
            return;
          default:
            this.questviewmanager = new QuestViewManager(_city, player);
            GameStateManager.tutorialmanager.WentToQuestCityView(player);
            this.blackout.SetAlpha(false, 0.3f, 0.0f, 1f);
            break;
        }
        BlockExit = true;
      }
    }

    public void DrawWorldMapManager()
    {
      if (this.questviewmanager != null && this.questviewmanager.Active)
        this.questviewmanager.DrawQuestViewManager();
      else
        this.mparenderer.DrawMapRenderer();
    }
  }
}

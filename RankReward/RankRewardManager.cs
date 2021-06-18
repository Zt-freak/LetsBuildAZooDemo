// Decompiled with JetBrains decompiler
// Type: TinyZoo.RankReward.RankRewardManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.IAPInfo;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.RankReward
{
  internal class RankRewardManager
  {
    private StoreBGManager storebg;
    private BackButton back;
    private BlackOut blackout;
    private LerpHandler_Float lerper;
    private PopLerper poplerper;
    private StringInBox HEader;
    private GameObject Ranker;
    private LerpHandler_Float BaclLerper;
    private TextButton Next;
    private bool Exiting;
    private float Delay;

    public RankRewardManager(Player player)
    {
      this.storebg = new StoreBGManager(IsAutumnal: true);
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(1f, 0.5f, 0.0f);
      this.blackout.SetAlpha(0.3f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.lerper.SetDelay(0.2f);
      this.HEader = new StringInBox("Rank Up", 3f, 100f, true);
      this.HEader.SetButtonYellow();
      this.HEader.vLocation = new Vector2(512f, 80f);
      this.HEader.TextColour = Color.Black;
      this.poplerper = new PopLerper();
      this.poplerper.SetDelay(0.8f);
      this.Ranker = IAPStatusManager.GetMedalIcon(player);
      this.Ranker.scale = 4f;
      this.Ranker.vLocation = Sengine.ReferenceScreenRes * 0.5f;
      this.Ranker.vLocation.Y += 20f;
      this.Next = new TextButton(SEngine.Localization.Localization.GetText(14));
      this.Next.SetButtonYellow();
      this.Next.vLocation = new Vector2(900f, 700f);
      this.Next.AddControllerButton(ControllerButton.XboxA);
      this.Exiting = false;
    }

    public void UpdateRankRewardManager(float DeltaTime, Player player)
    {
      this.storebg.UpdateStoreBGManager(DeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
      int num = (int) this.poplerper.OnUpdate(DeltaTime);
      this.Delay += DeltaTime;
      if ((double) this.Delay <= 1.0 || this.Exiting || !this.Next.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        return;
      this.Exiting = true;
      TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
      TinyZoo.Game1.screenfade.BeginFade(true);
      player.OldSaveThisPlayer();
    }

    public void DrawRankRewardManager()
    {
      this.storebg.DrawStoreBGManager(Vector2.Zero);
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.HEader.DrawStringInBox(new Vector2(this.lerper.Value * 1024f, 0.0f), AssetContainer.pointspritebatchTop05);
      if ((double) this.poplerper.CurrentValue > 0.0)
        this.Ranker.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Vector2.Zero, this.Ranker.scale * this.poplerper.CurrentValue, 1f);
      this.Next.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

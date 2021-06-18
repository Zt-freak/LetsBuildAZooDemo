// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildThisInfo.BuildUI
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildThisInfo
{
  internal class BuildUI
  {
    private GameObjectNineSlice frame;
    private Vector2 VScale;
    private SimpleTextHandler textrendr;
    private SimpleTextHandler textrendr2;
    private SimpleTextHandler TextRenderer3;
    private TextButton Button;
    private LerpHandler_Float lerper;
    private bool DrawTR3;
    private TileStats tstats;
    public bool HasEnoughCash;
    private int Duplicates;
    private Vector2 Offset;
    private TILETYPE tletobuild;
    private Vector2 ExtraPhoneOffset;
    public bool CanBuild;
    private BuildMessageType LastMessageType;
    private float ParaWidth = 0.4f;
    private float TScale = 2f;

    public BuildUI(TILETYPE _tletobuild, Player player, int _Duplicates = 1)
    {
      this.ExtraPhoneOffset = Vector2.Zero;
      float y = 140f * Sengine.UltraWideSreenUpwardsMultiplier;
      float x = 800f;
      if (GameFlags.MobileUIScale)
        this.ExtraPhoneOffset = new Vector2(0.0f, -20f);
      this.tletobuild = _tletobuild;
      this.Duplicates = _Duplicates;
      this.frame = new GameObjectNineSlice(new Rectangle(877, 350, 21, 21), 7);
      this.frame.vLocation = new Vector2(512f, (float) (768.0 - (double) y / 2.0));
      this.VScale = new Vector2(x, y);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 1f, 3f);
      float Length = 30f;
      if (GameFlags.IsConsoleVersion || DebugFlags.IsPCVersion)
        Length = 45f;
      this.Button = new TextButton(SEngine.Localization.Localization.GetText(9), Length);
      if (GameFlags.MobileUIScale)
        this.Button.CollisionEx = new Vector2(10f, 20f);
      this.Button.AddControllerButton(ControllerButton.XboxA);
      this.frame.SetAlpha(1f);
      this.tstats = TileData.GetTileStats(this.tletobuild);
      this.DrawTR3 = false;
      this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(6) + " " + this.tstats.Name + SEngine.Localization.Localization.GetText(7) + (object) (player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates), true, 0.4f, 2f * Sengine.UltraWideSreenDownardsMultiplier);
      if (PlayerStats.language == Language.Korean)
        this.textrendr = new SimpleTextHandler(string.Format(SEngine.Localization.Localization.GetText(83), (object) this.tstats.Name, (object) (player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates)), true, 0.4f, 2f * Sengine.UltraWideSreenDownardsMultiplier);
      this.textrendr2 = new SimpleTextHandler(SEngine.Localization.Localization.GetText(8), true, 0.4f, 2f);
      this.textrendr2.paragraph.linemaker.SetAllColours(0.5f, 0.1f, 0.1f);
      this.textrendr.paragraph.linemaker.SetAllColours(0.4666667f, 0.3333333f, 0.2235294f);
      this.textrendr.AutoCompleteParagraph();
      this.textrendr2.AutoCompleteParagraph();
      this.SetUp(this.tletobuild, player, _Duplicates);
    }

    public bool CheckBlocked(Vector2 Location) => MathStuff.CheckPointCollision(true, this.frame.vLocation + this.Offset, 1f, this.VScale.X, this.VScale.Y, Location);

    public void SetUp(
      TILETYPE tletobuild,
      Player player,
      int _Duplicates = 1,
      bool MinimumFootPrintMet = true,
      bool IsGraveYard = false,
      int CurrentSize = -1)
    {
      if (_Duplicates == this.Duplicates)
        return;
      this.Duplicates = _Duplicates;
      if (this.LastMessageType == BuildMessageType.CanBuild)
      {
        this.LastMessageType = BuildMessageType.Count;
        this.SetUp(BuildMessageType.CanBuild, player, IsGraveYard);
        this.lerper.Value = 0.0f;
      }
      else
      {
        if (this.LastMessageType != BuildMessageType.CanBuild_ButNoMoney)
          return;
        this.LastMessageType = BuildMessageType.Count;
        this.SetUp(BuildMessageType.CanBuild, player, IsGraveYard);
      }
    }

    public void SetUp(
      BuildMessageType messagetype,
      Player player,
      bool IsCemeteary = false,
      bool SkipLerp = false)
    {
      this.TScale = 3f * Sengine.UltraWideSreenDownardsMultiplier;
      this.ParaWidth = 0.6f;
      if (DebugFlags.IsPCVersion)
        this.TScale = RenderMath.GetPixelSizeBestMatch(GameFlags.GetSmallTextScale());
      if (this.LastMessageType == messagetype)
        return;
      this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);
      this.LastMessageType = messagetype;
      switch (messagetype)
      {
        case BuildMessageType.PlaceNextToExistingWall:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler("Must be next to existing structure.", true, this.ParaWidth, this.TScale);
          break;
        case BuildMessageType.Overlapping:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(23), true, this.ParaWidth, this.TScale);
          break;
        case BuildMessageType.CanBuild:
          if (IsCemeteary)
          {
            this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(30), true, this.ParaWidth, this.TScale);
          }
          else
          {
            if (DebugFlags.IsPCVersion)
              this.Duplicates = 1;
            int Cost = player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates;
            if (player.Stats.GetCashHeld() >= Cost)
            {
              this.Button.SetButtonGreen();
              string TextToWrite = SEngine.Localization.Localization.GetText(6) + this.tstats.Name + SEngine.Localization.Localization.GetText(7) + (object) (player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates);
              if (PlayerStats.language == Language.Korean || PlayerStats.language == Language.English)
                TextToWrite = string.Format(SEngine.Localization.Localization.GetText(83), (object) this.tstats.Name, (object) (player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates));
              this.textrendr = new SimpleTextHandler(TextToWrite, true, this.ParaWidth, this.TScale);
            }
            else
            {
              this.textrendr = new SimpleTextHandler(string.Format(SEngine.Localization.Localization.GetText(29), (object) Cost), true, this.ParaWidth, this.TScale);
              this.Button.SetButtonRed();
            }
            this.Button.SetAsBuyButton(Cost, player);
          }
          if (!SkipLerp)
          {
            this.lerper.SetLerp(false, 1f, 0.0f, 3f, true);
            this.CanBuild = true;
            break;
          }
          break;
        case BuildMessageType.CanBuild_ButNoMoney:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(28), true, this.ParaWidth, this.TScale);
          break;
        case BuildMessageType.TooSmall:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(26), true, this.ParaWidth, this.TScale);
          break;
        case BuildMessageType.TooBig:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(27), true, this.ParaWidth, this.TScale);
          break;
        case BuildMessageType.MakeTaller:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(24), true, this.ParaWidth, this.TScale);
          break;
        case BuildMessageType.MakeWider:
          this.CanBuild = false;
          this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(25), true, this.ParaWidth, this.TScale);
          break;
        default:
          this.CanBuild = false;
          if ((double) this.lerper.TargetValue != 1.0)
          {
            this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
            break;
          }
          break;
      }
      this.textrendr.AutoCompleteParagraph();
      this.textrendr.paragraph.linemaker.SetAllColours(0.4666667f, 0.3333333f, 0.2235294f);
    }

    public bool GetCanBuild() => this.CanBuild && (double) this.lerper.Value == 0.0;

    public bool UpdateBuildUI(float DeltaTime, Player player, bool UseTouchStart = false)
    {
      if (FeatureFlags.BlockBuyPanel)
        return false;
      this.Offset = new Vector2(0.0f, this.lerper.Value * 120f);
      if (!this.HasEnoughCash && player.Stats.GetCashHeld() >= player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates)
      {
        this.HasEnoughCash = true;
        this.textrendr = new SimpleTextHandler(SEngine.Localization.Localization.GetText(6) + this.tstats.Name + SEngine.Localization.Localization.GetText(7) + (object) (player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates), true, this.ParaWidth, this.TScale);
        if (PlayerStats.language == Language.Korean)
          this.textrendr = new SimpleTextHandler(string.Format(SEngine.Localization.Localization.GetText(83), (object) this.tstats.Name, (object) (player.livestats.GetCost(this.tletobuild, player, true) * this.Duplicates)), true, 0.4f, 2f * Sengine.UltraWideSreenDownardsMultiplier);
        this.textrendr.AutoCompleteParagraph();
        this.textrendr.paragraph.linemaker.SetAllColours(0.4666667f, 0.3333333f, 0.2235294f);
        this.Button.SetButtonGreen();
      }
      this.lerper.UpdateLerpHandler(DeltaTime);
      return this.HasEnoughCash && this.LastMessageType == BuildMessageType.CanBuild && this.Button.UpdateTextButton(player, this.frame.vLocation + this.Offset + this.ExtraPhoneOffset + new Vector2(0.0f, 25f * Sengine.ScreenRationReductionMultiplier.Y), DeltaTime, UseTouchStart: UseTouchStart);
    }

    public void DrawBuildUI()
    {
      if (FeatureFlags.BlockBuyPanel)
        return;
      this.Offset = new Vector2(0.0f, this.lerper.Value * this.VScale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Offset, this.VScale);
      float num = -30f;
      if (GameFlags.MobileUIScale)
        num = -20f;
      this.textrendr.DrawSimpleTextHandler(this.frame.vLocation + this.Offset + this.ExtraPhoneOffset + new Vector2(0.0f, num * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
      if (this.LastMessageType != BuildMessageType.CanBuild)
        return;
      this.Button.DrawTextButton(this.frame.vLocation + this.ExtraPhoneOffset + this.Offset + new Vector2(0.0f, 25f * Sengine.ScreenRationReductionMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}

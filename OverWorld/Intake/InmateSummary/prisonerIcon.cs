// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.InmateSummary.prisonerIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Intake.InmateSummary.Psners;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.Intake.InmateSummary
{
  internal class prisonerIcon
  {
    private GenericBox box;
    private GameObject Name;
    private PrisonerSprite prisonersprite;
    public Vector2 Location;
    private GameObject Valffue;
    private GameObject FloorTile;
    private string NameString;
    private string EnemyTypeString;
    private string ValueString;

    public prisonerIcon(IntakePerson intakeperson)
    {
      this.box = new GenericBox(new Vector2(240f, 80f));
      this.prisonersprite = new PrisonerSprite(intakeperson.animaltype, intakeperson.CLIndex);
      this.prisonersprite.vLocation.X = -90f;
      this.prisonersprite.scale = 2f * Sengine.UltraWideSreenUpwardsMultiplier;
      this.Name = new GameObject();
      this.Name.SetAllColours(ColourData.Cyannz);
      this.NameString = intakeperson.Name;
      this.ValueString = string.Format(SEngine.Localization.Localization.GetText(3), (object) intakeperson.P_PerDay);
      this.Valffue = new GameObject();
      this.Valffue.SetAllColours(0.2f, 1f, 0.2f);
      this.EnemyTypeString = EnemyData.GetEnemyTypeName(intakeperson.animaltype);
      this.FloorTile = new GameObject();
      CellBlockType celltype = CellBlockType.Grasslands;
      if (LiveStats.reqforpeople.wantsbyperson[(int) intakeperson.animaltype].CellRequirement > -1)
        celltype = (CellBlockType) LiveStats.reqforpeople.wantsbyperson[(int) intakeperson.animaltype].CellRequirement;
      float Rotation = 0.0f;
      this.FloorTile.DrawRect = TileData.GetTileInfo(TileData.GetCellBlockTypeToPice(celltype, CellBlockPiece.Floor)).GetRect(0, ref Rotation);
      this.FloorTile.SetDrawOriginToCentre();
    }

    public void UpdateprisonerIcon()
    {
    }

    public void DrawprisonerIcon(Vector2 Offset, SpriteBatch spritebatch)
    {
      float num = 1f;
      if (PlayerStats.language == Language.Japanese)
        num = 1.3f;
      if (PlayerStats.language == Language.Chinese_Simplified || PlayerStats.language == Language.Chinese_Traditional || PlayerStats.language == Language.Korean)
        num = 1.3f;
      this.box.DrawGenericBox(Offset + this.Location, spritebatch);
      this.FloorTile.scale = this.prisonersprite.scale;
      this.FloorTile.Draw(spritebatch, AssetContainer.EnvironmentSheet, this.prisonersprite.vLocation + Offset + this.Location);
      this.prisonersprite.DrawPrisonerSprite(Offset + this.Location, spritebatch);
      this.Name.vLocation = new Vector2(-65f, -30f * Sengine.UltraWideSreenUpwardsMultiplier);
      TextFunctions.DrawTextWithDropShadow(this.NameString, 2f * Sengine.UltraWideSreenUpwardsMultiplier, Offset + this.Location + this.Name.vLocation, this.Name.GetColour(), this.Name.fAlpha, AssetContainer.springFont, spritebatch, false);
      if (PlayerStats.language != Language.Chinese_Simplified && PlayerStats.language != Language.Korean && (PlayerStats.language != Language.Chinese_Traditional && PlayerStats.language != Language.Japanese))
        TextFunctions.DrawTextWithDropShadow(this.EnemyTypeString, 1.5f * Sengine.UltraWideSreenUpwardsMultiplier, Offset + this.Location + this.Name.vLocation + new Vector2(0.0f, 20f * Sengine.UltraWideSreenUpwardsMultiplier), this.Name.GetColour(), this.Name.fAlpha, AssetContainer.springFont, spritebatch, false);
      TextFunctions.DrawTextWithDropShadow(this.ValueString, 2f * Sengine.UltraWideSreenUpwardsMultiplier * num * Sengine.UltraWideSreenDownardsMultiplier, Offset + this.Location + this.Name.vLocation + new Vector2(0.0f, 40f * Sengine.UltraWideSreenUpwardsMultiplier), this.Valffue.GetColour(), this.Name.fAlpha, AssetContainer.springFont, spritebatch, false);
    }
  }
}

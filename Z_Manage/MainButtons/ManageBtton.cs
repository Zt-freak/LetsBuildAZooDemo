// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.ManageBtton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Manage.MainButtons.ButtonExtras;

namespace TinyZoo.Z_Manage.MainButtons
{
  internal class ManageBtton
  {
    private TextButton button;
    private GameObjectNineSlice BTNFrame;
    private Vector2 VSCale;
    public Vector2 Location;
    private SimpleTextHandler textdesc;
    private TextButton mainbutton;
    private Sp_CoolDownTimer sp_cooldown;
    private SpecialIcon specialIcon;
    private ButtonProgressBar buttonprgressbar;
    public ManageButtonType ThisButonType;
    private ScreenHeading screenheading;

    public ManageBtton(ManageButtonType managebuttontype)
    {
      this.screenheading = new ScreenHeading("ZOO MANAGEMENT", 70f);
      this.ThisButonType = managebuttontype;
      this.specialIcon = new SpecialIcon(managebuttontype);
      this.Location = new Vector2(256f, 150f);
      Vector3 SecondaryColour;
      this.BTNFrame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.VSCale = new Vector2(400f, 150f);
      this.BTNFrame.scale = 3f * Sengine.ScreenRationReductionMultiplier.Y;
      string TextToWrite = "";
      string TextToDraw = "";
      string str;
      switch (managebuttontype)
      {
        case ManageButtonType.Hiring:
          str = "Employ Staff to help!";
          TextToWrite = "No longer in game";
          TextToDraw = "DISABLED";
          break;
        case ManageButtonType.Research:
          TextToWrite = "Assign Architects to design new buildings for the zoo";
          TextToDraw = "Design";
          break;
        case ManageButtonType.Genomesequencing:
          TextToWrite = "Map a genome to unlock the lab";
          TextToDraw = "Lab";
          break;
        case ManageButtonType.Accounts:
          TextToWrite = "Your Zoo's financial records";
          TextToDraw = "Accounts";
          break;
        case ManageButtonType.BusUpgrades:
          TextToWrite = "Control how many people can reach your Zoo";
          TextToDraw = "Transport";
          break;
        case ManageButtonType.BuyLand:
          str = "Invest in increasing the size of your park";
          TextToWrite = "No longer in game";
          TextToDraw = "DISABLED";
          break;
        case ManageButtonType.CleanPen:
          TextToWrite = "";
          TextToDraw = "Clean";
          break;
        case ManageButtonType.MoveAnimals:
          TextToWrite = "";
          TextToDraw = "Move";
          break;
        case ManageButtonType.Feed:
          TextToWrite = "";
          TextToDraw = "Feed";
          break;
        case ManageButtonType.AnimalShow:
          TextToWrite = "";
          TextToDraw = "Show";
          break;
        case ManageButtonType.UpgradePen:
          TextToWrite = "";
          TextToDraw = "Upgrade";
          break;
        case ManageButtonType.CustomizePen:
          TextToWrite = "";
          TextToDraw = "Customize";
          break;
      }
      if (DebugFlags.IsPCVersion)
      {
        this.textdesc = new SimpleTextHandler(TextToWrite, false, 0.23f, GameFlags.GetSmallTextScale(), false, false);
        this.textdesc.AutoCompleteParagraph();
        this.textdesc.Location = new Vector2(-10f, -50f);
        this.textdesc.paragraph.linemaker.SetAllColours(SecondaryColour);
        this.textdesc.Location.Y = 30f;
        this.textdesc.Location.X = -45f;
      }
      this.mainbutton = new TextButton(TextToDraw, 70f);
      this.mainbutton.vLocation = new Vector2(-105f, 40f);
      this.mainbutton.vLocation = new Vector2(60f, 0.0f);
      this.mainbutton.AddControllerButton(GameFlags.GetCorrectButtonFace(ButtonPressed.Confirm));
    }

    public bool UpdateManageBtton(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      bool blockControllerIcon = false)
    {
      if (this.sp_cooldown != null)
        this.sp_cooldown.UpdateSp_CoolDownTimer(player);
      return this.mainbutton.UpdateTextButton(player, this.Location, DeltaTime, BlockControllerIcon: blockControllerIcon) && (FeatureFlags.DarkenAllButThisInMANAGE == ManageButtonType.Count || this.ThisButonType == FeatureFlags.DarkenAllButThisInMANAGE);
    }

    public void DrawManageBtton(Vector2 Offset, bool MouseOver_Controller = false)
    {
      if (this.screenheading != null)
        this.screenheading.DrawScreenHeading(Offset, AssetContainer.pointspritebatch03);
      this.BTNFrame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location + Offset, this.VSCale);
      float num = (float) ((double) this.VSCale.X * 0.5 - 70.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.specialIcon.DrawSpecialIcon(Offset + this.Location + new Vector2(-num * Sengine.ScreenRatioUpwardsMultiplier.Y, 0.0f), AssetContainer.pointspritebatch03);
      this.textdesc.DrawSimpleTextHandler(Offset + this.Location);
      if (GameFlags.IsUsingController)
        this.mainbutton.MouseOver = MouseOver_Controller;
      this.mainbutton.DrawTextButton(Offset + this.Location, 1f, AssetContainer.pointspritebatch03, !MouseOver_Controller);
      if (FeatureFlags.DarkenAllButThisInMANAGE != ManageButtonType.Count && this.ThisButonType != FeatureFlags.DarkenAllButThisInMANAGE)
      {
        float fAlpha = this.BTNFrame.fAlpha;
        this.BTNFrame.fAlpha = 0.8f;
        this.BTNFrame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location + Offset, this.VSCale);
        this.BTNFrame.fAlpha = fAlpha;
      }
      else
      {
        if (this.buttonprgressbar != null)
          this.buttonprgressbar.DrawButtonProgressBar(Offset + this.Location);
        if (this.sp_cooldown == null)
          return;
        this.sp_cooldown.DrawSp_CoolDownTimer(Offset + this.Location);
      }
    }
  }
}

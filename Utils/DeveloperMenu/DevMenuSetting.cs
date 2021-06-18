// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.DeveloperMenu.DevMenuSetting
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Utils.DeveloperMenu
{
  internal class DevMenuSetting
  {
    public int ThisSetting;
    private TextButton setting;
    private string DrawThis;
    private DeveloperOverrides thisoverride;
    private int Max = 2;
    private bool Wrap = true;
    public Vector2 Location;

    public DevMenuSetting(int _ThisSetting, DeveloperOverrides _thisoverride)
    {
      this.thisoverride = _thisoverride;
      this.ThisSetting = _ThisSetting;
      this.setting = new TextButton("Off", 50f);
      this.DrawThis = string.Concat((object) this.thisoverride);
      if (Z_DebugFlags.IsBetaVersion)
      {
        switch (this.thisoverride)
        {
          case DeveloperOverrides.AllAnimalsFight:
          case DeveloperOverrides.GatesWillBreak:
            this.ThisSetting = 3;
            break;
        }
      }
      switch (this.thisoverride)
      {
        case DeveloperOverrides.AllAnimalsFight:
        case DeveloperOverrides.GatesWillBreak:
          this.Max = 4;
          break;
        case DeveloperOverrides.InfiniteResearchPoints:
        case DeveloperOverrides.UnlockAllResearh:
        case DeveloperOverrides.EmployeesWillQuit:
        case DeveloperOverrides.InfiniteMoney:
        case DeveloperOverrides.EnrichmentDisabled:
        case DeveloperOverrides.StopTime:
        case DeveloperOverrides.MakeLotsOfBabies:
        case DeveloperOverrides.DisableUI:
        case DeveloperOverrides.DisableMoralityBlocks:
          this.Max = 1;
          break;
        case DeveloperOverrides.AddAnimal:
        case DeveloperOverrides.AddPeople:
        case DeveloperOverrides.UnlockAllLand:
        case DeveloperOverrides.ClearDeadAnimals:
        case DeveloperOverrides.SaveNow:
          this.Max = 0;
          break;
      }
      if (this.thisoverride == DeveloperOverrides.UnlockAllResearh)
        this.Wrap = false;
      this.SetString();
    }

    private void SetString()
    {
      Z_DebugFlags.developerOverrides[(int) this.thisoverride] = this.ThisSetting;
      if (this.Max == 0)
      {
        this.setting.SetText("Do it!");
        this.setting.SetButtonColour(BTNColour.Green);
      }
      else if (this.Max == 4)
      {
        switch (this.ThisSetting)
        {
          case 0:
            this.setting.SetText("Default");
            this.setting.SetButtonColour(BTNColour.Red);
            break;
          case 1:
            this.setting.SetText("On");
            this.setting.SetButtonColour(BTNColour.Green);
            break;
          case 2:
            this.setting.SetText("Boost");
            this.setting.SetButtonColour(BTNColour.Blue);
            break;
          case 3:
            this.setting.SetText("Disabled");
            this.setting.SetButtonColour(BTNColour.EvilPurple);
            break;
        }
      }
      else
      {
        switch (this.ThisSetting)
        {
          case 0:
            this.setting.SetText("Off");
            this.setting.SetButtonColour(BTNColour.Red);
            break;
          case 1:
            this.setting.SetText("On");
            this.setting.SetButtonColour(BTNColour.Green);
            break;
          case 2:
            this.setting.SetText("Boost");
            this.setting.SetButtonColour(BTNColour.Blue);
            break;
        }
      }
    }

    public bool UpdateDevMenuSetting(Player player, Vector2 Offset, float DeltaTime)
    {
      Offset += this.Location;
      if (!this.setting.UpdateTextButton(player, Offset, DeltaTime))
        return false;
      if (this.Wrap || this.ThisSetting != this.Max)
      {
        ++this.ThisSetting;
        if (this.ThisSetting > this.Max)
          this.ThisSetting = 0;
        this.SetString();
      }
      return true;
    }

    public void DrawDevMenuSetting(Vector2 Offset)
    {
      Offset += this.Location;
      this.setting.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatch07Final);
      TextFunctions.DrawTextWithDropShadow(this.DrawThis, RenderMath.GetPixelZoomOneToOne(), Offset + this.setting.vLocation + new Vector2(100f, -5f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch07Final, false);
    }
  }
}

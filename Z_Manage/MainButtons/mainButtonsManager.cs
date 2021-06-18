// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.mainButtonsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Buttons;
using SEngine.Input;
using System.Collections.Generic;
using TinyZoo.Audio;

namespace TinyZoo.Z_Manage.MainButtons
{
  internal class mainButtonsManager
  {
    internal static ManageBtton[] managebuttons;
    private ControllerGridNavMatrix controllerMatrix;
    private int columns = 2;

    public mainButtonsManager(bool IsPenManager = false, bool IsShop = false)
    {
      if (!IsShop)
      {
        if (IsPenManager)
        {
          mainButtonsManager.managebuttons = new ManageBtton[4];
          mainButtonsManager.managebuttons[0] = new ManageBtton(ManageButtonType.CleanPen);
          mainButtonsManager.managebuttons[1] = new ManageBtton(ManageButtonType.Feed);
          mainButtonsManager.managebuttons[2] = new ManageBtton(ManageButtonType.PenSummary);
          mainButtonsManager.managebuttons[3] = new ManageBtton(ManageButtonType.FoodChain);
        }
        else
        {
          List<ManageButtonType> manageButtonTypeList = new List<ManageButtonType>();
          manageButtonTypeList.Add(ManageButtonType.Hiring);
          manageButtonTypeList.Add(ManageButtonType.Accounts);
          manageButtonTypeList.Add(ManageButtonType.Research);
          manageButtonTypeList.Add(ManageButtonType.Genomesequencing);
          manageButtonTypeList.Add(ManageButtonType.BusUpgrades);
          manageButtonTypeList.Add(ManageButtonType.BuyLand);
          int num1 = 200;
          int num2 = 200;
          int num3 = 256;
          int num4 = 512;
          mainButtonsManager.managebuttons = new ManageBtton[manageButtonTypeList.Count];
          for (int index = 0; index < manageButtonTypeList.Count; ++index)
          {
            mainButtonsManager.managebuttons[index] = new ManageBtton(manageButtonTypeList[index]);
            mainButtonsManager.managebuttons[index].Location = new Vector2((float) (num3 + num4 * (index % this.columns)), (float) (num1 + num2 * (index / this.columns)));
          }
        }
      }
      this.controllerMatrix = new ControllerGridNavMatrix(this.columns, mainButtonsManager.managebuttons.Length, 0);
    }

    public ManageButtonType UpdatemainButtonsManager(
      Player player,
      float DeltaTime,
      Vector2 Offset)
    {
      ManageButtonType manageButtonType = ManageButtonType.Count;
      int num = -1;
      for (int index = 0; index < mainButtonsManager.managebuttons.Length; ++index)
      {
        if (FeatureFlags.DarkenAllButThisInMANAGE != ManageButtonType.Count && mainButtonsManager.managebuttons[index].ThisButonType == FeatureFlags.DarkenAllButThisInMANAGE)
        {
          num = index;
          this.controllerMatrix.SelectedIndex = num;
          break;
        }
      }
      int selectedIndex = this.controllerMatrix.SelectedIndex;
      if (num == -1 && this.controllerMatrix.UpdateGridNavigatorMatrix(player.inputmap.HeldButtons[17], player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[18], player.inputmap.HeldButtons[19], DeltaTime, out DirectionPressed _) && selectedIndex != this.controllerMatrix.SelectedIndex)
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
      for (int index = 0; index < mainButtonsManager.managebuttons.Length; ++index)
      {
        if (mainButtonsManager.managebuttons[index].UpdateManageBtton(DeltaTime, player, Offset, this.controllerMatrix.SelectedIndex != index))
          manageButtonType = mainButtonsManager.managebuttons[index].ThisButonType;
      }
      if (manageButtonType != ManageButtonType.Count)
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
      return manageButtonType;
    }

    public void DrawmainButtonsManager(Vector2 Offset)
    {
      for (int index = 0; index < mainButtonsManager.managebuttons.Length; ++index)
        mainButtonsManager.managebuttons[index].DrawManageBtton(Offset, this.controllerMatrix.SelectedIndex == index);
    }
  }
}

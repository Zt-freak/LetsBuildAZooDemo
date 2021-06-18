// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.ExpandPopUp.ExpansionPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.OverWorld.Store_Local.StoreBG;

namespace TinyZoo.Z_OverWorld.ExpandPopUp
{
  internal class ExpansionPopUpManager
  {
    private StoreBGManager storeBG;
    private LerpHandler_Float lerper;
    private TextButton textbutton;
    private CharacterTextBox charactertextbox;
    private DollarPanel dollarpanel;
    private CoinAndString coinandstring;
    private SimpleTextBox TBox;

    public ExpansionPopUpManager(Player player)
    {
      this.storeBG = new StoreBGManager(true);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "Would you like to expand your zoo?");
      this.coinandstring = new CoinAndString(100);
      this.textbutton = new TextButton("Yes");
    }

    public void UpdateExpansionPopUpManager(float DeltaTime, Player player)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
    }

    public void DrawExpansionPopUpManager()
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.storeBG.DrawStoreBGManager(Offset);
      this.coinandstring.DrawCoinAndString(AssetContainer.pointspritebatchTop05, Offset);
    }
  }
}

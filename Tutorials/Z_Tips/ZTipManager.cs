// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tips.ZTipManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Tutorials.Z_Tips
{
  internal class ZTipManager
  {
    private SmartCharacterBox charactertextbox;
    private Arrow arrow;
    private Vector2 ArrowLocation;

    public ZTipManager(ZTipType tiptype, ref ZTipType LastTip, ref int ZTIPCYCLE)
    {
      ZTIPCYCLE = 0;
      LastTip = tiptype;
      string FirstText = "";
      switch (tiptype)
      {
        case ZTipType.TicketPriceTooCheap:
          FirstText = "Your ticket price is very cheap!~You could be making more money!~~Select the Ticket booth at the main entrance to adjust the price!";
          break;
        case ZTipType.TicketPriceTooExpensive:
          FirstText = "Your ticket price is very expensive!~This will make people leave the zoo without coming in!~~Select the Ticket booth at the main entrance to adjust the price!";
          break;
        case ZTipType.AnimalsBreedReady:
          FirstText = "Your animals have finished breeding!~~Go to the Hutch to collect your newborns!";
          break;
        case ZTipType.YouCanCompleteATrade:
          FirstText = "You have enough animals to trade with another zoo for something new!~Go to the world map hwen you want to collect!";
          break;
        case ZTipType.UnhappyAnimalsNoSpace:
          FirstText = "Some of your animals are not being looked after!~This impacts your ticket price, park rating, end in extreme cases might lead to your animals getting sick, or worse...~Some of your animals that need more space.";
          break;
      }
      this.charactertextbox = new SmartCharacterBox(FirstText, AnimalType.Administrator);
      this.charactertextbox.Location.Y += 60f;
    }

    public bool UpdateZTipManager(float DeltaTime, Player player) => TutorialManager.currenttutorial != TUTORIALTYPE.None || OverWorldManager.overworldstate != OverWOrldState.MainMenu || (Game1.gamestate != GAMESTATE.OverWorld || this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, DoNotClearInput: true));

    public void DrawZTipManager()
    {
      if (DebugFlags.HideAllUI_DEBUG)
        return;
      this.charactertextbox.DrawSmartCharacterBox();
    }
  }
}

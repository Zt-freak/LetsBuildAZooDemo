// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_NewGameSettings.NewSettingsOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_NewGameSettings
{
  internal class NewSettingsOption
  {
    private NewGameButtonType newgamebuttontype;
    private GameObjectNineSlice Framer;
    public Vector2 Location;
    private SimpleTextHandler simpletextbox;
    private Vector3 SecondaryColour;
    private List<string> Desciptions;
    private List<string> ButtonText;
    public TextButton button;
    public int Index;
    private bool UseNewLayout = true;

    public NewSettingsOption(NewGameButtonType _newgamebuttontype)
    {
      this.newgamebuttontype = _newgamebuttontype;
      this.Framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out this.SecondaryColour), 7);
      this.Framer.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.SetUpText();
      this.Index = 0;
      this.SetText();
      this.simpletextbox.AutoCompleteParagraph();
    }

    private void SetUpText()
    {
      this.ButtonText = new List<string>();
      this.Desciptions = new List<string>();
      switch (this.newgamebuttontype)
      {
        case NewGameButtonType.SimLevel:
          this.ButtonText.Add("Sim Level: Default");
          this.ButtonText.Add("Sim Level: Easy");
          this.ButtonText.Add("Sim Level: Hardcore");
          this.Desciptions.Add("A well balanced game, play as it was intended.");
          this.Desciptions.Add("Everything will be a bit easier to achieve.");
          this.Desciptions.Add("Only the most brave and courageous zoo keepers should take on this challenge!");
          break;
        case NewGameButtonType.StartingLoan:
          this.ButtonText.Add("Starting Loan: Default");
          this.ButtonText.Add("Starting Loan: Low");
          this.ButtonText.Add("Starting Loan: High");
          this.Desciptions.Add("Start the game with an acceptable volume of money!");
          this.Desciptions.Add("With barely any money to use, do you have what it takes! NOTE- This is part of HARD MODE");
          this.Desciptions.Add("Start the game with excess cash! Maybe you have a wealthy uncle who helped you get that extra good start in life!");
          break;
        case NewGameButtonType.InterestRate:
          this.ButtonText.Add("Interest Rate: Default");
          this.ButtonText.Add("Interest Rate: High");
          this.ButtonText.Add("Interest Rate: None");
          this.Desciptions.Add("Your loans will need to be repaid with a fair and reasonable  interest rate.");
          this.Desciptions.Add("Your loans will need to be repaid at an extremely unfair rate! NOTE- This is part of HARD MODE");
          this.Desciptions.Add("Your loans will have no interest at all, which is clearly unrealistic. But who wants realism in video games?");
          break;
        case NewGameButtonType.AnimalLifeSpan:
          this.ButtonText.Add("Animal Lifespan: Default");
          this.ButtonText.Add("Animal Lifespan: Infinite Lives");
          this.ButtonText.Add("Animal Lifespan: Immortality");
          this.Desciptions.Add("Animals will age and die. Your actions can help them live long and healthy lives, or result in a tragedy earlier than expected.");
          this.Desciptions.Add("Animals can only die from disease, undernourishment and unnatural causes. They will never die of old age.");
          this.Desciptions.Add("Animals will be immortal! NOTE- Playing with this feature enabled substantially changes the experience, and removes multiple game options.");
          break;
        case NewGameButtonType.Crime:
          this.ButtonText.Add("Crime: Default");
          this.ButtonText.Add("Crime: None");
          this.Desciptions.Add("You will need security and police to help ensure a safe environment for everyone.");
          this.Desciptions.Add("Your zoo will be a crime-free utopia! Note that some special events will no longer occur with this enabled.");
          break;
        case NewGameButtonType.Disease:
          this.ButtonText.Add("Disease: Default");
          this.ButtonText.Add("Disease: None");
          this.Desciptions.Add("Animals and people can get sick, use quarantine to stop the spread of contagions, and research to discover them early!");
          this.Desciptions.Add("The air is as clear as a nuclear winter. No microbes will be infecting your animals or your customers!");
          break;
        case NewGameButtonType.BuildingDelapedation:
          this.ButtonText.Add("Building Dilapidation: Default");
          this.ButtonText.Add("Building Dilapidation: None");
          this.Desciptions.Add("Buildings need upkeep and repair, and animal pens may deteriorate, resulting in animals escaping and endangering themselves and the public!");
          this.Desciptions.Add("Buildings will not be damaged over time. Note that some special events will no longer occur with this enabled.");
          break;
        case NewGameButtonType.AnimalsCanAttackHumans:
          this.ButtonText.Add("Animal Temperament: Default");
          this.ButtonText.Add("Animal Temperament: Passive");
          this.Desciptions.Add("Animals may strike out against members of the public or their keepers.");
          this.Desciptions.Add("Animals will never hurt another human. NOTE- Some content will not be available if playing with this selection.");
          break;
        case NewGameButtonType.HabitatNeeds:
          this.ButtonText.Add("Habitat Needs: Default");
          this.ButtonText.Add("Habitat Needs: None");
          this.Desciptions.Add("Animals like to live in their favorite places, and be entertained by their favorite things! Stressed and sad animals might not breed, or cause untold havoc.");
          this.Desciptions.Add("Animals are happy wherever you put them! If only real life was so easy!");
          break;
        case NewGameButtonType.Seasons:
          this.ButtonText.Add("Seasons: Default");
          this.ButtonText.Add("Seasons: None");
          this.Desciptions.Add("Some animals will only mate during certain seasons, while others might struggle during hotter of colder months.");
          this.Desciptions.Add("All animals and events will happen regardless of the time of year.");
          break;
        case NewGameButtonType.ImportPreviousProgress:
          this.ButtonText.Add("Import Progress: Default");
          this.ButtonText.Add("Import Progress: True");
          this.Desciptions.Add("Start a brand new game from zero! No research complete, and no animal discoveries made!");
          this.Desciptions.Add("Instantly unlock anything you have discovered in previous playthroughs of the game.");
          break;
      }
    }

    private void SetText()
    {
      this.simpletextbox = new SimpleTextHandler(this.Desciptions[this.Index], false, 0.425f, GameFlags.GetSmallTextScale(), false, false);
      this.simpletextbox.paragraph.linemaker.SetAllColours(this.SecondaryColour);
      this.button = new TextButton(this.ButtonText[this.Index], 150f, OverAllMultiplier: 0.7f);
      if (this.Index != 0)
        this.button.SetButtonColour(BTNColour.PaleYellow);
      this.button.stringinabox.Frame.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.button.vLocation.X = -210f;
    }

    public string GetDescription() => this.Desciptions[this.Index];

    public bool UpdateNewSettingsOption(float DeltaTime, Player player, Vector2 Offset)
    {
      this.simpletextbox.UpdateSimpleTextHandler(DeltaTime);
      if (!this.button.UpdateTextButton(player, this.Location + Offset, DeltaTime))
        return false;
      ++this.Index;
      if (this.Index >= this.Desciptions.Count)
        this.Index = 0;
      this.SetText();
      return true;
    }

    public void DrawNewSettingsOption(Vector2 Offset)
    {
      if (!this.UseNewLayout)
        this.Framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + this.Location, new Vector2(800f, 80f));
      int numberoflines = this.simpletextbox.paragraph.Numberoflines;
      float num = 0.0f;
      switch (numberoflines)
      {
        case 2:
          num = -13f;
          break;
        case 3:
          num = -19f;
          break;
        case 4:
          num = -24f;
          break;
      }
      if (!this.UseNewLayout)
        this.simpletextbox.DrawSimpleTextHandler(Offset + this.Location + new Vector2(-30f, num * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.button.DrawTextButton(Offset + this.Location);
    }
  }
}

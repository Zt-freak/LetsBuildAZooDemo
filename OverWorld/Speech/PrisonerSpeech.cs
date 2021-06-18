// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Speech.PrisonerSpeech
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.OverWorld.Speech
{
  internal class PrisonerSpeech
  {
    internal static string GetSpeech(Enemy enemy, bool GetDrone)
    {
      if (!GetDrone)
      {
        switch (enemy.refperson.animaltype)
        {
          case AnimalType.Rabbit:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "meep";
              case 1:
                return "thump thump";
              case 2:
                return "hiccup";
              case 3:
                return "honk";
              case 4:
                return "squeek";
            }
            break;
          case AnimalType.Goose:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Squawk";
              case 1:
                return "Peck";
              case 2:
                return "Beep Beep";
              case 3:
                return "honk";
              case 4:
                return "Hiss";
            }
            break;
          case AnimalType.Capybara:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Bark";
              case 1:
                return "Cackle";
              case 2:
                return "Whistle";
              case 3:
                return "Whine";
              default:
                return "Grunt";
            }
          case AnimalType.Pig:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Snort";
              case 1:
                return "Grunt";
              case 2:
                return "Squeal";
              case 3:
                return "Sniff";
              default:
                return "Oink";
            }
          case AnimalType.Duck:
            switch (Game1.Rnd.Next(0, 8))
            {
              case 0:
                return "Honk";
              case 1:
                return "Squalk";
              case 2:
                return "Hoot";
              case 3:
                return "Hiss";
              case 4:
                return "Quack Quack";
              default:
                return "Quack";
            }
          case AnimalType.Snake:
            switch (Game1.Rnd.Next(0, 8))
            {
              case 0:
                return "Hiss";
              case 1:
                return "Growl";
              case 2:
                return "Shriek";
              case 3:
                return "Pop";
              default:
                return "Sssss";
            }
          case AnimalType.Badger:
            switch (Game1.Rnd.Next(0, 9))
            {
              case 0:
                return "Yelp";
              case 1:
                return "Chitter";
              case 2:
                return "Purr";
              case 3:
                return "Grunt";
              case 4:
                return "Snarl";
              case 5:
                return "Bark";
              case 6:
                return "Snort";
              default:
                return "Growl";
            }
          case AnimalType.Hyena:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Growl";
              case 1:
                return "Whoop";
              case 2:
                return "Giggle";
              case 3:
                return "Groan";
              case 4:
                return "Snarl";
              default:
                return "Laugh";
            }
          case AnimalType.Porcupine:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Shrill";
              case 1:
                return "Screech";
              case 2:
                return "Cough";
              case 3:
                return "Groan";
              case 4:
                return "Whine";
              default:
                return "Grunt";
            }
          case AnimalType.Bear:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Roar";
              case 1:
                return "Grumble";
              case 2:
                return "Huff";
              case 3:
                return "Groan";
              default:
                return "Growl";
            }
          case AnimalType.Meerkat:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Murmur";
              case 1:
                return "Bark";
              case 2:
                return "Shrill";
              case 3:
                return "Hiss";
              case 4:
                return "Growl";
              default:
                return "Purr";
            }
          case AnimalType.Horse:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Snort";
              case 1:
                return "Squeal";
              case 2:
                return "Nicker";
              case 3:
                return "Blow";
              case 4:
                return "Grunt";
              case 5:
                return "Sigh";
              default:
                return "Neigh";
            }
          case AnimalType.Armadillo:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Chirp";
              case 1:
                return "Screech";
              case 2:
                return "Shriek";
              case 3:
                return "Squeal";
              case 4:
                return "Scream";
              default:
                return "Grunt";
            }
          case AnimalType.Donkey:
            return Game1.Rnd.Next(0, 4) == 0 ? "Neigh" : "Hee-Haw";
          case AnimalType.Cow:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Grunt";
              case 1:
                return "Snort";
              case 2:
                return "Bellow";
              default:
                return "Moo";
            }
          case AnimalType.Tapir:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Hiccup";
              case 1:
                return "Screech";
              case 2:
                return "Squeal";
              default:
                return "Whistle";
            }
          case AnimalType.Ostrich:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Chirp";
              case 1:
                return "Bark";
              case 2:
                return "Hiss";
              case 3:
                return "Roar";
              case 4:
                return "Honk";
              case 5:
                return "Grunt";
              default:
                return "Hum";
            }
          case AnimalType.Tortoise:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Cluck";
              case 1:
                return "Hiss";
              default:
                return "Grunt";
            }
          case AnimalType.Chicken:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Cackle";
              case 1:
                return "Cheep";
              default:
                return "Cluck";
            }
          case AnimalType.Camel:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Grunt";
              case 1:
                return "Groan";
              case 2:
                return "Bellow";
              case 3:
                return "Roar";
              case 4:
                return "Moan";
              default:
                return "Bleat";
            }
          case AnimalType.Penguin:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Honk";
              case 1:
                return "Growl";
              case 2:
                return "Chirp";
              case 3:
                return "Chitter";
              case 4:
                return "Squeak";
              default:
                return "Squawk";
            }
          case AnimalType.Antelope:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Bellow";
              case 1:
                return "Grunt";
              case 2:
                return "Moan";
              case 3:
                return "Snort";
              default:
                return "Bleat";
            }
          case AnimalType.Panther:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Purr";
              case 1:
                return "Hiss";
              case 2:
                return "Moan";
              case 3:
                return "Growl";
              case 4:
                return "Yowl";
              default:
                return "Snarl";
            }
          case AnimalType.Seal:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Groan";
              case 1:
                return "Chug";
              case 2:
                return "Growl";
              case 3:
                return "Whistle";
              default:
                return "Bark";
            }
          case AnimalType.Wolf:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Bark";
              case 1:
                return "Whimper";
              case 2:
                return "Growl";
              default:
                return "Howl";
            }
          case AnimalType.Lemur:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Yell";
              case 1:
                return "Wail";
              case 2:
                return "Whistle";
              case 3:
                return "Purr";
              case 4:
                return "Moan";
              case 5:
                return "Chirp";
              default:
                return "Howl";
            }
          case AnimalType.Alpaca:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Purr";
              case 1:
                return "Bleat";
              default:
                return "Hum";
            }
          case AnimalType.KomodoDragon:
            Game1.Rnd.Next(0, 2);
            return "Hiss";
          case AnimalType.Walrus:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Growl";
              case 1:
                return "Grunt";
              case 2:
                return "Bark";
              case 3:
                return "Whistle";
              case 4:
                return "Rasps";
              case 5:
                return "Click";
              default:
                return "Tap";
            }
          case AnimalType.Orangutan:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Roar";
              case 1:
                return "Howl";
              case 2:
                return "Grumble";
              case 3:
                return "Grumph";
              case 4:
                return "Groan";
              default:
                return "Kiss Squeak";
            }
          case AnimalType.PolarBear:
            switch (Game1.Rnd.Next(0, 9))
            {
              case 0:
                return "Hiss";
              case 1:
                return "Roar";
              case 2:
                return "Chuff";
              case 3:
                return "Moan";
              case 4:
                return "Rumble";
              case 5:
                return "Whimper";
              default:
                return "Growl";
            }
          case AnimalType.Skunk:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Squeal";
              case 1:
                return "Growl";
              case 2:
                return "Coo";
              default:
                return "Hiss";
            }
          case AnimalType.Crocodile:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Hiss";
              case 1:
                return "Bellow";
              case 2:
                return "Snarl";
              default:
                return "Growl";
            }
          case AnimalType.WildBoar:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Squeal";
              case 1:
                return "Grunt";
              case 2:
                return "Huff";
              default:
                return "Growl";
            }
          case AnimalType.Peacock:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Hoot";
              case 1:
                return "Rattle";
              case 2:
                return "Squawk";
              default:
                return "Honk";
            }
          case AnimalType.Platypus:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Purr";
              case 1:
                return "Groan";
              case 2:
                return "Snort";
              default:
                return "Growl";
            }
          case AnimalType.Deer:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Bellow";
              case 1:
                return "Bleat";
              case 2:
                return "Wheeze";
              case 3:
                return "Snort";
              default:
                return "Grunt";
            }
          case AnimalType.Monkey:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Squeak";
              case 1:
                return "Chatter";
              case 2:
                return "Gibber";
              case 3:
                return "Whoop";
              case 4:
                return "Screech";
              default:
                return "Grunt";
            }
          case AnimalType.Flamingo:
            switch (Game1.Rnd.Next(0, 3))
            {
              case 0:
                return "Honk";
              case 1:
                return "Grunt";
              default:
                return "Growl";
            }
          case AnimalType.Gorilla:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Belch";
              case 1:
                return "Burp";
              case 2:
                return "Scream";
              case 3:
                return "Chuckle";
              case 4:
                return "Roar";
              default:
                return "Grunt";
            }
          case AnimalType.Tiger:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Grunt";
              case 1:
                return "Roar";
              case 2:
                return "Growl";
              case 3:
                return "Snarl";
              default:
                return "Chuff";
            }
          case AnimalType.Kangaroo:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Cough";
              case 1:
                return "Bark";
              case 2:
                return "Growl";
              case 3:
                return "Cluck";
              default:
                return "Chortle";
            }
          case AnimalType.Beavers:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Grunt";
              case 1:
                return "Grumble";
              case 2:
                return "Bark";
              case 3:
                return "Moan";
              default:
                return "Whine";
            }
          case AnimalType.RedPanda:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Squeal";
              case 1:
                return "Twitter";
              case 2:
                return "Huff";
              case 3:
                return "Snort";
              default:
                return "Whistle";
            }
          case AnimalType.Zebra:
            switch (Game1.Rnd.Next(0, 4))
            {
              case 0:
                return "Neigh";
              case 1:
                return "Bark";
              case 2:
                return "Snort";
              default:
                return "Nicker";
            }
          case AnimalType.Fox:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Scream";
              case 1:
                return "Bark";
              case 2:
                return "Howl";
              case 3:
                return "Chatter";
              default:
                return "Squeal";
            }
          case AnimalType.Raccoon:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Purr";
              case 1:
                return "Chitter";
              case 2:
                return "Growl";
              case 3:
                return "Snarl";
              case 4:
                return "Whimper";
              case 5:
                return "Screech";
              default:
                return "Hiss";
            }
          case AnimalType.Elephant:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Rumble";
              case 1:
                return "Snort";
              case 2:
                return "Grunt";
              default:
                return "Trumpet";
            }
          case AnimalType.Cheetah:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Growl";
              case 1:
                return "Purr";
              case 2:
                return "Chirp";
              case 3:
                return "Hiss";
              case 4:
                return "Moan";
              default:
                return "Yelp";
            }
          case AnimalType.Otter:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Squeal";
              case 1:
                return "Purr";
              case 2:
                return "Chirp";
              case 3:
                return "Hum";
              default:
                return "Gurgle";
            }
          case AnimalType.Owl:
            switch (Game1.Rnd.Next(0, 9))
            {
              case 0:
                return "Whistle";
              case 1:
                return "Shriek";
              case 2:
                return "Hiss";
              case 3:
                return "Coo";
              case 4:
                return "Scream";
              default:
                return "Hoot";
            }
          case AnimalType.Rhino:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Bellow";
              case 1:
                return "Huff";
              case 2:
                return "Snort";
              case 3:
                return "Squeal";
              case 4:
                return "Grunt";
              default:
                return "Growl";
            }
          case AnimalType.Panda:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Squeak";
              case 1:
                return "Huff";
              case 2:
                return "Bark";
              case 3:
                return "Honk";
              default:
                return "Growl";
            }
          case AnimalType.Giraffe:
            switch (Game1.Rnd.Next(0, 5))
            {
              case 0:
                return "Snort";
              case 1:
                return "Hum";
              case 2:
                return "Grunt";
              case 3:
                return "Low Hum";
            }
            break;
          case AnimalType.Hippopotamus:
            switch (Game1.Rnd.Next(0, 7))
            {
              case 0:
                return "Squeak";
              case 1:
                return "Grunt";
              case 2:
                return "Croak";
              case 3:
                return "Whine";
              default:
                return "Honk";
            }
          case AnimalType.Lion:
            switch (Game1.Rnd.Next(0, 6))
            {
              case 0:
                return "Purr";
              case 1:
                return "Grunt";
              case 2:
                return "Growl";
              case 3:
                return "Moan";
              default:
                return "Roar";
            }
        }
        return "meep";
      }
      Game1.Rnd.Next(0, 35);
      return SEngine.Localization.Localization.GetText(362);
    }
  }
}

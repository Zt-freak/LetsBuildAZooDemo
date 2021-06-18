// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Animal_Data.HybridNames
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_Animal_Data
{
  internal class HybridNames
  {
    internal static string GetAnimalCombinedName(AnimalType BODY, AnimalType HEAD)
    {
      switch (BODY)
      {
        case AnimalType.Rabbit:
          switch (HEAD)
          {
            case AnimalType.Armadillo:
              return "Rabbadillo";
            case AnimalType.Donkey:
              return "Rabbule";
            case AnimalType.KomodoDragon:
              return "RabbodoDragon";
            case AnimalType.RedPanda:
              return "RedRabbanda";
          }
          break;
        case AnimalType.Goose:
          switch (HEAD)
          {
            case AnimalType.Rabbit:
              return "Goobit";
            case AnimalType.Duck:
              return "Gooseduck";
            case AnimalType.Donkey:
              return "Goosule";
            case AnimalType.Ostrich:
              return "Goostrich";
            case AnimalType.KomodoDragon:
              return "GooseDragon";
            case AnimalType.Skunk:
              return "Gooskunk";
            case AnimalType.WildBoar:
              return "Gooboar";
            case AnimalType.Monkey:
              return "GooseMonkey";
            case AnimalType.RedPanda:
              return "RedGoosanda";
            case AnimalType.Giraffe:
              return "Gooraffe";
          }
          break;
        case AnimalType.Capybara:
          switch (HEAD)
          {
            case AnimalType.Pig:
              return "Cappig";
            case AnimalType.Hyena:
              return "Capyena";
            case AnimalType.Porcupine:
              return "Cappupine";
            case AnimalType.Meerkat:
              return "Capykat";
            case AnimalType.Donkey:
              return "Capybule";
            case AnimalType.Antelope:
              return "Capylope";
            case AnimalType.KomodoDragon:
              return "CapodoDragon";
            case AnimalType.Peacock:
              return "Capyfowl";
            case AnimalType.Platypus:
              return "Capypus";
            case AnimalType.Kangaroo:
              return "Caparoo";
            case AnimalType.RedPanda:
              return "RedCapybanda";
            case AnimalType.Zebra:
              return "Capybra";
            case AnimalType.Hippopotamus:
              return "Cappopotamus";
          }
          break;
        case AnimalType.Pig:
          switch (HEAD)
          {
            case AnimalType.Donkey:
              return "Pigule";
            case AnimalType.Penguin:
              return "Pigguin";
            case AnimalType.Crocodile:
              return "Pigodile";
            case AnimalType.RedPanda:
              return "RedPiganda";
          }
          break;
        case AnimalType.Duck:
          switch (HEAD)
          {
            case AnimalType.Meerkat:
              return "Duckkat";
            case AnimalType.Armadillo:
              return "Duckadillo";
            case AnimalType.Donkey:
              return "Duckule";
            case AnimalType.Chicken:
              return "Ducken";
            case AnimalType.Skunk:
              return "Dunk";
            case AnimalType.RedPanda:
              return "RedDuckanda";
          }
          break;
        case AnimalType.Snake:
          switch (HEAD)
          {
            case AnimalType.Badger:
              return "Snadger";
            case AnimalType.Donkey:
              return "Snakule";
            case AnimalType.Chicken:
              return "Snicken";
            case AnimalType.Panther:
              return "Snanther";
            case AnimalType.Seal:
              return "Sneal";
            case AnimalType.Alpaca:
              return "Snalpaca";
            case AnimalType.KomodoDragon:
              return "SnamodoDragon";
            case AnimalType.Walrus:
              return "Snalrus";
            case AnimalType.Skunk:
              return "Snunk";
            case AnimalType.Monkey:
              return "Snankey";
            case AnimalType.Beavers:
              return "Sneaver";
            case AnimalType.RedPanda:
              return "RedSnanda";
            case AnimalType.Zebra:
              return "Snebra";
            case AnimalType.Fox:
              return "Snox";
            case AnimalType.Cheetah:
              return "Sneetah";
            case AnimalType.Panda:
              return "Snanda";
          }
          break;
        case AnimalType.Badger:
          switch (HEAD)
          {
            case AnimalType.Donkey:
              return "Badule";
            case AnimalType.Chicken:
              return "Badgicken";
            case AnimalType.RedPanda:
              return "RedBadanda";
            case AnimalType.Giraffe:
              return "Badgeraffe";
          }
          break;
        case AnimalType.Hyena:
          switch (HEAD)
          {
            case AnimalType.Capybara:
              return "Hypybara";
            case AnimalType.Horse:
              return "Hyhorse";
            case AnimalType.Armadillo:
              return "Hyadillo";
            case AnimalType.Donkey:
              return "Hyenule";
            case AnimalType.Ostrich:
              return "Hystrich";
            case AnimalType.Chicken:
              return "Hycken";
            case AnimalType.Penguin:
              return "Hyenguin";
            case AnimalType.Antelope:
              return "Hyelope";
            case AnimalType.Panther:
              return "Hyenther";
            case AnimalType.Lemur:
              return "Hyemur";
            case AnimalType.KomodoDragon:
              return "HyenoDragon";
            case AnimalType.Orangutan:
              return "Hyangutan";
            case AnimalType.Crocodile:
              return "Hyodile";
            case AnimalType.Peacock:
              return "Hyfowl";
            case AnimalType.Tiger:
              return "Hyger";
            case AnimalType.RedPanda:
              return "RedHyenanda";
            case AnimalType.Elephant:
              return "Hyelephant";
            case AnimalType.Cheetah:
              return "Hyeetah";
          }
          break;
        case AnimalType.Porcupine:
          switch (HEAD)
          {
            case AnimalType.Donkey:
              return "Porcupule";
            case AnimalType.Antelope:
              return "Porculope";
            case AnimalType.KomodoDragon:
              return "PorcodoDragon";
            case AnimalType.Crocodile:
              return "Porcodile";
            case AnimalType.Peacock:
              return "Porcufowl";
            case AnimalType.Platypus:
              return "Porcupus";
            case AnimalType.Kangaroo:
              return "Porcaroo";
            case AnimalType.RedPanda:
              return "RedPorcupanda";
            case AnimalType.Elephant:
              return "Porcuphant";
            case AnimalType.Giraffe:
              return "Porcuraffe";
            case AnimalType.Hippopotamus:
              return "Porcupotamus";
          }
          break;
        case AnimalType.Bear:
          switch (HEAD)
          {
            case AnimalType.Meerkat:
              return "Bearkat";
            case AnimalType.Horse:
              return "Borse";
            case AnimalType.Donkey:
              return "Bearule";
            case AnimalType.Tortoise:
              return "Beartoise";
            case AnimalType.Penguin:
              return "Benguin";
            case AnimalType.Seal:
              return "Beal";
            case AnimalType.Walrus:
              return "Balrus";
            case AnimalType.PolarBear:
              return "PizzlyBear";
            case AnimalType.RedPanda:
              return "RedBearanda";
            case AnimalType.Giraffe:
              return "Bearaffe";
          }
          break;
        case AnimalType.Meerkat:
          switch (HEAD)
          {
            case AnimalType.Duck:
              return "Meerduck";
            case AnimalType.Donkey:
              return "Meerkule";
            case AnimalType.Ostrich:
              return "Meerstrich";
            case AnimalType.KomodoDragon:
              return "MeerkodoDragon";
            case AnimalType.Crocodile:
              return "Meerkodile";
            case AnimalType.Peacock:
              return "Meerfowl";
            case AnimalType.Monkey:
              return "Meerkey";
            case AnimalType.RedPanda:
              return "RedMeerkanda";
          }
          break;
        case AnimalType.Horse:
          switch (HEAD)
          {
            case AnimalType.Goose:
              return "Hoose";
            case AnimalType.Duck:
              return "Huck";
            case AnimalType.Donkey:
              return "Hinny";
            case AnimalType.Ostrich:
              return "Horstrich";
            case AnimalType.Tortoise:
              return "Hortoise";
            case AnimalType.KomodoDragon:
              return "HorsodoDragon";
            case AnimalType.Crocodile:
              return "Horsodile";
            case AnimalType.RedPanda:
              return "RedHorsanda";
          }
          break;
        case AnimalType.Armadillo:
          switch (HEAD)
          {
            case AnimalType.Capybara:
              return "Armybara";
            case AnimalType.Badger:
              return "Armadger";
            case AnimalType.Ostrich:
              return "Armastrich";
            case AnimalType.Chicken:
              return "Armicken";
            case AnimalType.KomodoDragon:
              return "ArmodoDragon";
            case AnimalType.Peacock:
              return "Armafowl";
            case AnimalType.Monkey:
              return "Armonkey";
            case AnimalType.RedPanda:
              return "RedArmadanda";
          }
          break;
        case AnimalType.Donkey:
          switch (HEAD)
          {
            case AnimalType.Duck:
              return "Donkeduck";
            case AnimalType.Horse:
              return "Hinny";
            case AnimalType.Armadillo:
              return "Donkadillo";
            case AnimalType.KomodoDragon:
              return "Donkeydo Dragon";
            case AnimalType.Crocodile:
              return "Donkodile";
            case AnimalType.RedPanda:
              return "RedDonkanda";
            case AnimalType.Zebra:
              return "Zorse";
          }
          break;
        case AnimalType.Cow:
          switch (HEAD)
          {
            case AnimalType.Porcupine:
              return "Cowcupine";
            case AnimalType.Armadillo:
              return "Cowadillo";
            case AnimalType.Ostrich:
              return "Costrich";
            case AnimalType.KomodoDragon:
              return "KomoodoDragon";
            case AnimalType.Crocodile:
              return "Cowodile";
            case AnimalType.Monkey:
              return "Cowmonkey";
            case AnimalType.RedPanda:
              return "RedCowanda";
            case AnimalType.Fox:
              return "Cowfox";
            case AnimalType.Otter:
              return "Cotter";
            case AnimalType.Owl:
              return "Cowl";
            case AnimalType.Giraffe:
              return "Cowaffe";
          }
          break;
        case AnimalType.Tapir:
          switch (HEAD)
          {
            case AnimalType.Donkey:
              return "Tapule";
            case AnimalType.KomodoDragon:
              return "TapodoDragon";
            case AnimalType.Deer:
              return "Tapideer";
            case AnimalType.RedPanda:
              return "RedTapanda";
          }
          break;
        case AnimalType.Ostrich:
          switch (HEAD)
          {
            case AnimalType.Donkey:
              return "Ostridonkey";
            case AnimalType.KomodoDragon:
              return "OstrodoDragon";
            case AnimalType.Peacock:
              return "Ostrifowl";
            case AnimalType.RedPanda:
              return "RedOstanda";
          }
          break;
        case AnimalType.Tortoise:
          switch (HEAD)
          {
            case AnimalType.Donkey:
              return "Tordonkey";
            case AnimalType.Ostrich:
              return "Tostrich";
            case AnimalType.KomodoDragon:
              return "TortodoDragon";
            case AnimalType.RedPanda:
              return "RedTortanda";
          }
          break;
        case AnimalType.Seal:
          if (HEAD == AnimalType.Lion)
            return "SeaLion";
          break;
        case AnimalType.Wolf:
          if (HEAD == AnimalType.Duck)
            return "Wolfduck";
          break;
        case AnimalType.PolarBear:
          if (HEAD == AnimalType.Bear)
            return "PizzlyBear";
          break;
        case AnimalType.Crocodile:
          if (HEAD == AnimalType.Chicken)
            return "Crochicken";
          break;
        case AnimalType.Peacock:
          if (HEAD == AnimalType.Duck)
            return "Peaduck";
          break;
        case AnimalType.RedPanda:
          if (HEAD == AnimalType.Chicken)
            return "RedPanicken";
          break;
        case AnimalType.Zebra:
          if (HEAD == AnimalType.Horse)
            return "Zorse";
          break;
        case AnimalType.Fox:
          if (HEAD == AnimalType.Wolf)
            return "Foxolf";
          break;
        case AnimalType.Raccoon:
          if (HEAD == AnimalType.Duck)
            return "Raccooduck";
          break;
        case AnimalType.Rhino:
          if (HEAD == AnimalType.Tiger)
            return "Rhiger";
          break;
        case AnimalType.Panda:
          if (HEAD == AnimalType.Chicken)
            return "Panicken";
          break;
        case AnimalType.Giraffe:
          if (HEAD == AnimalType.Duck)
            return "Giraffeduck";
          break;
        case AnimalType.Lion:
          if (HEAD == AnimalType.Tiger)
            return "Liger";
          break;
      }
      string str = "";
      for (int index = 0; index < 2; ++index)
      {
        AnimalType animalType = BODY;
        if (index == 1)
          animalType = HEAD;
        switch (animalType)
        {
          case AnimalType.Rabbit:
            str = index != 0 ? str + "it" : "Rabb";
            break;
          case AnimalType.Goose:
            str = index != 0 ? str + "oose" : "Goos";
            break;
          case AnimalType.Capybara:
            str = index != 0 ? str + "ybara" : "Capyb";
            break;
          case AnimalType.Pig:
            str = index != 0 ? str + "ig" : "Pig";
            break;
          case AnimalType.Duck:
            str = index != 0 ? str + "uck" : "Duck";
            break;
          case AnimalType.Snake:
            str = index != 0 ? str + "ake" : "Snak";
            break;
          case AnimalType.Badger:
            str = index != 0 ? str + "adger" : "Bad";
            break;
          case AnimalType.Hyena:
            str = index != 0 ? str + "ena" : "Hyen";
            break;
          case AnimalType.Porcupine:
            str = index != 0 ? str + "upine" : "Porcup";
            break;
          case AnimalType.Bear:
            str = index != 0 ? str + "ear" : "Bear";
            break;
          case AnimalType.Meerkat:
            str = index != 0 ? str + "eerkat" : "Meerk";
            break;
          case AnimalType.Horse:
            str = index != 0 ? str + "orse" : "Hors";
            break;
          case AnimalType.Armadillo:
            str = index != 0 ? str + "illo" : "Armad";
            break;
          case AnimalType.Donkey:
            str = index != 0 ? str + "onkey" : "Donk";
            break;
          case AnimalType.Cow:
            str = index != 0 ? str + "ow" : "Cow";
            break;
          case AnimalType.Tapir:
            str = index != 0 ? str + "apir" : "Tap";
            break;
          case AnimalType.Ostrich:
            str = index != 0 ? str + "rich" : "Ost";
            break;
          case AnimalType.Tortoise:
            str = index != 0 ? str + "ortoise" : "Tort";
            break;
          case AnimalType.Chicken:
            str = index != 0 ? str + "icken" : "Chick";
            break;
          case AnimalType.Camel:
            str = index != 0 ? str + "amel" : "Cam";
            break;
          case AnimalType.Penguin:
            str = index != 0 ? str + "enguin" : "Peng";
            break;
          case AnimalType.Antelope:
            str = index != 0 ? str + "elope" : "Ant";
            break;
          case AnimalType.Panther:
            str = index != 0 ? str + "anther" : "Panth";
            break;
          case AnimalType.Seal:
            str = index != 0 ? str + "eal" : "Seal";
            break;
          case AnimalType.Wolf:
            str = index != 0 ? str + "olf" : "Wolf";
            break;
          case AnimalType.Lemur:
            str = index != 0 ? str + "emur" : "Lem";
            break;
          case AnimalType.Alpaca:
            str = index != 0 ? str + "aca" : "Alp";
            break;
          case AnimalType.KomodoDragon:
            str = index != 0 ? str + "Dragon" : "Komodo";
            break;
          case AnimalType.Walrus:
            str = index != 0 ? str + "alrus" : "Wal";
            break;
          case AnimalType.Orangutan:
            str = index != 0 ? str + "angutan" : "Orangut";
            break;
          case AnimalType.PolarBear:
            str = index != 0 ? str + "iBear" : "Polar";
            break;
          case AnimalType.Skunk:
            str = index != 0 ? str + "unk" : "Skunk";
            break;
          case AnimalType.Crocodile:
            str = index != 0 ? str + "ocodile" : "Crocod";
            break;
          case AnimalType.WildBoar:
            str = index != 0 ? str + "oar" : "Boar";
            break;
          case AnimalType.Peacock:
            str = index != 0 ? str + "fowl" : "Peaf";
            break;
          case AnimalType.Platypus:
            str = index != 0 ? str + "ypus" : "Plat";
            break;
          case AnimalType.Deer:
            str = index != 0 ? str + "eer" : "Deer";
            break;
          case AnimalType.Monkey:
            str = index != 0 ? str + "onkey" : "Monk";
            break;
          case AnimalType.Flamingo:
            str = index != 0 ? str + "ingo" : "Flaming";
            break;
          case AnimalType.Gorilla:
            str = index != 0 ? str + "illa" : "Gorill";
            break;
          case AnimalType.Tiger:
            str = index != 0 ? str + "iger" : "Tig";
            break;
          case AnimalType.Kangaroo:
            str = index != 0 ? str + "aroo" : "Kang";
            break;
          case AnimalType.Beavers:
            str = index != 0 ? str + "eaver" : "Beav";
            break;
          case AnimalType.RedPanda:
            str = index != 0 ? str + "anda" : "RedPand";
            break;
          case AnimalType.Zebra:
            str = index != 0 ? str + "ebra" : "Zeb";
            break;
          case AnimalType.Fox:
            str = index != 0 ? str + "ox" : "Fox";
            break;
          case AnimalType.Raccoon:
            str = index != 0 ? str + "oon" : "Racc";
            break;
          case AnimalType.Elephant:
            str = index != 0 ? str + "ephant" : "Eleph";
            break;
          case AnimalType.Cheetah:
            str = index != 0 ? str + "eetah" : "Cheet";
            break;
          case AnimalType.Otter:
            str = index != 0 ? str + "otter" : "Ott";
            break;
          case AnimalType.Owl:
            str = index != 0 ? str + "owl" : "Owl";
            break;
          case AnimalType.Rhino:
            str = index != 0 ? str + "ino" : "Rhin";
            break;
          case AnimalType.Panda:
            str = index != 0 ? str + "anda" : "Pand";
            break;
          case AnimalType.Giraffe:
            str = index != 0 ? str + "iraffe" : "Giraff";
            break;
          case AnimalType.Hippopotamus:
            str = index != 0 ? str + "opotamus" : "Hippo";
            break;
          case AnimalType.Lion:
            str = index != 0 ? str + "ion" : "Lion";
            break;
        }
      }
      return str;
    }
  }
}

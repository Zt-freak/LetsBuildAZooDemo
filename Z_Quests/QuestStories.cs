// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.QuestStories
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_Quests
{
  internal class QuestStories
  {
    internal static string GetStory(
      QuestPack Ref_questpack,
      CityName CITY,
      Player player,
      out string CityHeading,
      out AnimalType TalkingHead,
      bool IsTradeCompleteText)
    {
      TalkingHead = AnimalType.None;
      CityHeading = "NO CITY NAME FOUND";
      List<string> stringList = new List<string>();
      TalkingHead = AnimalType.LondonZookeeper;
      if (Ref_questpack == null)
      {
        switch (CITY)
        {
          case CityName.Sydney:
            CityHeading = SEngine.Localization.Localization.GetText(925);
            TalkingHead = AnimalType.AustralianZookeeper;
            stringList.Add("Hey, I hope you are looking after those rabbits we gave you?~~If you need any more, try visiting the shelter.");
            break;
          case CityName.Africa:
            CityHeading = "Botswana";
            TalkingHead = AnimalType.AfricaZookeeper;
            stringList.Add("Did you know, there are many other places to go if you play the full version of Let's Build a Zoo? Am I breaking the 4th wall, you betcha! Anyways, I represent the last quest for the beta, and I hope you had fun. Hopefully I will see you again when the full game comes out!");
            break;
          case CityName.Tokyo:
            CityHeading = SEngine.Localization.Localization.GetText(930);
            TalkingHead = AnimalType.TokyoZookeeper;
            stringList.Add("Since we traded, our customers have been so happy with the new animals!");
            break;
          case CityName.Toronto:
            CityHeading = SEngine.Localization.Localization.GetText(917);
            TalkingHead = AnimalType.TorontoZookeeper;
            stringList.Add("Hi there!~ I think we both have pretty exciting zoos!");
            break;
          case CityName.Moscow:
            CityHeading = SEngine.Localization.Localization.GetText(909);
            TalkingHead = AnimalType.MoscowZookeeper;
            stringList.Add("Hi, nice to have you visiting us again!~It is very cold here though. If I were you, I would go somewhere warmer for my travels!");
            break;
          case CityName.Brazil:
            CityHeading = "Zoo Rio";
            TalkingHead = AnimalType.RioZookeeper;
            stringList.Add("We don't have anything new to trade right now");
            break;
        }
      }
      else if (Z_DebugFlags.IsBetaVersion)
      {
        switch (Ref_questpack.city)
        {
          case CityName.Sydney:
            CityHeading = SEngine.Localization.Localization.GetText(925);
            TalkingHead = AnimalType.AustralianZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Rabbit)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add(SEngine.Localization.Localization.GetText(926));
                break;
              }
              stringList.Add(SEngine.Localization.Localization.GetText(927));
              break;
            }
            break;
          case CityName.Africa:
            CityHeading = "Botswana";
            TalkingHead = AnimalType.AfricaZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Hippopotamus)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add("Hippos are powerful creatures. I heard from other zoos that you have proven yourself a mighty enough zookeeper to take care of them, and hence I will be willing to part with one for some capybaras!");
                break;
              }
              stringList.Add("Great! Our hippos will be in your care, I hope that they will bring much joy to you and your zoo visitors!");
              break;
            }
            break;
          case CityName.Tokyo:
            CityHeading = SEngine.Localization.Localization.GetText(930);
            TalkingHead = AnimalType.TokyoZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Capybara)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add(SEngine.Localization.Localization.GetText(931));
                break;
              }
              stringList.Add(SEngine.Localization.Localization.GetText(932));
              break;
            }
            break;
          case CityName.Toronto:
            CityHeading = SEngine.Localization.Localization.GetText(917);
            if (Ref_questpack.GetThisAnimal == AnimalType.Horse)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add("We love horses, and they love apples!");
                break;
              }
              stringList.Add("Horses can sleep standing up, imagine that? Well, here it's so cold, I wouldn't want to lie on the ground. Would you?");
              break;
            }
            break;
          case CityName.Moscow:
            CityHeading = SEngine.Localization.Localization.GetText(909);
            TalkingHead = AnimalType.MoscowZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Wolf)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add("We have too many wolves in our zoo right now. Would you be interested in them? In return, we would love to have some snake friends from the tropics here.");
                break;
              }
              stringList.Add("Take good care of these wolves! They are magnificent beasts that deserve the best.");
              break;
            }
            break;
          case CityName.Brazil:
            CityHeading = "Zoo Rio";
            TalkingHead = AnimalType.RioZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Snake)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add("So why don't people like snakes? To me, they constantly look like they are smiling! Anyways, we had an unfortunate incident here, and some people may have err...died. You want some snakes?");
                break;
              }
              stringList.Add("Lucky you, a pair of quite harmless but deadly poisonous snakes. You have nothing to fear apart from fear itself...Oh, and the deadly venom, best to keep an eye out for that.");
              break;
            }
            break;
        }
      }
      else
      {
        switch (Ref_questpack.city)
        {
          case CityName.Sydney:
            CityHeading = SEngine.Localization.Localization.GetText(925);
            TalkingHead = AnimalType.AustralianZookeeper;
            switch (Ref_questpack.GetThisAnimal)
            {
              case AnimalType.Rabbit:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(926));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(927));
                break;
              case AnimalType.Hyena:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(928));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(929));
                break;
              case AnimalType.Donkey:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("Hey there mate, great to see ya! So you wanna trade for a pair of donkies? Yay or Nay?");
                  break;
                }
                stringList.Add("So I hope you are happy with your new ass? I think people will come from miles around to look at it. Oh yeah, I gave you a pair of 'em? Well that's great, just look after 'em won't ya?");
                break;
              case AnimalType.Kangaroo:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("They are mighty and they are fighty! It's the good old ranga! Look, I dunno what you heard, but these animals are top fella's, I am not literally trading what us ausies consider pests for a pair of beautiful flamingos. Nah mate, it's all above board.");
                  break;
                }
                stringList.Add("The pride of Australia right there, oh wait, that'd be Ausie rules rugby, or maybe AC-DC, but yeah we love the ranga's. In a chart of things we love, they would be around errr, number 47... Probably.");
                break;
            }
            break;
          case CityName.London:
            CityHeading = SEngine.Localization.Localization.GetText(901);
            switch (Ref_questpack.GetThisAnimal)
            {
              case AnimalType.Goose:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(902));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(903));
                break;
              case AnimalType.Pig:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(904));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(905));
                break;
              case AnimalType.Badger:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(906));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(907));
                break;
              case AnimalType.Cow:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("I have the mightiest burger making device on the planet ready for trade. I am vegetarian, just like the burger machine known as the cow. Perhaps I would make a tasty steak too?");
                  break;
                }
                stringList.Add("Two wonderful cows are on their way to your zoo. Please look after them. They are gentle, friendly and intelligent, so try to think of them as more than burgers on legs.");
                break;
            }
            break;
          case CityName.Singapore:
            CityHeading = SEngine.Localization.Localization.GetText(908);
            TalkingHead = AnimalType.SingaporeZookeeper;
            break;
          case CityName.Tokyo:
            CityHeading = SEngine.Localization.Localization.GetText(930);
            TalkingHead = AnimalType.TokyoZookeeper;
            switch (Ref_questpack.GetThisAnimal)
            {
              case AnimalType.Capybara:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(931));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(932));
                break;
              case AnimalType.Snake:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(933));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(934));
                break;
            }
            break;
          case CityName.Toronto:
            CityHeading = SEngine.Localization.Localization.GetText(917);
            TalkingHead = AnimalType.TorontoZookeeper;
            switch (Ref_questpack.GetThisAnimal)
            {
              case AnimalType.Duck:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(918));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(919));
                break;
              case AnimalType.Horse:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("We love horses, and they love apples!");
                  break;
                }
                stringList.Add("Horses can sleep standing up, imagine that? Well, here it's so cold, I wouldn't want to lie on the ground. Would you?");
                break;
            }
            break;
          case CityName.Oakland:
            CityHeading = SEngine.Localization.Localization.GetText(922);
            TalkingHead = AnimalType.OaklandZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Bear)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add(SEngine.Localization.Localization.GetText(923));
                break;
              }
              stringList.Add(SEngine.Localization.Localization.GetText(924));
              break;
            }
            break;
          case CityName.Utah:
            CityHeading = SEngine.Localization.Localization.GetText(914);
            TalkingHead = AnimalType.UtahZookeeper;
            if (Ref_questpack.GetThisAnimal == AnimalType.Porcupine)
            {
              if (!IsTradeCompleteText)
              {
                stringList.Add(SEngine.Localization.Localization.GetText(915));
                break;
              }
              stringList.Add(SEngine.Localization.Localization.GetText(916));
              break;
            }
            break;
          case CityName.Moscow:
            CityHeading = SEngine.Localization.Localization.GetText(909);
            TalkingHead = AnimalType.MoscowZookeeper;
            switch (Ref_questpack.GetThisAnimal)
            {
              case AnimalType.Meerkat:
                if (!IsTradeCompleteText)
                {
                  stringList.Add(SEngine.Localization.Localization.GetText(910));
                  break;
                }
                stringList.Add(SEngine.Localization.Localization.GetText(911));
                break;
              case AnimalType.Horse:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("");
                  break;
                }
                stringList.Add("");
                break;
            }
            break;
          case CityName.Brazil:
            CityHeading = "Zoo Rio";
            TalkingHead = AnimalType.RioZookeeper;
            switch (Ref_questpack.GetThisAnimal)
            {
              case AnimalType.Snake:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("So why don't people like snakes? To me, they constantly look like they are smiling! Anyways, we had an unfortunate incident here, and some people may have err...died. You want some snakes?");
                  break;
                }
                stringList.Add("Lucky you, a pair of quite harmless but deadly poisonous snakes. You have nothing to fear apart from fear itself...Oh, and the deadly venom, best to keep an eye out for that.");
                break;
              case AnimalType.Armadillo:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("Olá! I have a few too many armadillos, and they really need rehousing! I have heard that your zoo would be a great new home! Can you trade with us?");
                  break;
                }
                stringList.Add("The armadillos are so excited! I can tell, cause I am an armadillo whisperer! I sense their thoughts, and they are thinking about their new life in your zoo!");
                break;
              case AnimalType.Chicken:
                if (!IsTradeCompleteText)
                {
                  stringList.Add("I told you these chickens were special right? Well they are, because they are the only chickens in the world that were traded for 5 horses! Probably not your best business decision! Anyway, I hope you like the chickens!");
                  break;
                }
                stringList.Add("The most underrated animal is the chicken! Sure, you can get a chicken from anywhere, but these chickens are special. Don't ask me why, just trade with us - You won't regret it.");
                break;
            }
            break;
        }
      }
      if (stringList.Count == 0)
        stringList.Add("Thanks for doing business with us! If you are looking for more animals, try taking a look in the rescue shelter!");
      return stringList[0];
    }
  }
}

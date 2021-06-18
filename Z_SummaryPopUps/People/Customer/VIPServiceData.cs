// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPServiceData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal static class VIPServiceData
  {
    public static string GetString(this VIPService service)
    {
      string str = "";
      switch (service)
      {
        case VIPService.FoodAndDrinks:
          str = "Free food and drinks";
          break;
        case VIPService.Alcohol:
          str = "Free-flow alcohol";
          break;
        case VIPService.Rides:
          str = "Free priority access to all rides";
          break;
        case VIPService.Souvenirs:
          str = "Free souvenirs";
          break;
        case VIPService.TourGuide:
          str = "Personal tour guide";
          break;
        case VIPService.BehindTheScenes:
          str = "Behind the scenes access";
          break;
        case VIPService.FeedTheAnimals:
          str = "Feed the animals";
          break;
      }
      return str;
    }
  }
}

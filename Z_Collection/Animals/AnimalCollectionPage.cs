// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.AnimalCollectionPage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Animals
{
  internal class AnimalCollectionPage
  {
    public Vector2 location;
    private List<ZGenericText> topTexts;
    private AnimalCollectionPageFrame frame;
    private CollectionType refCollectionType;
    private int refBuildingUID;

    public AnimalCollectionPage(
      CollectionType collectionType,
      Player player,
      float BaseScale,
      Vector2 forcedSize,
      int numberPerRow = -1,
      int buildingUID = -1)
    {
      this.refCollectionType = collectionType;
      this.refBuildingUID = buildingUID;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num1 = 0.0f;
      this.topTexts = new List<ZGenericText>();
      List<string> stringList = new List<string>();
      switch (collectionType)
      {
        case CollectionType.Animals:
          int totalSpeciesInGame;
          float num2 = (float) Math.Round((double) player.Stats.GetTotalSpeciesFound(out totalSpeciesInGame) / (double) totalSpeciesInGame * 100.0, 1);
          int num3 = 10 * totalSpeciesInGame;
          float num4 = (float) Math.Round((double) player.Stats.GetTotalVaiantsFound() / (double) num3 * 100.0, 1);
          stringList.Add(string.Format("Species Discovered: {0}%", (object) num2));
          stringList.Add(string.Format("Variants Discovered: {0}%", (object) num4));
          break;
        case CollectionType.EmployeesJobs:
          stringList.Add("Jobs Discovered: ???");
          stringList.Add("Employees Discovered: ???");
          break;
        case CollectionType.QuarantineAnimals:
          QuarantineBuilding quarantineBuilding = player.animalquarantine.GetThisQuarantineBuilding(buildingUID);
          stringList.Add(string.Format("Animals in Quarantine: {0}/{1}", (object) quarantineBuilding.GetListOfQuarantinedAnimals().Count, (object) AnimalQuarantine.MaxSlotsPerQuarantineBuidling));
          break;
        case CollectionType.Seeds:
          stringList.Add("Pick A crop to grow");
          stringList.Add("Crops Discovered: ???");
          break;
      }
      for (int index = 0; index < stringList.Count; ++index)
      {
        ZGenericText zgenericText1 = new ZGenericText(stringList[index], BaseScale, false, _UseOnePointFiveFont: true);
        ZGenericText zgenericText2 = zgenericText1;
        zgenericText2.vLocation = zgenericText2.vLocation + -forcedSize * 0.5f;
        zgenericText1.vLocation.Y += num1;
        num1 += zgenericText1.GetSize().Y;
        this.topTexts.Add(zgenericText1);
      }
      if (stringList.Count > 0)
        num1 += defaultYbuffer;
      this.frame = new AnimalCollectionPageFrame(collectionType, player, BaseScale, new Vector2(forcedSize.X, forcedSize.Y - num1), numberPerRow, buildingUID);
      this.frame.location.Y += (float) (-(double) forcedSize.Y * 0.5 + (double) this.frame.GetSize().Y * 0.5) + num1;
    }

    public void RefreshGridContents(Player player)
    {
      this.frame.RefreshGridContents(player);
      if (this.refCollectionType != CollectionType.QuarantineAnimals)
        return;
      this.topTexts[0].textToWrite = string.Format("Animals in Quarantine: {0}/{1}", (object) player.animalquarantine.GetThisQuarantineBuilding(this.refBuildingUID).GetListOfQuarantinedAnimals().Count, (object) AnimalQuarantine.MaxSlotsPerQuarantineBuidling);
    }

    public void UpdateAnimalCollectionPage(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ForceClosePanel)
    {
      offset += this.location;
      bool RefreshGridList = false;
      this.frame.UpdateAnimalCollectionPageFrame(player, DeltaTime, offset, out ForceClosePanel, out RefreshGridList);
      if (!RefreshGridList)
        return;
      this.RefreshGridContents(player);
    }

    public void DrawAnimalCollectionPage(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.topTexts.Count; ++index)
        this.topTexts[index].DrawZGenericText(offset, spriteBatch);
      this.frame.DrawAnimalCollectionPageFrame(offset, spriteBatch);
    }
  }
}

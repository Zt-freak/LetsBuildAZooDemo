// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.TrashAndStuff
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_Notification;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.PlayerDir
{
  internal class TrashAndStuff
  {
    public List<TrashEntry> trashentries;
    public int TotalVomit;
    public int TotalFoodTrash;
    public int TotalAnimalPoop;

    public TrashAndStuff() => this.trashentries = new List<TrashEntry>();

    public TrashEntry GetThis(TrashDrop trashdrop)
    {
      for (int index = this.trashentries.Count - 1; index > -1; --index)
      {
        if (this.trashentries[index].TileLocation.CompareMatches(trashdrop.TileLocation) && this.trashentries[index].trashtype == trashdrop.trashtype)
          return this.trashentries[index];
      }
      return (TrashEntry) null;
    }

    public void AddTrash(TrashDrop trashdrop, Player player)
    {
      this.trashentries.Add(new TrashEntry(trashdrop.TileLocation, trashdrop.trashtype, trashdrop.animal, trashdrop.PrisonUID));
      if (trashdrop.trashtype == TrashType.Vomit)
        ++this.TotalVomit;
      else if (trashdrop.trashtype == TrashType.AnimalPoop)
        ++this.TotalAnimalPoop;
      else
        ++this.TotalFoodTrash;
      Z_NotificationManager.ScrubTrash = true;
      player.worldhistory.DroppedTrash();
    }

    public void RemoveTrashOnNoPositions(TrashEntry trashentryfromsave)
    {
      for (int index = this.trashentries.Count - 1; index > -1; --index)
      {
        if (this.trashentries[index].TileLocation.CompareMatches(trashentryfromsave.TileLocation) && this.trashentries[index].trashtype == trashentryfromsave.trashtype)
        {
          if (this.trashentries[index].trashtype == TrashType.Vomit)
            --this.TotalVomit;
          else if (this.trashentries[index].trashtype == TrashType.AnimalPoop)
            --this.TotalAnimalPoop;
          else
            --this.TotalFoodTrash;
          Z_NotificationManager.ScrubTrash = true;
          this.trashentries.RemoveAt(index);
          return;
        }
      }
      throw new Exception("If cannot remove - then the game will crash - as this trash doesnt have a position");
    }

    public void RemoveTrash(TrashDrop trashdrop, Player player)
    {
      if (this.trashentries == null)
        throw new Exception("THE TRASH LIST IS NULL!");
      if (trashdrop == null)
        throw new Exception("trashdrop IS NULL!");
      for (int index = this.trashentries.Count - 1; index > -1; --index)
      {
        if (this.trashentries[index].TileLocation.CompareMatches(trashdrop.TileLocation) && this.trashentries[index].trashtype == trashdrop.trashtype)
        {
          if (this.trashentries[index].trashtype == TrashType.Vomit)
            --this.TotalVomit;
          else if (this.trashentries[index].trashtype == TrashType.AnimalPoop)
            --this.TotalAnimalPoop;
          else
            --this.TotalFoodTrash;
          this.trashentries.RemoveAt(index);
          player.worldhistory.TrashPickedUp();
          Z_NotificationManager.ScrubTrash = true;
          return;
        }
      }
      throw new Exception("THIS TRASH WASNT IN TEH PALYERS SAVE!");
    }

    public TrashAndStuff(Reader reader)
    {
      this.trashentries = new List<TrashEntry>();
      int _out = 0;
      int num = (int) reader.ReadInt("t", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        this.trashentries.Add(new TrashEntry(reader));
        if (this.trashentries[index].trashtype == TrashType.Vomit)
          ++this.TotalVomit;
        else if (this.trashentries[index].trashtype == TrashType.AnimalPoop)
          ++this.TotalAnimalPoop;
        else
          ++this.TotalFoodTrash;
      }
      Z_NotificationManager.ScrubTrash = true;
    }

    public void SaveTrashAndStuff(Writer writer)
    {
      writer.WriteInt("t", this.trashentries.Count);
      for (int index = 0; index < this.trashentries.Count; ++index)
        this.trashentries[index].SaveTrashEntry(writer);
    }
  }
}

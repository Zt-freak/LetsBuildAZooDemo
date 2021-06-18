// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.CurrentDiseaseList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current
{
  internal class CurrentDiseaseList
  {
    private List<DiseaseIcon> Diseases;
    public Vector2 Position;
    public int SelectedIndex;
    private CustomerFrame BGframe;

    public CurrentDiseaseList(Player player, float BaseScale, bool IsJournal = false)
    {
      this.Diseases = new List<DiseaseIcon>();
      int num1 = 0;
      int num2 = 0;
      int num3 = 3;
      if (IsJournal)
      {
        for (int index = 0; index < player.Stats.InnacitveDiseases.Count; ++index)
          this.Diseases.Add(new DiseaseIcon(BaseScale, player.Stats.InnacitveDiseases[index], player, IsJournal));
      }
      else
      {
        for (int index = 0; index < player.Stats.ActiveDiseases.Count; ++index)
          this.Diseases.Add(new DiseaseIcon(BaseScale, player.Stats.ActiveDiseases[index], player, IsJournal));
      }
      for (int count = this.Diseases.Count; count < num3; ++count)
        this.Diseases.Add(new DiseaseIcon(BaseScale, (TinyZoo.Z_Diseases.Disease) null, player, IsJournal));
      for (int index = 0; index < this.Diseases.Count; ++index)
      {
        this.Diseases[index].Position = new Vector2((this.Diseases[index].GetSize().X + BaseScale * 10f) * (float) num1, (this.Diseases[index].GetSize().Y + BaseScale * 10f) * (float) num2);
        this.Diseases[index].Position += this.Diseases[index].GetSize() * 0.5f;
        ++num1;
        if (num1 >= num3)
        {
          num1 = 0;
          ++num2;
        }
      }
      Vector2 position = this.Diseases[this.Diseases.Count - 1].Position;
      position.X = this.Diseases[num3 - 1].Position.X;
      position += this.Diseases[0].GetSize() * 0.5f;
      position += new Vector2(BaseScale * 20f, BaseScale * 20f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      for (int index = 0; index < this.Diseases.Count; ++index)
      {
        this.Diseases[index].Position -= position * 0.5f;
        this.Diseases[index].Position.X += 10f * BaseScale;
        this.Diseases[index].Position.Y += 10f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      this.BGframe = new CustomerFrame(position, true, BaseScale);
    }

    public Vector2 GetSize() => this.BGframe.VSCale;

    public TinyZoo.Z_Diseases.Disease GetSelectedDisease() => this.Diseases[this.SelectedIndex].Ref_Disease;

    public bool UpdateCurrentDiseaseList(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Position;
      for (int index = 0; index < this.Diseases.Count; ++index)
      {
        if (this.Diseases[index].UpdateDiseaseIcon(DeltaTime, player, Offset) && this.Diseases[index].Ref_Disease != null)
        {
          this.SelectedIndex = index;
          return true;
        }
      }
      return false;
    }

    public void DrawCurrentDiseaseList(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Position;
      this.BGframe.DrawCustomerFrame(Offset, spritebatch);
      for (int index = 0; index < this.Diseases.Count; ++index)
        this.Diseases[index].DrawDiseaseIcon(Offset, spritebatch);
    }
  }
}

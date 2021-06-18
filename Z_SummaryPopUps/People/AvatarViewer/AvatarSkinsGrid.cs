// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.AvatarViewer.AvatarSkinsGrid
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_CharacterSelect;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.AvatarViewer
{
  internal class AvatarSkinsGrid
  {
    public Vector2 location;
    private List<AvatarSkinGroup> genders;
    private SelectionFrame SelectionFrame_Controller;
    private Vector2 size;
    private int selectedSet;
    private int selectedIndexInSet;
    private WalkingPerson refWalkingPerson;

    public AvatarSkinsGrid(Player player, WalkingPerson walkingPerson, float BaseScale)
    {
      this.refWalkingPerson = walkingPerson;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.size = Vector2.Zero;
      this.genders = new List<AvatarSkinGroup>(2);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        bool flag = index1 == 1;
        List<AnimalType> forZookeeperAvatar = this.GetListOfAnimalTypesForZookeeperAvatar(flag);
        if (!flag)
          forZookeeperAvatar.Reverse();
        AvatarSkinGroup avatarSkinGroup = new AvatarSkinGroup(forZookeeperAvatar, flag, BaseScale);
        avatarSkinGroup.location = this.size;
        avatarSkinGroup.location.X += avatarSkinGroup.GetSize().X * 0.5f;
        this.size.X += avatarSkinGroup.GetSize().X;
        for (int index2 = 0; index2 < forZookeeperAvatar.Count; ++index2)
          avatarSkinGroup.SetIsActiveSkin(index2, forZookeeperAvatar[index2] == (AnimalType) player.Stats.ZooKeeperAvatarIndex);
        this.genders.Add(avatarSkinGroup);
        if (index1 == 0)
          this.size.X += defaultBuffer.X * 2f;
      }
      this.size.Y = this.genders[0].GetSize().Y;
      for (int index = 0; index < this.genders.Count; ++index)
        this.genders[index].location.X -= this.size.X * 0.5f;
    }

    public Vector2 GetSize() => this.size;

    private List<AnimalType> GetListOfAnimalTypesForZookeeperAvatar(bool GetFemale)
    {
      List<AnimalType> animalTypeList = new List<AnimalType>();
      if (GetFemale)
      {
        animalTypeList.Add(AnimalType.FemaleZookeeper);
        animalTypeList.Add(AnimalType.FemaleAsianZookeeper);
        animalTypeList.Add(AnimalType.FemaleDarkZookeeper);
      }
      else
      {
        animalTypeList.Add(AnimalType.MaleZookeeper);
        animalTypeList.Add(AnimalType.MaleAsianZookeeper);
        animalTypeList.Add(AnimalType.MaleDarkZookeeper);
      }
      return animalTypeList;
    }

    public bool UpdateAvatarSkinsGrid(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.genders.Count; ++index)
      {
        int num = this.genders[index].UpdateAvatarSkinGroup(player, DeltaTime, offset);
        if (num != -1)
        {
          this.selectedSet = index;
          this.selectedIndexInSet = num;
          this.OnConfirmSelect(player);
          return true;
        }
      }
      return false;
    }

    private void OnConfirmSelect(Player player)
    {
      int animalTypeOfThisIndex = (int) this.genders[this.selectedSet].GetAnimalTypeOfThisIndex(this.selectedIndexInSet);
      if (animalTypeOfThisIndex == player.Stats.ZooKeeperAvatarIndex)
        return;
      player.Stats.ZooKeeperAvatarIndex = animalTypeOfThisIndex;
      this.refWalkingPerson.OverwriteGraphicsAndPersonType((AnimalType) animalTypeOfThisIndex);
      this.refWalkingPerson.SetFrame();
      for (int index1 = 0; index1 < this.genders.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.genders[index1].GetNumberInThisGroup(); ++index2)
          this.genders[index1].SetIsActiveSkin(index2, index1 == this.selectedSet && index2 == this.selectedIndexInSet);
      }
    }

    public void DrawAvatarSkinsGrid(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.genders.Count; ++index)
        this.genders[index].DrawAvatarSkinGroup(offset, spriteBatch);
    }
  }
}

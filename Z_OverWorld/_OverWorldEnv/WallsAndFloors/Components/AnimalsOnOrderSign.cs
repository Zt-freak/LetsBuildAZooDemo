// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.AnimalsOnOrderSign
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components
{
  internal class AnimalsOnOrderSign : RenderComponent
  {
    private bool Active;
    private GameObject Sign;
    private GameObject SignHead;
    private AnimalOrder refAnimalOrder;
    public int PrisonUID;
    public Vector2Int Location;

    public AnimalsOnOrderSign(TileRenderer parent, int _PrisonUID)
      : base(parent, RenderComponentType.AnimalsOnOrderSign)
    {
      this.Location = new Vector2Int(parent.TileLocation);
      this.PrisonUID = _PrisonUID;
      this.Sign = new GameObject();
      this.Sign.DrawRect = new Rectangle(1264, 3050, 16, 36);
      this.Sign.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.Sign.DrawOrigin.Y -= 8f;
    }

    public void SetUpOrderStatus(Player player, int TotalGroups, int PrisonUID)
    {
      this.Active = (uint) TotalGroups > 0U;
      if (!this.Active)
        return;
      int num1 = 0;
      float num2 = 0.0f;
      float num3 = 0.0f;
      AnimalType animal = AnimalType.None;
      int Variant = 0;
      for (int index = 0; index < player.animalsonorder.animalsonorder.Count; ++index)
      {
        if (PrisonUID == player.animalsonorder.animalsonorder[index].PrisonUID)
        {
          float daysToArrival;
          float secondInDayOrArrival;
          if (num1 == 0)
          {
            daysToArrival = (float) player.animalsonorder.animalsonorder[index].DaysToArrival;
            animal = player.animalsonorder.animalsonorder[index].incominganaimals[0].animaltype;
            Variant = player.animalsonorder.animalsonorder[index].incominganaimals[0].CLIndex;
            secondInDayOrArrival = (float) player.animalsonorder.animalsonorder[index].SecondInDayOrArrival;
            this.refAnimalOrder = player.animalsonorder.animalsonorder[index];
            if (player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadType != AnimalType.None)
            {
              animal = player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadType;
              Variant = player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadVariant;
              break;
            }
            break;
          }
          if ((double) num2 < (double) player.animalsonorder.animalsonorder[index].DaysToArrival)
          {
            secondInDayOrArrival = (float) player.animalsonorder.animalsonorder[index].SecondInDayOrArrival;
            daysToArrival = (float) player.animalsonorder.animalsonorder[index].DaysToArrival;
            animal = player.animalsonorder.animalsonorder[index].incominganaimals[0].animaltype;
            Variant = player.animalsonorder.animalsonorder[index].incominganaimals[0].CLIndex;
            this.refAnimalOrder = player.animalsonorder.animalsonorder[index];
            if (player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadType != AnimalType.None)
            {
              animal = player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadType;
              Variant = player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadVariant;
              break;
            }
            break;
          }
          if ((double) num2 == (double) player.animalsonorder.animalsonorder[index].DaysToArrival && (double) num3 > (double) player.animalsonorder.animalsonorder[index].SecondInDayOrArrival)
          {
            secondInDayOrArrival = (float) player.animalsonorder.animalsonorder[index].SecondInDayOrArrival;
            daysToArrival = (float) player.animalsonorder.animalsonorder[index].DaysToArrival;
            animal = player.animalsonorder.animalsonorder[index].incominganaimals[0].animaltype;
            Variant = player.animalsonorder.animalsonorder[index].incominganaimals[0].CLIndex;
            this.refAnimalOrder = player.animalsonorder.animalsonorder[index];
            if (player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadType != AnimalType.None)
            {
              animal = player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadType;
              Variant = player.animalsonorder.animalsonorder[index].incominganaimals[0].HeadVariant;
              break;
            }
            break;
          }
          num1 += player.animalsonorder.animalsonorder[index].incominganaimals.Count;
        }
      }
      this.SignHead = new GameObject();
      this.SignHead.DrawRect = GeneData.GetHeadRect(animal, Variant);
      if (this.SignHead.DrawRect.Height > 10)
      {
        int num4 = this.SignHead.DrawRect.Height - 10;
        this.SignHead.DrawRect.Height = 10;
        this.SignHead.DrawRect.Y += num4 / 2;
      }
      this.SignHead.SetDrawOriginToCentre();
      this.SignHead.DrawOrigin.Y += 14f;
    }

    public override void DrawRenderComponent(
      TileRenderer parent,
      Texture2D drawWIthThis,
      SpriteBatch spritebatch,
      float ALphaMod,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale,
      bool IsTopLayer)
    {
      parent.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      parent.WorldOffsetDraw(spritebatch, drawWIthThis, 1f);
      if (!this.Active)
        return;
      this.Sign.vLocation = parent.vLocation;
      this.Sign.WorldOffsetDraw(spritebatch, AssetContainer.EnvironmentSheet, 1f);
      this.SignHead.vLocation = parent.vLocation;
      this.SignHead.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, 1f);
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(parent.vLocation);
      if (this.refAnimalOrder.InTransit)
      {
        TextFunctions.DrawJustifiedText(SEngine.Localization.Localization.GetText(977), Sengine.WorldOriginandScale.Z * 0.5f, screenSpace + new Vector2(0.0f, -5f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.WorldOriginandScale.Z), Color.White, 1f, AssetContainer.springFont, spritebatch);
        TextFunctions.DrawJustifiedText(SEngine.Localization.Localization.GetText(978), Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
      }
      else if (this.refAnimalOrder.DaysToArrival > 0)
      {
        TextFunctions.DrawJustifiedText(string.Concat((object) this.refAnimalOrder.DaysToArrival), Sengine.WorldOriginandScale.Z * 0.5f, screenSpace + new Vector2(0.0f, -5f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.WorldOriginandScale.Z), Color.White, 1f, AssetContainer.springFont, spritebatch);
        if (this.refAnimalOrder.DaysToArrival == 1)
          TextFunctions.DrawJustifiedText(" Day", Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
        else
          TextFunctions.DrawJustifiedText(" " + SEngine.Localization.Localization.GetText(975), Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
      }
      else
      {
        int untilThisInHours = Z_GameFlags.GetTimeUntilThisInHours((float) this.refAnimalOrder.SecondInDayOrArrival);
        if (untilThisInHours > 0)
        {
          TextFunctions.DrawJustifiedText(string.Concat((object) untilThisInHours), Sengine.WorldOriginandScale.Z * 0.5f, screenSpace + new Vector2(0.0f, -5f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.WorldOriginandScale.Z), Color.White, 1f, AssetContainer.springFont, spritebatch);
          if (untilThisInHours == 1)
            TextFunctions.DrawJustifiedText(" hr", Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
          else
            TextFunctions.DrawJustifiedText(" hrs", Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
        }
        else
        {
          int untilThisInMinutes = Z_GameFlags.GetTimeUntilThisInMinutes((float) this.refAnimalOrder.SecondInDayOrArrival);
          TextFunctions.DrawJustifiedText(string.Concat((object) untilThisInMinutes), Sengine.WorldOriginandScale.Z * 0.5f, screenSpace + new Vector2(0.0f, -5f * Sengine.ScreenRatioUpwardsMultiplier.Y * Sengine.WorldOriginandScale.Z), Color.White, 1f, AssetContainer.springFont, spritebatch);
          if (untilThisInMinutes == 1 || untilThisInMinutes == 0)
            TextFunctions.DrawJustifiedText(" min", Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
          else
            TextFunctions.DrawJustifiedText(" mins", Sengine.WorldOriginandScale.Z * 0.5f, screenSpace, Color.White, 1f, AssetContainer.springFont, spritebatch);
        }
      }
    }
  }
}

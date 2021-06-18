// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ActiveBreedPairManage.ReturnToPen.ReturnToPenmanager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_BreedScreen.ActiveBreedPairManage.ReturnToPen
{
  internal class ReturnToPenmanager
  {
    private TextButton button;
    public Vector2 Location;
    private PredictionTable predictiontable;

    public ReturnToPenmanager(Player player, float XScal, ActiveBreed breed, float BaseScale)
    {
      string CustomText = "You can remove your animals from this breeding slot by returning them to a pen.";
      this.predictiontable = new PredictionTable(PredictionTableType.ReturnToPen, player, breed, (PrisonerInfo) null, XScal, BaseScale, CustomText);
    }

    public float GetHeight() => this.predictiontable.customerframe.VSCale.Y;

    public bool UpdateReturnToPenmanager(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      return this.predictiontable.UpdatePredictionTable(Offset, player, DeltaTime);
    }

    public void DrawReturnToPenmanager(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.Location;
      this.predictiontable.DrawPredictionTable(Offset, spritebatch);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: TinyZoo.SwitchRandom.SwitchRandomData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo.SwitchRandom
{
  internal class SwitchRandomData
  {
    private static List<ListHolder> listholders;

    internal static int[] GetRandoms(int SeedIndex)
    {
      if (SwitchRandomData.listholders == null)
        SwitchRandomData.listholders = new List<ListHolder>();
      for (int index = 0; index < SwitchRandomData.listholders.Count; ++index)
      {
        if (SwitchRandomData.listholders[index].MainSeed == SeedIndex)
          return SwitchRandomData.listholders[index].listofnumbers;
      }
      switch (SeedIndex)
      {
        case 0:
          ListHolder listHolder1 = new ListHolder(SeedIndex);
          listHolder1.listofnumbers = new int[9];
          listHolder1.listofnumbers[0] = 167;
          listHolder1.listofnumbers[1] = 126;
          listHolder1.listofnumbers[2] = 3;
          listHolder1.listofnumbers[3] = 132;
          listHolder1.listofnumbers[4] = 45;
          listHolder1.listofnumbers[5] = 2;
          listHolder1.listofnumbers[6] = 204;
          listHolder1.listofnumbers[7] = 76;
          listHolder1.listofnumbers[8] = 3;
          SwitchRandomData.listholders.Add(listHolder1);
          return listHolder1.listofnumbers;
        case 1:
          ListHolder listHolder2 = new ListHolder(SeedIndex);
          listHolder2.listofnumbers = new int[9];
          listHolder2.listofnumbers[0] = 79;
          listHolder2.listofnumbers[1] = 32;
          listHolder2.listofnumbers[2] = 1;
          listHolder2.listofnumbers[3] = 213;
          listHolder2.listofnumbers[4] = 105;
          listHolder2.listofnumbers[5] = 1;
          listHolder2.listofnumbers[6] = 106;
          listHolder2.listofnumbers[7] = 143;
          listHolder2.listofnumbers[8] = 0;
          SwitchRandomData.listholders.Add(listHolder2);
          return listHolder2.listofnumbers;
        case 3:
          ListHolder listHolder3 = new ListHolder(SeedIndex);
          listHolder3.listofnumbers = new int[9];
          listHolder3.listofnumbers[0] = 44;
          listHolder3.listofnumbers[1] = 215;
          listHolder3.listofnumbers[2] = 3;
          listHolder3.listofnumbers[3] = 35;
          listHolder3.listofnumbers[4] = 176;
          listHolder3.listofnumbers[5] = 0;
          listHolder3.listofnumbers[6] = 40;
          listHolder3.listofnumbers[7] = 286;
          listHolder3.listofnumbers[8] = 1;
          SwitchRandomData.listholders.Add(listHolder3);
          return listHolder3.listofnumbers;
        case 4:
          ListHolder listHolder4 = new ListHolder(SeedIndex);
          listHolder4.listofnumbers = new int[12];
          listHolder4.listofnumbers[0] = 185;
          listHolder4.listofnumbers[1] = 149;
          listHolder4.listofnumbers[2] = 2;
          listHolder4.listofnumbers[3] = 101;
          listHolder4.listofnumbers[4] = 19;
          listHolder4.listofnumbers[5] = 0;
          listHolder4.listofnumbers[6] = 161;
          listHolder4.listofnumbers[7] = 77;
          listHolder4.listofnumbers[8] = 1;
          listHolder4.listofnumbers[9] = 171;
          listHolder4.listofnumbers[10] = 49;
          listHolder4.listofnumbers[11] = 2;
          SwitchRandomData.listholders.Add(listHolder4);
          return listHolder4.listofnumbers;
        case 5:
          ListHolder listHolder5 = new ListHolder(SeedIndex);
          listHolder5.listofnumbers = new int[12];
          listHolder5.listofnumbers[0] = 70;
          listHolder5.listofnumbers[1] = 92;
          listHolder5.listofnumbers[2] = 1;
          listHolder5.listofnumbers[3] = 116;
          listHolder5.listofnumbers[4] = 138;
          listHolder5.listofnumbers[5] = 3;
          listHolder5.listofnumbers[6] = 39;
          listHolder5.listofnumbers[7] = 266;
          listHolder5.listofnumbers[8] = 2;
          listHolder5.listofnumbers[9] = 34;
          listHolder5.listofnumbers[10] = 272;
          listHolder5.listofnumbers[11] = 1;
          SwitchRandomData.listholders.Add(listHolder5);
          return listHolder5.listofnumbers;
        case 6:
          ListHolder listHolder6 = new ListHolder(SeedIndex);
          listHolder6.listofnumbers = new int[15];
          listHolder6.listofnumbers[0] = 195;
          listHolder6.listofnumbers[1] = 107;
          listHolder6.listofnumbers[2] = 3;
          listHolder6.listofnumbers[3] = 190;
          listHolder6.listofnumbers[4] = 159;
          listHolder6.listofnumbers[5] = 3;
          listHolder6.listofnumbers[6] = 139;
          listHolder6.listofnumbers[7] = 88;
          listHolder6.listofnumbers[8] = 2;
          listHolder6.listofnumbers[9] = 117;
          listHolder6.listofnumbers[10] = 128;
          listHolder6.listofnumbers[11] = 0;
          listHolder6.listofnumbers[12] = 173;
          listHolder6.listofnumbers[13] = 109;
          listHolder6.listofnumbers[14] = 0;
          SwitchRandomData.listholders.Add(listHolder6);
          return listHolder6.listofnumbers;
        case 8:
          ListHolder listHolder7 = new ListHolder(SeedIndex);
          listHolder7.listofnumbers = new int[12];
          listHolder7.listofnumbers[0] = 291;
          listHolder7.listofnumbers[1] = 39;
          listHolder7.listofnumbers[2] = 1;
          listHolder7.listofnumbers[3] = 96;
          listHolder7.listofnumbers[4] = 126;
          listHolder7.listofnumbers[5] = 2;
          listHolder7.listofnumbers[6] = 165;
          listHolder7.listofnumbers[7] = 78;
          listHolder7.listofnumbers[8] = 3;
          listHolder7.listofnumbers[9] = 84;
          listHolder7.listofnumbers[10] = 42;
          listHolder7.listofnumbers[11] = 2;
          SwitchRandomData.listholders.Add(listHolder7);
          return listHolder7.listofnumbers;
        case 10:
          ListHolder listHolder8 = new ListHolder(SeedIndex);
          listHolder8.listofnumbers = new int[12];
          listHolder8.listofnumbers[0] = 213;
          listHolder8.listofnumbers[1] = 166;
          listHolder8.listofnumbers[2] = 3;
          listHolder8.listofnumbers[3] = 160;
          listHolder8.listofnumbers[4] = 160;
          listHolder8.listofnumbers[5] = 1;
          listHolder8.listofnumbers[6] = 96;
          listHolder8.listofnumbers[7] = 108;
          listHolder8.listofnumbers[8] = 0;
          listHolder8.listofnumbers[9] = 215;
          listHolder8.listofnumbers[10] = 147;
          listHolder8.listofnumbers[11] = 1;
          SwitchRandomData.listholders.Add(listHolder8);
          return listHolder8.listofnumbers;
        case 11:
          ListHolder listHolder9 = new ListHolder(SeedIndex);
          listHolder9.listofnumbers = new int[12];
          listHolder9.listofnumbers[0] = 68;
          listHolder9.listofnumbers[1] = 28;
          listHolder9.listofnumbers[2] = 1;
          listHolder9.listofnumbers[3] = 117;
          listHolder9.listofnumbers[4] = 59;
          listHolder9.listofnumbers[5] = 0;
          listHolder9.listofnumbers[6] = 109;
          listHolder9.listofnumbers[7] = 248;
          listHolder9.listofnumbers[8] = 1;
          listHolder9.listofnumbers[9] = 52;
          listHolder9.listofnumbers[10] = 113;
          listHolder9.listofnumbers[11] = 0;
          SwitchRandomData.listholders.Add(listHolder9);
          return listHolder9.listofnumbers;
        case 13:
          ListHolder listHolder10 = new ListHolder(SeedIndex);
          listHolder10.listofnumbers = new int[12];
          listHolder10.listofnumbers[0] = 148;
          listHolder10.listofnumbers[1] = 61;
          listHolder10.listofnumbers[2] = 3;
          listHolder10.listofnumbers[3] = 101;
          listHolder10.listofnumbers[4] = 23;
          listHolder10.listofnumbers[5] = 3;
          listHolder10.listofnumbers[6] = 203;
          listHolder10.listofnumbers[7] = 84;
          listHolder10.listofnumbers[8] = 2;
          listHolder10.listofnumbers[9] = 33;
          listHolder10.listofnumbers[10] = 77;
          listHolder10.listofnumbers[11] = 2;
          SwitchRandomData.listholders.Add(listHolder10);
          return listHolder10.listofnumbers;
        case 15:
          ListHolder listHolder11 = new ListHolder(SeedIndex);
          listHolder11.listofnumbers = new int[15];
          listHolder11.listofnumbers[0] = 115;
          listHolder11.listofnumbers[1] = 51;
          listHolder11.listofnumbers[2] = 1;
          listHolder11.listofnumbers[3] = 149;
          listHolder11.listofnumbers[4] = 169;
          listHolder11.listofnumbers[5] = 2;
          listHolder11.listofnumbers[6] = 126;
          listHolder11.listofnumbers[7] = 167;
          listHolder11.listofnumbers[8] = 3;
          listHolder11.listofnumbers[9] = 157;
          listHolder11.listofnumbers[10] = 71;
          listHolder11.listofnumbers[11] = 0;
          listHolder11.listofnumbers[12] = 182;
          listHolder11.listofnumbers[13] = 60;
          listHolder11.listofnumbers[14] = 1;
          SwitchRandomData.listholders.Add(listHolder11);
          return listHolder11.listofnumbers;
        case 16:
          ListHolder listHolder12 = new ListHolder(SeedIndex);
          listHolder12.listofnumbers = new int[24];
          listHolder12.listofnumbers[0] = 39;
          listHolder12.listofnumbers[1] = 206;
          listHolder12.listofnumbers[2] = 3;
          listHolder12.listofnumbers[3] = 280;
          listHolder12.listofnumbers[4] = 176;
          listHolder12.listofnumbers[5] = 2;
          listHolder12.listofnumbers[6] = 36;
          listHolder12.listofnumbers[7] = 190;
          listHolder12.listofnumbers[8] = 3;
          listHolder12.listofnumbers[9] = 63;
          listHolder12.listofnumbers[10] = 47;
          listHolder12.listofnumbers[11] = 3;
          listHolder12.listofnumbers[12] = 187;
          listHolder12.listofnumbers[13] = 310;
          listHolder12.listofnumbers[14] = 0;
          listHolder12.listofnumbers[15] = 22;
          listHolder12.listofnumbers[16] = 234;
          listHolder12.listofnumbers[17] = 3;
          listHolder12.listofnumbers[18] = 46;
          listHolder12.listofnumbers[19] = 205;
          listHolder12.listofnumbers[20] = 3;
          listHolder12.listofnumbers[21] = 226;
          listHolder12.listofnumbers[22] = 274;
          listHolder12.listofnumbers[23] = 2;
          SwitchRandomData.listholders.Add(listHolder12);
          return listHolder12.listofnumbers;
        case 17:
          ListHolder listHolder13 = new ListHolder(SeedIndex);
          listHolder13.listofnumbers = new int[15];
          listHolder13.listofnumbers[0] = 54;
          listHolder13.listofnumbers[1] = 349;
          listHolder13.listofnumbers[2] = 2;
          listHolder13.listofnumbers[3] = 27;
          listHolder13.listofnumbers[4] = 380;
          listHolder13.listofnumbers[5] = 1;
          listHolder13.listofnumbers[6] = 49;
          listHolder13.listofnumbers[7] = 417;
          listHolder13.listofnumbers[8] = 0;
          listHolder13.listofnumbers[9] = 50;
          listHolder13.listofnumbers[10] = 353;
          listHolder13.listofnumbers[11] = 2;
          listHolder13.listofnumbers[12] = 36;
          listHolder13.listofnumbers[13] = 146;
          listHolder13.listofnumbers[14] = 3;
          SwitchRandomData.listholders.Add(listHolder13);
          return listHolder13.listofnumbers;
        case 19:
          ListHolder listHolder14 = new ListHolder(SeedIndex);
          listHolder14.listofnumbers = new int[18];
          listHolder14.listofnumbers[0] = 109;
          listHolder14.listofnumbers[1] = 61;
          listHolder14.listofnumbers[2] = 0;
          listHolder14.listofnumbers[3] = 104;
          listHolder14.listofnumbers[4] = 105;
          listHolder14.listofnumbers[5] = 0;
          listHolder14.listofnumbers[6] = 76;
          listHolder14.listofnumbers[7] = (int) sbyte.MaxValue;
          listHolder14.listofnumbers[8] = 1;
          listHolder14.listofnumbers[9] = 56;
          listHolder14.listofnumbers[10] = 50;
          listHolder14.listofnumbers[11] = 1;
          listHolder14.listofnumbers[12] = 115;
          listHolder14.listofnumbers[13] = 57;
          listHolder14.listofnumbers[14] = 1;
          listHolder14.listofnumbers[15] = 145;
          listHolder14.listofnumbers[16] = 113;
          listHolder14.listofnumbers[17] = 3;
          SwitchRandomData.listholders.Add(listHolder14);
          return listHolder14.listofnumbers;
        case 20:
          ListHolder listHolder15 = new ListHolder(SeedIndex);
          listHolder15.listofnumbers = new int[21];
          listHolder15.listofnumbers[0] = 43;
          listHolder15.listofnumbers[1] = 168;
          listHolder15.listofnumbers[2] = 2;
          listHolder15.listofnumbers[3] = 148;
          listHolder15.listofnumbers[4] = 69;
          listHolder15.listofnumbers[5] = 0;
          listHolder15.listofnumbers[6] = 154;
          listHolder15.listofnumbers[7] = 122;
          listHolder15.listofnumbers[8] = 1;
          listHolder15.listofnumbers[9] = 119;
          listHolder15.listofnumbers[10] = 23;
          listHolder15.listofnumbers[11] = 0;
          listHolder15.listofnumbers[12] = 77;
          listHolder15.listofnumbers[13] = 209;
          listHolder15.listofnumbers[14] = 3;
          listHolder15.listofnumbers[15] = 99;
          listHolder15.listofnumbers[16] = 77;
          listHolder15.listofnumbers[17] = 1;
          listHolder15.listofnumbers[18] = 50;
          listHolder15.listofnumbers[19] = 30;
          listHolder15.listofnumbers[20] = 1;
          SwitchRandomData.listholders.Add(listHolder15);
          return listHolder15.listofnumbers;
        case 21:
          ListHolder listHolder16 = new ListHolder(SeedIndex);
          listHolder16.listofnumbers = new int[18];
          listHolder16.listofnumbers[0] = 161;
          listHolder16.listofnumbers[1] = 148;
          listHolder16.listofnumbers[2] = 1;
          listHolder16.listofnumbers[3] = 24;
          listHolder16.listofnumbers[4] = 109;
          listHolder16.listofnumbers[5] = 3;
          listHolder16.listofnumbers[6] = 81;
          listHolder16.listofnumbers[7] = 148;
          listHolder16.listofnumbers[8] = 2;
          listHolder16.listofnumbers[9] = 19;
          listHolder16.listofnumbers[10] = 119;
          listHolder16.listofnumbers[11] = 3;
          listHolder16.listofnumbers[12] = 30;
          listHolder16.listofnumbers[13] = 70;
          listHolder16.listofnumbers[14] = 2;
          listHolder16.listofnumbers[15] = 46;
          listHolder16.listofnumbers[16] = 109;
          listHolder16.listofnumbers[17] = 0;
          SwitchRandomData.listholders.Add(listHolder16);
          return listHolder16.listofnumbers;
        case 22:
          ListHolder listHolder17 = new ListHolder(SeedIndex);
          listHolder17.listofnumbers = new int[21];
          listHolder17.listofnumbers[0] = 82;
          listHolder17.listofnumbers[1] = 130;
          listHolder17.listofnumbers[2] = 0;
          listHolder17.listofnumbers[3] = 93;
          listHolder17.listofnumbers[4] = 75;
          listHolder17.listofnumbers[5] = 3;
          listHolder17.listofnumbers[6] = 248;
          listHolder17.listofnumbers[7] = 214;
          listHolder17.listofnumbers[8] = 2;
          listHolder17.listofnumbers[9] = 133;
          listHolder17.listofnumbers[10] = 223;
          listHolder17.listofnumbers[11] = 2;
          listHolder17.listofnumbers[12] = 246;
          listHolder17.listofnumbers[13] = 394;
          listHolder17.listofnumbers[14] = 1;
          listHolder17.listofnumbers[15] = 250;
          listHolder17.listofnumbers[16] = 63;
          listHolder17.listofnumbers[17] = 2;
          listHolder17.listofnumbers[18] = 98;
          listHolder17.listofnumbers[19] = 360;
          listHolder17.listofnumbers[20] = 2;
          SwitchRandomData.listholders.Add(listHolder17);
          return listHolder17.listofnumbers;
        case 23:
          ListHolder listHolder18 = new ListHolder(SeedIndex);
          listHolder18.listofnumbers = new int[9];
          listHolder18.listofnumbers[0] = 39;
          listHolder18.listofnumbers[1] = 45;
          listHolder18.listofnumbers[2] = 3;
          listHolder18.listofnumbers[3] = 30;
          listHolder18.listofnumbers[4] = 46;
          listHolder18.listofnumbers[5] = 2;
          listHolder18.listofnumbers[6] = 22;
          listHolder18.listofnumbers[7] = 65;
          listHolder18.listofnumbers[8] = 3;
          SwitchRandomData.listholders.Add(listHolder18);
          return listHolder18.listofnumbers;
        case 24:
          ListHolder listHolder19 = new ListHolder(SeedIndex);
          listHolder19.listofnumbers = new int[33];
          listHolder19.listofnumbers[0] = 54;
          listHolder19.listofnumbers[1] = 187;
          listHolder19.listofnumbers[2] = 2;
          listHolder19.listofnumbers[3] = 114;
          listHolder19.listofnumbers[4] = 26;
          listHolder19.listofnumbers[5] = 2;
          listHolder19.listofnumbers[6] = 110;
          listHolder19.listofnumbers[7] = 113;
          listHolder19.listofnumbers[8] = 3;
          listHolder19.listofnumbers[9] = 33;
          listHolder19.listofnumbers[10] = 209;
          listHolder19.listofnumbers[11] = 0;
          listHolder19.listofnumbers[12] = 34;
          listHolder19.listofnumbers[13] = 206;
          listHolder19.listofnumbers[14] = 3;
          listHolder19.listofnumbers[15] = 18;
          listHolder19.listofnumbers[16] = 205;
          listHolder19.listofnumbers[17] = 3;
          listHolder19.listofnumbers[18] = 63;
          listHolder19.listofnumbers[19] = 137;
          listHolder19.listofnumbers[20] = 0;
          listHolder19.listofnumbers[21] = 122;
          listHolder19.listofnumbers[22] = 27;
          listHolder19.listofnumbers[23] = 1;
          listHolder19.listofnumbers[24] = 23;
          listHolder19.listofnumbers[25] = 207;
          listHolder19.listofnumbers[26] = 2;
          listHolder19.listofnumbers[27] = 96;
          listHolder19.listofnumbers[28] = 52;
          listHolder19.listofnumbers[29] = 1;
          listHolder19.listofnumbers[30] = 22;
          listHolder19.listofnumbers[31] = 170;
          listHolder19.listofnumbers[32] = 3;
          return listHolder19.listofnumbers;
        case 25:
          ListHolder listHolder20 = new ListHolder(SeedIndex);
          listHolder20.listofnumbers = new int[21];
          listHolder20.listofnumbers[0] = 204;
          listHolder20.listofnumbers[1] = 38;
          listHolder20.listofnumbers[2] = 0;
          listHolder20.listofnumbers[3] = 230;
          listHolder20.listofnumbers[4] = 83;
          listHolder20.listofnumbers[5] = 1;
          listHolder20.listofnumbers[6] = 41;
          listHolder20.listofnumbers[7] = 148;
          listHolder20.listofnumbers[8] = 0;
          listHolder20.listofnumbers[9] = 134;
          listHolder20.listofnumbers[10] = 112;
          listHolder20.listofnumbers[11] = 3;
          listHolder20.listofnumbers[12] = 212;
          listHolder20.listofnumbers[13] = 81;
          listHolder20.listofnumbers[14] = 1;
          listHolder20.listofnumbers[15] = 170;
          listHolder20.listofnumbers[16] = 67;
          listHolder20.listofnumbers[17] = 1;
          listHolder20.listofnumbers[18] = 100;
          listHolder20.listofnumbers[19] = 150;
          listHolder20.listofnumbers[20] = 0;
          SwitchRandomData.listholders.Add(listHolder20);
          return listHolder20.listofnumbers;
        case 26:
          ListHolder listHolder21 = new ListHolder(SeedIndex);
          listHolder21.listofnumbers = new int[30];
          listHolder21.listofnumbers[0] = 100;
          listHolder21.listofnumbers[1] = 48;
          listHolder21.listofnumbers[2] = 3;
          listHolder21.listofnumbers[3] = 45;
          listHolder21.listofnumbers[4] = 83;
          listHolder21.listofnumbers[5] = 1;
          listHolder21.listofnumbers[6] = 167;
          listHolder21.listofnumbers[7] = 51;
          listHolder21.listofnumbers[8] = 0;
          listHolder21.listofnumbers[9] = 250;
          listHolder21.listofnumbers[10] = 48;
          listHolder21.listofnumbers[11] = 3;
          listHolder21.listofnumbers[12] = 153;
          listHolder21.listofnumbers[13] = 86;
          listHolder21.listofnumbers[14] = 0;
          listHolder21.listofnumbers[15] = 88;
          listHolder21.listofnumbers[16] = 72;
          listHolder21.listofnumbers[17] = 0;
          listHolder21.listofnumbers[18] = 119;
          listHolder21.listofnumbers[19] = 44;
          listHolder21.listofnumbers[20] = 1;
          listHolder21.listofnumbers[21] = 77;
          listHolder21.listofnumbers[22] = 44;
          listHolder21.listofnumbers[23] = 0;
          listHolder21.listofnumbers[24] = 83;
          listHolder21.listofnumbers[25] = 35;
          listHolder21.listofnumbers[26] = 1;
          listHolder21.listofnumbers[27] = 232;
          listHolder21.listofnumbers[28] = 68;
          listHolder21.listofnumbers[29] = 0;
          SwitchRandomData.listholders.Add(listHolder21);
          return listHolder21.listofnumbers;
        case 28:
          ListHolder listHolder22 = new ListHolder(SeedIndex);
          listHolder22.listofnumbers = new int[3];
          listHolder22.listofnumbers[0] = 100;
          listHolder22.listofnumbers[1] = 22;
          listHolder22.listofnumbers[2] = 1;
          SwitchRandomData.listholders.Add(listHolder22);
          return listHolder22.listofnumbers;
        case 29:
          ListHolder listHolder23 = new ListHolder(SeedIndex);
          listHolder23.listofnumbers = new int[30];
          listHolder23.listofnumbers[0] = 198;
          listHolder23.listofnumbers[1] = 61;
          listHolder23.listofnumbers[2] = 0;
          listHolder23.listofnumbers[3] = 171;
          listHolder23.listofnumbers[4] = 57;
          listHolder23.listofnumbers[5] = 3;
          listHolder23.listofnumbers[6] = 203;
          listHolder23.listofnumbers[7] = 149;
          listHolder23.listofnumbers[8] = 2;
          listHolder23.listofnumbers[9] = 217;
          listHolder23.listofnumbers[10] = 105;
          listHolder23.listofnumbers[11] = 0;
          listHolder23.listofnumbers[12] = 134;
          listHolder23.listofnumbers[13] = 92;
          listHolder23.listofnumbers[14] = 1;
          listHolder23.listofnumbers[15] = 45;
          listHolder23.listofnumbers[16] = 25;
          listHolder23.listofnumbers[17] = 3;
          listHolder23.listofnumbers[18] = 112;
          listHolder23.listofnumbers[19] = 90;
          listHolder23.listofnumbers[20] = 2;
          listHolder23.listofnumbers[21] = 216;
          listHolder23.listofnumbers[22] = 136;
          listHolder23.listofnumbers[23] = 0;
          listHolder23.listofnumbers[24] = 128;
          listHolder23.listofnumbers[25] = 41;
          listHolder23.listofnumbers[26] = 3;
          listHolder23.listofnumbers[27] = 152;
          listHolder23.listofnumbers[28] = 92;
          listHolder23.listofnumbers[29] = 1;
          SwitchRandomData.listholders.Add(listHolder23);
          return listHolder23.listofnumbers;
        case 239:
          ListHolder listHolder24 = new ListHolder(SeedIndex);
          listHolder24.listofnumbers = new int[15];
          listHolder24.listofnumbers[0] = 194;
          listHolder24.listofnumbers[1] = 143;
          listHolder24.listofnumbers[2] = 3;
          listHolder24.listofnumbers[3] = 189;
          listHolder24.listofnumbers[4] = 32;
          listHolder24.listofnumbers[5] = 1;
          listHolder24.listofnumbers[6] = 317;
          listHolder24.listofnumbers[7] = 63;
          listHolder24.listofnumbers[8] = 2;
          listHolder24.listofnumbers[9] = 139;
          listHolder24.listofnumbers[10] = 67;
          listHolder24.listofnumbers[11] = 0;
          listHolder24.listofnumbers[12] = 299;
          listHolder24.listofnumbers[13] = 125;
          listHolder24.listofnumbers[14] = 1;
          SwitchRandomData.listholders.Add(listHolder24);
          return listHolder24.listofnumbers;
        case 890:
          ListHolder listHolder25 = new ListHolder(SeedIndex);
          listHolder25.listofnumbers = new int[12];
          listHolder25.listofnumbers[0] = 59;
          listHolder25.listofnumbers[1] = 248;
          listHolder25.listofnumbers[2] = 3;
          listHolder25.listofnumbers[3] = 49;
          listHolder25.listofnumbers[4] = 24;
          listHolder25.listofnumbers[5] = 1;
          listHolder25.listofnumbers[6] = 59;
          listHolder25.listofnumbers[7] = 243;
          listHolder25.listofnumbers[8] = 0;
          listHolder25.listofnumbers[9] = 45;
          listHolder25.listofnumbers[10] = 253;
          listHolder25.listofnumbers[11] = 1;
          SwitchRandomData.listholders.Add(listHolder25);
          return listHolder25.listofnumbers;
        case 1238:
          ListHolder listHolder26 = new ListHolder(SeedIndex);
          listHolder26.listofnumbers = new int[9];
          listHolder26.listofnumbers[0] = 117;
          listHolder26.listofnumbers[1] = 28;
          listHolder26.listofnumbers[2] = 0;
          listHolder26.listofnumbers[3] = 182;
          listHolder26.listofnumbers[4] = 40;
          listHolder26.listofnumbers[5] = 1;
          listHolder26.listofnumbers[6] = 140;
          listHolder26.listofnumbers[7] = 99;
          listHolder26.listofnumbers[8] = 0;
          SwitchRandomData.listholders.Add(listHolder26);
          return listHolder26.listofnumbers;
        case 2352:
          ListHolder listHolder27 = new ListHolder(SeedIndex);
          listHolder27.listofnumbers = new int[6];
          listHolder27.listofnumbers[0] = 76;
          listHolder27.listofnumbers[1] = 183;
          listHolder27.listofnumbers[2] = 3;
          listHolder27.listofnumbers[3] = 89;
          listHolder27.listofnumbers[4] = 35;
          listHolder27.listofnumbers[5] = 3;
          SwitchRandomData.listholders.Add(listHolder27);
          return listHolder27.listofnumbers;
        case 8474:
          ListHolder listHolder28 = new ListHolder(SeedIndex);
          listHolder28.listofnumbers = new int[9];
          listHolder28.listofnumbers[0] = 222;
          listHolder28.listofnumbers[1] = 50;
          listHolder28.listofnumbers[2] = 3;
          listHolder28.listofnumbers[3] = 90;
          listHolder28.listofnumbers[4] = 30;
          listHolder28.listofnumbers[5] = 3;
          listHolder28.listofnumbers[6] = 216;
          listHolder28.listofnumbers[7] = 113;
          listHolder28.listofnumbers[8] = 1;
          SwitchRandomData.listholders.Add(listHolder28);
          return listHolder28.listofnumbers;
        case 12263:
          ListHolder listHolder29 = new ListHolder(SeedIndex);
          listHolder29.listofnumbers = new int[9];
          listHolder29.listofnumbers[0] = 37;
          listHolder29.listofnumbers[1] = 19;
          listHolder29.listofnumbers[2] = 1;
          listHolder29.listofnumbers[3] = 16;
          listHolder29.listofnumbers[4] = 121;
          listHolder29.listofnumbers[5] = 0;
          listHolder29.listofnumbers[6] = 56;
          listHolder29.listofnumbers[7] = 97;
          listHolder29.listofnumbers[8] = 0;
          SwitchRandomData.listholders.Add(listHolder29);
          return listHolder29.listofnumbers;
        case 12321:
          ListHolder listHolder30 = new ListHolder(SeedIndex);
          listHolder30.listofnumbers = new int[15];
          listHolder30.listofnumbers[0] = 49;
          listHolder30.listofnumbers[1] = 28;
          listHolder30.listofnumbers[2] = 3;
          listHolder30.listofnumbers[3] = 40;
          listHolder30.listofnumbers[4] = 371;
          listHolder30.listofnumbers[5] = 3;
          listHolder30.listofnumbers[6] = 42;
          listHolder30.listofnumbers[7] = 274;
          listHolder30.listofnumbers[8] = 1;
          listHolder30.listofnumbers[9] = 57;
          listHolder30.listofnumbers[10] = 219;
          listHolder30.listofnumbers[11] = 2;
          listHolder30.listofnumbers[12] = 61;
          listHolder30.listofnumbers[13] = 214;
          listHolder30.listofnumbers[14] = 1;
          SwitchRandomData.listholders.Add(listHolder30);
          return listHolder30.listofnumbers;
        default:
          return (int[]) null;
      }
    }
  }
}

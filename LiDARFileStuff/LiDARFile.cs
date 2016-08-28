using System;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Linq;

namespace LiDARFileStuff
{
    public static class PointDataRecordFormatFactory
    {
        public static PointDataRecordFormatBase Get(int type, BinaryReader br)
        {
            switch (type)
            {
                case 4:
                    return new PointDataRecordFormat4(br);
                case 5:
                    return new PointDataRecordFormat5(br);
                default:
                    throw new Exception(string.Format("Record format not supported: {0}", type));
            }
        }
    }
    public class WDPFile
    {

        BinaryReader br;
        public string FileName
        {
            get; set;
        }

        int[] Data
        {
            get; set;
        }
        public WDPFile(string fName)
        {
            FileName = fName;
            br = new BinaryReader(File.Open(fName, FileMode.Open, FileAccess.Read));
        }

        public void GetNextRec(long FWFOffset, int recordSize)
        {
            br.BaseStream.Position = FWFOffset;
            Data = new int[recordSize / 2];              // Record size is in bytes and data recs are integers
            for (int i = 0; i < (recordSize / 2); i++)
                Data[i] = br.ReadInt16();
        }

        public string GetRecStr(int recordSize, char separator = ' ')
        {
            string ret = string.Empty;
            for (int i = 0; i < Data.Length; i++)
                ret += Data[i].ToString() + separator;
            return ret.Trim(separator)+Environment.NewLine;
        }

        public void Close()
        {
            br.Close();
        }
    }

    public class PointDataRecordFormatBase
    {
        protected BinaryReader br;

        #region Properties
        public static double XScaleFactor
        {
            get; set;
        }

        public static double YScaleFactor
        {
            get; set;
        }

        public static double ZScaleFactor
        {
            get; set;
        }

        public static double XOffset
        {
            get; set;
        }

        public static double YOffset
        {
            get; set;
        }

        public static double ZOffset
        {
            get; set;
        }

        public int X
        {
            get; set;
        }
        public int Y
        {
            get; set;
        }
        public int Z
        {
            get; set;
        }
        public ushort Intensity
        {
            get; set;
        }
        public byte bitMask
        {
            get; set;
        }

        public int ReturnNumber
        {
            get; set;
        }

        public int NumberOfReturns
        {
            get; set;
        }

        public int ScanDirectionFlag
        {
            get; set;
        }

        public int EdgeOfFligtLine
        {
            get; set;
        }

        public byte Classification
        {
            get; set;
        }

        public byte ScanAngleRank
        {
            get; set;
        }

        public byte UserData
        {
            get; set;
        }

        public ushort PointSourceID
        {
            get; set;
        }
        public double GPSTime
        {
            get; set;
        }

        public byte WavePackageDescriptorIndex
        {
            get; set;
        }

        public UInt64 Offset2WFData
        {
            get; set;
        }

        public static UInt32 WaveFormPacketSize
        {
            get; set;
        }

        public float ReturnPointWaveFormLocation
        {
            get; set;
        }
        public float Xt
        {
            get; set;
        }
        public float Yt
        {
            get; set;
        }
        public float Zt
        {
            get; set;
        }

        public double AdjustedX
        {
            get
            {
                return (X * XScaleFactor) + XOffset;
            }
        }
        public double AdjustedY
        {
            get
            {
                return (Y * YScaleFactor) + YOffset;
            }
        }
        public double AdjustedZ
        {
            get
            {
                return (Z * ZScaleFactor) + ZOffset;
            }
        }

        public virtual string DataAsString
        {
            get
            {
                //                     X              Y              Z              Int.    RN      NR     SDF       EFL     CLF     SAR    URD      PSID        GPS TIME             WPDI    Ofs. WF Data WF Pk. Size Ret.Point WF Loc.
                return string.Format("{0,16:0.00000} {1,16:0.00000} {2,16:0.00000} {3:0000} {4:00} {5:00} {6:0}     {7:0}   {8:0000} {9:000} {10:0000} {11:000}  {12:#.000000000000} {13,3:#} {14,10:#0} {15,10:#0} {16,10:#.#} {17:0.000E+0} {18:0.000E+0} {19:0.000E+0}",
                    AdjustedX, AdjustedY, AdjustedZ,
                    Intensity, ReturnNumber, NumberOfReturns,
                    ScanDirectionFlag, EdgeOfFligtLine, Classification, ScanAngleRank, UserData, PointSourceID,
                    GPSTime, WavePackageDescriptorIndex,
                    Offset2WFData, PointDataRecordFormatBase.WaveFormPacketSize, ReturnPointWaveFormLocation, Xt, Yt, Zt);
            }
        }
        #endregion
        public void ReadFieldsFormatBase()
        {
            X = br.ReadInt32();
            Y = br.ReadInt32();
            Z = br.ReadInt32();
            Intensity = br.ReadUInt16();
            bitMask = br.ReadByte();
            ReturnNumber = bitMask & 0x07;             // 00000111
            NumberOfReturns = (bitMask & 0x38) >> 3;   // 00111000
            ScanDirectionFlag = (bitMask & 0x40) >> 6; // 01000000
            EdgeOfFligtLine = (bitMask & 0x80) >> 7;   // 01000000
            Classification = br.ReadByte();
            ScanAngleRank = br.ReadByte();
            UserData = br.ReadByte();
            PointSourceID = br.ReadUInt16();
            GPSTime = br.ReadDouble();
        }

        public virtual void GetFieldValues()
        {
            ReadFieldsFormatBase();
        }
        public PointDataRecordFormatBase(BinaryReader br)
        {
            this.br = br;
        }

        // Adjust coordinate values by scale and offset
        public double AdjCoordinate(long ScaleFactor, long Offset)
        {
            return (X * ScaleFactor) + Offset;
        }
    }
    public class PointDataRecordFormat4 : PointDataRecordFormatBase
    {
        public void ReadFieldsFormat4()
        {
            WavePackageDescriptorIndex = br.ReadByte();
            Offset2WFData = br.ReadUInt64();
            WaveFormPacketSize = br.ReadUInt32();
            ReturnPointWaveFormLocation = br.ReadSingle();
            Xt = br.ReadSingle();
            Yt = br.ReadSingle();
            Yt = br.ReadSingle();
        }
        public PointDataRecordFormat4(BinaryReader br)
            : base(br)
        {
        }

        public override void GetFieldValues()
        {
            base.GetFieldValues();
            ReadFieldsFormat4();
        }
    }
    public class PointDataRecordFormat5 : PointDataRecordFormat4
    {
        #region Properties
        public ushort Red
        {
            get; set;
        }

        public ushort Green
        {
            get; set;
        }

        public ushort Blue
        {
            get; set;
        }
        #endregion
        public override string DataAsString
        {
            get
            {
                return base.DataAsString + string.Format(" {0:000} {1:000} {2:000}", Red, Green, Blue);
            }
        }

        public void ReadFieldsFormat5()
        {
            Red = br.ReadUInt16();
            Green = br.ReadUInt16();
            Blue = br.ReadUInt16();
        }
        public PointDataRecordFormat5(BinaryReader br)
            :base(br)
        {
        }

        public override void GetFieldValues()
        {
            ReadFieldsFormatBase();
            ReadFieldsFormat5();
            ReadFieldsFormat4();
        }
    }
    public class VariableLengthRecordHeader
    {
        #region properties
        public ushort Reserved
        {
            get; set;
        }
        public string UserID
        {
            get; set;
        }
        public ushort RecordID
        {
            get; set;
        }
        public ushort RecordLengthAfterHeader
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }

        public byte[] DataAfterHeader
        {
            get; set;
        }
        #endregion
        public VariableLengthRecordHeader(BinaryReader br)
        {
            Reserved = br.ReadUInt16();
            UserID = new string(Encoding.ASCII.GetString(br.ReadBytes(16)).Where(c => (c >= 32) && (c <= 175)).ToArray());
            RecordID = br.ReadUInt16();
            RecordLengthAfterHeader = br.ReadUInt16();
            Description = new string(Encoding.ASCII.GetString(br.ReadBytes(32)).Where(c => (c >= 32) && (c <= 175)).ToArray());
            DataAfterHeader = br.ReadBytes(RecordLengthAfterHeader);
        }

    }
    public class LiDARFile
    {
        BinaryReader br;
        uint WFPackageSize = 0;
        #region Properties
        public bool Modified
        {
            get; set;
        }
        public string LASFileName
        {
            get; set;
        }
        public string FileSignature
        {
            get; set;
        }
        public ushort FileSourceID
        {
            get; set;
        }
        public ushort GlobalEncoding
        {
            get; set;
        }
        public byte[] GUIDArr
        {
            get; set;
        }
        public byte VersionMajor
        {
            get; set;
        }
        public byte VersionMinor
        {
            get; set;
        }
        public string SystemIdentifier
        {
            get; set;
        }
        public string GeneratingSoftware
        {
            get; set;
        }
        public ushort FileCreationDayOfYear
        {
            get; set;
        }
        public ushort FileCreationYear
        {
            get; set;
        }
        public ushort HeaderSize
        {
            get; set;
        }
        public uint OffsetPointData
        {
            get; set;
        }
        public uint NumberOfVariableLengthRecords
        {
            get; set;
        }
        public static byte PointDataRecordFormat
        {
            get; set;
        }
        public ushort PointDataRecordLength
        {
            get; set;
        }
        public uint LegacyNumberOfPointRecords
        {
            get; set;
        }
        public uint[] LegacyNumberOfPointbyReturn 
        {
            get; set;
        }
        public double XScaleFactor
        {
            get; set;
        }
        public double YScaleFactor
        {
            get; set;
        }
        public double ZScaleFactor
        {
            get; set;
        }
        public double XOffset
        {
            get; set;
        }
        public double YOffset
        {
            get; set;
        }
        public double ZOffset
        {
            get; set;
        }
        public double MinX
        {
            get; set;
        }
        public double MaxX
        {
            get; set;
        }
        public double MinY
        {
            get; set;
        }
        public double MaxY
        {
            get; set;
        }
        public double MinZ
        {
            get; set;
        }
        public double MaxZ
        {
            get; set;
        }

        public UInt64 StartOfWaveFormDataPacketRecord
        {
            get; set;
        }
        public string Version
        {
            get
            {
                return string.Format("{0}.{1}", VersionMajor, VersionMinor);
            }
        }

        public DateTime FileCreationDateTime
        {
            get
            {
                return new DateTime(FileCreationYear, 1, 1).AddDays(FileCreationDayOfYear - 1);
            }
        }

        string guid;
        public string GUID
        {
            get
            {
                return new Guid(GUIDArr).ToString();
            }
            set
            {
                guid = value;
//                Guid g = new Guid(value);
                GUIDArr = new Guid(value).ToByteArray();
            }
        }

        public string WDPFileName
        {
            get
            {
                return string.Format(@"{0}\{1}.{2}",
                    Path.GetDirectoryName(LASFileName),
                    Path.GetFileNameWithoutExtension(LASFileName), "wdp");
            }
        }

        #region GlobalDecoded
        public int[] GlobalDecoded
        {
            get
            {
                int[] gd = new int[] { 0,0,0,0};

                if (VersionMinor < 3)
                    return gd;
                //else
                gd[0] = GlobalEncoding & 1; /* GPS Time Type:  The meaning of GPS Time in the Point Records
                                                                0(not set)->GPS time in the point record fields is
                                                                GPS Week Time (the same as previous versions
                                                                of LAS)
                                                                1(set)->GPS Time is standard GPS Time
                                                                (satellite GPS Time) minus 1 x 109
                                                                (Adjusted Standard GPS Time).The offset moves the time
                                                                back to near zero to improve floating point
                                                                resolution.
                                             */

                gd[1] = (GlobalEncoding & 2) >> 1; /* Waveform Data
                                                       Pckts Internal: If this bit is set, the waveform data packets are
                                                                       located within this file (note that this bit is mutually
                                                                       exclusive with bit 2)
                                             */

                gd[2] = (GlobalEncoding & 4) >> 2; /* Waveform Data
                                                       Pckts External: If this bit is set, the waveform data packets are
                                                                       located external to this file in an auxiliary file with
                                                                       the same base name as this file and the extension
                                                                        “.wdp”. (note that this bit is mutually exclusive
                                                                       with bit 1)
                                             */

                gd[3] = (GlobalEncoding & 8) >> 3; /* Return numbers
                                                       have been
                                                       synthetically
                                                       generated:      If set, the point return numbers in the Point Data
                                                                       Records have been synthetically generated. This
                                                                       could be the case, for example, when a composite
                                                                       file is created by combining a First Return File and
                                                                       a Last Return File. In this case, first return data
                                                                       will be labeled “1 of 2” and second return data will
                                                                       be labeled “2 of 2”
                                             */
                return gd;
            }

        }

        public bool HasFWFData
        {
            get
            {
                int[] gd = GlobalDecoded;
                return (gd[1] == 1) || (gd[2] == 1);
            }
        }
        #endregion
        #endregion

        #region Methods
        public LiDARFile(string fName)
        {
            LASFileName = fName;
            br = new BinaryReader(File.Open(LASFileName, FileMode.Open,FileAccess.ReadWrite));
            brBraseStream = br.BaseStream;
            FileSignature = Encoding.ASCII.GetString(br.ReadBytes(4));
            FileSourceID = br.ReadUInt16();
            GlobalEncoding = br.ReadUInt16();
            GUIDArr = new byte[16];
            GUIDArr = br.ReadBytes(16);
            VersionMajor = br.ReadByte();
            VersionMinor = br.ReadByte();
            SystemIdentifier = Encoding.ASCII.GetString(br.ReadBytes(32)).Trim('\0');
            GeneratingSoftware = Encoding.ASCII.GetString(br.ReadBytes(32)).Trim('\0');
            FileCreationDayOfYear = br.ReadUInt16();
            FileCreationYear = br.ReadUInt16();
            HeaderSize = br.ReadUInt16();
            OffsetPointData = br.ReadUInt32();
            NumberOfVariableLengthRecords = br.ReadUInt32();
            PointDataRecordFormat = br.ReadByte();
            PointDataRecordLength = br.ReadUInt16();
            LegacyNumberOfPointRecords = br.ReadUInt32();

            LegacyNumberOfPointbyReturn = new uint[5];
            for (int i = 0; i < 5; i++)
                LegacyNumberOfPointbyReturn[i] = br.ReadUInt32();
            XScaleFactor = br.ReadDouble();
            YScaleFactor = br.ReadDouble();
            ZScaleFactor = br.ReadDouble();
            XOffset = br.ReadDouble();
            YOffset = br.ReadDouble();
            ZOffset = br.ReadDouble();
            MaxX = br.ReadDouble();
            MinX = br.ReadDouble();
            MaxY = br.ReadDouble();
            MinY = br.ReadDouble();
            MaxZ = br.ReadDouble();
            MinZ = br.ReadDouble();
            if (VersionMinor > 2)
                StartOfWaveFormDataPacketRecord = br.ReadUInt64();
            Modified = false;
        }

        public void Close()
        {
            br.Close();
            if (Modified)
            {
                SaveChanges();
                Modified = false;
            }
        }

        private void SaveChanges()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(LASFileName, FileMode.Open, FileAccess.ReadWrite));
            Stream bwBaseStream = bw.BaseStream;
            bw.Write(Encoding.ASCII.GetBytes(FileSignature));
            bw.Write(FileSourceID);
            bw.Write(GlobalEncoding);
            bw.Write(GUIDArr);
            bw.Write(VersionMajor);
            bw.Write(VersionMinor);
            bw.Write(Encoding.ASCII.GetBytes(SystemIdentifier.PadRight(32, '\0')));
            bw.Write(Encoding.ASCII.GetBytes(GeneratingSoftware.PadRight(32, '\0')));
            bw.Write(FileCreationDayOfYear);
            bw.Write(FileCreationYear);
            bw.Write(HeaderSize);
            bw.Write(OffsetPointData);
            bw.Write(NumberOfVariableLengthRecords);
            bw.Write(PointDataRecordFormat);
            bw.Write(PointDataRecordLength);
            bw.Write(LegacyNumberOfPointRecords);
            for (int i = 0; i < 5; i++)
                bw.Write(LegacyNumberOfPointbyReturn[i]);
            bw.Write(XScaleFactor);
            bw.Write(YScaleFactor);
            bw.Write(ZScaleFactor);
            bw.Write(XOffset);
            bw.Write(YOffset);
            bw.Write(ZOffset);
            bw.Write(MaxX);
            bw.Write(MinX);
            bw.Write(MaxY);
            bw.Write(MinY);
            bw.Write(MaxZ);
            bw.Write(MinZ);
            if (VersionMinor > 2)
                bw.Write(StartOfWaveFormDataPacketRecord);
            bw.Close();
        }

        public void SetNewGUID()
        {
            Guid guid = Guid.NewGuid();
            GUIDArr = guid.ToByteArray();
            Modified = true;
        }

        public void InititalizeOffSetScaleFactors()
        {
            PointDataRecordFormatBase.XScaleFactor = XScaleFactor;
            PointDataRecordFormatBase.YScaleFactor = YScaleFactor;
            PointDataRecordFormatBase.ZScaleFactor = ZScaleFactor;
            PointDataRecordFormatBase.XOffset = XOffset;
            PointDataRecordFormatBase.YOffset = YOffset;
            PointDataRecordFormatBase.ZOffset = ZOffset;
        }

        public string GetVariableLengthRecs()
        {
            string ret = string.Empty;
            for (uint i = 0; i < NumberOfVariableLengthRecords; i++)
            {
                VariableLengthRecordHeader vlrh = new VariableLengthRecordHeader(br);
                ret += string.Format(
@"Reserved:            {0}
User ID:             {1}
Record ID            {2}
RecLen. after header:{3}
Description:         {4}
Data after header:   {5}

", vlrh.Reserved, vlrh.UserID, vlrh.RecordID, vlrh.RecordLengthAfterHeader,vlrh.Description,
   string.Join(" ", Array.ConvertAll(vlrh.DataAfterHeader, b => b.ToString("X2")))
                    );
            }
            return ret;
        }
        public string GetGlobalDecodedString()
        {
            if (VersionMinor < 3)
                return string.Format("Not implemented in {0}", Version);
            else
                return string.Format(
@"GPS Time Type:                   {0}
WaveForm Data Packs External:    {1}
Ret. numbers sinthetic:          {2}
Wave Format F. Name:             {3}", GlobalDecoded[0] == 0 ? "Week Type (same of old LAS versions)" : "Adjusted Standard GPS Time",
                           (GlobalDecoded[1] == 1) && (GlobalDecoded[2] == 0) ? "False" :
                           (GlobalDecoded[1] == 0) && (GlobalDecoded[2] == 1) ? "True" :
                           (GlobalDecoded[1] == 0) && (GlobalDecoded[2] == 0) ? "False" : "Error",
                           (GlobalDecoded[1] == 0) ? "False" : "True",WDPFileName
                          
                );
        }

        public string GetFWFData(int recs2Get, char sepChar = ' ')
        {
            br.BaseStream.Position = (long)OffsetPointData;
            WDPFile wdp = new WDPFile(WDPFileName);
            string ret = string.Empty;
            for(int i=0; i<recs2Get; i++)
            {
                PointDataRecordFormatBase p = PointDataRecordFormatFactory.Get(LiDARFile.PointDataRecordFormat, br);
                p.GetFieldValues();
                wdp.GetNextRec((long)p.Offset2WFData, (int)PointDataRecordFormatBase.WaveFormPacketSize);
                ret += string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}", 
                    sepChar, i, p.GPSTime,p.AdjustedX,p.AdjustedY, p.AdjustedZ, p.Intensity,p.NumberOfReturns, p.ReturnNumber,p.Offset2WFData, PointDataRecordFormatBase.WaveFormPacketSize);
                ret += wdp.GetRecStr((int)PointDataRecordFormatBase.WaveFormPacketSize, sepChar);
            }
            wdp.Close();
            return ret;
        }

        Stream brBraseStream;
        public string GetLiDARHeader()
        {
            return string.Format(
@"File Name:           {0}
File Signature:      {1}
File Source ID:      {2}
Global Encoding:     {3}
GUID:                {4}
Version:             {5}
System Identifier:   {6}
Generating Software: {7}
File Creation Date:  {8}
Header Size:         {9}
Offset Point Data:   {10}
Variable Length Recs:{11}
Point DataRec Format:{12}
Point DataRec Length:{13}
Number Of Points:    {14}
Points By Return:    {15},{16},{17},{18},{19}
X Scale Factor:      {20}
Y Scale Factor:      {21}
Z Scale Factor:      {22}
X Offset:            {23}
Y Offset:            {24}
Z Offset:            {25} 
Min X:               {26}
Max X:               {27} 
Min Y:               {28}
Max Y:               {29}
Min Z:               {30}
Max Z:               {31}
Current position:    {32}", LASFileName, FileSignature, FileSourceID, GlobalEncoding, GUID, Version, SystemIdentifier, GeneratingSoftware,
                                FileCreationDateTime, HeaderSize, OffsetPointData, NumberOfVariableLengthRecords, PointDataRecordFormat,
                                PointDataRecordLength, LegacyNumberOfPointRecords, LegacyNumberOfPointbyReturn[0], LegacyNumberOfPointbyReturn[1],
                                LegacyNumberOfPointbyReturn[2], LegacyNumberOfPointbyReturn[3], LegacyNumberOfPointbyReturn[4],
                                XScaleFactor, YScaleFactor, ZScaleFactor, XOffset, YOffset, ZOffset,
                                MinX, MaxX, MinY, MaxY, MinZ, MaxZ, brBraseStream.Position);

        }
        public string GetLiDARPrintableData(decimal points2Print)
        {
            string ret = GetLiDARHeader();
            if (VersionMinor < 3)
                return ret;
            else
                ret += string.Format(
@"
================= v1.3 and above =================
{0}
Start of WF Data Packet Record:  {1}

Variable length records
--------------------------------------------------
{2}Current position:    {3}

Points data
X                Y                Z                Int  RN NR SDF   EFL CLF  SAR URD  PSID GPS TIME            WPDI    Ofs. WF Data WF Pk. Size Ret.Point WF Loc. R  G  B
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
", GetGlobalDecodedString(), StartOfWaveFormDataPacketRecord, GetVariableLengthRecs(),br.BaseStream.Position);

            InititalizeOffSetScaleFactors();
            br.BaseStream.Position = (long)OffsetPointData;
            PointDataRecordFormatBase.WaveFormPacketSize = WFPackageSize;
            PointDataRecordFormatBase p;
            for (int i=0; i < points2Print;i++)
            {
                p = PointDataRecordFormatFactory.Get(PointDataRecordFormat, br);
                p.GetFieldValues();
                ret += p.DataAsString + "\r\n";
            }
            return ret;
        }

        public string Raw()
        {
            string ret = string.Empty;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(this);
                ret += string.Format("{0}={1}\r\n", name, value);
                Console.WriteLine("{0}={1}\r\n", name, value);
            }
            return ret;
        }
#endregion
    }
}

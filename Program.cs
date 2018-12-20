using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace piWorksDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(@"D:\Desktop\piWorksDataReader\piWorksDataReader\exhibitA-input.csv"))
            {
                int control = 0;

                List<string> ClientList = new List<string>();
                List<int> SongCountByClientList = new List<int>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split("\t");
                    
                    if(control >= 1 && !(isThereTheSecondParameterInThisList(ClientList, values.GetValue(2).ToString())))
                    {
                        ClientList.Add(values.GetValue(2).ToString());
                    }

                    control++;
                }

                using (var reader2 = new StreamReader(@"D:\Desktop\piWorksDataReader\piWorksDataReader\exhibitA-input.csv"))
                {
                    List<string> TempList = new List<string>();

                    foreach (string CurrentClientID in ClientList)
                    {

                        while (!reader2.EndOfStream)
                        {
                            var line2 = reader2.ReadLine();
                            var values2 = line2.Split("\t");
                            if (values2.GetValue(2).ToString() == CurrentClientID)
                            {

                                if (!(isThereTheSecondParameterInThisList(TempList, values2.GetValue(1).ToString())))
                                {
                                    TempList.Add(values2.GetValue(1).ToString());
                                }

                            }

                        }

                        SongCountByClientList.Add(countOfList(TempList));

                        //Console.WriteLine(countOfList(TempList));

                        reader2.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                        TempList.Clear();
                    }
                }

                Console.WriteLine("How many users played 346 distinct songs : " + Q2Function(346, SongCountByClientList));
                Console.WriteLine("Maximum number of distinct songs  played : " + Q3Function(SongCountByClientList));

                Thread.Sleep(2000000000);

            }

        }

        public static Boolean isThereTheSecondParameterInThisList(List<string> inComeList, string ClientID)
        {
            if (inComeList == null)
            {
                return false;
            }

            foreach (string item in inComeList)
            {
                if (item == ClientID)
                {
                    return true;
                }
            }

            return false;
        }


        public static int Q2Function(int distinctSongCount, List<int> SongCountList)
        {
            int returnCount = 0;

            foreach (int Current in SongCountList)
            {
                if (Current == distinctSongCount)
                {
                    returnCount++;
                }
            }

            return returnCount;
        }

        public static int Q3Function(List<int> SongCountList)
        {
            return SongCountList.Max();
        }

        public static int countOfList(List<string> ListOfFunc)
        {
            int countOfTempList = 0;

            foreach (string justCount in ListOfFunc)
            {
                countOfTempList++;
            }

            return countOfTempList;
        }

    }
}

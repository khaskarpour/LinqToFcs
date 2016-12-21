using LinqToFcs.Core;
using LinqToFcs.Core.Entities;
using System;
using System.Linq;

namespace LinqToFcs.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (FcsReader reader = new FcsReader("TestFile.fcs"))
            //{
            //    using (FcsWriter writer = new FcsWriter("TestFile2.fcs"))
            //    {
            //        //reader.DataSets[0].Events

            //        var dataSet = new FcsDataSet((TextData)reader.DataSets[0].TextData.Clone(), reader.DataSets[0].Events);
            //        var dataSets = new FcsDataSets();
            //        dataSets.Add(dataSet);

            //        writer.Write(dataSets);
            //    }
            //}

            //return;
            using (FcsReader cnx = new FcsReader("TestFile2.fcs"))
            {
                Console.WriteLine("---------------------------------------");

                foreach (var item in cnx.DataSets.Select((x, index) => new { DataSet = x, Index = index }))
                {
                    FcsDataSet dataSet = item.DataSet;

                    Console.WriteLine(string.Format("Header {0}", item.Index + 1));

                    Console.WriteLine(string.Format("   Version = {0}", dataSet.HeaderData.Version));
                    Console.WriteLine(string.Format("   BeginText = {0}", dataSet.HeaderData.BeginText));
                    Console.WriteLine(string.Format("   EndText = {0}", dataSet.HeaderData.EndText));
                    Console.WriteLine(string.Format("   BeginData = {0}", dataSet.HeaderData.BeginData));
                    Console.WriteLine(string.Format("   EndData = {0}", dataSet.HeaderData.EndData));

                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine();

                    Console.WriteLine(string.Format("Text {0}", item.Index + 1));

                    Console.WriteLine(string.Format("   Version = {0}", dataSet.TextData.FIL));
                    Console.WriteLine(string.Format("   BeginText = {0}", dataSet.TextData.EXP));
                    Console.WriteLine(string.Format("   EndText = {0}", dataSet.TextData.ETIM));
                    Console.WriteLine(string.Format("   BeginData = {0}", dataSet.TextData.GATE));
                    Console.WriteLine(string.Format("   EndData = {0}", dataSet.TextData.MODE));
                    Console.WriteLine(string.Format("   EndData = {0}", dataSet.TextData.PAR));
                    Console.WriteLine(string.Format("   EndData = {0}", dataSet.TextData.OP));
                    // any other parameter of text segment, refer to TextData entity
                }

                var query = cnx.DataSets[0]
                    .Events;
                    //.Where(x => (float)x["Time"] > 100);

                foreach (var i in query)
                {
                    Console.WriteLine(i.ToString());
                }

                Console.ReadLine();
            }
        }
    }
}

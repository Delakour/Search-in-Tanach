using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace DAL
{
    public class BibleDal
    {
        const string Tanach_File_Path = "D:\\Desktop\\תכנות\\שנה ב\\C#\\תרגילים\\פרויקט תנך\\BibleProject\\Dal\\Tanach\\";
        const string Data_Path = "D:\\Desktop\\תכנות\\שנה ב\\C#\\תרגילים\\פרויקט תנך\\BibleProject\\Dal\\Data.json";
        const string Psukim_Path = "D:\\Desktop\\תכנות\\שנה ב\\C#\\תרגילים\\פרויקט תנך\\BibleProject\\Dal\\psukim.json";
        public const string Text_Path = "D:\\Desktop\\תכנות\\שנה ב\\C#\\תרגילים\\פרויקט תנך\\BibleProject\\Dal\\tanach.txt";


        public static string ReadingDataJson()
        {
            //read from data.json and return it
            string data = File.ReadAllText(Data_Path);
             
            dynamic recent = JsonConvert.DeserializeObject(data);

            data = recent.ToString();

            return data;
        }
        public static string ReadingPsukimJson()
        {
            //read from psukim.json and return it
            string data = File.ReadAllText(Psukim_Path);

            dynamic psukim = JsonConvert.DeserializeObject(data);

            data = psukim.ToString();

            return data;
        }
        public static void WritingJson(List<Enteties.Location> words)
        {
            //write to recent search
            string data = ReadingDataJson();
            dynamic newJsonObj;
            dynamic myData = JsonConvert.DeserializeObject(data);
            JObject j = [];
            string location;
            string code = "";
            string add = "";
            
            foreach (var i in words)
            {
                newJsonObj = j;
                code = ConvertLocationToCode(i);
                location = JsonConvert.SerializeObject(i);
                dynamic l = JsonConvert.DeserializeObject(location);
                newJsonObj[code] = l;
                myData["recent_search"][i.WordFound] = newJsonObj;
                add = JsonConvert.SerializeObject(myData, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Data_Path, add);
            }
        }
        private static void WriteTanachJson()
        {
            //private - function to be used just one time
            //STEP 1
            //WRITE TANACH IN A TEXT FILE

            //--- = new sefer
            //||| = new paracha (הוספתי ידנית)
            //* * * = new perek

            string dataString = ReadingDataJson();
            dynamic data = JsonConvert.DeserializeObject(dataString);
            string add = "";
            for (int sefer = 1; sefer <= 39; sefer++)
            {
                add += "---\n";
                string current = File.ReadAllText(Tanach_File_Path + $"{data["Sefer"][$"{sefer}"]}.json");

                dynamic currentPsukim = JsonConvert.DeserializeObject(current);

                int perek = 1;
                while(currentPsukim[$"{perek}"] != null)
                {
                    int i = 1;
                    while(currentPsukim[$"{perek}"][$"{i}"] != null)
                    {
                        add += JsonConvert.SerializeObject(currentPsukim[$"{perek}"][$"{i}"], Newtonsoft.Json.Formatting.Indented) + "\n";
                        i++;
                    }
                    add += "* * *\n";
                    perek++;
                }
            }
            File.WriteAllText(Text_Path, add);

            //STEP 2
            string[] tanach = File.ReadAllLines(Text_Path);

            string psukim = ReadingPsukimJson();
            dynamic addToJson = JsonConvert.DeserializeObject(psukim);

            string convert;
            string code;
            int countSefer = 0, countParacha = 0, countPerek = 0, countPasuk = 0;

            for (int i = 0; i < tanach.Length; i++)
            {
                if (tanach[i] == "---")
                {
                    countSefer++;
                    countPerek = 0;
                    continue;
                }
                if (tanach[i] == "|||")
                {
                    countParacha++;
                    continue;
                }
                if (tanach[i] == "* * *")
                {
                    countPerek++;
                    countPasuk = 0;
                    continue;
                }
                if (countSefer > 5)
                    countParacha = 0;

                countPasuk++;
                code = $"{countSefer}-{countParacha}-{countPerek}-{countPasuk}";

                addToJson[code] = tanach[i];

                convert = JsonConvert.SerializeObject(addToJson, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Psukim_Path, convert);
            }

            //DELETE QUOTS
            string s = File.ReadAllText(Text_Path);
            s = s.Replace("\"", "");
            File.WriteAllText(Text_Path, s);
        }
        private static string ConvertLocationToCode(Enteties.Location l)
        {
            string tanach = ReadingDataJson();
            dynamic data = JsonConvert.DeserializeObject(tanach);
            string code = "";

            int i = 0;
            string s = data["Sefer"][$"{i}"];
            while (!l.Sefer.Equals(s))
            {
                i++;
                s = data["Sefer"][$"{i}"];
            }
               
            code += i + "-";

            i = 0;
            s = data["Paracha"][$"{i}"];
            while (!l.Paracha.Equals(s))
            {
                i++;
                s = data["Paracha"][$"{i}"];
            }
               
            code += i + "-";

            code += ConvertLetterToNum(l.Perek) + "-" + ConvertLetterToNum(l.Pasuk);

            return code;
        }
        private static int ConvertLetterToNum(string letters)
        {
            char[] letter = {
                'א','ב','ג','ד','ה','ו','ז','ח','ט','י','כ','ל','מ'
            ,'נ','ס','ע','פ','צ','ק','ר','ש','ת'
            };

            char[] slice = letters.ToCharArray();
            int sum = 0;
            int index = 0;


            for (int i = slice.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < letter.Length; j++)
                {
                    if (letter[j] == slice[i]) 
                    {
                        index = j;
                        break;
                    }
                }
                sum += index + 1;
            }
            return sum;
        }
    }
}

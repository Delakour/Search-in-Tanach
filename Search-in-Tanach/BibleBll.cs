using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using DAL;
using Enteties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BLL
{
    public class BibleBll
    {
        public static List<string> SearchName(string name)
        {
            string[] psukim = File.ReadAllLines(BibleDal.Text_Path);

            List<string> send = new List<string>();

            char[] nameLetter = name.ToCharArray();

            foreach (var i in psukim)
            {
                if (((i.Contains(" " + name + " ") || i.EndsWith(" " + name) || i.StartsWith(name + " ")) ||
                    ((i.StartsWith(nameLetter[0])) &&
                    (i.EndsWith(nameLetter[nameLetter.Length - 1])))))
                {
                    send.Add(i);
                }
            }
            return send;
        }
        public static List<Enteties.Location> SearchWord(string word)
        {
            string read = DAL.BibleDal.ReadingDataJson();

            dynamic recent = JsonConvert.DeserializeObject(read);
            recent = recent["recent_search"];

            List<Enteties.Location> locations = new List<Enteties.Location>();

            //first, search if the word have been already search
            bool isRecently = false;

            foreach (var i in recent)
            {
                if (i.Name.ToString() == word)
                {
                    foreach (var j in i.Value)
                    {
                        locations.Add(JsonConvert.DeserializeObject<Enteties.Location>(j.Value.ToString()));
                        isRecently = true;
                    }
                    break;
                }
            }
            if (isRecently)
                return locations;

            //else, have to search the word in the whole tanach
            return SearchInTanach(word);
        }
        private static List<Enteties.Location> SearchInTanach(string word)
        {
            List<Enteties.Location> locations = new List<Enteties.Location>();
            string current;

            string psukim = DAL.BibleDal.ReadingPsukimJson();
            dynamic data = JsonConvert.DeserializeObject(psukim);
            foreach (var i in data)
            {
                current = i.Value.ToString();
                if (current.Contains(" " + word + " ") ||
                    current.EndsWith(" " + word) ||
                    current.StartsWith(word + " "))
                {
                    locations.Add(ConvertCodeToLocation(i.Value.ToString(), word, i.Name.ToString()));
                }
            }
            DAL.BibleDal.WritingJson(locations);
            return locations;
        }
        private static Enteties.Location ConvertCodeToLocation(string all, string word, string code)
        {
            string tanach = DAL.BibleDal.ReadingDataJson();
            dynamic data = JsonConvert.DeserializeObject(tanach);

            Enteties.Location l = new Enteties.Location();

            string[] location = code.Split("-");


            l.WordFound = word;
            l.AllPasuk = all;
            l.Sefer = data["Sefer"][$"{location[0]}"];
            l.Paracha = data["Paracha"][$"{location[1]}"];
            l.Perek = ConvertNumToLetters(Int32.Parse(location[2]));
            l.Pasuk = ConvertNumToLetters(Int32.Parse(location[3]));
            return l;
        }
        private static string ConvertNumToLetters(int number)
        {
            char[] letters = {
                'א','ב','ג','ד','ה','ו','ז','ח','ט','י','כ','ל','מ'
            ,'נ','ס','ע','פ','צ','ק','ר','ש','ת'
            };

            string toRet = "";
            int x;
            int index;


            for (int i = 0; i < 3 && number > 0; i++)
            {
                x = number % 10;
                index = x + i * 10 - (i + 1);

                if ((toRet == "ו" || toRet == "ה") && x == 1)
                {
                    toRet = toRet == "ה" ? "טו" : "טז";
                    number = number / 10;
                    continue;
                }
                if (index < 0)
                {
                    number = number / 10;
                    continue;
                }


                toRet = letters[index] + toRet;
                number = number / 10;
            }
            return toRet;
        }
    }
}
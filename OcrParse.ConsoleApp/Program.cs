using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OcrParse.ConsoleApp
{

    public class OcrResult
    {
        public OcrResult(int line, string text)
        {
            Line = line;
            Text = text;
        }

        public int Line { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            int precision = 5;
            var responseJson = LoadFileToJToken(".\\file\\response.json");

            var result = OcrParse(precision, responseJson);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        private static List<OcrResult> OcrParse(int precision, JToken responseJson)
        {
            List<OcrResult> result = new List<OcrResult>();
            var values = responseJson.Select(x => new { yCoordinate = int.Parse(x.SelectToken("boundingPoly.vertices[0]")["y"]?.ToString()), description = x.SelectToken("description") }).OrderBy(x => x.yCoordinate).ToList();

            int beforeYcoordinate = values.FirstOrDefault().yCoordinate;
            values = values.Skip(1).ToList();
            int line = 1;
            var text = "";

            for (int i = 0; i < values.Count(); i++)
            {
                var child = values[i];
                var desc = child.description;
                var y = child.yCoordinate;

                if ((beforeYcoordinate + precision >= y && beforeYcoordinate - precision <= y))
                {
                    beforeYcoordinate = y;
                    text += !string.IsNullOrEmpty(text) ? " " + desc : desc;
                    if (values.Count() - 1 == i)
                        result.Add(new OcrResult(line, text));
                    continue;
                }
                else
                {
                    result.Add(new OcrResult(line, text));
                    text = desc.ToString();
                }

                beforeYcoordinate = y;
                line++;
            }

            return result;
        }

        public static JToken LoadFileToJToken(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                return JToken.Parse(json);
            }
        }
    }
}

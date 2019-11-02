using ClosedXML.Excel;
using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[0] == "-i" && args[2] == "-o" && args[4] == "-f")
                {
                    List<string> list = new List<string>();
                    using (StreamReader sr = new StreamReader(args[1] + ".csv", System.Text.Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            list.Add(line);
                        }
                    }
                    Group group = new Group(list);
                    if (args[5] == "JSON")
                        WriteJsonFile(group, args[3]);
                    else if (args[5] == "EXCEL")
                        WriteExelFile(group, args[3]);
                    else
                        throw new Exception("Incorrect parameters: " + args[5]);
                }
                else
                    throw new Exception("Incorrect parameters");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("end of program");
            Console.ReadLine();
        }

        static void WriteJsonFile(Group group, string name)
        {
            string s = JsonConvert.SerializeObject(group);
            using (StreamWriter sw = new StreamWriter(name + ".json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(s);
            }
        }

        static void WriteExelFile(Group group, string name)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Лист1");
            worksheet.Cell("A" + 1).Value = "Фамилия";
            worksheet.Cell("B" + 1).Value = "Имя";
            worksheet.Cell("C" + 1).Value = "Отчество";
            worksheet.Cell("D" + 1).Value = "Средняя оценка";
            int i = 0;
            for (i = 0; i < group.Persons.Count; i++)
            {
                worksheet.Cell("A" + (i + 2)).Value = group.Persons[i].Surname;
                worksheet.Cell("B" + (i + 2)).Value = group.Persons[i].Name;
                worksheet.Cell("C" + (i + 2)).Value = group.Persons[i].Patronymic;
                worksheet.Cell("D" + (i + 2)).Value = group.Persons[i].Ball;
            }
            worksheet.Cell("A" + (i + 3)).Value = "Предмет";
            worksheet.Cell("B" + (i + 3)).Value = "Средняя оценка";
            int j = i+4;
            for (i = 0; i < group.Subjects.Count; i++)
            {
                worksheet.Cell("A" + (i + j)).Value = group.Subjects[i].Name;
                worksheet.Cell("B" + (i + j)).Value = group.Subjects[i].Ball;
            }
            
            worksheet.Cell("A" + (j+i+2)).Value = "Среднее по группе";
            worksheet.Cell("B" + (j+i+2)).Value = group.Ball;
            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(name + ".xlsm");
        }
    }
}

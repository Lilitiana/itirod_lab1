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
                    List<Person> people = new List<Person>();
                    Group group = new Group();
                    using (StreamReader sr = new StreamReader(args[1]+".csv", System.Text.Encoding.Default))
                    {
                        using (CsvReader csvReader = new CsvReader(sr))
                        {
                            // указываем используемый разделитель
                            csvReader.Configuration.Delimiter = ";";
                            csvReader.Configuration.HeaderValidated = null;
                            csvReader.Configuration.MissingFieldFound = null;
                            // получаем строки
                            people = csvReader.GetRecords<Person>().ToList();

                            //средние баллы
                            foreach (Person p in people)
                            {
                                p.Process();
                                group.AverageBallSelectedChaptersOfComputerScience += p.SelectedChaptersOfComputerScience;
                                group.AverageBallInternetApplicationDevelopment += p.InternetApplicationDevelopment;
                                group.AverageBallAlgorithmsAndDataStructures += p.AlgorithmsAndDataStructures;
                                group.AverageBallMathematicalModelingOfComplexSystems += p.MathematicalModelingOfComplexSystems;
                                group.AverageBallOperatingSystemsAndEnvironments += p.OperatingSystemsAndEnvironments;
                            }
                            group.AverageBallSelectedChaptersOfComputerScience = group.AverageBallSelectedChaptersOfComputerScience / people.Count();
                            group.AverageBallInternetApplicationDevelopment = group.AverageBallInternetApplicationDevelopment / people.Count();
                            group.AverageBallAlgorithmsAndDataStructures = group.AverageBallAlgorithmsAndDataStructures / people.Count();
                            group.AverageBallMathematicalModelingOfComplexSystems = group.AverageBallMathematicalModelingOfComplexSystems / people.Count();
                            group.AverageBallOperatingSystemsAndEnvironments = group.AverageBallOperatingSystemsAndEnvironments / people.Count();
                            group.GroupAverage = (group.AverageBallSelectedChaptersOfComputerScience + group.AverageBallInternetApplicationDevelopment + group.AverageBallAlgorithmsAndDataStructures + group.AverageBallMathematicalModelingOfComplexSystems + group.AverageBallOperatingSystemsAndEnvironments) / 5.0;
                        }
                    }
                    if (args[5] == "JSON")
                        WriteJsonFile(group, people, args[3]);
                    if (args[5] == "EXCEL")
                        WriteExelFile(group, args[3]); ;
                }
                else
                    throw new Exception("Incorrect parameters");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        static void WriteJsonFile(Group group, List<Person> people, string name)
        {
            group.People = people;
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
            for (i = 0; i < group.People.Count; i++)
            {
                worksheet.Cell("A" + (i + 2)).Value = group.People[i].Surname;
                worksheet.Cell("B" + (i + 2)).Value = group.People[i].Name;
                worksheet.Cell("C" + (i + 2)).Value = group.People[i].Patronymic;
                worksheet.Cell("D" + (i + 2)).Value = group.People[i].AverageBall;
            }
            worksheet.Cell("A" + (i + 3)).Value = "Предмет";
            worksheet.Cell("A" + (i + 4)).Value = "Избранные главы информатики";
            worksheet.Cell("A" + (i + 5)).Value = "Алгоритмы и структуры данных";
            worksheet.Cell("A" + (i + 6)).Value = "Математическое моделирование сложных систем";
            worksheet.Cell("A" + (i + 7)).Value = "Операционные системы и среды";
            worksheet.Cell("A" + (i + 8)).Value = " Разработка приложений для Интернет";
            worksheet.Cell("A" + (i + 10)).Value = "Среднее по группе";

            worksheet.Cell("B" + (i + 3)).Value = "Средняя оценка";
            worksheet.Cell("B" + (i + 4)).Value = group.AverageBallSelectedChaptersOfComputerScience;
            worksheet.Cell("B" + (i + 5)).Value = group.AverageBallAlgorithmsAndDataStructures;
            worksheet.Cell("B" + (i + 6)).Value = group.AverageBallMathematicalModelingOfComplexSystems;
            worksheet.Cell("B" + (i + 7)).Value = group.AverageBallOperatingSystemsAndEnvironments;
            worksheet.Cell("B" + (i + 8)).Value = group.AverageBallInternetApplicationDevelopment;
            worksheet.Cell("B" + (i + 10)).Value = group.GroupAverage;
            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(name + ".xlsm");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Task1
{
    [DataContract]
    public class Person
    {
        [DataMember]
        [Name("Фамилия")]
        public string Surname { get; set; }//фамилия
        [DataMember]
        [Name("Имя")]
        public string Name { get; set; }//имя
        [DataMember]
        [Name("Отчество")]
        public string Patronymic { get; set; }//отчество
        [Name("Избранные главы информатики")]
        public int SelectedChaptersOfComputerScience { get; set; }//Избранные главы информатики
        [Name("Алгоритмы и структуры данных")]
        public int AlgorithmsAndDataStructures { get; set; }//Алгоритмы и структуры данных
        [Name("Математическое моделирование сложных систем")]
        public int MathematicalModelingOfComplexSystems { get; set; }//Математическое моделирование сложных систем
        [Name("Операционные системы и среды")]
        public int OperatingSystemsAndEnvironments { get; set; }//Операционные системы и среды
        [Name("Разработка приложений для Интернет")]
        public int InternetApplicationDevelopment { get; set; }//Разработка приложений для Интернет
        [DataMember]
        public double AverageBall { get; set; }//Средняя оценка студента

        public void Process()
        {
            AverageBall = (SelectedChaptersOfComputerScience + AlgorithmsAndDataStructures + MathematicalModelingOfComplexSystems + OperatingSystemsAndEnvironments + InternetApplicationDevelopment) / 5.0;
        }
    }
}

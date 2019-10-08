using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace Task1
{
    [DataContract]
    public class Group
    {
        [DataMember]
        public List<Person> People { get; set; }
        [DataMember]
        public double AverageBallSelectedChaptersOfComputerScience { get; set; }//Избранные главы информатики
        [DataMember]
        public double AverageBallAlgorithmsAndDataStructures { get; set; }//Алгоритмы и структуры данных
        [DataMember]
        public double AverageBallMathematicalModelingOfComplexSystems { get; set; }//Математическое моделирование сложных систем
        [DataMember]
        public double AverageBallOperatingSystemsAndEnvironments { get; set; }//Операционные системы и среды
        [DataMember]
        public double AverageBallInternetApplicationDevelopment { get; set; }//Разработка приложений для Интернет
        [DataMember]
        public double GroupAverage { get; set; }//Средняя оценка группы        
    }
}

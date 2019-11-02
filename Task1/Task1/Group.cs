using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1
{
    public class Group
    {
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Person> Persons { get; set; } = new List<Person>();
        public double Ball { get; set; } = 0;
        public Group (List<string> file)
        {
            string[] tmp = file[0].Split(';');
            for (int i = 3; i < tmp.Length; i++)
                Subjects.Add(new Subject() { Name = tmp[i],Ball=0});
            file.RemoveAt(0);
            foreach(string p in file)
            {
                tmp = p.Split(";");
                Person person = new Person() { Surname = tmp[0], Name = tmp[1], Patronymic = tmp[2] };
                for(int i = 3; i < tmp.Length; i++)
                {
                    person.Ball+=Convert.ToDouble(tmp[i]);
                    Subjects[i - 3].Ball += Convert.ToDouble(tmp[i]);
                }
                person.Ball /= (tmp.Length - 3);
                Persons.Add(person);
            }
            foreach (Subject subject in Subjects)
                subject.Ball /= Persons.Count;
            Ball = Subjects.Sum(p => p.Ball) / Subjects.Count;
        }
    }
}

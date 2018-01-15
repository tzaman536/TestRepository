using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.IEnumerableTest
{
    public class Person
    {
        public string Name { get; set; }
    }

    public class People: IEnumerable
    {
        public Person[] _people;

        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }


    public class PeopleEnum : IEnumerator
    { 
        public Person[] _people;

        int position = -1;

        public PeopleEnum(Person[] list)
        {
            _people = list;
        }


        public Person Current
        {
            get
            {
                try {
                    return _people[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }




    public class ImplementIEnumerable
    {
        public static void Start()
        {
            /*
                https://msdn.microsoft.com/en-us/library/system.collections.ienumerator(v=vs.110).aspx
           */

            try {
                Person[] personArry = new Person[] {
                    new Person() { Name = "Tanweer" }
                    ,new Person() { Name = "Raif" }
                    , new Person() { Name = "Denia" }
                    , new Person() { Name = "Zaara" }
                };

                People peopleList = new People(personArry);

                foreach(var p in peopleList)
                {
                    Console.WriteLine(p.Name);
                }


                Console.ReadLine();
            }
            catch(Exception ex)
            {

            }


        }
    }
}

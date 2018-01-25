using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.IEnumerableTest
{
    public class AnimalEnum
    {
        class Animal
        {
            public string Name { get; set; }
        }



        class Animals: IEnumerable
        {
            Animal[] _animals;
            public Animals(Animal[] a)
            {
                _animals = new Animal[a.Length];
                for(int i=0; i<a.Length; i++)
                {
                    _animals[i] = a[i];
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new AnimalEnumerator(_animals);
            }
        }

        class AnimalEnumerator : IEnumerator
        {
            Animal[] _animals;
            int _index;

            public AnimalEnumerator(Animal[] a)
            {
                _animals = a;
                _index = -1;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return _animals[_index];
                    }
                    catch(IndexOutOfRangeException ex)
                    {
                        throw ex;
                    }
                    
                }
            }

            public bool MoveNext()
            {
                _index++;
                return (_index < _animals.Length);
            }

            public void Reset()
            {
                _index = -1;
            }
        }




        public static void Start()
        {
            Animal[] animals = new[]
            {
                new Animal() { Name = "Tiger" }
                , new Animal() { Name="Monkey" }
                , new Animal() { Name = "Elephant" }
            };

            Animals a = new Animals(animals);
            foreach(Animal animal in a)
            {
                Console.WriteLine(animal.Name);
            }
            Console.ReadLine();
        }
    }
}

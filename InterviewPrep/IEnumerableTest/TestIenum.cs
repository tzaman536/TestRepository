using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.IEnumerableTest
{
 
    public class Car
    {
        public string Name { get; set; }
    }  

    public class Cars: IEnumerable
    {
        Car[] _cars;
        public Cars(Car[] carList)
        {
            _cars = new Car[carList.Length];

            for(int i=0; i< carList.Length; i++)
            {
                _cars[i] = carList[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new CarEnumerator(_cars);
        }
    }

    public class CarEnumerator : IEnumerator
    {
        Car[] _cars;
        int _index;
        public CarEnumerator(Car[] carList)
        {
            _cars = carList;
            _index = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _cars[_index];
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
            return (_index < _cars.Length);
        }

        public void Reset()
        {
            _index = -1;
        }
    }



    public class TestIenum
    {
        public static void Start()
        {
            Car[] arr = new Car[] {
                        new Car() { Name = "Toyota" },
                        new Car() { Name = "Honda" },
                        new Car() { Name = "Lexus" }
                        };

            Cars cars = new Cars(arr);

            foreach(Car c in cars)
            {
                Console.WriteLine(c.Name);
            }

            Console.ReadLine();


        }
    }
}

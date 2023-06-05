using System.Collections.Generic;

namespace MegaCore.DataModule
{

    public class DataClass : DataAbstract
    {

        #region Insert Custom Data

        // int
        public int gems;

        // float
        public float speed;

        // Class
        public class Car
        {
            public string name;
            public int capacity;
        }

        // List
        public List<Car> carList;

        // Dictionary
        public Dictionary<string, Car> carDict;

        // List of Dictionary
        public List<Dictionary<string, Car>> carsDictList;

        #endregion


        #region Initiate Custom Data
        public DataClass()
        {
            gems = 100;

            speed = 195.7f;

            carList = new List<Car>();
            carList.Add(new Car { name = "BMW", capacity = 5 });
            carList.Add(new Car { name = "Mazda", capacity = 4 });
            carList.Add(new Car { name = "Nissan", capacity = 6 });

            carDict = new Dictionary<string, Car>();
            carDict["BMW"] = carList[0];
            carDict["Mazda"] = carList[1];
            carDict["Nissan"] = carList[2];

            carsDictList = new List<Dictionary<string, Car>>();
            carsDictList.Add(carDict);
        }
        #endregion

    }

}
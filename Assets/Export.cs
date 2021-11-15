using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//ref https://docs.microsoft.com/en-us/dotnet/api/system.io.file.appendtext?view=net-5.0

public class Export : MonoBehaviour
{
    string filename = "";

    [System.Serializable]
    public class Data
    {
        public struct Space
        {
            public string levelID;
            public int passableSize;
            public int impassableSize;
            public int playableSize; //biggest space
            public int unreachableCount;
        }

        public struct Room
        {
            public int count;
            public float averageSize;
            public float biggestSize;
            public float smallestSize;
            public int decisionsPerRoom;
        }

        public struct Corridor
        {
            public int count;
            public float averageSize;
            public float biggestSize;
            public float smallestSize;
        }

        public Space space = new Space();
        public Room room = new Room();
        public Corridor corridor = new Corridor();
    }
    Data data = new Data(); //fill this class

    void Start()
    {
        filename = Application.dataPath + "/test.csv";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("saved");
            WriteCSV(data);
        }
    }

    public void WriteCSV(Data data)
    {
        if (!File.Exists(filename))
        {
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine("Level ID, Passable Space, Impassable Space, Playble Space Size, Unreachable Space Count," +
                            "Room Count, Average Room Size, Biggest Room Size, Smallest Room Size, Decisions Per Room" +
                            "Corridor Count, Average Corridor Size, Biggest Corridor Size, Smallest Corridor Size");
                print("File created and intiialized");
            }
        }
        using (StreamWriter sw = File.AppendText(filename))
        {
            for (int i = 0; i < 5; i++)
            {
                sw.WriteLine(data.space.levelID  + "," + data.space.passableSize + "," + data.space.impassableSize  + "," + 
                            data.space.playableSize + "," + data.space.unreachableCount + "," + 
                            data.room.count + "," + data.room.averageSize + "," + data.room.biggestSize + "," +
                            data.room.smallestSize + "," + data.room.decisionsPerRoom + "," +
                            data.corridor.count + "," + data.corridor.averageSize + "," + 
                            data.corridor.biggestSize + "," + data.corridor.smallestSize);
                print("Line added");
            }
        }
    }
}

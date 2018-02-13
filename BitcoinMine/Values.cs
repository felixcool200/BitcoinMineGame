using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MemeMine
{
    class Values
    {
        public Values()
        {

            if (!File.Exists(pathSave))
            {
                string[] newSave = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    newSave[i] = "0";
                }
                newSave[4] = "1";
                File.WriteAllLines(pathSave, newSave);
            }
            string[] saveFile = File.ReadAllLines(pathSave);
            bitcoins = double.Parse(saveFile[0]);
            miners = double.Parse(saveFile[1]);
            gamingrigs = double.Parse(saveFile[2]);
            servers = double.Parse(saveFile[3]);
            datacenters = double.Parse(saveFile[4]);
            level = int.Parse(saveFile[5]);
        }
        public int level = 1;
        public string path = Directory.GetCurrentDirectory().ToString();
        public string pathSave = Directory.GetCurrentDirectory().ToString() + @"\save.txt";
        public double bitcoins = 0, miners = 0, gamingrigs = 0, servers = 0, datacenters = 0;

    }
}

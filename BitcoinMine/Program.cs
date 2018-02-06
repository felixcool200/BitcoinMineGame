using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MemeMine
{
    class Program
    {
        static void Main(string[] args)
        {
            long startTime = DateTime.Now.Ticks;
            long lastClock = 0;
            Random rand = new Random();
            double bitcoins = 0, miners = 0, gamingrigs = 0, servers = 0, datacenters = 0;
            int level = 1;
            string path = Directory.GetCurrentDirectory().ToString();
            string pathSave = path + @"\save.txt";
            try
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
                bitcoins = double.Parse(File.ReadLines(pathSave).Skip(0).Take(1).First());
                miners = double.Parse(File.ReadLines(pathSave).Skip(1).Take(1).First());
                gamingrigs = double.Parse(File.ReadLines(pathSave).Skip(2).Take(1).First());
                servers = double.Parse(File.ReadLines(pathSave).Skip(3).Take(1).First());
                datacenters = double.Parse(File.ReadLines(pathSave).Skip(4).Take(1).First());
                level = int.Parse(File.ReadLines(pathSave).Skip(5).Take(1).First());
                startText();
            }
            catch
            {
                Console.WriteLine("Missing Gamefile, shutting down.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            while (true)
            {
                saveMethod(ref bitcoins, ref miners, ref gamingrigs, ref servers, ref datacenters, ref level, ref pathSave);
                startTime = DateTime.Now.Ticks;
                if (startTime >= lastClock + 100000000)
                {
                    bitcoins += (miners * 10) + (gamingrigs * 50)+ (servers * 100)+ (datacenters * 500);
                    lastClock = startTime;
                }

                string input = Console.ReadLine().ToLower();

                if (input == "")
                {
                    Console.WriteLine(bitcoins.ToString());
                    continue;
                }

                if (input[0] == ':')
                {
                    //Other input
                    switch (input)
                    {
                        //Help Command
                        case ":help":
                            Console.WriteLine(":mine = Mine bitcoins");
                            Console.WriteLine(":buy");
                            Console.WriteLine(":bitcoins = Dispaly current amount of bitcoins");
                            Console.WriteLine("save = save current progress (Auto save is enabled)");
                            Console.WriteLine("reset = reset your progress");
                            Console.WriteLine("quit = saves then exits the application");
                            Console.WriteLine("clear = clears the console window");
                            break;
                        //Display
                        case ":bitcoins":
                            Console.WriteLine("Your amount of bitcoins are : " + bitcoins.ToString());
                            Console.WriteLine("Your amount of miners are : " + miners.ToString());
                            Console.WriteLine("Your amount of fracker are : " + datacenters.ToString());
                            Console.WriteLine("Your level are : " + level.ToString());
                            break;
                        //Mine
                        case ":mine":
                            double newbitcoins = rand.Next(1, 100);
                            bitcoins += newbitcoins;
                            Console.WriteLine("You found :" + (newbitcoins).ToString() + " bitcoins.");
                            break;
                        case ":buy":
                            Console.WriteLine("Now enter how many you want to buy (or type max)");
                            double amount = 0;
                            string amountInput = "";
                            while (true)
                            {
                                try
                                {
                                    amountInput = Console.ReadLine().ToLower();
                                    if (amountInput != "max")
                                    {
                                        amount = double.Parse(amountInput);
                                    }
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid number try again");
                                    continue;
                                }
                            }
                            Console.WriteLine("Now enter what you would want to buy");
                            Console.WriteLine("Miner = 100 bitcoins");
                            Console.WriteLine("GamingRig = 1000 bitcoins");
                            Console.WriteLine("Server = 1000k bitcoins");
                            Console.WriteLine("DataCenter = 100000k bitcoins");
                            string whatToBuy = "";
                            while (true)
                            {
                                whatToBuy = Console.ReadLine().ToLower();
                                if ((whatToBuy != "miner" && whatToBuy != "datacenter") && (whatToBuy != "gamingrig" && whatToBuy != "server"))
                                {
                                    Console.WriteLine("Not a valid item");
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            switch (whatToBuy)
                            {
                                case "miner":
                                    buy("miner", ref amount, ref bitcoins, ref amountInput, ref miners, ref gamingrigs, ref servers, ref datacenters);
                                    break;

                                case "gamingrig":
                                    buy("gamingrig", ref amount, ref bitcoins, ref amountInput, ref miners, ref gamingrigs, ref servers, ref datacenters);
                                    break;

                                case "server":
                                    buy("server", ref amount, ref bitcoins, ref amountInput, ref miners, ref gamingrigs, ref servers, ref datacenters);
                                    break;

                                case "datacenter":
                                    buy("datacenter", ref amount, ref bitcoins, ref amountInput, ref miners, ref gamingrigs, ref servers, ref datacenters);
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Unknown command!");
                            break;
                    }
                    continue;
                }
                else
                {
                    switch (input)
                    {
                        case "save":
                            saveMethod(ref bitcoins, ref miners, ref gamingrigs, ref servers, ref datacenters, ref level, ref pathSave);
                            Console.WriteLine("Save Complete");
                            continue;
                        case " ":
                            for (int i = 0; i < 100; i++)
                            {
                                Console.WriteLine();
                            }
                            continue;
                        case "quit":
                            saveMethod(ref bitcoins, ref miners, ref gamingrigs, ref servers, ref datacenters, ref level, ref pathSave);
                            Console.WriteLine("Save Complete");
                            break;
                        case "clear":
                            Console.Clear();
                            startText();
                            continue;
                        case "reset":
                            File.Delete(pathSave);
                            bitcoins = 0;
                            miners = 0;
                            datacenters = 0;
                            level = 1;
                            saveMethod(ref bitcoins, ref miners, ref gamingrigs, ref servers, ref datacenters, ref level, ref pathSave);
                            continue;
                        default:
                            Console.WriteLine("Not a Command");
                            continue;
                    }
                }
                break;
            }
            Console.ReadKey();
        }

        public static void saveMethod(ref double bitcoins, ref double miners, ref double gamingrig,ref double server, ref double datacenters, ref int level, ref string pathSave)
        {
            if (!File.Exists(pathSave))
            {
                string[] newSave = new string[4];
                for (int i = 0; i < 3; i++)
                {
                    newSave[i] = "0";
                }
                newSave[3] = "1";
                File.WriteAllLines(pathSave, newSave);
            }
            string[] savefile = new string[File.ReadLines(pathSave).Count()];
            savefile = File.ReadAllLines(pathSave);
            savefile[0] = bitcoins.ToString();
            savefile[1] = miners.ToString();
            savefile[2] = gamingrig.ToString();
            savefile[3] = server.ToString();
            savefile[4] = datacenters.ToString();
            savefile[5] = level.ToString();
            File.WriteAllLines(pathSave, savefile);
        }

        public static void startText()
        {
            Console.WriteLine("Welcome to");
            Console.Write(@"    ____     _    __                   _                    __  ___    _                       
   / __ )   (_)  / /_  _____  ____    (_)   ____           /  |/  /   (_)   ____   ___    _____
  / __  |  / /  / __/ / ___/ / __ \  / /   / __ \         / /|_/ /   / /   / __ \ / _ \  / ___/
 / /_/ /  / /  / /_  / /__  / /_/ / / /   / / / /        / /  / /   / /   / / / //  __/ / /    
/_____/  /_/   \__/  \___/  \____/ /_/   /_/ /_/        /_/  /_/   /_/   /_/ /_/ \___/ /_/     
                                                                                               ");
            Console.WriteLine();
            Console.Write(@" _       __    _                __                                    ______       __    _    __     _                
| |     / /   (_)   ____   ____/ /  ____  _      __   _____          / ____/  ____/ /   (_)  / /_   (_)  ____    ____ 
| | /| / /   / /   / __ \ / __  /  / __ \| | /| / /  / ___/         / __/    / __  /   / /  / __/  / /  / __ \  / __ \
| |/ |/ /   / /   / / / // /_/ /  / /_/ /| |/ |/ /  (__  )         / /___   / /_/ /   / /  / /_   / /  / /_/ / / / / /
|__/|__/   /_/   /_/ /_/ \__,_/   \____/ |__/|__/  /____/         /_____/   \__,_/   /_/   \__/  /_/   \____/ /_/ /_/ 
                                                                                                                      ");
            Console.WriteLine();
            Console.WriteLine("To list the commands type :help");
        }

        public static void buy(string item, ref double amount, ref double  bitcoins, ref string amountInput, ref double miners, ref double gamingrigs, ref double servers, ref double datacenters)
        {
            if (amountInput == "max")
            {
                amount = bitcoins / price(item);
                amount = Math.Floor(amount);
            }
            if (bitcoins >= (price(item) * amount))
            {
                bitcoins -= price(item) * amount;
                switch (item)
                {
                    case "miners":
                        miners += amount;
                        break;
                    case "gamingrigs":
                        gamingrigs += amount;
                        break;
                    case "servers":
                        servers += amount;
                        break;
                    case "datacenters":
                        datacenters += amount;
                        break;

                }
                Console.WriteLine("You Bought " + amount.ToString()+ " " + item);
            }
            else
            {
                Console.Write("You can't afford that becuase you have ");
                Console.Write(bitcoins.ToString());
                Console.Write(" bitcoins and you need ");
                Console.Write((price(item) * amount).ToString());
                Console.Write(" bitcoins you need to earn ");
                Console.WriteLine(((price(item) * amount) - bitcoins).ToString());
            }
        }

        public static int price(string item)
        {
            switch (item)
            {
                case "miner":
                    return 100;
                case "gamingrig":
                    return 1000;
                case "server":
                    return 10000;
                case "datacenter":
                    return 100000;
                default:
                    return 0;
            }
        }
    }

}

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
            Values currentsave = new Values();
            try
            {
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
                saveMethod(currentsave);
                startTime = DateTime.Now.Ticks;
                if (startTime >= lastClock + 100000000)
                {
                    currentsave.bitcoins += (currentsave.miners * 10) + (currentsave.gamingrigs * 50)+ (currentsave.servers * 100)+ (currentsave.datacenters * 500);
                    lastClock = startTime;
                }

                string input = Console.ReadLine().ToLower();

                if (input == "")
                {
                    Console.WriteLine(currentsave.bitcoins.ToString());
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
                            Console.WriteLine("prestige = preside to the next level");
                            Console.WriteLine("save = save current progress (Auto save is enabled)");
                            Console.WriteLine("reset = reset your progress");
                            Console.WriteLine("quit = saves then exits the application");
                            Console.WriteLine("clear = clears the console window");
                            break;
                        //Display
                        case ":bitcoins":
                            Console.WriteLine("Your amount of bitcoins are : " + currentsave.bitcoins.ToString());
                            Console.WriteLine("Your amount of miners are : " + currentsave.miners.ToString());
                            Console.WriteLine("Your amount of gamingrigs are : " + currentsave.gamingrigs.ToString());
                            Console.WriteLine("Your amount of servers are : " + currentsave.servers.ToString());
                            Console.WriteLine("Your amount of datacenters are : " + currentsave.datacenters.ToString());
                            Console.WriteLine("Your level are : " + currentsave.level.ToString());
                            break;
                        //Mine
                        case ":mine":
                            double newbitcoins = rand.Next(1, 100);
                            currentsave.bitcoins += newbitcoins;
                            Console.WriteLine("You found :" + (newbitcoins).ToString() + " bitcoins.");
                            break;
                        case "prestige":
                            if (currentsave.miners >= currentsave.level * 100)
                            {
                                if (currentsave.gamingrigs >= currentsave.level * 100)
                                {
                                    if (currentsave.servers >= currentsave.level * 100)
                                    {
                                        if (currentsave.datacenters >= currentsave.level * 100)
                                        {
                                            reset();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Your amount of miners are " + currentsave.miners.ToString() + " you need another " + (currentsave.level * 100 - currentsave.miners).ToString() + " to prestige");
                                Console.WriteLine("Your amount of gamingrigs are " + currentsave.gamingrigs.ToString() + " you need another " + (currentsave.level * 100 - currentsave.gamingrigs).ToString() + " to prestige");
                                Console.WriteLine("Your amount of servers are " + currentsave.servers.ToString() + " you need another " + (currentsave.level * 100 - currentsave.servers).ToString() + " to prestige");
                                Console.WriteLine("Your amount of datacenters are " + currentsave.datacenters.ToString() + " you need another " + (currentsave.level * 100 - currentsave.datacenters).ToString() + " to prestige");
                                Console.WriteLine("Your level are : " + currentsave.level.ToString());
                            }
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
                            Console.WriteLine("Server = 10000 bitcoins");
                            Console.WriteLine("DataCenter = 100000 bitcoins");
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
                                    buy(currentsave,"miner", ref amount, ref amountInput);
                                    break;

                                case "gamingrig":
                                    buy(currentsave,"gamingrig", ref amount, ref amountInput);
                                    break;

                                case "server":
                                    buy(currentsave,"server", ref amount, ref amountInput);
                                    break;

                                case "datacenter":
                                    buy(currentsave,"datacenter", ref amount, ref amountInput);
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
                            saveMethod(currentsave);
                            Console.WriteLine("Save Complete");
                            continue;
                        case "quit":
                            saveMethod(currentsave);
                            Console.WriteLine("Save Complete");
                            break;
                        case "clear":
                            Console.Clear();
                            startText();
                            continue;
                        case "reset":
                            File.Delete(currentsave.pathSave);
                            currentsave.bitcoins = 0;
                            currentsave.miners = 0;
                            currentsave.datacenters = 0;
                            currentsave.level = 1;
                            saveMethod(currentsave);
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

        public static void saveMethod(Values currentsave)
        {
            if (!File.Exists(currentsave.pathSave))
            {
                string[] newSave = new string[4];
                for (int i = 0; i < 3; i++)
                {
                    newSave[i] = "0";
                }
                newSave[3] = "1";
                File.WriteAllLines(currentsave.pathSave, newSave);
            }
            string[] savefile = new string[File.ReadLines(currentsave.pathSave).Count()];
            savefile = File.ReadAllLines(currentsave.pathSave);
            savefile[0] = currentsave.bitcoins.ToString();
            savefile[1] = currentsave.miners.ToString();
            savefile[2] = currentsave.gamingrigs.ToString();
            savefile[3] = currentsave.servers.ToString();
            savefile[4] = currentsave.datacenters.ToString();
            savefile[5] = currentsave.level.ToString();
            File.WriteAllLines(currentsave.pathSave, savefile);
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

        public static void buy(Values currentsave,string item, ref double amount, ref string amountInput)
        {
            if (amountInput == "max")
            {
                amount = currentsave.bitcoins / price(item);
                amount = Math.Floor(amount);
            }
            if (currentsave.bitcoins >= (price(item) * amount))
            {
                currentsave.bitcoins -= price(item) * amount;
                switch (item)
                {
                    case "miners":
                        currentsave.miners += amount;
                        break;
                    case "gamingrigs":
                        currentsave.gamingrigs += amount;
                        break;
                    case "servers":
                        currentsave.servers += amount;
                        break;
                    case "datacenters":
                        currentsave.datacenters += amount;
                        break;

                }
                Console.WriteLine("You Bought " + amount.ToString()+ " " + item);
            }
            else
            {
                Console.Write("You can't afford that becuase you have ");
                Console.Write(currentsave.bitcoins.ToString());
                Console.Write(" bitcoins and you need ");
                Console.Write((price(item) * amount).ToString());
                Console.Write(" bitcoins you need to earn ");
                Console.WriteLine(((price(item) * amount) - currentsave.bitcoins).ToString());
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

        public static void reset()
        {


        }
    }

}

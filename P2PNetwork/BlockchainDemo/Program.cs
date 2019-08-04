using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        public static int Port = 0;
        public static P2PServer Server = null;
        //public static P2PClient Client = new P2PClient();
        public static Blockchain PhillyCoin = new Blockchain();
        public static string name = "Unknown";

        static void Main(string[] args)
        {
            PhillyCoin.InitializeChain();

            Port = 6000;
            name = "Server";
        

            if (Port > 0)
            {
                Server = new P2PServer();
                Server.Start();
            }
            if (name != "Unkown")
            {
                Console.WriteLine($"Current user is {name}");
            }

            Console.WriteLine("=========================");
            Console.WriteLine("1. Display Blockchain");
            Console.WriteLine("2. Write Data");
            Console.WriteLine("3. Exit");
            Console.WriteLine("=========================");

            int selection = 0;
            while (selection != 3)
            {
                switch (selection)
                {
                    //case 1:
                    //    Console.WriteLine("Please enter the server URL");
                    //    string serverURL = Console.ReadLine();
                    //    Client.Connect($"{serverURL}/Blockchain");
                    //    break;
                    //case 2:
                    //    Console.WriteLine("Please enter the receiver name");
                    //    string receiverName = Console.ReadLine();
                    //    Console.WriteLine("Please enter the amount");
                    //    string amount = Console.ReadLine();
                    //    PhillyCoin.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
                    //    PhillyCoin.ProcessPendingTransactions(name);
                    //    Client.Broadcast(JsonConvert.SerializeObject(PhillyCoin));
                    //    break;
                    case 1:
                        Console.WriteLine("Blockchain");
                        Console.WriteLine(JsonConvert.SerializeObject(PhillyCoin, Formatting.Indented));
                        break;
                    case 2:
                        Console.WriteLine("Please enter Text to blockchain");
                        string data = Console.ReadLine();
                        PhillyCoin.AddBlock(new Block(DateTime.Now, null, "{" + data + "}"));
                        //Client.Broadcast(JsonConvert.SerializeObject(PhillyCoin));
                        try
                        {
                            Server.Boardcast(JsonConvert.SerializeObject(PhillyCoin));
                        } catch (Exception ex) { throw new Exception(ex.ToString()); }
                        
                        break;

                }

                Console.WriteLine("Please select an action");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }

            //Client.Close();
            
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using ITP213.DAL;
using System.Diagnostics;

namespace Blockchain_Text
{
    public class P2PClient
    {
        IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();
        static WebSocket ws = null;

        public static void Connect(string url)
        {
            ws = new WebSocket("ws://127.0.0.1:6000/Blockchain");
            ws.OnOpen += (sender, e) =>
            {
                ws.Send("Hi Server");
                //ws.Send(JsonConvert.SerializeObject(Program.PhillyCoin));
            };


            ws.OnMessage += (sender, e) =>
            {
                if (e.Data.IndexOf("Hi Client") != -1)
                {
                    Debug.WriteLine(e.Data);
                    string parseData = e.Data.Substring(e.Data.IndexOf("Hi Client") + "Hi Client".Length+ 1);
                    Program.PhillyCoin = JsonConvert.DeserializeObject<Blockchain>(parseData);
                    Debug.WriteLine(JsonConvert.SerializeObject(Program.PhillyCoin));

                }
                else
                {
                    Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);
                    if (newChain.IsValid() && newChain.Chain.Count > Program.PhillyCoin.Chain.Count)
                    {


                        //List<Transaction> newTransactions = new List<Transaction>();
                        //newTransactions.AddRange(newChain.PendingTransactions);
                        //newTransactions.AddRange(Program.PhillyCoin.PendingTransactions);

                        //newChain.PendingTransactions = newTransactions;
                        Program.PhillyCoin = newChain;
                    }
                }
            };
            ws.Connect();
            Debug.WriteLine(JsonConvert.SerializeObject(Program.PhillyCoin));
            //wsDict.Add(url, ws);

        }

        public static void Send(string data)
        {
            //foreach (var item in wsDict)
            //{
            //    if (item.Key == url)
            //    {
            //        item.Value.Send(data);
            //    }
            //}
            ws.Send(data);
        }

        public void Broadcast(string data)
        {
            foreach (var item in wsDict)
            {
                item.Value.Send(data);
            }
        }

        public IList<string> GetServers()
        {
            IList<string> servers = new List<string>();
            foreach (var item in wsDict)
            {
                servers.Add(item.Key);
            }
            return servers;
        }

        public void Close()
        {
            foreach (var item in wsDict)
            {
                item.Value.Close();
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Diagnostics;

namespace BlockchainDemo
{
    public class P2PServer: WebSocketBehavior
    {
        bool chainSynched = false;
        WebSocketServer wss = null;

        public void Start()
        {
            wss = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            wss.AddWebSocketService<P2PServer>("/Blockchain");
            wss.Start();
            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");
        }

        public void Boardcast(string msg)
        {
            wss.WebSocketServices.Broadcast(msg);
        }
        
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("Hi Client " + JsonConvert.SerializeObject(Program.PhillyCoin));
            }
            else
            {
                Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);
                
                if (newChain.IsValid() && newChain.Chain.Count > Program.PhillyCoin.Chain.Count)
                {
                    Program.PhillyCoin = newChain;
                    Debug.WriteLine(JsonConvert.SerializeObject(Program.PhillyCoin));
                }

                if (!chainSynched)
                {
                    Send(JsonConvert.SerializeObject(Program.PhillyCoin));
                    chainSynched = true;
                }
            }
        }
    }
}

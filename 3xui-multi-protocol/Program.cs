using Newtonsoft.Json;




while (true)
{

    using var db = new MultiProtocolContext();
    var Clients = db.Client_Traffics
       .ToList();
    if (!File.Exists("LocalDB.json"))
    {
        localDB local = new localDB() { Sec = 10, clients = Clients };
        var LocalD =File.Create("LocalDB.json");
        using( var writer = new StreamWriter(LocalD))
        {
            writer.Write(JsonConvert.SerializeObject(local));
        }
        LocalD.Close();
    }
    localDB localDB = JsonConvert.DeserializeObject<localDB>(File.ReadAllText("LocalDB.json"));

   
    List<Client> ALLClients = new List<Client>();

    var inbounds = db.Inbounds.ToList();
    foreach (var item in inbounds)
    {
        inboundsetting setting = JsonConvert.DeserializeObject<inboundsetting>(item.Settings);
        ALLClients.AddRange(setting.clients);
    }

    List<Client> FinalClients = new List<Client>();
    List<Client_Traffics> FinalClients_Traffic = new List<Client_Traffics>();

    foreach (var client in ALLClients)
    {
        if (!FinalClients.Any(x => x.subId == client.subId))
        {
            if (ALLClients.Where(x => x.subId == client.subId).Count() > 1)
            {

                List<Client> Calculate = ALLClients.Where(x => x.subId == client.subId).ToList();
                List<Client_Traffics> Calculate2 = new List<Client_Traffics>();
                foreach (var client2 in Calculate)
                {
                    Calculate2.Add(Clients.Where(x => x.Email == client2.email).FirstOrDefault());
                }

                Int64? maxTotalGB = Calculate.Max(x => x.totalGB);
                Int64? maxTotal = Calculate2.Max(x => x.Total);

                Int64? maxUP = Calculate2.Max(x => x.Up);
                Int64? maxDOWN = Calculate2.Max(x => x.Down);
                Int64? UP = 0;
                Int64? DOWN = 0;

                Int64? DateMax= Calculate2.Max(x => x.Expiry_Time);
                Int64? DateMin= Calculate2.Min(x => x.Expiry_Time);
                Int64? ExpireTime=0;
                if (DateMax > 0)
                {
                    ExpireTime = DateMax;
                }
                else if (DateMin < 0)
                    ExpireTime = DateMin;
                try
                {
                    foreach (var client2 in Calculate2)
                    {
                        
                        if (client2.Up != maxUP)
                        {
                            Int64? oldusage = localDB.clients.Where(x => x.Email == client2.Email).First().Up;
                            if (client2.Up > oldusage && oldusage != 0 && oldusage != null)
                                UP += client2.Up - oldusage;
                        }
                        if (client2.Down != maxDOWN)
                        {
                            Int64? oldusage = localDB.clients.Where(x => x.Email == client2.Email).First().Down;

                            if (client2.Down > oldusage && oldusage != 0 && oldusage != null)
                                DOWN += client2.Down - oldusage;
                        }
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }

                bool check = false;
                if (Calculate2.Where(x => x.Enable == true).Count() == 1)
                {
                    check = true;
                }

                foreach (var cal2 in Calculate2)
                {
                    cal2.Total = maxTotal;
                    cal2.Up = maxUP+UP;
                    cal2.Down = maxDOWN+DOWN;
                    cal2.Expiry_Time = ExpireTime;
                    FinalClients_Traffic.Add(cal2);

                }
                foreach (var cal in Calculate)
                {
                    cal.totalGB = maxTotalGB;
                    cal.expiryTime = ExpireTime;
                    FinalClients.Add(cal);
                }
            }

        }

    }


    db.Client_Traffics.UpdateRange(FinalClients_Traffic);

    List<Inbound> FinalInbounds = new List<Inbound>();
    try {
        foreach (var inbound in db.Inbounds)
        {
            if(inbound.Protocol== "vmess" || inbound.Protocol == "vless")
            {
                inboundsetting setting = JsonConvert.DeserializeObject<inboundsetting>(inbound.Settings);
                var clis = FinalClients_Traffic.Where(x => x.Inbound_Id == inbound.Id).ToList();
                List<Client> addtoInbound = new List<Client>();
                foreach (var client in clis)
                {
                    addtoInbound.Add(FinalClients.Where(x => x.email == client.Email).FirstOrDefault());

                }
                if (addtoInbound.Count() > 0)
                {

                    List<Client> pastclients = new List<Client>();
                    foreach (Client client in setting.clients)
                        if (!addtoInbound.Any(x => x.email == client.email)) { pastclients.Add(client); }
                    pastclients.AddRange(addtoInbound);
                    setting.clients = pastclients;
                    inbound.Settings = JsonConvert.SerializeObject(setting);
                    FinalInbounds.Add(inbound);
                }
            }
            
        }

    }
    catch (Exception e) { Console.WriteLine(e.Message); }
    db.Inbounds.UpdateRange(FinalInbounds);
    db.SaveChanges();
    var client_Traffics = new MultiProtocolContext().Client_Traffics
       .ToList();

     localDB updateLocal = new localDB() { Sec = localDB.Sec, clients = client_Traffics };
    File.Delete("LocalDB.json");
    var file = File.Create("LocalDB.json");
    StreamWriter streamWriter = new StreamWriter(file);
        streamWriter.Write(JsonConvert.SerializeObject(updateLocal));
        streamWriter.Close();
    file.Close();

    Console.WriteLine("Done");
    Thread.Sleep(25 * 1000);

}







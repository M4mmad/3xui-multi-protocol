using Newtonsoft.Json;

while (true)
{
    Thread.Sleep(5000);
    using var db = new MultiProtocolContext();

    var Clients = db.Client_Traffics
        .ToList();

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
                foreach (var cal in Calculate)
                {
                    cal.totalGB = maxTotalGB;
                    FinalClients.Add(cal);
                }
                foreach (var cal2 in Calculate2)
                {
                    cal2.Total = maxTotal;
                    cal2.Up = maxUP;
                    cal2.Down = maxDOWN;
                    FinalClients_Traffic.Add(cal2);
                }
            }

        }

    }


    db.Client_Traffics.UpdateRange(FinalClients_Traffic);

    List<Inbound> FinalInbounds = new List<Inbound>();
    foreach (var inbound in db.Inbounds)
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
    db.Inbounds.UpdateRange(FinalInbounds);
    db.SaveChanges();
    Console.WriteLine("Done");
}







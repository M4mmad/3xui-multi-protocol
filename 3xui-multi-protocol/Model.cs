using Microsoft.EntityFrameworkCore;

public class MultiProtocolContext : DbContext
{
    public DbSet<Inbound> Inbounds { get; set; }
    public DbSet<Client_Traffics> Client_Traffics { get; set; }

    public string DbPath { get; }

    public MultiProtocolContext()
    {
        var folder = "/etc/x-ui/";
     
        DbPath = System.IO.Path.Join(folder, "x-ui.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}



public class Inbound
{
    public int? Id { get; set; }
    public string? listen { get; set; }
    public int? user_id { get; set; }
    public Int64? Up { get; set; }
    public Int64? Down { get; set; }
    public Int64? Total { get; set; }
    public string? Settings { get; set; }
    public string? tag { get; set; }
    public string? sniffing { get; set; }
    public string? Stream_Settings { get; set; }
    public string? Remark { get; set; }
    public bool? Enable { get; set; }
    public Int64? Expiry_Time { get; set; }

    public int? Port { get; set; }
    public string? Protocol { get; set; }
}




public class Client_Traffics
{
    public int? Id { get; set; }
    public int? Inbound_Id { get; set; }
    public int? Reset { get; set; }
    public string? Email { get; set; }
    public Int64? Up { get; set; }
    public Int64? Down { get; set; }
    public Int64? Total { get; set; }
    public Int64? Expiry_Time { get; set; }
    public bool? Enable { get; set; }

}

public class Client
{
    public string? email { get; set; }
    public bool? enable { get; set; }
    public Int64? expiryTime { get; set; }
    public string? flow { get; set; }
    public string? id { get; set; }
    public int? limitIp { get; set; }
    public bool? reset { get; set; }
    public string? subId { get; set; }
    public string? tgId { get; set; }
    public Int64? totalGB { get; set; }

}

public class inboundsetting
{
    public List<Client> clients { get; set; }
    public string decryption { get; set; }
    public List<object> fallbacks { get; set; }
}

public class localDB
{
    public int Sec { get; set; }

    public List<Client_Traffics> clients { get; set; }
}

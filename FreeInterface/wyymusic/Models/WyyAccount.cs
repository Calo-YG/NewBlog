namespace FreeInterface.wyymusic.Models
{
    public class WyyAccount
    {
        public bool AnonimousUser { get; set; }

        public int Ban { get; set; }

        public int BaoyueVersion { get; set; }

        public long CreateTime { get; set; }    

        public int DonateVersion { get; set; }  

        public long Id { get; set; }    

        public bool PaidFee { get; set; }   

        public int TokenVersion { get; set; }

        public int Type { get; set; }

        public string Username { get; set; }

        public int VipType { get; set; }

        public int WhitelistAuthority { get; set; }
    }
}

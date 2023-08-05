namespace FreeInterface.wyymusic.Models
{
    public class WyyProfile
    {
        public int AccountStatus { get; set; }  

        public int AccountType { get; set; }

        public bool Anchor { get; set; }    

        public int AuthStatus { get; set; }

        public int Authenticated { get; set; }  

        public int AuthenticationTypes { get; set; }  
        
        public int Authority { get; set; }

        public string AvatarimgId { get; set; } 

        public string Avatarurl { get; set;}

        public string BackgroundImgId { get; set; } 

        public string BackgroundUrl { get; set; }   

        public string Birthday { get; set; }    

        public int City { get; set; }

        public long CreatTime { get; set; }

        public bool DefaultAvatar { get; set; }

        public string? Description { get; set; }

        public int DjStatus { get; set; }
    
        public bool Followed { get; set; }  

        public int Gender { get; set; }

        public string LastLoginIP { get; set; }

        public long LastLoginTime { get; set; }

        public int LocationStatus { get; set; }

        public bool Mutual { get; set; }    

        public string NickName { get; set; }

        public int Province { get; set; }

        public string ShortUserName { get; set; }

        public string Signatrue { get; set; }

        public long UserId { get; set; }

        public string Username { get; set; }

        public int VipType { get; set; }

        public int WhitelistAuthority { get; set; }
    }
}

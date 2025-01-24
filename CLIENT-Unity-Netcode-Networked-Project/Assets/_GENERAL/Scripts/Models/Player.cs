namespace Game.Model
{
    public class JSON
    {
        public class Player
        {
            public string gamertag;
            public string steam_id;
        }

        public class Time
        {
            public int Year, Month, Day;
            public int Hours, Minutes, Second;
        }
    }
}
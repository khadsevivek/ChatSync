namespace SigletoneDesignPatternImplementation.Services
{
    public class Singletone
    {
        //Atchual instence
        private static Singletone _instence { get; set; }
        //Lock object verible for thread safety
        private static readonly object _lock  = new();
        //Constructor to prevent object creation
        public static Guid TestId { get; private set; }
        private Singletone(){}

        //method to return the instence of a singletone class
        public static Singletone getInstence()
        {
            if(_instence == null)
            {
                //Lock new thred till completion of old one
                lock (_lock)
                {
                    if (_instence == null)
                    {
                        _instence = new Singletone();
                        TestId = Guid.NewGuid();
                    }
                }
                
            }
            return _instence;
        }
    }
}

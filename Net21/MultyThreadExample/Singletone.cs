namespace MultyThreadExample
{
    public class Singletone
    {
        private Singletone() { }

        private static object obj = new object();

        private static Singletone instanse;

        public static Singletone GetSingletone()
        {

            if (instanse == null)
            {
                lock (obj)
                {
                    if (instanse == null)
                    {
                        instanse = new Singletone();
                    }
                }
            }

            return instanse;
        }
    }
}

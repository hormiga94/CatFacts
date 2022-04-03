namespace CF_Utility
{
    public class CatFact
    {
        private string _fact;
        private int _length;
        public string Fact
        {
            get => _fact;
        }
        public int Length
        {
            get => _length;
        }

        public CatFact(string fact, int length)
        {
            _fact = fact;
            _length = length;
        }

    }
}
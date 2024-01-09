namespace Rubius.Models
{
    public class LoadData<T>
    {
        public T Value;

        public LoadData(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }
    }
}

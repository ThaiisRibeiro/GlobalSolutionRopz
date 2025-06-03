namespace GlobalSolutionRopz.Model
{
    public class WeatherResponse
    {
        public Main Main { get; set; }
        public string Name { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
    }
}


namespace Services.Models
{
    public abstract class Species
    {
        public int minimum_length { get; set; }
        public int[][] fishCount { get; set; }
        public int maximum_length { get; set; }
    }
}

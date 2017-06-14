namespace ReTagger
{
    public enum Operations { Add, Remove, Set}
    public class Modification
    {
        public Operations Operation { get; set; }
        public Tag Tag { get; set; }
    }
}
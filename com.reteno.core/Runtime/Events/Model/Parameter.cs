namespace Reteno.Events
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Parameter()
        {
            // noop
        }

        public Parameter(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}

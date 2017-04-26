namespace Nether.Analytics
{
    public class DictionaryBasedMessageCsvSerializerOptions
    {
        private readonly char _separator = '\t';
        private readonly bool _includeHeaderRow = false;
        private readonly string[] _columns;

        public DictionaryBasedMessageCsvSerializerOptions()
        {
        }

        public DictionaryBasedMessageCsvSerializerOptions(params string[] columns)
        {
            _columns = columns;
        }

        public DictionaryBasedMessageCsvSerializerOptions(char separator, bool includeHeaderRow = false, params string[] columns)
        {
            _separator = separator;
            _includeHeaderRow = includeHeaderRow;
            _columns = columns;
        }

        public char Separator => _separator;
        public bool IncludeHeaderRow => _includeHeaderRow;
        public string[] Columns => _columns;
    }
}

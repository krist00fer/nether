
// KEEP

using System.Threading.Tasks;

namespace Nether.Analytics.Parsers
{
    public interface IMessageParser<RawMessageFormat, ParsedMessageFormat>
    {
        Task<ParsedMessageFormat> ParseAsync(RawMessageFormat message);
    }
}
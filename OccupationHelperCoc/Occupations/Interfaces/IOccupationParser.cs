using Occupations.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Occupations.Interfaces
{
    public interface IOccupationParser
    {
        Task<IEnumerable<Occupation>> ParseDataAsync(string pathToFile);
    }
}

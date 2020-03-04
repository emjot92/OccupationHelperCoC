using Occupations.Classes;
using Occupations.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Occupations.Parsers
{
    public class OccupationParser : IOccupationParser
    {
        public async Task<IEnumerable<Occupation>> ParseDataAsync(string pathToFile)
        {
            var records = new List<string>();
            using (var file = File.OpenRead(pathToFile))
            {
                using(var reader = new StreamReader(file))
                {
                    while(!reader.EndOfStream)
                    {
                        var record = await ReadOneRecordAsync(reader);
                        records.Add(record);
                    }
                }
            }

            var occupations = new List<Occupation>();
            foreach(var record in records)
            {
                var splitted = record.Split('\t');
                if(splitted.Count() == 7)
                {
                    Enum.TryParse<OccupationType>(splitted[6], out OccupationType occupationType);
                    var occupation = new Occupation
                    {
                        Name = splitted[0],
                        Description = splitted[1],
                        OccupationSkillPoints = splitted[2],
                        MinCredit = int.Parse(splitted[3].Trim()),
                        MaxCredit = int.Parse(splitted[4].Trim()),
                        Skills = splitted[5],
                        OccupationType = occupationType
                    };
                    occupations.Add(occupation);
                }
            }
            return occupations;
        }

        private async Task<string> ReadOneRecordAsync(StreamReader reader)
        {
            string line = string.Empty;
            string record = string.Empty;
            var occupationTypesList = Enum.GetNames(typeof(OccupationType)).Select(t => $"\t{t}");
            do
            {
                line = await reader.ReadLineAsync();
                //if (line == null)
                //    line = string.Empty;
                record += line;
                var b = occupationTypesList.Select(t => line.EndsWith(t)).Any();
            }
            while (line != null && !occupationTypesList.Select(t => line.EndsWith(t)).Any(x => x));
            return record;
        }
    }
}

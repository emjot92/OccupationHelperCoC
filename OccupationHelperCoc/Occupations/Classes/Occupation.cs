namespace Occupations.Classes
{
    public class Occupation
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string OccupationSkillPoints { get; set; }

        public int MinCredit { get; set; }

        public int MaxCredit { get; set; }

        public string Skills { get; set; }

        public OccupationType OccupationType { get; set; }
    }
}

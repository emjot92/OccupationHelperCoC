using Occupations.Classes;
using ReactiveUI;

namespace OccupationHelperCoC
{
    public class OccupationViewModel : ReactiveObject
    {
        public string Name { get; }

        public string Description { get; }

        public string OccupationSkillPoints { get; }

        public int MinCredit { get; }

        public int MaxCredit { get; }

        public string Skills { get; }

        public OccupationType OccupationType { get; }
    }
}

using Occupations.Classes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OccupationHelperCoC
{
    public class OccupationViewModel : ReactiveObject
    {
        private readonly Occupation _occupation;

        public string Name => _occupation.Name;

        public string Description => _occupation.Description;

        public string OccupationSkillPoints => _occupation.OccupationSkillPoints;

        public int MinCredit => _occupation.MinCredit;

        public int MaxCredit => _occupation.MaxCredit;

        public string Skills => _occupation.Skills;

        public OccupationType OccupationType => _occupation.OccupationType;

        [Reactive]
        public int SerialNumber { get; set; }

        public OccupationViewModel(Occupation occupation)
        {
            this._occupation = occupation;
        }
    }
}

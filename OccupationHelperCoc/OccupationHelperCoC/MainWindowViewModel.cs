using DynamicData;
using OccupationHelperCoC.Extensions;
using OccupationHelperCoC.Helpers;
using Occupations.Classes;
using Occupations.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace OccupationHelperCoC
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly IOccupationParser _occupationParser;

        private readonly ReadOnlyObservableCollection<CheckedItemViewModel> _occupationTypes;

        private readonly ReadOnlyObservableCollection<OccupationViewModel> _occupations;

        public ReadOnlyObservableCollection<CheckedItemViewModel> OccupationTypes => _occupationTypes;

        public ReadOnlyObservableCollection<OccupationViewModel> Occupations => _occupations;

        [Reactive]
        public OccupationViewModel SelectedItem { get; set; }

        [Reactive]
        public double? SelectedNumber { get; set; }

        public ReactiveCommand<int, Unit> GetItemWithSerialNumber { get; set; }

        public ReactiveCommand<Unit, Unit> Random { get; set; }

        public ReactiveCommand<Unit, Unit> Initialize { get; set; }

        public MainWindowViewModel(IOccupationParser occupationParser)
        {
            this._occupationParser = occupationParser;
            var occupationTypesSourceList = new SourceList<OccupationType>();
            var occupationsSourceList = new SourceList<Occupation>();

            var occupationTypes = occupationTypesSourceList.Connect().Transform(x => new CheckedItemViewModel(x.GetDescription(), true)).Publish();
            var occupations = occupationsSourceList.Connect().Transform(x => new OccupationViewModel(x)).Publish();

            IObservable<Func<OccupationViewModel, bool>> filter = occupationTypes.AutoRefresh(x => x.IsChecked).Filter(x => x.IsChecked).Transform(x => x.Name)
                .ToCollection()
                .Throttle(TimeSpan.FromMilliseconds(250))
                .Select(items =>
                {
                    var types = items.Select(t => t.GetValueFromDescription<OccupationType>());
                    OccupationType? type = null;
                    foreach (var item in types)
                    {
                        if (type == null)
                        {
                            type = item;
                        }
                        else
                        {
                            type |= item;
                        }
                    }
                    bool Predicate(OccupationViewModel vm) => type.Value.HasFlag(vm.OccupationType);
                    return (Func<OccupationViewModel, bool>)Predicate;
                });

            occupationTypes.ObserveOn(RxApp.MainThreadScheduler).Bind(out this._occupationTypes).Subscribe();
            occupations.Filter(filter).Transform((x, i) =>
            {
                x.SerialNumber = i + 1;
                return x;
            }).ObserveOn(RxApp.MainThreadScheduler).Bind(out this._occupations).Subscribe();
            occupationTypes.Connect();
            occupations.Connect();

            Initialize = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await this._occupationParser.ParseDataAsync("Occupations.txt");
                occupationsSourceList.AddRange(result);
                occupationTypesSourceList.AddRange(Enum.GetValues(typeof(OccupationType)).Cast<OccupationType>());
            });

            GetItemWithSerialNumber = ReactiveCommand.Create<int>(number =>
            {
                SelectedItem = Occupations.SingleOrDefault(x => x.SerialNumber == number);
            });

            Random = ReactiveCommand.CreateFromTask(async () =>
            {
                var random = new Random();
                var number = random.Next(1, Occupations.Count);
                await GetItemWithSerialNumber.Execute(number);
            });
        }
    }
}

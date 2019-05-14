using System;
using System.Collections.ObjectModel;

namespace Assignment4Core.Models
{
    public class School : PlaceOfInterest
    {
        public string SchoolName => PlaceName;

        public School()
        {
        }
    }

    public class SchoolList
    {
        public ObservableCollection<School> schools { get; set; }
    }
}

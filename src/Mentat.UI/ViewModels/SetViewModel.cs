using System.Collections.Generic;
using Mentat.Repository.Models;

namespace Mentat.UI.ViewModels
{
    public class SetViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public List<Card> Cards { get; set; }

        public List<Set> Sets { get; set; }

        public SetViewModel(Set set)
        {
            Id = set.Id;
            Title = set.Title;
            Description = set.Description;
            IsPublic = set.IsPublic;
        }

        public SetViewModel(List<Set> sets)
        {
            Sets = sets;
        }

        public SetViewModel()
        {
        }
    }
}


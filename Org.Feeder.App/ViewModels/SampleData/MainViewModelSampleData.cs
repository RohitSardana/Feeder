using System.Collections.Generic;
using System.Linq;
using Org.Feeder.App.Models;

namespace Org.Feeder.App.ViewModels.SampleData
{
    public class MainViewModelSampleData : MainViewModel
    {
        private static readonly IEnumerable<PostSummary> SamplePosts = from id in Enumerable.Range(1, 100)
                                                                  select new PostSummary(id, "What is the answer to the ultimate question?");
        public MainViewModelSampleData() : base(SamplePosts)
        { }
    }
}

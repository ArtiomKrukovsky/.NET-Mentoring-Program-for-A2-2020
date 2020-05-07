namespace Reactive.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataService
    {
        public async Task<IEnumerable<string>> GetAsync(string searchTerm)
        {
            return await Task.Factory.StartNew(
           () =>
               {
                   var words = Data._rxWikiPage.Split(' ', '\n').ToList();
                   var pairs = words.Zip(words.Skip(1), (w1, w2) => w1 + " " + w2).ToList();
                   return words.Concat(pairs).Where(w => w.Contains(searchTerm)).Distinct();
               });
        }
    }
}

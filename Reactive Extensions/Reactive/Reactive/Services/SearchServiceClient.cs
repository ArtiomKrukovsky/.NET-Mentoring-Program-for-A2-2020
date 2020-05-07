namespace Reactive.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SearchServiceClient
    {
        readonly DataService _data = new DataService();

        public async Task<IEnumerable<string>> SearchAsync(string searchTerm)
        {
            return await this._data.GetAsync(searchTerm);
        }
    }
}

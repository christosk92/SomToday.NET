using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuardAgainstLib;
using SomToday.NET.Api;

namespace SomToday.NET.Paging
{
    public class SimplePaginator : IPaginator
    {
        protected virtual Task<bool> ShouldContinue<T>(List<T> results, IPaginatable<T> page)
        {
            return Task.FromResult(true);
        }

        protected virtual Task<bool> ShouldContinue<T, TNext>(List<T> results, IPaginatable<T, TNext> page)
        {
            return Task.FromResult(true);
        }

        public async Task<IList<T>> PaginateAll<T>(IPaginatable<T> firstPage, IApiConnector connector)
        {
            GuardAgainst.ArgumentBeingNull(firstPage, nameof(firstPage));
            GuardAgainst.ArgumentBeingNull(connector, nameof(connector));

            var page = firstPage;
            var results = new List<T>();
            if (page.Items != null)
            {
                results.AddRange(page.Items);
            }

            while (page.Next != null 
                   && await ShouldContinue(results, page).ConfigureAwait(false))
            {
              /*  page = await connector.Get<Paging<T>>(new Uri(page.Next, UriKind.Absolute)).ConfigureAwait(false);
                if (page.Items != null)
                {
                    results.AddRange(page.Items);
                }*/
              //TODO: Implement custom paging
              throw new NotImplementedException();
            }

            return results;
        }

        public async Task<IList<T>> PaginateAll<T, TNext>(
            IPaginatable<T, TNext> firstPage, Func<TNext, IPaginatable<T, TNext>> mapper, IApiConnector connector
        )
        {
            GuardAgainst.ArgumentBeingNull(firstPage, nameof(firstPage));
            GuardAgainst.ArgumentBeingNull(mapper, nameof(mapper));
            GuardAgainst.ArgumentBeingNull(connector, nameof(connector));

            var page = firstPage;
            var results = new List<T>();
            if (page.Items != null)
            {
                results.AddRange(page.Items);
            }

            while (page.Next != null && await ShouldContinue(results, page).ConfigureAwait(false))
            {
                //TODO: Implement custom paging
                throw new NotImplementedException();

                /*var next = await connector.Get<TNext>(new Uri(page.Next, UriKind.Absolute)).ConfigureAwait(false);
                page = mapper(next);
                if (page.Items != null)
                {
                    results.AddRange(page.Items);
                }*/
            }

            return results;
        }
    }
}
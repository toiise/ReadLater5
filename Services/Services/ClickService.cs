using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity;
using Services.Interfaces;
using Services.ServiceModels;

namespace Services
{
    public class ClickService : IClickService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public ClickService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }
        public Clicks AddClick(string url, string userId)
        {
            var addedClick = new Clicks
            {
                Url = url,
                CreateDate = DateTime.Now
            };

            if (userId != null)
            {
                addedClick.UserID = userId;
            }

            _ReadLaterDataContext.Clicks.Add(addedClick);
            _ReadLaterDataContext.SaveChanges();

            return addedClick;

            
        }
        
        public Task<List<ClickVM>> GetMostPopularClicks()
        {
            var allClicks = _ReadLaterDataContext.Clicks.ToList().GroupBy(x => x.Url)
                .Select(a => new ClickVM()
                    {
                        Url = a.First().Url,
                        TotalClicks = a.Count(),

                    }
                ).ToList();
            allClicks.OrderBy(x => x.TotalClicks).Take(5);
            return Task.FromResult(allClicks);
        }

        public Task<List<ClickVM>> GetMostPopularClicksToday()
        {
            
                var allClicks = _ReadLaterDataContext.Clicks.ToList()
                    .Where(z => z.CreateDate.ToShortDateString() == DateTime.Now.ToShortDateString()).GroupBy(x=>x.Url).Select(a => new ClickVM()
                        {
                           
                            Url = a.First().Url,
                            TotalClicks = a.Count(),

                        }
                    ).ToList();


                allClicks.OrderBy(x => x.TotalClicks).Take(5);
                return Task.FromResult(allClicks);
        }
    }
}

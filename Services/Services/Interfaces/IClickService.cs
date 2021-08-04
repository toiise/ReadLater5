using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Services.ServiceModels;

namespace Services.Interfaces
{
  public interface IClickService
    {
        Clicks AddClick(string url, string userId);
        Task<List<ClickVM>> GetMostPopularClicks();
        Task<List<ClickVM>> GetMostPopularClicksToday();
        
    } 
   
}

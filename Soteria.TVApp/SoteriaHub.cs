using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Soteria.DataComponents.DataContext;
using Soteria.DataComponents.ViewModel.Common;
using Soteria.TVApp.Controllers;
using Soteria.TVApp.Models;

namespace Soteria.TVApp
{
    public class SoteriaHub : Hub
    {

        public List<BathroomSummaryLog> DataObject { get; private set; }

        public void Send(int SchoolId)
        {
            var searchCriteria = new SearchCriteria();
            searchCriteria.SchoolId = SchoolId;
            DataObject = StudentContext.GetBathroomSummaryLog(searchCriteria);
            Clients.All.showLiveResult(JsonConvert.SerializeObject(DataObject));
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Helpers
{
    public class SaveTrack
    {
        public void Triggers(string currentTime)
        {
            var db = new CoreToWorkflowEntities();
            var change = db.tableTracks.Add(new tableTracks());
            change.CurrentTime = currentTime;
            db.SaveChanges();
        }
    }
}
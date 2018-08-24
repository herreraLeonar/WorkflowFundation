using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using WebApp.Models;

namespace CoreToWorkflow
{

    class Program
    {
        static void Main(string[] args)
        {
            var db = new CoreToWorkflowEntities();
            int idWF = 1;
            string _duration = db.tableWorkflow.SingleOrDefault(x => x.Id == idWF).Duration.ToString();

            var parameters = new Dictionary<string, object>();
            parameters.Add("_TimeExecution", _duration);
            parameters.Add("_IdWorkflow", idWF);

            Activity _workflow = new _WfNative();
            WorkflowInvoker.Invoke(_workflow, parameters);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Activities;
using WebApp.Models;
namespace CoreToWorkflow
{
    class Tracking : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> CurrentTime { get; set; }
        [RequiredArgument]
        public InArgument<int> IdWorkflow { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var db = new CoreToWorkflowEntities();

            var change = db.tableTracks.Add(new tableTracks());
            change.CurrentTime = CurrentTime.Get(context);
            change.IdTableWorkflow = IdWorkflow.Get(context);
            db.SaveChanges();
        }
    }

    class GetAmountTasksToWorkflow : CodeActivity<int>
    {
        [RequiredArgument]
        public InArgument<int> IdWorkflow { get; set; }

        public InArgument<int> AmountTaskValue { get; set; }

        protected override int Execute(CodeActivityContext context)
        {
            var res = 0;
            int idWF = IdWorkflow.Get(context);
            var db = new CoreToWorkflowEntities();
            res = db.tableTasks.Where(x => x.IdTableEstatus == idWF).Count();
            AmountTaskValue.Set(context,res);
            return AmountTaskValue.Get(context);
        }
    }

    class ExecuteTask : CodeActivity<string>
    {
        [RequiredArgument]
        public InArgument<int> TaskNumber { get; set; }

        [RequiredArgument]
        public InArgument<int> IdWorkflow { get; set; }

        protected override string Execute(CodeActivityContext context)
        {
            var db = new CoreToWorkflowEntities();
            int idWF = IdWorkflow.Get(context);
            int _position = TaskNumber.Get(context);
            var positionTaskInTable = db.tableTasks.Where(x => x.IdTableWorkflow == idWF).OrderBy(j=> j.Id).ToList();
            var nameFunction = positionTaskInTable[_position].TaskFunction;

            var obj = new _Tasks();

            try
            {
                obj.GetType().GetMethod(nameFunction).Invoke(obj, null);
                return "se ejecuto la función: " + nameFunction;
            }
            catch
            {
                return "no existe la función: " + nameFunction;
            }
        }
    }
}

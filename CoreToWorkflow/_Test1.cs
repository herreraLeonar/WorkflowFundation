using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.Activities.Statements;
using System.IO;

namespace CoreToWorkflow
{
    class _Test1 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> _TimeExecution { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Variable<string> _CurrentTime = new Variable<string> { Name = "CurrentTime"};
            Variable<string> _AmountTasks = new Variable<string> { Name = "_AmountTasks" };
            Variable<string> _i = new Variable<string> { Name = "_i"};
            var c = System.TimeSpan.Parse(context.GetValue(_TimeExecution));
            Activity GetTrack = new Sequence
            {
                Variables = { _CurrentTime, _AmountTasks},
                Activities =
                {
                    new GetAmountTasksToWorkflow
                    {
                      Result = _AmountTasks
                    },
                    new WriteLine
                    {
                        Text = _AmountTasks
                    },
                    new Assign<string>
                    {
                        To = _i,
                        Value = "0"
                    },
                    new While
                    {
                        Condition  = int.Parse( _i.Get(context) ) < int.Parse(_AmountTasks.Get(context) ),
                        Body = new Sequence
                        {
                            Activities =
                            {
                                new Assign<string>
                                {
                                    To = _CurrentTime,
                                    Value = "momento del Fin: "+  DateTime.Now.ToString() + " duración: " + context.GetValue(_TimeExecution)
                                },
                                new WriteLine
                                {
                                    Text = "ejecutando en _i " + _i.Get(context).ToString()
                                },
                                new Delay
                                {
                                  Duration = System.TimeSpan.Parse(context.GetValue(_TimeExecution))
                                },
                                new Tracking
                                {
                                    CurrentTime = _CurrentTime
                                },
                                new WriteLine
                                {
                                    Text = _CurrentTime
                                },
                                new Assign<string>
                                {
                                    To = _i,
                                    Value = ( int.Parse(_i.ToString() ) + 1 ).ToString()
                                }
                            }
                        }
                    }
                }
            };
            WorkflowInvoker.Invoke(GetTrack);
        }
    }
}
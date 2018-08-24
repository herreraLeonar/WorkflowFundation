using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.Activities.Statements;
using System.IO;
using System.Windows.Markup;

namespace CoreToWorkflow
{
    class _WfNative : NativeActivity
    {
        [RequiredArgument]
        public InArgument<string> _TimeExecution { get; set; }
        [RequiredArgument]
        public InArgument<int> _IdWorkflow { get; set; }
        /*[DependsOnAttribute("Variables")]
        public Activity<bool> Condition { get; set; }*/
        protected override void Execute(NativeActivityContext context)
        {

            Variable<string> _CurrentTime = new Variable<string> { Name = "CurrentTime"};
            Variable<string> _resTask = new Variable<string> { Name = "_resTask" };
            Variable<int> _AmountTasks = new Variable<int> { Name = "_AmountTasks", Default = 0 };
            Variable<int> _i = new Variable<int> { Name="_i" , Default = 0 };
            Variable<bool> _condition = new Variable<bool> { Name="condit", Default=true };
            var c = System.TimeSpan.Parse(context.GetValue(_TimeExecution));
            Activity executeTasksToWorkflow = new Sequence
            {
                Variables = { _CurrentTime, _AmountTasks, _i, _condition, _resTask },
                Activities =
                {
                    new GetAmountTasksToWorkflow
                    {
                      IdWorkflow = context.GetValue(_IdWorkflow),
                      Result = _AmountTasks
                    },
                    new WriteLine
                    {
                        Text = new InArgument<string>((e)=> "Cantidad de tareas: " + _AmountTasks.Get(e)), 
                    },
                    new Assign<int>
                    {
                        To = _i,
                        Value = 0
                    },
                    new While
                    {
                        Condition  = _condition,
                        Body = new Sequence
                        {
                            Activities =
                            {
                                new Delay
                                {
                                  Duration = System.TimeSpan.Parse(context.GetValue(_TimeExecution))
                                },
                                new Assign<string>
                                {
                                    To = _CurrentTime,
                                    Value = DateTime.Now.ToString()
                                },
                                new Tracking
                                {
                                    IdWorkflow = context.GetValue(_IdWorkflow),
                                    CurrentTime = _CurrentTime
                                },
                                new WriteLine
                                {
                                    Text = _CurrentTime
                                },
                                new ExecuteTask
                                {
                                    TaskNumber = new InArgument<int>((env) => _i.Get(env) ),
                                    IdWorkflow = context.GetValue(_IdWorkflow),
                                    Result = _resTask
                                },
                                new WriteLine
                                {
                                    Text = new InArgument<string>((env) => "ejecutando en tarea "+ (_i.Get(env)+1))
                                },
                                new Assign<int>
                                {
                                    To = _i,
                                    Value =  new InArgument<int>((env) => _i.Get(env)+1)
                                },
                                new WriteLine
                                {
                                    Text = _resTask
                                },
                                new If
                                {
                                    Condition = new InArgument<bool>( (env) => _i.Get(env) >= _AmountTasks.Get(env) ),
                                    Then = new Assign<bool>
                                    {
                                        To=_condition,
                                        Value=false
                                    }
                                }
                            }
                        }
                    },
                    new WriteLine
                    {
                        Text = "finalizado Worflow nro " + context.GetValue(_IdWorkflow)
                    }
                }

            };
            WorkflowInvoker.Invoke(executeTasksToWorkflow);
        }
    }
}
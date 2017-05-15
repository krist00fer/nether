using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nether.Analytics
{
    public class PipelineScheduler
    {
        private PipelineSchedulerOptions _options;
        private Pipeline _pipeline;
        private ISchedulerSynchronizer _synchronizer;

        public PipelineScheduler(Pipeline pipeline, PipelineSchedulerOptions hourlyOnTheHour, ISchedulerSynchronizer synchronizer)
        {
            _pipeline = pipeline;
            _options = hourlyOnTheHour;
            _synchronizer = synchronizer;
        }
    }

    public enum PipelineSchedulerOptions
    {
        HourlyOnTheHour
    }

    public class Pipeline
    {
        private readonly PipelineState _startState;
        private readonly IPipelineAction[] _actions;

        public Pipeline(params IPipelineAction[] actions)
        {
            _startState = new PipelineState();
            _actions = actions;
        }

        public Pipeline(Dictionary<string, string> initialState, params IPipelineAction[] actions) :
            this(new PipelineState(initialState), actions)
        {
        }

        public Pipeline(PipelineState pipelineStartState, params IPipelineAction[] actions)
        {
            _startState = pipelineStartState;
            _actions = actions;
        }

        public async Task<bool> ProcessAsync()
        {
            var pipelineState = _startState.Clone();

            foreach (var action in _actions)
            {
                var result = await action.ProcessAsync(pipelineState);
            }
            throw new NotImplementedException();
        }
    }

    public class PipelineState : IPropertyStore
    {
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public PipelineState()
        {
            Properties = new Dictionary<string, string>();
        }

        public PipelineState(Dictionary<string, string> properties)
        {
            Properties = properties;
        }

        public PipelineState Clone()
        {
            return new PipelineState(Properties);
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.AppendLine($"PipelineState Properties:");

            foreach (var prop in Properties.Keys)
            {
                str.AppendLine($"    {prop}: {Properties[prop]}");
            }

            return str.ToString();
        }
    }

    public interface IPropertyStore
    {
        Dictionary<string, string> Properties { get; }
    }

    public interface IPipelineAction
    {
        Task<bool> ProcessAsync(IPropertyStore pipelineState);
    }

    public interface ISchedulerSynchronizer
    {
        bool GetLock();
        DateTime LastJobTime { get; set; }
    }

    public class ConsoleOutPipelineAction : IPipelineAction
    {
        public Task<bool> ProcessAsync(IPropertyStore pipelineState)
        {
            if (pipelineState.Properties.TryGetValue("msg", out var value))
            {
                Console.WriteLine(value);
            }
            else
            {
                Console.WriteLine("ConsoleOutPipelineAction executed at " + DateTime.UtcNow + " UTC");
            }

            return Task.FromResult(true);
        }
    }

    public class SetPropertyPipelineAction : IPipelineAction
    {
        Dictionary<string, string> _properties;

        public SetPropertyPipelineAction(Dictionary<string, string> properties)
        {
            _properties = properties;
        }
        public SetPropertyPipelineAction(string property, string value)
            : this(new Dictionary<string, string> { { property, value} })
        {
        }

        public SetPropertyPipelineAction(string property, object value)
             : this(new Dictionary<string, string> { { property, value.ToString() } })
        {
        }

        public Task<bool> ProcessAsync(IPropertyStore pipelineState)
        {
            foreach (var prop in _properties)
            {
                pipelineState.Properties[prop.Key] = prop.Value;
            }

            return Task.FromResult(true);
        }
    }

    public class UtcNowPipelineAction : DateTimePipelineAction
    {
        public UtcNowPipelineAction(string prefix = "")
            : base(DateTime.UtcNow, prefix)
        {
        }
    }

    public class DateTimePipelineAction : IPipelineAction
    {
        string _prefix;
        DateTime _dateTime;

        public DateTimePipelineAction(DateTime dateTime, string prefix = "")
        {
            _dateTime = dateTime;
            _prefix = prefix;
        }

        public Task<bool> ProcessAsync(IPropertyStore pipelineState)
        {
            pipelineState.Properties[PropertyName(_prefix, "year")] = _dateTime.Year.ToString("D4");
            pipelineState.Properties[PropertyName(_prefix, "month")] = _dateTime.Month.ToString("D2");
            pipelineState.Properties[PropertyName(_prefix, "day")] = _dateTime.Day.ToString("D2");
            pipelineState.Properties[PropertyName(_prefix, "hour")] = _dateTime.Hour.ToString("D2");
            pipelineState.Properties[PropertyName(_prefix, "minute")] = _dateTime.Minute.ToString("D2");
            pipelineState.Properties[PropertyName(_prefix, "second")] = _dateTime.Second.ToString("D2");
            pipelineState.Properties[PropertyName(_prefix, "now")] = _dateTime.ToString();

            return Task.FromResult(true);
        }

        private string PropertyName(string prefix, string name)
        {
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return name;
            }
            else
            {
                return prefix + name[0].ToString().ToUpper() + name.Substring(1);
            }
        }
    }

    public class DebugPipelineAction : IPipelineAction
    {
        public Task<bool> ProcessAsync(IPropertyStore pipelineState)
        {
            Console.WriteLine(pipelineState);

            return Task.FromResult(true);
        }
    }
}

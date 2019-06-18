using Veruthian.Library.Collections;

namespace Veruthian.Library.Steps.Actions
{
    public class BooleanStep : BaseStep, IActionStep
    {
        public BooleanStep(bool? value, bool atStart = false, bool atComplete = false)
        {
            this.Value = value;

            this.AtStart = atStart;

            this.AtComplete = atComplete;
        }

        public override string Type => TypeName;

        public override string Name => Value == true ? "True" : Value == false ? "False" : "Null";


        public bool? Value { get; }

        public bool AtStart { get; }

        public bool AtComplete { get; }


        public const string TypeName = "Boolean";

        public void Act(StepWalker walker, ObjectTable states)
        {
            if (!walker.StepCompleted)
            {
                if (AtStart)
                    walker.State = Value;
            }
            else
            {
                if (AtComplete)
                    walker.State = Value;
            }
        }
    }
}
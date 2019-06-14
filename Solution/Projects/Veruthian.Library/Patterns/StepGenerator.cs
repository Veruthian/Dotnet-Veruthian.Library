using System.Collections.Generic;
using Veruthian.Library.Collections.Extensions;
using Veruthian.Library.Utility;

namespace Veruthian.Library.Patterns
{
    public class StepGenerator
    {
        public BooleanStep True => BooleanStep.True;

        public BooleanStep False => BooleanStep.False;

        public BooleanStep Null => BooleanStep.Null;


        // Sequence
        public GeneralStep Sequence(params IStep[] steps) => Sequence((IEnumerable<IStep>)steps);

        public GeneralStep Sequence(IEnumerable<IStep> steps)
        {
            var first = new GeneralStep();

            var current = first;

            foreach (var step in steps)
            {
                if (current != first)
                {
                    var next = new GeneralStep();

                    current.Next = next;

                    current = next;
                }

                current.Down = step;
            }

            return first;
        }


        // Choice
        public GeneralStep Choice(params IStep[] steps)
        {
            var first = new GeneralStep();

            var current = first;

            foreach (var step in steps)
            {
                if (current != first)
                {
                    var next = new GeneralStep();
                    
                    current.Next = next;

                    current = next;
                }

                current.Shunt = current.Down = step;
            }

            current.Next = False;

            return first;
        }

        // Optional
        public GeneralStep Optional(IStep step)
        {
            ExceptionHelper.VerifyNotNull(step, nameof(step));

            return new GeneralStep
            {
                Shunt = step,

                Down = step,

                Next = True
            };
        }


        // If
        public GeneralStep If(IStep condition)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            return new GeneralStep
            {
                Shunt = condition,

                Down = True,

                Next = False
            };
        }

        public GeneralStep IfThen(IStep condition, IStep thenStep)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            ExceptionHelper.VerifyNotNull(thenStep, nameof(thenStep));

            return new GeneralStep
            {
                Shunt = condition,

                Down = thenStep,

                Next = False
            };
        }

        public GeneralStep IfElse(IStep condition, IStep elseStep)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            ExceptionHelper.VerifyNotNull(elseStep, nameof(elseStep));

            return new GeneralStep
            {
                Shunt = condition,

                Down = True,

                Next = elseStep
            };
        }

        public GeneralStep IfThenElse(IStep condition, IStep thenStep, IStep elseStep)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            ExceptionHelper.VerifyNotNull(thenStep, nameof(thenStep));

            ExceptionHelper.VerifyNotNull(elseStep, nameof(elseStep));

            return new GeneralStep
            {
                Shunt = condition,

                Down = thenStep,

                Next = elseStep
            };
        }


        // Unless
        public GeneralStep Unless(IStep condition)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            return new GeneralStep
            {
                Shunt = condition,

                Down = False,

                Next = True
            };
        }

        public GeneralStep UnlessThen(IStep condition, IStep thenStep)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            ExceptionHelper.VerifyNotNull(thenStep, nameof(thenStep));

            return new GeneralStep
            {
                Shunt = condition,

                Down = False,

                Next = thenStep
            };
        }

        public GeneralStep UnlessElse(IStep condition, IStep elseStep)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            ExceptionHelper.VerifyNotNull(elseStep, nameof(elseStep));

            return new GeneralStep
            {
                Shunt = condition,

                Down = elseStep,

                Next = True
            };
        }

        public GeneralStep UnlessThenElse(IStep condition, IStep thenStep, IStep elseStep)
        {
            ExceptionHelper.VerifyNotNull(condition, nameof(condition));

            ExceptionHelper.VerifyNotNull(thenStep, nameof(thenStep));

            ExceptionHelper.VerifyNotNull(elseStep, nameof(elseStep));

            return new GeneralStep
            {
                Shunt = condition,

                Down = elseStep,

                Next = thenStep
            };
        }


        // Repeat
        public GeneralStep Repeat(IStep step)
        {
            var repeater = new GeneralStep();

            repeater.Shunt = step;

            repeater.Next = True;

            repeater.Down = new GeneralStep
            {
                Down = step,

                Next = repeater                
            };

            return repeater;
        }

        public GeneralStep Exactly(int times, IStep step) 
        {
            var first = new GeneralStep();

            var current = first;

            for (int i = 0; i < times; i++)
            {
                if (current != first)
                {
                    var next = new GeneralStep();

                    current.Next = next;

                    current = next;
                }

                current.Down = step;
            }

            return first;
        }
    
        public GeneralStep AtMost(int times, IStep step)
        {
            var first = new GeneralStep();

            var current = first;

            for (int i = 0; i < times; i++)
            {
                if (current != first)
                {
                    var next = new GeneralStep();

                    current.Next = next;

                    current = next;
                }

                current.Shunt = step;

                var down = new GeneralStep();

                current.Down = down;

                down.Down = step;

                current = down;
            }

            return first;
        }
 
        public GeneralStep AtLeast(int times, IStep step)
        {
            var result = Exactly(times, step);

            result.Next = Repeat(step);

            return result;
        }
 
        public GeneralStep Between(int min, int max, IStep step)
        {
            var result = Exactly(min, step);

            result.Next = AtMost(max - min, step);

            return result;
        }
    }
}
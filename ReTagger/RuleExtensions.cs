using System.Collections.Generic;
using System.Linq;

namespace ReTagger
{
    public static class RuleExtensions
    {
        public static bool Apply(this Rule rule, TagLib.File file)
        {
            foreach(var condition in rule.Conditions)
            {
                if(file.Tag.GetTag<string>(condition.TagName) != condition.Value)
                {
                    return false;
                }
            }

            foreach (var action in rule.Actions) 
            {
                List<string> tagvalues;

                switch (action.Operation)
                {
                    case Operations.Add:
                        tagvalues = file.Tag.GetTag<string[]>(action.Tag.TagName).ToList();
                        tagvalues.Add(action.Tag.Value);
                        file.Tag.SetTag(action.Tag.TagName, tagvalues);
                        break;
                    case Operations.Remove:
                        tagvalues = file.Tag.GetTag<string[]>(action.Tag.TagName).ToList();
                        tagvalues.Remove(action.Tag.Value);
                        file.Tag.SetTag(action.Tag.TagName, tagvalues);
                        break;
                    case Operations.Set:
                        file.Tag.SetTag(action.Tag.TagName, action.Tag.Value);
                        break;
                    default:
                        break;
                }

            }

            return true;

        }
    }
}

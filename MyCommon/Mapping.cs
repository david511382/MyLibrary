using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommon
{
    public static class Mapping
    {
        public static void MappingArray(string[] baseArray, string[] target, Action<int, int> matchBaseToTargetAction)
        {
            int matchInTarget = 0, matchInBase = 0, indexOfTarget = 0, indexOfBase = 0, done = 0;
            while (done < baseArray.Length && indexOfBase < baseArray.Length && indexOfTarget < target.Length)
            {
                if (matchInBase >= baseArray.Length)
                {
                    matchInBase = indexOfBase;
                    indexOfTarget++;
                }

                if (matchInTarget >= target.Length)
                {
                    matchInTarget = indexOfTarget;
                    indexOfBase++;
                }

                if (baseArray[matchInBase].Equals(target[indexOfTarget]))
                {
                    matchBaseToTargetAction(matchInBase, indexOfTarget);
                    indexOfTarget++;
                    if (matchInTarget < indexOfTarget)
                        matchInTarget = indexOfTarget;
                    matchInBase = indexOfBase;
                    done++;
                }
                else
                    matchInBase++;

                if (baseArray[indexOfBase].Equals(target[matchInTarget]))
                {
                    matchBaseToTargetAction(indexOfBase, matchInTarget);
                    indexOfBase++;
                    if (matchInBase < indexOfBase)
                        matchInBase = indexOfBase;
                    matchInTarget = indexOfTarget;
                    done++;
                }
                else
                    matchInTarget++;
            }
        }
    }
}

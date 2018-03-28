using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Health
{
    /// <summary>
    /// A simple scriptable object that defines which other alignments it can harm. It can never harm
    /// any other alignment that's not a SimpleAlignment
    /// </summary>
    [CreateAssetMenu(fileName = "TeamAlignment.asset", menuName = "Team Alignment", order = 1)]
    public class TeamAlignment : ScriptableObject, IAlignmentProvider
    {
        /// <summary>
        /// A collection of other alignment objects that we can harm
        /// </summary>
        public List<TeamAlignment> opponents;

        /// <summary>
        /// Gets whether the given alignment is in our known list of opponents
        /// </summary>
        public bool CanHarm(IAlignmentProvider other)
        {
            if (other == null)
            {
                return true;
            }

            var otherAlignment = other as TeamAlignment;

            return otherAlignment != null && opponents.Contains(otherAlignment);
        }
    }
}

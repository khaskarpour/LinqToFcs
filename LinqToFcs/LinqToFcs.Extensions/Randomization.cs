using LinqToFcs.Core.Entities;
using System.Collections.Generic;

namespace LinqToFcs.Extensions
{
    public static class Randomization 
    {
        public enum RandomizationType
        {
            /// <summary>
            /// 
            /// </summary>
            Uniform,

            /// <summary>
            /// 
            /// </summary>
            Gaussian,

            /// <summary>
            /// 
            /// </summary>
            HalfMarioZero
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events"></param>
        /// <param name="randomizationType"></param>
        /// <param name="sigma"></param>
        /// <param name="zeroSigma"></param>
        /// <param name="zeroPower"></param>
        /// <returns></returns>
        //public IEnumerable<FcsEvent<double>> Randomize(
        //    this IEnumerable<FcsEvent<double>> events,
        //    RandomizationType randomizationType,
        //    double sigma,
        //    double zeroSigma,
        //    double zeroPower,
        //    IEnumerable<int> excludingColumns = null)
        //{
        //    switch(randomizationType)
        //    {
        //        case RandomizationType.HalfMarioZero:
        //            return HalfMarioZero(events, zeroSigma, zeroPower);

        //        case RandomizationType.Gaussian:
        //            return Gaussian(events, sigma);

        //    }
        //    return null;
        //}

        public static IEnumerable<FcsEvent<double>> GaussianRandomization(this IEnumerable<FcsEvent<double>> events, double sigma, IEnumerable<int> excludingColumns = null)
        {
            foreach (FcsEvent<double> ev in events)
            {
                yield return ev;
            }
        }

        public static IEnumerable<FcsEvent<double>> HalfMarioZeroRandomization(IEnumerable<FcsEvent<double>> events, double zeroSigma, double zeroPower, IEnumerable<int> excludingColumns = null)
        {
            foreach (FcsEvent<double> ev in events)
            {
                yield return ev;
            }
        }

        public static IEnumerable<FcsEvent<double>> UniformRandomization(IEnumerable<FcsEvent<double>> events, IEnumerable<int> excludingColumns = null)
        {
            foreach (FcsEvent<double> ev in events)
            {
                yield return ev;
            }
        }
    }
}

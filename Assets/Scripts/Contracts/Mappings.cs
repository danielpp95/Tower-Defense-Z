namespace Assets.Scripts.Contracts
{

    using System.Collections.Generic;
    public static class Mappings
    {
        public static Contracts.Wave Map(this Scripts.LevelEditor.UI.WaveUI waveView)
        {
            return new Contracts.Wave
            {
                Enemy = waveView.Wave.Enemy,
                InitialCountdown = waveView.Wave.InitialCountdown,
                Level = waveView.Wave.Level,
                Quantity = waveView.Wave.Quantity
            };
        }

        public static List<Contracts.Wave> Map(this List<Scripts.LevelEditor.UI.WaveUI> waveViews)
        {
            var returnList = new List<Contracts.Wave>();

            foreach (var wave in waveViews)
            {
                returnList.Add(wave.Map());
            }

            return returnList;
        }
    }
}

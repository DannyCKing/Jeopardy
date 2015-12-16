using System.Media;

namespace Jeopardy.Model
{
    public static class MySoundPlayer
    {
        private static SoundPlayer soundPlayer;

        private static bool PlayBuzzInSound = false;

        public static void PlayWrongAnswerSound()
        {
            soundPlayer = new SoundPlayer(Properties.Resources.JeopardyWrongAnswer);
            soundPlayer.Play();
        }

        public static void PlayRightAnswerSound()
        {
            soundPlayer = new SoundPlayer(Properties.Resources.JeopardyCorrect);
            soundPlayer.Play();
        }

        public static void PlayBuzzInAnswerSound()
        {
            if (PlayBuzzInSound)
            {
                soundPlayer = new SoundPlayer(Properties.Resources.JeopardyBuzzIn);
                soundPlayer.Play();
            }
        }

        public static void PlayNoAnswerSound()
        {
            soundPlayer = new SoundPlayer(Properties.Resources.JeopardyNoAnswer);
            soundPlayer.Play();
        }

        public static void PlayStartRoundSound()
        {
            soundPlayer = new SoundPlayer(Properties.Resources.JeopardyStartOfRound);
            soundPlayer.Play();
        }


    }
}

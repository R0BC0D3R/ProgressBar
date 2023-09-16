using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProgressBar
{
    public partial class Form1 : Form
    {
        // Change header to dark
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }


        DailySnapshot[] _SnapshotDictionary;

        // Hardcoded ATH values
        long _athBabyDoge = 1700000000;
        long _athBabyShib = 14000000;


        public Form1()
        {
            InitializeComponent();

            // Create hardcoded Market Cap snapshots
            CareteSnapshots();


            // Last item in array is for today
            int todaysIndex = _SnapshotDictionary.Length - 1;

            lblDate.Text = _SnapshotDictionary[todaysIndex].Date.ToLongDateString();

            tbxBabyDogeMc.Text = _SnapshotDictionary[todaysIndex].BabyDogeMc.ToString("C0");
            tbxBabyShibMc.Text = _SnapshotDictionary[todaysIndex].BabyShibMc.ToString("C0");


            // Set Market Cap progress
            double mcDifferenceNow = ((double)_SnapshotDictionary[todaysIndex].BabyShibMc / _SnapshotDictionary[todaysIndex].BabyDogeMc) * 100;

            tbxProgressMc.Text = mcDifferenceNow.ToString("N2") + "%";
            progressBar1.Value = (int)mcDifferenceNow;


            // Set ATHs
            tbxBabyDogeAth.Text = _athBabyDoge.ToString("C0");
            tbxBabyShibAth.Text = _athBabyShib.ToString("C0");

            double progressAth = ((double)_athBabyShib / _athBabyDoge) * 100;
            tbxProgressAth.Text = progressAth.ToString("N2") + "%";


            // Set Xs
            btxMcXs.Text = (_SnapshotDictionary[todaysIndex].BabyDogeMc / _SnapshotDictionary[todaysIndex].BabyShibMc).ToString() + "x";
            tbxAthXs.Text = (_athBabyDoge / _athBabyShib).ToString() + "x";


            // 24 hour progress change
            double mcDifference24hours = ((double)_SnapshotDictionary[todaysIndex - 1].BabyShibMc / _SnapshotDictionary[todaysIndex - 1].BabyDogeMc) * 100;
            double mcChange24hours = ((mcDifferenceNow - mcDifference24hours) * 100) / mcDifference24hours;
            tbxChange24hours.Text = (mcChange24hours).ToString("N2") + "%";

            // 3 day progress change
            double mcDifference3days = ((double)_SnapshotDictionary[todaysIndex - 3].BabyShibMc / _SnapshotDictionary[todaysIndex - 3].BabyDogeMc) * 100;
            double mcChange3days = ((mcDifferenceNow - mcDifference3days) * 100) / mcDifference3days;
            tbxChange3Days.Text = (mcChange3days).ToString("N2") + "%";

            // 7 day progress change
            double mcDifference7days = ((double)_SnapshotDictionary[todaysIndex - 7].BabyShibMc / _SnapshotDictionary[todaysIndex - 7].BabyDogeMc) * 100;
            double mcChange7days = ((mcDifferenceNow - mcDifference7days) * 100) / mcDifference7days;
            tbxChange7Days.Text = (mcChange7days).ToString("N2") + "%";
        }

        private void CareteSnapshots()
        {
            // Add new day as next value to array
            // Need to increment array size when adding new day
            _SnapshotDictionary = new DailySnapshot[10];

            _SnapshotDictionary[0] = new DailySnapshot { Date = new DateTime(2023, 9, 6), BabyDogeMc = 176274000, BabyShibMc = 2467000 };
            _SnapshotDictionary[1] = new DailySnapshot { Date = new DateTime(2023, 9, 7), BabyDogeMc = 176185000, BabyShibMc = 3675000 };
            _SnapshotDictionary[2] = new DailySnapshot { Date = new DateTime(2023, 9, 8), BabyDogeMc = 180266000, BabyShibMc = 2728000 };
            _SnapshotDictionary[3] = new DailySnapshot { Date = new DateTime(2023, 9, 9), BabyDogeMc = 176727000, BabyShibMc = 2265000 };
            _SnapshotDictionary[4] = new DailySnapshot { Date = new DateTime(2023, 9, 10), BabyDogeMc = 173086000, BabyShibMc = 1862000 };
            _SnapshotDictionary[5] = new DailySnapshot { Date = new DateTime(2023, 9, 11), BabyDogeMc = 171613000, BabyShibMc = 1787000 };
            _SnapshotDictionary[6] = new DailySnapshot { Date = new DateTime(2023, 9, 12), BabyDogeMc = 161701022, BabyShibMc = 3226011 };
            _SnapshotDictionary[7] = new DailySnapshot { Date = new DateTime(2023, 9, 13), BabyDogeMc = 159734940, BabyShibMc = 3691406 };
            _SnapshotDictionary[8] = new DailySnapshot { Date = new DateTime(2023, 9, 14), BabyDogeMc = 158653835, BabyShibMc = 5087416 };
            _SnapshotDictionary[9] = new DailySnapshot { Date = new DateTime(2023, 9, 15), BabyDogeMc = 159365866, BabyShibMc = 4522963 };
        }
    }
}
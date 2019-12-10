using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpfClient
{
    public class PitField
    {
        public Button pit { get; set; }
        public int value;
        public int pitNumber;

        public PitField(Button pit, int value, int pitNumber)
        {
            this.pit = pit;
            this.value = value;
            this.pitNumber = pitNumber;
        }


        

    }
}
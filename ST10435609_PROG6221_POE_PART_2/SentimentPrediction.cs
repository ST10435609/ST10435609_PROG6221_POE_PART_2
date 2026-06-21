using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ML.Data;

namespace ST10435609_PROG6221_POE_PART_3
{//start of namespace


    public class SentimentPrediction
    {//start of SentimentPrediction class
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }


    }//end of SentimentPrediction class



}//end of nameapsce
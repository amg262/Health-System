﻿using IS_Proj_HIT.Entities;

namespace IS_Proj_HIT.ViewModels
{
    public class CareAssessmentPageModel
    {
        public Pcarecord Assessment { get; set; }
        public Encounter Encounter { get; set; }
        public Patient Patient { get; set; }
    }
}
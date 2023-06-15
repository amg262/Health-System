$(document).ready(function() {
    
    /*
    *   Toggle accordion chevron 
    */
    
    //TODO: Add Expand/Collapse all for the accordion. See Physician Discharge for an example of it. Might involve reworking some of this existing code as I had initially written it without planning for an option to expand/collapse all 
    
    $('#presentIllnessHeader button').on('click', () => $('#presentIllnessHeader i').toggleClass('fa-chevron-right fa-chevron-down'));
    $('#allergiesAndMedicationsHeader button').on('click', () => $('#allergiesAndMedicationsHeader i').toggleClass('fa-chevron-right fa-chevron-down'));
    $('#socialAndFamilyHistoryHeader button').on('click', () => $('#socialAndFamilyHistoryHeader i').toggleClass('fa-chevron-right fa-chevron-down'));
    $('#reviewOfSystemsHeader button').on('click', () => $('#reviewOfSystemsHeader i').toggleClass('fa-chevron-right fa-chevron-down'));
    $('#physicalExamHeader button').on('click', () => $('#physicalExamHeader i').toggleClass('fa-chevron-right fa-chevron-down'));
    $('#diagnosticsAndPlanHeader button').on('click', () => $('#diagnosticsAndPlanHeader i').toggleClass('fa-chevron-right fa-chevron-down'));
    
    /*
    *   Provider / Cosigner dropdown 
    */
    
    const providerDropdown = $('#provider-dropdown');
    const cosignerDropdown = $('#cosigner-dropdown');
    let showCosignerDropdown = $('#provider-dropdown option:selected').data('cosigner') ?? 'false';
    
    const setCosignerDropdownDisplay = () => {
        if (showCosignerDropdown.toLowerCase() === 'true') cosignerDropdown.show();
        else cosignerDropdown.hide();
    };
    
    setCosignerDropdownDisplay();
    
    $(providerDropdown).change(() => {
        showCosignerDropdown = $('#provider-dropdown option:selected').data('cosigner');
        setCosignerDropdownDisplay();
    });
    
    /*
    *   Review of Systems form 
    */
    
    //TODO: Comment section should only be visible when Abnormal is selected for the system
    
    /*
    *   Physical Exam form
    */
    
    //TODO: Same as Review of Systems
    
    /*
    *   Form Validation
    */
    
    //TODO: Add form validation for History & Physical
    
})

$(document).ready(function() {

  
        $("#myBtn").click(function(){
            $(this).text(function(i, v){
                return v === 'View All' ? 'Close All' : 'View All'
             });
        ExpandCollapse(); });

        function ExpandCollapse(){
            currentvalue = document.getElementById('myBtn').value;
            if(currentvalue == "View All"){
              document.getElementById("myBtn").value="Collapse All";
              $("#collapseOne,#collapseTwo,#collapseThree,#collapseFour,#collapseFive,#collapseSix,#collapseSeven,#collapseEight").collapse("show");
            }else{
              document.getElementById("myBtn").value="View All";
              $("#collapseOne,#collapseTwo,#collapseThree,#collapseFour,#collapseFive,#collapseSix,#collapseSeven,#collapseEight").collapse("hide");
            }
          }
})
  
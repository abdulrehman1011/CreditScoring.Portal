





$(document).ready(function () {
    var doc = new jsPDF();

    // We'll make our own renderer to skip this editor
    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

    $('#cmd').click(function () {
        doc.fromHTML($('#pdf-content').get(0), 10, 10, {
            'width': 950,
            'elementHandlers': specialElementHandlers
        });
        doc.save('sample-file.pdf');
    });

   
  
});
//$('#cmd').click(() => {
//    var pdf = new jsPDF('p', 'pt');
   
//    //$("#pdf-content").css("background", "black");
//    pdf.addHTML($("#pdf-content")[0], 10, 10, {
//        'width': 350,
//        'margin': 50,
//        'pagesplit': true
//    }, function () {
//        pdf.save('web.pdf');
//    });
    
//})
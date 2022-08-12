$(function () {
    $('#btnSubmit').on('click', function () {

        var fileExtension = ['csv'];
        var filename = $('#fUpload').val();

        if (filename.length === 0) {
            alert("Please select a file to upload.");
            return false;
        }
        else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) === -1) {
                alert("Please upload .csv files only.");
                return false;
            }
        }

        var fdata = new FormData();
        var fileUpload = $("#fUpload").get(0);
        var files = fileUpload.files;
        fdata.append("file", files[0]);

        $.ajax({
            type: "POST",
            url: "api/UploadMeterReadingAPI/meter-reading-uploads",
            data: fdata,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.length === null)
                    alert('An error occured uploading the file.');
                else {
                    $("#successfulReadingsCount").text(response.successfulReadingsCount);
                    $("#unsuccessfulReadingsCount").text(response.failedReadingsCount);
                }
            }
        });

    });
});
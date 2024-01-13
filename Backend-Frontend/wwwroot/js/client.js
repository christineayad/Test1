var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'Client/getall'},
        "columns": [
            { data: 'firstName', "width": "20%" },
            { data: 'lastName', "width": "20%" },

            { data: 'dateBirth', "width": "20%" },
            { data: 'status', "width": "20%" },
            { data: 'mobile', "width": "20%" },
            { data: 'email', "width": "20%" },
       
          
            /*{ data: 'iamge', "width": "10%" },*/
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="Client/AddEdit?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                   
                     <a onClick=Delete('Client/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    //toastr.success(data.message);
                }
            })
        }
    })
}

//select file - image
$('#blah').click(function () {
    $('#imgInp').click();
})

//preview the image
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgInp").change(function () {
    readURL(this);
});
//Time Picker

// Single Date with time
$(document).ready(function () {
    $('#date-time-picker').daterangepicker({
        singleDatePicker: true,
        timePicker: true,
        timePickerIncrement: 5,
        locale: {
            format: 'YYYY-MM-DD'
        }
    });
});

//function Search() {
//    var searchString = document.getElementById('searchterm').value;

//    // Send AJAX request to the server
//    var xhr = new XMLHttpRequest();
//    xhr.onreadystatechange = function () {
//        if (xhr.readyState === 4 && xhr.status === 200) {
//            document.getElementById('clientTableContainer').innerHTML = xhr.responseText;
//        }
//    };
//    var url = '@Url.Action("Search", "Client")';
//    url += '?searchString= ' + encodeURIComponent(searchString);

//    xhr.open('GET', url, true);
//    xhr.send();
//}
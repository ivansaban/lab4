// Write your JavaScript code.
function getStores() {
    var data = {
        firstName: 'Gustavo',
        phone: '398-555-0132'
    };

    var id = document.getElementById('id').value;

    if (id) {
        $.ajax({
            type: 'get',
            url: '/api/Stores/' + id,
            success: function (result, jqXHR) {
                $('#table').empty();
                $('#table').append('<tr><th>Name</th><th>Id</th><th>Bonus</th></tr>');
                
                if (result.salesPerson) {
                    var $tr = $('<tr>').append(
                        $('<td>').text(result.name),
                        $('<td>').text(result.businessEntityId),
                        $('<td>').text(result.salesPerson.bonus)
                    ).appendTo('#table');
                } else {
                    var $tr = $('<tr>').append(
                        $('<td>').text(result.name),
                        $('<td>').text(result.businessEntityId),
                        $('<td>').text("")
                    ).appendTo('#table');
                }
            },
            error: function (result, jqXHR) {
                $('#table').empty();
            }
        });
    } else {
        $.ajax({
            type: 'post',
            url: '/api/Token/RequestToken',
            contentType: "application/json;",
            data: JSON.stringify(data),
            success: function (result, jqXHR) {
                $.ajax({
                    type: 'get',
                    url: '/api/Stores',
                    beforeSend: function (request) {
                        request.setRequestHeader('Authorization', 'bearer ' + result);
                    },
                    success: function (result, jqXHR) {
                        $('#table').empty();
                        $('#table').append('<tr><th>Name</th><th>Id</th><th>Bonus</th></tr>');
                        var i;
                        $(function () {
                            $.each(result, function (i, item) {
                                if (item.salesPerson) {
                                    var $tr = $('<tr>').append(
                                        $('<td>').text(item.name),
                                        $('<td>').text(item.businessEntityId),
                                        $('<td>').text(item.salesPerson.bonus)
                                    ).appendTo('#table');
                                } else {
                                    var $tr = $('<tr>').append(
                                        $('<td>').text(item.name),
                                        $('<td>').text(item.businessEntityId),
                                        $('<td>').text("")
                                    ).appendTo('#table');
                                }
                            });
                        });
                    }
                });
            }
        });
    }
}
$(function () {
    $('#modalFormSubmit').on('click', function (e) {
        e.preventDefault();

        var array = $("#manModalForm input").serializeArray();
        var jsona = {};

        jQuery.each(array, function () {
            jsona[this.name] = this.value || '';
        });

        //alert(json);
        $.ajax({
            type: "POST",
            url: "/api/webapi/AddManufacturer",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(jsona),

            success: function (response) {
                $('#exampleModal').modal('hide');
                var x = document.getElementById("Manufacturer_Id");
                var option = document.createElement("option");
                option.text = jsona["Name"];
                option.value = response["ItemId"];
                x.add(option);
                x.value = response["ItemId"];
            },
            error: function () {
                alert('Error');
            }
        });
        return false;
    });

    // Type form handling
    $('#typModalFormSubmit').on('click', function (e) {
        e.preventDefault();

        var array = $("#typModalForm input").serializeArray();
        var jsona = {};

        jQuery.each(array, function () {
            jsona[this.name] = this.value || '';
        });

        //alert(json);
        $.ajax({
            type: "POST",
            url: "/api/webapi/AddType",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(jsona),

            success: function (response) {
                $('#typModal').modal('hide');
                var x = document.getElementById("Type_Id");
                var option = document.createElement("option");
                option.text = jsona["Name"];
                option.value = response["ItemId"];
                x.add(option);
                x.value = response["ItemId"];
            },
            error: function () {
                alert('Error');
            }
        });
        return false;
    });

    // Location form handling
    $('#locModalFormSubmit').on('click', function (e) {
        e.preventDefault();

        var array = $("#locModalForm input").serializeArray();
        var jsona = {};

        jQuery.each(array, function () {
            jsona[this.name] = this.value || '';
        });

        //alert(json);
        $.ajax({
            type: "POST",
            url: "/api/webapi/AddLocation",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(jsona),

            success: function (response) {
                $('#locModal').modal('hide');
                var x = document.getElementById("Location_Id");
                var option = document.createElement("option");
                option.text = jsona["Name"];
                option.value = response["ItemId"];
                x.add(option);
                x.value = response["ItemId"];
            },
            error: function () {
                alert('Error');
            }
        });
        return false;
    });

    // Package form handling
    $('#pacModalFormSubmit').on('click', function (e) {
        e.preventDefault();

        var array = $("#pacModalForm input").serializeArray();
        var jsona = {};

        jQuery.each(array, function () {
            jsona[this.name] = this.value || '';
        });

        //alert(json);
        $.ajax({
            type: "POST",
            url: "/api/webapi/AddPackage",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(jsona),

            success: function (response) {
                $('#pacModal').modal('hide');
                var x = document.getElementById("Package_Id");
                var option = document.createElement("option");
                option.text = jsona["Name"];
                option.value = response["ItemId"];
                x.add(option);
                x.value = response["ItemId"];
            },
            error: function () {
                alert('Error');
            }
        });
        return false;
    });
});

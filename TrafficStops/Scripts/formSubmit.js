var duplicateValues;

var values = {};

function handleReceivedData(data, values) {

    
    $("#query-results").toggle("slow", function () {

        //$("#data-load").toggle();
    });

    console.log("data: ", data);

    console.log("values: ", values);



    var queryBarHeight = 300 * (data[0] * 0.01);
    if (queryBarHeight < 28) {
        $("#query-num").css("top", "0px");
    }

    var averageBarHeight;
    var averageNum;

    if (values["Result"] == "WARNING") {
        averageBarHeight = 72;
        averageNum = 24;
    }

    if (values["Result"] == "CITATION ISSUED") {
        averageBarHeight = 228;
        averageNum = 76;
    }





    $("#query-bar").css("height", queryBarHeight + "px");
    $("#query-num").append(data[0] + "%");

    $("#average-bar").css("height", averageBarHeight + "px");
    $("#average-num").append(averageNum + "%");


    var driverParameters = "";
    var officerParameters = "";

    if (values["RaceDriver"] != "") {
        driverParameters += values["RaceDriver"] + ", ";
    }
    
    if (values["GenderDriver"] != "") {
        driverParameters += values["GenderDriver"] + ", ";
    }
    
    if (values["AgeDriver"] != "") {
        driverParameters += values["AgeDriver"] + "yrs";
    } 




    if (values["RaceOfficer"] != "") {
        officerParameters += values["RaceOfficer"] + ", ";
    }
    
    if (values["GenderOfficer"] != "") {
        officerParameters += values["GenderOfficer"] + ", ";
    } 
    if (values["AgeOfficer"] != "" ) {
        officerParameters += values["AgeOfficer"] + "yrs";
    } 


    if (officerParameters[officerParameters.length - 1] == " " && officerParameters[officerParameters.length - 2] == ",") {
        officerParameters = officerParameters.slice(0, -2);
    }

    if (driverParameters[driverParameters.length - 1] == " " && driverParameters[driverParameters.length - 2] == ",") {
        driverParameters = driverParameters.slice(0, -2);
    }


    $("#data-load").toggle();
    $("#heading-for-graph").append(" " + values["Result"]);
    $("#records-found").append("(" + data[2] + " records found)");


    $("#result-parameters").append(
         "<div><span>*Officer: </span>" + officerParameters + "</div>" +
         "<span>*Driver: </span>" + driverParameters

        );



    $("#download-section").toggle();

}


function onClickSend() {
    $(document.body).on("click", "#submit-query", function (e) {
        e.preventDefault();

        sendRequest(getRadioValues());

    })
}

function onClickSearchAgain() {
    $(document.body).on("click", "#search-again", function (e) {
        e.preventDefault();

        values = {};

        $("#query-num").empty();
        $("#average-num").empty();
        $("#form-section").toggle("slow");
        $("#query-results").toggle("slow");
        $("#download-section").toggle();
        $("#result-parameters").empty();
        $("#records-found").empty();
        $("#heading-for-graph").empty();

    })

}

function setFormValues(values) {

    $("#dl-race-driver").attr("value", values["RaceDriver"]);
    $("#dl-gender-driver").attr("value", values["GenderDriver"]);
    $("#dl-age-driver").attr("value", values["AgeDriver"]);
    $("#dl-race-officer").attr("value", values["RaceOfficer"]);
    $("#dl-gender-officer").attr("value", values["GenderOfficer"]);
    $("#dl-age-officer").attr("value", values["AgeOfficer"]);
    $("#dl-result").attr("value", values["Result"]);



}


//url: "/api/StopVarApi?parameters="+JSON.stringify(values),
function sendRequest(values) {
    //console.log(values);
    if (values["Result"] == undefined) {
        $("#result-validation").toggle("slow");
        return;
    }
    
    $("#form-section").toggle("slow", function () {
        $("#data-load").toggle();
    });
    

    console.log(values);
    setFormValues(values);

    $.ajax({
        url: "/api/StopVarApi",
        type: "GET",
        data: values,
        contentType: 'application/json; charset=utf-8',
        cache: false,
        dataType: 'json'
    }).done(function (data) {
        duplicateValues = values;
        handleReceivedData(data, values);

    });

    //THIS DIDNT WORK BECAUSE THE JSON OBJECT WAS STRINGIFIED BEFOREHAND
    //    var request = $.ajax({
    //        url: http://mydomain.com/case,
    //        type: 'GET',
    //        data: JSON.stringify(filter),
    //        contentType: 'application/json; charset=utf-8',
    //    cache: false,
    //    dataType: 'json'
    //});


}

var getRadioValues = function () {

    var raceDriver = $("#drop-race-driver").val();
    var genderDriver = $("#drop-gender-driver").val();
    var ageDriver = $("#drop-age-driver").val();
    var raceOfficer = $("#drop-race-officer").val();
    var genderOfficer = $("#drop-gender-officer").val();
    var ageOfficer = $("#drop-age-officer").val();




    var result = $("input[name='result']:checked").val();

    //var wasVehicleSearched = $("input[name='result']:checked").val();

    values["RaceDriver"] = raceDriver;
    values["GenderDriver"] = genderDriver;
    values["AgeDriver"] = ageDriver;

    values["RaceOfficer"] = raceOfficer;
    values["GenderOfficer"] = genderOfficer;
    values["AgeOfficer"] = ageOfficer;

    values["Result"] = result;
    console.log(values);


    return values;

    
}

function hidingElements() {
    $(document.body).on("click", "#driver-params-button", function () {

        $("#driver-params").toggle("slow");

    });

    $(document.body).on("click", "#officer-params-button", function () {

        $("#officer-params").toggle("slow");

    });

    $(document.body).on("click", "#specify-driver-race", function () {

        $("#driver-race").toggle("slow");

    });

    $(document.body).on("click", "#specify-driver-gender", function (e) {
        $("#driver-gender").toggle("slow");

    });

    $(document.body).on("click", "#specify-driver-age", function (e) {
        $("#driver-age").toggle("slow");

    });



    $(document.body).on("click", "#specify-officer-race", function () {

        $("#officer-race").toggle("slow");

    });

    $(document.body).on("click", "#specify-officer-gender", function () {

        $("#officer-gender").toggle("slow");

    });

    $(document.body).on("click", "#specify-officer-age", function () {

        $("#officer-age").toggle("slow");

    });

    $(document.body).on("click", "#info", function () {

        $(".paragraph").toggle("slow");

    });

}


function MakeCommentsTable(result) {


    $("#tabel_id").DataTable({
        "bPaginate": false,
        "order": [[1, "desc"]],
        "bFilter": false,
        "bInfo" : false,
        data: result,
        columns: [
            {
                data: [0]
            },
            {
                data: [1]
            },
            {
                data: [2]
            },
            {
                data: [3]
            },
            {
                data: [4]
            },

            {
                data: [5]
            },
            {
                data: [6]
            },
            {
                data: [7]
            },
            {
                data: [8]
            },
            {
                data: [9]
            },
            {
                data: [10]
            },
            {
                data: [11]
            },
            {
                data: [12]
            },
            {
                data: [13]
            },
            {
                data: [14]
            },
            {
                data: [15]
            },
            {
                data: [16]
            },
            {
                data: [17]
            }
        ]
    });

}


onClickSend();
hidingElements();
onClickSearchAgain()
$(function () {
    //Set the results div to the loading icon
    $('#RollDiceResultsDiv').html($('#LoadingIconSection').html());

    //Sample
    //$.get().done(function (data) {
    //    $.fn.GeneratePieChart('Title', data, 'chartDivId').done(function () {
    //        $('#chartLoadDiv').fadeOut('fast', function () {
    //            $('#chartDivId').fadeIn('fast');
    //        });
    //    });
    //});
});

$('#RollDiceRequestForm').on('submit', function (event) {
    event.preventDefault();

    let formData = $('#RollDiceRequestForm').serialize();
    let formAction = $('#RollDiceRequestForm').attr('action');

    $('#RollDiceRequestDiv').fadeOut('fast', function () {
        $('#RollDiceResultsDiv').fadeIn('fast');
        $.post(formAction, formData).done(function (returnData) {
            $('#RollDiceResultsDiv').fadeOut('fast', function () {
                $('#RollDiceResultsDiv').html(returnData);
                $('#RollDiceResultsDiv').fadeIn('fast');
            });
        });
    });
});

$('#RollDiceModal').on('hidden.bs.modal', function () {
    $('#RollDiceResultsDiv').html($('#LoadingIconSection').html());
    $('#RollDiceResultsDiv').hide();
    $('#RollDiceRequestDiv').show();
    $('#RollDiceRequestForm')[0].reset();
});

$.fn.BuildDatasets = function (labels, dataSet) {
    let convertedDataSet = [];

    for (let i = 0; i < dataSet.length; i++) {
        let curRec = dataSet[i];
        let curData = [];

        for (let j = 0; j < labels.length; j++) {
            let index = curRec.Data.map(row => row.Key).indexOf(labels[j]);
            if (index < 0) {
                curData[j] = 0;
            }
            else {
                curData[j] = curRec.Data[index].Value;
            }
        }
        convertedDataSet[i] = { label: curRec.Name, data: curData };
    }

    return {
        labels: labels,
        dataSets: convertedDataSet
    };
};

$.fn.GeneratePieChart = function (title, ChartData, chartId) {
    new Chart(
        document.getElementById(chartId),
        {
            type: 'doughnut',
            data: {
                labels: ChartData.map(row => row.Key),
                datasets: [
                    {
                        data: ChartData.map(row => row.Value)
                    }
                ]
            },
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: title
                    }
                }
            }
        }
    );
};

$.fn.GenerateComboBarChart = function (title, ChartsData, chartId) {
    new Chart(
        document.getElementById(chartId),
        {
            type: 'bar',
            data: {
                labels: ChartsData.labels,
                datasets: ChartsData.dataSets
            },
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: title
                    }
                }
            }
        }
    );
};

$.fn.GenerateComboLineChart = function (title, ChartsData, chartId) {
    new Chart(
        document.getElementById(chartId),
        {
            type: 'line',
            data: {
                labels: ChartsData.labels.map(row => row.split('|')[0]),
                datasets: ChartsData.dataSets
            },
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: title
                    }
                }
            }
        }
    );
};

/*
$(document).ready(function () {
    $.fn.GeneratePieChart('Total Leads Received', $.parseJSON($('#LeadReceivedChartData').val()), 'TotalLeadsReceivedChart')

    $.fn.GeneratePieChart('Total Call Attempt Results', $.parseJSON($('#CallsMadeChartData').val()), 'TotalCallAttemptsChart')

    $.fn.GeneratePieChart('Total Call Attempts In Respect To Standard Call Types', $.parseJSON($('#StandardScheduledOverallChartData').val()), 'StandardScheduledOverallChart');

    let statusLabels = $.parseJSON($('#CallsMadeChartData').val()).map(row => row.Key);

    let multipointDataSetCallDir = [
        {
            Name: 'Inbound Calls',
            Data: $.parseJSON($('#InboundCallsMadeChartData').val())
        },
        {
            Name: 'Outbound Calls',
            Data: $.parseJSON($('#OutboundCallsMadeChartData').val())
        },
        {
            Name: 'Automated Calls',
            Data: $.parseJSON($('#AutomatedCallsMadeChartDate').val())
        }
    ];

    $.fn.GenerateComboBarChart('Call Direction Comparison', $.fn.BuildDatasets(statusLabels, multipointDataSetCallDir), 'CallAttemptsDirectionChart');

    let multipointDataSetCallType = [];

    $('.StandardCallTypeData').each(function (index) {
        multipointDataSetCallType[index] = {
            Name: $(this).attr('tableName'),
            Data: $.parseJSON($(this).val())
        }
    });

    $.fn.GenerateComboBarChart('Scheduled Call Types Comparison', $.fn.BuildDatasets(statusLabels, multipointDataSetCallType), 'StandardCallTypesChart');

    let curMarkCamp = $('#MarketingCampaignId').val();
    console.log(curMarkCamp);
    $.get('GetHistoricalData', { MarketingCampaignId: curMarkCamp }).done(function (data) {
        $('#historydataholder').fadeOut('fast', function () {
            $('#historydataholder').html(data);
            let multipointDataSetHistorical = [];
            let statusLabelsHistorical = $.parseJSON($('#HistoricalRecordLabels').val());
            $('.HistoricalRecordData').each(function (index) {
                multipointDataSetHistorical[index] = {
                    Name: $(this).attr('tableName'),
                    Data: $.parseJSON($(this).val())
                }
            });
            $.fn.GenerateComboLineChart('Historical Status Counts By Month', $.fn.BuildDatasets(statusLabelsHistorical, multipointDataSetHistorical), 'HistoricalDataChart');

            $('#historydataholder').fadeIn('fast');
        });
    });
});
*/